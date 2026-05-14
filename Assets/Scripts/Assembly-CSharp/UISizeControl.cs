// Source: Ghidra work/06_ghidra/decompiled_full/UISizeControl/ — manages max width/height constraint for child rect.

using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

public class UISizeControl : MonoBehaviour
{
	private float _minWidth;
	private float _minHeight;
	public float _maxWidth;
	public float _maxHeight;

	[NoToLua] public RectTransform _childrenRect;
	[NoToLua] public GameObject _bottomArrow;
	[NoToLua] public GameObject _TopArrow;
	[NoToLua] public GameObject _leftArrow;
	[NoToLua] public GameObject _rightArrow;

	private RectTransform _myRect;
	private LayoutElement _ControlLayout;
	private float _preWidth;
	private float _preHeight;

	[NoToLua] public bool bFixedSize;

	private Vector3[] _tempArray;

	[NoToLua]
	private void Start()
	{
		_myRect = GetComponent<RectTransform>();
		_ControlLayout = GetComponent<LayoutElement>();
	}

	[NoToLua]
	private void Update()
	{
		CorrectSetting();
	}

	[NoToLua]
	private void CorrectSetting()
	{
		if (_myRect == null || _childrenRect == null) return;
		Vector2 childSize = _childrenRect.sizeDelta;
		float w = Mathf.Clamp(childSize.x, _minWidth, _maxWidth);
		float h = Mathf.Clamp(childSize.y, _minHeight, _maxHeight);
		if (bFixedSize) { w = _maxWidth; h = _maxHeight; }
		if (Mathf.Abs(_preWidth - w) > 0.01f || Mathf.Abs(_preHeight - h) > 0.01f)
		{
			_myRect.sizeDelta = new Vector2(w, h);
			_preWidth = w;
			_preHeight = h;
		}
	}

	public void SetMaxWidth(float width) { _maxWidth = width; CorrectSetting(); }
	public void SetMaxHeight(float height) { _maxHeight = height; CorrectSetting(); }

	[NoToLua] private void SetMinWidth(float width) { _minWidth = width; }
	[NoToLua] private void SetMinHeight(float height) { _minHeight = height; }

	public UISizeControl() { }
}
