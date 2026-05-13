using System;
using Cpp2IlInjected;

namespace MarsSDK.Platform
{
	[Obsolete("Use [MarsErrorCode] instead. ", true)]
	public class UJBillingStatus
	{
		public const int STATUS_SUCCESS = 0;

		public const int STATUS_UNKNOWN_ACTION = 67001;

		public const int STATUS_GET_PARAMS_FOR_CREATE_ORDER_FAIL = 67002;

		public const int STATUS_GET_ITEM_INFO_FAIL = 67003;

		public const int STATUS_UNKNOWN_PLATFORM = 67004;

		public const int STATUS_CREATE_UJ_ORDER_FAIL = 67005;

		public const int STATUS_ORDER_ALREADY_EXISTS = 67006;

		public const int STATUS_GET_PARAMS_FOR_VERIFY_ANDROID_ORDER_FAIL = 67008;

		public const int STATUS_ORDER_NOT_FOUND = 67009;

		public const int STATUS_PRODUCT_ID_NOT_MATCH = 67011;

		public const int STATUS_BUYER_PLAYER_ID_NOT_MATCH = 67012;

		public const int STATUS_UPDATE_UJ_ORDER_FAIL = 67013;

		public const int STATUS_OFFER_PRODUCT_FAIL = 67014;

		public const int STATUS_ALREADY_OFFER_PRODUCT = 67018;

		public const int STATUS_UJ_ORDER_ID_IS_EMPTY = 67019;

		public const int STATUS_GOOGLE_RSA_KEY_NOT_MATCH = 67022;

		public const int STATUS_MISS_SOME_CHARACTER_DATA = 67026;

		public const int STATUS_COME_FROM_BAN_COUNTRY = 67033;

		public const int STATUS_INVALID_ITEM = 67035;

		public const int STATUS_ALREADY_IN_BILLING_LIST = 67036;

		public const int STATUS_COME_FROM_BAN_IP = 67037;

		public const int STATUS_SERVER_FORCE_CONSUME = 67038;

		public const int STATUS_BILLING_NOT_READY = 67039;

		public const int STATUS_PLAYER_IS_LOCKED_BY_THIRD_SERVICE = 67043;

		public const int STATUS_THIRD_WEB_API_NO_RESPONSE = 67044;

		public const int STATUS_THIRD_WEB_API_ERROR_RESPONSE = 67045;

		public const int STATUS_BILLING_IS_NOT_SUPPORTED = 67046;

		public const int STATUS_TP_ACCOUNT_IS_NOT_LOGIN = 67047;

		public UJBillingStatus()
		{ }
	}
}
