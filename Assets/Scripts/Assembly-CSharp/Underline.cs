using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Underline : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayUpdateUnderline_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Underline _003C_003E4__this;

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
		public _003CDelayUpdateUnderline_003Ed__5(int _003C_003E1__state)
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

	public Text sourceTextObj;

	public Image underlineImgObj;

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void OnEnable()
	{ }

	public void ProcessUnderlineImage()
	{ }

	[IteratorStateMachine(typeof(_003CDelayUpdateUnderline_003Ed__5))]
	private IEnumerator DelayUpdateUnderline()
	{ return default; }

	// Source: Ghidra work/06_ghidra/decompiled_rva/Underline___ctor.c RVA 0x017C8B8C
	// 1-1: just base.ctor — no field init.
	public Underline()
	{
	}
}
