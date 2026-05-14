// Source: Ghidra work/06_ghidra/decompiled_full/AnimatedWidget/ — sets RectTransform sizeDelta from serialized width/height.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
	public float width;
	public float height;

	private Graphic mWidget;
	private RectTransform mRectTrans;

	private void OnEnable()
	{
		mWidget = GetComponent<Graphic>();
		mRectTrans = GetComponent<RectTransform>();
	}

	private void LateUpdate()
	{
		if (mRectTrans != null) mRectTrans.sizeDelta = new Vector2(width, height);
	}

	public AnimatedWidget() { }
}
