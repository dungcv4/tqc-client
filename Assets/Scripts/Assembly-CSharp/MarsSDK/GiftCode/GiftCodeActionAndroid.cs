using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.GiftCode
{
	internal class GiftCodeActionAndroid : IGiftCodeAction
	{
		private static AndroidJavaClass mJc;

		private static AndroidJavaObject mJo;

		private static AndroidJavaObject getGiftCodeInstance()
		{ return default; }

		public int QueryGifts()
		{ return default; }

		public int OpenGiftCodePanel(string serverId, string characterId, string characterName)
		{ return default; }

		public int OpenGiftCodePanel()
		{ return default; }

		public int RedeemGiftCode(string giftCode)
		{ return default; }

		public GiftCodeActionAndroid()
		{ }
	}
}
