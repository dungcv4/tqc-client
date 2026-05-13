using System;
using Cpp2IlInjected;
using LuaInterface;
using MarsAgent.Login;
using MarsSDK.LitJson;
using UnityEngine;

namespace MarsSDK
{
	public class MarsFunction
	{
		public class DeleteAccountAction
		{
			public static bool IsWaitingToReset()
			{ return default; }

			public static long GetRestoreTimestamp()
			{ return default; }

			public static void RequestDeleteAccount()
			{ }

			public static void RequestRestoreAccount()
			{ }

			public static void ShowMarsDeleteAccountUI()
			{ }

			public static void ShowMarsRestoreAccountUI()
			{ }

			public DeleteAccountAction()
			{ }
		}

		private static AndroidJavaObject deleteAccountBridge;

		public MarsFunction()
		{ }

		public static void ClearInfoForLogin()
		{ }

		public static string GetDeviceId()
		{ return default; }

		public static string GetPlayerId()
		{ return default; }

		public static JsonData GetBindPlatformList()
		{ return default; }

		public static bool IsBindAnyPlatform()
		{ return default; }

		public static string GetPassword()
		{ return default; }

		public static string GetServerUrl()
		{ return default; }

		public static string GetLoginSession()
		{ return default; }

		public static string GetLoginPasskey()
		{ return default; }

		public static JsonData GetJsonLoginPasskey()
		{ return default; }

		public static void LoginByOneClick()
		{ }

		public static void LoginByOneClickV2()
		{ }

		public static void LoginByHashAccountId()
		{ }

		public static bool IsAlreadyLogin()
		{ return default; }

		public static bool HasInfoForLogin()
		{ return default; }

		public static bool IsNewAccount()
		{ return default; }

		public static bool LoginByPlayerIDWithPassword(string playerid, string password)
		{ return default; }

		public static void LoginExistedAccountBySNS(PlatformDefinition platform, string token)
		{ }

		public static void LoginExistAccountBySNS(PlatformDefinition platform, string token, bool autoCreate = false)
		{ }

		public static bool ModifyPassword(string newpass, string confirmnewpass)
		{ return default; }

		public static bool IsBindNativePlatform()
		{ return default; }

		public static void LoginByNativePlatform()
		{ }

		public static void BindNativePlatform()
		{ }

		public static void UnBindNativePlatform()
		{ }

		public static void LoginByFacebook()
		{ }

		public static void LoginByFacebook(bool updateToken)
		{ }

		public static void LoginByFacebook(string[] permission)
		{ }

		public static void LoginByFacebook(string[] permission, bool updateToken)
		{ }

		public static void UnBindFacebook()
		{ }

		public static bool IsBindFacebook()
		{ return default; }

		public static void BindByFacebook()
		{ }

		public static void BindByFacebook(string[] permission, bool replace)
		{ }

		public static void LoginByUserjoyFacebook(string token = null)
		{ }

		public static void LoginByUserjoyFacebook(bool updateToken)
		{ }

		public static void LoginByUserjoyFacebook(string[] permission)
		{ }

		public static void LoginByUserjoyFacebook(string[] permission, bool updateToken)
		{ }

		public static void BindByUserjoyFacebook(string token = null)
		{ }

		public static void BindByUserjoyFacebook(string[] permission, bool replace)
		{ }

		public static void RequestSetNickname(string nickname)
		{ }

		public static void CheckRequestSetNickname(string nickname)
		{ }

		public static void OpenLoginAccountPanel()
		{ }

		public static void OpenUserCenterPanel()
		{ }

		public static string GetDSSDownloadURL()
		{ return default; }

		public static void UseSystemData(bool enabled)
		{ }

		public static void RequestResetAccount()
		{ }

		public static bool BindEmailAccount(string mailaccount, string password)
		{ return default; }

		public static void UnBindEmailAccount(string mailaccount, string password)
		{ }

		public static bool CreateEmailAccount(string mailaccount, string password)
		{ return default; }

		public static bool LoginEmailAccount(string mailaccount, string password)
		{ return default; }

		public static bool ModifyEmailAccountPassword(string password, string newpassword)
		{ return default; }

		public static bool ForgotEmailAccountPassword(string mailaccount)
		{ return default; }

		public static string GetMobileMailAccount()
		{ return default; }

		public static bool SetMobileMailAccount(string mailaccount)
		{ return default; }

		public static bool IsBindEmail()
		{ return default; }

		public static void OpenLoginMailPanel()
		{ }

		public static void OpenCreateMailPanel()
		{ }

		public static void OpenModifyMailPanel()
		{ }

		public static void OpenForgotMailPanel()
		{ }

		[NoToLua]
		[Obsolete("Call MarsFunction.GetPlayerId instead.", false)] // shim: v1 Wrap compat
		public static string GetAccountId()
		{ return default; }

		[Obsolete("To change the ServiceURL, please re-initiate the SDK by using StartCoroutine(UJMSDK_Main.InitMSDK(url)) ", false)] // shim: v1 Wrap compat
		public static void SetServiceUrl(string url)
		{ }

		[Obsolete("This setting will reply from the initialized Service URL", false)] // shim: v1 Wrap compat
		public static void SetDSSDownloadURL(string url)
		{ }

		[Obsolete("This setting will reply from the initialized Service URL", false)] // shim: v1 Wrap compat
		public static void SetDSSUploadURL(string url)
		{ }

		[Obsolete("This setting will reply from the initialized Service URL", false)] // shim: v1 Wrap compat
		public static void SetImagePersonalUploadURL(string url)
		{ }

		[Obsolete("This setting will reply from the initialized Service URL", false)] // shim: v1 Wrap compat
		public static void SetImageMessageUploadURL(string url)
		{ }

		static MarsFunction()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
