using System;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class TaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		private static class Uniforms
		{
			internal static int _Jitter;

			internal static int _SharpenParameters;

			internal static int _FinalBlendParameters;

			internal static int _HistoryTex;

			internal static int _MainTex;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private const string k_ShaderString = "Hidden/Post FX/Temporal Anti-aliasing";

		private const int k_SampleCount = 8;

		private readonly RenderBuffer[] m_MRT;

		private int m_SampleIndex;

		private bool m_ResetHistory;

		private RenderTexture m_HistoryTexture;

		public override bool active
		{
			get
			{ return default; }
		}

		public Vector2 jitterVector
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			private set
			{ }
		}

		public override DepthTextureMode GetCameraFlags()
		{ return default; }

		public void ResetHistory()
		{ }

		public void SetProjectionMatrix(Func<Vector2, Matrix4x4> jitteredFunc)
		{ }

		public void Render(RenderTexture source, RenderTexture destination)
		{ }

		private float GetHaltonValue(int index, int radix)
		{ return default; }

		private Vector2 GenerateRandomOffset()
		{ return default; }

		private Matrix4x4 GetPerspectiveProjectionMatrix(Vector2 offset)
		{ return default; }

		private Matrix4x4 GetOrthographicProjectionMatrix(Vector2 offset)
		{ return default; }

		public override void OnDisable()
		{ }

		public TaaComponent()
		{ }
	}
}
