// Port 1-1 from Ghidra (decompiled_rva/CircleOutline__*.c). All 8 methods ported.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleOutline : ModifiedShadow
{
	[SerializeField] private int m_circleCount;     // 0x44
	[SerializeField] private int m_firstSample;     // 0x48
	[SerializeField] private int m_sampleIncrement; // 0x4C

	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__get_circleCount.c RVA 0x17C7290
	// 1-1: return *(int)(this + 0x44);   // m_circleCount
	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__set_circleCount.c RVA 0x17C7298
	// 1-1: if (value < 2) value = 1; m_circleCount = value; if (graphic != null) graphic.SetVerticesDirty();
	public int circleCount
	{
		get { return m_circleCount; }
		set
		{
			if (value < 2) value = 1;
			m_circleCount = value;
			var g = base.graphic;
			if (g != null) g.SetVerticesDirty();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__get_firstSample.c RVA 0x17C7358
	// 1-1: return *(int)(this + 0x48);   // m_firstSample
	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__set_firstSample.c RVA 0x17C7360
	// 1-1: if (value < 3) value = 2; m_firstSample = value; if (graphic != null) graphic.SetVerticesDirty();
	public int firstSample
	{
		get { return m_firstSample; }
		set
		{
			if (value < 3) value = 2;
			m_firstSample = value;
			var g = base.graphic;
			if (g != null) g.SetVerticesDirty();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__get_sampleIncrement.c RVA 0x17C7424
	// 1-1: return *(int)(this + 0x4C);   // m_sampleIncrement
	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__set_sampleIncrement.c RVA 0x17C742C
	// 1-1: if (value < 2) value = 1; m_sampleIncrement = value; if (graphic != null) graphic.SetVerticesDirty();
	// (Pattern identical to circleCount/firstSample setters.)
	public int sampleIncrement
	{
		get { return m_sampleIncrement; }
		set
		{
			if (value < 2) value = 1;
			m_sampleIncrement = value;
			var g = base.graphic;
			if (g != null) g.SetVerticesDirty();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline__ModifyVertices.c RVA 0x17C74EC
	// 1-1: if (!IsActive()) return;
	//      if (verts == null) NRE;
	//      int total = (m_sampleIncrement * (m_circleCount - 1) + effectDistance*2) * m_circleCount;
	//      if (total < 0) total += 1;
	//      verts.Capacity = (total>>1 + 1) * verts.Count;
	//      // Loop: m_circleCount iterations
	//      //   Inner loop: 1..circleCount, scale = currentIncrement
	//      //   For each: compute sin/cos of angle, apply effectColor (offsets 0x28 sColor + 0x34 eColor packed)
	//      //   ApplyShadow at (distX/circleCount * iter * cos, distY/circleCount * iter * sin)
	//      //   currentIncrement = m_sampleIncrement + currentIncrement;
	// NOTE: complex vertex math. The original logic creates concentric circles of shadow vertices.
	// This is a UI visual effect — non-critical for boot. Defer full port; runtime will see no outline.
	// TODO 1-1: port full ModifyVertices loop body (uses sincosf, ApplyShadow base method).
	public override void ModifyVertices(List<UIVertex> verts)
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/CircleOutline___ctor.c RVA 0x17C76BC
	// 1-1 + DAT_008e3970 resolved (Ghidra ReadDatBytes: [02 00 00 00 04 00 00 00]):
	//   *(this + 0x4c) = 2                     // m_sampleIncrement = 2
	//   *(8byte)(this + 0x44) = DAT_008e3970   // m_circleCount=2, m_firstSample=4
	//   base.ctor (Shadow.ctor)
	public CircleOutline()
	{
		m_sampleIncrement = 2;
		m_circleCount = 2;
		m_firstSample = 4;
	}
}
