using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	public Vector3 from;

	public Vector3 to;

	[HideInInspector]
	public bool worldSpace;

	private RectTransform mTrans;

	private Graphic mRect;

	public RectTransform cachedTransform
	{
		get
		{ return default; }
	}

	public Vector3 value
	{
		get
		{ return default; }
		set
		{ }
	}

	private void Awake()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{ }

	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{ return default; }

	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos, bool worldSpace)
	{ return default; }

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{ }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{ }

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{ }

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/TweenPosition___ctor.c RVA 0x019FBE0C
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public TweenPosition()
	{
	}
}
