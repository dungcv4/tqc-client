using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Events/Touch Controller")]
public class TouchController : IWndComponent, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	private WndForm _wnd;

	public bool _canFocus;

	public bool _autoDrag;

	public WndAudioClip _audioPress;

	public override void InitComponent(WndForm wnd)
	{ }

	public override void DinitComponent(WndForm wnd)
	{ }

	public void OnPointerDown(PointerEventData eventData)
	{ }

	public void OnDrag(PointerEventData eventData)
	{ }

	public TouchController()
	{ }
}
