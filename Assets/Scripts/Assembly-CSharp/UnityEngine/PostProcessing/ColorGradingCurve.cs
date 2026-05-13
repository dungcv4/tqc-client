using System;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	[Serializable]
	public sealed class ColorGradingCurve
	{
		public AnimationCurve curve;

		[SerializeField]
		private bool m_Loop;

		[SerializeField]
		private float m_ZeroValue;

		[SerializeField]
		private float m_Range;

		private AnimationCurve m_InternalLoopingCurve;

		public ColorGradingCurve(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Cache()
		{ }

		public float Evaluate(float t)
		{ return default; }
	}
}
