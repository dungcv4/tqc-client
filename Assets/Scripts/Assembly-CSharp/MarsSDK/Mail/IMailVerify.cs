namespace MarsSDK.Mail
{
	internal interface IMailVerify
	{
		int RequestSendVerifyCode(string mailAddress);

		int RequestVerify(string verifyCode);

		bool IsBind();

		bool IsWaitToVerify();

		bool IsWaitingStatus();

		int GetBindFailCount();

		int GetVerifyCodeExpiryTime();

		int GetVerifyCodeResendTime();

		int GetBindFailTime();

		int GetBindCodeRemainingCount();

		int GetBindFailHistory();

		void RequestMailVerifyStatus();

		bool GetDailyLockStatus();

		bool GetAccountVerifyLockStatus();

		bool GetLockStatus();

		string GetVerifyPrefix();

		string GetBindMail();

		void TestServerMailContent();
	}
}
