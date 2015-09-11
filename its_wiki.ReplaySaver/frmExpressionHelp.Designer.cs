using HtmlRenderer;
namespace its_wiki.ReplaySaver
{
	partial class frmExpressionHelp
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpressionHelp));
			this.htmlDisplay = new HtmlPanel();
			this.SuspendLayout();
			// 
			// htmlDisplay
			// 
			this.htmlDisplay.AutoScroll = true;
			this.htmlDisplay.AutoScrollMinSize = new System.Drawing.Size(75, 17);
			this.htmlDisplay.BackColor = System.Drawing.SystemColors.Window;
			this.htmlDisplay.BaseStylesheet = null;
			this.htmlDisplay.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.htmlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.htmlDisplay.Location = new System.Drawing.Point(0, 0);
			this.htmlDisplay.Name = "htmlDisplay";
			this.htmlDisplay.Size = new System.Drawing.Size(537, 338);
			this.htmlDisplay.TabIndex = 0;
			// 
			// frmExpressionHelp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 338);
			this.Controls.Add(this.htmlDisplay);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmExpressionHelp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Expression help";
			this.ResumeLayout(false);

		}

		#endregion

		private HtmlRenderer.HtmlPanel htmlDisplay;
	}
}