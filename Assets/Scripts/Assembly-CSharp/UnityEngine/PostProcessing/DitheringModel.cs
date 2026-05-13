using System;
using System.Runtime.InteropServices;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	[Serializable]
	public class DitheringModel : PostProcessingModel
	{
		[Serializable]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct Settings
		{
			public static Settings defaultSettings
			{
				get
				{ return default; }
			}
		}

		[SerializeField]
		private Settings m_Settings;

		public Settings settings
		{
			get
			{ return default; }
			set
			{ }
		}

		public override void Reset()
		{ }

		public DitheringModel()
		{ }
	}
}
