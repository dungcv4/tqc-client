using System.Collections.Generic;
using Cpp2IlInjected;
using LuaFramework;
using UnityEngine;

public class MapToolMgr : MonoBehaviour
{
	private bool isInit;

	public bool isDrawWall;

	public bool isDrawNpc;

	public bool isEditNpc;

	public int curLevelID;

	public int curWrdID;

	public int curBinID;

	public int curNpcID;

	public MapToolObject_Npc curSelectObj;

	public int mapWidth;

	public int mapHeight;

	public int mouseX;

	public int mouseZ;

	public int gridX;

	public int gridY;

	private MapToolObject_Wall[] mapCodeObjectAry;

	private Dictionary<Vector2, MapToolObject_Npc> mapNpcObjectDic;

	private bool _WarpPoint;

	private bool _ShopPoint;

	private TResource<GameObject> _WarpPointRes;

	private TResource<GameObject> _ShopPointRes;

	private Dictionary<int, List<Vector2>> objMap;

	private MapToolObject_Select[] mapSelectAry;

	private int _initSelectX;

	private int _initSelectY;

	private int _endSelectX;

	private int _endSelectY;

	private static MapToolMgr instance;

	public GameObject mouseTile;

	public GameObject mouseNpcModel;

	private static LuaManager _LuaMgr;

	public TResource<GameObject> WarpPointRes
	{
		get
		{ return default; }
	}

	public TResource<GameObject> ShopPointRes
	{
		get
		{ return default; }
	}

	public static MapToolMgr Instance
	{
		get
		{ return default; }
		set
		{ }
	}

	public static LuaManager LuaMgr
	{
		get
		{ return default; }
	}

	public void StartLuaScriptMgr()
	{ }

	public void Awake()
	{ }

	public void Update()
	{ }

	public void mouseUpdate()
	{ }

	public void BackToLastStep()
	{ }

	public void clearMouseModel()
	{ }

	public void init(int levelID, string strSceneName, int wrdMapID, int binMapID)
	{ }

	public void createMapObject(int x, int y, int objID, bool isWall, bool isSelect = false)
	{ }

	public void ReplaceMapObject(int x, int y, int objID)
	{ }

	public void destroyMapObjectByObjID(int objID)
	{ }

	public void destroyMapObject(int x, int y, int objID, bool isWall, bool isSelect = false)
	{ }

	public void ClearAllSelectObj(bool bReset)
	{ }

	public MapToolObject_Npc getMapObject(int x, int y)
	{ return default; }

	public bool haveMapObject(int x, int y)
	{ return default; }

	public void saveDataToWRD()
	{ }

	public void saveDataToBIN()
	{ }

	public static Vector2 getCenterXYFromGridXY(int gridX, int gridY)
	{ return default; }

	private void InitWarpPoint()
	{ }

	public void CBOnLoadWarpPointInEditor(TResource<GameObject> res)
	{ }

	public void CBOnLoadShopPointInEditor(TResource<GameObject> res)
	{ }

	public void CreateMapObjectFromEveData()
	{ }

	public void SaveLuaFile()
	{ }

	public MapToolMgr()
	{ }
}
