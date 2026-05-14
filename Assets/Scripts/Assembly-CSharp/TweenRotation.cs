// Source: Ghidra work/06_ghidra/decompiled_full/TweenRotation/ (all 1-1)
// Source: dump.cs — TweenRotation : UITweener
// Field offsets: from@0x80 (Vector3 EulerDegrees), to@0x8C (Vector3 EulerDegrees),
//   quaternionLerp@0x98 (bool), mTrans@0xA0 (Transform)
// DAT_0091c138 = 0.0174533 (Mathf.Deg2Rad) used to convert Euler degrees → radians for Internal_FromEulerRad

using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	public Vector3 from;

	public Vector3 to;

	public bool quaternionLerp;

	private Transform mTrans;

	// Source: Ghidra get_cachedTransform.c — if mTrans == null → mTrans = transform; return mTrans.
	public Transform cachedTransform
	{
		get
		{
			if (mTrans == null) mTrans = transform;
			return mTrans;
		}
	}

	// Source: Ghidra get_value.c / set_value.c — Quaternion = cachedTransform.localRotation
	public Quaternion value
	{
		get
		{
			if (cachedTransform == null) throw new System.NullReferenceException();
			return cachedTransform.localRotation;
		}
		set
		{
			if (cachedTransform == null) throw new System.NullReferenceException();
			cachedTransform.localRotation = value;
		}
	}

	// Source: Ghidra OnUpdate.c RVA 0x019fbf0c
	// 1-1 logic:
	//   if quaternionLerp:
	//     qFrom = Quaternion.Internal_FromEulerRad(from * Deg2Rad)
	//     qTo   = Quaternion.Internal_FromEulerRad(to   * Deg2Rad)
	//     value = Quaternion.Slerp(qFrom, qTo, factor)
	//   else:
	//     factor = max(factor, 0)
	//     eul = from + (to - from) * factor   (component-wise)
	//     value = Quaternion.Internal_FromEulerRad(eul * Deg2Rad)
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (quaternionLerp)
		{
			Quaternion qFrom = Quaternion.Euler(from);
			Quaternion qTo = Quaternion.Euler(to);
			value = Quaternion.Slerp(qFrom, qTo, factor);
		}
		else
		{
			if (factor < 0f) factor = 0f;
			Vector3 eul = new Vector3(
				from.x + (to.x - from.x) * factor,
				from.y + (to.y - from.y) * factor,
				from.z + (to.z - from.z) * factor);
			value = Quaternion.Euler(eul);
		}
	}

	// Source: Ghidra Begin.c RVA 0x019fc030
	// 1-1: c.from = c.value.eulerAngles; c.to = rot.eulerAngles;
	//   if duration <= 0: c.Sample(1, true); c.enabled = false.
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation c = UITweener.Begin<TweenRotation>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.from = c.value.eulerAngles;
		c.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			c.Sample(1f, true);
			c.enabled = false;
		}
		return c;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue() { from = value.eulerAngles; }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue() { to = value.eulerAngles; }

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart() { value = Quaternion.Euler(from); }

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd() { value = Quaternion.Euler(to); }

	public TweenRotation() { }
}
