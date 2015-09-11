using its_wiki.EA.Replays.KW;
using its_wiki.ReplaySaver.PluginSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaySaver.PatchDetector
{
    public class Plugin_PatchDetector : iPlugin
    {
		Plugin_PatchDetector_Regex rg = null;
		public Plugin_PatchDetector(iPluginHost Host) : base(Host) { rg = new Plugin_PatchDetector_Regex(Host); }
		public override void Init() { }
		public override void Destuct() { }

		public override Type ConfigurationType { get { return typeof(Plugin_PatchDetector_Config); } }
		public override RegexCommand[] CustomExpressionValue
		{
			get
			{
				return new RegexCommand[] { rg };
			}
		}

		public override string Name
		{
			get { return "+PatchDetector"; }
		}
		public override Version PluginVersion
		{
			get { return new Version(0, 3, 15, 0); }
		}
	}
	[Serializable]
	public class Plugin_PatchDetector_Config
	{
		[Serializable]
		public class PPMapping
		{
			public string FriendlyName { get; set; }
			public string Mapped_MC { get; set; }
			public PPMapping() { }
			public PPMapping(string FriendlyName, string Mapped_MC)
			{
				this.FriendlyName = FriendlyName;
				this.Mapped_MC = Mapped_MC;
			}

			public override string ToString()
			{
				return FriendlyName;
			}
		}

		[Description("The +Patch \"config to friendly name\" mappings to use")]
		public List<PPMapping> Mappings { get; set; }

		public Plugin_PatchDetector_Config() { Mappings = new List<PPMapping>(); }
	}
	public class Plugin_PatchDetector_Regex : RegexCommand
	{
		private iPluginHost Host = null;
		public Plugin_PatchDetector_Regex(iPluginHost Host) { this.Host = Host; }
		public override string InnerRegex { get { return @"\bPPD\b"; } set { } }

		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			Plugin_PatchDetector_Config cfg = (Plugin_PatchDetector_Config)Host.GetConfiguration(typeof(Plugin_PatchDetector_Config), typeof(Plugin_PatchDetector));
			Plugin_PatchDetector_Config.PPMapping mapped = null;
			if (replay.MapName.Contains("1.02+")) mapped = cfg.Mappings.FirstOrDefault(pp => pp.Mapped_MC.ToLower() == replay.MapVersion.ToLower());
			
			if (mapped == null) return "Vanilla";
			return mapped.FriendlyName;
		}

		public override string FriendlyRegex { get { return "{PPD}"; } }

		public override string Description { get { return "Returns a friendly mapped version name, \"Vanilla\" if no mappings match."; } }
	}
}
