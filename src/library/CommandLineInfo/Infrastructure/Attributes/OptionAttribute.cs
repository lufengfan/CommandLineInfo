//using CommandLine.Parsing;
using System;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class OptionAttribute : BaseOptionAttribute
	{
		public OptionAttribute()
		{
			base.AutoName = true;
		}
		
		public OptionAttribute(string shortName = null, string longName = null) : base(shortName, longName)
		{
		}

		/*
		internal OptionInfo CreateOptionInfo()
		{
			return new OptionInfo(this.ShortName, base.LongName);
		}
		*/
	}
}
