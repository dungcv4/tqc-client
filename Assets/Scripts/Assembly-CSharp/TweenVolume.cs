// Source: Ghidra work/06_ghidra/decompiled_full/TweenVolume/ (all 1-1)
// Field offsets: from@0x80, to@0x84, mSource@0x88 (AudioSource)

using System;
using Cpp2IlInjected;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	[Range(0f, 1f)]
	public float from;

	[Range(0f, 1f)]
	public float to;

	private AudioSource mSource;

	public AudioSource audioSource
	{
		get
		{
			if (mSource == null) mSource = GetComponent<AudioSource>();
			return mSource;
		}
	}

	[Obsolete("Use 'value' instead")]
	public float volume
	{
		get { return value; }
		set { this.value = value; }
	}

	public float value
	{
		get
		{
			if (audioSource == null) return 0f;
			return mSource.volume;
		}
		set
		{
			if (audioSource == null) return;
			mSource.volume = value;
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = (1f - factor) * from + to * factor;
	}

	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume c = UITweener.Begin<TweenVolume>(go, duration);
		if (c == null) throw new System.NullReferenceException();
		c.from = c.value;
		c.to = targetVolume;
		if (duration <= 0f) { c.Sample(1f, true); c.enabled = false; }
		return c;
	}

	public override void SetStartToCurrentValue() { from = value; }
	public override void SetEndToCurrentValue() { to = value; }

	public TweenVolume() { }
}
