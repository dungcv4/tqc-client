using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class GooglePlatform : BasePlatform
	{
		public delegate void doProccess(string[] args);

		public delegate void dEventProcess();

		public delegate void doSkuProcess(string status, List<UJBillingProduct> ujSkuDetails);

		public delegate void dEventProcessWithOneArg(string status, string arg);

		private static GooglePlatform mInstance;

		private bool isSupplied;

		private static AndroidJavaClass mJc;

		private static AndroidJavaObject mJo;

		public const string GOOGLE_PLATFORM_INIT_FINISHED_MSG = "1";

		public const string GOOGLE_PLATFORM_PURCHASE_FINISHED_MSG = "2";

		public const string GOOGLE_PLATFORM_CANCEL_PURCHASE_MSG = "3";

		public const string GOOGLE_PLATFORM_PURCHASE_FAIL_MSG = "4";

		public const string GOOGLE_PLATFORM_GOOGLE_PLAY_SERVICE_NOT_SUPPORTED_MSG = "5";

		public const string GOOGLE_PLATFORM_MISS_GOOGLE_ACCOUNT_MSG = "6";

		public const string GOOGLE_PLATFORM_GOOGLE_RSA_KEY_NOT_MATCH = "7";

		public const string GOOGLE_PLATFORM_VERIFY_FAIL_MSG = "9";

		public const string GOOGLE_PLATFORM_SEND_UJ_ORDER_LIST = "10";

		public const string GOOGLE_PLATFORM_GOOGLE_PLAY_API_INIT_RESULT_MSG = "11";

		public const string GOOGLE_PLATFORM_QUERY_INVENTORY_MSG = "16";

		public const string GOOGLE_PLATFORM_GPP_SEND_ORDER_INFO = "17";

		public const string GOOGLE_PLATFORM_CREATE_HANDLE_LOST_GOLD_EVENT = "18";

		public const string GOOGLE_PLATFORM_PURCHASE_UPDATED_MSG = "19";

		public const string GOOGLE_PLATFORM_PURCHASE_PENDING_MSG = "20";

		public const string GOOGLE_PLATFORM_PLAY_STORE_NEED_UPGRATE_MSG = "21";

		public const string GOOGLE_PLATFORM_REQUEST_GOLD = "22";

		public const string GOOGLE_PLATFORM_REQUEST_PLAYSTORE_BILLING_CONFIG = "23";

		public static dEventProcess doEventInitSucceeded;

		public static dEventProcess doEventInitFailed;

		public static dEventProcess doEventPurchaseSucceeded;

		public static dEventProcess doEventUserCanceled;

		public static doProccess doEventPurchaseFailed;

		public static doProccess doEventUJOrderListCount;

		public static dEventProcess doEventRequestGold;

		public static dEventProcess doEventDeviceNotSupportGooglePlayService;

		public static dEventProcess doEventMissGoogleAccount;

		public static dEventProcess doEventRSAKeyNotMatch;

		public static doProccess doEventVerifyFail;

		public static dEventProcess doEventGooglePlayApiConnected;

		public static dEventProcess doEventGooglePlayApiConnectionSuspended;

		public static doProccess doEventGotGPPOrderInfo;

		public static doProccess doEventHandleLostGold;

		public static doProccess doEventPurchaseUpdated;

		public static doProccess doEventPurchasePending;

		public static doProccess doEventPlayStoreNeedUpgrate;

		public static doProccess doEventQueryInventory_SGC;

		public static doSkuProcess doEventQueryInventory;

		[NoToLua]
		public static dEventProcessWithOneArg doEventRequestBillingConfig;

		private Dictionary<string, UJBillingProduct> _ujProductMap;

		public const string GOOGLE_LOGIN_SUCCESS_MSG = "12";

		public const string GOOGLE_LOGIN_FAIL_MSG = "13";

		public const string GOOGLE_LOGIN_CANCEL_MSG = "14";

		public const string GOOGLE_REVOKE_ACCESS_MSG = "15";

		public static doProccess doEventGooglePlayLoginSuccess;

		public static doProccess doEventGooglePlayLoginFail;

		public static doProccess doEventGooglePlayLoginCancel;

		public static doProccess doEventGooglePlayRevokeAccess;

		public static GooglePlatform Instance()
		{ return default; }

		public GooglePlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public bool IsSupplied()
		{ return default; }

		[NoToLua]
		public static AndroidJavaClass GetGoogleClass()
		{ return default; }

		[NoToLua]
		public static AndroidJavaObject GetGoogleInstance()
		{ return default; }

		[NoToLua]
		public void InitGooglePlayApi()
		{ }

		public void ShowArchievemet()
		{ }

		public void UnlockAchievement(string achievementId, bool showsCompletionNotification)
		{ }

		public void IncrementAchievements(string achievementId, int steps)
		{ }

		[NoToLua]
		[Obsolete("已棄用，請轉移使用 NotificationManager，並重新設計流程，相關流程請參照 Demo。", true)]
		public void Notify(string iconName, int notifyID, string title, string msg, long time)
		{ }

		[NoToLua]
		[Obsolete("已棄用，請轉移使用 NotificationManager，並重新設計流程，相關流程請參照 Demo。", true)]
		public void NotifyWithImage(string iconName, int notifyID, string title, string msg, long time, byte[] imageData)
		{ }

		[Obsolete("已棄用，請轉移使用 NotificationManager，並重新設計流程，相關流程請參照 Demo。", true)]
		[NoToLua]
		public void NotifyWithURL(string iconName, int notifyID, string title, string msg, long time, string url)
		{ }

		[NoToLua]
		[Obsolete("已棄用，請轉移使用 NotificationManager，並重新設計流程，相關流程請參照 Demo。", true)]
		public void CancelNotify(int notifyID)
		{ }

		[Obsolete("已棄用，請轉移使用 NotificationManager，並重新設計流程，相關流程請參照 Demo。", true)]
		[NoToLua]
		public void CancelAllNotify()
		{ }

		public int InitGoogleIAB(string publicKey, string serverID, string characterID, string characterName)
		{ return default; }

		public int InitGoogleIABWithCountryCode(string publicKey, string serverID, string characterID, string characterName, string countryCode)
		{ return default; }

		public int DoPurchase(string designID, string extraInformation = "", bool enableDelay = true)
		{ return default; }

		public int RequestUJOrderList()
		{ return default; }

		public void QueryPurchases()
		{ }

		public void QueryInventoryInApp()
		{ }

		public void QueryProducts(UJBillingProduct.eProductType productType)
		{ }

		public UJBillingProduct GetProduct(string google_productId)
		{ return default; }

		[NoToLua]
		public int GetPlayStoreBillingConfigAsync()
		{ return default; }

		public long GetSkuPriceAmountMicros(string sku)
		{ return default; }

		public string GetSkuPrice(string sku)
		{ return default; }

		public string GetSkuCurrency(string sku)
		{ return default; }

		public string GetSkuFormatPrice(string sku)
		{ return default; }

		public void ConsumeAllForTest()
		{ }

		private string arrayOptValue(string[] sourceArray, int index, string defaultValue = "")
		{ return default; }

		[Obsolete("MarsSDK deprecated this API, please use GooglePlatform.Instance().InitGoogleIAB(publicKey, serverID, characterID, characterName ) instead", true)]
		public void InitGoogleIAB(string publicKey)
		{ }

		[Obsolete("MarsSDK deprecated this API, please use GooglePlatform.Instance().DoPurchase(designID, extraInformation) instead", true)]
		public void DoPurchase(string designID, string serverID, string characterID, string characterName, string extraInformation = "")
		{ }

		[Obsolete("MarsSDK deprecated this API, please use GooglePlatform.Instance().DoPurchase(designID, serverID, characterID, characterName) instead", true)]
		public void DoPurchaseMultiChar(string designID, string serverID, string characterID)
		{ }

		[Obsolete("MarsSDK deprecated this API, please use GooglePlatform.Instance().RequestUJOrderList() instead", true)]
		public void RequestUJOrderList(string serverID, string characterID, string characterName)
		{ }

		[Obsolete("MarsSDK deprecated this API, please use GooglePlatform.Instance().RequestUJOrderList(serverID, characterID, characterName) instead", true)]
		public void RequestUJOrderListMultiChar(string serverID, string characterID)
		{ }

		[Obsolete("MarsSDK deprecated this API, please use GooglePlatform.Instance().queryInventoryInApp() instead", true)]
		public void RecheckProductList()
		{ }

		public void GPPClaimOrder(string[] orderIDList)
		{ }

		public void GPPClaimAllOrder()
		{ }

		private void MsgProcessInitFinished(string[] args)
		{ }

		private void MsgProcessPurchaseFinished(string[] args)
		{ }

		private void MsgProcessCancelPurchase(string[] args)
		{ }

		private void MsgProcessPurchaseFail(string[] args)
		{ }

		private void MsgProcessUJOrderListCount(string[] args)
		{ }

		private void MsgProcessDeviceNotSupportGooglePlayService(string[] args)
		{ }

		private void MsgProcessMissGoogleAccount(string[] args)
		{ }

		private void MsgProcessRSAKeyNotMatch(string[] args)
		{ }

		private void MsgProcessVerifyFail(string[] args)
		{ }

		private void MsgProcessGooglePlayApiInitResult(string[] args)
		{ }

		private void MsgProcessQueryInventory(string[] args)
		{ }

		private void MsgProcessSendGPPOrderInfo(string[] args)
		{ }

		private void MsgProcessCreateHandleLostGoldEvent(string[] args)
		{ }

		private void MsgProcessPurchaseUpdated(string[] args)
		{ }

		private void MsgProcessPurchasePending(string[] args)
		{ }

		private void MsgProcessPlayStoreNeedUpgrate(string[] args)
		{ }

		private void MsgProcessRequestGold(string[] args)
		{ }

		private void MsgProcessRequestBillingConfig(string[] args)
		{ }

		public bool IsBindGoogle()
		{ return default; }

		public string GetGoogleAccountDisplayName()
		{ return default; }

		public string GetGoogleAccountEmail()
		{ return default; }

		public string GetGoogleAccountUID()
		{ return default; }

		public string GetGoogleAccountType()
		{ return default; }

		public string GetGoogleAccountPhotoUri()
		{ return default; }

		public string GetBindGoogleUID()
		{ return default; }

		public void SignOut()
		{ }

		public void LoginByGoogle()
		{ }

		public void BindByGoogle()
		{ }

		public void UnBindByGoogle()
		{ }

		public void RevokeAccess()
		{ }

		public bool IOSGoogleSignInActivity()
		{ return default; }

		private void MsgProcessLoginSuccess(string[] args)
		{ }

		private void MsgProcessLoginFail(string[] args)
		{ }

		private void MsgProcessLoginCancel(string[] args)
		{ }

		private void MsgProcessRevokeAccess(string[] args)
		{ }
	}
}
