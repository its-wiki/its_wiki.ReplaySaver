using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace its_wiki.ReplaySaver.PluginSystem
{
	public enum MessageType
	{
		OK,
		Error,
		Warning,
		Notification,
	}
	public interface iPluginHost
	{
		void LogMessage(MessageType Type, string Format, params object[] args);
		object GetConfiguration(Type ConfType, Type PluginType);
		bool SaveConfiguration(object Config, Type PluginType);
	}
}
