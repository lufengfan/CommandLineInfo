using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This defines a switch option
	/// </summary>
	/// <remarks>A switch option is one that is only represented by its name</remarks>
	public sealed class SwitchOption : BaseOption
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The switch option name</param>
		/// <param name="description">The switch option description</param>
		public SwitchOption(string name, string description) : base(name, description)
		{
		}

		/// <inheritdoc />
		internal override ParseResult ParseArgument(string argument)
		{
			if (argument.Length > 0)
			{
				return ParseResult.MalformedArgument;
			}
			if (base.IsPresent)
			{
				return ParseResult.MultipleOccurence;
			}
			base.Value = base.Name;
			return ParseResult.Success;
		}

		/// <inheritdoc />
		internal override void WriteTemplate(TextWriter writer)
		{
			writer.WriteLine("/{0}", base.Name);
		}
	}
}
