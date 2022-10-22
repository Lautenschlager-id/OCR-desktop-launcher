using System.Drawing;
using System.Windows.Forms;

static class ResizeTool
{
	public static int BorderThickness { get; set; } = 10;

	public enum Direction {
		None = 1,
		Left = 10,
		Right = 11,
		Top = 12,
		TopLeft = 13,
		TopRight = 14,
		Bottom = 15,
		BottomLeft = 16,
		BottomRight = 17
	};

	public static Direction GetDirection(Point mouseLocation, Form form)
	{
		bool nearTop = mouseLocation.Y <= BorderThickness;
		bool nearBottom = mouseLocation.Y >= (form.ClientRectangle.Height - BorderThickness);

		bool nearLeft = mouseLocation.X <= BorderThickness;
		bool nearRight = mouseLocation.X >= (form.ClientRectangle.Width - BorderThickness);

		if (nearTop)
		{
			if (nearLeft)
				return Direction.TopLeft;
			else if (nearRight)
				return Direction.TopRight;
			else
				return Direction.Top;
		}
		else if (nearBottom)
		{
			if (nearLeft)
				return Direction.BottomLeft;
			else if (nearRight)
				return Direction.BottomRight;
			else
				return Direction.Bottom;
		}
		else if (nearLeft)
			return Direction.Left;
		else if (nearRight)
			return Direction.Right;
		return Direction.None;
	}
}