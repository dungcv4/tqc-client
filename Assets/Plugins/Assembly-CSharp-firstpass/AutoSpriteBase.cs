using Cpp2IlInjected;
using UnityEngine;

public abstract class AutoSpriteBase : SpriteBase, ISpriteAggregator, ISpritePackable
{
	protected Texture2D[] sourceTextures;

	protected CSpriteFrame[] spriteFrames;

	public bool doNotTrimImages;

	[HideInInspector]
	public UVAnimation[] animations;

	protected UVAnimation curAnim;

	private int pauseFrame;

	public abstract TextureAnim[] States { get; set; }

	public virtual CSpriteFrame DefaultFrame
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual TextureAnim DefaultState
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual bool SupportsArbitraryAnimations
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual bool DoNotTrimImages
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

	public Texture2D[] SourceTextures
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public CSpriteFrame[] SpriteFrames
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	GameObject ISpriteAggregator.gameObject
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	GameObject ISpritePackable.gameObject
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPauseFrame(int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public int GetPauseFrame()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Clear()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Setup(float w, float h)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Setup(float w, float h, Material material)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Copy(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void CopyAll(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override bool StepAnim(float time)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void CallAnimCompleteDelegate()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(UVAnimation anim, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(UVAnimation anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(int index, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(string name, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnimInReverse(UVAnimation anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnimInReverse(UVAnimation anim, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnimInReverse(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnimInReverse(int index, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnimInReverse(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnimInReverse(string name, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(UVAnimation anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetCurFrame(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetFrame(UVAnimation anim, int frameNum)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetFrame(string anim, int frameNum)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetFrame(int anim, int frameNum)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void StopAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void UnpauseAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void AddToAnimatedList()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void RemoveFromAnimatedList()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation GetCurAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation GetAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override int GetStateIndex(string stateName)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void SetState(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual Material GetPackedMaterial(out string errString)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected AutoSpriteBase()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
