using its_wiki.EA.Replays.KW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace its_wiki.ReplaySaver.PluginSystem
{
	[Serializable]
	public abstract class RegexCommand
	{
		[Description("The inner regex to match, the {} are automaticly added")]
		public abstract string InnerRegex { get; set; }

		[Description("The formatting function")]
		public abstract string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "");

		public abstract string FriendlyRegex { get; }
		public abstract string Description { get; }
	}
}
