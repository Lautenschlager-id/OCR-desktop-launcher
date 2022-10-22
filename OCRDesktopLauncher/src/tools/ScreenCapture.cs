using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

public static class ScreenCapture
{
	private static Image currentImage;

	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern IntPtr GetDesktopWindow();

	[StructLayout(LayoutKind.Sequential)]
	private struct Rect
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

	public static void CaptureDesktop()
	{
		CaptureWindow(GetDesktopWindow());
	}

	public static void CaptureActiveWindow()
	{
		CaptureWindow(GetForegroundWindow());
	}

	public static void CaptureWindow(IntPtr handle)
	{
		Rect rect = new Rect();
		GetWindowRect(handle, ref rect);
		Rectangle bounds =
			new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
		Bitmap result = new Bitmap(bounds.Width, bounds.Height);

		using (Graphics graphics = Graphics.FromImage(result))
		{
			graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
		}

		currentImage = result;
	}

	public static bool Save(string fileName, ImageFormat imageFormat)
	{
		if (currentImage == null)
			return false;

		using (FileStream fs = File.Create(fileName))
		{
			currentImage.Save(fs, imageFormat);
			currentImage = null;
			return true;
		}
	}
}