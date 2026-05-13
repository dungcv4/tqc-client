using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

// Source: dump.cs TypeDefIndex 737. Field layout @ offsets 0x10/0x18/0x20.
public class WrdData
{
	// dump.cs: private int _levelID; // 0x10
	private int _levelID;

	// dump.cs: private tagmapHEADER _mapHeader; // 0x18
	private tagmapHEADER _mapHeader;

	// dump.cs: private tagmapCODEDATA[] _mapCodeAry; // 0x20
	private tagmapCODEDATA[] _mapCodeAry;

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__get_mapWidth.c RVA 0x18E2C74
	//         Ghidra work/06_ghidra/decompiled_rva/WrdData__set_mapWidth.c RVA 0x18E3B0C
	// get: if _mapHeader@0x18 == 0 → null-deref trap; else return _mapHeader.mapWidth@0x20.
	// set: same null-deref guard then write _mapHeader.mapWidth@0x20.
	public int mapWidth
	{
		get
		{
			if (_mapHeader == null) throw new System.NullReferenceException();
			return _mapHeader.mapWidth;
		}
		set
		{
			if (_mapHeader == null) throw new System.NullReferenceException();
			_mapHeader.mapWidth = value;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__get_mapHeight.c RVA 0x18E2C90
	//         Ghidra work/06_ghidra/decompiled_rva/WrdData__set_mapHeight.c RVA 0x18E3B28
	// Same null-deref guard pattern; reads/writes _mapHeader.mapHeight@0x24.
	public int mapHeight
	{
		get
		{
			if (_mapHeader == null) throw new System.NullReferenceException();
			return _mapHeader.mapHeight;
		}
		set
		{
			if (_mapHeader == null) throw new System.NullReferenceException();
			_mapHeader.mapHeight = value;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__get_mapHeader.c RVA 0x18E3B44
	// Plain field read of _mapHeader@0x18.
	public tagmapHEADER mapHeader
	{
		get { return _mapHeader; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__get_mapCodeAry.c RVA 0x18E3B4C
	// Plain field read of _mapCodeAry@0x20.
	public tagmapCODEDATA[] mapCodeAry
	{
		get { return _mapCodeAry; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__get_levelID.c RVA 0x18E3B54
	// Plain field read of _levelID@0x10.
	public int levelID
	{
		get { return _levelID; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData___ctor_int.c RVA 0x18E1BC4
	// System_Object___ctor(this, 0); then *(int*)(this+0x10) = levelID.
	// No header alloc, no array init. Just writes _levelID and returns.
	public WrdData(int levelID)
	{
		_levelID = levelID;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData___ctor_int_int_int.c RVA 0x18E1D48
	// 1. _levelID = levelID
	// 2. _mapHeader = new tagmapHEADER()
	// 3. _mapHeader.mapWidth = mapWidth, _mapHeader.mapHeight = mapHeight
	// 4. _mapCodeAry = new tagmapCODEDATA[mapWidth * mapHeight] (FUN_015cb754 = il2cpp_array_new)
	// 5. For i in [0, mapWidth*mapHeight): _mapCodeAry[i] = new tagmapCODEDATA() with mapCode = 0.
	//    Ghidra inlines the array store + type-check thunk (thunk_FUN_01560118 = il2cpp_class_is_assignable_from).
	public WrdData(int levelID, int mapWidth, int mapHeight)
	{
		_levelID = levelID;
		_mapHeader = new tagmapHEADER();
		if (_mapHeader == null) throw new System.NullReferenceException();
		_mapHeader.mapWidth = mapWidth;
		_mapHeader.mapHeight = mapHeight;
		uint count = (uint)(mapWidth * mapHeight);
		_mapCodeAry = new tagmapCODEDATA[count];
		if ((int)count > 0)
		{
			for (uint i = 0; i < count; i++)
			{
				tagmapCODEDATA elem = new tagmapCODEDATA();
				elem.mapCode = 0;
				_mapCodeAry[i] = elem;
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__LoadFile.c RVA 0x18E1BEC
	// 1. _levelID = levelID
	// 2. _mapHeader = new tagmapHEADER()
	// 3. _mapCodeAry = WrdFileTool.readWRDFile(levelID, ref _mapHeader)
	// 4. If null: UJDebug.Log("WrdFileMgr LoadFile fail:" + levelID.ToString()); clean();
	// 5. Return _mapCodeAry != null.
	// Note: StringLiteral_12628 at 0x3461418 = "WrdFileMgr LoadFile fail:" (per stringliteral.json 0x3477FE8 alt indirection — value verified by content match).
	public bool LoadFile(int levelID)
	{
		_levelID = levelID;
		_mapHeader = new tagmapHEADER();
		_mapCodeAry = WrdFileTool.readWRDFile(levelID, ref _mapHeader);
		if (_mapCodeAry == null)
		{
			UJDebug.Log("WrdFileMgr LoadFile fail:" + levelID.ToString());
			clean();
		}
		return _mapCodeAry != null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__ToString.c RVA 0x18E3B5C
	// Returns String.Format("class WrdData, levelID:{0},mapWidth:{1}, mapHeight:{2}",
	//                       _levelID, _mapHeader.mapWidth, _mapHeader.mapHeight).
	// StringLiteral_15542 at 0x3461500 = "class WrdData, levelID:{0},mapWidth:{1}, mapHeight:{2}" (verified via stringliteral.json 0x347DAF8).
	// Throws if _mapHeader == null (Ghidra falls through to FUN_015cb8fc trap).
	public override string ToString()
	{
		if (_mapHeader == null) throw new System.NullReferenceException();
		return string.Format("class WrdData, levelID:{0},mapWidth:{1}, mapHeight:{2}",
			_levelID, _mapHeader.mapWidth, _mapHeader.mapHeight);
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__clean.c RVA 0x18E3A9C
	// 1. _levelID = 0
	// 2. _mapHeader = new tagmapHEADER()
	// 3. _mapCodeAry = null
	private void clean()
	{
		_levelID = 0;
		_mapHeader = new tagmapHEADER();
		_mapCodeAry = null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__getMapSize.c RVA 0x18E3C2C
	// Note: Ghidra's signature was decompiled as `float WrdData__getMapSize(long param_1)`
	// (Ghidra mis-recognized the Vector2 return as a single float). dump.cs declares
	// `public Vector2 getMapSize()`. The body Ghidra showed:
	//   if (_mapHeader == 0) trap; else return (float)_mapHeader.mapWidth.
	// Vector2 in IL2CPP is passed via SRet (out-pointer); Ghidra only printed the first
	// field assignment (x = mapWidth). The y field is set right after but the decompile
	// truncated it. Per C# convention with Vector2(width, height):
	//   return new Vector2(_mapHeader.mapWidth, _mapHeader.mapHeight);
	// 1-1 with the dump.cs Vector2 signature; Ghidra body proves the first slot is mapWidth.
	public Vector2 getMapSize()
	{
		if (_mapHeader == null) throw new System.NullReferenceException();
		return new Vector2(_mapHeader.mapWidth, _mapHeader.mapHeight);
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__setMapCode.c RVA 0x18E2E7C
	// 1. Null-check mapCode arg → trap if null.
	// 2. _mapCodeAry = new tagmapCODEDATA[mapCode.Count] (read _size@0x18 of List<uint>).
	// 3. Enumerate mapCode via GetEnumerator/MoveNext.
	// 4. For each uint value at index i:
	//      _mapCodeAry[i] = new tagmapCODEDATA() { mapCode = value }
	// 5. Dispose enumerator.
	public void setMapCode(List<uint> mapCode)
	{
		if (mapCode == null) throw new System.NullReferenceException();
		_mapCodeAry = new tagmapCODEDATA[mapCode.Count];
		uint i = 0;
		using (List<uint>.Enumerator e = mapCode.GetEnumerator())
		{
			while (e.MoveNext())
			{
				uint val = e.Current;
				tagmapCODEDATA elem = new tagmapCODEDATA();
				elem.mapCode = 0;
				if (_mapCodeAry == null) throw new System.NullReferenceException();
				_mapCodeAry[i] = elem;
				if (_mapCodeAry[i] == null) throw new System.NullReferenceException();
				_mapCodeAry[i].mapCode = val;
				i++;
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/WrdData__SetIconData.c RVA 0x18E32DC
	// Guard chain:
	//   if (x < 0) return;
	//   if (_mapHeader == null) trap;
	//   if (y < 0) return;
	//   if (_mapHeader.mapWidth <= x) return;
	//   if (_mapHeader.mapHeight <= y) return;
	//   if (_mapCodeAry == null) trap;
	//   idx = x + _mapHeader.mapWidth * y;
	//   _mapCodeAry[idx].mapCode = MapCode;
	public void SetIconData(int x, int y, uint MapCode)
	{
		if (x < 0) return;
		if (_mapHeader == null) throw new System.NullReferenceException();
		if (y < 0) return;
		if (_mapHeader.mapWidth <= x) return;
		if (_mapHeader.mapHeight <= y) return;
		if (_mapCodeAry == null) throw new System.NullReferenceException();
		int idx = x + _mapHeader.mapWidth * y;
		if (_mapCodeAry[idx] == null) throw new System.NullReferenceException();
		_mapCodeAry[idx].mapCode = MapCode;
	}
}
