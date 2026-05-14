// Source: Ghidra work/06_ghidra/decompiled_full/WndDropMethod/ (3 .c)
// String literal #3463 = "BtnDrop_CallBack"

using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Drop Method")]
public class WndDropMethod : IWndComponent, IDropHandler, IEventSystemHandler
{
	public string _methodName;

	public Component _comp;

	private WndForm _wnd;

	private MethodInfo _method;

	private object[] _methodParams;

	public override void InitComponent(WndForm wnd)
	{
		if (wnd != null && !string.IsNullOrEmpty(_methodName))
		{
			System.Type wndType = wnd.GetType();
			if (wndType != typeof(WndForm_Lua))
			{
				var types = new System.Type[] { typeof(Component), typeof(PointerEventData) };
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
				var types = new System.Type[] { typeof(string), typeof(Component), typeof(PointerEventData) };
				_method = wndType.GetMethod("BtnDrop_CallBack", types);
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

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		_method = null;
		_methodParams = null;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;
		if (_method == null) return;
		bool isLuaWnd = (_wnd.GetType() == typeof(WndForm_Lua));
		if (_methodParams == null) return;
		if (!isLuaWnd)
		{
			if (_methodParams.Length < 2) return;
			_methodParams[1] = eventData;
		}
		else
		{
			if (_methodParams.Length < 3) return;
			_methodParams[2] = eventData;
		}
		_method.Invoke(_wnd, _methodParams);
	}

	public void OnDrop(GameObject obj) { }

	public WndDropMethod() { }
}
