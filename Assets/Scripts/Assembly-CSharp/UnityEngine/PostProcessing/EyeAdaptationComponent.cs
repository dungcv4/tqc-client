using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class EyeAdaptationComponent : PostProcessingComponentRenderTexture<EyeAdaptationModel>
	{
		private static class Uniforms
		{
			internal static readonly int _Params;

			internal static readonly int _Speed;

			internal static readonly int _ScaleOffsetRes;

			internal static readonly int _ExposureCompensation;

			internal static readonly int _AutoExposure;

			internal static readonly int _DebugWidth;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private ComputeShader m_EyeCompute;

		private ComputeBuffer m_HistogramBuffer;

		private readonly RenderTexture[] m_AutoExposurePool;

		private int m_AutoExposurePingPing;

		private RenderTexture m_CurrentAutoExposure;

		private RenderTexture m_DebugHistogram;

		private static uint[] s_EmptyHistogramBuffer;

		private bool m_FirstFrame;

		private const int k_HistogramBins = 64;

		private const int k_HistogramThreadX = 16;

		private const int k_HistogramThreadY = 16;

		public override bool active
		{
			get
			{ return default; }
		}

		public void ResetHistory()
		{ }

		public override void OnEnable()
		{ }

		public override void OnDisable()
		{ }

		private Vector4 GetHistogramScaleOffsetRes()
		{ return default; }

		public Texture Prepare(RenderTexture source, Material uberMaterial)
		{ return default; }

		public void OnGUI()
		{ }

		public EyeAdaptationComponent()
		{ }
	}
}
