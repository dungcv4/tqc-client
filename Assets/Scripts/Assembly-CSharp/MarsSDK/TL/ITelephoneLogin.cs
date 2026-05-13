namespace MarsSDK.TL
{
	internal interface ITelephoneLogin
	{
		string GetIAmNotARobot();

		int GetMessageCount();

		long GetVerifyCodeExpiry();

		int StartTelephoneLogin();

		int RequestSendMessage(string verifyCode, string telephoneNumber);

		int VerifyMessage(string verifyCode);
	}
}
