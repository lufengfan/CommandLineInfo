using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This is used to hold the results of parsing a set of command line option strings
	/// </summary>
	public sealed class ParseArgumentsResult
	{
		private OptionCollection options;

		private Dictionary<string, ParseResult> errors;

		private List<string> nonOptions;

		/// <summary>
		/// This read-only property is used to get the option collection related to the results
		/// </summary>
		public OptionCollection Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>
		/// This read-only property is used to see if the options were parsed successfully
		/// </summary>
		/// <value>Returns true if successful, false if not</value>
		public bool Success
		{
			get
			{
				return this.errors.Count == 0;
			}
		}

		/// <summary>
		/// This read-only property returns a collection of the unused arguments
		/// </summary>
		public ReadOnlyCollection<string> UnusedArguments
		{
			get
			{
				return new ReadOnlyCollection<string>(this.nonOptions);
			}
		}

		/// <summary>
		/// Internal constructor
		/// </summary>
		/// <param name="options">The option collection related to the results</param>
		internal ParseArgumentsResult(OptionCollection options)
		{
			this.options = options;
			this.errors = new Dictionary<string, ParseResult>();
			this.nonOptions = new List<string>();
		}

		/// <summary>
		/// This is used to add a parsing error
		/// </summary>
		/// <param name="optionName">The option name</param>
		/// <param name="error">The error result</param>
		internal void AddError(string optionName, ParseResult error)
		{
			this.errors[optionName] = error;
		}

		/// <summary>
		/// This is used to add a non-option
		/// </summary>
		/// <param name="value">The non-option value</param>
		internal void AddNonOption(string value)
		{
			this.nonOptions.Add(value);
		}

		/// <summary>
		/// This is used to write out a list of all parsing errors
		/// </summary>
		/// <param name="writer">The <see cref="TextWriter" /> to which the summary is written</param>
		public void WriteParseErrors(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException(nameof(writer));
			}
			foreach (KeyValuePair<string, ParseResult> current in this.errors)
			{
				string arg;
				switch (current.Value)
				{
					case ParseResult.ArgumentNotAllowed:
						arg = "The option argument is not allowed";
						break;
					case ParseResult.MalformedArgument:
						arg = "The option argument is malformed";
						break;
					case ParseResult.MissingOption:
						arg = this.options[current.Key].RequiredMessage;
						break;
					case ParseResult.UnrecognizedOption:
						arg = "Unrecognized option";
						break;
					case ParseResult.MultipleOccurence:
						arg = "The option cannot occur more than once";
						break;
					default:
						arg = "Unexpected result code (" + current.Value.ToString() + ") for option";
						break;
				}
				writer.WriteLine("{0}: {1}", arg, current.Key);
			}
		}
	}
}
