using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class WndFormAtlas : MonoBehaviour
{
	public List<WndFormSpriteData> spriteDatas;

	public Dictionary<string, int> spriteDataIndices;

	public Sprite GetSprite(string name)
	{ return default; }

	public string AddOrUpdateSprite(string name, Sprite sprite)
	{ return default; }

	public string DeleteNullSprites()
	{ return default; }

	public void RebuildIndices()
	{ }

	public void Destroy()
	{ }

	public WndFormAtlas()
	{ }
}
