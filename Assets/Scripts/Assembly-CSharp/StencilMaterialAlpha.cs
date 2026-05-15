// Source: Ghidra work/06_ghidra/decompiled_full/StencilMaterialAlpha/AddAlpha.c   RVA 0x17C30E8
// Source: Ghidra work/06_ghidra/decompiled_full/StencilMaterialAlpha/RemoveAlpha.c RVA 0x17C3B60
// Source: dump.cs TypeDefIndex 159 (MatAlphaEntry) + 160 (StencilMaterialAlpha)
// Material property names resolved 1-1 from work/03_il2cpp_dump/stringliteral.json:
//   #13453 _Stencil  #13454 _StencilComp  #13455 _StencilOp  #13456 _StencilReadMask
//   #13457 _StencilWriteMask  #13273 _ColorMask  #13294 _EffectAmount  #13491 _UseAlphaClip
//   #10491 = the name-Format string  #7965 "Material "  #217..222 = " doesn't have _X property"
// (Previous port set "_GrayScaleAmount" with *0.01f — chế cháo; the real metadata literal is
//  "_EffectAmount" set as raw (float)iGrayScale. Fixed 1-1 here.)

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class StencilMaterialAlpha
{
	// dump.cs TypeDefIndex 159 — field offsets verified 1-1.
	private class MatAlphaEntry
	{
		public Material baseMat;                  // 0x10
		public Material customMat;                // 0x18
		public int count;                         // 0x20
		public int stencilId;                     // 0x24
		public StencilOp operation;               // 0x28
		public CompareFunction compareFunction;   // 0x2C
		public int readMask;                      // 0x30
		public int writeMask;                     // 0x34
		public bool useAlphaClip;                 // 0x38
		public ColorWriteMask colorMask;          // 0x3C
		public int iGrayScaleAmount;              // 0x40

		// RVA: 0x17C4374
		public MatAlphaEntry() { }
	}

	private static List<MatAlphaEntry> m_ListAlpha;   // 0x0

	// Source: Ghidra AddAlpha.c RVA 0x17C30E8 — 1-1.
	public static Material AddAlpha(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask, int iGrayScale)
	{
		// guard: if ((0 < param_2) || (param_5 != 0xf))
		if (stencilID > 0 || (int)colorWriteMask != 0xf)
		{
			// op_Equality(param_1,0)==0  → baseMat != null
			if (baseMat != null)
			{
				// HasProperty chain (exact order + messages from Ghidra/string literals).
				// On any miss: UJDebug.LogWarning("Material "+name+" doesn't have _X property", (bool)baseMat); return baseMat.
				if (!baseMat.HasProperty("_Stencil"))
				{
					UJDebug.LogWarning(string.Concat("Material ", baseMat.name, " doesn't have _Stencil property"), baseMat != null);
					return baseMat;
				}
				if (!baseMat.HasProperty("_StencilOp"))
				{
					UJDebug.LogWarning(string.Concat("Material ", baseMat.name, " doesn't have _StencilOp property"), baseMat != null);
					return baseMat;
				}
				if (!baseMat.HasProperty("_StencilComp"))
				{
					UJDebug.LogWarning(string.Concat("Material ", baseMat.name, " doesn't have _StencilComp property"), baseMat != null);
					return baseMat;
				}
				if (!baseMat.HasProperty("_StencilReadMask"))
				{
					UJDebug.LogWarning(string.Concat("Material ", baseMat.name, " doesn't have _StencilReadMask property"), baseMat != null);
					return baseMat;
				}
				if (!baseMat.HasProperty("_StencilWriteMask"))
				{
					UJDebug.LogWarning(string.Concat("Material ", baseMat.name, " doesn't have _StencilWriteMask property"), baseMat != null);
					return baseMat;
				}
				if (!baseMat.HasProperty("_ColorMask"))
				{
					UJDebug.LogWarning(string.Concat("Material ", baseMat.name, " doesn't have _ColorMask property"), baseMat != null);
					return baseMat;
				}

				// iVar1 = (param_2 != 9999) ? param_6 : 0   ;  iVar2 = (param_2 != 9999) ? param_2 : 0
				int readMaskEff = (stencilID != 9999) ? readMask : 0;
				int stencilIdEff = (stencilID != 9999) ? stencilID : 0;

				// dedup search (compares baseMat,stencilId,op,comp,readMask,writeMask,colorMask,iGrayScale)
				for (int i = 0; i < m_ListAlpha.Count; i++)
				{
					MatAlphaEntry e = m_ListAlpha[i];
					if (e.baseMat == baseMat
						&& e.stencilId == stencilIdEff
						&& (int)e.operation == (int)operation
						&& (int)e.compareFunction == (int)compareFunction
						&& e.readMask == readMaskEff
						&& e.writeMask == writeMask
						&& (int)e.colorMask == (int)colorWriteMask
						&& e.iGrayScaleAmount == iGrayScale)
					{
						e.count = e.count + 1;
						return e.customMat;
					}
				}

				MatAlphaEntry ne = new MatAlphaEntry();
				ne.count = 1;
				ne.baseMat = baseMat;
				Material nm = new Material(baseMat);
				ne.customMat = nm;
				if (ne.customMat != null)
				{
					ne.customMat.hideFlags = (HideFlags)0x3d;   // HideFlags.HideAndDontSave
					ne.writeMask = writeMask;
					ne.colorMask = colorWriteMask;
					ne.iGrayScaleAmount = iGrayScale;
					ne.compareFunction = compareFunction;
					ne.readMask = readMaskEff;
					ne.stencilId = stencilIdEff;
					ne.operation = operation;
					// *(bool*)(lVar7+0x38) = param_3 != 0 && 0 < param_7
					ne.useAlphaClip = ((int)operation != 0 && writeMask > 0);
					// String literal #10491 — arg order from Ghidra boxed object[9] (plVar8[4..12]).
					ne.customMat.name = string.Format(
						"Stencil Id:{0}, Op:{1}, Comp:{2}, WriteMask:{3}, ReadMask:{4}, ColorMask:{5} AlphaClip:{6} ({7}) GrayScale:{8}",
						stencilIdEff, operation, compareFunction, writeMask, readMaskEff, colorWriteMask, ne.useAlphaClip, baseMat.name, iGrayScale);
					ne.customMat.SetInt("_Stencil", stencilIdEff);
					ne.customMat.SetInt("_StencilOp", (int)operation);
					ne.customMat.SetInt("_StencilComp", (int)compareFunction);
					ne.customMat.SetInt("_StencilReadMask", readMaskEff);
					ne.customMat.SetInt("_StencilWriteMask", writeMask);
					ne.customMat.SetInt("_ColorMask", (int)colorWriteMask);
					ne.customMat.SetInt("_UseAlphaClip", ne.useAlphaClip ? 1 : 0);
					ne.customMat.SetFloat("_EffectAmount", (float)iGrayScale);
					m_ListAlpha.Add(ne);
					return ne.customMat;
				}
			}
		}
		return baseMat;
	}

	// Source: Ghidra RemoveAlpha.c RVA 0x17C3B60 — 1-1.
	public static void RemoveAlpha(Material customMat)
	{
		// op_Equality(param_1,0)&1==0  → customMat != null
		if (customMat != null)
		{
			int i = 0;
			MatAlphaEntry found;
			while (true)
			{
				if (i >= m_ListAlpha.Count)
				{
					return;
				}
				found = m_ListAlpha[i];
				// op_Inequality(entry.customMat, param_1)&1==0  → equal → break (found)
				if (!(found.customMat != customMat))
				{
					break;
				}
				i = i + 1;
			}
			int newCount = found.count - 1;
			found.count = newCount;
			if (newCount == 0)
			{
				Misc.DestroyImmediate(found.customMat);
				found.baseMat = null;
				m_ListAlpha.RemoveAt(i);
				return;
			}
		}
	}

	// RVA: 0x17C4384 — .cctor
	static StencilMaterialAlpha()
	{
		m_ListAlpha = new List<MatAlphaEntry>();
	}
}
