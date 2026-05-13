using Cpp2IlInjected;

namespace MarsSDK
{
	public class MarsErrorCode
	{
		public const int STATUS_SUCCESS = 0;

		public const int STATUS_INPUT_PARAMS_ERROR = 41001;

		public const int STATUS_PARAM_COUNT_ERROR = 41002;

		public const int STATUS_GET_UNKNOWN_EXCEPTION = 41005;

		public const int STATUS_GET_NETWORK_ERROR = 41006;

		public const int STATUS_GET_NET_RESPONSE_ERROR = 41007;

		public const int STATUS_GET_SERVICE_ERROR = 41008;

		public const int STATUS_PLAYER_NOT_LOGIN = 41009;

		public const int STATUS_TOO_MANY_REQUEST = 41010;

		public const int STATUS_METHOD_NOT_SUPPORTED = 41011;

		public const int STATUS_GIFT_CODE_USED = 61004;

		public const int STATUS_GIFT_CODE_CHAR_REACHED_ENTER_TIME = 61005;

		public const int STATUS_EXCHANGE_GIFT_CODE_COOL_DOWN = 61007;

		public const int STATUS_GIFT_CODE_NOT_FOUND = 61009;

		public const int STATUS_GIFT_CODE_NOT_ENABLED = 61010;

		public const int STATUS_GIFT_CODE_RECEIVED = 61011;

		public const int STATUS_GIFT_CODE_EXPIRED = 61013;

		public const int STATUS_GIFT_CODE_NOT_ELIGIBLE = 61014;

		public const int STATUS_GIFT_CODE_EXCEEDED_AVAILABLE_COUNT = 61015;

		public const int STATUS_GIFT_CODE_WRONG_SERVER = 61017;

		public const int STATUS_GIFT_CODE_DUPLICATE = 61018;

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

		public const int STATUS_CONSUME_ORDER_FAILED = 67049;

		public const int STATUS_ORDER_IS_NOT_VERIFIED_BY_UJ_PLATFORM = 67050;

		public const int STATUS_ORDER_CANCELED = 67051;

		public const int STATUS_AWAITING_PAYMENT = 67052;

		public MarsErrorCode()
		{ }
	}
}
