using System;
using Cpp2IlInjected;
using MarsSDK.LitJson;
using UnityEngine;

namespace MarsSDK
{
	public class MarsLogReport : MarsBrigeSingleton<MarsLogReport>
	{
		// Source: dump.cs EOperationAgent.LogReport  // matches class name
		private const string TAG_NATIVE = "[MARS-Native] ";

		private const string CB_MSG_LOG_REPORT = "1";

		private const string CB_MSG_CRASH_REPORT = "2";

		private static AndroidJavaClass mJc;

		private static AndroidJavaObject mJo;

		public MarsLogReport() : base(EOperationAgent.LogReport)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static AndroidJavaObject getJavaInstance()
		{ return default; }

		[Obsolete("此功能已由MarsServer設定")]
		public void setCrashReportEnabled(bool enabled)
		{ }

		[Obsolete("此功能已由MarsServer設定")]
		public void setLogReportEnabled(bool enabled)
		{ }

		[Obsolete("此功能已由MarsServer設定")]
		public void setLogReportThreshold(MarsDefines.LogLevel reportLevel)
		{ }

		internal void SetLogReportConfig(JsonData jd)
		{ }

		private void MsgProcessLogReport(string[] args)
		{ }

		private void MsgProcessCrashReport(string[] args)
		{ }
	}
}
