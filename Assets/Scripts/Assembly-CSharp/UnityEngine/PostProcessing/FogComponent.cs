using Cpp2IlInjected;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	public sealed class FogComponent : PostProcessingComponentCommandBuffer<FogModel>
	{
		private static class Uniforms
		{
			internal static readonly int _FogColor;

			internal static readonly int _Density;

			internal static readonly int _Start;

			internal static readonly int _End;

			internal static readonly int _TempRT;

			static Uniforms()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private const string k_ShaderString = "Hidden/Post FX/Fog";

		public override bool active
		{
			get
			{ return default; }
		}

		public override string GetName()
		{ return default; }

		public override DepthTextureMode GetCameraFlags()
		{ return default; }

		public override CameraEvent GetCameraEvent()
		{ return default; }

		public override void PopulateCommandBuffer(CommandBuffer cb)
		{ }

		public FogComponent()
		{ }
	}
}
