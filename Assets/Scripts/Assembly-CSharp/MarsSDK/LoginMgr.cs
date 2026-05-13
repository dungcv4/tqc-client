// Source: work/03_il2cpp_dump/dump.cs MarsSDK.LoginMgr (TypeDefIndex: 1095) +
//   work/06_ghidra/decompiled_full/MarsSDK.LoginMgr/*.c
// All methods ported 1-1 from Ghidra pseudo-C. Where Unity Editor cannot reach
// Android JNI (AndroidJavaClass / AndroidJavaObject), behavior is documented per
// §E10 SDK strip (see MarsSDK.Permission.PermissionManager for the canonical
// template — same JNI shape, same null-guard pattern, same MarsLog.Info on failure).
//
// JNI target Java class: "com.userjoy.mars.core.LoginMgr"  (StringLit_15690).
// All static methods marshal directly into `_marsLoginMgr` AndroidJavaObject which
// is `LoginMgr.Instance(currentActivity)` on the Java side.

using System;
using UnityEngine;

using MarsSDK.LitJson;

namespace MarsSDK
{
	public class LoginMgr
	{
		// Fields — 1-1 from dump.cs:74147-74150
		public static string uID;          // 0x0
		private static bool _initialized;  // 0x8
		private static AndroidJavaObject _marsLoginMgr; // 0x10

		// Source: Ghidra .cctor.c RVA 0x019956bc
		// 1-1: if (Application.platform == RuntimePlatform.Android (0xb) && !_initialized)
		//          DoInitialize();
		// (the 0xb int compare = RuntimePlatform.Android per Unity enum)
		static LoginMgr()
		{
			if (Application.platform == RuntimePlatform.Android && !_initialized)
			{
				DoInitialize();
			}
		}

		// Source: Ghidra DoInitialize.c RVA 0x01995748
		// 1-1:
		//   var jc = new AndroidJavaClass("com.userjoy.mars.core.LoginMgr");  (StringLit_15690)
		//   _marsLoginMgr = jc.CallStatic<AndroidJavaObject>(
		//       "Instance",                                                   (StringLit_6609)
		//       UnityPlayer.currentActivity);
		//   if (_marsLoginMgr == null)
		//       MarsLog.Info("Initialize Failed!!", <ctx>);                   (StringLit_6574)
		//   else _initialized = true;
		// [§E10 SDK strip: Mars SDK Java AAR not present in Editor; AndroidJavaClass
		//  throws — wrap try/catch, leave _marsLoginMgr null + _initialized false,
		//  matching gốc's "Initialize Failed" branch.]
		public static void DoInitialize()
		{
			try
			{
				using (var jc = new AndroidJavaClass("com.userjoy.mars.core.LoginMgr"))
				{
					using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
					{
						var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
						_marsLoginMgr = jc.CallStatic<AndroidJavaObject>("Instance", activity);
					}
				}
			}
			catch (System.Exception ex)
			{
				UnityEngine.Debug.LogWarning("[LoginMgr.DoInitialize] §E10 stripped JNI: " + ex.Message);
				_marsLoginMgr = null;
			}

			if (_marsLoginMgr == null)
			{
				MarsLog.Info("Initialize Failed!!");
			}
			else
			{
				_initialized = true;
			}
		}

		// Source: Ghidra GetServiceURL.c RVA 0x01995ab0
		// 1-1: return _marsLoginMgr.Call<string>("GetServiceURL", currentActivity);  (StringLit_6033)
		public static string GetServiceURL()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetServiceURL] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetServiceURL", activity);
		}

		// Source: Ghidra SetServiceURL.c RVA 0x01995ba8
		// 1-1: _marsLoginMgr.Call("SetServiceURL", new object[]{url});  (StringLit_10146)
		[Obsolete("To change the ServiceURL, please re-initiate the SDK by using StartCoroutine(UJMSDK_Main.InitMSDK(url)) ", true)]
		public static void SetServiceURL(string url)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.SetServiceURL] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("SetServiceURL", new object[] { url });
		}

		// Source: Ghidra SetDSSDownloadURL.c RVA 0x01995c98
		// 1-1: _marsLoginMgr.Call("SetDSSDownloadURL", new object[]{url});  (StringLit_10014)
		[Obsolete("This setting will reply from the initialized Service URL", true)]
		public static void SetDSSDownloadURL(string url)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.SetDSSDownloadURL] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("SetDSSDownloadURL", new object[] { url });
		}

		// Source: Ghidra GetDSSDownloadURL.c RVA 0x01995d88
		// 1-1: return _marsLoginMgr.Call<string>("GetDSSDownloadURL", currentActivity);  (StringLit_5793)
		public static string GetDSSDownloadURL()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetDSSDownloadURL] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetDSSDownloadURL", activity);
		}

		// Source: Ghidra SetDSSUploadURL.c RVA 0x01995e80
		// 1-1: _marsLoginMgr.Call("SetDSSUploadURL", new object[]{url});  (StringLit_10015)
		[Obsolete("This setting will reply from the initialized Service URL", true)]
		public static void SetDSSUploadURL(string url)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.SetDSSUploadURL] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("SetDSSUploadURL", new object[] { url });
		}

		// Source: Ghidra SetImagePersonalUploadURL.c RVA 0x01995f70
		// 1-1: _marsLoginMgr.Call("SetImagePersonalUploadURL", new object[]{url});  (StringLit_10068)
		public static void SetImagePersonalUploadURL(string url)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.SetImagePersonalUploadURL] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("SetImagePersonalUploadURL", new object[] { url });
		}

		// Source: Ghidra SetImageMessageUploadURL.c RVA 0x01996060
		// 1-1: _marsLoginMgr.Call("SetImageMessageUploadURL", new object[]{url});  (StringLit_10067)
		public static void SetImageMessageUploadURL(string url)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.SetImageMessageUploadURL] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("SetImageMessageUploadURL", new object[] { url });
		}

		// Source: Ghidra ClearInfoForLogin.c RVA 0x01996150
		// 1-1: _marsLoginMgr.Call("ClearInfoForLogin", currentActivity);  (StringLit_4097)
		public static void ClearInfoForLogin()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.ClearInfoForLogin] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("ClearInfoForLogin", activity);
		}

		// Source: Ghidra ClearMarsInfoForLogin.c RVA 0x01996234
		// 1-1: _marsLoginMgr.Call("ClearMarsInfoForLogin", currentActivity);  (StringLit_4099)
		public static void ClearMarsInfoForLogin()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.ClearMarsInfoForLogin] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("ClearMarsInfoForLogin", activity);
		}

		// Source: Ghidra LoginByOneClick.c RVA 0x01996318
		// 1-1: _marsLoginMgr.Call("LoginByOneClick", currentActivity);  (StringLit_7735)
		public static void LoginByOneClick()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByOneClick] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("LoginByOneClick", activity);
		}

		// Source: Ghidra LoginByOneClickV2.c RVA 0x019963fc
		// 1-1: _marsLoginMgr.Call("LoginByOneClickV2", currentActivity);  (StringLit_7736)
		public static void LoginByOneClickV2()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByOneClickV2] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("LoginByOneClickV2", activity);
		}

		// Source: Ghidra LoginByHashAccountId.c RVA 0x019964e0
		// 1-1: _marsLoginMgr.Call("LoginByHashAccountId", currentActivity);  (StringLit_7733)
		public static void LoginByHashAccountId()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByHashAccountId] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("LoginByHashAccountId", activity);
		}

		// Source: Ghidra HasInfoForLogin.c RVA 0x019965c4
		// 1-1: return _marsLoginMgr.Call<bool>("HasInfoForLogin", currentActivity);  (StringLit_6219)
		public static bool HasInfoForLogin()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.HasInfoForLogin] §E10 stripped — _marsLoginMgr null"); return false; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<bool>("HasInfoForLogin", activity);
		}

		// Source: Ghidra LoginByPlayerIDWithPassword.c RVA 0x019966bc
		// 1-1: _marsLoginMgr.Call("LoginByPlayerIDWithPassword", new object[]{playerid, password});  (StringLit_7737)
		public static void LoginByPlayerIDWithPassword(string playerid, string password)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByPlayerIDWithPassword] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("LoginByPlayerIDWithPassword", new object[] { playerid, password });
		}

		// Source: Ghidra LoginByFacebook(string[],bool).c RVA 0x019968f0 — main overload.
		// 1-1: _marsLoginMgr.Call("LoginByFacebook", new object[]{permission, updateToken});  (StringLit_7730)
		// No-arg + (bool) overloads (RVA 0x019967ec, 0x0199683c) wrap this with null/default permission.
		// (Ghidra batch only kept the 3-param decompile for overload set; in IL2CPP overload chains
		// reuse the same StringLit_7730 — matching the cpp2il diff signatures.)
		public static void LoginByFacebook()
		{
			LoginByFacebook(null, false);
		}

		// Source: Ghidra LoginByFacebook.c RVA 0x0199683c (forwards to main overload)
		public static void LoginByFacebook(bool updateToken)
		{
			LoginByFacebook(null, updateToken);
		}

		// Source: Ghidra LoginByFacebook.c RVA 0x019968f0
		public static void LoginByFacebook(string[] permission, bool updateToken)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByFacebook] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("LoginByFacebook", new object[] { permission, updateToken });
		}

		// Source: Ghidra LoginByUserjoyFacebook(string[],bool).c RVA 0x01996b4c — main overload.
		// 1-1: _marsLoginMgr.Call("LoginByUserjoyFacebook", new object[]{permission, updateToken});  (StringLit_7742)
		public static void LoginByUserjoyFacebook()
		{
			LoginByUserjoyFacebook(null, false);
		}

		// Source: Ghidra LoginByUserjoyFacebook.c RVA 0x01996a9c (forwards to main overload)
		public static void LoginByUserjoyFacebook(bool updateToken)
		{
			LoginByUserjoyFacebook(null, updateToken);
		}

		// Source: Ghidra LoginByUserjoyFacebook.c RVA 0x01996b4c
		public static void LoginByUserjoyFacebook(string[] permission, bool updateToken)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByUserjoyFacebook] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("LoginByUserjoyFacebook", new object[] { permission, updateToken });
		}

		// Source: Ghidra BindByFacebook(string[],bool).c RVA 0x01996d8c — main overload.
		// 1-1: _marsLoginMgr.Call("BindByFacebook", new object[]{permission, replace});  (StringLit_3393)
		public static void BindByFacebook()
		{
			BindByFacebook(null, false);
		}

		// Source: Ghidra BindByFacebook.c RVA 0x01996d8c
		public static void BindByFacebook(string[] permission, bool replace)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.BindByFacebook] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("BindByFacebook", new object[] { permission, replace });
		}

		// Source: Ghidra BindByUserjoyFacebook(string[],bool).c RVA 0x01996fcc — main overload.
		// 1-1: _marsLoginMgr.Call("BindByUserjoyFacebook", new object[]{permission, replace});  (StringLit_3397)
		public static void BindByUserjoyFacebook()
		{
			BindByUserjoyFacebook(null, false);
		}

		// Source: Ghidra BindByUserjoyFacebook.c RVA 0x01996fcc
		public static void BindByUserjoyFacebook(string[] permission, bool replace)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.BindByUserjoyFacebook] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("BindByUserjoyFacebook", new object[] { permission, replace });
		}

		// Source: Ghidra GetPlayerID.c RVA 0x01997128
		// 1-1: return _marsLoginMgr.Call<string>("GetPlayerID", currentActivity);  (StringLit_5967)
		public static string GetPlayerID()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetPlayerID] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetPlayerID", activity);
		}

		// Source: Ghidra GetDeviceID.c RVA 0x01997220
		// 1-1: return _marsLoginMgr.Call<string>("GetDeviceID", currentActivity);  (StringLit_5802)
		public static string GetDeviceID()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetDeviceID] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetDeviceID", activity);
		}

		// Source: Ghidra GetOneClickPassword.c RVA 0x01997318
		// 1-1: return _marsLoginMgr.Call<string>("GetOneClickPassword", currentActivity);  (StringLit_5938)
		public static string GetOneClickPassword()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetOneClickPassword] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetOneClickPassword", activity);
		}

		// Source: Ghidra GetAccessToken.c RVA 0x01997410
		// 1-1: return _marsLoginMgr.Call<string>("GetAccessToken", currentActivity);  (StringLit_5695)
		public static string GetAccessToken()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetAccessToken] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetAccessToken", activity);
		}

		// Source: Ghidra IsNewAccount.c RVA 0x01997508
		// 1-1: return _marsLoginMgr.Call<bool>("IsNewAccount", currentActivity);  (StringLit_7052)
		public static bool IsNewAccount()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.IsNewAccount] §E10 stripped — _marsLoginMgr null"); return false; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<bool>("IsNewAccount", activity);
		}

		// Source: Ghidra GetLoginSession.c RVA 0x01997600
		// 1-1: return _marsLoginMgr.Call<string>("GetLoginSession", currentActivity);  (StringLit_5900)
		public static string GetLoginSession()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetLoginSession] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetLoginSession", activity);
		}

		// Source: Ghidra GetPassKey.c RVA 0x019976f8
		// 1-1: return _marsLoginMgr.Call<string>("GetPassKey", currentActivity);  (StringLit_5946)
		public static string GetPassKey()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetPassKey] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<string>("GetPassKey", activity);
		}

		// Source: Ghidra RequestSetNickname.c RVA 0x019977f0
		// 1-1: _marsLoginMgr.Call("RequestSetNickname", currentActivity);  (StringLit_9557)
		public static void RequestSetNickname()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.RequestSetNickname] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("RequestSetNickname", activity);
		}

		// Source: Ghidra CheckRequestSetNickname.c RVA 0x019978d4
		// 1-1: _marsLoginMgr.Call("CheckRequestSetNickname", new object[]{pid, name});  (StringLit_4045)
		public static void CheckRequestSetNickname(string pid, string name)
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.CheckRequestSetNickname] §E10 stripped — _marsLoginMgr null"); return; }
			_marsLoginMgr.Call("CheckRequestSetNickname", new object[] { pid, name });
		}

		// Source: Ghidra IsBindAnyPlatform.c RVA 0x01997a04
		// 1-1: return _marsLoginMgr.Call<bool>("IsBindAnyPlatform", currentActivity);  (StringLit_6923)
		public static bool IsBindAnyPlatform()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.IsBindAnyPlatform] §E10 stripped — _marsLoginMgr null"); return false; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<bool>("IsBindAnyPlatform", activity);
		}

		// Source: Ghidra GetBindPlatformList.c RVA 0x01997afc
		// 1-1:
		//   string s = _marsLoginMgr.Call<string>("GetBindPlatformListString", currentActivity);  (StringLit_5736)
		//   return JsonMapper.ToObject(s);
		public static JsonData GetBindPlatformList()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetBindPlatformList] §E10 stripped — _marsLoginMgr null"); return null; }
			AndroidJavaObject activity = GetCurrentActivity();
			string json = _marsLoginMgr.Call<string>("GetBindPlatformListString", activity);
			return JsonMapper.ToObject(json);
		}

		// Source: Ghidra LoginByGooglePlay.c RVA 0x01997c2c
		// 1-1: _marsLoginMgr.Call("LoginByGooglePlay", currentActivity);  (StringLit_7732)
		public static void LoginByGooglePlay()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.LoginByGooglePlay] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("LoginByGooglePlay", activity);
		}

		// Source: Ghidra BindByGooglePlay.c RVA 0x01997d10
		// 1-1: _marsLoginMgr.Call("BindByGooglePlay", currentActivity);  (StringLit_3395)
		public static void BindByGooglePlay()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.BindByGooglePlay] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("BindByGooglePlay", activity);
		}

		// Source: Ghidra UnBindGooglePlay.c RVA 0x01997df4
		// 1-1: _marsLoginMgr.Call("UnBindGooglePlay", currentActivity);  (StringLit_11984)
		public static void UnBindGooglePlay()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.UnBindGooglePlay] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("UnBindGooglePlay", activity);
		}

		// Source: Ghidra UnBindFacebook.c RVA 0x01997ed8
		// 1-1: _marsLoginMgr.Call("UnBindFacebook", currentActivity);  (StringLit_11983)
		public static void UnBindFacebook()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.UnBindFacebook] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("UnBindFacebook", activity);
		}

		// Source: Ghidra RequestResetAccount.c RVA 0x01997fbc
		// 1-1: _marsLoginMgr.Call("RequestResetAccount", currentActivity);  (StringLit_9551)
		public static void RequestResetAccount()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.RequestResetAccount] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("RequestResetAccount", activity);
		}

		// Source: Ghidra RequestRestoreAccount.c RVA 0x019980a0
		// 1-1: _marsLoginMgr.Call("RequestRestoreAccount", currentActivity);  (StringLit_9552)
		public static void RequestRestoreAccount()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.RequestRestoreAccount] §E10 stripped — _marsLoginMgr null"); return; }
			AndroidJavaObject activity = GetCurrentActivity();
			_marsLoginMgr.Call("RequestRestoreAccount", activity);
		}

		// Source: Ghidra GetRestoreTimestamp.c RVA 0x01998184
		// 1-1: return _marsLoginMgr.Call<long>("GetRestoreAccountTimestamp", currentActivity);  (StringLit_6011)
		public static long GetRestoreTimestamp()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.GetRestoreTimestamp] §E10 stripped — _marsLoginMgr null"); return 0L; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<long>("GetRestoreAccountTimestamp", activity);
		}

		// Source: Ghidra IsWaitingToReset.c RVA 0x0199827c
		// 1-1: return _marsLoginMgr.Call<bool>("IsWaitingToReset", currentActivity);  (StringLit_7141)
		public static bool IsWaitingToReset()
		{
			if (_marsLoginMgr == null) { UnityEngine.Debug.LogWarning("[LoginMgr.IsWaitingToReset] §E10 stripped — _marsLoginMgr null"); return false; }
			AndroidJavaObject activity = GetCurrentActivity();
			return _marsLoginMgr.Call<bool>("IsWaitingToReset", activity);
		}

		// Source: Ghidra .ctor.c RVA 0x01998374
		// 1-1: System_Object___ctor(this, 0)  ==  default Object base ctor — no-op in C#.
		public LoginMgr()
		{
		}

		// Helper: matches Ghidra pattern reading PTR_DAT_03446688 (UnityPlayer static) +0xb8 (currentActivity slot).
		// Mirrors the inline 8-line currentActivity-resolve idiom emitted by IL2CPP for every JNI call site
		// in this class. Centralized to avoid 38 copies of the same try/catch.
		// [§E10: Editor returns null; Android runtime returns the real Activity AndroidJavaObject.]
		private static AndroidJavaObject GetCurrentActivity()
		{
			try
			{
				using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
