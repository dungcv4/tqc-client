using System;
using System.Collections.Generic;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public sealed class MaterialFactory : IDisposable
	{
		private Dictionary<string, Material> m_Materials;

		public MaterialFactory()
		{ }

		public Material Get(string shaderName)
		{ return default; }

		public void Dispose()
		{ }
	}
}
