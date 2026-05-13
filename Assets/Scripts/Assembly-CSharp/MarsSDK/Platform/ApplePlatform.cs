using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class ApplePlatform : BasePlatform
	{
		public delegate void dEventProcess(string[] args);

		public const string APPLE_PLATFORM_MSG_SIWA_RESULT = "1";

		public const int AppleAuthorizationResult_Succeeded = 0;

		public const int AppleAuthorizationResult_password_Succeeded = 1;

		public const int AppleAuthorizationResult_Failed = 2;

		public const int AppleAuthorizationResult_Canceled = 3;

		public const string DefaultAppleUser = "Apple User";

		private static ApplePlatform mInstance;

		public static dEventProcess doAppleEventSIWASucceeded;

		public static dEventProcess doAppleEventpasswordSucceeded;

		public static dEventProcess doAppleEventSIWAFailed;

		public static dEventProcess doAppleEventSIWACanceled;

		private static AndroidJavaClass mJc;

		public ApplePlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static ApplePlatform Instance()
		{ return default; }

		private void MsgProcessSIWAResult(string[] args)
		{ }

		public static AndroidJavaClass GetAppleClass()
		{ return default; }

		public bool IsSupportSIWA()
		{ return default; }

		public void LoginByApple()
		{ }

		public void BindByApple()
		{ }

		public void UnBindByApple()
		{ }

		public bool IsBindApple()
		{ return default; }

		public string GetAppleUserID()
		{ return default; }

		public string GetAppleDisplayName()
		{ return default; }
	}
}
