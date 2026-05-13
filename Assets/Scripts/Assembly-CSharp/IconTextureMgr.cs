// Source: Ghidra work/06_ghidra/decompiled_rva/IconTextureMgr__*.c — 5 methods ported 1-1.
// Source: dump.cs TypeDefIndex 660 — IconTextureMgr
// switch(nMode) maps to ResMgr static AssetBundleOP fields:
//   0 → ItemIconBundleOP (0x40), 1 → HeadIconBundleOP (0x48), 2 → SkillIconBundleOP (0x50),
//   3 → CardIconBundleOP (0xA0), 4 → EmojiBundleOP (0xA8), default → null.
// PTR_DAT_0345a358 = typeof(Sprite), PTR_DAT_03460300 = typeof(Sprite[]).

using Cpp2IlInjected;
using UnityEngine;

public class IconTextureMgr
{
	protected static IconTextureMgr _instance;

	// Source: Ghidra work/06_ghidra/decompiled_rva/IconTextureMgr__get_Instance.c RVA 0x018C6280
	public static IconTextureMgr Instance
	{
		get
		{
			IconTextureMgr lVar3 = _instance;
			if (lVar3 == null)
			{
				IconTextureMgr uVar2 = new IconTextureMgr();
				_instance = uVar2;
				lVar3 = _instance;
			}
			return lVar3;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/IconTextureMgr___ctor.c RVA 0x018C6278
	public IconTextureMgr()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/IconTextureMgr__GetIconSprite.c RVA 0x018C6308
	public Sprite GetIconSprite(string iconName, int nMode)
	{
		AssetBundleOP lVar2;
		switch (nMode)
		{
			case 0:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.ItemIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.ItemIconBundleOP;
				break;
			case 1:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.HeadIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.HeadIconBundleOP;
				break;
			case 2:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.SkillIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.SkillIconBundleOP;
				break;
			case 3:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.CardIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.CardIconBundleOP;
				break;
			case 4:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.EmojiBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.EmojiBundleOP;
				break;
			default:
				return null;
		}
		System.Type uVar4 = typeof(UnityEngine.Sprite);
		if (lVar2 != null)
		{
			UnityEngine.Object plVar3 = lVar2.Load(iconName, uVar4);
			if (plVar3 == null)
			{
				return null;
			}
			Sprite asSprite = plVar3 as Sprite;
			if (asSprite == null)
			{
				return null;
			}
			return asSprite;
		}
		throw new System.NullReferenceException();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/IconTextureMgr__LoadIconSpriteAsync.c RVA 0x018C66DC
	public AssetBundleRequest LoadIconSpriteAsync(string iconName, int nMode)
	{
		AssetBundleOP lVar2;
		switch (nMode)
		{
			case 0:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.ItemIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.ItemIconBundleOP;
				break;
			case 1:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.HeadIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.HeadIconBundleOP;
				break;
			case 2:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.SkillIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.SkillIconBundleOP;
				break;
			case 3:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.CardIconBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.CardIconBundleOP;
				break;
			case 4:
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				if (ResMgr.Instance.EmojiBundleOP == null)
				{
					return null;
				}
				if (ResMgr.Instance == null) throw new System.NullReferenceException();
				lVar2 = ResMgr.Instance.EmojiBundleOP;
				break;
			default:
				return null;
		}
		System.Type uVar3 = typeof(UnityEngine.Sprite);
		if (lVar2 != null)
		{
			AssetBundleRequest req = lVar2.LoadAsync(iconName, uVar3);
			return req;
		}
		throw new System.NullReferenceException();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/IconTextureMgr__GetSpriteWithSubSprites.c RVA 0x018C6A8C
	public Sprite[] GetSpriteWithSubSprites(string iconName)
	{
		if (ResMgr.Instance == null) throw new System.NullReferenceException();
		if (ResMgr.Instance.EmojiBundleOP == null)
		{
			return null;
		}
		if (ResMgr.Instance == null) throw new System.NullReferenceException();
		AssetBundleOP lVar2 = ResMgr.Instance.EmojiBundleOP;
		if (lVar2 != null)
		{
			return lVar2.LoadWithSubAssets<Sprite>(iconName);
		}
		throw new System.NullReferenceException();
	}
}
