using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class ChromaticAberrationComponent : PostProcessingComponentRenderTexture<ChromaticAberrationModel>
	{
		private static class Uniforms
		{
			internal static readonly int _ChromaticAberration_Amount;

			internal static readonly int _ChromaticAberration_Spectrum;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private Texture2D m_SpectrumLut;

		public override bool active
		{
			get
			{ return default; }
		}

		public override void OnDisable()
		{ }

		public override void Prepare(Material uberMaterial)
		{ }

		public ChromaticAberrationComponent()
		{ }
	}
}
