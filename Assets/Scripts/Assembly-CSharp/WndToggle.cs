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
	{ }

	public override void DinitComponent(WndForm wnd)
	{ }

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	public void Toggle(int controlID)
	{ }

	private void ToggleChange(int nTaggle)
	{ }

	public void DontChangeToggle(bool b_change)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WndToggle___ctor.c RVA 0x01960BD4
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public WndToggle()
	{
	}
}
