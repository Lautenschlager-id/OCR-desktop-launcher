namespace OCRDesktopLauncher
{
	partial class Launcher
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
			this.SuspendLayout();
			// 
			// Launcher
			// 
			this.BackColor = System.Drawing.Color.MintCream;
			this.ClientSize = new System.Drawing.Size(250, 250);
			this.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Launcher";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.MintCream;
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			this.ResumeLayout(false);

		}

		#endregion

	}
}

