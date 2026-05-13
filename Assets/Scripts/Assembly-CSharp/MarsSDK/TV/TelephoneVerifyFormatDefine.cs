using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsSDK.TV
{
	public class TelephoneVerifyFormatDefine
	{
		public static readonly Dictionary<string, string> fixFormatMap;

		public static readonly Dictionary<uint, string[]> ruleMap;

		public TelephoneVerifyFormatDefine()
		{ }

		static TelephoneVerifyFormatDefine()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
