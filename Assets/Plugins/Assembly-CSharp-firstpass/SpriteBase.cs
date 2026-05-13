using Cpp2IlInjected;
using UnityEngine;

[ExecuteInEditMode]
public abstract class SpriteBase : SpriteRoot, ISpriteAnimatable
{
	public delegate void AnimCompleteDelegate(SpriteBase sprite);

	public delegate void AnimFrameDelegate(SpriteBase sprite, int frame);

	public bool playAnimOnStart;

	[HideInInspector]
	public bool crossfadeFrames;

	public int defaultAnim;

	protected int curAnimIndex;

	protected AnimCompleteDelegate animCompleteDelegate;

	protected AnimFrameDelegate animFrameDelegate;

	protected float timeSinceLastFrame;

	protected float timeBetweenAnimFrames;

	protected float framesToAdvance;

	protected bool animating;

	protected bool currentlyAnimating;

	protected SPRITE_FRAME nextFrameInfo;

	public bool Animating
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public int CurAnimIndex
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	protected override void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Clear()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Delete()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void OnDisable()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void OnEnable()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Copy(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Hide(bool tf)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnimCompleteDelegate(AnimCompleteDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnimFrameDelegate(AnimFrameDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetSpriteResizedDelegate(SpriteResizedDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void AddSpriteResizedDelegate(SpriteResizedDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void RemoveSpriteresizedDelegate(SpriteResizedDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual bool StepAnim(float time)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void PlayAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void PlayAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void PlayAnimInReverse(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void PlayAnimInReverse(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetFramerate(float fps)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PauseAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void StopAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void RevertToStatic()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected abstract void AddToAnimatedList();

	protected abstract void RemoveFromAnimatedList();

	public bool IsAnimating()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected SpriteBase()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
