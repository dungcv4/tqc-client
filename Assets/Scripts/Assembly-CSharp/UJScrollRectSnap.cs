using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UJScrollRectSnap : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
	[Serializable]
	public class UJScrollRectSnapEvent : UnityEvent<int>
	{
		public UJScrollRectSnapEvent()
		{ }
	}

	[Serializable]
	public class UJScrollRectSnapStartEvent : UnityEvent<int>
	{
		public UJScrollRectSnapStartEvent()
		{ }
	}

	public class UJScrollRectSnapEndEvent : UnityEvent<int>
	{
		public UJScrollRectSnapEndEvent()
		{ }
	}

	public delegate void SnapEvent(int snapIdx);

	[CompilerGenerated]
	private sealed class _003CSnapRect_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UJScrollRectSnap _003C_003E4__this;

		private float _003CstartNormal_003E5__2;

		private float _003CendNormal_003E5__3;

		private float _003Cduration_003E5__4;

		private float _003Ctimer_003E5__5;

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
		public _003CSnapRect_003Ed__53(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CSnapRectRecursive_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UJScrollRectSnap _003C_003E4__this;

		private float _003CstartNormal_003E5__2;

		private float _003CendNormal_003E5__3;

		private float _003Cduration_003E5__4;

		private float _003Ctimer_003E5__5;

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
		public _003CSnapRectRecursive_003Ed__54(int _003C_003E1__state)
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

	private ScrollRect _scrollRect;

	private RectTransform _scrollRectTrans;

	private float _startPos;

	private float _endPos;

	private float _snapNormalizePosEachPage;

	private float _endPageIdx;

	private float _delta;

	private int _snapPageNum;

	private float _rectWidth;

	[SerializeField]
	private UJScrollRectSnapEvent _onSnapFinished;

	[SerializeField]
	private UJScrollRectSnapStartEvent _onSnapStart;

	[SerializeField]
	private UJScrollRectSnapEndEvent _onSnapEnd;

	public SnapEvent OnSnapFinished;

	public AnimationCurve curve;

	public float speed;

	public bool fastSnap;

	public bool horizontalSnap;

	public bool dragEnable;

	private int _snapPageIdx;

	[SerializeField]
	private int _maxPage;

	public Button prevBtn;

	public Button nextBtn;

	public float _minSnapPageWidth;

	public float _snapPageWidth;

	public float _backSpeed;

	private bool _isScrollWheel;

	public UJScrollRectSnapEvent onSnapFinished
	{
		get
		{ return default; }
		set
		{ }
	}

	public UJScrollRectSnapStartEvent onSnapStart
	{
		get
		{ return default; }
		set
		{ }
	}

	public UJScrollRectSnapEndEvent onSnapEnd
	{
		get
		{ return default; }
		set
		{ }
	}

	public int snapPageIdx
	{
		get
		{ return default; }
	}

	public int maxPage
	{
		get
		{ return default; }
		set
		{ }
	}

	private void Awake()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void PrevPage()
	{ }

	private void NextPage()
	{ }

	public void OnBeginDrag(PointerEventData eventData)
	{ }

	public void OnDrag(PointerEventData eventData)
	{ }

	public void OnEndDrag(PointerEventData eventData)
	{ }

	public void Snap(bool forward)
	{ }

	public void SnapToPage(int pageIdx, bool disableEffect = true)
	{ }

	public void SetButtonVisible(bool status)
	{ }

	[IteratorStateMachine(typeof(_003CSnapRect_003Ed__53))]
	private IEnumerator SnapRect()
	{ return default; }

	[IteratorStateMachine(typeof(_003CSnapRectRecursive_003Ed__54))]
	private IEnumerator SnapRectRecursive()
	{ return default; }

	public void OnScroll(PointerEventData eventData)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/UJScrollRectSnap___ctor.c RVA 0x0196565C
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public UJScrollRectSnap()
	{
	}
}
