using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class UVAnimation
{
	public enum ANIM_END_ACTION
	{
		Do_Nothing = 0,
		Revert_To_Static = 1,
		Play_Default_Anim = 2,
		Hide = 3,
		Deactivate = 4,
		Destroy = 5
	}

	// HAND-FIX visibility: dump.cs declares these as protected, but the IL2CPP binary writes them
	// directly from AutoSpriteBase (assembly-mate, not subclass). Promoted to internal so port can
	// preserve raw-offset writes — equivalent runtime behaviour.
	internal SPRITE_FRAME[] frames;

	internal int curFrame;

	internal int stepDir;

	internal int numLoops;

	internal bool playInReverse;

	internal float length;

	public string name;

	public int loopCycles;

	public bool loopReverse;

	[HideInInspector]
	public float framerate;

	[HideInInspector]
	public int index;

	[HideInInspector]
	public ANIM_END_ACTION onAnimEnd;

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

	public UVAnimation(UVAnimation anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation Clone()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Reset()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayInReverse()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetStepDir(int dir)
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

	public SPRITE_FRAME GetFrame(int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SPRITE_FRAME[] BuildUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SPRITE_FRAME[] BuildWrappedUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SPRITE_FRAME[] BuildWrappedUVAnim(Vector2 start, Vector2 cellSize, int totalCells)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnim(SPRITE_FRAME[] anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnim(TextureAnim anim, int idx)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void AppendAnim(SPRITE_FRAME[] anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetCurrentFrame(int f)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPosition(float pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetClipPosition(float pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void CalcLength()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public float GetLength()
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

	public int GetFramesDisplayed()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public int GetCurPosition()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
