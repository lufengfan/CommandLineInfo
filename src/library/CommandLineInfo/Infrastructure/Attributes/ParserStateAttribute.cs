using System;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ParserStateAttribute : Attribute
	{
	}
}
