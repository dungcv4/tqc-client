// Source: Ghidra work/06_ghidra/decompiled_full/WndDragMethod/ (3 .c)
// Field offsets: _methodName@0x20, _comp@0x28, _wnd@0x30, _method@0x38, _methodParams@0x40
// String literal #3462 = "BtnDrag_CallBack"
// Note: OnDrag.c is intentionally empty body — original APK has no logic here.

using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Drag Method")]
public class WndDragMethod : IWndComponent, IDragHandler, IEventSystemHandler
{
	public string _methodName;

	public Component _comp;

	private WndForm _wnd;

	private MethodInfo _method;

	private object[] _methodParams;

	// Source: Ghidra InitComponent.c RVA 0x1958d78
	// 1-1: if wnd != null && _methodName non-empty:
	//   if wndType != typeof(WndForm_Lua): types=[Component, Vector2]; len=2; params[0]=_comp
	//   else: GetMethod("BtnDrag_CallBack", types=[string, Component, Vector2]); len=3; params[0]=_methodName, [1]=_comp
	public override void InitComponent(WndForm wnd)
	{
		if (wnd != null && !string.IsNullOrEmpty(_methodName))
		{
			System.Type wndType = wnd.GetType();
			if (wndType != typeof(WndForm_Lua))
			{
				var types = new System.Type[] { typeof(Component), typeof(Vector2) };
				_method = wndType.GetMethod(_methodName, types);
				if (_method != null)
				{
					_wnd = wnd;
					_methodParams = new object[2];
					_methodParams[0] = _comp;
				}
			}
			else
			{
				var types = new System.Type[] { typeof(string), typeof(Component), typeof(Vector2) };
				_method = wndType.GetMethod("BtnDrag_CallBack", types);
				if (_method != null)
				{
					_wnd = wnd;
					_methodParams = new object[3];
					_methodParams[0] = _methodName;
					_methodParams[1] = _comp;
				}
			}
		}
	}

	// Source: Ghidra DinitComponent.c — clear _wnd, _method, _methodParams.
	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		_method = null;
		_methodParams = null;
	}

	// Source: Ghidra OnDrag.c RVA 0x1959454 — body is empty (`return;`).
	public void OnDrag(PointerEventData eventData)
	{
	}

	public void OnDrag(Vector2 delta)
	{
	}

	public WndDragMethod()
	{
	}
}
