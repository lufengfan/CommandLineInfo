using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLineInfo.Core.ComponentModel1
{
	/// <summary>
	/// This enumerated type defines the command line option parsing results
	/// </summary>
	internal enum ParseResult
	{
		/// <summary>Success</summary>
		Success,
		/// <summary>Argument not allowed</summary>
		ArgumentNotAllowed,
		/// <summary>Malformed argument</summary>
		MalformedArgument,
		/// <summary>Missing option</summary>
		MissingOption,
		/// <summary>Unrecognized option</summary>
		UnrecognizedOption,
		/// <summary>A single-use option appeared multiple times</summary>
		MultipleOccurence
	}
}
