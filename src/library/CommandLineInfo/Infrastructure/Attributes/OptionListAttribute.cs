using System;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class OptionListAttribute : BaseOptionAttribute
	{
		private const char DefaultSeparator = ':';

		public char Separator
		{
			get;
			set;
		}

		public OptionListAttribute()
		{
			base.AutoName = true;
			this.Separator = ':';
		}
		
		public OptionListAttribute(string shortName = null, string longName = null) : base(shortName, longName)
		{
			this.Separator = ':';
		}

		public OptionListAttribute(string shortName = null, string longName = null, char separator = ':') : base(shortName, longName)
		{
			this.Separator = separator;
		}
	}
}
