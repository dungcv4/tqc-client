using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Drag End Method")]
public class WndDragEndMethod : IWndComponent, IEndDragHandler, IEventSystemHandler
{
	public string _methodName;

	public Component _comp;

	private WndForm _wnd;

	private MethodInfo _method;

	private object[] _methodParams;

	public override void InitComponent(WndForm wnd)
	{ }

	public override void DinitComponent(WndForm wnd)
	{ }

	public void OnEndDrag(PointerEventData eventData)
	{ }

	public void OnDrag(Vector2 delta)
	{ }

	public WndDragEndMethod()
	{ }
}
