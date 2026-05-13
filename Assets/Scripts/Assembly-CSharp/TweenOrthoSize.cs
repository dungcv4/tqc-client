using Cpp2IlInjected;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
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

	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{ return default; }

	public override void SetStartToCurrentValue()
	{ }

	public override void SetEndToCurrentValue()
	{ }

	public TweenOrthoSize()
	{ }
}
