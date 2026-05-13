using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class SuperSprite : MonoBehaviour
{
	public delegate void AnimCompleteDelegate(SuperSprite sprite);

	public bool playDefaultAnimOnStart;

	public int defaultAnim;

	public SuperSpriteAnim[] animations;

	protected SuperSpriteAnim curAnim;

	protected bool animating;

	protected AnimCompleteDelegate animCompleteDelegate;

	protected SpriteBase.AnimFrameDelegate animFrameDelegate;

	protected bool m_started;

	public SpriteRoot CurrentSprite
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(SuperSpriteAnim anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(string anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(SuperSpriteAnim anim)
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

	public void StopAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PauseAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void UnpauseAnim()
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

	public SuperSpriteAnim GetCurAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SuperSpriteAnim GetAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SuperSpriteAnim GetAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool IsAnimating()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void AnimFinished(SuperSpriteAnim anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnimCompleteDelegate(AnimCompleteDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnimFrameDelegate(SpriteBase.AnimFrameDelegate del)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SuperSprite()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
