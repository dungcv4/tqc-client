using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageViewer : ScrollRect
{
	public float maxScale;

	public float minScale;

	public float posDivisor;

	public float scaleDivisor;

	public const int maxHeadIconWidth = 128;

	public const int maxHeadIconHeight = 128;

	public const int maxHeadIconFileSize = 65536;

	private Touch oldTouch1;

	private Touch oldTouch2;

	private int touchNum;

	public override void OnBeginDrag(PointerEventData eventData)
	{ }

	public override void OnDrag(PointerEventData eventData)
	{ }

	private void Update()
	{ }

	public Sprite CutImage()
	{ return default; }

	public void ReleasePreviousHeadIconTexture()
	{ }

	public Rect RectTransformToScreenSpace(RectTransform rt)
	{ return default; }

	// Source: Ghidra work/06_ghidra/decompiled_rva/ImageViewer___ctor.c RVA 0x015B3F40
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public ImageViewer()
	{
	}
}
