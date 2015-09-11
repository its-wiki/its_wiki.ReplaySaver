using its_wiki.EA.Replays.Common;
using its_wiki.EA.Replays.KW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace its_wiki.ReplaySaver.PluginSystem
{
	public abstract class iPlugin
	{
		public iPlugin(iPluginHost Host) { this.Host = Host; }
		protected iPluginHost Host { get; private set; }
		public virtual void OnPreSave(KWReplayFile replay) { }
		public virtual void OnSaveComplete(string SavedFilename, string Fullname, KWReplayFile replay) { }
		public virtual Type ConfigurationType { get { return typeof(iPlugin); } }
		public virtual RegexCommand[] CustomExpressionValue { get { return null; } }

		public abstract void Init();
		public abstract void Destuct();
		public abstract string Name { get; }
		public abstract Version PluginVersion { get; }

		[DebuggerStepThrough]
		public object GetConfiguration()
		{
			if (ConfigurationType == typeof(iPlugin)) throw new InvalidOperationException("The plugin has no configuration!");
			return Host.GetConfiguration(ConfigurationType, this.GetType());
		}
		[DebuggerStepThrough]
		public ConfType GetConfiguration<ConfType>()
		{
			return (ConfType)Host.GetConfiguration(typeof(ConfType), this.GetType());
		}
		[DebuggerStepThrough]
		public bool SaveConfiguration(object Config)
		{
			return Host.SaveConfiguration(Config, this.GetType());
		}
	}
}
