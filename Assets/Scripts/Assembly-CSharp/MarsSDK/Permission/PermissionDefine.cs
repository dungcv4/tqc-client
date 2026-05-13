using Cpp2IlInjected;

namespace MarsSDK.Permission
{
	public class PermissionDefine
	{
		public class Android
		{
			public const string READ_EXTERNAL_STORAGE = "android.permission.READ_EXTERNAL_STORAGE";

			public const string WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";

			public const string RECORD_AUDIO = "android.permission.RECORD_AUDIO";

			public const string CAMERA = "android.permission.CAMERA";

			public const string ACCESS_COARSE_LOCATION = "android.permission.ACCESS_COARSE_LOCATION";

			public const string ACCESS_FINE_LOCATION = "android.permission.ACCESS_FINE_LOCATION";

			public const string POST_NOTIFICATIONS = "android.permission.POST_NOTIFICATIONS";

			public Android()
			{ }
		}

		public class iOS
		{
			public const string PHOTO = "Photo";

			public const string MICROPHONE = "Microphone";

			public const string CAMERA = "Camera";

			public const string LOCATION = "Location";

			public const string ATTRACKING = "ATTracking";

			public iOS()
			{ }
		}

		public PermissionDefine()
		{ }
	}
}
