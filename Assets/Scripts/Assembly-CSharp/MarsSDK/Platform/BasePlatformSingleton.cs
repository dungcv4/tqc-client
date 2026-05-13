using System;
using Cpp2IlInjected;
using MarsAgent.Login;

namespace MarsSDK.Platform
{
	public abstract class BasePlatformSingleton<T> : BasePlatform where T : BasePlatformSingleton<T>
	{
		private static T mInstance;

		public static T Instance
		{
			get
			{ return default; }
		}

		public static void Touch()
		{ }

		public BasePlatformSingleton(EOperationAgent agentId, PlatformDefinition platformDefinition) : base(agentId, platformDefinition)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public BasePlatformSingleton(EOperationAgent agentId) : base(agentId)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public BasePlatformSingleton(PlatformDefinition platformDefinition) : base(platformDefinition)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }
	}
}
