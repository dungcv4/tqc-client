using System.Collections.Generic;
using Cpp2IlInjected;

public class WrdFileMgr
{
	private static WrdFileMgr _instance;

	private WrdData _wrdData;

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr__get_Instance.c RVA 0x18D78E8
	// Lazy singleton: if _instance == null, allocate new WrdFileMgr() and store.
	public static WrdFileMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new WrdFileMgr();
			}
			return _instance;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr__loadLevel.c RVA 0x18D7970
	// 1. Allocate new WrdData (System_Object_ctor + write levelID@0x10).
	// 2. Assign to this._wrdData@0x10.
	// 3. Call _wrdData.LoadFile(levelID); log warning on failure.
	public bool loadLevel(int levelID)
	{
		// Ghidra allocates WrdData via thunk_FUN_01560214 then calls System_Object___ctor and writes
		// levelID directly at offset 0x10. C# equivalent is `new WrdData(levelID)` — the (int) ctor
		// stores into _levelID@0x10 per dump.cs. Original IL skips WrdData's int ctor and uses raw
		// alloc + Object.ctor; in C# we use the typed ctor for the same effect.
		_wrdData = new WrdData(levelID);
		if (_wrdData == null) throw new System.NullReferenceException();
		bool ok = _wrdData.LoadFile(levelID);
		if (!ok)
		{
			UJDebug.LogWarning("WrdFileMgr load level fail: " + levelID.ToString());
		}
		return ok;
	}

	public void clean()
	{ }

	public void newLevel(int levelID, int mapWidth, int mapHeight)
	{ }

	public void printData()
	{ }

	public void printToFile()
	{ }

	public void writeToFile()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr__getMapWidth.c RVA 0x18D7A7C
	// Returns _wrdData == null ? 0 : _wrdData._mapHeader.mapWidth (@0x20 within tagmapHEADER).
	public int getMapWidth()
	{
		if (_wrdData == null) return 0;
		tagmapHEADER hdr = _wrdData.mapHeader;
		if (hdr == null) throw new System.NullReferenceException();
		return hdr.mapWidth;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr__getMapHeight.c RVA 0x18D7AA8
	// Returns _wrdData == null ? 0 : _wrdData._mapHeader.mapHeight (@0x24 within tagmapHEADER).
	public int getMapHeight()
	{
		if (_wrdData == null) return 0;
		tagmapHEADER hdr = _wrdData.mapHeader;
		if (hdr == null) throw new System.NullReferenceException();
		return hdr.mapHeight;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr__getMapCode.c RVA 0x18D7AD4
	// Returns _wrdData != null ? _wrdData._mapCodeAry : new tagmapCODEDATA[0]
	public tagmapCODEDATA[] getMapCode()
	{
		if (_wrdData != null) return _wrdData.mapCodeAry;
		return new tagmapCODEDATA[0];
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr__getMapBlock.c RVA 0x18E2CAC
	// Builds a List<int> by iterating _wrdData._mapCodeAry. For each entry whose mapCode == -1
	// (sentinel for "block"), adds the index to the list. Then converts list to int[].
	// Note: Ghidra emits inlined List<T> add (resize check + raw array write). We use C# idioms.
	public int[] getMapBlock()
	{
		List<int> blocks = new List<int>();
		if (_wrdData == null)
		{
			// Ghidra falls through to FUN_015cb8fc null-deref trap when _wrdData == null
			throw new System.NullReferenceException();
		}
		tagmapCODEDATA[] codeAry = _wrdData.mapCodeAry;
		if (codeAry == null)
		{
			// Empty result if codeAry missing — Ghidra breaks the loop and returns the
			// freshly converted (empty) array via System_Collections_Generic_List__ToArray path.
			return blocks.ToArray();
		}
		for (int i = 0; i < codeAry.Length; i++)
		{
			tagmapCODEDATA entry = codeAry[i];
			if (entry == null) break;
			// Ghidra reads *(int*)(entry + 0x10) == -1 → entry.mapCode (uint @0x10) cast to int == -1
			if ((int)entry.mapCode == -1)
			{
				blocks.Add(i);
			}
		}
		return blocks.ToArray();
	}

	public void setMapSize(int _width, int _height, List<uint> _mapCode = null)
	{ }

	public void setMapCode(List<uint> _mapCode)
	{ }

	public static uint getServerMapHeight(int levelID)
	{ return default; }

	public void FillIconRectData(int x1, int y1, int x2, int y2, uint MapCode)
	{ }

	public bool GetIconCode(int posX, int posY, out uint code) { code = default; return default; }

	public bool CheckBlockMoveAdjustY(float posX, ref float posY)
	{ return default; }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileMgr___ctor.c RVA 0x18E1BBC
	// Empty body — only calls System_Object___ctor(this, 0) which is implicit base.
	public WrdFileMgr()
	{ }
}
