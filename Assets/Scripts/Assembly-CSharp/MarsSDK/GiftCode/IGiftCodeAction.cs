namespace MarsSDK.GiftCode
{
	internal interface IGiftCodeAction
	{
		int RedeemGiftCode(string giftCode);

		int QueryGifts();

		int OpenGiftCodePanel(string serverId, string characterId, string characterName);

		int OpenGiftCodePanel();
	}
}
