namespace its_wiki.ReplaySaver
{
	partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.label1 = new System.Windows.Forms.Label();
			this.txtSaveBase = new System.Windows.Forms.TextBox();
			this.btnBrowseBase = new System.Windows.Forms.Button();
			this.txtExpression = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseReplay = new System.Windows.Forms.Button();
			this.txtLastReplay = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.ttMain = new System.Windows.Forms.ToolTip(this.components);
			this.htmlMessages = new HtmlRenderer.HtmlPanel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 100;
			this.label1.Text = "Save base path:";
			this.ttMain.SetToolTip(this.label1, "The base directory where the replay files are stored.\r\n(Subdirectories can be cre" +
        "ated, but that should be done within the expression).");
			// 
			// txtSaveBase
			// 
			this.txtSaveBase.Location = new System.Drawing.Point(103, 38);
			this.txtSaveBase.Name = "txtSaveBase";
			this.txtSaveBase.Size = new System.Drawing.Size(420, 20);
			this.txtSaveBase.TabIndex = 2;
			// 
			// btnBrowseBase
			// 
			this.btnBrowseBase.Location = new System.Drawing.Point(529, 38);
			this.btnBrowseBase.Name = "btnBrowseBase";
			this.btnBrowseBase.Size = new System.Drawing.Size(25, 20);
			this.btnBrowseBase.TabIndex = 3;
			this.btnBrowseBase.Text = "...";
			this.btnBrowseBase.UseVisualStyleBackColor = true;
			this.btnBrowseBase.Click += new System.EventHandler(this.btnBrowseBase_Click);
			// 
			// txtExpression
			// 
			this.txtExpression.Location = new System.Drawing.Point(103, 64);
			this.txtExpression.Name = "txtExpression";
			this.txtExpression.Size = new System.Drawing.Size(451, 20);
			this.txtExpression.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(36, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 101;
			this.label2.Text = "Expression:";
			this.ttMain.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
			// 
			// btnBrowseReplay
			// 
			this.btnBrowseReplay.Location = new System.Drawing.Point(529, 12);
			this.btnBrowseReplay.Name = "btnBrowseReplay";
			this.btnBrowseReplay.Size = new System.Drawing.Size(25, 20);
			this.btnBrowseReplay.TabIndex = 1;
			this.btnBrowseReplay.Text = "...";
			this.btnBrowseReplay.UseVisualStyleBackColor = true;
			this.btnBrowseReplay.Click += new System.EventHandler(this.btnBrowseReplay_Click);
			// 
			// txtLastReplay
			// 
			this.txtLastReplay.Location = new System.Drawing.Point(103, 12);
			this.txtLastReplay.Name = "txtLastReplay";
			this.txtLastReplay.Size = new System.Drawing.Size(420, 20);
			this.txtLastReplay.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(36, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 99;
			this.label3.Text = "Last replay:";
			this.ttMain.SetToolTip(this.label3, "The Last Replay.KWReplay file (may differ on non English users).\r\nThe ReplaySaver" +
        " will monitor this file for changes");
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(560, 12);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(46, 72);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// ttMain
			// 
			this.ttMain.AutoPopDelay = 30000;
			this.ttMain.InitialDelay = 500;
			this.ttMain.ReshowDelay = 100;
			this.ttMain.ShowAlways = true;
			// 
			// htmlMessages
			// 
			this.htmlMessages.AutoScroll = true;
			this.htmlMessages.BackColor = System.Drawing.SystemColors.Window;
			this.htmlMessages.BaseStylesheet = null;
			this.htmlMessages.Cursor = System.Windows.Forms.Cursors.Default;
			this.htmlMessages.Location = new System.Drawing.Point(12, 90);
			this.htmlMessages.Name = "htmlMessages";
			this.htmlMessages.Size = new System.Drawing.Size(594, 254);
			this.htmlMessages.TabIndex = 102;
			this.htmlMessages.Text = null;
			this.htmlMessages.LinkClicked += new System.EventHandler<HtmlRenderer.Entities.HtmlLinkClickedEventArgs>(this.htmlMessages_LinkClicked);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(618, 356);
			this.Controls.Add(this.htmlMessages);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnBrowseReplay);
			this.Controls.Add(this.txtLastReplay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtExpression);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBrowseBase);
			this.Controls.Add(this.txtSaveBase);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ReplaySaver";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSaveBase;
		private System.Windows.Forms.Button btnBrowseBase;
		private System.Windows.Forms.TextBox txtExpression;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseReplay;
		private System.Windows.Forms.TextBox txtLastReplay;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ToolTip ttMain;
		private HtmlRenderer.HtmlPanel htmlMessages;
	}
}

