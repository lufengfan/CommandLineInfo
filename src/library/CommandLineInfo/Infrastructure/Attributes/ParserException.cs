using System;
using System.Runtime.Serialization;

namespace SamLu.CommandLineInfo.Infrastructure.Attributes
{
	[Serializable]
	public class ParserException : Exception
	{
		public ParserException()
		{
		}

		public ParserException(string message) : base(message)
		{
		}

		public ParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
