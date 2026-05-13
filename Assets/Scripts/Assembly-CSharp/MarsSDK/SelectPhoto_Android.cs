using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	internal class SelectPhoto_Android : ISelectPhoto
	{
		private static AndroidJavaObject GetPlatformInstance()
		{ return default; }

		public void SelectPhoto(float quality)
		{ }

		public void SelectPhotoFromGallery(float quality)
		{ }

		public void SelectPhotoFromCamera(float quality)
		{ }

		public bool HasCamera()
		{ return default; }

		public bool InitCamera()
		{ return default; }

		public SelectPhoto_Android()
		{ }
	}
}
