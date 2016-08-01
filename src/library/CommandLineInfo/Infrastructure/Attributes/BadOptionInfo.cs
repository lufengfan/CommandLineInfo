using System;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	public sealed class BadOptionInfo
	{
		public char? ShortName
		{
			get;
			internal set;
		}

		public string LongName
		{
			get;
			internal set;
		}

		internal BadOptionInfo()
		{
		}

		internal BadOptionInfo(char? shortName, string longName)
		{
			this.ShortName = shortName;
			this.LongName = longName;
		}
	}
}
