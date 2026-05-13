using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Effects/UIFixedProgressbarController")]
public class UIFixedProgressbarController : MonoBehaviour
{
	public GameObject initProgressObj;

	public string MaxValue;

	public string Value;

	private List<GameObject> ProgressObjects;

	private int nMaxValue;

	private int nValue;

	private bool bActive;

	private void Awake()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	private void Init()
	{ }

	private void SetMaxValue(string value)
	{ }

	public void SetValue(string value)
	{ }

	private void OnValidate()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/UIFixedProgressbarController___ctor.c RVA 0x01A01818
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public UIFixedProgressbarController()
	{
	}
}
