using System;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class OptionArrayAttribute : BaseOptionAttribute
	{
		public OptionArrayAttribute()
		{
			base.AutoName = true;
		}
		public OptionArrayAttribute(string shortName = null, string longName = null) : base(shortName, longName)
		{
		}
	}
}
