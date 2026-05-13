using System;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	public class JsonException : ApplicationException
	{
		public JsonException()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal JsonException(ParserToken token)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal JsonException(ParserToken token, Exception inner_exception)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal JsonException(int c)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal JsonException(int c, Exception inner_exception)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonException(string message)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonException(string message, Exception inner_exception)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
