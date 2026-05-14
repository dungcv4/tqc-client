// Source: Ghidra work/06_ghidra/decompiled_full/ScrollRectNoDrag/ — ScrollRect that DISABLES user dragging (all 3 drag methods empty).

using Cpp2IlInjected;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectNoDrag : ScrollRect
{
	public override void OnBeginDrag(PointerEventData eventData) { }
	public override void OnDrag(PointerEventData eventData) { }
	public override void OnEndDrag(PointerEventData eventData) { }
	public ScrollRectNoDrag() { }
}
