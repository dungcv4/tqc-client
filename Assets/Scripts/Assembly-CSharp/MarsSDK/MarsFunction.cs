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

		// Source: Ghidra decompiled/MarsSDK.MarsFunction/*.c — these are thin facades that
		// delegate 1-1 to MarsSDK.LoginMgr (already ported 1-1). RVAs in comments.

		// RVA 0x199AA3C → LoginMgr.ClearMarsInfoForLogin
		public static void ClearInfoForLogin()
		{ LoginMgr.ClearMarsInfoForLogin(); }

		// RVA 0x199AA88 → LoginMgr.GetDeviceID
		public static string GetDeviceId()
		{ return LoginMgr.GetDeviceID(); }

		// RVA 0x199AAD4 → LoginMgr.GetPlayerID
		public static string GetPlayerId()
		{ return LoginMgr.GetPlayerID(); }

		// RVA 0x199AB20 → LoginMgr.GetBindPlatformList
		public static JsonData GetBindPlatformList()
		{ return LoginMgr.GetBindPlatformList(); }

		// RVA 0x199AB6C → LoginMgr.IsBindAnyPlatform
		public static bool IsBindAnyPlatform()
		{ return LoginMgr.IsBindAnyPlatform(); }

		// RVA 0x199ABB8 → LoginMgr.GetOneClickPassword
		public static string GetPassword()
		{ return LoginMgr.GetOneClickPassword(); }

		// RVA 0x199AC04 → LoginMgr.GetServiceURL
		public static string GetServerUrl()
		{ return LoginMgr.GetServiceURL(); }

		// RVA 0x199AC50 → LoginMgr.GetLoginSession
		public static string GetLoginSession()
		{ return LoginMgr.GetLoginSession(); }

		// RVA 0x199AC9C → LoginMgr.GetPassKey
		public static string GetLoginPasskey()
		{ return LoginMgr.GetPassKey(); }

		// RVA 0x199ACE8 — 1-1: new JsonData(); jd.Add(GetLoginPasskey()); return jd;
		public static JsonData GetJsonLoginPasskey()
		{
			JsonData jd = new JsonData();
			jd.Add(GetLoginPasskey());
			return jd;
		}

		// RVA 0x199AD7C → LoginMgr.LoginByOneClick
		public static void LoginByOneClick()
		{ LoginMgr.LoginByOneClick(); }

		// RVA 0x199ADC8 → LoginMgr.LoginByOneClickV2
		public static void LoginByOneClickV2()
		{ LoginMgr.LoginByOneClickV2(); }

		// → LoginMgr.LoginByHashAccountId
		public static void LoginByHashAccountId()
		{ LoginMgr.LoginByHashAccountId(); }

		// RVA 0x199AE60 — 1-1: !IsNullOrEmpty(GetLoginPasskey()) && !IsNullOrEmpty(GetLoginSession())
		public static bool IsAlreadyLogin()
		{
			if (string.IsNullOrEmpty(GetLoginPasskey()))
			{
				return false;
			}
			return !string.IsNullOrEmpty(GetLoginSession());
		}

		// RVA 0x199AEE8 → LoginMgr.HasInfoForLogin
		public static bool HasInfoForLogin()
		{ return LoginMgr.HasInfoForLogin(); }

		// → LoginMgr.IsNewAccount
		public static bool IsNewAccount()
		{ return LoginMgr.IsNewAccount(); }

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
