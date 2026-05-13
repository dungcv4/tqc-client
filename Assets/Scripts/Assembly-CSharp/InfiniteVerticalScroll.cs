using Cpp2IlInjected;
using UnityEngine;

public class InfiniteVerticalScroll : InfiniteScroll
{
	protected override float GetSize(RectTransform item)
	{ return default; }

	protected override float GetDimension(Vector2 vector)
	{ return default; }

	protected override Vector2 GetVector(float value)
	{ return default; }

	protected override float GetPos(RectTransform item)
	{ return default; }

	protected override int OneOrMinusOne()
	{ return default; }

	protected override void ResetContentPos(float startPos)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/InfiniteVerticalScroll___ctor.c RVA 0x019FFFE8
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public InfiniteVerticalScroll()
	{
	}
}
