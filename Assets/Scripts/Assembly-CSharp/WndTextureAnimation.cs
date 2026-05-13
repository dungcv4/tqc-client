using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Texture Animation")]
public class WndTextureAnimation : WndAnimation
{
	[SerializeField]
	private float _fps;

	[SerializeField]
	private int _sourceIndex;

	[SerializeField]
	private int _totalFrames;

	[SerializeField]
	private RawImage _uiTexture;

	private List<Rect> _listUVRect;

	private int _curFrame;

	public float fps
	{
		get
		{ return default; }
		set
		{ }
	}

	public int sourceIndex
	{
		get
		{ return default; }
		set
		{ }
	}

	public int totalFrames
	{
		get
		{ return default; }
		set
		{ }
	}

	public RawImage uiTexture
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

	public WndTextureAnimation()
	{ }
}
