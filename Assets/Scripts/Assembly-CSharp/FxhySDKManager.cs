// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL (IL metadata).
// 2026-05-12: Bodies ported 1-1 from work/06_ghidra/decompiled_full/FxhySDKManager/*.c per CLAUDE.md §D6.
// JNI bridge calls (Plugins.Scripts.FXHYSDKUnityClass) wired through C# wrapper.

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using LuaInterface;
using Plugins.Scripts;
using UnityEngine;

public class FxhySDKManager
{
	[NoToLua]
	public delegate void StringArgEvent(string[] args);

	[NoToLua]
	public delegate void NoArgEvent();

	public class FxhySDKCallbackParams
	{
		public int code;

		public string[] strings;

		// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnFxhySDKResult.c (System_Object___ctor only)
		// RVA: 0x15AC26C
		public FxhySDKCallbackParams()
		{
		}
	}

	public const string FXHYSDK_PLAYER_ID = "FXHYSDK_PLAYER_ID";

	public const string FXHYSDK_AGREE_STATUS = "FXHYSDK_AGREE_STATUS";

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowPersonalInfoGuideDialogResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKInitResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKCheckLoginResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKStartLoginResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKStartLogoutResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKStartUserCenterResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowCloseAccountResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowRealNameAuthenticationResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowRoundIdentityInfoResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowWebDialogResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowWebUIDialogResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKShowWebSystemResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKUploadGameUserInfoResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKPayResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKNotImplementEventResult;

	[NoToLua]
	public static StringArgEvent OnFxhySDKClose;

	private static bool _initMgrDone;

	private static bool _initSdkDone;

	private static bool _showingPersonalInfoGuide;

	private static bool _agreePersonalInfoGuide;

	private static string _playerId;

	private static bool _realNameAuthenticationDone;

	private static bool _isLogin;

	private static string _gameRegionName;

	private static Queue<FxhySDKCallbackParams> _resultCallbackParamQueue;

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/InitManager.c
	// RVA: 0x15A9B98
	[NoToLua]
	public static void InitManager()
	{
		if (_initMgrDone)
		{
			return;
		}
		FXHYSDKUnityClass instance = FXHYSDKUnityClass.GetInstance();
		FXHYSDKUnityClass.ResultCallBack cb = new FXHYSDKUnityClass.ResultCallBack(OnFxhySDKResult);
		if (instance == null)
		{
			return;
		}
		instance.setResultCallBack(cb);
		_agreePersonalInfoGuide = true;
		string[] regionArr = GetGameRegionNameFromSdk();
		if (regionArr == null || regionArr.Length == 0)
		{
			return;
		}
		_gameRegionName = regionArr[0];
		Debug.LogWarning("FxhySDK Game Region Name = " + _gameRegionName);
		_initMgrDone = true;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/SetPlayerId.c
	// RVA: 0x15A9DAC
	public static void SetPlayerId(string playerId)
	{
		_playerId = playerId;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetPlayerId.c
	// RVA: 0x15A9E0C
	public static string GetPlayerId()
	{
		return _playerId;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/HasInfoForLogin.c
	// RVA: 0x15A9E64
	public static bool HasInfoForLogin()
	{
		Debug.LogWarning("HasInfoForLogin = " + _playerId + ", " + _isLogin.ToString());
		if (_isLogin && !string.IsNullOrEmpty(_playerId))
		{
			return _realNameAuthenticationDone;
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetGameRegionName.c
	// RVA: 0x15A9FD4
	public static string GetGameRegionName()
	{
		return _gameRegionName;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetAgreePersonalInfoGuideStatus.c
	// RVA: 0x15AA02C
	public static bool GetAgreePersonalInfoGuideStatus()
	{
		return _agreePersonalInfoGuide;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetShowingPersonalInfoGuideStatus.c
	// RVA: 0x15AA084
	public static bool GetShowingPersonalInfoGuideStatus()
	{
		return _showingPersonalInfoGuide;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetInitSdkDone.c
	// RVA: 0x15AA0DC
	public static bool GetInitSdkDone()
	{
		return _initSdkDone;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/CheckCallback.c
	// RVA: 0x15AA134
	[NoToLua]
	public static void CheckCallback()
	{
		object lockObj = _resultCallbackParamQueue;
		lock (lockObj)
		{
			while (_resultCallbackParamQueue != null && _resultCallbackParamQueue.Count > 0)
			{
				FxhySDKCallbackParams p = _resultCallbackParamQueue.Dequeue();
				ProcessFxhySDKCallbacks(p.code, p.strings);
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowPersonalInfoGuideDialog.c (empty/no-op)
	// RVA: 0x15AA848
	public static void ShowPersonalInfoGuideDialog()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/InitSDK.c
	// RVA: 0x15AA84C
	public static void InitSDK()
	{
		Debug.LogWarning("Init FxhySDK");
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/IsLogin.c (returns false constant)
	// RVA: 0x15AA8B4
	public static bool IsLogin()
	{
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/CheckLogin.c (empty/no-op)
	// RVA: 0x15AA8BC
	public static void CheckLogin()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/StartLogin.c (empty/no-op)
	// RVA: 0x15AA8C0
	public static void StartLogin()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/StartLogout.c (empty/no-op)
	// RVA: 0x15AA8C4
	public static void StartLogout()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/StartUserCenter.c (empty/no-op)
	// RVA: 0x15AA8C8
	public static void StartUserCenter()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowCloseAccount.c (empty/no-op)
	// RVA: 0x15AA8CC
	public static void ShowCloseAccount()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowRealNameAuthentication.c (empty/no-op)
	// RVA: 0x15AA8D0
	public static void ShowRealNameAuthentication()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowRoundIdentityInfo.c (empty/no-op)
	// RVA: 0x15AA8D4
	public static void ShowRoundIdentityInfo()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowWebDialog.c (empty/no-op)
	// RVA: 0x15AA8D8
	public static void ShowWebDialog(string url)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowWebUIDialog.c (empty/no-op)
	// RVA: 0x15AA8DC
	public static void ShowWebUIDialog(string url)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowWebSystem.c (empty/no-op)
	// RVA: 0x15AA8E0
	public static void ShowWebSystem(string url)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/UploadGameUserInfo.c (empty/no-op)
	// RVA: 0x15AA8E4
	public static void UploadGameUserInfo(string infoType, string areaName, string serverId, string roleId)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/CreateOrderAndPay.c (empty/no-op)
	// RVA: 0x15AA8E8
	public static void CreateOrderAndPay(string orderId, string productId, string productName, string price, string area, string fromwhere, string server, string playerid, string notifyUrl, string sign)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetGameRegionNameFromSdk.c
	// RVA: 0x15A9D1C
	public static string[] GetGameRegionNameFromSdk()
	{
		string[] arr = new string[1];
		arr[0] = _gameRegionName;
		return arr;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/GetCheckForbiddenWordSign.c
	// RVA: 0x15AA8EC
	public static string GetCheckForbiddenWordSign(string param)
	{
		if (string.IsNullOrEmpty(param))
		{
			return _gameRegionName;
		}
		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
		Encoding utf8 = Encoding.UTF8;
		string concat = param + "KhG3NsfRbUhmUOjJUBVtuon5";
		if (utf8 == null)
		{
			return null;
		}
		byte[] bytes = utf8.GetBytes(concat);
		if (md5 == null)
		{
			return null;
		}
		byte[] hash = md5.ComputeHash(bytes);
		StringBuilder sb = new StringBuilder();
		if (hash == null)
		{
			return null;
		}
		for (int i = 0; i < hash.Length; i++)
		{
			byte b = hash[i];
			sb.AppendFormat("{0:x2}", b);
		}
		return sb.ToString();
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/SetTestEnvironment.c (empty/no-op)
	// RVA: 0x15AAABC
	public static void SetTestEnvironment(int code)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowPersonalInfoGuideDialogResult.c
	// RVA: 0x15AAAC0
	[NoToLua]
	private static void OnShowPersonalInfoGuideDialogResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowPersonalInfoGuideDialogResult", code, strings);
		bool agree = (code == 0x3ea || code == 0x7d2 || code == 0xbba);
		_agreePersonalInfoGuide = agree;
		_showingPersonalInfoGuide = false;
		if (agree)
		{
			ConfigMgr inst = ConfigMgr.Instance;
			if (inst == null)
			{
				return;
			}
			inst.SetConfigVarBool("FXHYSDK_AGREE_STATUS", true);
			InitSDK();
		}
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKShowPersonalInfoGuideDialogResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnInitSDKResult.c
	// RVA: 0x15AADD4
	[NoToLua]
	private static void OnInitSDKResult(int code, string[] strings)
	{
		ShowResultLogs("OnInitSDKResult", code, strings);
		bool ok = (code == 0x3fe || code == 0x7e6 || code == 0xbce);
		_initSdkDone = ok;
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKInitResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnCheckLoginResult.c
	// RVA: 0x15AAF6C
	[NoToLua]
	private static void OnCheckLoginResult(int code, string[] strings)
	{
		ShowResultLogs("OnCheckLoginResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		_isLogin = (code == 0x412 || code == 0x7fa || code == 0xbe2);
		StringArgEvent ev = OnFxhySDKCheckLoginResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnStartLoginResult.c
	// RVA: 0x15AB0F4
	[NoToLua]
	private static void OnStartLoginResult(int code, params string[] strings)
	{
		ShowResultLogs("OnStartLoginResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		string newPlayerId;
		bool login;
		if (code == 0x426 || code == 0xbf6 || code == 0x80e)
		{
			if (strings.Length == 0)
			{
				return;
			}
			newPlayerId = strings[0];
			login = true;
		}
		else
		{
			newPlayerId = _gameRegionName;
			login = false;
		}
		_playerId = newPlayerId;
		_isLogin = login;
		StringArgEvent ev = OnFxhySDKStartLoginResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnStartLogoutResult.c
	// RVA: 0x15AB2D4
	[NoToLua]
	private static void OnStartLogoutResult(int code, string[] strings)
	{
		ShowResultLogs("OnStartLogoutResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		_playerId = _gameRegionName;
		_realNameAuthenticationDone = false;
		_isLogin = false;
		StringArgEvent ev = OnFxhySDKStartLogoutResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnStartUserCenterResult.c
	// RVA: 0x15AB444
	[NoToLua]
	private static void OnStartUserCenterResult(int code, string[] strings)
	{
		ShowResultLogs("OnStartUserCenterResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKStartUserCenterResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowCloseAccountResult.c
	// RVA: 0x15AB580
	[NoToLua]
	private static void OnShowCloseAccountResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowCloseAccountResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKShowCloseAccountResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowRealNameAuthenticationResult.c
	// RVA: 0x15AB6BC
	[NoToLua]
	private static void OnShowRealNameAuthenticationResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowRealNameAuthenticationResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		bool ok = (code == 0x476 || code == 0x85e || code == 0xc46);
		_realNameAuthenticationDone = ok;
		StringArgEvent ev = OnFxhySDKShowRealNameAuthenticationResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowRoundIdentityInfoResult.c
	// RVA: 0x15AB830
	[NoToLua]
	private static void OnShowRoundIdentityInfoResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowRoundIdentityInfoResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKShowRoundIdentityInfoResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowWebDialogResult.c
	// RVA: 0x15AB96C
	[NoToLua]
	private static void OnShowWebDialogResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowWebDialogResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKShowWebDialogResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowWebUIDialogResult.c
	// RVA: 0x15ABAA8
	[NoToLua]
	private static void OnShowWebUIDialogResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowWebUIDialogResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKShowWebUIDialogResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnShowWebSystemResult.c
	// RVA: 0x15ABBE4
	[NoToLua]
	private static void OnShowWebSystemResult(int code, string[] strings)
	{
		ShowResultLogs("OnShowWebSystemResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKShowWebSystemResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnUploadGameUserInfoResult.c
	// RVA: 0x15ABD20
	[NoToLua]
	private static void OnUploadGameUserInfoResult(int code, string[] strings)
	{
		ShowResultLogs("OnUploadGameUserInfoResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKUploadGameUserInfoResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnPayResult.c
	// RVA: 0x15ABE5C
	[NoToLua]
	private static void OnPayResult(int code, string[] strings)
	{
		ShowResultLogs("OnPayResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKPayResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnNotImplementEventResult.c
	// RVA: 0x15ABF98
	[NoToLua]
	private static void OnNotImplementEventResult(int code, string[] strings)
	{
		ShowResultLogs("OnNotImplementEventResult", code, strings);
		if (strings == null)
		{
			return;
		}
		string[] arr = new string[strings.Length + 1];
		arr[0] = code.ToString();
		Array.Copy(strings, 0, arr, 1, strings.Length);
		StringArgEvent ev = OnFxhySDKNotImplementEventResult;
		if (ev != null)
		{
			ev(arr);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/OnFxhySDKResult.c
	// RVA: 0x15AC0D4
	private static void OnFxhySDKResult(int code, params string[] strings)
	{
		object lockObj = _resultCallbackParamQueue;
		lock (lockObj)
		{
			FxhySDKCallbackParams p = new FxhySDKCallbackParams();
			p.strings = strings;
			p.code = code;
			if (_resultCallbackParamQueue == null)
			{
				return;
			}
			_resultCallbackParamQueue.Enqueue(p);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ProcessFxhySDKCallbacks.c
	// RVA: 0x15AA310
	// Dispatcher: maps code → On*Result handler. Code ranges from Ghidra switch.
	// 0x3e9-0x3eb,0x7d1-0x7d3,0xbb9-0xbbb → OnShowPersonalInfoGuideDialogResult
	// 0x3fd-0x3ff,0x7e5-0x7e7,0xbcd-0xbce → OnInitSDKResult
	// 0x411-0x413,0x7f9-0x7fb,0xbe1-0xbe3 → OnCheckLoginResult
	// 0x425-0x427,0x80d-0x80f,0xbf5-0xbf6 → OnStartLoginResult
	// 0x439-0x43a,0x821-0x822,0xc09-0xc0a → OnStartLogoutResult
	// 0x44d-0x44f,0x835-0x837,0xc1d-0xc1e → OnStartUserCenterResult
	// 0x461-0x464,0x849-0x84a,0xc31,0x4b1,0x899-0x89a,0xc81,0x8a3-0x8a4,0xc8b,0x4bb,0x8ad-0x8ae,0xc95 → OnShowWebDialogResult/OnShowWebUIDialogResult/OnShowWebSystemResult/OnShowCloseAccountResult (sub-ranges)
	// 0x475-0x479,0x85d-0x85f,0xc45-0xc46 → OnShowRealNameAuthenticationResult
	// 0x489-0x48b,0x871-0x873,0xc59-0xc5a → OnShowRoundIdentityInfoResult
	// 0x515-0x518,0x8fd-0x8ff,0xce5-0xce6 → OnUploadGameUserInfoResult
	// 0x579-0x57b,0x961-0x963,0xd49-0xd4a → OnPayResult
	private static void ProcessFxhySDKCallbacks(int code, string[] strings)
	{
		if (code < 0x84b)
		{
			if (0x4b1 < code)
			{
				if (code < 0x7d4)
				{
					if (code < 0x4c6)
					{
						if (code == 0x4bb)
						{
							OnShowWebUIDialogResult(code, strings);
							return;
						}
						if (code == 0x4c5)
						{
							OnShowWebSystemResult(code, strings);
							return;
						}
					}
					else
					{
						if ((uint)(code - 0x515) < 4u)
						{
							OnUploadGameUserInfoResult(code, strings);
							return;
						}
						if ((uint)(code - 0x579) < 3u)
						{
							OnPayResult(code, strings);
							return;
						}
						if ((uint)(code - 0x7d1) < 3u)
						{
							OnShowPersonalInfoGuideDialogResult(code, strings);
							return;
						}
					}
					OnNotImplementEventResult(code, strings);
					return;
				}
				if (code < 0x810)
				{
					if ((uint)(code - 0x7e5) < 3u)
					{
						OnInitSDKResult(code, strings);
						return;
					}
					if ((uint)(code - 0x7f9) < 3u)
					{
						OnCheckLoginResult(code, strings);
						return;
					}
					if ((uint)(code - 0x80d) < 3u)
					{
						OnStartLoginResult(code, strings);
						return;
					}
					OnNotImplementEventResult(code, strings);
					return;
				}
				if ((uint)(code - 0x821) > 1u)
				{
					if ((uint)(code - 0x835) < 3u)
					{
						OnStartUserCenterResult(code, strings);
						return;
					}
					if ((uint)(code - 0x849) < 2u)
					{
						OnShowCloseAccountResult(code, strings);
						return;
					}
					OnNotImplementEventResult(code, strings);
					return;
				}
				OnStartLogoutResult(code, strings);
				return;
			}
			if (0x43a < code)
			{
				if (code < 0x465)
				{
					if ((uint)(code - 0x44d) < 3u)
					{
						OnStartUserCenterResult(code, strings);
						return;
					}
					if ((uint)(code - 0x461) < 4u)
					{
						OnShowCloseAccountResult(code, strings);
						return;
					}
				}
				else
				{
					if ((uint)(code - 0x475) < 5u)
					{
						OnShowRealNameAuthenticationResult(code, strings);
						return;
					}
					if ((uint)(code - 0x489) < 3u)
					{
						OnShowRoundIdentityInfoResult(code, strings);
						return;
					}
					if (code == 0x4b1)
					{
						OnShowWebDialogResult(code, strings);
						return;
					}
				}
				OnNotImplementEventResult(code, strings);
				return;
			}
			if (code < 0x400)
			{
				if ((uint)(code - 0x3e9) < 4u)
				{
					OnShowPersonalInfoGuideDialogResult(code, strings);
					return;
				}
				if ((uint)(code - 0x3fd) < 3u)
				{
					OnInitSDKResult(code, strings);
					return;
				}
				OnNotImplementEventResult(code, strings);
				return;
			}
			if ((uint)(code - 0x411) < 3u)
			{
				OnCheckLoginResult(code, strings);
				return;
			}
			if ((uint)(code - 0x425) < 3u)
			{
				OnStartLoginResult(code, strings);
				return;
			}
			if ((uint)(code - 0x439) < 2u)
			{
				OnStartLogoutResult(code, strings);
				return;
			}
			OnNotImplementEventResult(code, strings);
			return;
		}
		if (code < 0xbe4)
		{
			if (code < 0x8af)
			{
				if (code < 0x874)
				{
					if ((uint)(code - 0x85d) < 3u)
					{
						OnShowRealNameAuthenticationResult(code, strings);
						return;
					}
					if ((uint)(code - 0x871) < 3u)
					{
						OnShowRoundIdentityInfoResult(code, strings);
						return;
					}
				}
				else
				{
					if ((uint)(code - 0x899) < 2u)
					{
						OnShowWebDialogResult(code, strings);
						return;
					}
					if ((uint)(code - 0x8a3) < 2u)
					{
						OnShowWebUIDialogResult(code, strings);
						return;
					}
					if ((uint)(code - 0x8ad) < 2u)
					{
						OnShowWebSystemResult(code, strings);
						return;
					}
				}
				OnNotImplementEventResult(code, strings);
				return;
			}
			if (code < 0x964)
			{
				if ((uint)(code - 0x8fd) < 3u)
				{
					OnUploadGameUserInfoResult(code, strings);
					return;
				}
				if ((uint)(code - 0x961) < 3u)
				{
					OnPayResult(code, strings);
					return;
				}
				OnNotImplementEventResult(code, strings);
				return;
			}
			if ((uint)(code - 0xbb9) < 3u)
			{
				OnShowPersonalInfoGuideDialogResult(code, strings);
				return;
			}
			if ((uint)(code - 0xbcd) < 2u)
			{
				OnInitSDKResult(code, strings);
				return;
			}
			if ((uint)(code - 0xbe1) < 3u)
			{
				OnCheckLoginResult(code, strings);
				return;
			}
			OnNotImplementEventResult(code, strings);
			return;
		}
		if (0xc46 < code)
		{
			if (code < 0xc8c)
			{
				if ((uint)(code - 0xc59) < 2u)
				{
					OnShowRoundIdentityInfoResult(code, strings);
					return;
				}
				if (code == 0xc81)
				{
					OnShowWebDialogResult(code, strings);
					return;
				}
				if (code == 0xc8b)
				{
					OnShowWebUIDialogResult(code, strings);
					return;
				}
			}
			else
			{
				if (code == 0xc95)
				{
					OnShowWebSystemResult(code, strings);
					return;
				}
				if ((uint)(code - 0xce5) < 2u)
				{
					OnUploadGameUserInfoResult(code, strings);
					return;
				}
				if ((uint)(code - 0xd49) < 2u)
				{
					OnPayResult(code, strings);
					return;
				}
			}
			OnNotImplementEventResult(code, strings);
			return;
		}
		if (0xc0a < code)
		{
			if ((uint)(code - 0xc1d) < 2u)
			{
				OnStartUserCenterResult(code, strings);
				return;
			}
			if (code == 0xc31)
			{
				OnShowCloseAccountResult(code, strings);
				return;
			}
			if ((uint)(code - 0xc45) < 2u)
			{
				OnShowRealNameAuthenticationResult(code, strings);
				return;
			}
			OnNotImplementEventResult(code, strings);
			return;
		}
		if ((uint)(code - 0xbf5) < 2u)
		{
			OnStartLoginResult(code, strings);
			return;
		}
		if ((uint)(code - 0xc09) < 2u)
		{
			OnStartLogoutResult(code, strings);
			return;
		}
		OnNotImplementEventResult(code, strings);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/FxhySDKManager/ShowResultLogs.c
	// RVA: 0x15AAC94
	private static void ShowResultLogs(string callbackName, int code, params string[] strings)
	{
		string log = callbackName + " code = " + code.ToString() + " strings = ";
		if (strings == null)
		{
			return;
		}
		for (int i = 0; i < strings.Length; i++)
		{
			string s = strings[i];
			if (i == 0)
			{
				log = log + s + ", ";
			}
			else
			{
				log = log + s;
			}
		}
		Debug.LogWarning(log);
	}

	// Source: Ghidra (no .ctor body — Cpp2IL default object .ctor)
	// RVA: 0x15AC274
	public FxhySDKManager()
	{
	}

	// Source: Ghidra (no .cctor body — static field inits default)
	// RVA: 0x15AC27C
	// _resultCallbackParamQueue allocated for thread-safe queueing per CheckCallback/OnFxhySDKResult.
	static FxhySDKManager()
	{
		_resultCallbackParamQueue = new Queue<FxhySDKCallbackParams>();
	}
}
