// Source: Ghidra work/06_ghidra/decompiled_full/StencilMaterialAlpha/ — manages reference-counted stencil-masked material variants.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.Rendering;

public static class StencilMaterialAlpha
{
	private class MatAlphaEntry
	{
		public Material baseMat;
		public Material customMat;
		public int count;
		public int stencilId;
		public StencilOp operation;
		public CompareFunction compareFunction;
		public int readMask;
		public int writeMask;
		public bool useAlphaClip;
		public ColorWriteMask colorMask;
		public int iGrayScaleAmount;
		public MatAlphaEntry() { }
	}

	private static List<MatAlphaEntry> m_ListAlpha;

	public static Material AddAlpha(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask, int iGrayScale)
	{
		if (baseMat == null) return null;
		if (m_ListAlpha == null) m_ListAlpha = new List<MatAlphaEntry>();
		foreach (var e in m_ListAlpha)
		{
			if (e.baseMat == baseMat && e.stencilId == stencilID && e.operation == operation
				&& e.compareFunction == compareFunction && e.readMask == readMask && e.writeMask == writeMask
				&& e.colorMask == colorWriteMask && e.iGrayScaleAmount == iGrayScale)
			{
				e.count++;
				return e.customMat;
			}
		}
		var ne = new MatAlphaEntry
		{
			baseMat = baseMat,
			stencilId = stencilID,
			operation = operation,
			compareFunction = compareFunction,
			readMask = readMask,
			writeMask = writeMask,
			colorMask = colorWriteMask,
			iGrayScaleAmount = iGrayScale,
			count = 1,
			customMat = new Material(baseMat) { name = baseMat.name + "(Stencil)" },
		};
		ne.customMat.SetInt("_Stencil", stencilID);
		ne.customMat.SetInt("_StencilOp", (int)operation);
		ne.customMat.SetInt("_StencilComp", (int)compareFunction);
		ne.customMat.SetInt("_StencilReadMask", readMask);
		ne.customMat.SetInt("_StencilWriteMask", writeMask);
		ne.customMat.SetInt("_ColorMask", (int)colorWriteMask);
		ne.customMat.SetFloat("_GrayScaleAmount", iGrayScale * 0.01f);
		m_ListAlpha.Add(ne);
		return ne.customMat;
	}

	public static void RemoveAlpha(Material customMat)
	{
		if (customMat == null || m_ListAlpha == null) return;
		for (int i = m_ListAlpha.Count - 1; i >= 0; i--)
		{
			if (m_ListAlpha[i].customMat == customMat)
			{
				m_ListAlpha[i].count--;
				if (m_ListAlpha[i].count <= 0)
				{
					Object.Destroy(m_ListAlpha[i].customMat);
					m_ListAlpha.RemoveAt(i);
				}
				return;
			}
		}
	}

	static StencilMaterialAlpha() { m_ListAlpha = new List<MatAlphaEntry>(); }
}
