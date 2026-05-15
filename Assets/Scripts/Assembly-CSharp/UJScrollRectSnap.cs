// Source: Ghidra work/06_ghidra/decompiled_full/UJScrollRectSnap/ — full 1-1 port.
//   .ctor RVA 0x196565C  Awake 0x1964B64  set_maxPage  SnapToPage 0x1964E14  Snap 0x1964F48
//   SnapRect 0x1965178 (+<SnapRect>d__53.MoveNext)  SnapRectRecursive 0x19651E4 (+d__54.MoveNext)
//   OnBeginDrag 0x...  OnDrag  OnEndDrag  OnScroll  PrevPage→Snap(false)  NextPage→Snap(true)
//   SetButtonVisible  get_snapPageIdx
// Source: dump.cs TypeDefIndex 243 — field offsets 1-1.
// Previous port was a chế-cháo reimplementation: empty .ctor, GetComponent (not FindInParents),
// SnapToPage used pageIdx/(_maxPage-1) instead of _snapNormalizePosEachPage*pageIdx, Awake never
// computed _snapNormalizePosEachPage nor called SnapToPage(0,true) → wrong initial scroll
// position (off-by-one page vs the infinite-scroll clone pages). Fixed 1-1 here.
// .ctor const 0x98=_minSnapPageWidth=0.15f from binary-verified DAT_008e3c28; 0xA0=_backSpeed=0.25f
// (0x3e800000). Exception text from stringliteral.json #9848.

using System;
using System.Collections;
using System.Diagnostics;
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

	private ScrollRect _scrollRect;                       // 0x20

	private RectTransform _scrollRectTrans;               // 0x28

	private float _startPos;                              // 0x30

	private float _endPos;                                // 0x34

	private float _snapNormalizePosEachPage;              // 0x38

	private float _endPageIdx;                            // 0x3C

	private float _delta;                                 // 0x40

	private int _snapPageNum;                             // 0x44

	private float _rectWidth;                             // 0x48

	[SerializeField]
	private UJScrollRectSnapEvent _onSnapFinished;        // 0x50

	[SerializeField]
	private UJScrollRectSnapStartEvent _onSnapStart;      // 0x58

	[SerializeField]
	private UJScrollRectSnapEndEvent _onSnapEnd;          // 0x60

	public SnapEvent OnSnapFinished;                      // 0x68

	public AnimationCurve curve;                          // 0x70

	public float speed;                                   // 0x78

	public bool fastSnap;                                 // 0x7C

	public bool horizontalSnap;                           // 0x7D

	public bool dragEnable;                               // 0x7E

	private int _snapPageIdx;                             // 0x80

	[SerializeField]
	private int _maxPage;                                 // 0x84

	public Button prevBtn;                                // 0x88

	public Button nextBtn;                                // 0x90

	public float _minSnapPageWidth;                       // 0x98

	public float _snapPageWidth;                          // 0x9C

	public float _backSpeed;                              // 0xA0

	private bool _isScrollWheel;                          // 0xA4

	public UJScrollRectSnapEvent onSnapFinished { get { return _onSnapFinished; } set { _onSnapFinished = value; } }
	public UJScrollRectSnapStartEvent onSnapStart { get { return _onSnapStart; } set { _onSnapStart = value; } }
	public UJScrollRectSnapEndEvent onSnapEnd { get { return _onSnapEnd; } set { _onSnapEnd = value; } }

	// Source: Ghidra get_snapPageIdx.c — return this+0x80.
	public int snapPageIdx { get { return _snapPageIdx; } }

	// Source: Ghidra set_maxPage.c — 1-1.
	public int maxPage
	{
		get { return _maxPage; }
		set
		{
			_maxPage = value;
			if (value != 0)
			{
				_snapNormalizePosEachPage = 1f / (float)value;
				Rect r = _scrollRectTrans.rect;     // get_rect; *(this+0x48)=width
				_rectWidth = r.width;
			}
			enabled = (value != 0);
		}
	}

	// Source: Ghidra .ctor RVA 0x196565C — 1-1 field init.
	public UJScrollRectSnap()
	{
		_onSnapFinished = new UJScrollRectSnapEvent();        // 0x50
		_onSnapStart = new UJScrollRectSnapStartEvent();      // 0x58
		_onSnapEnd = new UJScrollRectSnapEndEvent();          // 0x60
		curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);        // 0x70
		horizontalSnap = true;                                 // 0x7D
		dragEnable = true;                                     // 0x7E
		_minSnapPageWidth = 0.15f;                             // 0x98 (DAT_008e3c28, binary-verified)
		_backSpeed = 0.25f;                                    // 0xA0 (0x3e800000)
	}

	// Source: Ghidra Awake.c RVA 0x1964B64 — 1-1.
	private void Awake()
	{
		if (_scrollRect == null)
		{
			_scrollRect = WndFormUtility.FindInParents<ScrollRect>(gameObject);
		}
		if (_scrollRect == null)
		{
			return;
		}
		Transform t = _scrollRect.transform;
		_scrollRectTrans = (t == null) ? null : (t as RectTransform);
		if (_maxPage == 0)
		{
			enabled = false;
			return;
		}
		if (_scrollRectTrans != null)
		{
			Rect r = _scrollRectTrans.rect;          // get_rect; *(this+0x48)=width
			_rectWidth = r.width;
			_snapNormalizePosEachPage = 1f / (float)_maxPage;     // 0x38
			if (prevBtn != null)
			{
				prevBtn.onClick.AddListener(PrevPage);
			}
			if (nextBtn == null)
			{
				SnapToPage(0, true);
				return;
			}
			nextBtn.onClick.AddListener(NextPage);
			SnapToPage(0, true);
		}
	}

	// Source: Ghidra PrevPage.c → Snap(false) ; NextPage.c → Snap(true).
	private void PrevPage() { Snap(false); }
	private void NextPage() { Snap(true); }

	// Source: Ghidra SnapToPage.c RVA 0x1964E14 — 1-1.
	public void SnapToPage(int pageIdx, bool disableEffect = true)
	{
		if (pageIdx < 0 || _maxPage < pageIdx)
		{
			return;
		}
		StopCoroutine(SnapRect());
		_snapPageIdx = pageIdx;
		if (!disableEffect)
		{
			if (_onSnapStart != null)
			{
				_onSnapStart.Invoke(pageIdx);
				StartCoroutine(SnapRect());
			}
			return;
		}
		if (!horizontalSnap)
		{
			_scrollRect.verticalNormalizedPosition = _snapNormalizePosEachPage * (float)(_maxPage - pageIdx);
		}
		else
		{
			_scrollRect.horizontalNormalizedPosition = _snapNormalizePosEachPage * (float)pageIdx;
		}
		if (_onSnapFinished != null)
		{
			_onSnapFinished.Invoke(_snapPageIdx);
		}
	}

	// Source: Ghidra Snap.c RVA 0x1964F48 — 1-1.
	public void Snap(bool forward)
	{
		if (_maxPage == 0)
		{
			return;
		}
		StopCoroutine(SnapRect());
		int idx = _snapPageIdx;
		if (!forward)
		{
			if (idx == 0)
			{
				return;
			}
			if (_onSnapStart != null)
			{
				_onSnapStart.Invoke(idx);
			}
			idx = _snapPageIdx - 1;
		}
		else
		{
			if (idx == _maxPage)
			{
				return;
			}
			if (_onSnapStart != null)
			{
				_onSnapStart.Invoke(idx);
			}
			idx = _snapPageIdx + 1;
		}
		_snapPageIdx = idx;
		StartCoroutine(SnapRect());
	}

	// Source: Ghidra OnBeginDrag.c — 1-1.
	public void OnBeginDrag(PointerEventData eventData)
	{
		_scrollRect.OnBeginDrag(eventData);
		if (_maxPage != 0 && dragEnable)
		{
			Vector2 local;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_scrollRectTrans, eventData.position, eventData.pressEventCamera, out local))
			{
				_startPos = horizontalSnap ? local.x : local.y;
				if (!fastSnap)
				{
					StopCoroutine(SnapRect());
				}
				else
				{
					StopCoroutine(SnapRectRecursive());
				}
				if (_onSnapStart != null)
				{
					_onSnapStart.Invoke(_snapPageIdx);
				}
			}
		}
	}

	// Source: Ghidra OnDrag.c — forward to ScrollRect IDragHandler.
	public void OnDrag(PointerEventData eventData)
	{
		_scrollRect.OnDrag(eventData);
	}

	// Source: Ghidra OnEndDrag.c — 1-1.
	public void OnEndDrag(PointerEventData eventData)
	{
		_scrollRect.OnEndDrag(eventData);
		if (_onSnapEnd != null)
		{
			_onSnapEnd.Invoke(0);
		}
		if (_maxPage == 0)
		{
			return;
		}
		if (!dragEnable)
		{
			return;
		}
		Vector2 local;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_scrollRectTrans, eventData.position, eventData.pressEventCamera, out local))
		{
			return;
		}
		_endPos = horizontalSnap ? local.x : local.y;
		_delta = (_endPos - _startPos) / _rectWidth;
		bool below;
		if (Mathf.Abs(_delta) >= _minSnapPageWidth)
		{
			below = false;
			_snapPageNum = (Mathf.Abs(_delta) >= _snapPageWidth) ? 2 : 1;
		}
		else
		{
			_snapPageNum = 0;
			below = true;
		}

		if (_delta >= 0f)
		{
			if (_delta <= 0f)
			{
				return;
			}
			int idx = _snapPageIdx;
			if (idx == 0)
			{
				return;
			}
			if (fastSnap)
			{
				int n;
				if (below)
				{
					n = 0;
				}
				else
				{
					if (_onSnapStart != null) _onSnapStart.Invoke(idx);
					idx = _snapPageIdx;
					n = _snapPageNum;
				}
				int prev = idx - n;
				_endPageIdx = (prev < 0) ? 0f : (float)prev;
				StartCoroutine(SnapRectRecursive());
				return;
			}
			else
			{
				int n;
				if (below)
				{
					n = 0;
				}
				else
				{
					if (_onSnapStart != null) _onSnapStart.Invoke(idx);
					idx = _snapPageIdx;
					n = _snapPageNum;
				}
				int prev = idx - n;
				_snapPageIdx = (prev < 0) ? 0 : prev;
				StartCoroutine(SnapRect());
				return;
			}
		}
		else
		{
			int cur = _snapPageIdx;
			int maxp = _maxPage;
			if (cur == maxp)
			{
				return;
			}
			if (fastSnap)
			{
				int n;
				if (below)
				{
					n = 0;
				}
				else
				{
					if (_onSnapStart != null) _onSnapStart.Invoke(cur);
					n = _snapPageNum;
					cur = _snapPageIdx;
					maxp = _maxPage;
				}
				int next = n + cur;
				_endPageIdx = (next <= maxp) ? (float)next : (float)maxp;
				StartCoroutine(SnapRectRecursive());
				return;
			}
			else
			{
				int n;
				if (below)
				{
					n = 0;
				}
				else
				{
					if (_onSnapStart != null) _onSnapStart.Invoke(cur);
					n = _snapPageNum;
					cur = _snapPageIdx;
					maxp = _maxPage;
				}
				int next = n + cur;
				_snapPageIdx = (next <= maxp) ? next : maxp;
				StartCoroutine(SnapRect());
				return;
			}
		}
	}

	// Source: Ghidra OnScroll.c — this+0xA4 = true.
	public void OnScroll(PointerEventData eventData)
	{
		_isScrollWheel = true;
	}

	// Source: Ghidra <SnapRect>d__53.MoveNext — 1-1 iterator.
	private IEnumerator SnapRect()
	{
		if (_scrollRect == null)
		{
			throw new Exception("Scroll Rect can not be null");
		}
		float startNormal = horizontalSnap ? _scrollRect.horizontalNormalizedPosition : _scrollRect.verticalNormalizedPosition;
		int idx = horizontalSnap ? _snapPageIdx : (_maxPage - _snapPageIdx);
		float endNormal = _snapNormalizePosEachPage * (float)idx;
		float duration = Mathf.Abs((endNormal - startNormal) / speed);
		float timer = 0f;
		while (true)
		{
			timer += Time.deltaTime / duration;
			if (timer > 1f)
			{
				timer = 1f;
			}
			if (curve != null)
			{
				float e = curve.Evaluate(timer);
				if (e < 0f)
				{
					e = 0f;
				}
				if (!horizontalSnap)
				{
					_scrollRect.verticalNormalizedPosition = startNormal + (endNormal - startNormal) * e;
				}
				else
				{
					_scrollRect.horizontalNormalizedPosition = startNormal + (endNormal - startNormal) * e;
				}
			}
			yield return new WaitForEndOfFrame();
			if (timer >= 1f)
			{
				if (_onSnapFinished != null)
				{
					_onSnapFinished.Invoke(_snapPageIdx);
				}
				yield break;
			}
		}
	}

	// Source: Ghidra <SnapRectRecursive>d__54.MoveNext — 1-1 iterator.
	private IEnumerator SnapRectRecursive()
	{
		if (_scrollRect == null)
		{
			throw new Exception("Scroll Rect can not be null");
		}
		int step = (_delta < 0f) ? _snapPageNum : (-_snapPageNum);
		int target = step + _snapPageIdx;
		if (target < 0)
		{
			target = 0;
		}
		else if (_maxPage < target)
		{
			target = _maxPage;
		}
		_snapPageIdx = target;
		float startNormal = horizontalSnap ? _scrollRect.horizontalNormalizedPosition : _scrollRect.verticalNormalizedPosition;
		int idx = horizontalSnap ? _snapPageIdx : (_maxPage - _snapPageIdx);
		float endNormal = _snapNormalizePosEachPage * (float)idx;
		float duration = Mathf.Abs((endNormal - startNormal) / speed);
		if (_snapPageNum == 0)
		{
			duration = _backSpeed;
		}
		float timer = 0f;
		while (true)
		{
			timer += Time.deltaTime / duration;
			if (timer > 1f)
			{
				timer = 1f;
			}
			if (curve != null)
			{
				float e = curve.Evaluate(timer);
				if (e < 0f)
				{
					e = 0f;
				}
				if (!horizontalSnap)
				{
					_scrollRect.verticalNormalizedPosition = startNormal + (endNormal - startNormal) * e;
				}
				else
				{
					_scrollRect.horizontalNormalizedPosition = startNormal + (endNormal - startNormal) * e;
				}
			}
			yield return new WaitForEndOfFrame();
			if (timer >= 1f)
			{
				if (_endPageIdx == (float)_snapPageIdx)
				{
					if (_onSnapFinished != null)
					{
						_onSnapFinished.Invoke(_snapPageIdx);
					}
				}
				else
				{
					StartCoroutine(SnapRectRecursive());
				}
				yield break;
			}
		}
	}

	// Source: Ghidra SetButtonVisible.c — 1-1: both buttons SetActive(show).
	public void SetButtonVisible(bool show)
	{
		if (prevBtn != null)
		{
			prevBtn.gameObject.SetActive(show);
		}
		if (nextBtn != null)
		{
			nextBtn.gameObject.SetActive(show);
		}
	}
}
