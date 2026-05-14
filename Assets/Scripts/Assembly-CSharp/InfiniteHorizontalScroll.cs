// Source: Ghidra work/06_ghidra/decompiled_full/InfiniteHorizontalScroll/ — horizontal axis impl of InfiniteScroll.

using Cpp2IlInjected;
using UnityEngine;

public class InfiniteHorizontalScroll : InfiniteScroll
{
	protected override float GetSize(RectTransform item) { return item.rect.width; }
	protected override float GetDimension(Vector2 vector) { return vector.x; }
	protected override Vector2 GetVector(float value) { return new Vector2(value, 0f); }
	protected override float GetPos(RectTransform item) { return item.anchoredPosition.x; }
	protected override int OneOrMinusOne() { return 1; }
	protected override void ResetContentPos(float startPos)
	{
		if (content != null) content.anchoredPosition = new Vector2(startPos, content.anchoredPosition.y);
	}

	public InfiniteHorizontalScroll() { }
}
