using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.TV
{
	internal class TelephoneVerify_Android : ITelephoneVerify
	{
		private static AndroidJavaObject GetPlatformInstance()
		{ return default; }

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

		public TelephoneVerify_Android()
		{ }
	}
}
