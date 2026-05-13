using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public static class GraphicsUtils
	{
		private static Texture2D s_WhiteTexture;

		private static Mesh s_Quad;

		public static bool isLinearColorSpace
		{
			get
			{ return default; }
		}

		public static bool supportsDX11
		{
			get
			{ return default; }
		}

		public static Texture2D whiteTexture
		{
			get
			{ return default; }
		}

		public static Mesh quad
		{
			get
			{ return default; }
		}

		public static void Blit(Material material, int pass)
		{ }

		public static void ClearAndBlit(Texture source, RenderTexture destination, Material material, int pass, bool clearColor = true, bool clearDepth = false)
		{ }

		public static void Destroy(Object obj)
		{ }

		public static void Dispose()
		{ }
	}
}
