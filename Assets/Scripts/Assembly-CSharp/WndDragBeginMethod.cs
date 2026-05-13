using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Drag Begin Method")]
public class WndDragBeginMethod : IWndComponent, IBeginDragHandler, IEventSystemHandler
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

	public void OnBeginDrag(PointerEventData eventData)
	{ }

	public void OnDrag(Vector2 delta)
	{ }

	public WndDragBeginMethod()
	{ }
}
