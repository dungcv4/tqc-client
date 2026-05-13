using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using MarsSDK.Platform;
using UnityEngine;

namespace MarsSDK.ThirdParty.DMM.GameStore
{
	public class DmmAppStorePlatform : BasePlatformSingleton<DmmAppStorePlatform>
	{
		private string _designId;

		public const string DMM_APP_STORE_PLATFORM_INIT_MSG = "1";

		public const string DMM_APP_STORE_PLATFORM_CREATE_ORDER_MSG = "2";

		public const string DMM_APP_STORE_PLATFORM_GET_UJ_ORDER_LIST = "3";

		public const string DMM_MOBILE_PAYMENT_CALLBACK_URL = "dmm_mobile_payment_callback_url";

		public const int STATUS_CREATE_HANDLE_LOST_GOLD_EVENT = 67041;

		public static MessageProcessDelegate doEventInitResult;

		public static MessageProcessDelegate doEvenCreateOrder;

		public static MessageProcessDelegate doEventRequestGold;

		public static MessageProcessDelegate doEventHandleLostGold;

		private static AndroidJavaObject mJo;

		public string paymentCallbackURL
		{
			get
			{ return default; }
		}

		private string DMM_AppId
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			set
			{ }
		}

		private string ServerId
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			set
			{ }
		}

		private string CharacterId
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			set
			{ }
		}

		private string CharacterName
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			set
			{ }
		}

		public static AndroidJavaObject GetDMMAppStoreMobileClass()
		{ return default; }

		public DmmAppStorePlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public int InitDmmGameStore(string dmmAppId, bool isAdult = false)
		{ return default; }

		public int InitDmmBilling(string serverID, string characterID, string characterName)
		{ return default; }

		public int RequestLogin(string user_id)
		{ return default; }

		public int CreateUJOrder(string dmm_paymentId, string designId, int itemQuantity, int itemUnitPrice, string extraInformation)
		{ return default; }

		public int RequestUJOrderList()
		{ return default; }

		private void MsgProcessInitResult(string[] args)
		{ }

		private void MsgProcessCreateOrder(string[] args)
		{ }

		private void MsgProcessUJOrderListCount(string[] args)
		{ }
	}
}
