// Source: Ghidra work/06_ghidra/decompiled_full/TweenOrthoSize/ (all 1-1)

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
			return cachedCamera.orthographicSize;
		}
		set
		{
			if (cachedCamera == null) throw new System.NullReferenceException();
			cachedCamera.orthographicSize = value;
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = (1f - factor) * from + to * factor;
	}

	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize c = UITweener.Begin<TweenOrthoSize>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.from = c.value;
		c.to = to;
		if (duration <= 0f) { c.Sample(1f, true); c.enabled = false; }
		return c;
	}

	public override void SetStartToCurrentValue() { from = value; }
	public override void SetEndToCurrentValue() { to = value; }

	public TweenOrthoSize() { }
}
