// Source: Ghidra work/06_ghidra/decompiled_full/TweenSlider/ (all 1-1)

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("NGUI/Tween/Tween Slider")]
public class TweenSlider : UITweener
{
	public float from;

	public float to;

	private Slider mSlider;

	public float value
	{
		get
		{
			if (mSlider == null) throw new System.NullReferenceException();
			return mSlider.value;
		}
		set
		{
			if (mSlider == null) throw new System.NullReferenceException();
			mSlider.value = value;
		}
	}

	private void Awake()
	{
		mSlider = GetComponent<Slider>();
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = (1f - factor) * from + to * factor;
	}

	public static TweenSlider Begin(GameObject go, float duration, float value)
	{
		TweenSlider c = UITweener.Begin<TweenSlider>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.from = c.value;
		c.to = value;
		if (duration <= 0f) { c.Sample(1f, true); c.enabled = false; }
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

	public TweenSlider() { }
}
