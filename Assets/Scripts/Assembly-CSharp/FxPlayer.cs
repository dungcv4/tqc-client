// Source: Ghidra work/06_ghidra/decompiled_full/FxPlayer/ — plays Animator + ParticleSystem at a given pos with timeScale.

using Cpp2IlInjected;
using UnityEngine;

public class FxPlayer : MonoBehaviour
{
	public Animator animator;
	public ParticleSystem particle;
	private float defaultStartLifetimeMultiplier;
	private float defaultStartDelayMultiplier;
	public Vector3 pos;

	public void Init()
	{
		if (particle != null)
		{
			var main = particle.main;
			defaultStartLifetimeMultiplier = main.startLifetimeMultiplier;
			defaultStartDelayMultiplier = main.startDelayMultiplier;
		}
	}

	public bool Play(bool active, float timeScale = 0f)
	{
		gameObject.SetActive(active);
		if (!active) return true;
		transform.position = pos;
		if (animator != null) animator.speed = (timeScale > 0f) ? timeScale : 1f;
		if (particle != null)
		{
			var main = particle.main;
			if (timeScale > 0f)
			{
				main.startLifetimeMultiplier = defaultStartLifetimeMultiplier / timeScale;
				main.startDelayMultiplier = defaultStartDelayMultiplier / timeScale;
			}
			particle.Play(true);
		}
		return true;
	}

	public FxPlayer() { }
}
