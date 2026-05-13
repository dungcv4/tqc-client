using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Field of View")]
[RequireComponent(typeof(Camera))]
public class TweenFOV : UITweener
{
	public float from;

	public float to;

	private Camera mCam;

	public Camera cachedCamera
	{
		get
		{ return default; }
	}

	public float value
	{
		get
		{ return default; }
		set
		{ }
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{ }

	public static TweenFOV Begin(GameObject go, float duration, float to)
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

	public TweenFOV()
	{ }
}
