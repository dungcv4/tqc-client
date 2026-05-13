using Cpp2IlInjected;
using UnityEngine;

public class Electric : MonoBehaviour
{
	[Header("材質")]
	public Material mat;

	[Header("電流要打向的目標")]
	public GameObject target;

	[Header("線段的節點數")]
	public int count;

	[Header("震幅")]
	public float amplifyMin;

	public float amplifyMax;

	[Header("線段寬度")]
	public float startWidth;

	public float endWidth;

	[Header("顏色")]
	public Color startColor;

	public Color endColor;

	private LineRenderer lineRender;

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	private void Lightning()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/Electric___ctor.c RVA 0x015B2AE0
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public Electric()
	{
	}
}
