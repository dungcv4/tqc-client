// Source: Ghidra work/06_ghidra/decompiled_full/TweenFOV/ (all 1-1)

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
		{
			if (mCam == null) mCam = GetComponent<Camera>();
			return mCam;
		}
	}

	public float value
	{
		get
		{
			if (cachedCamera == null) throw new System.NullReferenceException();
			return cachedCamera.fieldOfView;
		}
		set
		{
			if (cachedCamera == null) throw new System.NullReferenceException();
			cachedCamera.fieldOfView = value;
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = (1f - factor) * from + to * factor;
	}

	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV c = UITweener.Begin<TweenFOV>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.from = c.value;
		c.to = to;
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

	public TweenFOV() { }
}
