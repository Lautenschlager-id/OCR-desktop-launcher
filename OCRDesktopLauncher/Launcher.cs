using System.Windows.Forms;

namespace OCRDesktopLauncher
{
	public partial class Launcher : Form
	{
		public ContextHandler ContextHandler;

		public Launcher()
		{
			InitializeComponent();

			FormBorderStyle = FormBorderStyle.None;

			DoubleBuffered = true;
			SetStyle(ControlStyles.ResizeRedraw, true);

			ContextHandler = new SpawnInitialRectangle(this);
		}

		private void OnMouseDown(object sender, MouseEventArgs evt)
		{
			ContextHandler.OnMouseDown();
		}

		private void OnMouseUp(object sender, MouseEventArgs evt)
		{
			ContextHandler.OnMouseUp();
		}

		private void OnMouseMove(object sender, MouseEventArgs evt)
		{
			ContextHandler.OnMouseMove();
		}

		private void OnPaint(object sender, PaintEventArgs evt)
		{
			ContextHandler.OnPaint(evt);
		}

		private void OnKeyUp(object sender, KeyEventArgs evt)
		{
			ContextHandler.OnKeyUp(evt);
		}

		protected override void WndProc(ref Message msg)
		{
			if (!ContextHandler.WndProc(ref msg))
				base.WndProc(ref msg);
		}
	}
}
