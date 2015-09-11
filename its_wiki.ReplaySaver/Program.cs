using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace its_wiki.ReplaySaver
{
	static class Program
	{
		public static string RootDir { get; private set; }
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			RootDir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show("A critical error has occured and the application needs to close!\r\n\r\n" + e.ExceptionObject.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Application.Exit();
		}
	}
}
