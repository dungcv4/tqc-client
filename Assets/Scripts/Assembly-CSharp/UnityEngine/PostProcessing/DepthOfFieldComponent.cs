using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class DepthOfFieldComponent : PostProcessingComponentRenderTexture<DepthOfFieldModel>
	{
		private static class Uniforms
		{
			internal static readonly int _DepthOfFieldTex;

			internal static readonly int _DepthOfFieldCoCTex;

			internal static readonly int _Distance;

			internal static readonly int _LensCoeff;

			internal static readonly int _MaxCoC;

			internal static readonly int _RcpMaxCoC;

			internal static readonly int _RcpAspect;

			internal static readonly int _MainTex;

			internal static readonly int _CoCTex;

			internal static readonly int _TaaParams;

			internal static readonly int _DepthOfFieldParams;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private const string k_ShaderString = "Hidden/Post FX/Depth Of Field";

		private RenderTexture m_CoCHistory;

		private const float k_FilmHeight = 0.024f;

		public override bool active
		{
			get
			{ return default; }
		}

		public override DepthTextureMode GetCameraFlags()
		{ return default; }

		private float CalculateFocalLength()
		{ return default; }

		private float CalculateMaxCoCRadius(int screenHeight)
		{ return default; }

		private bool CheckHistory(int width, int height)
		{ return default; }

		private RenderTextureFormat SelectFormat(RenderTextureFormat primary, RenderTextureFormat secondary)
		{ return default; }

		public void Prepare(RenderTexture source, Material uberMaterial, bool antialiasCoC, Vector2 taaJitter, float taaBlending)
		{ }

		public override void OnDisable()
		{ }

		public DepthOfFieldComponent()
		{ }
	}
}
