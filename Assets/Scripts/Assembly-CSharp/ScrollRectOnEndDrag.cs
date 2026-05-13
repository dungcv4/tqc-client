using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/ScrollRectOnEndDrag", 30)]
public class ScrollRectOnEndDrag : ScrollRect
{
	public delegate void OverNormalizeValueEvent();

	[SerializeField]
	public float _NormalizeValue;

	public OverNormalizeValueEvent OnOverFunc;

	public float NormalizeValue
	{
		get
		{ return default; }
		set
		{ }
	}

	public override void OnEndDrag(PointerEventData eventData)
	{ }

	public ScrollRectOnEndDrag()
	{ }
}
