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

	[NoToLua]
	public RectTransform _childrenRect;

	[NoToLua]
	public GameObject _bottomArrow;

	[NoToLua]
	public GameObject _TopArrow;

	[NoToLua]
	public GameObject _leftArrow;

	[NoToLua]
	public GameObject _rightArrow;

	private RectTransform _myRect;

	private LayoutElement _ControlLayout;

	private float _preWidth;

	private float _preHeight;

	[NoToLua]
	public bool bFixedSize;

	private Vector3[] _tempArray;

	[NoToLua]
	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	[NoToLua]
	private void Update()
	{ }

	[NoToLua]
	private void CorrectSetting()
	{ }

	public void SetMaxWidth(float width)
	{ }

	public void SetMaxHeight(float height)
	{ }

	[NoToLua]
	private void SetMinWidth(float width)
	{ }

	[NoToLua]
	private void SetMinHeight(float height)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISizeControl___ctor.c RVA 0x01A0286C
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public UISizeControl()
	{
	}
}
