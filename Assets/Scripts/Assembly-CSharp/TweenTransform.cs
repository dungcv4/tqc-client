// Source: Ghidra work/06_ghidra/decompiled_full/TweenTransform/ (all 1-1)
// Field offsets: from@0x80 (Transform), to@0x88 (Transform), parentWhenFinished@0x90 (bool — not used in OnUpdate),
//   mTrans@0x98 (own Transform), mPos@0xA0 (Vector3), mRot@0xAC (Quaternion), mScale@0xBC (Vector3)

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

	// Source: Ghidra OnUpdate.c RVA 0x019fc738
	// 1-1 flow:
	//   if (to == null) return;
	//   if (mTrans == null) {
	//     mTrans = transform;
	//     mPos = mTrans.position;     // capture start
	//     mRot = mTrans.rotation;
	//     mScale = mTrans.localScale;
	//   }
	//   srcPos = (from != null) ? from.position : mPos;
	//   srcRot = (from != null) ? from.rotation : mRot;
	//   srcScale = (from != null) ? from.localScale : mScale;
	//   mTrans.position   = Vector3.Lerp(srcPos, to.position, factor);
	//   mTrans.rotation   = Quaternion.Slerp(srcRot, to.rotation, factor);
	//   mTrans.localScale = Vector3.Lerp(srcScale, to.localScale, factor);
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (to == null) return;
		if (mTrans == null)
		{
			mTrans = transform;
			if (mTrans == null) throw new System.NullReferenceException();
			mPos = mTrans.position;
			mRot = mTrans.rotation;
			mScale = mTrans.localScale;
		}

		Vector3 srcPos = (from != null) ? from.position : mPos;
		Quaternion srcRot = (from != null) ? from.rotation : mRot;
		Vector3 srcScale = (from != null) ? from.localScale : mScale;

		mTrans.position = Vector3.Lerp(srcPos, to.position, factor);
		mTrans.rotation = Quaternion.Slerp(srcRot, to.rotation, factor);
		mTrans.localScale = Vector3.Lerp(srcScale, to.localScale, factor);
	}

	// Source: Ghidra Begin.c RVA 0x019fcafc — 4-arg overload (go, duration, from, to)
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return Begin(go, duration, null, to);
	}

	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform c = UITweener.Begin<TweenTransform>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.from = from;
		c.to = to;
		if (duration <= 0f) { c.Sample(1f, true); c.enabled = false; }
		return c;
	}

	public TweenTransform() { }
}
