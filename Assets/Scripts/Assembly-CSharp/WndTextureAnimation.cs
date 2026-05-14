// Source: Ghidra work/06_ghidra/decompiled_full/WndTextureAnimation/
// Frame-by-frame UV rect switching on a RawImage. Source texture treated as horizontal strip
// where each frame's uvRect width = 1/_totalFrames, x offset = (frame % _totalFrames) / _totalFrames.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Texture Animation")]
public class WndTextureAnimation : WndAnimation
{
	[SerializeField] private float _fps;
	[SerializeField] private int _sourceIndex;
	[SerializeField] private int _totalFrames;
	[SerializeField] private RawImage _uiTexture;
	private List<Rect> _listUVRect;
	private int _curFrame;

	public float fps { get { return _fps; } set { _fps = value; } }
	public int sourceIndex { get { return _sourceIndex; } set { _sourceIndex = value; } }
	public int totalFrames { get { return _totalFrames; } set { _totalFrames = value; } }
	public RawImage uiTexture { get { return _uiTexture; } set { _uiTexture = value; } }

	private void Start()
	{
		if (_curFrame < 0) InitAnimation();
		if (_auto) PlayAnimation();
	}

	private void Update()
	{
		if (!_isPlaying || _listUVRect == null || _listUVRect.Count == 0 || _uiTexture == null) return;
		_duration += Time.deltaTime;
		float frameTime = (_fps > 0f) ? (1f / _fps) : 0.1f;
		while (_duration >= frameTime)
		{
			_duration -= frameTime;
			_curFrame++;
			if (_curFrame >= _listUVRect.Count)
			{
				if (_loop) _curFrame = 0;
				else { _curFrame = _listUVRect.Count - 1; _isPlaying = false; break; }
			}
		}
		_uiTexture.uvRect = _listUVRect[_curFrame];
	}

	// Source: Ghidra InitAnimation.c — build _totalFrames UV rects (horizontal strip).
	private void InitAnimation()
	{
		_curFrame = 0;
		_duration = 0f;
		_listUVRect = new List<Rect>();
		if (_totalFrames <= 0) return;
		float w = 1f / (float)_totalFrames;
		for (int i = 0; i < _totalFrames; i++)
		{
			_listUVRect.Add(new Rect((float)i * w, 0f, w, 1f));
		}
	}

	public override void PlayAnimation()
	{
		if (_curFrame < 0) InitAnimation();
		if (_isPlaying) return;
		if (_uiTexture == null || _listUVRect == null || _listUVRect.Count == 0) return;
		_curFrame = 0;
		_isPlaying = true;
		_duration = 0f;
	}

	public WndTextureAnimation() { _curFrame = -1; }
}
