using OCRDesktopLauncher;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public abstract class ContextHandler
{
	internal Launcher Form { get; set; }

	internal bool IsMouseDown { get; set; } = false;

	#region Border
	internal bool IgnoreRectangleBorder { get; set; } = true;
	internal Pen WindowBoundaryBorderPen { get; set; }
	#endregion

	private void SetUpBorderLayout()
	{
		WindowBoundaryBorderPen = new Pen(Color.White, 2)
		{
			DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
		};
	}

	private void DrawBorder(PaintEventArgs evt)
	{
		if (IgnoreRectangleBorder) return;
		evt.Graphics.DrawRectangle(WindowBoundaryBorderPen, evt.ClipRectangle);
	}

	private void EndProcess()
	{
		Process.GetCurrentProcess().Kill();
	}

	public ContextHandler(Launcher form)
	{
		this.Form = form;
		SetUpBorderLayout();
	}

	public virtual void OnMouseDown() { }

	public virtual void OnMouseUp() { }

	public virtual void OnMouseMove() { }

	public virtual void OnPaint(PaintEventArgs evt) => DrawBorder(evt);

	public virtual void OnKeyUp(KeyEventArgs evt)
	{
		switch (evt.KeyCode)
		{
			case Keys.Escape:
				EndProcess();
				break;
		}
	}

	public virtual bool WndProc(ref Message msg)
	{
		return false;
	}
}