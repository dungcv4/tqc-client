using Cpp2IlInjected;
using UnityEngine;

namespace Plugins.Scripts
{
	public class FXHYSDKUnityClass
	{
		public delegate void ResultCallBack(int code, params string[] strings);

		private sealed class classCallBack_ShowPersonalInfoGuideDialog : AndroidJavaProxy
		{
			public classCallBack_ShowPersonalInfoGuideDialog() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onAgree()
			{ }

			public void onNotAgree()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_InitSDK : AndroidJavaProxy
		{
			public classCallBack_InitSDK() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onInitialized()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_StartLogin : AndroidJavaProxy
		{
			public classCallBack_StartLogin() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void loginSuccess(string uid, string token)
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_StartUserCenter : AndroidJavaProxy
		{
			public classCallBack_StartUserCenter() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void loginSuccess(string uid, string token)
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_ShowCloseAccount : AndroidJavaProxy
		{
			public classCallBack_ShowCloseAccount() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onSuccess()
			{ }

			public void onCancel()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_ShowRealNameAuthentication : AndroidJavaProxy
		{
			public classCallBack_ShowRealNameAuthentication() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onSuccess(string yyyymmdd)
			{ }

			public void onFail()
			{ }

			public void onCancel()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_ShowRoundIdentityInfo : AndroidJavaProxy
		{
			public classCallBack_ShowRoundIdentityInfo() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onSuccess()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_UploadGameUserInfo : AndroidJavaProxy
		{
			public classCallBack_UploadGameUserInfo() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onSuccess()
			{ }

			public void onFail()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_CreateOrderAndPay : AndroidJavaProxy
		{
			public classCallBack_CreateOrderAndPay() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void onPaid()
			{ }

			public void onError(int code, string msg)
			{ }
		}

		private sealed class classCallBack_OpenTapUpdate : AndroidJavaProxy
		{
			public classCallBack_OpenTapUpdate() : base((string)null!)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public void cancelUpdate()
			{ }
		}

		private static FXHYSDKUnityClass sm_this;

		private ResultCallBack sm_ResultCallBack;

		private static AndroidJavaObject sm_AndroidJavaObject_FengxiaMESDK_instance;

		private static AndroidJavaObject sm_AndroidJavaObject_currentActivity;

		private static AndroidJavaObject sm_AndroidJavaObject_ApplicationContext;

		public static FXHYSDKUnityClass GetInstance()
		{ return default; }

		public void setResultCallBack(ResultCallBack cb)
		{ }

		private static AndroidJavaObject GetAndroidJavaInstance()
		{ return default; }

		private static AndroidJavaObject GetAndroidJavaActivity()
		{ return default; }

		private static AndroidJavaObject GetAndroidJavaContext()
		{ return default; }

		public void ShowPersonalInfoGuideDialog()
		{ }

		public void InitSDK(string appId, string appKey)
		{ }

		public bool IsLogin()
		{ return default; }

		public void StartLogin()
		{ }

		public bool StartLogout()
		{ return default; }

		public void StartUserCenter()
		{ }

		public void ShowCloseAccount()
		{ }

		public void ShowRealNameAuthentication()
		{ }

		public void ShowRoundIdentityInfo()
		{ }

		public void ShowWebDialog(string url)
		{ }

		public void ShowWebUIDialog(string url)
		{ }

		public void ShowWebSystem(string url)
		{ }

		public void UploadGameUserInfo(string type, string areaname, string serverid, string role_id)
		{ }

		public void CreateOrderAndPay(string orderId, string productId, string productName, string price, string area, string fromwhere, string server, string playerid, string notifyUrl, string sign)
		{ }

		public string[] GetSDKVersionInfo()
		{ return default; }

		public string[] GetGameRegionName()
		{ return default; }

		public bool IsTapUserLogin()
		{ return default; }

		public bool OpenTapReview()
		{ return default; }

		public void OpenAppStoreReview()
		{ }

		public bool IsTapChannel()
		{ return default; }

		public void OpenTapUpdate()
		{ }

		public void SetDebugMode(bool isDebug)
		{ }

		public void SetTestEnvironment(int code)
		{ }

		public FXHYSDKUnityClass()
		{ }
	}
}
