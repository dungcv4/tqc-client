using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using MarsSDK.TV;

namespace MarsSDK.Platform
{
	public class TelephoneVerifyPlatform : BasePlatform
	{
		public delegate void dEventProcess();

		public delegate void dEventErrorProcess(int code);

		public const string TELEPHONE_VERIFY_PLATFORM_SEND_STATUS_MSG = "1";

		public const string TELEPHONE_VERIFY_PLATFORM_BIND_RESULT_MSG = "2";

		public const string TELEPHONE_VERIFY_PLATFORM_REQUEST_PASS_RESULT_MSG = "3";

		public const string TELEPHONE_VERIFY_PLATFORM_SEND_VERIFY_CODE_RESULT_MSG = "4";

		public const string TELEPHONE_VERIFY_PLATFORM_VERIFY_CODE_CANCELED_MSG = "5";

		public const int STATUS_REQUEST_VERIFY_CODE_SUCCESS = 0;

		public const int STATUS_VERIFY_FAIL_REACH_LIMITED = 1;

		public const int STATUS_TELEPHONE_NUMBER_REACH_BIND_LIMIT = 2;

		public const int STATUS_ACCOUNT_HAS_PHONE_VERIFIED = 3;

		public const int STATUS_ENTITY_CREATE_FAIL = 4;

		public const int STATUS_VERIFY_CODE_SEND_FAIL = 5;

		public const int STATUS_VERIFY_FAIL = 6;

		public const int STATUS_TELEPHONE_NUMBER_ALREADY_BIND_THIS_ACC = 7;

		public const int STATUS_VERIFY_CODE_EXIST = 8;

		public const int STATUS_BIND_SUCCESS = 0;

		private ITelephoneVerify _telephoneVerifyAction;

		private static TelephoneVerifyPlatform mInstance;

		public static dEventProcess doEventHasBinded;

		public static dEventProcess doEventBindSucceeded;

		public static dEventErrorProcess doEventBindFailed;

		public static dEventProcess doEventTelephoneNumberReachBindLimit;

		public static dEventErrorProcess doEventVerifyCodeSendFailed;

		public static dEventProcess doEventVerifyCodeTimeout;

		public static dEventProcess doEventBindFailLock;

		public static dEventProcess doEventRequestPassSucceeded;

		public static dEventErrorProcess doEventRequestPassFailed;

		public static dEventProcess doEventVerifyCodeReady;

		public static dEventProcess doEventVerifyCodeExist;

		public static dEventProcess doEventVerifyCodeCanceled;

		[Obsolete("已棄用，此事件等同doEventHasBinded，該事件將於之後移除", true)]
		public static dEventProcess doEventAccountHasTelephoneVerified;

		[Obsolete("已棄用，此事件不會被觸發，該事件將於之後移除", true)]
		public static dEventProcess doEventVerifyFailed;

		[Obsolete("已棄用，此事件等同doEventBindFailLock，該事件將於之後移除", true)]
		public static dEventProcess doEventVerifyFailReachLimited;

		public TelephoneVerifyPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static TelephoneVerifyPlatform Instance()
		{ return default; }

		private void MsgProcessSendTelephoneVerifyStatus(string[] args)
		{ }

		private void MsgProcessBindResult(string[] args)
		{ }

		private void MsgProcessRequestPassResult(string[] args)
		{ }

		private void MsgProcessSendVerifyCodeResult(string[] args)
		{ }

		private void MsgProcessVerifyCodeCanceled(string[] args)
		{ }

		private void CreateEvent(int result)
		{ }

		public bool IsBind()
		{ return default; }

		public int GetBindFailCount()
		{ return default; }

		public int GetBindFailHistory()
		{ return default; }

		public long GetBindFailTime()
		{ return default; }

		public long GetVerifyCodeExpiryTime()
		{ return default; }

		public bool IsWaitToVerify()
		{ return default; }

		[Obsolete("已棄用，可能造成錯誤", false)]
		public bool IsWaitingStatus()
		{ return default; }

		public long GetPassTime()
		{ return default; }

		public bool CheckPass()
		{ return default; }

		public string GetBoundPhoneNumber()
		{ return default; }

		public bool GetDailyLockStatus()
		{ return default; }

		public bool GetAccountVerifyLockStatus()
		{ return default; }

		public bool GetLockStatus()
		{ return default; }

		public int GetBindCodeRemainingCount()
		{ return default; }

		public Dictionary<string, string> GetAreaCodeList()
		{ return default; }

		public int RequestSendVerifyCode(string phoneNumber)
		{ return default; }

		public int RequestVerify(string verifyCode)
		{ return default; }

		public int RequestTelephoneVerifyPass()
		{ return default; }

		public int RequestPassVerify(string verifyCode)
		{ return default; }

		public void RequestTelephoneVerifyStatus()
		{ }

		public void TestServerSMSContent(string phoneNumber)
		{ }

		[Obsolete("已棄用，請使用 TelephoneVerifyPlatform.GetAccountVerifyLockStatus，該 API 將於之後移除", true)]
		public bool GetForeverLockStatus()
		{ return default; }

		[Obsolete("已棄用，請使用 TelephoneVerifyPlatform.RequestSendVerifyCode，該 API 將於之後移除", true)]
		public int StartTelephoneBind(string telephoneNumber)
		{ return default; }

		[Obsolete("已棄用，請使用 TelephoneVerifyPlatform.RequestVerify，該 API 將於之後移除", true)]
		public int DoTelephoneBind(string verifyCode)
		{ return default; }

		[Obsolete("已棄用，請使用 TelephoneVerifyPlatform.RequestPassVerify，該 API 將於之後移除", true)]
		public int DoTelephoneVerifyPass(string verifyCode)
		{ return default; }

		[Obsolete("已棄用，請使用 TelephoneVerifyPlatform.RequestTelephoneVerifyStatus，該 API 將於之後移除", true)]
		public void GetTelephoneVerifyStatus()
		{ }
	}
}
