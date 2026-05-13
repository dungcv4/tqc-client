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

	public tageventDATA[] getEveData()
	{ return default; }

	public void setEveData(List<tageventDATA> eventLis)
	{ }

	public void newLevel(int levelID)
	{
		// Source: dump.cs — Ghidra .c not yet extracted (RVA 0x18D00A4).
		// Ghidra loadLevel() invokes this BEFORE BinData.loadFile(). It's expected to allocate
		// a fresh BinData(levelID). Without the actual decompile, we fall back to the (int) ctor
		// which matches dump.cs: `public void .ctor(int levelID)` writes _levelID@0x10.
		// TODO RVA 0x18D00A4 — decompile pending; behavior may include extra dataAry init.
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
