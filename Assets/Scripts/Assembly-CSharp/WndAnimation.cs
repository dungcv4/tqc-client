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
		get
		{ return default; }
		set
		{ }
	}

	public bool loop
	{
		get
		{ return default; }
		set
		{ }
	}

	public float onceDuration
	{
		get
		{ return default; }
		set
		{ }
	}

	public float duration
	{
		get
		{ return default; }
		set
		{ }
	}

	public virtual bool isPlaying
	{
		get
		{ return default; }
	}

	public abstract void PlayAnimation();

	public virtual void StopAnimation()
	{ }

	protected WndAnimation()
	{ }
}
