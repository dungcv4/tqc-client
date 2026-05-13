using Cpp2IlInjected;

namespace MarsSDK.Mail
{
	public class MailVerifyDefine
	{
		public static int MAX_FAIL_COUNT;

		public static int MAX_DAY_FAIL_COUNT;

		public const int STATUS_MAIL_VERIFY_SUCCESS = 0;

		public const int STATUS_MAIL_VERIFY_FAIL = 1;

		public const int STATUS_MAIL_VERIFY_FORMAT_ERROR = 2;

		public const int STATUS_MAIL_VERIFY_CODE_TIMEOUT = 3;

		public const int STATUS_MAIL_VERIFY_HAD_BINDED = 4;

		public const int STATUS_MAIL_VERIFY_LOCKED = 5;

		public const int STATUS_MAIL_VERIFY_SENT = 6;

		public const int Action_RequestVerifyCode = 1;

		public const int Action_DoVerify = 2;

		public MailVerifyDefine()
		{ }

		static MailVerifyDefine()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
