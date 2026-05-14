// Source: Ghidra work/06_ghidra/decompiled_full/WndColorAnimation/ — node-based color animation on Graphic.

using System;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Color Animation")]
public class WndColorAnimation : WndAnimation
{
	[Serializable]
	public class Node
	{
		[SerializeField] private Color _color;
		[SerializeField] private float _duration;

		public Color color { get { return _color; } set { _color = value; } }
		public float duration { get { return _duration; } set { _duration = value; } }

		public Node() { _color = Color.white; }
	}

	[SerializeField] private Graphic _uiWidget;
	[SerializeField] private Node[] _pathNodes;
	private int _curFrame;

	public Node[] pathNodes { get { return _pathNodes; } set { _pathNodes = value; } }
	public Graphic uiWidget { get { return _uiWidget; } set { _uiWidget = value; } }

	private void Start()
	{
		if (_curFrame < 0) InitAnimation();
		if (_auto) PlayAnimation();
	}

	private void Update()
	{
		if (!_isPlaying) return;
		_duration += Time.deltaTime;
		if (_pathNodes == null || _uiWidget == null) return;
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
			_isPlaying = false;
			if (n == 0) return;
			Node last = _pathNodes[n - 1];
			if (last != null) _uiWidget.color = last.color;
			return;
		}

		int nextFrame = (_curFrame + 1) % n;
		Node a = _pathNodes[_curFrame];
		Node b = _pathNodes[nextFrame];
		if (a == null || b == null) return;
		float t = (a.duration > 0f) ? (_duration / a.duration) : 0f;
		_uiWidget.color = Color.LerpUnclamped(a.color, b.color, t);
	}

	private void InitAnimation()
	{
		_curFrame = 0;
		_duration = 0f;
	}

	public override void PlayAnimation()
	{
		if (_curFrame < 0) InitAnimation();
		if (_isPlaying) return;
		if (_uiWidget == null || _pathNodes == null || _pathNodes.Length <= 1) return;
		_curFrame = 0;
		_isPlaying = true;
		_duration = _onceDuration;
		_onceDuration = 0f;
	}

	public override void StopAnimation()
	{
		_isPlaying = false;
	}

	public WndColorAnimation() { _curFrame = -1; }
}
