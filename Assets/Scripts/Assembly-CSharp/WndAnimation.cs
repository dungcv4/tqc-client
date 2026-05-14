// Source: Ghidra work/06_ghidra/decompiled_full/WndAnimation/ — base class for all Wnd*Animation subclasses.
// Properties are simple get/set, isPlaying is virtual.

using Cpp2IlInjected;
using UnityEngine;

public abstract class WndAnimation : MonoBehaviour
{
	[SerializeField]
	protected bool _auto;

	[SerializeField]
	protected bool _loop;

	[SerializeField]
	protected float _onceDuration;

	protected float _duration;

	protected bool _isPlaying;

	[SerializeField]
	protected bool _usedCoroutine;

	public bool auto
	{
		get { return _auto; }
		set { _auto = value; }
	}

	public bool loop
	{
		get { return _loop; }
		set { _loop = value; }
	}

	public float onceDuration
	{
		get { return _onceDuration; }
		set { _onceDuration = value; }
	}

	public float duration
	{
		get { return _duration; }
		set { _duration = value; }
	}

	public virtual bool isPlaying
	{
		get { return _isPlaying; }
	}

	public abstract void PlayAnimation();

	public virtual void StopAnimation()
	{
		_isPlaying = false;
	}

	protected WndAnimation() { }
}
