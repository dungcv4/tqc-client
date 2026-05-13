using Cpp2IlInjected;
using UnityEngine;

public class WndSortingLayer : MonoBehaviour
{
	[SerializeField]
	private Renderer[] _renderers;

	[HideInInspector]
	public int layerSel;

	[HideInInspector]
	public string sortingLayer;

	public bool forceResetOnStart;

	public int sortingOrder;

	private void Awake()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	public void Reset(bool getRenderer = true)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WndSortingLayer___ctor.c RVA 0x017CA604
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public WndSortingLayer()
	{
	}
}
