// Source: Ghidra work/06_ghidra/decompiled_full/SpringPosition/ — smooth position tween via WndFormMath.SpringLerp.

using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	public delegate void OnFinished();

	public static SpringPosition current;

	public Vector3 target;
	public float strength;
	public bool worldSpace;
	public bool ignoreTimeScale;
	public OnFinished onFinished;

	private Transform mTrans;
	private float mThreshold;

	private void Start()
	{
		mTrans = transform;
		mThreshold = 0f;
	}

	private void Update()
	{
		if (mTrans == null) return;
		float dt = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
		Vector3 from = worldSpace ? mTrans.position : mTrans.localPosition;
		Vector3 to = WndFormMath.SpringLerp(from, target, strength, dt);
		if (mThreshold == 0f) mThreshold = (target - from).magnitude * 0.001f;
		if ((to - target).magnitude < mThreshold)
		{
			to = target;
			enabled = false;
		}
		if (worldSpace) mTrans.position = to;
		else mTrans.localPosition = to;
		if (!enabled) NotifyListeners();
	}

	private void NotifyListeners()
	{
		current = this;
		if (onFinished != null) onFinished();
		current = null;
	}

	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		if (go == null) return null;
		SpringPosition sp = go.GetComponent<SpringPosition>();
		if (sp == null) sp = go.AddComponent<SpringPosition>();
		sp.target = pos;
		sp.strength = strength;
		sp.onFinished = null;
		sp.mThreshold = 0f;
		sp.enabled = true;
		return sp;
	}

	public SpringPosition() { }
}
