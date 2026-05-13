using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class IOSPlatform : BasePlatform
	{
		public delegate void dEventProcess();

		public delegate void dEventProcessWithArgs(string[] args);

		public delegate void doProccess(string[] args);

		public delegate void doSkuProcess(string status, List<UJBillingProduct> ujSkuDetails);

		private bool isSupplied;

		private string initMsg;

		private Dictionary<string, double> achievementDataList;

		public const string IOS_PLATFORM_INIT_FINISHED_MSG = "1";

		public const string IOS_PLATFORM_PURCHASE_FINISHED_MSG = "2";

		public const string IOS_PLATFORM_CANCEL_PURCHASE_MSG = "3";

		public const string IOS_PLATFORM_PURCHASE_FAIL_MSG = "4";

		public const string IOS_PLATFORM_VERIFY_FAIL_MSG = "9";

		public const string IOS_PLATFORM_SEND_UJ_ORDER_LIST = "10";

		public const string IOS_PLATFORM_GAME_CENTER_AUTHENTICATE_RESULT_MSG = "11";

		public const string IOS_PLATFORM_GAMECENTER_LOGIN_SUCCESS_MSG = "12";

		public const string IOS_PLATFORM_GAMECENTER_LOGIN_FAIL_MSG = "13";

		public const string IOS_PLATFORM_LOAD_ACHIEVEMENTS_MSG = "14";

		public const string IOS_PLATFORM_QUERY_INVENTORY_MSG = "15";

		public const string IOS_PLATFORM_REQUEST_ATTRACKING_AUTHORIZED_MSG = "16";

		public const string IOS_PLATFORM_REQUEST_ATTRACKING_DENIED_MSG = "17";

		public const string IOS_PLATFORM_CREATE_HANDLE_LOST_GOLD_EVENT = "18";

		public const string IOS_PLATFORM_PURCHASE_UPDATED_MSG = "19";

		public const string IOS_PLATFORM_REQUEST_GOLD = "20";

		private Dictionary<string, UJBillingProduct> _ujProductMap;

		private static IOSPlatform mInstance;

		private static AndroidJavaClass mJc;

		public static dEventProcess doEventInitSucceeded;

		public static dEventProcess doEventInitFailed;

		public static dEventProcess doEventPurchaseSucceeded;

		public static dEventProcess doEventUserCanceled;

		public static dEventProcessWithArgs doEventPurchaseFailed;

		public static dEventProcessWithArgs doEventUJOrderListCount;

		public static dEventProcess doEventRequestGold;

		public static dEventProcessWithArgs doEventVerifyFail;

		public static dEventProcess doEventGameCenterAuthenticateSucceeded;

		public static dEventProcess doEventGameCenterAuthenticateFailed;

		public static dEventProcess doEventLoadAchievementsDone;

		public static dEventProcessWithArgs doEventRequestATTrackingAuthorized;

		public static dEventProcessWithArgs doEventRequestATTrackingDenied;

		public static dEventProcessWithArgs doEventHandleLostGold;

		public static dEventProcessWithArgs doEventPurchaseUpdated;

		public static doProccess doEventQueryInventory_SGC;

		public static doSkuProcess doEventQueryInventory;

		public IOSPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static IOSPlatform Instance()
		{ return default; }

		public static AndroidJavaClass GetGameCenterClass()
		{ return default; }

		public bool IsAvailableStoreKitV2()
		{ return default; }

		public bool CanMakePayment()
		{ return default; }

		public void StoreKitPluginInit()
		{ }

		public void StoreKitPluginInit(string serverID, string characterID, string characterName)
		{ }

		public void StoreKitPluginInitWithCountryCode(string serverID, string characterID, string characterName, string countryCode)
		{ }

		public void QueryProducts()
		{ }

		public void DoPurchase(string designID, string extraInformation = "", bool enableDelay = true)
		{ }

		public void RequestUJOrderList()
		{ }

		[Obsolete("MarsSDK deprecated this API, please use IOSPlatform.Instance().DoPurchase(string designID, string extraInformation) instead", true)]
		public void DoPurchase(string designID, string serverID, string characterID, string characterName, string extraInformation = "")
		{ }

		[Obsolete("MarsSDK deprecated this API, please use IOSPlatform.Instance().RequestUJOrderList() instead", true)]
		public void RequestUJOrderList(string serverID, string characterID, string characterName)
		{ }

		[Obsolete("MarsSDK deprecated this API, please use IOSPlatform.Instance().DoPurchase(designID, serverID, characterID, characterName) instead", true)]
		public void DoPurchaseMultiChar(string designID, string serverID, string characterID)
		{ }

		[Obsolete("MarsSDK deprecated this API, please use IOSPlatform.Instance().RequestUJOrderList(serverID, characterID, characterName) instead", true)]
		public void RequestUJOrderListMultiChar(string serverID, string characterID)
		{ }

		public UJBillingProduct GetProduct(string productId)
		{ return default; }

		public string GetProductPrice(string productID)
		{ return default; }

		public string GetProductFormatPrice(string productID)
		{ return default; }

		public string GetProductCurrency(string productID)
		{ return default; }

		public void RefreshReceipt()
		{ }

		public void GameCenterAuthenticate()
		{ }

		public void LoadAchievements()
		{ }

		public void SetAchievement(string achievementID, double percentComplete, bool showCompletionBanner)
		{ }

		public void ShowGameCenter()
		{ }

		public void ResetAllAchievement()
		{ }

		[Obsolete("已棄用，請轉移使用 NotificationManager，Apple 流程無太大變動。", true)]
		[NoToLua]
		public void Notify(string title, string msg, long time)
		{ }

		[Obsolete("已棄用，請轉移使用 NotificationManager，Apple 流程無太大變動。", true)]
		[NoToLua]
		public void CancelNotify(string title)
		{ }

		[Obsolete("已棄用，請轉移使用 NotificationManager，Apple 流程無太大變動。", true)]
		[NoToLua]
		public void CancelAllNotify()
		{ }

		public static string GetGameCenterDisplayName()
		{ return default; }

		public static string GetGameCenterAccountUID()
		{ return default; }

		public static string GetBindGameCenterUID()
		{ return default; }

		public static bool IsBindGameCenter()
		{ return default; }

		public Dictionary<string, double> GetAchievementDataList()
		{ return default; }

		private void MsgProcessInitFinished(string[] args)
		{ }

		private void MsgProcessPurchaseFinished(string[] args)
		{ }

		private void MsgProcessCancelPurchase(string[] args)
		{ }

		private void MsgProcessPurchaseFail(string[] args)
		{ }

		private void MsgProcessVerifyFail(string[] args)
		{ }

		private void MsgProcessUJOrderListCount(string[] args)
		{ }

		private void MsgProcessLoadAchievements(string[] args)
		{ }

		private void MsgGaneCenterLoginSuccess(string[] args)
		{ }

		private void MsgGaneCenterLoginFail(string[] args)
		{ }

		private void MsgProcessQueryInventory(string[] args)
		{ }

		private void MsgProcessRequestATTrackingAuthorized(string[] args)
		{ }

		private void MsgProcessRequestATTrackingDenied(string[] args)
		{ }

		private void MsgProcessCreateHandleLostGoldEvent(string[] args)
		{ }

		private void MsgProcessPurchaseUpdated(string[] args)
		{ }

		private void MsgProcessRequestGold(string[] args)
		{ }
	}
}
