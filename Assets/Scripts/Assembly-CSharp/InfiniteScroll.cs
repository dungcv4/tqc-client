// Source: Ghidra work/06_ghidra/decompiled_full/InfiniteScroll/ — recycling scroll base; subclassed by H/V variants.
// Maintains a pool of items; spawns/recycles as user scrolls.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InfiniteScroll : ScrollRect
{
	public delegate void InfiniteScrollUpdateEvent(int index, RectTransform rt);
	public delegate void InfiniteScrollRemoveEvent(int index);

	private RectTransform _t;
	public RectTransform prefabItem;
	public List<RectTransform> _pool;
	private int _maxCount;
	private int _startIndex;
	private int _endIndex;
	private bool init;
	private Vector2 dragOffset;
	private bool _nextTickReset;

	public InfiniteScrollUpdateEvent OnUpdateData;
	public InfiniteScrollRemoveEvent OnRemove;

	protected RectTransform t
	{
		get
		{
			if (_t == null) _t = GetComponent<RectTransform>();
			return _t;
		}
	}

	public int MaxCount
	{
		get { return _maxCount; }
		set { _maxCount = value; _nextTickReset = true; }
	}

	protected abstract float GetSize(RectTransform item);
	protected abstract float GetDimension(Vector2 vector);
	protected abstract Vector2 GetVector(float value);
	protected abstract float GetPos(RectTransform item);
	protected abstract int OneOrMinusOne();
	protected abstract void ResetContentPos(float startPos);

	private new void Awake()
	{
		base.Awake();
		_pool = new List<RectTransform>();
	}

	private void Init()
	{
		if (init) return;
		init = true;
		_startIndex = 0;
		_endIndex = -1;
	}

	public void ResetByIndex(int startIndex)
	{
		Init();
		// Move everything to pool
		while (_endIndex >= _startIndex)
		{
			if (content.childCount > 0)
			{
				RectTransform rt = content.GetChild(content.childCount - 1) as RectTransform;
				ReturnToPool(rt);
			}
			_endIndex--;
		}
		_startIndex = startIndex;
		_endIndex = startIndex - 1;
		ResetContentPos(0f);
	}

	public void Reset() { ResetByIndex(0); }

	public void _Reset() { Reset(); }

	private void Update()
	{
		if (_nextTickReset)
		{
			_nextTickReset = false;
			Reset();
		}
		if (!init || content == null) return;
		// Ensure visible area covered: add at end if needed.
		Vector2 pos = content.anchoredPosition;
		float dim = GetDimension(viewport.rect.size);
		while (_endIndex < _maxCount - 1)
		{
			RectTransform last = (content.childCount > 0) ? content.GetChild(content.childCount - 1) as RectTransform : null;
			if (last != null)
			{
				float lastPos = GetPos(last);
				float lastSize = GetSize(last);
				if (lastPos * OneOrMinusOne() + lastSize > dim + GetDimension(pos) * OneOrMinusOne() + dim) break;
			}
			NewItemAtEnd();
		}
	}

	private void ReturnToPool(RectTransform rt)
	{
		if (rt == null) return;
		rt.gameObject.SetActive(false);
		rt.SetParent(transform, false);
		_pool.Add(rt);
	}

	private RectTransform GetFromPool()
	{
		if (_pool.Count > 0)
		{
			RectTransform rt = _pool[_pool.Count - 1];
			_pool.RemoveAt(_pool.Count - 1);
			rt.gameObject.SetActive(true);
			return rt;
		}
		return InstantiateItem();
	}

	private RectTransform NewItemAtStart()
	{
		if (_startIndex <= 0) return null;
		_startIndex--;
		RectTransform rt = GetFromPool();
		rt.SetParent(content, false);
		rt.SetSiblingIndex(0);
		if (OnUpdateData != null) OnUpdateData(_startIndex, rt);
		return rt;
	}

	private RectTransform NewItemAtEnd()
	{
		_endIndex++;
		if (_endIndex >= _maxCount) { _endIndex--; return null; }
		RectTransform rt = GetFromPool();
		rt.SetParent(content, false);
		rt.SetAsLastSibling();
		if (OnUpdateData != null) OnUpdateData(_endIndex, rt);
		return rt;
	}

	private RectTransform InstantiateItem()
	{
		if (prefabItem == null) return null;
		var go = Object.Instantiate(prefabItem.gameObject);
		return go.GetComponent<RectTransform>();
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);
		dragOffset = content.anchoredPosition;
	}

	public override void OnDrag(PointerEventData eventData)
	{
		base.OnDrag(eventData);
	}

	protected InfiniteScroll() { }
}
