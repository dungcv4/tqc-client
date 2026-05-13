using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsAgent.Config
{
	public class MarsConfig
	{
		private static string _env;

		private static Dictionary<string, object> _setting;

		private static Dictionary<string, object> _publicSetting;

		public static void Load()
		{ }

		public static object Get(string key)
		{ return default; }

		public static object GetPublic(string key)
		{ return default; }

		public MarsConfig()
		{ }

		static MarsConfig()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
