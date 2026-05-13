using Cpp2IlInjected;

namespace MarsSDK.ThirdParty.Extensions
{
	public abstract class ExtensionBase : MarsMessageProcess
	{
		public ExtensionBase(ExtensionDefinition extensionDefinition) : base(extensionDefinition)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public ExtensionBase(EOperationAgent agent, ExtensionDefinition extensionDefinition) : base(agent, extensionDefinition)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
