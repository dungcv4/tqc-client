// Source: Ghidra work/06_ghidra/decompiled_full/TweenPosition/ (10 .c files, all 1-1)
// Source: dump.cs — TweenPosition : UITweener
// Field offsets (from Ghidra):
//   from@0x80 (Vector3: x@80, y@84, z@88), to@0x8C (x@8C, y@90, z@94),
//   worldSpace@0x98 (bool), mTrans@0xA0 (RectTransform), mRect@0xA8 (Graphic)

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	public Vector3 from;

	public Vector3 to;

	[HideInInspector]
	public bool worldSpace;

	private RectTransform mTrans;

	private Graphic mRect;

	// Source: Ghidra get_cachedTransform.c RVA 0x019fb930
	// 1-1: if mTrans == null → mTrans = GetComponent<RectTransform>(); return mTrans.
	public RectTransform cachedTransform
	{
		get
		{
			if (mTrans == null)
			{
				mTrans = GetComponent<RectTransform>();
			}
			return mTrans;
		}
	}

	// Source: Ghidra get_value.c RVA 0x019fb9d8 + set_value.c RVA 0x019fba5c
	// 1-1 getter:
	//   t = cachedTransform; if t == null → NRE
	//   if worldSpace → return t.position
	//   else if mRect != null → return t.anchoredPosition (as Vector3, z=t.localPosition.z)
	//   else → t.localPosition
	//
	// 1-1 setter mirrors the read path.
	public Vector3 value
	{
		get
		{
			RectTransform t = cachedTransform;
			if (t == null) throw new System.NullReferenceException();
			if (worldSpace) return t.position;
			if (mRect != null)
			{
				Vector2 ap = t.anchoredPosition;
				return new Vector3(ap.x, ap.y, t.localPosition.z);
			}
			return t.localPosition;
		}
		set
		{
			RectTransform t = cachedTransform;
			if (t == null) throw new System.NullReferenceException();
			if (mRect == null)
			{
				if (worldSpace) t.position = value;
				else t.localPosition = value;
				return;
			}
			// Has Graphic (UI element): set anchoredPosition (x,y) AND localPosition.z
			t.anchoredPosition = new Vector2(value.x, value.y);
			Vector3 lp = t.localPosition;
			lp.z = value.z;
			t.localPosition = lp;
		}
	}

	// Source: Ghidra Awake.c RVA 0x019fbba8 — mRect = GetComponent<Graphic>();
	private void Awake()
	{
		mRect = GetComponent<Graphic>();
	}

	// Source: Ghidra OnUpdate.c RVA 0x019fbc00
	// 1-1: value = from * (1-factor) + to * factor   (component-wise Vector3 lerp)
	protected override void OnUpdate(float factor, bool isFinished)
	{
		float inv = 1f - factor;
		value = new Vector3(
			from.x * inv + to.x * factor,
			from.y * inv + to.y * factor,
			from.z * inv + to.z * factor);
	}

	// Source: Ghidra Begin.c RVA 0x019fbcf0  (Ghidra signature has 'worldSpace' param)
	// 1-1: c.worldSpace = worldSpace; c.from = c.value; c.to = pos;
	//   if duration <= 0: c.Sample(1, true); c.enabled = false.
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		return Begin(go, duration, pos, false);
	}

	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos, bool worldSpace)
	{
		TweenPosition c = UITweener.Begin<TweenPosition>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.worldSpace = worldSpace;
		c.from = c.value;
		c.to = pos;
		if (duration <= 0f)
		{
			c.Sample(1f, true);
			c.enabled = false;
		}
		return c;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		from = value;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		to = value;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		value = from;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		value = to;
	}

	public TweenPosition()
	{
	}
}
