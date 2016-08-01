using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This abstract base class represents a command line option
	/// </summary>
	public abstract class BaseOption
	{
		private object optionValue;

		/// <summary>
		/// This read-only property returns the option name
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// This read-only property returns the option description
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// This is used to get or set a message to display if the option is missing
		/// </summary>
		/// <remarks>The default is null and the parameter is optional.  If set to a non-null, non-empty string
		/// value, the parameter is required.</remarks>
		public string RequiredMessage { get; set; }

		/// <summary>
		/// This property is used to get or set the option value
		/// </summary>
		/// <remarks>As written, the value can only be set when initially parsed</remarks>
		/// <exception cref="InvalidOperationException">This is thrown if an attempt is made to retrieve the
		/// value before it has been set or if an attempt is made to set the value twice.</exception>
		public virtual object Value
		{
			get
			{
				if (this.optionValue == null)
				{
					throw new InvalidOperationException("The option value has not been set");
				}
				return this.optionValue;
			}
			protected set
			{
				if (this.optionValue != null)
				{
					throw new InvalidOperationException("The option has already been set");
				}
				this.optionValue = value;
			}
		}

		/// <summary>
		/// This read-only property is used to determine whether or not the option value was present on the
		/// command line.
		/// </summary>
		public virtual bool IsPresent
		{
			get
			{
				return this.optionValue != null;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The option name</param>
		/// <param name="description">The option description</param>
		/// <exception cref="ArgumentException">This is thrown if the <paramref name="name" /> parameter is null
		/// or empty or contains non-alphabetic characters.</exception>
		protected BaseOption(string name, string description)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("An option name is required", nameof(name));
			}
			if (!name.ToCharArray().All((char c) => char.IsLetter(c) || c == '?'))
			{
				throw new ArgumentException("Names must consist of letters", nameof(name));
			}
			this.Name = name;
			this.Description = (string.IsNullOrEmpty(description) ? "No description" : description);
		}

		/// <summary>
		/// This method is overridden to parse the option arguments, if any
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		/// <returns>A <see cref="ParseResult" /> value to indicate the success or failure of the operation</returns>
		internal abstract ParseResult ParseArgument(string args);

		/// <summary>
		/// This method is overridden to show the command line syntax for the option
		/// </summary>
		/// <param name="writer">The <see cref="TextWriter" /> to which the syntax string is written</param>
		internal abstract void WriteTemplate(TextWriter writer);
	}
}