// Source: Ghidra work/06_ghidra/decompiled_full/WndSpriteMultiAnimation/
// Cycles through MULTIPLE sprite sequences (one per prefix), with end-of-sequence callbacks.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Sprite multiple Animation")]
public class WndSpriteMultiAnimation : WndAnimation
{
	public delegate void NotifyAnimationChange(object param);

	[SerializeField] private float _fps;
	[SerializeField] private List<string> _prefixNames;
	[SerializeField] private Image _uiSprite;
	[SerializeField] private string _atlasName;
	private List<List<WndFormSpriteData>> _listSprite;
	private int _curFrame;
	private int _curSpriteList;
	private NotifyAnimationChange _callback;
	private object _callbackParam;
	private NotifyAnimationChange _callbackEnd;

	public float fps { get { return _fps; } set { _fps = value; } }
	public List<string> prefixNames { get { return _prefixNames; } set { _prefixNames = value; } }
	public Image uiSprite { get { return _uiSprite; } set { _uiSprite = value; } }
	public string atlasName { get { return _atlasName; } set { _atlasName = value; } }

	public void SetCallBack(NotifyAnimationChange callback, object callbackParam, NotifyAnimationChange callbackEnd)
	{
		_callback = callback;
		_callbackParam = callbackParam;
		_callbackEnd = callbackEnd;
	}

	private void Start()
	{
		if (_curFrame < 0) InitAnimation();
		if (_auto) PlayAnimation();
	}

	private void Update()
	{
		if (!_isPlaying || _listSprite == null || _listSprite.Count == 0 || _uiSprite == null) return;
		if (_curSpriteList >= _listSprite.Count) return;
		List<WndFormSpriteData> seq = _listSprite[_curSpriteList];
		if (seq == null || seq.Count == 0) return;

		_duration += Time.deltaTime;
		float frameTime = (_fps > 0f) ? (1f / _fps) : 0.1f;
		while (_duration >= frameTime)
		{
			_duration -= frameTime;
			_curFrame++;
			if (_curFrame >= seq.Count)
			{
				// Sequence ended — invoke callback, advance to next
				if (_callback != null) _callback(_callbackParam);
				_curFrame = 0;
				_curSpriteList++;
				if (_curSpriteList >= _listSprite.Count)
				{
					if (_loop) _curSpriteList = 0;
					else
					{
						_isPlaying = false;
						if (_callbackEnd != null) _callbackEnd(_callbackParam);
						return;
					}
				}
				seq = _listSprite[_curSpriteList];
				if (seq == null || seq.Count == 0) return;
			}
		}

		WndFormSpriteData d = seq[_curFrame];
		if (d != null && d.sprite != null) _uiSprite.sprite = d.sprite;
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
		_curSpriteList = 0;
		_isPlaying = true;
		_duration = 0f;
	}

	public override void StopAnimation()
	{
		_isPlaying = false;
	}

	public WndSpriteMultiAnimation() { _curFrame = -1; }
}
