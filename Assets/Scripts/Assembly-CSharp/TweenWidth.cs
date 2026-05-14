// Source: Ghidra work/06_ghidra/decompiled_full/TweenWidth/ (all 1-1)
// Field offsets: from@0x80 (int), to@0x84 (int), mWidget@0x90 (Graphic), mRectTrans@? (unused in body)
// Body uses WndFormExtensions.GetWidth/SetWidth helpers.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	public int from;

	public int to;

	private Graphic mWidget;

	private RectTransform mRectTrans;

	// Source: Ghidra — if mWidget == null lazy-init via GetComponent<Graphic>().
	public Graphic cachedWidget
	{
		get
		{
			if (mWidget == null) mWidget = GetComponent<Graphic>();
			return mWidget;
		}
	}

	// Source: Ghidra get_value.c — return (int)WndFormExtensions.GetWidth(mWidget).
	// set_value.c — WndFormExtensions.SetWidth(mWidget, (float)value).
	public int value
	{
		get { return (int)WndFormExtensions.GetWidth(cachedWidget.rectTransform); }
		set { WndFormExtensions.SetWidth(cachedWidget.rectTransform,(float)value); }
	}

	// Source: Ghidra OnUpdate.c RVA 0x19fd0b0
	// 1-1: f = (1-factor)*from + factor*to; SetWidth(mWidget, Mathf.RoundToInt(f)).
	protected override void OnUpdate(float factor, bool isFinished)
	{
		float f = (1f - factor) * (float)from + (float)to * factor;
		WndFormExtensions.SetWidth(cachedWidget.rectTransform,(float)Mathf.RoundToInt(f));
	}

	// Source: Ghidra Begin.c RVA 0x19fd1bc
	// 1-1: c.cachedWidget = widget; c.from = c.value; c.to = width;
	//   if duration <= 0: c.Sample(1, true); c.enabled = false.
	public static TweenWidth Begin(Graphic widget, float duration, int width)
	{
		if (widget == null) throw new System.NullReferenceException();
		TweenWidth c = UITweener.Begin<TweenWidth>(widget.gameObject, duration);
		if (c == null) throw new System.NullReferenceException();
		c.mWidget = widget;
		c.from = c.value;
		c.to = width;
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

	public TweenWidth() { }
}
