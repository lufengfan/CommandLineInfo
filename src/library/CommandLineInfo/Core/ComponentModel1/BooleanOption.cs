using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This defines a Boolean option
	/// </summary>
	/// <remarks>A Boolean option is one that has a name followed by a '+' for true or a '-' for false</remarks>
	public sealed class BooleanOption : BaseOption
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The Boolean option name</param>
		/// <param name="description">The Boolean option description</param>
		public BooleanOption(string name, string description) : base(name, description) { }

		/// <inheritdoc />
		internal override ParseResult ParseArgument(string argument)
		{
			if (argument != "+" && argument != "-")
			{
				return ParseResult.MalformedArgument;
			}
			if (base.IsPresent)
			{
				return ParseResult.MultipleOccurence;
			}
			base.Value = (argument == "+");
			return ParseResult.Success;
		}

		/// <inheritdoc />
		internal override void WriteTemplate(TextWriter writer)
		{
			writer.WriteLine("/{0}+|-", base.Name);
		}
	}
}
