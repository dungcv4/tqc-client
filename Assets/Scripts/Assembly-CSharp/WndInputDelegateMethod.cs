// Source: Ghidra work/06_ghidra/decompiled_full/WndInputDelegateMethod/ (5 .c + closures)
// Field offsets:
//   _input@0x20, _submitMethodName@0x28, _changeMethodName@0x30, _wnd@0x38,
//   _submitMethod@0x40, _changeMethod@0x48, _methodParams@0x50

using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Progs/Input Delegate Method")]
public class WndInputDelegateMethod : IWndComponent
{
	public InputField _input;

	public string _submitMethodName;

	public string _changeMethodName;

	private WndForm _wnd;

	private MethodInfo _submitMethod;

	private MethodInfo _changeMethod;

	private object[] _methodParams;

	// Source: Ghidra InitComponent.c RVA 0x195a49c
	// 1-1: lookup BOTH _submitMethodName and _changeMethodName on wnd type with single-arg signature.
	// Hooks _input.onEndEdit (Submit) and _input.onValueChanged (Change) lambdas that call OnSubmit/OnChange.
	public override void InitComponent(WndForm wnd)
	{
		if (wnd == null) return;
		System.Type wndType = wnd.GetType();
		// Submit signature: (string) — InputField.text
		var submitTypes = new System.Type[] { typeof(string) };
		// Change signature: (string) — InputField.text on value change
		var changeTypes = new System.Type[] { typeof(string) };

		if (!string.IsNullOrEmpty(_submitMethodName))
		{
			_submitMethod = wndType.GetMethod(_submitMethodName, submitTypes);
		}
		if (!string.IsNullOrEmpty(_changeMethodName))
		{
			_changeMethod = wndType.GetMethod(_changeMethodName, changeTypes);
		}
		_wnd = wnd;
		_methodParams = new object[1];

		if (_input != null)
		{
			_input.onEndEdit.AddListener(delegate { OnSubmit(); });
			_input.onValueChanged.AddListener(delegate { OnChange(); });
		}
	}

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		_submitMethod = null;
		_changeMethod = null;
		_methodParams = null;
	}

	// Source: Ghidra OnSubmit.c RVA 0x195a8c0
	// 1-1: guards + Invoke(_submitMethod, _wnd, _methodParams).
	public void OnSubmit()
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;
		if (_submitMethod == null) return;
		if (_methodParams != null && _input != null && _methodParams.Length >= 1)
		{
			_methodParams[0] = _input.text;
		}
		_submitMethod.Invoke(_wnd, _methodParams);
	}

	// Source: Ghidra OnChange.c RVA 0x195a924 — same structure, different MethodInfo.
	public void OnChange()
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;
		if (_changeMethod == null) return;
		if (_methodParams != null && _input != null && _methodParams.Length >= 1)
		{
			_methodParams[0] = _input.text;
		}
		_changeMethod.Invoke(_wnd, _methodParams);
	}

	public WndInputDelegateMethod() { }
}
