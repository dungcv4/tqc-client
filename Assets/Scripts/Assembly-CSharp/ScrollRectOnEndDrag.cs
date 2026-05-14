// Source: Ghidra work/06_ghidra/decompiled_full/ScrollRectOnEndDrag/ — ScrollRect that fires event when normalized scroll position exceeds threshold.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/ScrollRectOnEndDrag", 30)]
public class ScrollRectOnEndDrag : ScrollRect
{
	public delegate void OverNormalizeValueEvent();

	[SerializeField] public float _NormalizeValue;
	public OverNormalizeValueEvent OnOverFunc;

	public float NormalizeValue
	{
		get { return _NormalizeValue; }
		set { _NormalizeValue = value; }
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		// Fire callback if vertical normalized position is below threshold (pulled past end).
		if (OnOverFunc != null && (verticalNormalizedPosition < _NormalizeValue || horizontalNormalizedPosition > 1f - _NormalizeValue))
		{
			OnOverFunc();
		}
	}

	public ScrollRectOnEndDrag() { }
}
