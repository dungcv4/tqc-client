using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	internal class FsmContext
	{
		public bool Return;

		public int NextState;

		public Lexer L;

		public int StateStack;

		public FsmContext()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
