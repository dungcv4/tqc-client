using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	public sealed class BuiltinDebugViewsComponent : PostProcessingComponentCommandBuffer<BuiltinDebugViewsModel>
	{
		private static class Uniforms
		{
			internal static readonly int _DepthScale;

			internal static readonly int _TempRT;

			internal static readonly int _Opacity;

			internal static readonly int _MainTex;

			internal static readonly int _TempRT2;

			internal static readonly int _Amplitude;

			internal static readonly int _Scale;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private enum Pass
		{
			Depth = 0,
			Normals = 1,
			MovecOpacity = 2,
			MovecImaging = 3,
			MovecArrows = 4
		}

		private class ArrowArray
		{
			public Mesh mesh
			{
				[CompilerGenerated]
				get
				{ return default; }
				[CompilerGenerated]
				private set
				{ }
			}

			public int columnCount
			{
				[CompilerGenerated]
				get
				{ return default; }
				[CompilerGenerated]
				private set
				{ }
			}

			public int rowCount
			{
				[CompilerGenerated]
				get
				{ return default; }
				[CompilerGenerated]
				private set
				{ }
			}

			public void BuildMesh(int columns, int rows)
			{ }

			public void Release()
			{ }

			public ArrowArray()
			{ }
		}

		private const string k_ShaderString = "Hidden/Post FX/Builtin Debug Views";

		private ArrowArray m_Arrows;

		public override bool active
		{
			get
			{ return default; }
		}

		public override DepthTextureMode GetCameraFlags()
		{ return default; }

		public override CameraEvent GetCameraEvent()
		{ return default; }

		public override string GetName()
		{ return default; }

		public override void PopulateCommandBuffer(CommandBuffer cb)
		{ }

		private void DepthPass(CommandBuffer cb)
		{ }

		private void DepthNormalsPass(CommandBuffer cb)
		{ }

		private void MotionVectorsPass(CommandBuffer cb)
		{ }

		private void PrepareArrows()
		{ }

		public override void OnDisable()
		{ }

		public BuiltinDebugViewsComponent()
		{ }
	}
}
