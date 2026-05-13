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
	{ }

	public bool Play(bool active, float timeScale = 0f)
	{ return default; }

	// Source: Ghidra work/06_ghidra/decompiled_rva/FxPlayer___ctor.c RVA 0x018F0278
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public FxPlayer()
	{
	}
}
