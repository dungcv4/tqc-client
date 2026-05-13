using System;
using Cpp2IlInjected;
using MarsSDK.Mail;

namespace MarsSDK.Platform
{
	public class MailVerifyPlatform : BasePlatformSingleton<MailVerifyPlatform>
	{
		public delegate void dEventProcess();

		public delegate void dEventErrorProcess(int code);

		public const string MAIL_VERIFY_PLATFORM_SEND_STATUS_MSG = "1";

		public const string MAIL_VERIFY_PLATFORM_BIND_RESULT_MSG = "2";

		public const string MAIL_VERIFY_PLATFORM_SEND_VERIFY_CODE_RESULT_MSG = "3";

		public const int STATUS_REQUEST_VERIFY_CODE_SUCCESS = 0;

		[Obsolete("不建議使用，伺服器回傳狀態應該有更清楚的指示狀態", false)]
		public const int STATUS_REQUEST_VERIFY_CODE_FAIL = 1;

		public const int STATUS_MAIL_REPEAT = 2;

		public const int STATUS_REQUEST_BOUND = 3;

		public const int STATUS_REQUEST_LOCK = 4;

		public const int STATUS_REQUEST_ENTITY_CREATE_FAIL = 5;

		public const int STATUS_REQUEST_VERIFY_FAIL = 6;

		public static dEventProcess doEventHasBinded;

		public static dEventProcess doEventBindSucceeded;

		public static dEventErrorProcess doEventBindFailed;

		public static dEventProcess doEventMailAddressRepeat;

		public static dEventProcess doEventVerifyCodeTimeout;

		public static dEventProcess doEventBindFailLock;

		public static dEventProcess doEventVerifyCodeReady;

		public static dEventProcess doEventMailWrongFormat;

		private IMailVerify _mailVerifyAction;

		public MailVerifyPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void MsgProcessSendMailStatus(string[] args)
		{ }

		private void MsgProcessBindMailResult(string[] args)
		{ }

		private void MsgProcessSendVerifyCodeResult(string[] args)
		{ }

		private void CreateEvent(int result)
		{ }

		public int RequestSendVerifyCode(string mailAddress)
		{ return default; }

		public int RequestVerify(string verifyCode)
		{ return default; }

		public bool IsBind()
		{ return default; }

		public bool GetDailyLockStatus()
		{ return default; }

		public bool GetAccountVerifyLockStatus()
		{ return default; }

		public bool GetLockStatus()
		{ return default; }

		public int GetVerifyCodeExpiryTime()
		{ return default; }

		public int GetBindCodeRemainingCount()
		{ return default; }

		public int GetVerifyCodeResendTime()
		{ return default; }

		public bool IsWaitToVerify()
		{ return default; }

		public bool IsWaitingStatus()
		{ return default; }

		public int GetBindFailTime()
		{ return default; }

		public int GetBindFailHistory()
		{ return default; }

		public int GetBindFailCount()
		{ return default; }

		public void RequestMailVerifyStatus()
		{ }

		public string GetVerifyPrefix()
		{ return default; }

		public string GetBindMail()
		{ return default; }

		public void TestServerMailContent()
		{ }

		[Obsolete("已棄用，請使用 MailVerifyPlatform.RequestSendVerifyCode，該 API 將於之後移除", true)]
		public int SendVerifyCodeToMail(string mailAddress)
		{ return default; }

		[Obsolete("已棄用，請使用 MailVerifyPlatform.RequestVerify，該 API 將於之後移除", true)]
		public int Verify(string verifyCode)
		{ return default; }

		[Obsolete("已棄用，請使用 MailVerifyPlatform.GetAccountVerifyLockStatus，該 API 將於之後移除", true)]
		public bool GetForeverLockStatus()
		{ return default; }

		[Obsolete("已棄用，請使用 MailVerifyPlatform.GetVerifyCodePrefix，該 API 將於之後移除", true)]
		public string GetVerifyCodePrefix()
		{ return default; }
	}
}
