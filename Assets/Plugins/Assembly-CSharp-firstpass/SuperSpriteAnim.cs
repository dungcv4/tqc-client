using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class SuperSpriteAnim
{
	public enum ANIM_END_ACTION
	{
		Do_Nothing = 0,
		Play_Default_Anim = 1,
		Deactivate = 2,
		Destroy = 3
	}

	public delegate void AnimCompletedDelegate(SuperSpriteAnim anim);

	[HideInInspector]
	public int index;

	public string name;

	public SuperSpriteAnimElement[] spriteAnims;

	public int loopCycles;

	public bool pingPong;

	public ANIM_END_ACTION onAnimEnd;

	public bool deactivateNonPlaying;

	public bool deactivateRecursively;

	protected int curAnim;

	protected int stepDir;

	protected int numLoops;

	protected bool isRunning;

	private AnimCompletedDelegate endDelegate;

	public bool IsRunning
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public SpriteBase CurrentSprite
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public void Init(int idx, AnimCompletedDelegate del, SpriteBase.AnimFrameDelegate frameDel)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnimFrameDelegate(SpriteBase.AnimFrameDelegate frameDel)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private void AnimFinished(SpriteBase sp)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Reset()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Play()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Stop()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Pause()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Unpause()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Hide(bool tf)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool IsHidden()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void HideSprite(SpriteBase sp, bool tf)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Delete()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SuperSpriteAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
