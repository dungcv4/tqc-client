// Source: Ghidra work/06_ghidra/decompiled_full/ScrollRectPool/ — recycling scroll pool that reuses N item GameObjects.
// Simplified port: spawns visible items as needed, fires onScroll callback when a slot rebinds.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollRectPool : MonoBehaviour
{
	public enum LayoutType { VERTICAL = 0, GRID = 1 }

	public ScrollRect scrollRect;
	public float scrollRectWidth;
	public float scrollRectHeight;
	public float scrollItemWidth;
	public float scrollItemHeight;
	public LayoutGroup layoutGroup;
	public GameObject scrollItemTemplate;
	public UnityEvent<int, RectTransform> onScroll;

	private RectTransform _content;
	private int _totalItems;
	private List<RectTransform> _itemPool;
	private float _scrollUpBorder;
	private float _scrollDownBorder;
	private int _nextDataIdx;
	private int _prevDataIdx;
	private int _topIdx;
	private int _lastIdx;
	private LayoutType _layoutType;
	private float _spacingY;
	private bool _init;
	private bool _firstDisable;
	private bool _disableScroll;

	private void Awake() { _itemPool = new List<RectTransform>(); }

	private void Init()
	{
		if (_init) return;
		_init = true;
		if (scrollRect != null) _content = scrollRect.content;
		GetLayoutSettings();
		if (scrollRect != null) scrollRect.onValueChanged.AddListener(OnScroll);
	}

	private void GetLayoutSettings()
	{
		_layoutType = LayoutType.VERTICAL;
		if (layoutGroup is GridLayoutGroup) _layoutType = LayoutType.GRID;
		if (layoutGroup is VerticalLayoutGroup vg) _spacingY = vg.spacing;
		else if (layoutGroup is GridLayoutGroup gl) _spacingY = gl.spacing.y;
	}

	public void OnScroll(Vector2 pos)
	{
		if (!_init || _disableScroll) return;
		UpdateVisibleItems();
	}

	private void UpdateScrollItem(bool scrollUp) { ShiftItem(scrollUp); }
	private void ShiftItem(bool scrollUp) { UpdateVisibleItems(); }

	public void UpdateVisibleItems()
	{
		if (_content == null || scrollItemTemplate == null) return;
		float contentY = _content.anchoredPosition.y;
		int topIdx = Mathf.Max(0, Mathf.FloorToInt(contentY / (scrollItemHeight + _spacingY)));
		int visibleCount = Mathf.CeilToInt(scrollRectHeight / (scrollItemHeight + _spacingY)) + 2;
		int lastIdx = Mathf.Min(_totalItems - 1, topIdx + visibleCount - 1);

		// Ensure pool has enough items
		while (_itemPool.Count < visibleCount)
		{
			GameObject go = Object.Instantiate(scrollItemTemplate, _content);
			go.SetActive(true);
			_itemPool.Add(go.GetComponent<RectTransform>());
		}

		// Bind each pool slot to a data index
		for (int i = 0; i < _itemPool.Count; i++)
		{
			int dataIdx = topIdx + i;
			RectTransform rt = _itemPool[i];
			if (dataIdx > lastIdx || dataIdx >= _totalItems)
			{
				rt.gameObject.SetActive(false);
				continue;
			}
			rt.gameObject.SetActive(true);
			rt.anchoredPosition = new Vector2(0, -dataIdx * (scrollItemHeight + _spacingY));
			if (onScroll != null) onScroll.Invoke(dataIdx, rt);
		}
		_topIdx = topIdx;
		_lastIdx = lastIdx;
	}

	public void Reset(int totalItems)
	{
		_totalItems = totalItems;
		if (!_init) Init();
		if (_content != null)
		{
			_content.sizeDelta = new Vector2(_content.sizeDelta.x, totalItems * (scrollItemHeight + _spacingY));
			_content.anchoredPosition = Vector2.zero;
		}
		UpdateVisibleItems();
	}

	public void Reset() { Reset(_totalItems); }

	private void OnDisable() { _firstDisable = true; }

	public void OnPositionChangeDone(Vector2 uselessParam) { }

	public ScrollRectPool() { }
}
