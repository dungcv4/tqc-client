using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using MarsSDK.LitJson;
using UnityEngine.Networking;

namespace MarsSDK.Demo
{
	public class DemoSettings
	{
		[CompilerGenerated]
		private sealed class _003CReadSetting_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			private UnityWebRequest _003CloadingRequest_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{ return default; }
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{ return default; }
			}

			[DebuggerHidden]
			public _003CReadSetting_003Ed__9(int _003C_003E1__state)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			private bool MoveNext()
			{ return default; }

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private const string LAST_CONNET_SERVICEURL = "msdk_last_connect_service_url";

		private static string _jsonString;

		private static JsonData _jd;

		private static DemoSettings _instance;

		private string[] gameIdList;

		private string[] projectNameList;

		private static string assetPath;

		private static bool _mars_demo_settings_exist_check;

		private static bool _mars_demo_settings_exist;

		public static string LastConnectEnvironment
		{
			get
			{ return default; }
		}

		public static string LastConnectServiceURL
		{
			get
			{ return default; }
			set
			{ }
		}

		[IteratorStateMachine(typeof(_003CReadSetting_003Ed__9))]
		public IEnumerator ReadSetting()
		{ return default; }

		public static bool CheckDemoSettingExist()
		{ return default; }

		public DemoSettings()
		{ }

		public static DemoSettings Instance()
		{ return default; }

		public JsonData GetDemoServiceURLList()
		{ return default; }

		public string GetDemoGameIDByProjectName(string projName)
		{ return default; }

		public string GetDemoServiceURL(string gameId, string env)
		{ return default; }

		public string GetDemoProjectName(string gameId)
		{ return default; }

		public string[] GetProjectNameList()
		{ return default; }

		public string[] GetGameIDList()
		{ return default; }

		static DemoSettings()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
