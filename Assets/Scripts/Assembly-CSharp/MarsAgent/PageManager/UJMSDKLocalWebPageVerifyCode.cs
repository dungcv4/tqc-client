// Source: dump.cs — UJMSDKLocalWebPageVerifyCode (TypeDefIndex 1255).
// cctor RVA 0x19F7820 (4 bytes — empty / just ret) → empty body.
// Instance getter RVA 0x19F7824 (256 bytes) — pattern likely lazy Resources.Load; no Ghidra .c yet.
// Instance setter RVA 0x19F7924 (104 bytes) — _instance = value; possibly DontDestroyOnLoad; no Ghidra .c yet.
// VerifyWebPage RVA 0x19F798C (8 bytes — `mov w0, #x; ret`); no Ghidra .c yet.

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
			// TODO: Ghidra RVA 0x19F7824 not yet decompiled. Probable: lazy Resources.Load("MarsSDK/LocalWebPageVerifyCode").
			get { return _instance; }
			// TODO: Ghidra RVA 0x19F7924 not yet decompiled.
			set { _instance = value; }
		}

		// Source: dump.cs cctor RVA 0x19F7820 — empty (4-byte ret).
		static UJMSDKLocalWebPageVerifyCode() { }

		// TODO: Ghidra RVA 0x19F798C not yet decompiled. 8-byte body — likely `return false;` or `return true;`.
		public bool VerifyWebPage(string fileName) { return false; }

		public UJMSDKLocalWebPageVerifyCode() { }
	}
}
