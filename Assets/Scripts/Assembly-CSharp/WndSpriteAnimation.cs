// Source: Ghidra work/06_ghidra/decompiled_full/WndSpriteAnimation/
// Cycles Image.sprite through atlas sprites matching `_prefixName` at `_fps`.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Sprite Animation")]
public class WndSpriteAnimation : WndAnimation
{
	[SerializeField] private float _fps;
	[SerializeField] private string _prefixName;
	[SerializeField] private Image _uiSprite;
	[SerializeField] private string _atlasName;

	private List<WndFormSpriteData> _listSprite;
	private int _curFrame;

	public float fps { get { return _fps; } set { _fps = value; } }
	public string prefixName { get { return _prefixName; } set { _prefixName = value; } }
	public Image uiSprite { get { return _uiSprite; } set { _uiSprite = value; } }
	public string atlasName { get { return _atlasName; } set { _atlasName = value; } }

	private void Start()
	{
		if (_curFrame < 0) InitAnimation();
		if (_auto) PlayAnimation();
	}

	private void Update()
	{
		if (!_isPlaying || _listSprite == null || _listSprite.Count == 0 || _uiSprite == null) return;
		_duration += Time.deltaTime;
		float frameTime = (_fps > 0f) ? (1f / _fps) : 0.1f;
		while (_duration >= frameTime)
		{
			_duration -= frameTime;
			_curFrame++;
			if (_curFrame >= _listSprite.Count)
			{
				if (_loop) _curFrame = 0;
				else { _curFrame = _listSprite.Count - 1; _isPlaying = false; break; }
			}
		}
		WndFormSpriteData d = _listSprite[_curFrame];
		if (d != null && d.sprite != null) _uiSprite.sprite = d.sprite;
	}

	// Source: Ghidra InitAnimation.c — fetch atlas; collect sprites whose name starts with _prefixName.
	private void InitAnimation()
	{
		_curFrame = 0;
		_duration = 0f;
		_listSprite = new List<WndFormSpriteData>();
		WndFormAtlas atlas = WndFormUtility.GetAtlas(_atlasName);
		if (atlas == null || atlas.spriteDatas == null) return;
		foreach (var d in atlas.spriteDatas)
		{
			if (d != null && !string.IsNullOrEmpty(d.name) && !string.IsNullOrEmpty(_prefixName) && d.name.StartsWith(_prefixName))
			{
				_listSprite.Add(d);
			}
		}
	}

	public override void PlayAnimation()
	{
		if (_curFrame < 0) InitAnimation();
		if (_isPlaying) return;
		if (_uiSprite == null || _listSprite == null || _listSprite.Count == 0) return;
		_curFrame = 0;
		_isPlaying = true;
		_duration = 0f;
	}

	public WndSpriteAnimation() { _curFrame = -1; }
}
