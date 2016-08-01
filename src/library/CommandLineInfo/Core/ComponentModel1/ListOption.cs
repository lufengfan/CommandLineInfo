using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This defines a list option
	/// </summary>
	/// <remarks>A a list option is like a <see cref="StringOption" /> but the value is a comma-separated
	/// list of one or more values.</remarks>
	public sealed class ListOption : StringOption
	{
		private List<string> values;

		/// <summary>
		/// This is overridden to return the value as a string array
		/// </summary>
		public override object Value
		{
			get
			{
				if (this.values == null)
				{
					throw new InvalidOperationException("Option value has not been set");
				}
				return this.values.ToArray();
			}
			protected set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The list option name</param>
		/// <param name="description">The list option description</param>
		public ListOption(string name, string description) : base(name, description)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The list option name</param>
		/// <param name="description">The list option description</param>
		/// <param name="template">A template to use when showing the command line syntax</param>
		public ListOption(string name, string description, string template) : base(name, description, template)
		{
		}

		/// <inheritdoc />
		/// <remarks>If the option appears multiple times, the values are combined into a single list</remarks>
		internal override ParseResult ParseArgument(string argument)
		{
			if (argument.Length == 0)
			{
				return ParseResult.MalformedArgument;
			}
			if (argument[0] != ':')
			{
				return ParseResult.MalformedArgument;
			}
			if (this.values == null)
			{
				this.values = new List<string>();
				base.Value = this.values;
			}
			this.values.AddRange(argument.Substring(1).Split(new char[]
			{
				','
			}));
			return ParseResult.Success;
		}

		/// <inheritdoc />
		internal override void WriteTemplate(TextWriter writer)
		{
			writer.WriteLine("/{0}:{1}[,{1},{1},...]", base.Name, base.Template);
		}
	}
}
