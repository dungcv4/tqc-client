using System.Collections.Generic;
using Cpp2IlInjected;
using LuaInterface;

public class BinFileMgr
{
	private static BinFileMgr _instance;

	private BinData _binData;

	public static BinFileMgr Instance
	{
		get
		{ return default; }
	}

	public BinData binData
	{
		get
		{ return default; }
		set
		{ }
	}

	public tageventDATA[] getEveData()
	{ return default; }

	public void setEveData(List<tageventDATA> eventLis)
	{ }

	public void newLevel(int levelID)
	{ }

	public bool loadLevel(int levelID)
	{ return default; }

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

	public BinFileMgr()
	{ }
}
