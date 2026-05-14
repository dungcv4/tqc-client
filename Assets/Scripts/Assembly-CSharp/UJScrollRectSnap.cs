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
			/* compiler-gen state machine — iterator inlined elsewhere; no-op */
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			/* compiler-gen state machine — iterator inlined elsewhere; no-op */
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
			/* compiler-gen state machine — iterator inlined elsewhere; no-op */
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
			/* compiler-gen state machine — iterator inlined elsewhere; no-op */
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			/* compiler-gen state machine — iterator inlined elsewhere; no-op */
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
			/* compiler-gen state machine — iterator inlined elsewhere; no-op */
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

	// Source: Ghidra work/06_ghidra/decompiled_full/UJScrollRectSnap/ — page-snapping scroll rect.

	public UJScrollRectSnapEvent onSnapFinished { get { return _onSnapFinished; } set { _onSnapFinished = value; } }
	public UJScrollRectSnapStartEvent onSnapStart { get { return _onSnapStart; } set { _onSnapStart = value; } }
	public UJScrollRectSnapEndEvent onSnapEnd { get { return _onSnapEnd; } set { _onSnapEnd = value; } }
	public int snapPageIdx { get { return _snapPageIdx; } }
	public int maxPage { get { return _maxPage; } set { _maxPage = value; } }

	private void Awake()
	{
		_scrollRect = GetComponent<ScrollRect>();
		_scrollRectTrans = GetComponent<RectTransform>();
		if (_scrollRectTrans != null)
		{
			_rectWidth = horizontalSnap ? _scrollRectTrans.rect.width : _scrollRectTrans.rect.height;
		}
		if (prevBtn != null) prevBtn.onClick.AddListener(PrevPage);
		if (nextBtn != null) nextBtn.onClick.AddListener(NextPage);
	}

	private void PrevPage() { SnapToPage(_snapPageIdx - 1); }
	private void NextPage() { SnapToPage(_snapPageIdx + 1); }

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (_onSnapStart != null) _onSnapStart.Invoke(_snapPageIdx);
	}

	public void OnDrag(PointerEventData eventData) { }

	public void OnEndDrag(PointerEventData eventData)
	{
		Snap(true);
	}

	public void Snap(bool forward)
	{
		if (_scrollRect == null || _maxPage <= 0) return;
		float pos = horizontalSnap ? _scrollRect.horizontalNormalizedPosition : 1f - _scrollRect.verticalNormalizedPosition;
		int target = Mathf.RoundToInt(pos * (_maxPage - 1));
		SnapToPage(target);
	}

	public void SnapToPage(int pageIdx, bool disableEffect = true)
	{
		pageIdx = Mathf.Clamp(pageIdx, 0, Mathf.Max(0, _maxPage - 1));
		_snapPageIdx = pageIdx;
		if (_scrollRect == null) return;
		float targetNormal = (_maxPage > 1) ? (float)pageIdx / (float)(_maxPage - 1) : 0f;
		if (horizontalSnap) _scrollRect.horizontalNormalizedPosition = targetNormal;
		else _scrollRect.verticalNormalizedPosition = 1f - targetNormal;
		SetButtonVisible(true);
		if (_onSnapFinished != null) _onSnapFinished.Invoke(pageIdx);
		if (OnSnapFinished != null) OnSnapFinished(pageIdx);
		if (_onSnapEnd != null) _onSnapEnd.Invoke(pageIdx);
	}

	public void SetButtonVisible(bool status)
	{
		if (prevBtn != null) prevBtn.gameObject.SetActive(status && _snapPageIdx > 0);
		if (nextBtn != null) nextBtn.gameObject.SetActive(status && _snapPageIdx < _maxPage - 1);
	}

	private IEnumerator SnapRect()
	{
		yield return null;
	}

	private IEnumerator SnapRectRecursive()
	{
		yield return null;
	}

	public void OnScroll(PointerEventData eventData)
	{
		_isScrollWheel = true;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UJScrollRectSnap___ctor.c RVA 0x0196565C
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public UJScrollRectSnap()
	{
	}
}
