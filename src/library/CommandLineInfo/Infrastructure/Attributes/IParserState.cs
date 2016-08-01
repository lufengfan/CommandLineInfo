using System;
using System.Collections.Generic;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	public interface IParserState
	{
		IList<ParsingError> Errors
		{
			get;
		}
	}
}
