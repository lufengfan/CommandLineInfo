using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This defines a string option
	/// </summary>
	/// <remarks>A string option is one that has a name/value pair separated by a colon</remarks>
	public class StringOption : BaseOption
	{
		private string template;

		/// <summary>
		/// This is used to specify the template used when showing the command line syntax
		/// </summary>
		public string Template
		{
			get
			{
				return this.template;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.template = "xxxx";
					return;
				}
				this.template = value;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The string option name</param>
		/// <param name="description">The string option description</param>
		public StringOption(string name, string description) : this(name, description, "xxxx")
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The string option name</param>
		/// <param name="description">The string option description</param>
		/// <param name="template">A template to use when showing the command line syntax</param>
		public StringOption(string name, string description, string template) : base(name, description)
		{
			this.Template = template;
		}

		/// <inheritdoc />
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
			if (base.IsPresent)
			{
				return ParseResult.MultipleOccurence;
			}
			base.Value = argument.Substring(1);
			return ParseResult.Success;
		}

		/// <inheritdoc />
		internal override void WriteTemplate(TextWriter writer)
		{
			writer.WriteLine("/{0}:{1}", base.Name, this.Template);
		}
	}
}
