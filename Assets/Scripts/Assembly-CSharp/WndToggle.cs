// Source: Ghidra work/06_ghidra/decompiled_full/WndToggle/ — manages a set of ButtonController checkboxes
// where exactly one (or zero if _canCancel) is "checked" at a time.

using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Events/Toggle")]
public class WndToggle : IWndComponent
{
	private WndForm _wnd;
	private ButtonController[] _checkBoxs;
	private MethodInfo _method;
	private object[] _methodParams;
	public bool _canCancel;
	public int _toggleControlID;
	public string _methodName;
	private bool doNotChangeToggle;

	public override void InitComponent(WndForm wnd)
	{
		_wnd = wnd;
		_checkBoxs = GetComponentsInChildren<ButtonController>(true);
		if (wnd != null && !string.IsNullOrEmpty(_methodName))
		{
			System.Type wndType = wnd.GetType();
			var types = new System.Type[] { typeof(int) };
			_method = wndType.GetMethod(_methodName, types);
			if (_method != null) _methodParams = new object[1];
		}
	}

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		_method = null;
		_methodParams = null;
		_checkBoxs = null;
	}

	private void Start() { }

	public void Toggle(int controlID)
	{
		if (doNotChangeToggle) return;
		ToggleChange(controlID);
		if (_method != null && _methodParams != null)
		{
			_methodParams[0] = controlID;
			_method.Invoke(_wnd, _methodParams);
		}
	}

	private void ToggleChange(int nToggle)
	{
		if (_checkBoxs == null) return;
		_toggleControlID = nToggle;
		foreach (var btn in _checkBoxs)
		{
			if (btn == null) continue;
			btn.SetButtonChecked(btn._controlID == nToggle);
		}
	}

	public void DontChangeToggle(bool b_change)
	{
		doNotChangeToggle = b_change;
	}

	public WndToggle() { }
}
