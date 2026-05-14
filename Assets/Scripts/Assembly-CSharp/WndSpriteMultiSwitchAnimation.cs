// Source: Ghidra work/06_ghidra/decompiled_full/WndSpriteMultiSwitchAnimation/
// Like WndSpriteMultiAnimation but caller manually picks which list via SetAnimationList(index).

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Sprite multiple switch Animation")]
public class WndSpriteMultiSwitchAnimation : WndAnimation
{
	[SerializeField] private float _fps;
	[SerializeField] private List<string> _prefixNames;
	[SerializeField] private Image _uiSprite;
	[SerializeField] private string _atlasName;
	private List<List<WndFormSpriteData>> _listSprite;
	private int _curFrame;
	private int _curSpriteList;

	public float fps { get { return _fps; } set { _fps = value; } }
	public List<string> prefixNames { get { return _prefixNames; } set { _prefixNames = value; } }
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
		if (_curSpriteList < 0 || _curSpriteList >= _listSprite.Count) return;
		List<WndFormSpriteData> seq = _listSprite[_curSpriteList];
		if (seq == null || seq.Count == 0) return;

		_duration += Time.deltaTime;
		float frameTime = (_fps > 0f) ? (1f / _fps) : 0.1f;
		if (_duration >= frameTime)
		{
			_duration -= frameTime;
			_curFrame++;
			if (_curFrame >= seq.Count)
			{
				if (_loop) _curFrame = 0;
				else { _curFrame = seq.Count - 1; _isPlaying = false; }
			}
			WndFormSpriteData d = seq[_curFrame];
			if (d != null && d.sprite != null) _uiSprite.sprite = d.sprite;
		}
	}

	private void InitAnimation()
	{
		_curFrame = 0;
		_curSpriteList = 0;
		_duration = 0f;
		_listSprite = new List<List<WndFormSpriteData>>();
		WndFormAtlas atlas = WndFormUtility.GetAtlas(_atlasName);
		if (atlas == null || atlas.spriteDatas == null || _prefixNames == null) return;
		foreach (string prefix in _prefixNames)
		{
			var seq = new List<WndFormSpriteData>();
			foreach (var d in atlas.spriteDatas)
			{
				if (d != null && !string.IsNullOrEmpty(d.name) && !string.IsNullOrEmpty(prefix) && d.name.StartsWith(prefix))
				{
					seq.Add(d);
				}
			}
			_listSprite.Add(seq);
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

	public override void StopAnimation()
	{
		_isPlaying = false;
	}

	public bool SetAnimationList(int index)
	{
		if (_listSprite == null || index < 0 || index >= _listSprite.Count) return false;
		_curSpriteList = index;
		_curFrame = 0;
		_duration = 0f;
		return true;
	}

	public WndSpriteMultiSwitchAnimation() { _curFrame = -1; }
}
