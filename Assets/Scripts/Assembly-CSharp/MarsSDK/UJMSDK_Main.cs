using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace MarsSDK
{
	public class UJMSDK_Main : MarsMonoSingleton<UJMSDKMessageListener>
	{
		[CompilerGenerated]
		private sealed class _003CInitMSDK_003Ed__1 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public string url;

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
			public _003CInitMSDK_003Ed__1(int _003C_003E1__state)
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

		private UJMSDK_Main()
		{ }

		[IteratorStateMachine(typeof(_003CInitMSDK_003Ed__1))]
		public static IEnumerator InitMSDK(string url)
		{ return default; }
	}
}
