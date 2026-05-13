// Source: Ghidra work/06_ghidra/decompiled_full/BoxOutline/ (5 .c) — all methods ported 1-1.
// Source: dump.cs TypeDefIndex 169 — BoxOutline : ModifiedShadow
// Field offsets: m_halfSampleCountX@0x44, m_halfSampleCountY@0x48
// Inherited Shadow fields: m_EffectColor@0x28 (Color), m_EffectDistance@0x38 (Vector2)

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOutline : ModifiedShadow
{
	private const int maxHalfSampleCount = 20;

	[SerializeField]
	[Range(1f, 20f)]
	private int m_halfSampleCountX;  // 0x44

	[Range(1f, 20f)]
	[SerializeField]
	private int m_halfSampleCountY;  // 0x48

	// Source: Ghidra get_halfSampleCountX.c RVA 0x17C6C78
	// Source: Ghidra set_halfSampleCountX.c RVA 0x17C6C80
	// 1-1: clamp value to [1,20]; m_halfSampleCountX = clamped; if graphic != null → graphic.SetVerticesDirty().
	public int halfSampleCountX
	{
		get { return m_halfSampleCountX; }
		set
		{
			if (value > 19) value = 20;
			if (value < 2) value = 1;
			m_halfSampleCountX = value;
			var g = this.graphic;
			if (g != null) g.SetVerticesDirty();
		}
	}

	// Source: Ghidra get_halfSampleCountY.c RVA 0x17C6CFC
	// Source: Ghidra set_halfSampleCountY.c RVA 0x17C6D04 — same shape as X.
	public int halfSampleCountY
	{
		get { return m_halfSampleCountY; }
		set
		{
			if (value > 19) value = 20;
			if (value < 2) value = 1;
			m_halfSampleCountY = value;
			var g = this.graphic;
			if (g != null) g.SetVerticesDirty();
		}
	}

	// Source: Ghidra ModifyVertices.c RVA 0x17C6E20
	// 1-1: if (!IsActive()) return;
	//      verts.Capacity = (hX*2+1) * count * (hY*2+1);   // pre-alloc grid
	//      for (x = -hX; x <= hX; x++)
	//        for (y = -hY; y <= hY; y++)
	//          if (x != 0 || y != 0) {
	//            end = start + origCount;
	//            color = (Color32)effectColor;
	//            ApplyShadow(verts, color, start, end, dx/hX * x, dy/hY * y);
	//            start = end;
	//          }
	public override void ModifyVertices(List<UIVertex> verts)
	{
		if (!this.IsActive()) return;
		if (verts == null) throw new System.NullReferenceException();
		int hX = m_halfSampleCountX;
		int hY = m_halfSampleCountY;
		int origCount = verts.Count;
		verts.Capacity = (hX * 2 + 1) * origCount * (hY * 2 + 1);
		float dx = this.effectDistance.x;
		float dy = this.effectDistance.y;
		float fX = (float)hX;
		float fY = (float)hY;
		int start = 0;
		for (int x = -hX; x <= hX; x++)
		{
			for (int y = -hY; y <= hY; y++)
			{
				if (x == 0 && y == 0) continue;
				int end = start + origCount;
				Color32 color = (Color32)this.effectColor;
				ApplyShadow(verts, color, start, end, (dx / fX) * (float)x, (dy / fY) * (float)y);
				start = end;
			}
		}
	}

	// Source: dump.cs — Ghidra .ctor.c not exported (likely empty body).
	public BoxOutline()
	{
	}
}
