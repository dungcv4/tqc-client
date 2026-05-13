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
		{ return default; }
		set
		{ }
	}

	private void Awake()
	{ }

	protected override void OnUpdate(float factor, bool isFinished)
	{ }

	public static TweenSlider Begin(GameObject go, float duration, float value)
	{ return default; }

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{ }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{ }

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{ }

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{ }

	public TweenSlider()
	{ }
}
