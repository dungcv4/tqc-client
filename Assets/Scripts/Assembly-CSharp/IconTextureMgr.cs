using Cpp2IlInjected;
using UnityEngine;

public class IconTextureMgr
{
	protected static IconTextureMgr _instance;

	public static IconTextureMgr Instance
	{
		get
		{ return default; }
	}

	public IconTextureMgr()
	{ }

	public Sprite GetIconSprite(string iconName, int nMode)
	{ return default; }

	public AssetBundleRequest LoadIconSpriteAsync(string iconName, int nMode)
	{ return default; }

	public Sprite[] GetSpriteWithSubSprites(string iconName)
	{ return default; }
}
