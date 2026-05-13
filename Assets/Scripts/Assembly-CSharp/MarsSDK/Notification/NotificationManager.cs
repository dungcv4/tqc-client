using System;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Notification
{
	public class NotificationManager : MarsMessageProcess
	{
		public delegate void NotificationPermissionCallback();

		public delegate void NotificationSettingCallback(string status);

		private static NotificationManager instance;

		private static AndroidJavaObject notificationManagerInstance;

		public static event NotificationPermissionCallback OnNotificationPermissionAllow
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static event NotificationPermissionCallback OnNotificationPermissionDontAllow
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static event NotificationPermissionCallback OnNotificationPermissionDenied
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static event NotificationPermissionCallback OnNotificationPermissionCancel
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static event NotificationSettingCallback OnIOSNotificationSettingStatus
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public static NotificationManager Instance()
		{ return default; }

		public NotificationManager() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetMessageUserType()
		{ return default; }

		protected override void V_doMessageProcess(string action, string[] status)
		{ }

		public bool NotificationAreEnable()
		{ return default; }

		public bool AndroidNotificationArePaused()
		{ return default; }

		public bool HasRuntimePermissionSupported()
		{ return default; }

		public void GetIOSNotificationSettingStatus()
		{ }

		public void ShowRuntimePermission(bool useMarsDialog)
		{ }

		public void ShowPermissionPage()
		{ }

		public void CreateChannelGroup(string groupID, string groupName, string description)
		{ }

		public void CreateChannel(string channelID, string channelName, ENotificationImportance importance, string groupID = "")
		{ }

		public void DeleteChannelGroup(string groupID)
		{ }

		public void DeleteChannel(string channelID)
		{ }

		public string[] GetAllChannel()
		{ return default; }

		public string[] GetAllGroup()
		{ return default; }

		public string CheckChannelParentGroup(string channelID)
		{ return default; }

		public void RegisterNotifyOnAndroid(string channelID, int notifyID, string title, string msg, string iconName, long time)
		{ }

		public void RegisterNotifyWithImageOnAndroid(string channelID, int notifyID, string title, string msg, string iconName, long time, byte[] imageData)
		{ }

		public void RegisterNotifyWithImageURLOnAndroid(string channelID, int notifyID, string title, string msg, string iconName, long time, string imageURL)
		{ }

		public void RegisterNotifyWithBannerOnAndroid(string channelID, int notifyID, string iconName, long time, byte[] imageData, byte[] bigImageData, byte[] headsUpImageData)
		{ }

		public void RegisterNotifyOnIOS(string title, string msg, long time)
		{ }

		public void RegisterNotifyWithImageOnIOS(string title, string msg, long time, byte[] imageData)
		{ }

		public void CancelNotify(int notifyID = 0, string notifyTitle = "")
		{ }

		public void CancelAllNotify()
		{ }

		[Obsolete("This API is deprecated. Use [RegisterNotifyWithImageOnAndroid()] instead", true)]
		public void RegisterNotifyOnAndroid(string channelID, int notifyID, string title, string msg, string iconName, long time, byte[] imageData)
		{ }

		[Obsolete("This API is deprecated. Use [RegisterNotifyWithImageURLOnAndroid()] instead", true)]
		public void RegisterNotifyOnAndroid(string channelID, int notifyID, string title, string msg, string iconName, long time, string imageURL)
		{ }

		[Obsolete("This API is deprecated. Use [RegisterNotifyOnIOS()] instead", true)]
		public void RegisterNotifyOnOnIOS(string title, string msg, long time)
		{ }

		[Obsolete("This API is deprecated. Use [RegisterNotifyWithImageOnIOS()] instead", true)]
		public void RegisterNotifyOnOnIOS(string title, string msg, long time, byte[] imageData)
		{ }

		private void MessagePermissionLogic(string args)
		{ }

		private void MessageSettingsLogic(string status)
		{ }

		public void InvokeOnNotificationPermissionCancel()
		{ }
	}
}
