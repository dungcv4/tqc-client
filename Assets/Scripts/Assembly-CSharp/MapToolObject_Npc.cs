using Cpp2IlInjected;
using UnityEngine;

public class MapToolObject_Npc : MapToolObject
{
	public int x;

	public int y;

	public PackedSprite pSprite;

	public PackedSprite pShadow;

	public GameObject pFX;

	public int eveCode;

	private TweenScale tweenScale;

	private GridTile ObjFailGrid;

	private GridTile ObjWallGrid;

	public MapToolObject_Npc(int objID)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void createGridModel(GameObject parent, int x, int y)
	{ }

	public void onMouseClick()
	{ }

	public void destroy()
	{ }

	public void setFocus(bool isFocus)
	{ }
}
