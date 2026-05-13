using System;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Effects/Transform Animation")]
public class WndTransformAnimation : WndAnimation
{
	[Serializable]
	public class Node
	{
		[SerializeField]
		private Vector3 _position;

		[SerializeField]
		private Vector3 _scale;

		[SerializeField]
		private Quaternion _rotation;

		[SerializeField]
		private float _duration;

		[SerializeField]
		private bool _useCurve;

		[SerializeField]
		private AnimationCurve _curve;

		public Vector3 position
		{
			get
			{ return default; }
			set
			{ }
		}

		public Vector3 scale
		{
			get
			{ return default; }
			set
			{ }
		}

		public Vector3 eulerAngles
		{
			get
			{ return default; }
			set
			{ }
		}

		public Quaternion rotation
		{
			get
			{ return default; }
		}

		public float duration
		{
			get
			{ return default; }
			set
			{ }
		}

		public bool useCurve
		{
			get
			{ return default; }
			set
			{ }
		}

		public AnimationCurve curve
		{
			get
			{ return default; }
			set
			{ }
		}

		public Node()
		{ }
	}

	[SerializeField]
	private Transform _applyTrans;

	[SerializeField]
	private bool _spherical;

	[SerializeField]
	private Node[] _pathNodes;

	[SerializeField]
	private int _state;

	private int _curFrame;

	private const int ST_Position = 1;

	private const int ST_Rotate = 2;

	private const int ST_Scale = 4;

	public bool spherical
	{
		get
		{ return default; }
		set
		{ }
	}

	public Node[] pathNodes
	{
		get
		{ return default; }
		set
		{ }
	}

	public Transform applyTrans
	{
		get
		{ return default; }
		set
		{ }
	}

	public bool appliedPosition
	{
		get
		{ return default; }
		set
		{ }
	}

	public bool appliedRotate
	{
		get
		{ return default; }
		set
		{ }
	}

	public bool appliedScale
	{
		get
		{ return default; }
		set
		{ }
	}

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	private void InitAnimation()
	{ }

	public override void PlayAnimation()
	{ }

	public override void StopAnimation()
	{ }

	public void StopAnimationAndShowEnd()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WndTransformAnimation___ctor.c RVA 0x01955D7C
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public WndTransformAnimation()
	{
	}
}
