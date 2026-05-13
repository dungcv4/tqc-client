using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsSDK.ThirdParty.Extensions
{
	public class ExtensionMgr
	{
		private static ExtensionMgr mInstance;

		private static Dictionary<ExtensionDefinition, ExtensionBase> _pluginDic;

		public static ExtensionMgr Instance()
		{ return default; }

		public ExtensionMgr()
		{ }

		public void RegisterExtensionPlugin(ExtensionDefinition pluginId, ExtensionBase extension)
		{ }

		public ExtensionBase Get(ExtensionDefinition pluginId)
		{ return default; }

		public void BroadcastMsg(ExtensionDefinition pluginId, string msg, string[] args)
		{ }
	}
}
