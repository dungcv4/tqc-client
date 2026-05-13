// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL (IL metadata extraction).
// Bodies: AnalysisFailedException replaced với NotImplementedException + RVA TODO per CLAUDE.md §D6.
// TODO: port từng method body từ work/06_ghidra/decompiled_full/<Class>/<Method>.c

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LuaFileList", menuName = "CustomAsset/LuaFileList")]
public class LuaFileListHolder : ScriptableObject
{
	[Serializable]
	public struct LuaFileDataHolder
	{
		public string data1;

		public byte[] data2;

		public byte[] data3;
	}

	public LuaFileDataHolder[] files;

	public LuaFileListHolder()
	{
		// Source: dump.cs — default empty ctor (ScriptableObject base init only).
	}
}
