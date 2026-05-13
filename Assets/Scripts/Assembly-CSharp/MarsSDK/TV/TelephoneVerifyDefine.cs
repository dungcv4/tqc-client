using Cpp2IlInjected;

namespace MarsSDK.TV
{
	public class TelephoneVerifyDefine
	{
		public const int MAX_FAIL_COUNT = 20;

		public const int MAX_DAY_FAIL_COUNT = 5;

		public const int STATUS_UI_MOBILE_VERIFY_SUCCESS = 0;

		public const int STATUS_UI_MOBILE_VERIFY_FAIL = 1;

		public const int STATUS_UI_MOBILE_VERIFY_FORMAT_ERROR = 2;

		public const int STATUS_UI_MOBILE_VERIFY_CODE_TIMEOUT = 3;

		public const int STATUS_UI_MOBILE_VERIFY_HAS_BINDED = 4;

		public const int STATUS_UI_MOBILE_VERIFY_LOCKED = 5;

		public const int STATUS_UI_MOBILE_VERIFY_RUNING = 6;

		public const int STATUS_UI_MOBILE_VERIFY_NEED_TO_BIND_FIRST = 7;

		public const int STATUS_UI_MOBILE_VERIFY_HAS_PASSED = 8;

		public const int Action_RequestVerifyCode = 1;

		public const int Action_DoVerify = 2;

		public TelephoneVerifyDefine()
		{ }
	}
}
