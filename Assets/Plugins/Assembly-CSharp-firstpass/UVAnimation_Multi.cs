using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class UVAnimation_Multi
{
	public string name;

	public int loopCycles;

	public bool loopReverse;

	public float framerate;

	public UVAnimation.ANIM_END_ACTION onAnimEnd;

	public UVAnimation_Auto[] clips;

	[HideInInspector]
	public int index;

	protected int curClip;

	protected int stepDir;

	protected int numLoops;

	protected float duration;

	protected bool ret;

	protected int i;

	protected int framePos;

	public int StepDirection
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

	public UVAnimation_Multi()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Multi(UVAnimation_Multi anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Multi Clone()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Auto GetCurrentClip()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Auto[] BuildUVAnim(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool GetNextFrame(ref SPRITE_FRAME nextFrame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SPRITE_FRAME GetCurrentFrame()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void AppendAnim(int index, SPRITE_FRAME[] anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void AppendClip(UVAnimation clip)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayInReverse()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnim(int index, SPRITE_FRAME[] frames)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Reset()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPosition(float pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnimPosition(float pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void CalcDuration()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public float GetDuration()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public int GetFrameCount()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public int GetCurPosition()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public int GetCurClipNum()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetCurClipNum(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
