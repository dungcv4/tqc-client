using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
	{
		private static class Uniforms
		{
			internal static readonly int _UserLut;

			internal static readonly int _UserLut_Params;

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

		public void OnGUI()
		{ }

		public UserLutComponent()
		{ }
	}
}
