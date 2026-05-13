namespace MarsSDK
{
	internal interface ISelectPhoto
	{
		bool InitCamera();

		void SelectPhoto(float quality);

		void SelectPhotoFromGallery(float quality);

		void SelectPhotoFromCamera(float quality);

		bool HasCamera();
	}
}
