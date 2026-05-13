using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace MarsSDK.Demo.UI
{
	public class HintPanel : PanelBase
	{
		[CompilerGenerated]
		private sealed class _003CHideForDelay_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public HintPanel _003C_003E4__this;

			public Action doActionsAfterHide;

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
			public _003CHideForDelay_003Ed__5(int _003C_003E1__state)
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

		private static HintPanel _instance;

		public static HintPanel Instance
		{
			get
			{ return default; }
		}

		public HintPanel()
		{ }

		public void Show(string msg, float display_time = 0f, Action doActionsAfterHide = null)
		{ }

		[IteratorStateMachine(typeof(_003CHideForDelay_003Ed__5))]
		private IEnumerator HideForDelay(float delay, Action doActionsAfterHide = null)
		{ return default; }
	}
}
