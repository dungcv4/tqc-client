using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class InstagramPlatform : BasePlatform
	{
		public delegate void dMsgProcess(string[] args);

		private static InstagramPlatform mInstance;

		public const string SHAREPHOTO_API = "SharePhoto";

		public static string INSTAGRAM_PLATFORM_CALLBACK_ONSUCCESS_MSG;

		public static string INSTAGRAM_PLATFORM_CALLBACK_ONCANCEL_MSG;

		public static string INSTAGRAM_PLATFORM_CALLBACK_ONERROR_MSG;

		public static dMsgProcess doMsgProcessCallbackOnSuccess;

		public static dMsgProcess doMsgProcessCallbackOnCancel;

		public static dMsgProcess doMsgProcessCallbackOnError;

		private static AndroidJavaClass mJc;

		public InstagramPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static InstagramPlatform Instance()
		{ return default; }

		private static void MsgProcessOnSuccess(string[] args)
		{ }

		private static void MsgProcessOnCancel(string[] args)
		{ }

		private static void MsgProcessOnError(string[] args)
		{ }

		public static AndroidJavaClass GetInstagramClass()
		{ return default; }

		public static void SharePhoto(string imgPath)
		{ }

		static InstagramPlatform()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
