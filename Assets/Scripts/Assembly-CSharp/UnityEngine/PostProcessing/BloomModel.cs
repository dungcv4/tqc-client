using System;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	[Serializable]
	public class BloomModel : PostProcessingModel
	{
		[Serializable]
		public struct BloomSettings
		{
			[Min(0f)]
			[Tooltip("Strength of the bloom filter.")]
			public float intensity;

			[Min(0f)]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual (0 = hard threshold, 1 = soft threshold).")]
			public float softKnee;

			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;

			public float thresholdLinear
			{
				get
				{ return default; }
				set
				{ }
			}

			public static BloomSettings defaultSettings
			{
				get
				{ return default; }
			}
		}

		[Serializable]
		public struct LensDirtSettings
		{
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture texture;

			[Tooltip("Amount of lens dirtiness.")]
			[Min(0f)]
			public float intensity;

			public static LensDirtSettings defaultSettings
			{
				get
				{ return default; }
			}
		}

		[Serializable]
		public struct Settings
		{
			public BloomSettings bloom;

			public LensDirtSettings lensDirt;

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

		public BloomModel()
		{ }
	}
}
