using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Sprite multiple Animation")]
public class WndSpriteMultiAnimation : WndAnimation
{
	public delegate void NotifyAnimationChange(object param);

	[SerializeField]
	private float _fps;

	[SerializeField]
	private List<string> _prefixNames;

	[SerializeField]
	private Image _uiSprite;

	[SerializeField]
	private string _atlasName;

	private List<List<WndFormSpriteData>> _listSprite;

	private int _curFrame;

	private int _curSpriteList;

	private NotifyAnimationChange _callback;

	private object _callbackParam;

	private NotifyAnimationChange _callbackEnd;

	public float fps
	{
		get
		{ return default; }
		set
		{ }
	}

	public List<string> prefixNames
	{
		get
		{ return default; }
		set
		{ }
	}

	public Image uiSprite
	{
		get
		{ return default; }
		set
		{ }
	}

	public string atlasName
	{
		get
		{ return default; }
		set
		{ }
	}

	public void SetCallBack(NotifyAnimationChange callback, object callbackParam, NotifyAnimationChange callbackEnd)
	{ }

	private void Start()
	{ }

	private void Update()
	{ }

	private void InitAnimation()
	{ }

	public override void PlayAnimation()
	{ }

	public override void StopAnimation()
	{ }

	public WndSpriteMultiAnimation()
	{ }
}
