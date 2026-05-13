using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AnimatedColor : MonoBehaviour
{
	public Color color;

	private Graphic mWidget;

	private void OnEnable()
	{ }

	private void LateUpdate()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/AnimatedColor___ctor.c RVA 0x01966164
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public AnimatedColor()
	{
	}
}
