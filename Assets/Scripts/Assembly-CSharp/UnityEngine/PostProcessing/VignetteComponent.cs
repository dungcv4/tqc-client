using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class VignetteComponent : PostProcessingComponentRenderTexture<VignetteModel>
	{
		private static class Uniforms
		{
			internal static readonly int _Vignette_Color;

			internal static readonly int _Vignette_Center;

			internal static readonly int _Vignette_Settings;

			internal static readonly int _Vignette_Mask;

			internal static readonly int _Vignette_Opacity;

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

		public override void Prepare(Material uberMaterial)
		{ }

		public VignetteComponent()
		{ }
	}
}
