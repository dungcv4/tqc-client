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
		{ return default; }
	}

	public int value
	{
		get
		{ return default; }
		set
		{ }
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{ }

	public static TweenHeight Begin(Graphic widget, float duration, int height)
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

	public TweenHeight()
	{ }
}
