using Cpp2IlInjected;

namespace MarsSDK.TL
{
	public class TelephoneLoginDefine
	{
		public const int MaxVerifyFailedCount = 5;

		public const int DailyMessageCount = 20;

		public const int STATUS_TL_SUCCESS = 0;

		public const int STATUS_TL_ROBOT_CODE_NOT_MATCH = 1;

		public const int STATUS_TL_FORMAT_ERROR = 2;

		public const int STATUS_TL_CODE_TIMEOUT = 3;

		public const int STATUS_TL_LOCKED = 4;

		public const int SERVER_REPLY_STATUS_SUCCESS = 0;

		public const int SERVER_REPLY_STATUS_MISS_PARAM = 1;

		public const int SERVER_REPLY_STATUS_VERIFY_FAILED = 2;

		public const int SERVER_REPLY_STATUS_GET_ENTITY_FAILED = 3;

		public const int SERVER_REPLY_STATUS_CAN_NOT_SEND_MESSAGE_TODAY = 4;

		public const int SERVER_REPLY_STATUS_SEND_MESSAGE_FAILED = 5;

		public const int SERVER_REPLY_STATUS_GET_DATA_MANAGER_FAILED = 6;

		public const int SERVER_REPLY_STATUS_LOAD_PROPERTY_FAILED = 7;

		public const int SERVER_REPLY_STATUS_ACCOUNT_IS_LOCKED = 8;

		public const int SERVER_REPLY_STATUS_ACCOUNT_IS_RESET = 9;

		public const int Action_CheckIsRobot = 1;

		public const int Action_VerifyAndSendMessage = 2;

		public const int Action_VerifyMessageAndLogin = 3;

		public TelephoneLoginDefine()
		{ }
	}
}
