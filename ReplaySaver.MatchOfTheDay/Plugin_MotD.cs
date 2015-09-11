using its_wiki.EA.Replays.KW;
using its_wiki.ReplaySaver.PluginSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaySaver.MatchOfTheDay
{
    public class Plugin_MatchOfTheDay : iPlugin
    {
		public Plugin_MatchOfTheDay(iPluginHost Host) : base(Host) { }
		private Plugin_MatchOfTheDay_Regex rc = null;
		private Plugin_MatchOfTheDay_Config config = null;
		public override void Init() 
		{
			rc = new Plugin_MatchOfTheDay_Regex(Host);
			config = GetConfiguration<Plugin_MatchOfTheDay_Config>();
			if (config.LastMatch.Date != DateTime.Today.Date)
			{
				config.LastMatch = DateTime.Now;
				config.CounterOfToday = 0;
			}
			SaveConfiguration(config);
		}
		public override void Destuct() { }

		public override string Name { get { return "MatchOfTheDay"; } }
		public override Version PluginVersion { get { return new Version(0, 4, 52, 0); } }
		public override Type ConfigurationType { get { return typeof(Plugin_MatchOfTheDay_Config); } }

		public override void OnPreSave(KWReplayFile replay)
		{
			config = GetConfiguration<Plugin_MatchOfTheDay_Config>();
			if (DateTime.Today.Date != config.LastMatch.Date)
			{
				config.LastMatch = DateTime.Today;
				config.CounterOfToday = 0;
			}
			SaveConfiguration(config);
		}
		public override void OnSaveComplete(string SavedFilename, string Fullname, KWReplayFile replay)
		{
			config = GetConfiguration<Plugin_MatchOfTheDay_Config>();
			config.CounterOfToday++;
			SaveConfiguration(config);
		}

		public override RegexCommand[] CustomExpressionValue
		{
			get
			{
				return new RegexCommand[]
				{
					rc
				};
			}
		}
	}
	[Serializable]
	public class Plugin_MatchOfTheDay_Config
	{
		public Plugin_MatchOfTheDay_Config() { }

		public DateTime LastMatch { get; set; }
		public uint CounterOfToday { get; set; }
	}

	public class Plugin_MatchOfTheDay_Regex : RegexCommand
	{
		public Plugin_MatchOfTheDay_Regex(iPluginHost host) { this.host = host; }
		public override string InnerRegex { get { return @"\bMotD\b(?<OptionalArguments>.*?)"; } set { } }
		iPluginHost host = null;
		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			int digit_count = string.IsNullOrWhiteSpace(OptionalArguments) ? 4 : int.Parse(OptionalArguments);
			StringBuilder sbformat = new StringBuilder(digit_count);
			sbformat.Append("{0:");
			for (int i = 0; i < digit_count; i++) sbformat.Append('0');
			sbformat.Append("}");

			string formatString = sbformat.ToString();
			sbformat.Clear(); sbformat = null;

			Plugin_MatchOfTheDay_Config cfg = (Plugin_MatchOfTheDay_Config)host.GetConfiguration(typeof(Plugin_MatchOfTheDay_Config), typeof(Plugin_MatchOfTheDay));
			return string.Format(formatString, cfg.CounterOfToday);
		}

		public override string FriendlyRegex { get { return "{MotD[:<digit count>]}"; } }
		public override string Description { get { return "Returns the number of matches saved this day. The optional argument defines the digit count and defaults to 4"; } }
	}
}
