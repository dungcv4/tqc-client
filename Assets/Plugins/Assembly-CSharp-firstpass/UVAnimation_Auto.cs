using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class UVAnimation_Auto : UVAnimation
{
	public Vector2 start;

	public Vector2 pixelsToNextColumnAndRow;

	public int cols;

	public int rows;

	public int totalCells;

	public UVAnimation_Auto()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Auto(UVAnimation_Auto anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public new UVAnimation_Auto Clone()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SPRITE_FRAME[] BuildUVAnim(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
