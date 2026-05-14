// Source: Ghidra work/06_ghidra/decompiled_full/WndProgressAnimation/ — node-based progress (Image.fillAmount) animation.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Progress Animation")]
public class WndProgressAnimation : WndAnimation
{
	[Serializable]
	public class Node
	{
		[SerializeField] private float _fillAmount;
		[SerializeField] private float _duration;

		public float fillAmount { get { return _fillAmount; } set { _fillAmount = value; } }
		public float duration { get { return _duration; } set { _duration = value; } }

		public Node() { }
	}

	[SerializeField] private Image _uiSprite;
	[SerializeField] private Node[] _pathNodes;
	private int _curFrame;

	public Image uiSprite { get { return _uiSprite; } set { _uiSprite = value; } }
	public Node[] pathNodes { get { return _pathNodes; } set { _pathNodes = value; } }

	private void Start()
	{
		if (_curFrame < 0) InitAnimation();
		if (_auto)
		{
			if (_usedCoroutine) StartCoroutine(__CoroutineUpdate());
			else PlayAnimation();
		}
	}

	private void Update()
	{
		if (!_usedCoroutine) __Process(Time.deltaTime);
	}

	private IEnumerator __CoroutineUpdate()
	{
		while (true)
		{
			yield return null;
			__Process(Time.deltaTime);
		}
	}

	private void __Process(float dTime)
	{
		if (!_isPlaying) return;
		_duration += dTime;
		if (_pathNodes == null || _uiSprite == null) return;
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
			if (last != null) _uiSprite.fillAmount = last.fillAmount;
			return;
		}

		int nextFrame = (_curFrame + 1) % n;
		Node a = _pathNodes[_curFrame];
		Node b = _pathNodes[nextFrame];
		if (a == null || b == null) return;
		float t = (a.duration > 0f) ? (_duration / a.duration) : 0f;
		_uiSprite.fillAmount = Mathf.LerpUnclamped(a.fillAmount, b.fillAmount, t);
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
		if (_uiSprite == null || _pathNodes == null || _pathNodes.Length <= 1) return;
		_curFrame = 0;
		_isPlaying = true;
		_duration = _onceDuration;
		_onceDuration = 0f;
	}

	public override void StopAnimation()
	{
		_isPlaying = false;
	}

	public WndProgressAnimation() { _curFrame = -1; }
}
