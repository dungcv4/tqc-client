using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public abstract class PostProcessingComponent<T> : PostProcessingComponentBase where T : PostProcessingModel
	{
		public T model
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			internal set
			{ }
		}

		public virtual void Init(PostProcessingContext pcontext, T pmodel)
		{ }

		public override PostProcessingModel GetModel()
		{ return default; }

		protected PostProcessingComponent()
		{ }
	}
}
