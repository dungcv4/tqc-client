using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace MarsSDK.Platform
{
	public class SelectPhotoPlatform : BasePlatformSingleton<SelectPhotoPlatform>
	{
		public delegate void OnSelectPhotoSuccess(byte[] photoByteArray, int width, int height);

		public delegate void OnSelectPhotoFailed(string errorCode);

		public delegate void OnSelectPhotoCancel();

		public const string SELECT_PHOTO_FROM_GALLERY_OR_CAMERA_SUCCESS = "1";

		public const string SELECT_PHOTO_FROM_GALLERY_OR_CAMERA_FAILED = "2";

		public const string SELECT_PHOTO_FROM_GALLERY_OR_CAMERA_CANCEL = "3";

		private ISelectPhoto _seletePhotoAction;

		public static event OnSelectPhotoSuccess onSelectPhotoSuccess
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static event OnSelectPhotoFailed onSelectPhotoFailed
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static event OnSelectPhotoCancel onSelectPhotoCancel
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public SelectPhotoPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		protected override void V_doMessageProcess(string msg, string[] args)
		{ }

		private void ReceiveData(string data, int width, int height)
		{ }

		public void SelectPhoto(float quality)
		{ }

		public void SelectPhotoFromGallery(float quality)
		{ }

		public void SelectPhotoFromCamera(float quality)
		{ }

		public bool InitCamera()
		{ return default; }

		public bool HasCamera()
		{ return default; }
	}
}
