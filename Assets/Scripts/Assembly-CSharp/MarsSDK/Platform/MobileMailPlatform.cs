using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class MobileMailPlatform : BasePlatform
	{
		public delegate void dMsgProcess(string[] args);

		public const string MOBILEMAIL_PLATFORM_MSG_SUCCESS = "1";

		public const string MOBILEMAIL_PLATFORM_MSG_ERROR = "2";

		public const string LOGIN = "Login";

		public const string CREATE_ACCOUNT = "CreateAccount";

		public const string MODIFY_PASSWORD = "ModifyPassword";

		public const string FORGOT_PASSWORD = "ForgotPassword";

		private static MobileMailPlatform mInstance;

		public static dMsgProcess doMobileMailCallbackOnSuccess;

		public static dMsgProcess doMobileMailCallbackOnError;

		private static AndroidJavaClass mJc;

		public MobileMailPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static MobileMailPlatform Instance()
		{ return default; }

		private static void MsgProcessSuccess(string[] args)
		{ }

		private static void MsgProcessError(string[] args)
		{ }

		public static AndroidJavaClass GetMobileMailClass()
		{ return default; }

		public static bool IsBindEmail()
		{ return default; }

		public static bool BindEmailAccount(string account, string password)
		{ return default; }

		public static bool UnBindEmailAccount(string account, string password)
		{ return default; }

		public static bool CreateEmailAccount(string account, string password)
		{ return default; }

		public static bool LoginEmailAccount(string account, string password)
		{ return default; }

		public static bool ModifyEmailAccountPassword(string account, string password, string newpassword)
		{ return default; }

		public static bool ForgotEmailAccountPassword(string account)
		{ return default; }

		public static string GetMobileMailAccount()
		{ return default; }

		public static bool SetMobileMailAccount(string mailaccount)
		{ return default; }

		public static string GetBindMailUID()
		{ return default; }

		public static void OpenLoginPanel()
		{ }

		public static void OpenCreatePanel()
		{ }

		public static void OpenModifyPanel()
		{ }

		public static void OpenForgotPanel()
		{ }
	}
}
