// Source: Ghidra work/06_ghidra/decompiled_full/MapToolObject_Npc/ (4 .c files)
// Editor-tool class for in-editor map building. Only used by MapToolMgr (editor utility).
// Body of createGridModel is large (~200 lines of GameObject building) but only runs in editor map-edit mode.

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

	// Source: Ghidra .ctor.c RVA 0x18d7d20 — System.Object..ctor(this); this.eveCode = objID.
	public MapToolObject_Npc(int objID)
	{
		eveCode = objID;
	}

	// Source: Ghidra createGridModel.c RVA 0x18d9950 — editor-tool body (builds GameObject hierarchy +
	// PackedSprite + shadow + FX based on NPC table lookup). Stub'd in Editor diag — only invoked from
	// MapToolMgr which isn't loaded in the runtime client.
	public void createGridModel(GameObject parent, int x, int y)
	{
		this.x = x;
		this.y = y;
		// Full body deferred — only used by map editor tool, not by gameplay client.
	}

	// Source: Ghidra onMouseClick.c — editor tool feedback.
	public void onMouseClick()
	{
		// Selection feedback in map editor.
	}

	// Source: Ghidra destroy.c — releases pSprite/pShadow/pFX/grids.
	public void destroy()
	{
		if (pFX != null) UnityEngine.Object.Destroy(pFX);
		if (ObjFailGrid != null) UnityEngine.Object.Destroy(ObjFailGrid.gameObject);
		if (ObjWallGrid != null) UnityEngine.Object.Destroy(ObjWallGrid.gameObject);
	}

	// Source: Ghidra setFocus.c — toggles tweenScale.PlayForward/PlayReverse based on isFocus.
	public void setFocus(bool isFocus)
	{
		if (tweenScale == null) return;
		if (isFocus) tweenScale.PlayForward();
		else tweenScale.PlayReverse();
	}
}
