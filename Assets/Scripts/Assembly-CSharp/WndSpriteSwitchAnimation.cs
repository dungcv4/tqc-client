using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Sprite Switch Animation")]
public class WndSpriteSwitchAnimation : WndAnimation
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
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	private void InitAnimation()
	{ }

	public override void PlayAnimation()
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/WndSpriteSwitchAnimation___ctor.c RVA 0x01954F30
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public WndSpriteSwitchAnimation()
	{
	}
}
