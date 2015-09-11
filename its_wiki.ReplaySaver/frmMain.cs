using its_wiki.EA.Replays.KW;
using its_wiki.ReplaySaver.ActualCode;
using its_wiki.ReplaySaver.PluginSystem;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace its_wiki.ReplaySaver
{
	public partial class frmMain : Form, iPluginHost
	{
		private SoundPlayer SPNOTIF = new SoundPlayer(Properties.Resources.Speech_On);
		private Options main_conf = new Options();
		private ConcurrentDictionary<string, Assembly> refloaded = new ConcurrentDictionary<string, Assembly>();
		private List<RegexCommand> defRegex = new List<RegexCommand>();
		private FileBinder fb = null;
		private List<iPlugin> LoadedPlugins = new List<iPlugin>();
		StringBuilder messageBuilder = new StringBuilder();
		public frmMain()
		{
			InitializeComponent();
			LogMessage(MessageType.Notification, "<a href=\"#exp_help\">Click here</a> for more information on all available variables!");
			LogMessage(MessageType.Warning, "Loading PluginSystem...");
			InitPluginSystem();

			LogMessage(MessageType.Warning, "Loading application config");
			main_conf = (Options)GetConfiguration(typeof(Options), typeof(frmMain));
			txtLastReplay.Text = main_conf.LastReplayLocation;
			txtSaveBase.Text = main_conf.BaseDirectory;
			txtExpression.Text = main_conf.Expression;

			LogMessage(MessageType.Warning, "Loading plugins...");
			defRegex.AddRange(new RegexCommand[] { new MapNameRegex(), new PlayerNameRegex(), new AllPlayersRegex(), new TimeStampRegex(), new IndexRegex() });
			LogMessage(MessageType.Notification, "Application initialized!");
			LogMessage(MessageType.Notification, "Click here to turn the filebinder <a href=\"#on\">on</a>/<a href=\"#off\">off</a> (default is on)");

			if (!string.IsNullOrWhiteSpace(txtLastReplay.Text))
			{
				fb = new FileBinder();
				fb.OnNewReplay += FileBinder_OnNewReplay;
				txtLastReplay.Enabled = false;
				fb.BindRebind(txtLastReplay.Text);
			}
		}

		private void FileBinder_OnNewReplay(string ReplayFile)
		{
			KWReplayFile replay = new KWReplayFile();
			using (FileStream fs = new FileStream(ReplayFile, FileMode.Open, FileAccess.Read)) replay.ParseStream(fs);

			LoadedPlugins.ForEach(lp => lp.OnPreSave(replay));

			string NewFile = EvalExpression(txtSaveBase.Text, txtExpression.Text, replay);
			string NewFileName = Path.GetFileName(NewFile);
			string NewFileDirectory = Path.GetDirectoryName(NewFile);
			if (!Directory.Exists(NewFileDirectory)) Directory.CreateDirectory(NewFileDirectory);
			File.Copy(ReplayFile, NewFile);
			SPNOTIF.Play();

			LoadedPlugins.ForEach(lp => lp.OnSaveComplete(NewFileName, NewFile, replay));

			LogMessage(MessageType.Notification, "Successfully saved \"<b>{0}</b>\"!", NewFileName);
		}

		#region iPluginHost
		[DebuggerStepThrough]
		public void LogMessage(MessageType Type, string Format, params object[] args)
		{
			StringBuilder sbNew = new StringBuilder();
			sbNew.AppendFormat("<li>[<span style=\"color: {0};\">{1}</span>][{2:HH:mm:ss}]: {3}</li>", ColorFromType(Type), Type, DateTime.Now, args != null ? string.Format(Format, args) : Format);
			sbNew.Append(messageBuilder.ToString());
			messageBuilder.Clear();

			messageBuilder = sbNew;
			Action a = delegate()
			{
				string html = string.Format("<html><head><style>body {{ font-family: Calibri; color: #000000; margin: 0px; padding: 2px; }} ul {{ list-style-type: none; width: 100%; padding: 0px; margin: 0px; }} li {{ display: block; width: 100%; }} a {{ color: #0094FF; }} a:visited {{ color: #0094FF; }}</style></head><body><ul>{0}</ul></body></html>", messageBuilder.ToString());
				htmlMessages.Text = html;
			};
			if (this.InvokeRequired) this.Invoke(a);
			else a();
		}
		[DebuggerStepThrough]
		public object GetConfiguration(Type ConfType, Type PluginType)
		{
			string target = Path.Combine(Program.RootDir, "Plugins\\Configs", PluginType.Name + ".conf.xml");
			XmlSerializer x = new XmlSerializer(ConfType);
			if (!File.Exists(target))
			{
				if (!Directory.Exists(Path.GetDirectoryName(target))) Directory.CreateDirectory(Path.GetDirectoryName(target));
				
				using (FileStream fs = new FileStream(target, FileMode.Create, FileAccess.Write))
				{
					x.Serialize(fs, Activator.CreateInstance(ConfType));
				}
			}
			using (FileStream fs = new FileStream(target, FileMode.Open, FileAccess.Read))
			{
				return x.Deserialize(fs);
			}
		}
		[DebuggerStepThrough]
		public bool SaveConfiguration(object Config, Type PluginType)
		{
			try
			{
				string target = Path.Combine(Program.RootDir, "Plugins\\Configs", PluginType.Name + ".conf.xml");
				XmlSerializer x = new XmlSerializer(Config.GetType());
				if (!File.Exists(target) && !Directory.Exists(Path.GetDirectoryName(target))) Directory.CreateDirectory(Path.GetDirectoryName(target));
				using (FileStream fs = new FileStream(target, FileMode.Create, FileAccess.ReadWrite))
				{
					x.Serialize(fs, Config);
				}
				return true;
			}
			catch { }
			return false;
		}
		#endregion
		#region HTML gui stuff
		private void htmlMessages_LinkClicked(object sender, HtmlRenderer.Entities.HtmlLinkClickedEventArgs e)
		{
			if (e.Link == "#on")
			{
				if (fb == null)
				{
					fb = new FileBinder();
					fb.OnNewReplay += FileBinder_OnNewReplay;
					fb.BindRebind(txtLastReplay.Text);
				}
				txtLastReplay.Enabled = false;
				fb.BindRebind(txtLastReplay.Text);
				LogMessage(MessageType.OK, "The FileBinder was succesfully rebound!");
			}
			else if (e.Link == "#off")
			{
				if (fb != null)
				{
					txtLastReplay.Enabled = true;
					fb.Unbind();
					LogMessage(MessageType.Warning, "The FileBinder was succesfully unbound. <b>REPLAY ARE NOT SAVED!</b>");
				}
			}
			else if (e.Link == "#exp_help")
			{
				new frmExpressionHelp(AvailableRegexes()).Show();
			}
			else if (e.Link.StartsWith("cfg://"))
			{
				ShowConfig(e.Link.Substring(6));
			}
			e.Handled = true;
		}
		#endregion
		#region Plugin loading/configuring
		private void InitPluginSystem()
		{
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			#region Dependency loading
			foreach (string ref_file in Directory.GetFiles(Path.Combine(Program.RootDir, "Plugins\\Refloaded"), "*.*", SearchOption.AllDirectories))
			{
				try
				{
					Assembly ass = Assembly.Load(ref_file);
					refloaded.TryAdd(ass.GetName().Name, ass);
				}
				catch(Exception ex) 
				{
					LogMessage(MessageType.Error, "Failed to load plugin reference[{0}]: {1}!", Path.GetFileName(ref_file), ex.Message);
				}
			}
			#endregion
			#region Plugin loading
			foreach (string plug_file in Directory.GetFiles(Path.Combine(Program.RootDir, "Plugins"), "*.dll", SearchOption.TopDirectoryOnly))
			{
				try
				{
					Assembly ass = Assembly.LoadFrom(plug_file);
					foreach (Type tt in ass.GetTypes())
					{
						if (typeof(iPlugin).IsAssignableFrom(tt))
						{
							iPlugin plug = (iPlugin)Activator.CreateInstance(tt, (iPluginHost)this);
							plug.Init();
							LoadedPlugins.Add(plug);
							LogMessage(MessageType.OK, "+Loaded plugin: <a href=\"cfg://{0}\">{0}</a> v{1}", plug.Name, plug.PluginVersion.ToString(3));
						}
					}
				}
				catch (Exception ex)
				{
					LogMessage(MessageType.Error, "Failed to load plugin[{0}]: {1}!", Path.GetFileName(plug_file), ex.Message);
				}
			}
			#endregion
		}
		private void ShowConfig(string plugName)
		{
			iPlugin selectedPlugin = LoadedPlugins.FirstOrDefault(ip => ip.Name == plugName);
			if (selectedPlugin == null)
			{
				LogMessage(MessageType.Error, "Plugin: <b>{0}</b> was not found!", plugName);
				return;
			}
			try
			{
				object Config = selectedPlugin.GetConfiguration();
				frmConfig cfg = new frmConfig(Config, (Action<object>)delegate(object objConfig)
				{
					this.SaveConfiguration(objConfig, selectedPlugin.GetType());
					LogMessage(MessageType.Notification, "Configuration saved for <b>{0}</b>", selectedPlugin.Name);
				});
				cfg.ShowDialog();
			}
			catch (InvalidOperationException)
			{
				LogMessage(MessageType.Warning, "The plugin <b>{0}</b> has no configuration!", selectedPlugin.Name);
			}
		}
		#endregion
		#region Regex engine/function mapping
		public IEnumerable<RegexCommand> AvailableRegexes()
		{
			foreach (iPlugin plug in LoadedPlugins)
			{
				if (plug.CustomExpressionValue == null || plug.CustomExpressionValue.Length == 0) continue;
				foreach (RegexCommand cmd in plug.CustomExpressionValue)
				{
					yield return cmd;
				}
			}
			#region Default regex commands
			foreach (RegexCommand regex in defRegex) yield return regex;
			#endregion
		}
		public string EvalExpression(string BaseDirectory, string Expression, KWReplayFile replay)
		{
			string target_file = BaseDirectory.TrimEnd('\\') + '\\' + Expression.TrimStart('\\');
			foreach (RegexCommand rc in AvailableRegexes())
			{
				string CreatedRegex = "{" + rc.InnerRegex + "}";
				Match rm = Regex.Match(target_file, CreatedRegex);
				if (rm != null && rm.Success)
				{
					string param = null;
					if (rm.Groups.Count != 0) param = rm.Groups["OptionalArguments"].Value.TrimStart(':');
					target_file = Regex.Replace(target_file, CreatedRegex, rc.FormatFunction(replay, target_file, param));
				}
			}
			return target_file;
		}
		#endregion
		#region Random functions
		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (File.Exists(args.Name))
			{
				return Assembly.LoadFrom(args.Name);
			}
			else
			{
				AssemblyName an = new AssemblyName(args.Name);

				if (!refloaded.ContainsKey(an.Name)) return null;
				return refloaded[an.Name];
			}
		}
		[DebuggerStepThrough]
		private string ColorFromType(MessageType Type)
		{
			switch (Type)
			{
				case MessageType.OK:
					return "#007F0E";
				case MessageType.Error:
					return "#FF0000";
				case MessageType.Warning:
					return "#FF802B";
				case MessageType.Notification:
					return "#0094FF";
				default:
					return "#000000";
			}
		}
		#endregion

		private void btnSave_Click(object sender, EventArgs e)
		{
			main_conf.LastReplayLocation = txtLastReplay.Text;
			main_conf.BaseDirectory = txtSaveBase.Text;
			main_conf.Expression = txtExpression.Text;
			SaveConfiguration(main_conf, typeof(frmMain));
			LogMessage(MessageType.OK, "Successfully saved application settings!");
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (fb != null)
			{
				fb.Unbind();
				fb.OnNewReplay -= FileBinder_OnNewReplay;
				fb = null;
			}
			if (SPNOTIF != null)
			{
				SPNOTIF.Dispose();
				SPNOTIF = null;
			}
			foreach (iPlugin plug in LoadedPlugins)
			{
				plug.Destuct();
			}
			LoadedPlugins.Clear();
			LoadedPlugins = null;
		}

		private void btnBrowseReplay_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog() { Filter = "KW Replay files (*.KWReplay)|*.KWReplay", CheckFileExists = true, Title = "Select your last replay file" };
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtLastReplay.Text = ofd.FileName;
			}
		}

		private void btnBrowseBase_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog() { ShowNewFolderButton = true, Description = "Select your base save location" };
			if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtSaveBase.Text = fbd.SelectedPath;
			}
		}
	}
}
