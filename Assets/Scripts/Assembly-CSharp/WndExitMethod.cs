// Source: Ghidra work/06_ghidra/decompiled_full/WndExitMethod/ (3 .c) — same pattern as WndEnterMethod.

using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Exit Method")]
public class WndExitMethod : IWndComponent, IPointerExitHandler, IEventSystemHandler
{
	public string _methodName;

	public Component _comp;

	private WndForm _wnd;

	private MethodInfo _method;

	private object[] _methodParams;

	public override void InitComponent(WndForm wnd)
	{
		if (wnd == null || string.IsNullOrEmpty(_methodName)) return;
		System.Type wndType = wnd.GetType();
		var types = new System.Type[] { typeof(Component), typeof(PointerEventData) };
		_method = wndType.GetMethod(_methodName, types);
		if (_method != null)
		{
			_wnd = wnd;
			_methodParams = new object[2];
			_methodParams[0] = _comp;
		}
	}

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		_method = null;
		_methodParams = null;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;
		if (_method == null) return;
		if (_methodParams == null) return;
		if (_methodParams.Length < 2) return;
		_methodParams[1] = eventData;
		_method.Invoke(_wnd, _methodParams);
	}

	public WndExitMethod() { }
}
