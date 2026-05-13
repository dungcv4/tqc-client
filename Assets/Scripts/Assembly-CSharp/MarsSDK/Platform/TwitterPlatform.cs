using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class TwitterPlatform : BasePlatform
	{
		public delegate void dMsgProcess(string[] args);

		private static TwitterPlatform mInstance;

		public const string TWITTER_PLATFORM_LOGIN_VERIFY_SUCCEEDED = "1";

		public const string TWITTER_PLATFORM_LOGIN_VERIFY_FAILED = "2";

		public const string TWITTER_PLATFORM_POST_TWEET_SUCCEEDED = "3";

		public const string TWITTER_PLATFORM_POST_TWEET_FAILED = "4";

		public static dMsgProcess doTwitterEventLoginVerifySucceeded;

		public static dMsgProcess doTwitterEventLoginVerifyFailed;

		public static dMsgProcess doTwitterEventPostTweetSucceeded;

		public static dMsgProcess doTwitterEventPostTweetFailed;

		private static AndroidJavaClass mJc;

		public TwitterPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static TwitterPlatform Instance()
		{ return default; }

		private static void MsgProcessLoginVerifySucceeded(string[] args)
		{ }

		private static void MsgProcessLoginVerifyFailed(string[] args)
		{ }

		private static void MsgProcessPostTweetSucceeded(string[] args)
		{ }

		private static void MsgProcessPostTweetFailed(string[] args)
		{ }

		public static AndroidJavaClass GetTwitterClass()
		{ return default; }

		public void LoginByTwitter()
		{ }

		public void BindByTwitter()
		{ }

		public void UnBindByTwitter()
		{ }

		public bool IsBindTwitter()
		{ return default; }

		public string GetTwitterAccountUID()
		{ return default; }

		public string GetTwitterAccountDisplayName()
		{ return default; }

		public string GetTwitterAccountPhotoURL()
		{ return default; }

		public void PostTweet(string message)
		{ }

		public void PostTweetWithImage(string message, byte[] imageData)
		{ }
	}
}
