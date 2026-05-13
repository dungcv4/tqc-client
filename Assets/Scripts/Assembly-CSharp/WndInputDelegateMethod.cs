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

	public override void InitComponent(WndForm wnd)
	{ }

	public override void DinitComponent(WndForm wnd)
	{ }

	public void OnSubmit()
	{ }

	public void OnChange()
	{ }

	public WndInputDelegateMethod()
	{ }
}
