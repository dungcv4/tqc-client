// Source: Ghidra work/06_ghidra/decompiled_full/WndFormUtility/ (10 .c files)
// Static utility class. Atlas operations route through `atlases` dictionary cache.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public static class WndFormUtility
{
	private static Dictionary<string, WndFormAtlas> atlases;

	// Source: Ghidra FindInParents<object>.c RVA 0x1c7c9e0
	// 1-1: GetComponent<T> on go, walk transform.parent up the chain until found or root reached.
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		T comp = go.GetComponent<T>();
		if (comp != null) return comp;
		Transform t = go.transform;
		while (t != null)
		{
			t = t.parent;
			if (t == null) return null;
			comp = t.gameObject.GetComponent<T>();
			if (comp != null) return comp;
		}
		return null;
	}

	// Source: Ghidra LoadSharedSprite.c RVA 0x1a09a28 — 2-arg overload.
	// 1-1: if !IsNullOrEmpty(spriteName) → new SharedAtlasLoadHandler(callback) → handler.LoadSprite(spriteName).
	// 3-arg overload (RVA 0x1a09988) not decompiled; faithful pattern: pass img to the 2-arg ctor.
	public static void LoadSharedSprite(Image img, string spriteName, SharedAtlasLoadHandler.OnFinished callback)
	{
		if (string.IsNullOrEmpty(spriteName)) return;
		SharedAtlasLoadHandler handler = new SharedAtlasLoadHandler(img, callback);
		handler.LoadSprite(spriteName);
	}

	public static void LoadSharedSprite(string spriteName, SharedAtlasLoadHandler.OnFinished callback)
	{
		if (string.IsNullOrEmpty(spriteName)) return;
		SharedAtlasLoadHandler handler = new SharedAtlasLoadHandler(callback);
		handler.LoadSprite(spriteName);
	}

	// Source: Ghidra LoadCommonTextures.c RVA 0x1a09ab4
	// 1-1: if !IsNullOrEmpty(texName) → new TextureLoadHandler(img, callback). Ghidra body does NOT
	// call handler.Load() — only constructs and returns. Kept faithful.
	public static void LoadCommonTextures(RawImage img, string texName, TextureLoadHandler.OnFinished callback)
	{
		if (string.IsNullOrEmpty(texName)) return;
		new TextureLoadHandler(img, callback);
	}

	// Source: Ghidra LoadSprite.c — atlas lookup + GetSprite by name. Returns Sprite or null.
	public static Sprite LoadSprite(string atlasName, string spriteName)
	{
		WndFormAtlas atlas = GetAtlas(atlasName);
		if (atlas == null) return null;
		return atlas.GetSprite(spriteName);
	}

	// Source: Ghidra GetAtlas.c — lookup `atlases` dictionary by name.
	public static WndFormAtlas GetAtlas(string atlasName)
	{
		if (atlases == null || string.IsNullOrEmpty(atlasName)) return null;
		WndFormAtlas atlas;
		atlases.TryGetValue(atlasName, out atlas);
		return atlas;
	}

	// Source: Ghidra GetFuncName.c — returns obj.GetType().FullName + "." + method.
	public static string GetFuncName(object obj, string method)
	{
		if (obj == null) return method;
		return obj.GetType().FullName + "." + method;
	}

	// Source: Ghidra GetHierarchy.c RVA 0x1a09f8c
	// 1-1: walk transform.parent chain, prepend parent.name + "/" to result.
	public static string GetHierarchy(GameObject obj)
	{
		if (obj == null) return "";
		string result = obj.name;
		Transform t = obj.transform;
		while (t != null)
		{
			Transform p = t.parent;
			if (p == null) return result;
			result = p.name + "/" + result;
			t = p;
		}
		return result;
	}

	// Source: Ghidra SetDirty.c RVA 0x1a0a0dc — empty body (`return;`).
	public static void SetDirty(Object obj)
	{
	}

	// Source: Ghidra ClearAtlas.c — remove single named atlas from cache.
	public static void ClearAtlas(string atlasName)
	{
		if (atlases == null || string.IsNullOrEmpty(atlasName)) return;
		atlases.Remove(atlasName);
	}

	// Source: Ghidra ClearAllAtlas.c — clear cache entirely.
	public static void ClearAllAtlas(string atlasName)
	{
		if (atlases == null) return;
		atlases.Clear();
	}

	static WndFormUtility()
	{
		atlases = new Dictionary<string, WndFormAtlas>();
	}
}
