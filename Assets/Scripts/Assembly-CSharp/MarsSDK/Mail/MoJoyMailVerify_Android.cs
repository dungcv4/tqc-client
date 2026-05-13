using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Mail
{
	internal class MoJoyMailVerify_Android : IMailVerify
	{
		public static AndroidJavaObject GetMoJoyMailVerifyInstance()
		{ return default; }

		public int RequestSendVerifyCode(string mailAddress)
		{ return default; }

		public int RequestVerify(string verifyCode)
		{ return default; }

		public bool IsBind()
		{ return default; }

		public bool IsWaitToVerify()
		{ return default; }

		public bool IsWaitingStatus()
		{ return default; }

		public int GetBindFailCount()
		{ return default; }

		public int GetVerifyCodeExpiryTime()
		{ return default; }

		public int GetBindFailTime()
		{ return default; }

		public int GetBindCodeRemainingCount()
		{ return default; }

		public int GetBindFailHistory()
		{ return default; }

		public void RequestMailVerifyStatus()
		{ }

		public bool GetDailyLockStatus()
		{ return default; }

		public bool GetAccountVerifyLockStatus()
		{ return default; }

		public bool GetLockStatus()
		{ return default; }

		public string GetVerifyPrefix()
		{ return default; }

		public string GetBindMail()
		{ return default; }

		public void TestServerMailContent()
		{ }

		public int GetVerifyCodeResendTime()
		{ return default; }

		public MoJoyMailVerify_Android()
		{ }
	}
}
