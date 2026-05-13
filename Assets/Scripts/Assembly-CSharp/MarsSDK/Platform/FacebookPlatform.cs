using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class FacebookPlatform : BasePlatform
	{
		public delegate void dMsgProcess(string[] args);

		public static string Regist_Facebook_APPID_Login;

		public static string Regist_Facebook_APPID_Game;

		public const string FACEBOOK_PLATFORM_CALLBACK_ONSUCCESS_MSG = "1";

		public const string FACEBOOK_PLATFORM_CALLBACK_ONCANCEL_MSG = "2";

		public const string FACEBOOK_PLATFORM_CALLBACK_ONERROR_MSG = "3";

		public const string SHARELINK_API = "ShareLink";

		public const string SHAREPHOTO_API = "SharePhoto";

		public const string LOGIN_API = "Login";

		public const string REQUEST_PERMISSION_API = "Request_Pemissions";

		public const string SEND_GAME_REQUEST_API = "Send_Game_Request";

		public const string SEND_REQUEST_DATA_API = "Send_Request_Data";

		public const string DELETE_REQUEST = "Delete_Request";

		public const string POSTPHOTO_API = "PostPhoto";

		public const string POSTMESSAGE_API = "PostMessage";

		public const string FRIENDLIST_API = "FriendList";

		private static FacebookPlatform mInstance;

		private static string mGameFBUID;

		private static List<FacebookRequestData> requestList;

		public static dMsgProcess doMsgProcessCallbackOnSuccess;

		public static dMsgProcess doMsgProcessCallbackOnCancel;

		public static dMsgProcess doMsgProcessCallbackOnError;

		private static AndroidJavaClass mJc;

		public FacebookPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static FacebookPlatform Instance()
		{ return default; }

		private static void MsgProcessOnSuccess(string[] args)
		{ }

		private static void MsgProcessOnCancel(string[] args)
		{ }

		private static void MsgProcessOnError(string[] args)
		{ }

		public static List<FacebookRequestData> GetRequestDatatList()
		{ return default; }

		private static void ProcessRequestData(string data)
		{ }

		public static AndroidJavaClass GetFacebookClass()
		{ return default; }

		public void LogOut()
		{ }

		public void LoginByFacebook()
		{ }

		public void LoginByFacebook(bool updateToken)
		{ }

		public void LoginByFacebook(string[] permission, bool updateToken)
		{ }

		public void BindByFacebook()
		{ }

		public void BindByFacebook(string[] permission, bool replace)
		{ }

		public void UnBindByFacebook()
		{ }

		public void ShareLink(string appLinkUrl)
		{ }

		public void SharePhoto(byte[] imageData)
		{ }

		[NoToLua]
		public void SharePhoto(sbyte[] imageData)
		{ }

		public void RequestFriendList()
		{ }

		public string[] GetFriendList()
		{ return default; }

		public string GetFriendName(string uid)
		{ return default; }

		public string GetGameFBUID()
		{ return default; }

		public bool IsBindFacebook()
		{ return default; }

		public string GetFacebookDisplayName()
		{ return default; }

		public string GetFacebookPhotoUri()
		{ return default; }

		public string GetFacebookUID()
		{ return default; }

		public string GetBindFacebookUID()
		{ return default; }

		public void LogEvent(string eventName)
		{ }

		public void LogEvent(string eventName, double valueToSum)
		{ }

		public void LogEvent(string eventName, Dictionary<string, object> info)
		{ }

		public void LogEvent(string eventName, double valueToSum, Dictionary<string, object> info)
		{ }

		static FacebookPlatform()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
