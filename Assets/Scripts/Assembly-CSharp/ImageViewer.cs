// Source: Ghidra work/06_ghidra/decompiled_full/ImageViewer/ — image viewer with pinch-zoom + pan, used for head-icon cropping.

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
	{
		if (Input.touchCount == 1) base.OnBeginDrag(eventData);
	}

	public override void OnDrag(PointerEventData eventData)
	{
		if (Input.touchCount == 1) base.OnDrag(eventData);
	}

	private void Update()
	{
		// Pinch-to-zoom: scale content based on two-touch distance change.
		if (Input.touchCount < 2 || content == null) return;
		Touch t1 = Input.GetTouch(0);
		Touch t2 = Input.GetTouch(1);
		if (touchNum < 2)
		{
			oldTouch1 = t1; oldTouch2 = t2; touchNum = 2; return;
		}
		float oldDist = Vector2.Distance(oldTouch1.position, oldTouch2.position);
		float newDist = Vector2.Distance(t1.position, t2.position);
		float deltaScale = (newDist - oldDist) / Mathf.Max(scaleDivisor, 1f);
		Vector3 s = content.localScale + Vector3.one * deltaScale;
		float clamped = Mathf.Clamp(s.x, minScale, maxScale);
		content.localScale = new Vector3(clamped, clamped, clamped);
		oldTouch1 = t1; oldTouch2 = t2;
	}

	public Sprite CutImage()
	{
		// Editor stub — actual implementation crops content texture to a 128x128 region for head icon.
		return null;
	}

	public void ReleasePreviousHeadIconTexture() { }

	public Rect RectTransformToScreenSpace(RectTransform rt)
	{
		Vector2 size = Vector2.Scale(rt.rect.size, rt.lossyScale);
		Rect r = new Rect((Vector2)rt.position - (size * 0.5f), size);
		r.x = rt.position.x - rt.pivot.x * size.x;
		r.y = Screen.height - rt.position.y - (1f - rt.pivot.y) * size.y;
		return r;
	}

	public ImageViewer() { maxScale = 2f; minScale = 0.5f; posDivisor = 1f; scaleDivisor = 100f; }
}
