using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This collection is used to hold a set of command line option definitions
	/// </summary>
	public sealed class OptionCollection : Collection<BaseOption>
	{
		private Dictionary<string, BaseOption> map = new Dictionary<string, BaseOption>();

		/// <summary>
		/// This read-only property can be used to retrieve an option by name
		/// </summary>
		/// <param name="name">The name of the option to retrieve</param>
		/// <returns></returns>
		public BaseOption this[string name]
		{
			get
			{
				BaseOption result;
				if (this.map.TryGetValue(name, out result))
				{
					return result;
				}
				return null;
			}
		}

		/// <inheritdoc />
		protected override void InsertItem(int index, BaseOption item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}
			base.InsertItem(index, item);
			this.map[item.Name] = item;
		}

		/// <inheritdoc />
		protected override void ClearItems()
		{
			base.ClearItems();
			this.map.Clear();
		}

		/// <inheritdoc />
		protected override void RemoveItem(int index)
		{
			BaseOption baseOption = base[index];
			base.RemoveItem(index);
			this.map.Remove(baseOption.Name);
		}

		/// <inheritdoc />
		protected override void SetItem(int index, BaseOption item)
		{
			BaseOption baseOption = base[index];
			base.SetItem(index, item);
			this.map.Remove(baseOption.Name);
			this.map[item.Name] = item;
		}

		/// <summary>
		/// Parse an array of command line option strings into command line option instances
		/// </summary>
		/// <param name="args">The array of options to parse</param>
		/// <returns>The results of parsing the command line option strings</returns>
		public ParseArgumentsResult ParseArguments(string[] args)
		{
			ParseArgumentsResult parseArgumentsResult = new ParseArgumentsResult(this);
			this.ParseArguments(args, parseArgumentsResult);
			foreach (BaseOption current in from o in this
										   where !o.IsPresent && !string.IsNullOrEmpty(o.RequiredMessage)
										   select o)
			{
				parseArgumentsResult.AddError(current.Name, ParseResult.MissingOption);
			}
			return parseArgumentsResult;
		}

		/// <summary>
		/// This is used to write out a summary of the options
		/// </summary>
		/// <param name="writer">The <see cref="TextWriter" /> to which the summary is written</param>
		/// <exception cref="ArgumentNullException">This is thrown if the <paramref name="writer" /> parameter
		/// is null.</exception>
		public void WriteOptionSummary(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException(nameof(writer));
			}
			foreach (BaseOption current in this)
			{
				writer.WriteLine();
				current.WriteTemplate(writer);
				writer.WriteLine(current.Description);
			}
		}

		/// <summary>
		/// This is used to parse the command line options and return the results
		/// </summary>
		/// <param name="args">The array of option strings to parse</param>
		/// <param name="results">The results of the parsing operation</param>
		private void ParseArguments(string[] args, ParseArgumentsResult results)
		{
			for (int i = 0; i < args.Length; i++)
			{
				string text = args[i];
				if (text.Length != 0)
				{
					if (text[0] == '/' || text[0] == '-')
					{
						int num = 1;
						while (num < text.Length && (char.IsLetter(text, num) || text[num] == '?'))
						{
							num++;
						}
						string key = text.Substring(1, num - 1);
						string args2 = text.Substring(num);
						if (this.map.ContainsKey(key))
						{
							BaseOption baseOption = this.map[key];
							ParseResult parseResult = baseOption.ParseArgument(args2);
							if (parseResult != ParseResult.Success)
							{
								results.AddError(text, parseResult);
							}
						}
						else
						{
							results.AddError(text, ParseResult.UnrecognizedOption);
						}
					}
					else if (text[0] == '@')
					{
						this.ParseArguments(File.ReadAllLines(text.Substring(1)), results);
					}
					else
					{
						results.AddNonOption(text);
					}
				}
			}
		}
	}
}
