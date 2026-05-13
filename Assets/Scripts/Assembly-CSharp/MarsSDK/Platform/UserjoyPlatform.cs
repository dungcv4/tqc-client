using System;
using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class UserjoyPlatform : BasePlatform
	{
		public enum eUJWebRedirectNo
		{
			Home = 0,
			News = 2,
			WebBilling = 3,
			CustomService = 5,
			FAQ = 8
		}

		[NoToLua]
		public delegate void dEventProcess();

		public delegate void doProccess(string[] args);

		public delegate void doProccessWithStatus(int status, string[] args);

		public const string UJ_PLATFORM_REDIRECT_NEWS = "2";

		public const string UJ_PLATFORM_REDIRECT_BILLING = "3";

		public const string UJ_PLATFORM_REDIRECT_CUSTOMERSERVICE = "5";

		public const string UJ_PLATFORM_REDIRECT_FAQ = "8";

		public const int OptionAutoOpenURL = 0;

		public const int OptionPassURL = 1;

		public const int STATUS_USERJOY_PLATFORM_SUCCESS = 0;

		public const int STATUS_USERJOY_PLATFORM_MISS_SOME_CHARACTER_DATA = 1;

		public const int STATUS_USERJOY_PLATFORM_SEND_REQUEST_ERROR = 2;

		private static UserjoyPlatform mInstance;

		public const string USERJOY_PLATFORM_SEND_UJ_ORDER_LIST = "1";

		public const string CB_EVENT_INIT_UJ_WEB_BILLING = "2";

		public const int ACTION_INIT = 1;

		public const int ACTION_GETTOKEN = 2;

		private bool _isInit;

		public const int PROTOCOL_REQUEST_UJ_ORDER_LIST = 5;

		public static doProccess doUJEventUJOrderListCount;

		[NoToLua]
		public static dEventProcess doUJEventRequestGold;

		public static doProccessWithStatus doInitBilling;

		private static AndroidJavaObject mJo;

		private static AndroidJavaClass mJc;

		public bool isInit
		{
			get
			{ return default; }
			private set
			{ }
		}

		public static UserjoyPlatform Instance()
		{ return default; }

		public UserjoyPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		private void MsgProcessUJOrderListCount(string[] args)
		{ }

		private void MsgProcessInitBilling(string[] args)
		{ }

		private void reset()
		{ }

		public void RequestNewsURL(string nickname, string setid, string characterid = "", string lan = "")
		{ }

		public void RequestCustomerServiceURL(string nickname, string setid, string characterid = "", string lan = "")
		{ }

		public void RequestFaqURL(string nickname, string setid, string characterid = "", string lan = "")
		{ }

		public int RequestUJOrderList()
		{ return default; }

		public int RequestUJOrderList(string serverID, string characterID, string characterName)
		{ return default; }

		[Obsolete("MarsSDK deprecated this API, please use UserjoyPlatform.Instance().RequestUJOrderList(serverID, characterID, characterName) instead", true)]
		public void RequestUJOrderListMultiChar(string serverID, string characterID)
		{ }

		public static AndroidJavaClass GetMarsClass()
		{ return default; }

		private static AndroidJavaObject getAndroidInstance()
		{ return default; }

		public static void OpenMainFrame()
		{ }

		public static void OpenWebFrame()
		{ }

		public static void OpenFirstLoginPanel()
		{ }

		public static void OpenLoginUJAccountPanel()
		{ }

		public static void OpenDevLoginPanel()
		{ }

		public static void OpenCreateUJAccountPanel()
		{ }

		public static void OpenModifyUJPasswordPanel()
		{ }

		public static void OpenForgotWebview()
		{ }

		public static void OpenGetBackPanel()
		{ }

		public static void CloseAllPanell()
		{ }

		public int InitUJWebBilling(string serverID, string characterID, string characterName)
		{ return default; }

		public static void StartWebBilling(string nickname, string setid, string lan = "", string ctype = "")
		{ }

		public void StartUJWeb(string nickname, string setid, string redirectno, string lan = "", string ctype = "", string characterid = "")
		{ }

		public void StartUJWebExtend(string nickname, string setid, string redirectno, string lan = "", string ctype = "", string characterid = "", int option = 0)
		{ }

		public void DoPurchase(string serverID, string characterID, string characterName, string itemID, string itemType)
		{ }

		public void StartUJWebWithCountryCode(string nickname, string setid, string redirectno, string lan = "", string ctype = "", string characterid = "", string countryCode = "")
		{ }

		public void DoPurchaseWithCountryCode(string serverID, string characterID, string characterName, string itemID, string itemType, string countryCode)
		{ }

		public void DoPurchaseWithPaymentChannel(string serverID, string characterID, string characterName, string itemID, string itemType, int option = 0, string countryCode = "", string paymentChannel = "GooglePlay")
		{ }
	}
}
