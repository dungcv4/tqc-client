// Source: Ghidra work/06_ghidra/decompiled_full/WndFormAtlas/ (5 .c files)
// Field offsets: spriteDatas@0x20 (List<WndFormSpriteData>), spriteDataIndices@0x28 (Dictionary<string,int>)

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class WndFormAtlas : MonoBehaviour
{
	public List<WndFormSpriteData> spriteDatas;

	public Dictionary<string, int> spriteDataIndices;

	// Source: Ghidra GetSprite.c RVA 0x1a08804
	// 1-1: if dict size != list count → RebuildIndices(); TryGetValue(name); fall back to linear scan.
	public Sprite GetSprite(string name)
	{
		if (spriteDataIndices == null || spriteDatas == null) return null;
		if (spriteDataIndices.Count != spriteDatas.Count) RebuildIndices();
		int idx;
		if (spriteDataIndices.TryGetValue(name, out idx))
		{
			if (idx >= 0 && idx < spriteDatas.Count)
			{
				WndFormSpriteData d = spriteDatas[idx];
				if (d != null) return d.sprite;
			}
		}
		// Linear fallback per Ghidra (cache invalidation case)
		for (int i = 0; i < spriteDatas.Count; i++)
		{
			WndFormSpriteData d = spriteDatas[i];
			if (d != null && d.name == name) return d.sprite;
		}
		return null;
	}

	// Source: Ghidra AddOrUpdateSprite.c RVA 0x1a08a50
	// 1-1: linear scan; if name match → update sprite; else append.
	public string AddOrUpdateSprite(string name, Sprite sprite)
	{
		if (spriteDatas == null) spriteDatas = new List<WndFormSpriteData>();
		for (int i = 0; i < spriteDatas.Count; i++)
		{
			WndFormSpriteData d = spriteDatas[i];
			if (d != null && d.name == name)
			{
				d.sprite = sprite;
				return "" + " : " + name + " update";
			}
		}
		var nd = new WndFormSpriteData();
		nd.name = name;
		nd.sprite = sprite;
		spriteDatas.Add(nd);
		return "" + " : " + name + " add";
	}

	// Source: Ghidra DeleteNullSprites.c — remove entries where sprite == null. Then RebuildIndices.
	public string DeleteNullSprites()
	{
		if (spriteDatas == null) return "";
		int removed = 0;
		for (int i = spriteDatas.Count - 1; i >= 0; i--)
		{
			if (spriteDatas[i] == null || spriteDatas[i].sprite == null)
			{
				spriteDatas.RemoveAt(i);
				removed++;
			}
		}
		RebuildIndices();
		return "removed=" + removed;
	}

	// Source: Ghidra RebuildIndices.c — rebuild spriteDataIndices from spriteDatas.
	public void RebuildIndices()
	{
		if (spriteDataIndices == null) spriteDataIndices = new Dictionary<string, int>();
		spriteDataIndices.Clear();
		if (spriteDatas == null) return;
		for (int i = 0; i < spriteDatas.Count; i++)
		{
			WndFormSpriteData d = spriteDatas[i];
			if (d != null && !string.IsNullOrEmpty(d.name) && !spriteDataIndices.ContainsKey(d.name))
			{
				spriteDataIndices[d.name] = i;
			}
		}
	}

	// Source: Ghidra Destroy.c — clear list + dict.
	public void Destroy()
	{
		if (spriteDatas != null) spriteDatas.Clear();
		if (spriteDataIndices != null) spriteDataIndices.Clear();
	}

	public WndFormAtlas() { }
}
