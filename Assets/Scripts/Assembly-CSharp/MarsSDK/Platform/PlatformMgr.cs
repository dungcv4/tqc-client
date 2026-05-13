using System.Collections.Generic;
using Cpp2IlInjected;
using MarsAgent.Login;

namespace MarsSDK.Platform
{
	public class PlatformMgr
	{
		private static PlatformMgr mInstance;

		private static Dictionary<PlatformDefinition, BasePlatform> _platformDic;

		public static PlatformMgr Instance()
		{ return default; }

		public PlatformMgr()
		{ }

		public void RegisterPlatform(PlatformDefinition platformID, BasePlatform platform)
		{ }

		public void UnregisterPlatform(PlatformDefinition platformID)
		{ }

		public BasePlatform Get(PlatformDefinition platformID)
		{ return default; }
	}
}
