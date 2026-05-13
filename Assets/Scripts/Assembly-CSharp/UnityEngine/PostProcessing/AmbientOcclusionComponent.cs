using Cpp2IlInjected;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	public sealed class AmbientOcclusionComponent : PostProcessingComponentCommandBuffer<AmbientOcclusionModel>
	{
		private static class Uniforms
		{
			internal static readonly int _Intensity;

			internal static readonly int _Radius;

			internal static readonly int _FogParams;

			internal static readonly int _Downsample;

			internal static readonly int _SampleCount;

			internal static readonly int _OcclusionTexture1;

			internal static readonly int _OcclusionTexture2;

			internal static readonly int _OcclusionTexture;

			internal static readonly int _MainTex;

			internal static readonly int _TempRT;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private enum OcclusionSource
		{
			DepthTexture = 0,
			DepthNormalsTexture = 1,
			GBuffer = 2
		}

		private const string k_BlitShaderString = "Hidden/Post FX/Blit";

		private const string k_ShaderString = "Hidden/Post FX/Ambient Occlusion";

		private readonly RenderTargetIdentifier[] m_MRT;

		private OcclusionSource occlusionSource
		{
			get
			{ return default; }
		}

		private bool ambientOnlySupported
		{
			get
			{ return default; }
		}

		public override bool active
		{
			get
			{ return default; }
		}

		public override DepthTextureMode GetCameraFlags()
		{ return default; }

		public override string GetName()
		{ return default; }

		public override CameraEvent GetCameraEvent()
		{ return default; }

		public override void PopulateCommandBuffer(CommandBuffer cb)
		{ }

		public AmbientOcclusionComponent()
		{ }
	}
}
