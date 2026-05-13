using System.Collections.Generic;
using Cpp2IlInjected;

public class WrdFileMgr
{
	private static WrdFileMgr _instance;

	private WrdData _wrdData;

	public static WrdFileMgr Instance
	{
		get
		{ return default; }
	}

	public bool loadLevel(int levelID)
	{ return default; }

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

	public int getMapWidth()
	{ return default; }

	public int getMapHeight()
	{ return default; }

	public tagmapCODEDATA[] getMapCode()
	{ return default; }

	public int[] getMapBlock()
	{ return default; }

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

	public WrdFileMgr()
	{ }
}
