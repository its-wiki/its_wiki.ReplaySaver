using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace its_wiki.ReplaySaver
{
	public partial class frmConfig : Form
	{
		private Action<object> Saved = null;
		public frmConfig(object Config, Action<object> Saved)
		{
			InitializeComponent();
			this.Text = "Config \"" + Config.GetType().Name + "\"";
			this.pgMain.SelectedObject = Config;
			this.Saved = Saved;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Saved(pgMain.SelectedObject);
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
