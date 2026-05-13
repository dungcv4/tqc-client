using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace UnityEngine.PostProcessing
{
	public class PostProcessingContext
	{
		public PostProcessingProfile profile;

		public Camera camera;

		public MaterialFactory materialFactory;

		public RenderTextureFactory renderTextureFactory;

		public bool interrupted
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			private set
			{ }
		}

		public bool isGBufferAvailable
		{
			get
			{ return default; }
		}

		public bool isHdr
		{
			get
			{ return default; }
		}

		public int width
		{
			get
			{ return default; }
		}

		public int height
		{
			get
			{ return default; }
		}

		public Rect viewport
		{
			get
			{ return default; }
		}

		public void Interrupt()
		{ }

		public PostProcessingContext Reset()
		{ return default; }

		public PostProcessingContext()
		{ }
	}
}
