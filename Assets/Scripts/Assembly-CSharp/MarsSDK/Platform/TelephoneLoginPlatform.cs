using System;
using Cpp2IlInjected;
using MarsSDK.TL;

namespace MarsSDK.Platform
{
	public class TelephoneLoginPlatform : BasePlatform
	{
		public delegate void dEventProcess();

		public delegate void dEventProcessWithParams(string[] args);

		public const string TELEPHONE_LOGIN_PLATFORM_CHECK_IS_ROBOT = "1";

		public const string TELEPHONE_LOGIN_PLATFORM_SEND_MESSAGE_RESULT = "2";

		public const string TELEPHONE_LOGIN_PLATFORM_VERIFY_MESSAGE_RESULT = "3";

		private ITelephoneLogin _telephoneLoginAction;

		private static TelephoneLoginPlatform mInstance;

		public static dEventProcess doTLEventCheckIsRobot;

		public static dEventProcess doTLEventVerifyRobotCodeNotMatch;

		public static dEventProcess doTLEventTelephoneNumberFormatIncorrect;

		public static dEventProcess doTLEventVerifyCodeReady;

		public static dEventProcess doTLEventVerifyCodeTimeout;

		public static dEventProcess doTLEventVerifyMessageFailed;

		public static dEventProcess doTLEventDailyMessageLimit;

		public static dEventProcess doTLEventAccountIsLocked;

		public static dEventProcess doTLEventAccountIsReset;

		public static dEventProcessWithParams doTLEventSendMessageFailed;

		public TelephoneLoginPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static TelephoneLoginPlatform Instance()
		{ return default; }

		private void MsgProcessCheckIsRobot(string[] args)
		{ }

		private void MsgProcessSendMessageResult(string[] args)
		{ }

		private void MsgProcessVerifyMessageResult(string[] args)
		{ }

		private void CreateEvent(int resultCode)
		{ }

		public string GetIAmNotARobot()
		{ return default; }

		public int GetMessageCount()
		{ return default; }

		public long GetVerifyCodeExpiry()
		{ return default; }

		public void StartTelephoneLogin()
		{ }

		public void RequestSendMessage(string verifyCode, string telephoneNumber)
		{ }

		public void VerifyMessage(string verifyCode)
		{ }
	}
}
