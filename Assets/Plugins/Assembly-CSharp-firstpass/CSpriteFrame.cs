// Source: work/03_il2cpp_dump/dump.cs class 'CSpriteFrame' (TypeDefIndex 8210) +
//         work/06_ghidra/decompiled_full/CSpriteFrame/*.c bodies.
// Field layout verified from Ghidra accessor RVAs:
//   0x10..0x17  uvs.position (Vector2 — first 8 bytes of Rect)
//   0x18..0x1F  uvs.size     (Vector2 — last 8 bytes of Rect)
//   0x20..0x27  scaleFactor  (Vector2)
//   0x28..0x2F  topLeftOffset (Vector2)
//   0x30..0x37  bottomRightOffset (Vector2)
// Total: 40 bytes (matches SPRITE_FRAME struct layout).
using System;
using UnityEngine;

[Serializable]
public class CSpriteFrame
{
	public Rect uvs;
	public Vector2 scaleFactor;
	public Vector2 topLeftOffset;
	public Vector2 bottomRightOffset;

	// Source: Ghidra Copy.c RVA 0x01583680
	// 1-1: copy SPRITE_FRAME struct fields (5 Vector2's) into this CSpriteFrame's fields.
	public void Copy(SPRITE_FRAME f)
	{
		uvs               = f.uvs;
		scaleFactor       = f.scaleFactor;
		topLeftOffset     = f.topLeftOffset;
		bottomRightOffset = f.bottomRightOffset;
	}

	// Source: Ghidra Copy_1.c RVA 0x015836A4
	// 1-1: if (f == null) NRE; copy all 5 Vector2 fields from f into this.
	public void Copy(CSpriteFrame f)
	{
		if (f == null) throw new System.NullReferenceException();
		uvs               = f.uvs;
		scaleFactor       = f.scaleFactor;
		topLeftOffset     = f.topLeftOffset;
		bottomRightOffset = f.bottomRightOffset;
	}

	// Source: Ghidra ToStruct.c RVA 0x015836D8
	// 1-1: build SPRITE_FRAME struct from this CSpriteFrame's fields and return.
	// Ghidra signature uses out-param (param_1) for the struct return; in C# the equivalent
	// is direct value-type return.
	public SPRITE_FRAME ToStruct()
	{
		SPRITE_FRAME f;
		f.uvs               = uvs;
		f.scaleFactor       = scaleFactor;
		f.topLeftOffset     = topLeftOffset;
		f.bottomRightOffset = bottomRightOffset;
		return f;
	}

	// Source: Ghidra _ctor.c RVA 0x015836EC
	// 1-1: initialize all 5 Vector2's to (1, 1) via NEON_fmov 4× 1.0f SIMD + static (1,1) loads.
	// Verified: auVar4 = NEON_fmov(0x3f800000, 4) → 4 floats of 1.0f, split into
	// _0_8_ (1,1) and _8_8_ (1,1). Then _DAT_0091c600 / _UNK_0091c608 / DAT_008e36b0 are
	// Vector2(1,1) constants loaded from rodata (matches SPRITE_FRAME._ctor pattern).
	public CSpriteFrame()
	{
		uvs               = new Rect(1f, 1f, 1f, 1f);
		scaleFactor       = Vector2.one;
		topLeftOffset     = Vector2.one;
		bottomRightOffset = Vector2.one;
	}

	// Source: Ghidra _ctor_1.c RVA 0x01583710
	// 1-1: default-init (same as ctor()) then if (f == null) NRE; copy fields from f.
	public CSpriteFrame(CSpriteFrame f)
	{
		uvs               = new Rect(1f, 1f, 1f, 1f);
		scaleFactor       = Vector2.one;
		topLeftOffset     = Vector2.one;
		bottomRightOffset = Vector2.one;
		if (f == null) throw new System.NullReferenceException();
		uvs               = f.uvs;
		scaleFactor       = f.scaleFactor;
		topLeftOffset     = f.topLeftOffset;
		bottomRightOffset = f.bottomRightOffset;
	}

	// Source: Ghidra _ctor_2.c RVA 0x01583778
	// 1-1: default-init then copy from SPRITE_FRAME struct (no null check — struct can't be null).
	public CSpriteFrame(SPRITE_FRAME f)
	{
		uvs               = new Rect(1f, 1f, 1f, 1f);
		scaleFactor       = Vector2.one;
		topLeftOffset     = Vector2.one;
		bottomRightOffset = Vector2.one;
		uvs               = f.uvs;
		scaleFactor       = f.scaleFactor;
		topLeftOffset     = f.topLeftOffset;
		bottomRightOffset = f.bottomRightOffset;
	}
}
