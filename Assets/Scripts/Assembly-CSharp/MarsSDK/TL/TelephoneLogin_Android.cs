using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.TL
{
	internal class TelephoneLogin_Android : ITelephoneLogin
	{
		private static AndroidJavaObject mJo;

		public AndroidJavaObject GetTelephoneLoginClass()
		{ return default; }

		public string GetIAmNotARobot()
		{ return default; }

		public int GetMessageCount()
		{ return default; }

		public long GetVerifyCodeExpiry()
		{ return default; }

		public int StartTelephoneLogin()
		{ return default; }

		public int RequestSendMessage(string verifyCode, string telephoneNumber)
		{ return default; }

		public int VerifyMessage(string verifyCode)
		{ return default; }

		public TelephoneLogin_Android()
		{ }
	}
}
