using System;
using System.Diagnostics;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	internal class MarsLog
	{
		public enum eLogTag
		{
			UNITY = 0,
			NATIVE = 1
		}

		private const string DEBUG_COLOR = "<color=#008000>";

		private const string INFO_COLOR = "<color=#0000FF>";

		private const string WARNING_COLOR = "<color=#FFA500>";

		private const string ERROR_COLOR = "<color=#FF0000>";

		private const string COLOR_END = "</color>";

		private const string TAG = "[MARS-UNITY] ";

		private static readonly bool debug;

		private static Logger marsLogger;

		private static MarsDefines.LogLevel _logLevel;

		internal static void SetLoglevel(MarsDefines.LogLevel loglevel)
		{ }

		private static bool isEnableLog(MarsDefines.LogLevel loglevel)
		{ return default; }

		private static string output(string fmt, params object[] args)
		{ return default; }

		[Conditional("MARS_LOG_DEBUG")]
		public static void Debug(string fmt, params object[] args)
		{ }

		[Conditional("MARS_LOG_DEBUG")]
		public static void DebugWithTag(string tag, string fmt, params object[] args)
		{ }

		public static void Info(string fmt, params object[] args)
		{ }

		public static void InfoWithTag(string tag, string fmt, params object[] args)
		{ }

		public static void Warn(string fmt, params object[] args)
		{ }

		public static void WarnWithTag(string tag, string fmt, params object[] args)
		{ }

		public static void Error(string fmt, params object[] args)
		{ }

		public static void ErrorWithTag(string tag, string fmt, params object[] args)
		{ }

		public static void Exception(Exception ex)
		{ }

		public static void Exception(Exception ex, string fmt, params object[] args)
		{ }

		public static void ExceptionWithTag(Exception ex, string tag, string fmt, params object[] args)
		{ }

		public MarsLog()
		{ }

		static MarsLog()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
