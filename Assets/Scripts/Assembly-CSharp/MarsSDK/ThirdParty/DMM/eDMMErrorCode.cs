using Cpp2IlInjected;

namespace MarsSDK.ThirdParty.DMM
{
	public class eDMMErrorCode
	{
		public const int SUCCESS = 0;

		public const int FAILED = 100;

		public const int TOKEN_EXPIRED = 101;

		public const int DMM_NOT_LOGIN = 102;

		public const int CONNECT_CALLBACLKURL_FAILED = 103;

		public const int GET_USER_INFO_FAILED = 104;

		public const int PARAMS_FAILED = 200;

		public const int READMINE_SEND_ERROR = 300;

		public const int INSUFFICIENT_BALANCE = 400;

		public const int GET_PAYMENT_ID_FAILED = 401;

		public const int CHECK_ITEM_FAILED = 402;

		public const int PAYMENT_FAILED = 403;

		public const int GET_BALANCE_FAILED = 403;

		public const int EXECUTE_DMM_GAME_PLAYER_FAILED = 500;

		public const int DB_ERROR = 900;

		public const int MAINTENANCE = 901;

		public const int UNKNOWN_ERROR = 999;

		public const int PAYMENT_STATUS_GET_PAYMENT_ID = 1;

		public const int PAYMENT_STATUS_COMPLETED = 2;

		public const int PAYMENT_STATUS_CANCEL = 3;

		public const int PAYMENT_STATUS_FAILED = 4;

		public eDMMErrorCode()
		{ }
	}
}
