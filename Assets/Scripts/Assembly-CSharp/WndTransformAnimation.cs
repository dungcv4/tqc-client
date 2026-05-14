// Source: Ghidra work/06_ghidra/decompiled_full/WndTransformAnimation/ (18 .c files, all 1-1)
// Path-node based transform animation. _state is a bitmask (Position=1, Rotate=2, Scale=4).
// Node field offsets: _position@0x10, _scale@0x1C, _rotation@0x28, _duration@0x38, _useCurve@0x3C, _curve@0x40

using System;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Effects/Transform Animation")]
public class WndTransformAnimation : WndAnimation
{
	[Serializable]
	public class Node
	{
		[SerializeField] private Vector3 _position;
		[SerializeField] private Vector3 _scale;
		[SerializeField] private Quaternion _rotation;
		[SerializeField] private float _duration;
		[SerializeField] private bool _useCurve;
		[SerializeField] private AnimationCurve _curve;

		public Vector3 position { get { return _position; } set { _position = value; } }
		public Vector3 scale { get { return _scale; } set { _scale = value; } }
		public Vector3 eulerAngles { get { return _rotation.eulerAngles; } set { _rotation = Quaternion.Euler(value); } }
		public Quaternion rotation { get { return _rotation; } }
		public float duration { get { return _duration; } set { _duration = value; } }
		public bool useCurve { get { return _useCurve; } set { _useCurve = value; } }
		public AnimationCurve curve { get { return _curve; } set { _curve = value; } }

		public Node() { _scale = Vector3.one; _rotation = Quaternion.identity; }
	}

	[SerializeField] private Transform _applyTrans;
	[SerializeField] private bool _spherical;
	[SerializeField] private Node[] _pathNodes;
	[SerializeField] private int _state;
	private int _curFrame;

	private const int ST_Position = 1;
	private const int ST_Rotate = 2;
	private const int ST_Scale = 4;

	public bool spherical { get { return _spherical; } set { _spherical = value; } }
	public Node[] pathNodes { get { return _pathNodes; } set { _pathNodes = value; } }
	public Transform applyTrans { get { return _applyTrans; } set { _applyTrans = value; } }

	public bool appliedPosition
	{
		get { return (_state & ST_Position) != 0; }
		set { if (value) _state |= ST_Position; else _state &= ~ST_Position; }
	}

	public bool appliedRotate
	{
		get { return (_state & ST_Rotate) != 0; }
		set { if (value) _state |= ST_Rotate; else _state &= ~ST_Rotate; }
	}

	public bool appliedScale
	{
		get { return (_state & ST_Scale) != 0; }
		set { if (value) _state |= ST_Scale; else _state &= ~ST_Scale; }
	}

	// Source: Ghidra Start.c — if _auto: PlayAnimation.
	private void Start()
	{
		if (_curFrame < 0) InitAnimation();
		if (_auto) PlayAnimation();
	}

	// Source: Ghidra Update.c RVA 0x1955848
	// 1-1: while playing, advance _duration by deltaTime, walk segments, when current segment
	// exceeds its node._duration → next frame (loop modulo if _loop). At end, snap to final node.
	// In segment: lerp/slerp position/rotation/scale based on _state flags and _spherical.
	private void Update()
	{
		if (!_isPlaying) return;
		_duration += Time.deltaTime;
		if (_pathNodes == null || _applyTrans == null) return;
		int n = _pathNodes.Length;
		while (_curFrame < n)
		{
			Node cur = _pathNodes[_curFrame];
			if (cur == null) return;
			if (_duration < cur.duration) break;
			_duration -= cur.duration;
			_curFrame++;
			if (_loop && n > 0) _curFrame = _curFrame % n;
		}

		if (n - _curFrame < 2 && !_loop)
		{
			// End: snap to final node
			_isPlaying = false;
			if (n == 0) return;
			Node last = _pathNodes[n - 1];
			if (last == null) return;
			if ((_state & ST_Position) != 0) _applyTrans.localPosition = last.position;
			if ((_state & ST_Rotate) != 0) _applyTrans.localRotation = last.rotation;
			if ((_state & ST_Scale) != 0) _applyTrans.localScale = last.scale;
			return;
		}

		// Interpolate current segment
		int nextFrame = (_curFrame + 1) % n;
		Node a = _pathNodes[_curFrame];
		Node b = _pathNodes[nextFrame];
		if (a == null || b == null) return;
		float t = (a.duration > 0f) ? (_duration / a.duration) : 0f;
		if (a.useCurve && a.curve != null) t = a.curve.Evaluate(t);

		if (!_spherical)
		{
			if ((_state & ST_Position) != 0)
			{
				float tc = (t < 0f) ? 0f : t;
				_applyTrans.localPosition = Vector3.LerpUnclamped(a.position, b.position, tc);
			}
			if ((_state & ST_Rotate) != 0)
			{
				_applyTrans.localRotation = Quaternion.Lerp(a.rotation, b.rotation, t);
			}
			if ((_state & ST_Scale) != 0)
			{
				float tc = (t < 0f) ? 0f : t;
				_applyTrans.localScale = Vector3.LerpUnclamped(a.scale, b.scale, tc);
			}
		}
		else
		{
			if ((_state & ST_Position) != 0)
			{
				_applyTrans.localPosition = Vector3.Slerp(a.position, b.position, t);
			}
			if ((_state & ST_Rotate) != 0)
			{
				_applyTrans.localRotation = Quaternion.Slerp(a.rotation, b.rotation, t);
			}
			if ((_state & ST_Scale) != 0)
			{
				_applyTrans.localScale = Vector3.Slerp(a.scale, b.scale, t);
			}
		}
	}

	// Source: Ghidra InitAnimation.c — set _curFrame = 0, _duration = 0.
	private void InitAnimation()
	{
		_curFrame = 0;
		_duration = 0f;
	}

	// Source: Ghidra PlayAnimation.c RVA 0x1955ba4
	public override void PlayAnimation()
	{
		if (_curFrame < 0) InitAnimation();
		if (_isPlaying) return;
		if (_applyTrans == null || _pathNodes == null || _pathNodes.Length <= 1) return;
		_curFrame = 0;
		_isPlaying = true;
		_duration = _onceDuration;
		_onceDuration = 0f;
	}

	public override void StopAnimation()
	{
		_isPlaying = false;
	}

	// Source: Ghidra StopAnimationAndShowEnd.c — stop + snap to final node.
	public void StopAnimationAndShowEnd()
	{
		_isPlaying = false;
		if (_pathNodes == null || _pathNodes.Length == 0 || _applyTrans == null) return;
		Node last = _pathNodes[_pathNodes.Length - 1];
		if (last == null) return;
		if ((_state & ST_Position) != 0) _applyTrans.localPosition = last.position;
		if ((_state & ST_Rotate) != 0) _applyTrans.localRotation = last.rotation;
		if ((_state & ST_Scale) != 0) _applyTrans.localScale = last.scale;
	}

	public WndTransformAnimation()
	{
		_curFrame = -1;
	}
}
