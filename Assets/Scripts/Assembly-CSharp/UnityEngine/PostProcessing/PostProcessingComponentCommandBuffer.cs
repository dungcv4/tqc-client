using Cpp2IlInjected;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		public abstract CameraEvent GetCameraEvent();

		public abstract string GetName();

		public abstract void PopulateCommandBuffer(CommandBuffer cb);

		protected PostProcessingComponentCommandBuffer()
		{ }
	}
}
