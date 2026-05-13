using System.IO;
using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	public class ZStreamException : IOException
	{
		public ZStreamException()
		{ }

		public ZStreamException(string s)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
