// Source: Ghidra work/06_ghidra/decompiled_full/TweenHeight/ (all 1-1)
// Same pattern as TweenWidth but operates on RectTransform height via WndFormExtensions.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	public int from;

	public int to;

	public bool updateTable;

	private Graphic mWidget;

	private RectTransform mRectTrans;

	public Graphic cachedWidget
	{
		get
		{
			if (mWidget == null) mWidget = GetComponent<Graphic>();
			return mWidget;
		}
	}

	public int value
	{
		get { return (int)WndFormExtensions.GetHeight(cachedWidget.rectTransform); }
		set { WndFormExtensions.SetHeight(cachedWidget.rectTransform, (float)value); }
	}

	// Source: Ghidra OnUpdate.c — same lerp + round as TweenWidth.
	protected override void OnUpdate(float factor, bool isFinished)
	{
		float f = (1f - factor) * (float)from + (float)to * factor;
		WndFormExtensions.SetHeight(cachedWidget.rectTransform, (float)Mathf.RoundToInt(f));
	}

	public static TweenHeight Begin(Graphic widget, float duration, int height)
	{
		if (widget == null) throw new System.NullReferenceException();
		TweenHeight c = UITweener.Begin<TweenHeight>(widget.gameObject, duration);
		if (c == null) throw new System.NullReferenceException();
		c.mWidget = widget;
		c.from = c.value;
		c.to = height;
		if (duration <= 0f)
		{
			c.Sample(1f, true);
			c.enabled = false;
		}
		return c;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue() { from = value; }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue() { to = value; }

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart() { value = from; }

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd() { value = to; }

	public TweenHeight() { }
}
