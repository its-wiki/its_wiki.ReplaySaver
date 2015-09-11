using its_wiki.ReplaySaver.PluginSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace its_wiki.ReplaySaver
{
	public partial class frmExpressionHelp : Form
	{
		public frmExpressionHelp(IEnumerable<RegexCommand> avail_regexes)
		{
			InitializeComponent();

			StringBuilder html = new StringBuilder();

			html.Append("<html><head><style>body { font-family: Calibri; width: 100%; height: 100%; padding: 2px; margin: 0px; } td { border-bottom: 1px solid black; } th { background-color: #333333; color: #F5F5F5 } .header { background-color: #333333; color: #F5F5F5 } .odd { background-color: #E5E5E5; }</style></head><body>");
			html.Append("<table><tr class=\"header\"><th>Expression variable</th><th>Description</th></th>");

			bool even = true;
			foreach (RegexCommand rc in avail_regexes)
			{
				string r = rc.FriendlyRegex.Replace("<", "&lt;").Replace(">", "&gt;");
				string f = rc.Description.Replace("<", "&lt;").Replace(">", "&gt;");
				if (even) html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", r, f);
				else html.AppendFormat("<tr class=\"odd\"><td class=\"odd\">{0}</td><td class=\"odd\">{1}</td></tr>", r, f);
				even = !even;
			}

			html.Append("</table></body></html>");

			htmlDisplay.Text = html.ToString();
			html.Clear(); html = null;
		}
	}
}
