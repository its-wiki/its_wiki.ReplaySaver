using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace its_wiki.ReplaySaver
{
	[Serializable]
	public class Options
	{
		public string LastReplayLocation { get; set; }
		public string BaseDirectory { get; set; }
		public string Expression { get; set; }
	}
}
