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
		{ return default; }
	}

	public int MaxCount
	{
		get
		{ return default; }
		set
		{ }
	}

	protected abstract float GetSize(RectTransform item);

	protected abstract float GetDimension(Vector2 vector);

	protected abstract Vector2 GetVector(float value);

	protected abstract float GetPos(RectTransform item);

	protected abstract int OneOrMinusOne();

	protected abstract void ResetContentPos(float startPos);

	private new void Awake()
	{ }

	private void Init()
	{ }

	public void ResetByIndex(int startIndex)
	{ }

	public void Reset()
	{ }

	public void _Reset()
	{ }

	private void Update()
	{ }

	private void ReturnToPool(RectTransform rt)
	{ }

	private RectTransform GetFromPool()
	{ return default; }

	private RectTransform NewItemAtStart()
	{ return default; }

	private RectTransform NewItemAtEnd()
	{ return default; }

	private RectTransform InstantiateItem()
	{ return default; }

	public override void OnBeginDrag(PointerEventData eventData)
	{ }

	public override void OnDrag(PointerEventData eventData)
	{ }

	protected InfiniteScroll()
	{ }
}
