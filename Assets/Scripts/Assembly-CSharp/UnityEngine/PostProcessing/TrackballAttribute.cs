using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class TrackballAttribute : PropertyAttribute
	{
		public readonly string method;

		public TrackballAttribute(string method)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
