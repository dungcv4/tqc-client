using System.Collections.Generic;
using Cpp2IlInjected;
using LuaInterface;

public class BinFileMgr
{
	private static BinFileMgr _instance;

	private BinData _binData;

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinFileMgr__get_Instance.c RVA 0x18CFDA8
	// Lazy singleton: if _instance == null, allocate new BinFileMgr() and store.
	public static BinFileMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new BinFileMgr();
			}
			return _instance;
		}
	}

	public BinData binData
	{
		get
		{ return _binData; }
		set
		{ _binData = value; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/BinFileMgr/getEveData.c RVA 0x18CFE48
	// 1-1 mapping:
	//   if (_binData != null) return _binData._dataAry;   // *(this+0x10)+0x20
	//   return new tageventDATA[0];                       // empty array fallback
	// Used by WndForm_MainSMap.lua:131 `BinFileMgr.Instance:getEveData()` during setObjObject —
	// previously returned `default` (null) which made Lua's `tageventDATAs.Length` raise
	// "attempt to index local 'tageventDATAs' (a nil value)" and V_Create exploded.
	public tageventDATA[] getEveData()
	{
		if (_binData != null)
		{
			return _binData.dataAry;
		}
		return new tageventDATA[0];
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/BinFileMgr/setEveData.c RVA 0x18CFEA8
	// 1-1 mapping:
	//   if (_binData == null) throw NullReferenceException;
	//   int levelID = _binData._levelID;                   // *(_binData+0x10)
	//   _binData = new BinData(levelID, eventLis);         // ctor(int, List<tageventDATA>)
	public void setEveData(List<tageventDATA> eventLis)
	{
		if (_binData == null) throw new System.NullReferenceException();
		int levelID = _binData.levelID;
		_binData = new BinData(levelID, eventLis);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/BinFileMgr/newLevel.c RVA 0x18D00A4
	// 1-1 mapping:
	//   if (_binData != null) _binData = null;              // explicit null + GC write barrier
	//   _binData = new BinData(levelID);                    // BinData..ctor(int)
	// The explicit null assignment before re-assignment is a Cpp2IL convention; in C# the GC
	// handles the write barrier automatically, so a single `_binData = new BinData(levelID)`
	// is semantically identical.
	public void newLevel(int levelID)
	{
		if (_binData != null)
		{
			_binData = null;
		}
		_binData = new BinData(levelID);
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinFileMgr__loadLevel.c RVA 0x18D01E8
	// 1. Call newLevel(levelID) — allocates _binData.
	// 2. If _binData != null: call _binData.loadFile(); on failure log warning + null _binData.
	public bool loadLevel(int levelID)
	{
		newLevel(levelID);
		if (_binData == null) throw new System.NullReferenceException();
		bool ok = _binData.loadFile();
		if (!ok)
		{
			UJDebug.LogWarning("BinFileMgr load level fail: " + levelID.ToString());
			_binData = null;
		}
		return ok;
	}

	public void printData()
	{ }

	public void printToFile()
	{ }

	public void writeToFile()
	{ }

	public void clean()
	{ }

	[NoToLua]
	public void CheckAllBinData()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinFileMgr___ctor.c RVA 0x18CFE30
	// Empty body — only base Object ctor call.
	public BinFileMgr()
	{ }
}
