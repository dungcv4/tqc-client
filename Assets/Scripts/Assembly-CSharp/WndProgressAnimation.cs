using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Progress Animation")]
public class WndProgressAnimation : WndAnimation
{
	[Serializable]
	public class Node
	{
		[SerializeField]
		private float _fillAmount;

		[SerializeField]
		private float _duration;

		public float fillAmount
		{
			get
			{ return default; }
			set
			{ }
		}

		public float duration
		{
			get
			{ return default; }
			set
			{ }
		}

		public Node()
		{ }
	}

	[CompilerGenerated]
	private sealed class _003C__CoroutineUpdate_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WndProgressAnimation _003C_003E4__this;

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
		public _003C__CoroutineUpdate_003Ed__12(int _003C_003E1__state)
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

	[SerializeField]
	private Image _uiSprite;

	[SerializeField]
	private Node[] _pathNodes;

	private int _curFrame;

	public Image uiSprite
	{
		get
		{ return default; }
		set
		{ }
	}

	public Node[] pathNodes
	{
		get
		{ return default; }
		set
		{ }
	}

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	[IteratorStateMachine(typeof(_003C__CoroutineUpdate_003Ed__12))]
	private IEnumerator __CoroutineUpdate()
	{ return default; }

	private void __Process(float dTime)
	{ }

	private void InitAnimation()
	{ }

	public override void PlayAnimation()
	{ }

	public override void StopAnimation()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WndProgressAnimation___ctor.c RVA 0x017CA370
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public WndProgressAnimation()
	{
	}
}
