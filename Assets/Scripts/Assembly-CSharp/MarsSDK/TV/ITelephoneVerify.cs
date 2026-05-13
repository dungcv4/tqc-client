using System.Collections.Generic;

namespace MarsSDK.TV
{
	internal interface ITelephoneVerify
	{
		bool IsBind();

		int GetBindFailCount();

		int GetBindFailHistory();

		long GetBindFailTime();

		long GetVerifyCodeExpiryTime();

		bool IsWaitToVerify();

		bool IsWaitingStatus();

		long GetPassTime();

		bool CheckPass();

		string GetBoundPhoneNumber();

		bool GetDailyLockStatus();

		bool GetAccountVerifyLockStatus();

		bool GetLockStatus();

		int GetBindCodeRemainingCount();

		Dictionary<string, string> GetAreaCodeList();

		int RequestSendVerifyCode(string phoneNumber);

		int RequestVerify(string verifyCode);

		int RequestTelephoneVerifyPass();

		int RequestPassVerify(string verifyCode);

		void RequestTelephoneVerifyStatus();

		void TestServerSMSContent(string phoneNumber);
	}
}
