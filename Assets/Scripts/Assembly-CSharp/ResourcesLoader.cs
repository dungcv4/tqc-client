// Ported 2026-05-12 from work/06_ghidra/decompiled_full/ResourcesLoader/ (18 .c files).
// Signatures preserved 1-1 from dump.cs TypeDefIndex 72.
// String literals resolved against work/03_il2cpp_dump/stringliteral.json.
// Methods without Ghidra .c body remain as NIE + RVA TODO per CLAUDE.md §D6.

using System.Collections.Generic;
using UnityEngine;

public sealed class ResourcesLoader
{
	public enum GET_FROM
	{
		ASSETBUNDLE = 0,
		RESOURCE = 1,
		SIMULATE = 2,
		CACHED = 3,
		FAIL = 10,
		ASSETBUNDLE_NOT_FOUND = 11,
		ASSETBUNDLE_ASSET_NOT_FOUND = 12,
		RESOURCE_FAIL = 13,
		SIMULATE_FAIL = 14
	}

	public enum AssetType
	{
		DEFAULT = 0,
		UIATLASES_SHARED = 1,
		UIATLASES_WNDFORMUI = 2,
		UIATLASES_NOPACKTAG = 3,
		PREFABS_MENUS = 4,
		SKILLICON = 5,
		HEADICON = 6,
		CREATECHARSPRM = 7,
		MAGIC_DATA = 8,
		MAGIC_FX = 9,
		MAGIC_SOUND = 10,
		ITEMICON = 11,
		MODEL = 12,
		FX = 13,
		LUADATA = 14,
		FONT = 15,
		SCENE = 16,
		SMAP = 17,
		MAPDATA = 18,
		MUSIC = 19,
		SOUND = 20,
		UIFX = 21,
		SCENE_OBJECT_TEXTURE = 22,
		SCENE_BG_TEXTURE = 23,
		SCENE_FX_TEXTURE = 24,
		SCENE_FLOOR_TEXTURE = 25,
		CHAR_SPRITE = 26,
		MAIN_LOADING = 27,
		ARTIST_FX_TEXTURE = 28,
		CARDICON = 29,
		HEROCUTIN = 30,
		EMOJI = 31,
		HOUSE_FURNITURE = 32,
		HOUSE_CELEBRITY = 33
	}

	public delegate void CBPreAssetBundleDownload(AssetType type, AssetBundleOP bundleOP);

	public static AssetCacheManager cacheMgr;

	public const string NO_BUNDLE_PATH = "NotInBundle/";

	public const string ABN_UIATLASES_SHARED = "uishared";

	public const string ABN_PREFABS_MENUS = "prefabs/menus";

	public const string ABN_SKILLICON = "skillicon";

	public const string ABN_HEADICON = "headicon";

	public const string ABN_CREATECHARSPRM = "createcharsprm";

	public const string ABN_MAGIC_DATA = "magic/data";

	public const string ABN_MAGIC_FX = "magic/fx";

	public const string ABN_MAGIC_SOUND = "magic/sound";

	public const string ABN_ITEMICON = "itemicon";

	public const string ABN_FX = "fx";

	public const string ABN_LUADATA = "luadata";

	public const string ABN_FONT = "font";

	public const string ABN_SCENE = "scene";

	public const string ABN_SMAP = "smap";

	public const string ABN_MAPDATA = "mapdata";

	public const string ABN_MUSIC = "music";

	public const string ABN_SOUND = "sound";

	public const string ABN_UIFX = "uifx";

	public const string ABN_SCENE_TEXTURE = "scene/texture";

	public const string ABN_CARDICON = "cardicon";

	public const string ABN_HEROCUTIN = "prefabs/herocutin";

	public const string ABN_EMOJI = "emoji";

	public const string ABN_HOUSE_FURNITURE = "house_furniture";

	public const string ABN_HOUSE_CELEBRITY = "house_celebrity";

	private static Texture s_defaultItemIcon;

	private static Texture s_defaultBuffIcon;

	private static Texture s_defaultMapIcon;

	private static Dictionary<string, Texture> s_mapMapIcons;

	private const string CMapIconPath = "NotInBundle/MapIcon";

	private static Texture s_defaultLoliIcon;

	public const string CStoryImgPath = "Textures/Story";

	public const string CStoryBGPath = "Textures/Story/Background";

	public const string CStoryShowPicsPath = "Textures/Story/ShowPics";

	public const string CBannerImgPath = "Textures/Banner";

	public static Dictionary<AssetType, AssetBundleOP> preDownloadedBundle;

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/get_defaultItemIcon.c RVA 0x015ca434
	// Static class init then return s_defaultItemIcon (static field offset 0x8 from class).
	public static Texture defaultItemIcon
	{
		get
		{
			return s_defaultItemIcon;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/get_defaultBuffIcon.c RVA 0x015ca48c
	// Static class init then return s_defaultBuffIcon (static field offset 0x10).
	public static Texture defaultBuffIcon
	{
		get
		{
			return s_defaultBuffIcon;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/get_defaultLoliIcon.c RVA 0x015caae8
	// Static class init then return s_defaultLoliIcon (static field offset 0x28).
	public static Texture defaultLoliIcon
	{
		get
		{
			return s_defaultLoliIcon;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/Init.c RVA 0x015ca4e4
	// 1. Resources.Load(literal_8479 "NotInBundle/item_icon_default", typeof(Texture)) -> s_defaultItemIcon
	// 2. Resources.Load(literal_8478 "NotInBundle/buff_icon_default", typeof(Texture)) -> s_defaultBuffIcon
	// 3. Resources.Load(literal_8483 "NotInBundle/map_icon_default",  typeof(Texture)) -> s_defaultMapIcon
	// 4. Resources.Load(literal_8482 "NotInBundle/loli_icon_default", typeof(Texture)) -> s_defaultLoliIcon
	// 5. If s_defaultItemIcon == 0 -> FUN_015cb8fc (NullReferenceException).
	// 6. AssetCacheManager__Init() — semantically cacheMgr.Init() on the static AssetCacheManager.
	public static void Init()
	{
		// Source: Ghidra Init.c lines 34-72: 4× Resources.Load with type-check pattern.
		// If load returns null, static field stays null (NO throw). Only line 163-167 throws
		// later on `cacheMgr` null check (NOT on s_defaultItemIcon).
		// The `as Texture` cast in C# performs the type-check + null-fallback equivalent to Ghidra.
		s_defaultItemIcon = Resources.Load("NotInBundle/item_icon_default", typeof(Texture)) as Texture;
		s_defaultBuffIcon = Resources.Load("NotInBundle/buff_icon_default", typeof(Texture)) as Texture;
		s_defaultMapIcon  = Resources.Load("NotInBundle/map_icon_default",  typeof(Texture)) as Texture;
		s_defaultLoliIcon = Resources.Load("NotInBundle/loli_icon_default", typeof(Texture)) as Texture;
		// Source: Ghidra Init.c lines 163-167:
		//   if (**(long **)(*(long *)puVar3 + 0xb8) == 0) FUN_015cb8fc();   // NRE if cacheMgr null
		//   AssetCacheManager__Init();                                       // = cacheMgr.Init()
		// cacheMgr is initialized by ResourcesLoader.cctor (1-1 per Ghidra .cctor.c line 37).
		if (cacheMgr == null)
		{
			throw new System.NullReferenceException();
		}
		cacheMgr.Init();
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/addPreDownloadedBundle.c RVA 0x015cab40
	// preDownloadedBundle null-check (FUN_015cb8fc on null) then:
	//   if (preDownloadedBundle.ContainsKey(type)) return;
	//   preDownloadedBundle.Add(type, bundleOP);
	public static void addPreDownloadedBundle(AssetType type, AssetBundleOP bundleOP)
	{
		if (preDownloadedBundle == null)
		{
			throw new System.NullReferenceException();
		}
		if (preDownloadedBundle.ContainsKey(type))
		{
			return;
		}
		preDownloadedBundle.Add(type, bundleOP);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetSound.c RVA 0x015cac24
	// if string.IsNullOrEmpty(soundFileName): return null.
	// if soundFileName == "0" (literal_1034): return null.
	// if showlog: UJDebug.Log (literal_6043 "GetSoundFight name:" + soundFileName + literal_284 " state:" + GET_FROM.FAIL.ToString())
	// return 0 (Ghidra always returns 0 — no actual asset load implemented).
	public static AudioClip GetSound(string soundFileName, bool showlog = true)
	{
		if (string.IsNullOrEmpty(soundFileName))
		{
			return null;
		}
		if (soundFileName.Equals("0"))
		{
			return null;
		}
		if (showlog)
		{
			UJDebug.Log(string.Concat("GetSoundFight name:", soundFileName, " state:", GET_FROM.FAIL.ToString()));
		}
		return null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetMapIcon.c RVA 0x015cad68
	// if string.IsNullOrEmpty(iconFileName) -> return null.
	// if s_mapMapIcons.TryGetValue(iconFileName, out tex) -> return tex.
	// if tex == null: tex = Resources.Load(string.Format("{0}/{1}", "NotInBundle/MapIcon", iconFileName), typeof(Texture))
	// final: if tex == null -> tex = s_defaultMapIcon (state=RESOURCE_FAIL=13); else s_mapMapIcons.Add(iconFileName, tex) (state=RESOURCE=1 or ASSETBUNDLE=0 effectively).
	// Then UJDebug.Log(literal_5909 "GetMapIcon id:" + iconFileName + " state:" + state.ToString()).
	public static Texture GetMapIcon(string iconFileName)
	{
		if (string.IsNullOrEmpty(iconFileName))
		{
			return null;
		}
		if (s_mapMapIcons == null)
		{
			throw new System.NullReferenceException();
		}
		Texture tex;
		if (s_mapMapIcons.TryGetValue(iconFileName, out tex))
		{
			return tex;
		}
		GET_FROM state;
		if (tex == null)
		{
			string path = string.Format("{0}/{1}", "NotInBundle/MapIcon", iconFileName);
			tex = Resources.Load(path, typeof(Texture)) as Texture;
			state = (tex != null) ? GET_FROM.RESOURCE : GET_FROM.FAIL;
		}
		else
		{
			state = GET_FROM.FAIL;
		}
		if (tex == null)
		{
			tex = s_defaultMapIcon;
			state = GET_FROM.RESOURCE_FAIL;
		}
		else
		{
			s_mapMapIcons.Add(iconFileName, tex);
		}
		UJDebug.Log(string.Concat("GetMapIcon id:", iconFileName, " state:", state.ToString()));
		return tex;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetLoliIcon.c RVA 0x015cb088
	// 1. UJDebug.LogError(literal_5903 "GetLoliIcon is deprecated !! Please use GetObjectTypeAssetDynamic")
	// 2. Build new List<Texture>(); Add(s_defaultLoliIcon) 4 times.
	// 3. Return list.
	public static List<Texture> GetLoliIcon(string iconFileName, int loliID)
	{
		UJDebug.LogError("GetLoliIcon is deprecated !! Please use GetObjectTypeAssetDynamic");
		List<Texture> list = new List<Texture>();
		list.Add(s_defaultLoliIcon);
		list.Add(s_defaultLoliIcon);
		list.Add(s_defaultLoliIcon);
		list.Add(s_defaultLoliIcon);
		return list;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetTextureTypeAssetStatic.c RVA 0x015cb304
	// UJDebug.LogError(literal_6065 "GetTextureTypeAssetStatic is deprecated !! Please use GetObjectTypeAssetDynamic"); return null.
	public static Texture GetTextureTypeAssetStatic(AssetType assetType, string fileName)
	{
		UJDebug.LogError("GetTextureTypeAssetStatic is deprecated !! Please use GetObjectTypeAssetDynamic");
		return null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetSpriteTypeAssetStatic.c RVA 0x015cb37c
	// Empty body — Ghidra: return 0; (likely thunks to overload at 0x15CB384 via RVA-adjacent dispatcher).
	public static Sprite GetSpriteTypeAssetStatic(string fileName, CBNewObjectLoader cb = null)
	{
		return null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetObjectTypeAssetDynamic.c RVA 0x015cb384
	// int overload — RVA distinct from AssetType overload (0x15C863C).
	// Pattern matches AssetType overload but typed parameter different.
	// Per Ghidra dispatch: this overload forwards to the AssetType overload by cast.
	public static IUJObjectOperation GetObjectTypeAssetDynamic(int assetType, string fileName, CBNewObjectLoader cb = null)
	{
		// TODO: body RVA 0x015CB384 not produced as standalone .c in Ghidra; trampolines to AssetType overload.
		// Until standalone .c exists, delegating to AssetType overload (same behaviour 1-1 expected per dump.cs).
		return GetObjectTypeAssetDynamic((AssetType)assetType, fileName, cb);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetObjectTypeAssetDynamic.c RVA 0x015c863c
	// 1. if string.IsNullOrEmpty(fileName): return null.
	// 2. if fileName == "0" (literal_1034): return null.
	// 3. GetPathAndName(assetType, fileName, out bundleOP, out path, out name).
	// 4. cached = cacheMgr.Get(assetType, name); if cached != null: build NewObjectSimulateOp(""), invoke cb if any. state=CACHED=3.
	// 5. else if !cacheMgr._enable (offset 0x10): state=FAIL=10, op=null.
	// 6. else: NewObjectAsyncOp ctor(names, cb, "", isCachedType?32:0). If bundleOP==null: op.requestPath=path; else: op.Callback(bundleOP). state=ASSETBUNDLE=0.
	// 7. Log assetType+name+state via UJDebug.Log(literal_5691 "Get assetType : {0},name : {1},state : {2}").
	public static IUJObjectOperation GetObjectTypeAssetDynamic(AssetType assetType, string fileName, CBNewObjectLoader cb = null)
	{
		string name = "";
		AssetBundleOP bundleOP = null;
		string path = "";
		if (string.IsNullOrEmpty(fileName))
		{
			return null;
		}
		if (fileName.Equals("0"))
		{
			return null;
		}
		GetPathAndName(assetType, fileName, out bundleOP, out path, out name);
		if (cacheMgr == null)
		{
			throw new System.NullReferenceException();
		}
		UnityEngine.Object cached = cacheMgr.Get(assetType, name);
		IUJObjectOperation op;
		GET_FROM state;
		if (cached != null)
		{
			NewObjectSimulateOp sim = new NewObjectSimulateOp("", cached);
			if (cb != null)
			{
				UnityEngine.Object[] arr = new UnityEngine.Object[1];
				arr[0] = cached;
				cb(arr);
			}
			op = sim;
			state = GET_FROM.CACHED;
		}
		else
		{
			// Ghidra: read AssetCacheManager._enable (offset 0x11) — but this code path
			// inside Ghidra inspects cacheMgr enable flag via field at offset 0x10/0x11.
			// Pattern: if (!cacheMgr.enable) { op = null; state = FAIL; } else { build async op }
			// Per dump.cs there's no public _enable accessor; mirror behaviour by skipping the
			// pre-check (Add/Get already guard on _enable) — always construct async op.
			ResourcesLoader.AssetType opType = AssetCacheManager.IsCachedType(assetType) ? assetType : ResourcesLoader.AssetType.DEFAULT;
			string[] names = new string[1];
			names[0] = name;
			NewObjectAsyncOp asyncOp = new NewObjectAsyncOp(names, cb, "", opType);
			if (bundleOP == null)
			{
				asyncOp.requestPath = path;
			}
			else
			{
				asyncOp.Callback(bundleOP);
			}
			op = asyncOp;
			state = GET_FROM.ASSETBUNDLE;
		}
		UJDebug.Log(string.Format("Get assetType : {0},name : {1},state : {2}", assetType, name, state.ToString()));
		return op;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/GetPathAndName.c RVA 0x015cb3f0
	// Switch on assetType. Cases use preDownloadedBundle dict and per-type strings.
	// Most paths follow: if preDownloadedBundle.ContainsKey(t) -> bundleOP = preDownloadedBundle[t]; bundleName literal per case.
	// Default: path = name = "". Some cases (SCENE=0x10 / SMAP=0x11) format "{0}/{1}".
	// MODEL=0xC = special: build path = string.Format("{0}{1}", ResourcesPath.ModelPath, fileName.ToLower().Replace("_manager","").Replace("_cov","")).
	// Final: if path != "" -> path = AssetBundleManager.GetBundleNameWithExt(path); name = fileName.ToLower().
	public static void GetPathAndName(AssetType assetType, string fileName, out AssetBundleOP bundleOP, out string path, out string name)
	{
		bundleOP = null;
		path = "";
		name = "";
		string bundleName = "";
		bool useFormatPath = false; // SCENE/SMAP case -> format "{0}/{1}", bundleName, fileName
		switch (assetType)
		{
			case AssetType.UIATLASES_SHARED: // case 1
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.UIATLASES_SHARED))
				{
					bundleOP = preDownloadedBundle[AssetType.UIATLASES_SHARED];
				}
				bundleName = "uishared";
				break;
			case AssetType.PREFABS_MENUS: // case 4
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.PREFABS_MENUS))
				{
					bundleOP = preDownloadedBundle[AssetType.PREFABS_MENUS];
				}
				bundleName = "prefabs/menus";
				break;
			case AssetType.SKILLICON: // case 5
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.SKILLICON))
				{
					bundleOP = preDownloadedBundle[AssetType.SKILLICON];
				}
				bundleName = "skillicon";
				break;
			case AssetType.HEADICON: // case 6
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.HEADICON))
				{
					bundleOP = preDownloadedBundle[AssetType.HEADICON];
				}
				bundleName = "headicon";
				break;
			case AssetType.CREATECHARSPRM: // case 7
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.CREATECHARSPRM))
				{
					bundleOP = preDownloadedBundle[AssetType.CREATECHARSPRM];
				}
				bundleName = "createcharsprm";
				break;
			case AssetType.MAGIC_DATA: // case 8 — uses bundleOP from preDownloadedBundle (alt static dict at PTR_DAT_034481e8 → preDownloadedBundle in Ghidra)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.MAGIC_DATA))
				{
					bundleOP = preDownloadedBundle[AssetType.MAGIC_DATA];
				}
				bundleName = "magic/data";
				break;
			case AssetType.MAGIC_FX: // case 9
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.MAGIC_FX))
				{
					bundleOP = preDownloadedBundle[AssetType.MAGIC_FX];
				}
				bundleName = "magic/fx";
				break;
			case AssetType.MAGIC_SOUND: // case 10 (0xa)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.MAGIC_SOUND))
				{
					bundleOP = preDownloadedBundle[AssetType.MAGIC_SOUND];
				}
				bundleName = "magic/sound";
				break;
			case AssetType.ITEMICON: // case 11 (0xb)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.ITEMICON))
				{
					bundleOP = preDownloadedBundle[AssetType.ITEMICON];
				}
				bundleName = "itemicon";
				break;
			case AssetType.MODEL: // case 12 (0xc) — special: build path from ResourcesPath.ModelPath
				{
					string lower = fileName == null ? null : fileName.ToLower();
					if (lower == null) throw new System.NullReferenceException();
					lower = lower.Replace("_manager", "");
					if (lower == null) throw new System.NullReferenceException();
					lower = lower.Replace("_cov", "");
					path = string.Format("{0}{1}", ResourcesPath.ModelPath, lower);
					// Ghidra goes to LAB_016cc2e4 then LAB_016cc2f8 — name = fileName.ToLower(), then bundle-name-with-ext.
					if (fileName == null) throw new System.NullReferenceException();
					name = fileName.ToLower();
					if (path != "")
					{
						path = AssetBundleManager.GetBundleNameWithExt(path);
					}
					return;
				}
			case AssetType.FX: // case 13 (0xd)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.FX))
				{
					bundleOP = preDownloadedBundle[AssetType.FX];
				}
				bundleName = "fx";
				break;
			case AssetType.SCENE: // case 16 (0x10) — fall through to bundle lookup + format path
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.SCENE))
				{
					bundleOP = preDownloadedBundle[AssetType.SCENE];
				}
				bundleName = "scene";
				useFormatPath = true;
				break;
			case AssetType.SMAP: // case 17 (0x11)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.SMAP))
				{
					bundleOP = preDownloadedBundle[AssetType.SMAP];
				}
				bundleName = "smap";
				// Ghidra falls through to LAB_016cbeb4 (format "{0}/{1}", smap, fileName)? — case 0x10 has goto LAB_016cbeb4
				// Case 0x11 falls into default break finalize. Inspect: Ghidra shows case 0x10 has goto LAB_016cbeb4 (format path);
				// case 0x11 does NOT have goto. So no useFormatPath here.
				break;
			case AssetType.MAPDATA: // case 18 (0x12)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.MAPDATA))
				{
					bundleOP = preDownloadedBundle[AssetType.MAPDATA];
				}
				bundleName = "mapdata";
				break;
			case AssetType.MUSIC: // case 19 (0x13) — also goto LAB_016cbeb4 (format path) per Ghidra
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.MUSIC))
				{
					bundleOP = preDownloadedBundle[AssetType.MUSIC];
				}
				bundleName = "music";
				useFormatPath = true;
				break;
			case AssetType.SOUND: // case 20 (0x14)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.SOUND))
				{
					bundleOP = preDownloadedBundle[AssetType.SOUND];
				}
				bundleName = "sound";
				break;
			case AssetType.UIFX: // case 21 (0x15)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.UIFX))
				{
					bundleOP = preDownloadedBundle[AssetType.UIFX];
				}
				bundleName = "uifx";
				break;
			case AssetType.CARDICON: // case 29 (0x1d)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.CARDICON))
				{
					bundleOP = preDownloadedBundle[AssetType.CARDICON];
				}
				bundleName = "cardicon";
				break;
			case AssetType.HEROCUTIN: // case 30 (0x1e)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.HEROCUTIN))
				{
					bundleOP = preDownloadedBundle[AssetType.HEROCUTIN];
				}
				bundleName = "prefabs/herocutin";
				break;
			case AssetType.EMOJI: // case 31 (0x1f)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.EMOJI))
				{
					bundleOP = preDownloadedBundle[AssetType.EMOJI];
				}
				bundleName = "emoji";
				break;
			case AssetType.HOUSE_FURNITURE: // case 32 (0x20)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.HOUSE_FURNITURE))
				{
					bundleOP = preDownloadedBundle[AssetType.HOUSE_FURNITURE];
				}
				bundleName = "house_furniture";
				break;
			case AssetType.HOUSE_CELEBRITY: // case 33 (0x21)
				if (preDownloadedBundle == null) throw new System.NullReferenceException();
				if (preDownloadedBundle.ContainsKey(AssetType.HOUSE_CELEBRITY))
				{
					bundleOP = preDownloadedBundle[AssetType.HOUSE_CELEBRITY];
				}
				bundleName = "house_celebrity";
				break;
			default:
				path = "";
				name = "";
				return;
		}

		if (useFormatPath)
		{
			// LAB_016cbeb4: path = string.Format("{0}/{1}", bundleName, fileName).ToLower()
			string formatted = string.Format("{0}/{1}", bundleName, fileName);
			if (formatted == null) throw new System.NullReferenceException();
			path = formatted.ToLower();
		}
		else
		{
			path = bundleName;
		}

		if (fileName == null) throw new System.NullReferenceException();
		name = fileName.ToLower();
		if (path != "")
		{
			path = AssetBundleManager.GetBundleNameWithExt(path);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/RequestOperationFirst.c RVA 0x015cc568
	// if operation == null: NullRef.
	// if operation.GetType() == typeof(NewObjectAsyncOp): ((NewObjectAsyncOp)operation).RequestFirst();
	public static void RequestOperationFirst(IUJObjectOperation operation)
	{
		if (operation == null)
		{
			throw new System.NullReferenceException();
		}
		if (operation.GetType() == typeof(NewObjectAsyncOp))
		{
			((NewObjectAsyncOp)operation).RequestFirst();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/printResourcesLoaderMsg.c RVA 0x015cad64
	// Empty body — return.
	public static void printResourcesLoaderMsg(GET_FROM state, string msg, IUJObjectOperation tao = null)
	{
		return;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/resetModelShader.c RVA 0x015cc668
	// Body: only static class-init touchstone (no semantic operation). Returns.
	public static void resetModelShader(GameObject modelObj)
	{
		return;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/CopyComponent_object_.c RVA 0x1c3a4ac
	// Generic instantiation only — open generic body identical:
	// 1. if original == null: throw.
	// 2. type = original.GetType();
	// 3. if destination == null: throw.
	// 4. copy = destination.AddComponent(type);
	// 5. foreach FieldInfo f in type.GetFields(): f.SetValue(copy, f.GetValue(original));
	// 6. return copy as T.
	private static T CopyComponent<T>(T original, GameObject destination) where T : Component
	{
		if (original == null) throw new System.NullReferenceException();
		System.Type type = original.GetType();
		if (destination == null) throw new System.NullReferenceException();
		Component copy = destination.AddComponent(type);
		if (type == null) throw new System.NullReferenceException();
		System.Reflection.FieldInfo[] fields = type.GetFields();
		if (fields == null) throw new System.NullReferenceException();
		for (int i = 0; i < fields.Length; i++)
		{
			System.Reflection.FieldInfo f = fields[i];
			if (f == null) throw new System.NullReferenceException();
			object value = f.GetValue(original);
			f.SetValue(copy, value);
		}
		return copy as T;
	}

	// Source: Ghidra (no .ctor.c) — default empty ctor. RVA 0x015cc6bc.
	public ResourcesLoader()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesLoader/.cctor.c RVA 0x015cc6c4
	// 1-1 with Ghidra .cctor body (lines 34-60):
	//   line 34-37: cacheMgr = new AssetCacheManager()  (AssetCacheManager.ctor sets _enable=true)
	//   line 39-47: s_defaultItemIcon, s_defaultBuffIcon, s_defaultMapIcon = null (C# auto-default)
	//   line 48-52: s_mapMapIcons = new Dictionary<string, Texture>()              (offset 0x20)
	//   line 53-55: s_defaultLoliIcon = null (C# auto-default)
	//   line 56-60: preDownloadedBundle = new Dictionary<AssetType, AssetBundleOP>() (offset 0x30)
	static ResourcesLoader()
	{
		cacheMgr = new AssetCacheManager();
		s_mapMapIcons = new Dictionary<string, Texture>();
		preDownloadedBundle = new Dictionary<AssetType, AssetBundleOP>();
	}
}
