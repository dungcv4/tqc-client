using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsAgent.PageManager
{
	[Serializable]
	public class UJMSDKLocalWebPageVerifyCode : ScriptableObject
	{
		public const string LocalWebPageVerifyCodePath = "Assets/Resources/MarsSDK/LocalWebPageVerifyCode.asset";

		public const string UserCenter = "user_center.html";

		public const string AccountBinding = "account_binding.html";

		public const string AccountManagement = "account_management.html";

		public const string DeleteAccountDialog = "delete_account_dialog.html";

		public const string Fanpage = "fanpage.html";

		public const string SystemMessage = "system_message.html";

		public const string WebviewEmbedded = "webview_embedded.html";

		public string UserCenterVerifyCode;

		public string AccountBindingVerifyCode;

		public string AccountManagementVerifyCode;

		public string DeleteAccountDialogVerifyCode;

		public string FanpageVerifyCode;

		public string SystemMessageVerifyCode;

		public string WebviewEmbeddedVerifyCode;

		private static UJMSDKLocalWebPageVerifyCode _instance;

		public static UJMSDKLocalWebPageVerifyCode Instance
		{
			get
			{ return default; }
			set
			{ }
		}

		static UJMSDKLocalWebPageVerifyCode()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool VerifyWebPage(string fileName)
		{ return default; }

		public UJMSDKLocalWebPageVerifyCode()
		{ }
	}
}
