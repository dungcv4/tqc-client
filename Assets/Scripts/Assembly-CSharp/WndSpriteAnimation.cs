using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Sprite Animation")]
public class WndSpriteAnimation : WndAnimation
{
	[SerializeField]
	private float _fps;

	[SerializeField]
	private string _prefixName;

	[SerializeField]
	private Image _uiSprite;

	[SerializeField]
	private string _atlasName;

	private List<WndFormSpriteData> _listSprite;

	private int _curFrame;

	public float fps
	{
		get
		{ return default; }
		set
		{ }
	}

	public string prefixName
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

	private void Start()
	{ }

	private void Update()
	{ }

	private void InitAnimation()
	{ }

	public override void PlayAnimation()
	{ }

	public WndSpriteAnimation()
	{ }
}
