using System;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	[Serializable]
	public abstract class PostProcessingModel
	{
		[SerializeField]
		[GetSet("enabled")]
		private bool m_Enabled;

		public bool enabled
		{
			get
			{ return default; }
			set
			{ }
		}

		public abstract void Reset();

		public virtual void OnValidate()
		{ }

		protected PostProcessingModel()
		{ }
	}
}
