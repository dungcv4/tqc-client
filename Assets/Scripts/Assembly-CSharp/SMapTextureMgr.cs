using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class SMapTextureMgr
{
	public delegate void OnFinished(Image __img);

	private Image _img;

	private OnFinished _onFinished;

	private static SMapTextureMgr _instance;

	public static SMapTextureMgr Instance
	{
		get
		{ return default; }
	}

	public SMapTextureMgr()
	{ }

	public Sprite GetSMapSprite(string smapName)
	{ return default; }
}
