// Source: dump.cs — MarsAgent.Config.MarsConfig — SDK helper, mostly stripped in Editor diag.
// .cctor allocates empty dictionaries to avoid NRE on Get/GetPublic.

using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsAgent.Config
{
	public class MarsConfig
	{
		private static string _env;
		private static Dictionary<string, object> _setting;
		private static Dictionary<string, object> _publicSetting;

		public static void Load() { }

		public static object Get(string key)
		{
			if (_setting == null || string.IsNullOrEmpty(key)) return null;
			object v;
			return _setting.TryGetValue(key, out v) ? v : null;
		}

		public static object GetPublic(string key)
		{
			if (_publicSetting == null || string.IsNullOrEmpty(key)) return null;
			object v;
			return _publicSetting.TryGetValue(key, out v) ? v : null;
		}

		public MarsConfig() { }

		static MarsConfig()
		{
			_env = "release";
			_setting = new Dictionary<string, object>();
			_publicSetting = new Dictionary<string, object>();
		}
	}
}
