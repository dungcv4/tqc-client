using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	public Transform from;

	public Transform to;

	public bool parentWhenFinished;

	private Transform mTrans;

	private Vector3 mPos;

	private Quaternion mRot;

	private Vector3 mScale;

	protected override void OnUpdate(float factor, bool isFinished)
	{ }

	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{ return default; }

	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{ return default; }

	public TweenTransform()
	{ }
}
