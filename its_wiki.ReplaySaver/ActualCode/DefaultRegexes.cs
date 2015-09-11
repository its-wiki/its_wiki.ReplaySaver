using its_wiki.EA.Replays.Common;
using its_wiki.EA.Replays.KW;
using its_wiki.EA.Replays.KW.Enums;
using its_wiki.ReplaySaver.PluginSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace its_wiki.ReplaySaver.ActualCode
{
	public class MapNameRegex : RegexCommand
	{
		public override string InnerRegex { get { return @"\bMapName\b"; } set { } }
		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			return replay.MapName;
		}

		public override string FriendlyRegex { get { return "{MapName}"; } }
		public override string Description { get { return "Returns the name of the map on which the match was played on"; } }
	}
	public class PlayerNameRegex : RegexCommand
	{
		public override string InnerRegex { get { return @"\bPlayers\b"; } set { } }
		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			StringBuilder sb = new StringBuilder();

			foreach (PlayerSlot player in replay.PlayerSlots.Where(ps => (!(ps.PlayerFaction == PlayerFaction.Commentator || ps.PlayerFaction == PlayerFaction.Observer) && ps != PlayerSlot.Empty)).OrderBy(ps2 => ps2.PlayerName).ThenBy(ps3 => ps3.PlayerIndex))
			{
				sb.AppendFormat("{0}.", player.PlayerName);
			}

			string rslt = sb.ToString().Trim('.', ' ');
			sb.Clear();
			sb = null;
			return rslt;
		}

		public override string FriendlyRegex { get { return "{Players}"; } }
		public override string Description { get { return "Returns a dot seperated list of all the players playing in the match. These are ordered by Nickname."; } }
	}
	public class AllPlayersRegex : RegexCommand
	{
		public override string InnerRegex { get { return @"\bAllPlayers\b"; } set { } }
		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			StringBuilder sb = new StringBuilder();

			foreach (PlayerSlot player in replay.PlayerSlots.Where(ps => ps != PlayerSlot.Empty).OrderBy(ps2 => ps2.PlayerName).ThenBy(ps3 => ps3.PlayerIndex))
			{
				sb.AppendFormat("{0}.", player.PlayerName);
			}

			string rslt = sb.ToString().Trim('.', ' ');
			sb.Clear();
			sb = null;
			return rslt;
		}

		public override string FriendlyRegex { get { return "{AllPlayers}"; } }
		public override string Description { get { return "Returns a dot seperated list of all the players that where in the match, including observers/commentators. These are ordered by Nickname."; } }
	}
	public class TimeStampRegex : RegexCommand
	{
		public override string InnerRegex { get { return @"\bTimeStamp\b(?<OptionalArguments>.*?)?"; } set { } }
		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			string fm = string.IsNullOrWhiteSpace(OptionalArguments) ? "dd.MM.yyyy" : OptionalArguments;
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(TimeSpan.FromSeconds((double)replay.MatchTimestamp)).ToString(fm);
		}

		public override string FriendlyRegex { get { return "{TimeStamp[:<date/time format>]}"; } }
		public override string Description { get { return "Returns the formatted timestamp of the match, the format defaults to \"dd.MM.yyyy\" without quotes"; } }
	}

	//THIS CANNOT BE USED AS A NORMAL REGEXCOMMAND!
	public class IndexRegex : RegexCommand
	{
		public override string InnerRegex { get { return @"\bIndex\b(?<OptionalArguments>.*?)?"; } set { } }
		public override string FormatFunction(KWReplayFile replay, string CurrentFilename, string OptionalArguments = "")
		{
			int digit_count = string.IsNullOrWhiteSpace(OptionalArguments) ? 4 : int.Parse(OptionalArguments);
			StringBuilder sbformat = new StringBuilder(digit_count);
			sbformat.Append("{0:");
			for (int i = 0; i < digit_count; i++) sbformat.Append('0');
			sbformat.Append("}");

			string formatString = sbformat.ToString();
			sbformat.Clear(); sbformat = null;

			int counter = 0;
			while (true)
			{
				if (!File.Exists(Regex.Replace(CurrentFilename, InnerRegex, string.Format(formatString, counter)))) break;
				else counter++;
			}
			return string.Format(formatString, counter);
		}

		public override string FriendlyRegex { get { return "{Index[:<digit count>]}"; } }
		public override string Description { get { return "Returns the formatted number where the index is the next available filename with that name."; } }
	}
}
