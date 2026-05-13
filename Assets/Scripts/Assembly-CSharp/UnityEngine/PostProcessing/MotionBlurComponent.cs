using Cpp2IlInjected;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	public sealed class MotionBlurComponent : PostProcessingComponentCommandBuffer<MotionBlurModel>
	{
		private static class Uniforms
		{
			internal static readonly int _VelocityScale;

			internal static readonly int _MaxBlurRadius;

			internal static readonly int _RcpMaxBlurRadius;

			internal static readonly int _VelocityTex;

			internal static readonly int _MainTex;

			internal static readonly int _Tile2RT;

			internal static readonly int _Tile4RT;

			internal static readonly int _Tile8RT;

			internal static readonly int _TileMaxOffs;

			internal static readonly int _TileMaxLoop;

			internal static readonly int _TileVRT;

			internal static readonly int _NeighborMaxTex;

			internal static readonly int _LoopCount;

			internal static readonly int _TempRT;

			internal static readonly int _History1LumaTex;

			internal static readonly int _History2LumaTex;

			internal static readonly int _History3LumaTex;

			internal static readonly int _History4LumaTex;

			internal static readonly int _History1ChromaTex;

			internal static readonly int _History2ChromaTex;

			internal static readonly int _History3ChromaTex;

			internal static readonly int _History4ChromaTex;

			internal static readonly int _History1Weight;

			internal static readonly int _History2Weight;

			internal static readonly int _History3Weight;

			internal static readonly int _History4Weight;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private enum Pass
		{
			VelocitySetup = 0,
			TileMax1 = 1,
			TileMax2 = 2,
			TileMaxV = 3,
			NeighborMax = 4,
			Reconstruction = 5,
			FrameCompression = 6,
			FrameBlendingChroma = 7,
			FrameBlendingRaw = 8
		}

		public class ReconstructionFilter
		{
			private RenderTextureFormat m_VectorRTFormat;

			private RenderTextureFormat m_PackedRTFormat;

			public ReconstructionFilter()
			{ }

			private void CheckTextureFormatSupport()
			{ }

			public bool IsSupported()
			{ return default; }

			public void ProcessImage(PostProcessingContext context, CommandBuffer cb, ref MotionBlurModel.Settings settings, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material)
			{ }
		}

		public class FrameBlendingFilter
		{
			private struct Frame
			{
				public RenderTexture lumaTexture;

				public RenderTexture chromaTexture;

				private float m_Time;

				private RenderTargetIdentifier[] m_MRT;

				public float CalculateWeight(float strength, float currentTime)
				{ return default; }

				public void Release()
				{ }

				public void MakeRecord(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, Material material)
				{ }

				public void MakeRecordRaw(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, RenderTextureFormat format)
				{ }
			}

			private bool m_UseCompression;

			private RenderTextureFormat m_RawTextureFormat;

			private Frame[] m_FrameList;

			private int m_LastFrameCount;

			public FrameBlendingFilter()
			{ }

			public void Dispose()
			{ }

			public void PushFrame(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, Material material)
			{ }

			public void BlendFrames(CommandBuffer cb, float strength, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material)
			{ }

			private static bool CheckSupportCompression()
			{ return default; }

			private static RenderTextureFormat GetPreferredRenderTextureFormat()
			{ return default; }

			private Frame GetFrameRelative(int offset)
			{ return default; }
		}

		private ReconstructionFilter m_ReconstructionFilter;

		private FrameBlendingFilter m_FrameBlendingFilter;

		private bool m_FirstFrame;

		public ReconstructionFilter reconstructionFilter
		{
			get
			{ return default; }
		}

		public FrameBlendingFilter frameBlendingFilter
		{
			get
			{ return default; }
		}

		public override bool active
		{
			get
			{ return default; }
		}

		public override string GetName()
		{ return default; }

		public void ResetHistory()
		{ }

		public override DepthTextureMode GetCameraFlags()
		{ return default; }

		public override CameraEvent GetCameraEvent()
		{ return default; }

		public override void OnEnable()
		{ }

		public override void PopulateCommandBuffer(CommandBuffer cb)
		{ }

		public override void OnDisable()
		{ }

		public MotionBlurComponent()
		{ }
	}
}
