// Source: work/03_il2cpp_dump/dump.cs class 'SPRITE_FRAME' (TypeDefIndex 8208) +
//         work/06_ghidra/decompiled_full/SPRITE_FRAME/*.c bodies.
// Field layout (40 bytes total, matches CSpriteFrame layout):
//   0x00..0x07 uvs.position  (Vector2)
//   0x08..0x0F uvs.size      (Vector2)
//   0x10..0x17 scaleFactor   (Vector2)
//   0x18..0x1F topLeftOffset (Vector2)
//   0x20..0x27 bottomRightOffset (Vector2)
using System;
using UnityEngine;

[Serializable]
public struct SPRITE_FRAME
{
	public Rect uvs;
	public Vector2 scaleFactor;
	public Vector2 topLeftOffset;
	public Vector2 bottomRightOffset;

	// Source: Ghidra _ctor.c RVA 0x01581374
	// 1-1: initialize all 5 Vector2's to (1, 1) via NEON_fmov 4× 1.0f SIMD + Vector2.one statics.
	// Constructor signature takes `float dummy` per dump.cs to allow explicit `new SPRITE_FRAME(0f)`
	// initialization; default field-init pattern requires an arg to disambiguate from default(struct).
	public SPRITE_FRAME(float dummy)
	{
		uvs               = new Rect(1f, 1f, 1f, 1f);
		scaleFactor       = Vector2.one;
		topLeftOffset     = Vector2.one;
		bottomRightOffset = Vector2.one;
	}

	// Source: Ghidra Copy.c RVA 0x01583608
	// 1-1: if (f == null) NRE; copy all 5 Vector2 fields from CSpriteFrame f into this struct.
	public void Copy(CSpriteFrame f)
	{
		if (f == null) throw new System.NullReferenceException();
		uvs               = f.uvs;
		scaleFactor       = f.scaleFactor;
		topLeftOffset     = f.topLeftOffset;
		bottomRightOffset = f.bottomRightOffset;
	}
}
