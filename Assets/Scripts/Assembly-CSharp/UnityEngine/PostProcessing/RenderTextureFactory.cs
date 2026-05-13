using System;
using System.Collections.Generic;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class RenderTextureFactory : IDisposable
	{
		private HashSet<RenderTexture> m_TemporaryRTs;

		public RenderTextureFactory()
		{ }

		public RenderTexture Get(RenderTexture baseRenderTexture)
		{ return default; }

		public RenderTexture Get(int width, int height, int depthBuffer = 0, RenderTextureFormat format = RenderTextureFormat.ARGBHalf, RenderTextureReadWrite rw = RenderTextureReadWrite.Default, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, string name = "FactoryTempTexture")
		{ return default; }

		public void Release(RenderTexture rt)
		{ }

		public void ReleaseAll()
		{ }

		public void Dispose()
		{ }
	}
}
