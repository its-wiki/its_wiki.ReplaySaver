using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace its_wiki.ReplaySaver.ActualCode
{
	public class FileBinder
	{
		public delegate void DEL_OnNewReplay(string ReplayFile);
		public event DEL_OnNewReplay OnNewReplay;

		private FileSystemWatcher fsw = null;

		public void Unbind()
		{
			fsw.EnableRaisingEvents = false;
			fsw.Changed -= ChangedHandle;
			fsw.Dispose();
			fsw = null;
		}
		public void BindRebind(string Fullname)
		{
			if (fsw != null)
			{
				Unbind();
			}
			fsw = new FileSystemWatcher(Path.GetDirectoryName(Fullname), Path.GetFileName(Fullname));
			fsw.Changed += ChangedHandle;
			fsw.EnableRaisingEvents = true;
		}
		public bool FileIsOpen(string FileName)
		{
			try
			{
				using (FileStream fs = new FileStream(FileName, FileMode.Open)) { }
				return true;
			}
			catch
			{
				return false;
			}
		}
		private void ChangedHandle(object sender, FileSystemEventArgs e)
		{
			if (FileIsOpen(e.FullPath))
			{
				if (OnNewReplay != null) OnNewReplay(e.FullPath);
			}
		}
	}
}
