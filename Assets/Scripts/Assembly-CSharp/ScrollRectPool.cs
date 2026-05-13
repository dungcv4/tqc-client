using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollRectPool : MonoBehaviour
{
	public enum LayoutType
	{
		VERTICAL = 0,
		GRID = 1
	}

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

	private void Awake()
	{ }

	private void Init()
	{ }

	private void GetLayoutSettings()
	{ }

	public void OnScroll(Vector2 pos)
	{ }

	private void UpdateScrollItem(bool scrollUp)
	{ }

	private void ShiftItem(bool scrollUp)
	{ }

	public void UpdateVisibleItems()
	{ }

	public void Reset(int totalItems)
	{ }

	public void Reset()
	{ }

	private void OnDisable()
	{ }

	public void OnPositionChangeDone(Vector2 uselessParam)
	{ }

	public ScrollRectPool()
	{ }
}
