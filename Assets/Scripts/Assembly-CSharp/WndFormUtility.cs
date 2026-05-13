using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public static class WndFormUtility
{
	private static Dictionary<string, WndFormAtlas> atlases;

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static void LoadSharedSprite(Image img, string spriteName, SharedAtlasLoadHandler.OnFinished callback = null)
	{ }

	public static void LoadSharedSprite(string spriteName, SharedAtlasLoadHandler.OnFinished callback = null)
	{ }

	public static void LoadCommonTextures(RawImage img, string texName, TextureLoadHandler.OnFinished callback = null)
	{ }

	public static Sprite LoadSprite(string atlasName, string spriteName)
	{ return default; }

	public static WndFormAtlas GetAtlas(string atlasName)
	{ return default; }

	public static string GetFuncName(object obj, string method)
	{ return default; }

	public static string GetHierarchy(GameObject obj)
	{ return default; }

	public static void SetDirty(Object obj)
	{ }

	public static void ClearAtlas(string atlasName)
	{ }

	public static void ClearAllAtlas(string atlasName)
	{ }

	static WndFormUtility()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
