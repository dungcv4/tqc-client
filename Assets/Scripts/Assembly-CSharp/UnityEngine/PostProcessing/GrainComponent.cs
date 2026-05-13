using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class GrainComponent : PostProcessingComponentRenderTexture<GrainModel>
	{
		private static class Uniforms
		{
			internal static readonly int _Grain_Params1;

			internal static readonly int _Grain_Params2;

			internal static readonly int _GrainTex;

			internal static readonly int _Phase;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private RenderTexture m_GrainLookupRT;

		public override bool active
		{
			get
			{ return default; }
		}

		public override void OnDisable()
		{ }

		public override void Prepare(Material uberMaterial)
		{ }

		public GrainComponent()
		{ }
	}
}
