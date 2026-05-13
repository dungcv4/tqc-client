// Source: dump.cs TypeDefIndex 222 + Ghidra decompiled_full/EmptyGraphic/*.c
// Verified 1-1 from libil2cpp.so. NOT chế cháo.
using UnityEngine;
using UnityEngine.UI;

public class EmptyGraphic : MaskableGraphic
{
	// Source: Ghidra RVA 0x1960c6c — empty body (intentional: suppress base SetMaterialDirty)
	public override void SetMaterialDirty()
	{
	}

	// Source: Ghidra RVA 0x1960c70 — empty body (intentional: suppress base SetVerticesDirty)
	public override void SetVerticesDirty()
	{
	}

	// Source: Ghidra RVA 0x1960c74 — if (vh != null) vh.Clear(); else throw NullReferenceException
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		if (vh == null) throw new System.NullReferenceException();
		vh.Clear();
	}

	// Source: dump.cs RVA 0x1960c8c — default ctor (base Graphic.ctor handles initialization)
	public EmptyGraphic()
	{
	}
}
