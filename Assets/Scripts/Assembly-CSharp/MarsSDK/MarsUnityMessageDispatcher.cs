using System.Collections.Generic;
using Cpp2IlInjected;
using MarsAgent.Login;
using MarsSDK.ThirdParty.Extensions;

namespace MarsSDK
{
	internal static class MarsUnityMessageDispatcher
	{
		private static Dictionary<EOperationAgent, MarsMessageProcess> _agentToMessageProcessor;

		private static Dictionary<PlatformDefinition, MarsMessageProcess> _platformToMessageProcessor;

		private static Dictionary<ExtensionDefinition, MarsMessageProcess> _extensionToMessageProcessor;

		public static void Register(EOperationAgent agentId, MarsMessageProcess messageProcess)
		{ }

		public static void UnRegister(EOperationAgent agentId)
		{ }

		public static bool IsExist(EOperationAgent agentId)
		{ return default; }

		public static void Register(PlatformDefinition platformDefinition, MarsMessageProcess messageProcess)
		{ }

		public static void UnRegister(PlatformDefinition platformDefinition)
		{ }

		public static bool IsExist(PlatformDefinition platformDefinition)
		{ return default; }

		public static void Register(ExtensionDefinition extensionDefinition, MarsMessageProcess messageProcess)
		{ }

		public static void UnRegister(ExtensionDefinition extensionDefinition)
		{ }

		public static bool IsExist(ExtensionDefinition extensionDefinition)
		{ return default; }

		public static bool SendMessage(EOperationAgent agentId, string msg, string[] args)
		{ return default; }

		public static bool SendMessageWithPlatformId(PlatformDefinition platformDefinition, string msg, string[] args)
		{ return default; }

		public static bool SendMessageToPlugin(ExtensionDefinition definition, string msg, string[] args)
		{ return default; }

		static MarsUnityMessageDispatcher()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
