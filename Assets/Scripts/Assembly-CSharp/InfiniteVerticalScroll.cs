// Source: Ghidra work/06_ghidra/decompiled_full/InfiniteVerticalScroll/ — vertical axis impl. Y goes negative downward in UI.

using Cpp2IlInjected;
using UnityEngine;

public class InfiniteVerticalScroll : InfiniteScroll
{
	protected override float GetSize(RectTransform item) { return item.rect.height; }
	protected override float GetDimension(Vector2 vector) { return vector.y; }
	protected override Vector2 GetVector(float value) { return new Vector2(0f, value); }
	protected override float GetPos(RectTransform item) { return item.anchoredPosition.y; }
	protected override int OneOrMinusOne() { return -1; }
	protected override void ResetContentPos(float startPos)
	{
		if (content != null) content.anchoredPosition = new Vector2(content.anchoredPosition.x, startPos);
	}

	public InfiniteVerticalScroll() { }
}
