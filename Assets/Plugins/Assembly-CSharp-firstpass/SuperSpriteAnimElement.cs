using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class SuperSpriteAnimElement
{
	public AutoSpriteBase sprite;

	public string animName;

	[HideInInspector]
	public UVAnimation anim;

	public void Init()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Play()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayInReverse()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SuperSpriteAnimElement()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
