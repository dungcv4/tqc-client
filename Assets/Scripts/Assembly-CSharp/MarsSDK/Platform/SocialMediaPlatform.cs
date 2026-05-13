using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class SocialMediaPlatform : BasePlatform
	{
		public delegate void dEventProcess(string[] args);

		public const string PLATFORM_CALLBACK_SHAREPHOTO_ONSUCCESS_MSG = "1";

		public const string PLATFORM_CALLBACK_SHAREPHOTO_ONCANCEL_MSG = "2";

		public const string PLATFORM_CALLBACK_SHAREPHOTO_ONERROR_MSG = "3";

		public const string PLATFORM_CALLBACK_SHAREPHOTO_ONSEND_MSG = "4";

		private static SocialMediaPlatform mInstance;

		public static dEventProcess doCallbackSharePhotoOnSuccess;

		public static dEventProcess doCallbackSharePhotoOnCancel;

		public static dEventProcess doCallbackSharePhotoOnError;

		public static dEventProcess doCallbackSharePhotoOnSend;

		private static AndroidJavaClass mJc;

		public SocialMediaPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static SocialMediaPlatform Instance()
		{ return default; }

		private void MsgProcessOnSuccess(string[] args)
		{ }

		private void MsgProcessOnCancel(string[] args)
		{ }

		private void MsgProcessOnError(string[] args)
		{ }

		private void MsgProcessOnSend(string[] args)
		{ }

		public static AndroidJavaClass GetSocialMediaPlatformClass()
		{ return default; }

		public bool OpenFacebookFanPage()
		{ return default; }

		public bool OpenTwitterFanPage()
		{ return default; }

		public bool OpenNaverCafeFanPage()
		{ return default; }
	}
}
