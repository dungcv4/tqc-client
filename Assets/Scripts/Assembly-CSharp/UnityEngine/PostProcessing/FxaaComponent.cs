using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class FxaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		private static class Uniforms
		{
			internal static readonly int _QualitySettings;

			internal static readonly int _ConsoleSettings;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public override bool active
		{
			get
			{ return default; }
		}

		public void Render(RenderTexture source, RenderTexture destination)
		{ }

		public FxaaComponent()
		{ }
	}
}
