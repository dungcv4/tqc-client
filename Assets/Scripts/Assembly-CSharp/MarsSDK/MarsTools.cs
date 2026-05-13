using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	public class MarsTools
	{
		private static readonly DateTime Jan1st1970;

		private static bool _initialized;

		private static AndroidJavaClass _ujTools;

		private static AndroidJavaObject _WaitProgress;

		public static long CurrentTimeMillis()
		{ return default; }

		public static long CurrentTimeSecond()
		{ return default; }

		public static long DateTimeToMillis(DateTime dateTime)
		{ return default; }

		static MarsTools()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static AndroidJavaObject getToolsInstance()
		{ return default; }

		public static AndroidJavaObject getWaitProgress()
		{ return default; }

		public static void RunOnUIThread(Action action)
		{ }

		public static void SafeToast(string msg)
		{ }

		public static void ShowProgress(string msg)
		{ }

		public static void DismissProgress()
		{ }

		[Obsolete("已棄用，請使用 NetworkStateManager", false)] // shim: v1 Wrap compat
		public static bool CheckNetworkConnection()
		{ return default; }

		[Obsolete("已棄用，請使用 NetworkStateManager", false)] // shim: v1 Wrap compat
		public static string GetNetworkType()
		{ return default; }

		public static void CopyToClipboard(string msg, string toastMsg)
		{ }

		public static void CopyToClipboard(string label, string msg, string toastMsg)
		{ }

		public static string GetDeviceLanguage()
		{ return default; }

		public static int GetDeviceLanguageAfterMapping()
		{ return default; }

		public static void SetSDKLanguage(int language)
		{ }

		public static int GetSDKLanguage()
		{ return default; }

		public static string GetStringResource(string stringName)
		{ return default; }

		public static string GetFacebookAndroidKeyHashes()
		{ return default; }

		public static void RestartUnity()
		{ }

		public static double GetScaledDensity()
		{ return default; }

		public static int GetDensityDpi()
		{ return default; }

		public MarsTools()
		{ }
	}
}
