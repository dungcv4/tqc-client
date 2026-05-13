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

		public MatAlphaEntry()
		{ }
	}

	private static List<MatAlphaEntry> m_ListAlpha;

	public static Material AddAlpha(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask, int iGrayScale)
	{ return default; }

	public static void RemoveAlpha(Material customMat)
	{ }

	static StencilMaterialAlpha()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
