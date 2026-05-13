using System;
using Cpp2IlInjected;
using MarsSDK.Mail;

namespace MarsSDK.Platform
{
	public class MoJoyPlatform : BasePlatformSingleton<MoJoyPlatform>
	{
		public delegate void dEventProcess();

		public delegate void dEventErrorProcess(int code);

		public const string MOJOY_PLATFORM_SEND_STATUS_MSG = "1";

		public const string MOJOY_PLATFORM_BIND_RESULT_MSG = "2";

		public const string MOJOY_PLATFORM_SEND_VERIFY_CODE_RESULT_MSG = "3";

		public const int STATUS_REQUEST_VERIFY_CODE_SUCCESS = 0;

		[Obsolete("不建議使用，伺服器回傳狀態應該有更清楚的指示狀態", false)]
		public const int STATUS_REQUEST_VERIFY_CODE_FAIL = 1;

		public const int STATUS_MAIL_REPEAT = 2;

		public const int STATUS_REQUEST_BOUND = 3;

		public const int STATUS_REQUEST_LOCK = 4;

		public const int STATUS_REQUEST_ENTITY_CREATE_FAIL = 5;

		public const int STATUS_REQUEST_VERIFY_FAIL = 6;

		public static dEventProcess doMoJoyEventHasBinded;

		public static dEventProcess doMoJoyEventBindSucceeded;

		public static dEventErrorProcess doMoJoyEventBindFailed;

		public static dEventProcess doMoJoyEventMailAddressRepeat;

		public static dEventProcess doMoJoyEventVerifyCodeTimeout;

		public static dEventProcess doMoJoyEventBindFailLock;

		public static dEventProcess doMoJoyEventVerifyCodeReady;

		public static dEventProcess doMoJoyEventMailWrongFormat;

		private IMailVerify _mojoyMailAction;

		public MoJoyPlatform() : base((EOperationAgent)default!)
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
	}
}
