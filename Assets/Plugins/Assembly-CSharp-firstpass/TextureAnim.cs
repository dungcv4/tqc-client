using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class TextureAnim
{
	public string name;

	public int loopCycles;

	public bool loopReverse;

	public float framerate;

	public UVAnimation.ANIM_END_ACTION onAnimEnd;

	[HideInInspector]
	public string[] framePaths;

	[HideInInspector]
	public string[] frameGUIDs;

	[HideInInspector]
	public CSpriteFrame[] spriteFrames;

	public void Allocate()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public TextureAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public TextureAnim(string n)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Copy(TextureAnim a)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
