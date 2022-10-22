using OCRDesktopLauncher;
using System;
using System.Drawing;
using System.Windows.Forms;

class SpawnInitialRectangle : ContextHandler
{
	private Point cursorInitialPoint = Point.Empty;

	private void DefineRectangleLocation()
	{
		Form.Location = cursorInitialPoint = Cursor.Position;
		IsMouseDown = true;

		IgnoreRectangleBorder = false;
		Form.Cursor = Cursors.Cross;
	}

	private void SpawnRectangle()
	{
		IsMouseDown = false;
		Form.ContextHandler = new ResizeExistingRectangle(Form);
	}

	private void DefineRectangleDimension()
	{
		if (!IsMouseDown) return;

		int mouseX = Cursor.Position.X;
		int mouseY = Cursor.Position.Y;
		int iniX = cursorInitialPoint.X;
		int iniY = cursorInitialPoint.Y;

		#region Natural direction
		// Directions for Right
		int width = mouseX - iniX;
		// Directions for Top
		int height = mouseY - iniY;
		#endregion

		#region Mirrored direction
		int iniXPosition, iniYPosition;
		int xPosition = iniXPosition = Form.Location.X;
		int yPosition = iniYPosition = Form.Location.Y;

		// Directions for Left
		if (mouseX < iniX)
			xPosition = iniX + width;
		// Directions for Top
		if (mouseY < iniY)
			yPosition = iniY + height;

		if (xPosition != iniXPosition || yPosition != iniYPosition)
			Form.Location = new Point(xPosition, yPosition);
		#endregion

		Form.Size = new Size(Math.Abs(width), Math.Abs(height));
	}

	public SpawnInitialRectangle(Launcher form) : base(form)
	{
		form.Cursor = Cursors.NoMove2D;
		form.Size = Screen.PrimaryScreen.Bounds.Size;

		IgnoreRectangleBorder = true;
	}

	public override void OnMouseDown() => DefineRectangleLocation();

	public override void OnMouseUp() => SpawnRectangle();

	public override void OnMouseMove() => DefineRectangleDimension();
}