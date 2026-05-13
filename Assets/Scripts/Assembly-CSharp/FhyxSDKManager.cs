using System;
using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;

public class FhyxSDKManager
{
	[Serializable]
	public class UserBoundInfoData
	{
		public int code;

		public int status;

		public int age;

		public string id;

		public string tel;

		public string message;

		public UserBoundInfoData()
		{ }
	}

	[Serializable]
	public class UserInfoData
	{
		public int code;

		public string message;

		public int isBind;

		public string uid;

		public string token;

		public UserInfoData()
		{ }
	}

	[Serializable]
	public class ActivationData
	{
		public int code;

		public string message;

		public string activationCode;

		public string activationAuthCode;

		public ActivationData()
		{ }
	}

	[Serializable]
	public class LogData
	{
		public int code;

		public string message;

		public string successCode;

		public string authCode;

		public LogData()
		{ }
	}

	[Serializable]
	public class CommonResponseData
	{
		public int code;

		public string message;

		public CommonResponseData()
		{ }
	}

	public const string ANDROID_PLUGIN_NAME = "com.userjoy.sgc.fhyxbridge.Bridge";

	public const string FHYXSDK_PLAYER_ID = "FHYXSDK_PLAYER_ID";

	public const string LISTENER_OBJ_NAME = "FhyxSDKListener";

	private static AndroidJavaClass _pluginClass;

	private static AndroidJavaObject _pluginInstance;

	private static string _sdkVersion;

	private static string _channel;

	private static string _playerID;

	private static UserInfoData _userInfo;

	private static UserBoundInfoData _userBoundInfo;

	public static bool initDone;

	public static void Init(int appLang = 0)
	{ }

	public static string GetSdkVersion()
	{ return default; }

	public static string GetChannel()
	{ return default; }

	public static void SetPlayerID(string playerID)
	{ }

	public static string GetPlayerID()
	{ return default; }

	[NoToLua]
	public static void SetUserInfo(UserInfoData userInfo)
	{ }

	[NoToLua]
	public static void SetUserBoundInfo(UserBoundInfoData userBoundInfo)
	{ }

	public static int GetBoundStatus()
	{ return default; }

	public static bool HasInfoForLogin()
	{ return default; }

	public static void LoginByAccount(string account, string passwd)
	{ }

	public static void GetLoginVerificationCode(string phoneNumber)
	{ }

	public static void LoginByVerificationCode(string phoneNumber, string verifyCode)
	{ }

	public static void LoginByThirdParty(int type)
	{ }

	public static void Logout()
	{ }

	public static bool IsReady()
	{ return default; }

	public static bool IsLogin()
	{ return default; }

	public static void GetUserBoundInfo()
	{ }

	public static void SendRegisterVerificationCode(string phoneNumber)
	{ }

	public static void Register(string phoneNumber, string verifyCode, string passwd, string token = "")
	{ }

	public static void BindPhone(string phoneNumber, string verifyCode, string token = "")
	{ }

	public static void BindingPersonalIdentity(string realName, string idNumber)
	{ }

	public static void GetUserInfo()
	{ }

	public static void ChangePassword(string oldPassword, string newPassword)
	{ }

	public static void ResetPasswordSendVerificationCode(string phoneNumber)
	{ }

	public static void ResetPasswordCheckVerificationCode(string phoneNumber, string verifyCode)
	{ }

	public static void ResetPassword(string newPassword)
	{ }

	public static void RebindPhoneSendVerificationCodeToOld(string phoneNumber)
	{ }

	public static void RebindPhoneCheckVerificationCode(string phoneNumber, string verifyCode)
	{ }

	public static void RebindPhoneSendVerificationCodeToNew(string phoneNumber)
	{ }

	public static void RebindPhone(string phoneNumber, string verifyCode)
	{ }

	public static void OpenWebView(string url, string title, bool showPopupOption = false)
	{ }

	public static void HideWebView()
	{ }

	public static void BindActivationCode(string activationCode)
	{ }

	public static void CheckAccountActivation()
	{ }

	public static void CreateOrderAndPay(int payWay, string orderID, int productID, string productName, string amount, string count, string playerID, string notifyURL, string areaID, string fromWhere, string serverID, string sign)
	{ }

	public static void SendLog(string gid, string areaID, string serverID, int logType)
	{ }

	public static void CheckWord(string inputText)
	{ }

	public FhyxSDKManager()
	{ }

	static FhyxSDKManager()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
