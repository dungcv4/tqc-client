using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using MarsSDK.LitJson;

namespace MarsSDK.GiftCode
{
	public class MarsGiftCode : MarsBrigeSingleton<MarsGiftCode>, IGiftCodeAction
	{
		// Source: dump.cs EOperationAgent.GiftCode  // matches class name
		private const int REEDEM_COOL_DOWN = 5000;

		public const int GiftCodeAction_RedeemGiftCode = 1;

		public const int GiftCodeAction_CheckGift = 2;

		public const string CB_MSG_GIFT_CODE_REDEEM_RESULT = "1";

		public const string CB_MSG_QUERY_GIFTS_RESULT = "2";

		public const string CB_MSG_HAS_NEW_GIFT = "3";

		public static dEventProcessWithStatus doEventRedeemCodeResult;

		public static dEventProcessWithStatus doEventQueryGiftsResult;

		public static dEventProcessWithArgs doEventNewGiftIsComing;

		private IGiftCodeAction _giftCodeAction;

		public int RedeemCoolDownMs
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			private set
			{ }
		}

		public MarsGiftCode() : base(EOperationAgent.GiftCode)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal void UpdateConfig(JsonData jd)
		{ }

		public int RedeemGiftCode(string giftCode)
		{ return default; }

		public int QueryGifts()
		{ return default; }

		public int OpenGiftCodePanel(string serverId, string characterId, string characterName)
		{ return default; }

		public int OpenGiftCodePanel()
		{ return default; }

		private void MsgProcessRedeemResult(string[] args)
		{ }

		private void MsgProcessQueryGiftsResult(string[] args)
		{ }

		private void MsgProcessHasNewGift(string[] args)
		{ }
	}
}
