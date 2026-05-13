using System;
using Cpp2IlInjected;
using MarsAgent.Login;

namespace MarsSDK.Platform
{
	public abstract class BasePlatform : MarsMessageProcess
	{
		protected BasePlatform(EOperationAgent agentId) : base(agentId)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		protected BasePlatform(PlatformDefinition platformDefinition) : base(platformDefinition)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		protected BasePlatform(EOperationAgent agentId, PlatformDefinition platformDefinition) : base(agentId, platformDefinition)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetMessageUserType()
		{ return default; }

		public abstract Type GetPlatformClassType();
	}
}
