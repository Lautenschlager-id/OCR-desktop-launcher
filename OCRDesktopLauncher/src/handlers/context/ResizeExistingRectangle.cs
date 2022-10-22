using OCRDesktopLauncher;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

class ResizeExistingRectangle : ContextHandler
{
	private Point cursorPositionOnMouseDownRelativeToTheWindow = Point.Empty;
	private bool IsProcessingImage { get; set; } = false;

	private void SelectRectangleToMove()
	{
		if (IsProcessingImage) return;

		cursorPositionOnMouseDownRelativeToTheWindow.X = Cursor.Position.X - Form.Left;
		cursorPositionOnMouseDownRelativeToTheWindow.Y = Cursor.Position.Y - Form.Top;

		IsMouseDown = true;
	}

	private void MoveRectangle()
	{
		if (!IsMouseDown) return;

		Form.Left = Cursor.Position.X - cursorPositionOnMouseDownRelativeToTheWindow.X;
		Form.Top = Cursor.Position.Y - cursorPositionOnMouseDownRelativeToTheWindow.Y;
	}

	private void CheckMouseAtResizablePosition(ref Message msg)
	{
		// MouseEventArgs.Location.X
		int x = (int)(msg.LParam.ToInt64() & 0xFFFF);
		// MouseEventArgs.Location.Y
		int y = (int)((msg.LParam.ToInt64() & 0xFFFF0000) >> 16);

		Point mouseLocation = Form.PointToClient(new Point(x, y));
		// Resize direction
		msg.Result = (IntPtr)ResizeTool.GetDirection(mouseLocation, Form);
	}

	private async Task TransformRectangleAreaIntoText()
	{
		if (IsProcessingImage) return;

		Cursor previousCursor = Form.Cursor;
		Point previousLocation = Form.Location;
		Size previousSize = Form.Size;
		
		Form.Cursor = Cursors.WaitCursor;

		IsProcessingImage = true;
		// Removes border while screenshoting so that it doesn't affect the image processing
		IgnoreRectangleBorder = true;
		Form.Refresh();

		try
		{
			#region Screenshot
			ScreenCapture.CaptureActiveWindow();

			// Changes the selection area so that the waiting cursor is displayed at any point
			// of the screen to notify the user that the process is on-going
			Form.Location = Point.Empty;
			Form.Size = Screen.PrimaryScreen.Bounds.Size;

			bool hasSaved =
				ScreenCapture.Save(GoogleContentVisionTransformRequest.ImagePath, ImageFormat.Jpeg);
			if (!hasSaved)
				throw new Exception("Capture couldn't be saved");
			#endregion

			#region Transform image into text
			string response = await GoogleContentVision.TransformIntoText.Request() as string;
			if (response == null)
				throw new Exception("No text identified in capture");
			Clipboard.SetText(response);
			#endregion
		}
		finally
		{
			// Brings back the border,
			// it also gets updated automatically because of the form updates below
			IgnoreRectangleBorder = false;

			Form.Location = previousLocation;
			Form.Size = previousSize;
			
			Form.Cursor = previousCursor;

			IsProcessingImage = false;
		}
	}

	public ResizeExistingRectangle(Launcher form) : base(form)
	{
		form.Cursor = Cursors.SizeAll;
		// Needs a minimum so that it still can be resized
		form.MinimumSize = new Size(50, 18);

		IgnoreRectangleBorder = false;
	}

	public override void OnMouseDown() => SelectRectangleToMove();

	public override void OnMouseUp()
	{
		// Ends the selection of the rectangle
		IsMouseDown = false;
	}

	public override void OnMouseMove() => MoveRectangle();

	public override void OnKeyUp(KeyEventArgs evt)
	{
		switch (evt.KeyCode)
		{
			case Keys.Enter:
				if (!IsProcessingImage)
					Task.FromResult(TransformRectangleAreaIntoText());
				break;
		}

		base.OnKeyUp(evt);
	}

	public override bool WndProc(ref Message msg)
	{
		if (msg.Msg != 0x84 || IgnoreRectangleBorder)
			return false;

		CheckMouseAtResizablePosition(ref msg);
		return true;
	}
}