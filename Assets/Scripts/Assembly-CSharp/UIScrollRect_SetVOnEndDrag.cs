using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScrollRect_SetVOnEndDrag : MonoBehaviour, IEndDragHandler, IEventSystemHandler
{
	public ScrollRect _scrollRect;

	public float v_Rate;

	public void OnEndDrag(PointerEventData data)
	{ }

	public UIScrollRect_SetVOnEndDrag()
	{ }
}
