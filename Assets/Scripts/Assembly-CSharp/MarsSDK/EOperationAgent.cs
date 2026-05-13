using System;

namespace MarsSDK
{
	public enum EOperationAgent
	{
		None = -1,
		PermissionMaganger = 0,
		Mars = 1,
		Google = 2,
		Userjoy = 3,
		Facebook = 4,
		IOS = 5,
		Twitter = 6,
		TelephoneVerify = 7,
		Instagram = 8,
		MobileMail = 9,
		TelephoneLogin = 11,
		[Obsolete("EOperationAgent.Extended is deprecated.", true)]
		Extended = 12,
		Apple = 13,
		MailVerify = 14,
		SelectPhoto = 15,
		MoJoy = 16,
		Notification = 17,
		NetworkState = 18,
		GiftCode = 20,
		PlayerCharactor = 21,
		LogReport = 22,
		ExtendedPlatform = 100,
		ExtendedPlatform_OneStore = 101,
		ExtendedPlatform_DMM = 102,
		ExtendedPlatform_DMM_AppStore = 103,
		ExtendedPlatform_PGS = 104,
		ExtendedPlatform_Erolabs = 105,
		ExtendedPlugin = 200,
		ExtendedPlugin_AdMob = 201
	}
}
