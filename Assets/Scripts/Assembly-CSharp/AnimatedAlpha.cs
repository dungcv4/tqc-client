// Source: Ghidra work/06_ghidra/decompiled_full/AnimatedAlpha/ — applies alpha to CanvasGroup OR Graphic.color.a.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	[Range(0f, 1f)]
	public float alpha;

	private Graphic mWidget;
	private CanvasRenderer mCr;
	private CanvasGroup mCanvasGroup;

	private void OnEnable()
	{
		mCanvasGroup = GetComponent<CanvasGroup>();
		mWidget = GetComponent<Graphic>();
		mCr = GetComponent<CanvasRenderer>();
	}

	private void LateUpdate()
	{
		if (mCanvasGroup != null) { mCanvasGroup.alpha = alpha; return; }
		if (mWidget != null)
		{
			Color c = mWidget.color;
			c.a = alpha;
			mWidget.color = c;
		}
		else if (mCr != null)
		{
			mCr.SetAlpha(alpha);
		}
	}

	public AnimatedAlpha() { alpha = 1f; }
}
