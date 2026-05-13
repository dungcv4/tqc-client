using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class WrdData
{
	private int _levelID;

	private tagmapHEADER _mapHeader;

	private tagmapCODEDATA[] _mapCodeAry;

	public int mapWidth
	{
		get
		{ return default; }
		set
		{ }
	}

	public int mapHeight
	{
		get
		{ return default; }
		set
		{ }
	}

	public tagmapHEADER mapHeader
	{
		get
		{ return default; }
	}

	public tagmapCODEDATA[] mapCodeAry
	{
		get
		{ return default; }
	}

	public int levelID
	{
		get
		{ return default; }
	}

	public WrdData(int levelID)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public WrdData(int levelID, int mapWidth, int mapHeight)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool LoadFile(int levelID)
	{ return default; }

	public override string ToString()
	{ return default; }

	private void clean()
	{ }

	public Vector2 getMapSize()
	{ return default; }

	public void setMapCode(List<uint> mapCode)
	{ }

	public void SetIconData(int x, int y, uint MapCode)
	{ }
}
