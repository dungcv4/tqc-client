// Source: Ghidra work/06_ghidra/decompiled_full/TweenColor/ (11 .c files, all 1-1)
// Source: dump.cs — TweenColor : UITweener
// Field offsets (from Ghidra):
//   from@0x80 (Color: r,g,b,a 4 floats), to@0x90 (Color), mCached@0xA0, mWidget@0xA8 (Graphic),
//   mMat@0xB0 (Material), mLight@0xB8 (Light), mSr@0xC0 (SpriteRenderer)
// DAT_0091c22c = 0.01 (light enabled brightness threshold)

using System;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	public Color from;

	public Color to;

	private bool mCached;

	private Graphic mWidget;

	private Material mMat;

	private Light mLight;

	private SpriteRenderer mSr;

	[Obsolete("Use 'value' instead")]
	public Color color
	{
		// Source: Ghidra get_color.c RVA 0x019facb0 / set_color.c — forward to value getter/setter
		get { return value; }
		set { this.value = value; }
	}

	// Source: Ghidra get_value.c RVA 0x019facb4 / set_value.c RVA 0x019fae24
	// Priority chain: mWidget → mMat → mSr → mLight → null/throw
	public Color value
	{
		get
		{
			if (!mCached) Cache();
			if (mWidget != null) return mWidget.color;
			if (mMat != null) return mMat.color;
			if (mSr != null) return mSr.color;
			if (mLight != null) return mLight.color;
			return new Color(0f, 0f, 0f, 0f);
		}
		set
		{
			if (!mCached) Cache();
			if (mWidget != null) { mWidget.color = value; return; }
			if (mMat != null) { mMat.color = value; return; }
			if (mSr != null) { mSr.color = value; return; }
			if (mLight != null)
			{
				mLight.color = value;
				// Source: DAT_0091c22c = 0.01 — enable light only when r+g+b > threshold
				mLight.enabled = (value.r + value.g + value.b) > 0.01f;
				return;
			}
			throw new NullReferenceException();
		}
	}

	// Source: Ghidra Cache.c RVA 0x019faaa0
	// 1-1: mCached = true
	//   mWidget = GetComponent<Graphic>();
	//   if mWidget == null:
	//     mSr = GetComponent<SpriteRenderer>();
	//     if mSr == null:
	//       Renderer r = GetComponent<Renderer>();
	//       if r != null: mMat = r.sharedMaterial;
	//       else:
	//         mLight = GetComponent<Light>();
	//         if mLight == null: mWidget = GetComponentInChildren<Graphic>();
	private void Cache()
	{
		mCached = true;
		mWidget = GetComponent<Graphic>();
		if (mWidget != null) return;

		mSr = GetComponent<SpriteRenderer>();
		if (mSr != null) return;

		Renderer rend = GetComponent<Renderer>();
		if (rend != null)
		{
			mMat = rend.sharedMaterial;
			return;
		}

		mLight = GetComponent<Light>();
		if (mLight == null)
		{
			mWidget = GetComponentInChildren<Graphic>();
		}
	}

	// Source: Ghidra OnUpdate.c RVA 0x019fb028
	// 1-1: clamp factor to [0,1]; value = from + factor * (to - from)  (component-wise Color lerp)
	protected override void OnUpdate(float factor, bool isFinished)
	{
		bool lessZero = factor < 0f;
		if (1f < factor) factor = 1f;
		if (lessZero) factor = 0f;
		Color f = from, t = to;
		value = new Color(
			f.r + (t.r - f.r) * factor,
			f.g + (t.g - f.g) * factor,
			f.b + (t.b - f.b) * factor,
			f.a + (t.a - f.a) * factor);
	}

	// Source: Ghidra Begin.c RVA 0x019fb05c
	// 1-1: var c = UITweener.Begin<TweenColor>(go, duration);
	//   c.from = c.value; c.to = color;
	//   if duration <= 0: c.Sample(1, true); c.enabled = false;
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor c = UITweener.Begin<TweenColor>(go, duration);
		if (c == null) throw new NullReferenceException();
		c.from = c.value;
		c.to = color;
		if (duration <= 0f)
		{
			c.Sample(1f, true);
			c.enabled = false;
		}
		return c;
	}

	// Source: Ghidra SetStartToCurrentValue.c — from = value
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		from = value;
	}

	// Source: Ghidra SetEndToCurrentValue.c — to = value
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		to = value;
	}

	// Source: Ghidra SetCurrentValueToStart.c — value = from
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		value = from;
	}

	// Source: Ghidra SetCurrentValueToEnd.c — value = to
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		value = to;
	}

	public TweenColor()
	{
		from = Color.white;
		to = Color.white;
	}
}
