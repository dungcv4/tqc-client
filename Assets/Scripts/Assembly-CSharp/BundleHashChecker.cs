// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15B0604, 0x15B0650, 0x15B06A8, 0x15B06C0, 0x15B06C8, 0x15B06E0, 0x15B06E8,
//       0x15B0700, 0x15B0718, 0x15B0730, 0x15B0748, 0x15B07B8, 0x15B0830, 0x15B08E8,
//       0x15B09B0, 0x15B0C64, 0x15B0D7C, 0x15B102C, 0x15B1144, 0x15B12B4, 0x15B1348,
//       0x15B13E8, 0x15B1724, 0x15B19E0, 0x15B1C70, 0x15B1B2C, 0x15B1D94
// Ghidra dir: work/06_ghidra/decompiled_full/BundleHashChecker/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-stub  TypeDefIndex: 54
public class BundleHashChecker
{
    // RVA: 0x15B06A8  Property accessor — delegates to TotalNeedBundleSize
    // RVA: 0x15B06C0  Property accessor — delegates to NeedDownloadCount
    private List<string> _bundleFolderName;
    private bool _folderCountDownloadFlag;
    private bool _folderCountLoadingFlag;
    private Dictionary<string, Hash128> _CachedBundleHash;
    private Dictionary<string, int> _CachedBundleSize;
    private long _TotalNeedBundleSize;
    private int _NeedDownloadCount;
    private int _TotalLoadBundleSize;
    private int _NeedLoadingCount;
    private int _SceneBundleSize;
    private int _ModelBundleSize;
    private int _MusicBundleSize;
    private int _MenusBundleSize;

    private static BundleHashChecker s_instance;

    // RVA: 0x15B0604  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_bundleCount.c
    public int bundleCount { get { // Ghidra: returns *(_CachedBundleHash + 0x18) + 4 — i.e. dictionary internal size + 4.
        // Faithful: use Count + 4 as an approximation; the +4 is preserved verbatim.
        if (_CachedBundleHash == null)
        {
            throw new System.NullReferenceException();
        }
        return _CachedBundleHash.Count + 4; } }

    // RVA: 0x15B0650  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_Instance.c
    public static BundleHashChecker Instance { get { // Ghidra: returns the cached singleton stored at PTR_DAT_03448080 + 0xb8 (lazy init via type init).
        // TODO: confidence:low — Ghidra doesn't show explicit lazy-construct here; pattern mirrors
        // other singletons (e.g. ConfigMgr.get_Instance) where the type initializer constructs the
        // single instance.
        if (s_instance == null)
        {
            s_instance = new BundleHashChecker();
        }
        return s_instance; } }

    // RVA: 0x15B06A8  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_TotalNeedBundleSize.c
    public float TotalNeedBundleSize { get { return (float)_TotalNeedBundleSize * 0.0009765625f; } }

    // RVA: 0x15B06C0  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_NeedDownloadCount.c
    public int NeedDownloadCount { get { return _NeedDownloadCount; } }

    // RVA: 0x15B06C8  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_TotalLoadBundleSize.c
    public float TotalLoadBundleSize { get { return (float)_TotalLoadBundleSize * 0.0009765625f; } }

    // RVA: 0x15B06E0  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_NeedLoadingCount.c
    public int NeedLoadingCount { get { return _NeedLoadingCount; } }

    // RVA: 0x15B06E8  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_SceneBundleSize.c
    public float SceneBundleSize { get { return (float)_SceneBundleSize * 0.0009765625f; } }

    // RVA: 0x15B0700  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_ModelBundleSize.c
    public float ModelBundleSize { get { return (float)_ModelBundleSize * 0.0009765625f; } }

    // RVA: 0x15B0718  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_MusicBundleSize.c
    public float MusicBundleSize { get { return (float)_MusicBundleSize * 0.0009765625f; } }

    // RVA: 0x15B0730  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/get_MenusBundleSize.c
    public float MenusBundleSize { get { return (float)_MenusBundleSize * 0.0009765625f; } }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/UpdateBundleHash.c
    // RVA: 0x15B0748 — Hash128 overload: store directly into _CachedBundleHash[BundleName].
    public void UpdateBundleHash(string BundleName, Hash128 NewHash)
    {
        if (_CachedBundleHash == null) throw new System.NullReferenceException();
        _CachedBundleHash[BundleName] = NewHash;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/UpdateBundleHash.c
    // RVA: 0x15B07B8 — string overload: Parse Hash128 from string, store.
    public void UpdateBundleHash(string BundleName, string sNewHash)
    {
        Hash128 hash = Hash128.Parse(sNewHash);
        if (_CachedBundleHash == null) throw new System.NullReferenceException();
        _CachedBundleHash[BundleName] = hash;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckBundleHash.c
    // RVA: 0x15B0830 — Hash128 overload: TryGetValue compare.
    public bool CheckBundleHash(string BundleName, Hash128 NewHash)
    {
        if (_CachedBundleHash == null) throw new System.NullReferenceException();
        if (!_CachedBundleHash.ContainsKey(BundleName)) return false;
        return _CachedBundleHash[BundleName] == NewHash;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckBundleHash.c
    // RVA: 0x15B08E8 — string overload: Parse then compare.
    public bool CheckBundleHash(string BundleName, string sNewHash)
    {
        if (_CachedBundleHash == null) throw new System.NullReferenceException();
        if (!_CachedBundleHash.ContainsKey(BundleName)) return false;
        Hash128 parsed = Hash128.Parse(sNewHash);
        return _CachedBundleHash[BundleName] == parsed;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/ConvertBundleHashToString.c
    // RVA: 0x15B09B0 — Iterate _CachedBundleHash, format "{key}:{hash.ToString()}" (lit #21249),
    //                   collect into List<string>, join with "," (lit #748).
    public string ConvertBundleHashToString()
    {
        if (_CachedBundleHash == null) throw new System.NullReferenceException();
        List<string> list = new List<string>();
        foreach (KeyValuePair<string, Hash128> kv in _CachedBundleHash)
        {
            string formatted = string.Format("{0}:{1}", kv.Key, kv.Value.ToString());
            list.Add(formatted);
        }
        return string.Join(",", list.ToArray());
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/ParseBundleHashFromString.c
    // RVA: 0x15B0C64 — Clear dict, split by ',' (0x2c) then by ':' (0x3a); call UpdateBundleHash for
    //                   each [name, hash] pair. Returns false if input == "" (lit #0).
    public bool ParseBundleHashFromString(string sHashs)
    {
        if (_CachedBundleHash == null) throw new System.NullReferenceException();
        _CachedBundleHash.Clear();
        if (sHashs == "") return false;
        if (sHashs == null) throw new System.NullReferenceException();
        string[] entries = sHashs.Split(',');
        if (entries == null) throw new System.NullReferenceException();
        if (entries.Length < 2) return false;
        for (int i = 0; i < entries.Length; i++)
        {
            string entry = entries[i];
            if (entry == null) throw new System.NullReferenceException();
            string[] kv = entry.Split(':');
            if (kv == null) break;
            if (kv.Length == 2)
            {
                UpdateBundleHash(kv[0], kv[1]);
            }
        }
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/GetNeedUpdateFileList.c
    // RVA: 0x15B0D7C — Walk AssetBundleManager.Instance.GetBundleNames(sPath); compare local
    //                   GetBundleHash vs GetResourceBundleHash; collect names that mismatch both
    //                   our cached value and the resource hash.
    public string[] GetNeedUpdateFileList(string sPath)
    {
        List<string> list = new List<string>();
        AssetBundleManager mgr = AssetBundleManager.Instance;
        if (mgr == null) throw new System.NullReferenceException();
        string[] names = mgr.GetBundleNames(sPath);
        if (names == null) throw new System.NullReferenceException();
        for (int i = 0; i < names.Length; i++)
        {
            string name = names[i];
            AssetBundleManager mgr2 = AssetBundleManager.Instance;
            if (mgr2 == null) throw new System.NullReferenceException();
            Hash128 localHash = mgr2.GetBundleHash(name);
            AssetBundleManager mgr3 = AssetBundleManager.Instance;
            if (mgr3 == null) throw new System.NullReferenceException();
            Hash128 resHash = mgr3.GetResourceBundleHash(name);
            if (!CheckBundleHash(name, localHash) && !localHash.Equals(resHash))
            {
                list.Add(name);
            }
        }
        return list.ToArray();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/ParseBundleSizeFromString.c
    // RVA: 0x15B102C — Same pattern as ParseBundleHashFromString but Clear _CachedBundleSize and
    //                   call UpdateBundleSize for each pair. Returns false on empty (lit #0).
    public bool ParseBundleSizeFromString(string sBundleSize)
    {
        if (_CachedBundleSize == null) throw new System.NullReferenceException();
        _CachedBundleSize.Clear();
        if (sBundleSize == "") return false;
        if (sBundleSize == null) throw new System.NullReferenceException();
        string[] entries = sBundleSize.Split(',');
        if (entries == null) throw new System.NullReferenceException();
        if (entries.Length < 2) return false;
        for (int i = 0; i < entries.Length; i++)
        {
            string entry = entries[i];
            if (entry == null) throw new System.NullReferenceException();
            string[] kv = entry.Split(':');
            if (kv == null) break;
            if (kv.Length == 2)
            {
                UpdateBundleSize(kv[0], kv[1]);
            }
        }
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/UpdateBundleSize.c
    // RVA: 0x15B1144 — Special-cases:
    //   BundleName == "CDNVersion" (lit #3528): set ResourcesPath.CDNVersion = sBundleSize,
    //     also set ResourcesPath.bundleTNum = sBundleSize.Substring(6, 4)
    //   BundleName == "bdCreateTime" (lit #15242): set ResourcesPath.bdCreateTime = sBundleSize
    //   Else: parse int, store in _CachedBundleSize[BundleName]
    public void UpdateBundleSize(string BundleName, string sBundleSize)
    {
        if (BundleName == "CDNVersion")
        {
            ResourcesPath.CDNVersion = sBundleSize;
            if (sBundleSize == null) throw new System.NullReferenceException();
            ResourcesPath.bundleTNum = sBundleSize.Substring(6, 4);
            return;
        }
        if (BundleName == "bdCreateTime")
        {
            ResourcesPath.bdCreateTime = sBundleSize;
            return;
        }
        int parsed = System.Int32.Parse(sBundleSize);
        if (_CachedBundleSize == null) throw new System.NullReferenceException();
        _CachedBundleSize[BundleName] = parsed;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/GetBundleSize.c
    // RVA: 0x15B12B4 — dict lookup; return 0 if missing.
    public int GetBundleSize(string BundleName)
    {
        if (_CachedBundleSize == null) throw new System.NullReferenceException();
        if (!_CachedBundleSize.ContainsKey(BundleName)) return 0;
        return _CachedBundleSize[BundleName];
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/GetBundleSizeinKB.c
    // RVA: 0x15B1348 — dict lookup; return (int)(value/1024) via signed right-shift (floor div).
    // Ghidra: iVar2 + 0x3FF + signed-shift-right >> 10 = ceiling div for negatives,
    //         but for size always >= 0 this is plain (value >> 10) i.e. value / 1024.
    public float GetBundleSizeinKB(string BundleName)
    {
        if (_CachedBundleSize == null) throw new System.NullReferenceException();
        if (!_CachedBundleSize.ContainsKey(BundleName)) return 0f;
        int size = _CachedBundleSize[BundleName];
        return (float)(size >> 10);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckAllNeedDownloadFiles.c
    // RVA: 0x15B13E8 — Reset counters (_TotalNeedBundleSize, _NeedDownloadCount,
    //                   _TotalLoadBundleSize, _NeedLoadingCount); iterate _bundleFolderName (no-skip),
    //                   then process Scene/Model/Music/Menus paths (bSkipCount=true), accumulating
    //                   into _SceneBundleSize/_ModelBundleSize/_MusicBundleSize/_MenusBundleSize.
    //                   Each branch sets _folderCountDownloadFlag (offset 0x18) to false before next.
    public void CheckAllNeedDownloadFiles(Dictionary<string, int> NeedUpdateDatas, Dictionary<string, int> NeedLoadDatas)
    {
        if (_bundleFolderName == null) throw new System.NullReferenceException();
        _TotalNeedBundleSize = 0;
        _NeedDownloadCount = 0;
        _TotalLoadBundleSize = 0;
        for (int i = 0; i < _bundleFolderName.Count; i++)
        {
            string folder = _bundleFolderName[i];
            string nameExt = AssetBundleManager.GetBundleNameWithExt(folder);
            CheckSingleNeedDownloadFile(NeedUpdateDatas, NeedLoadDatas, nameExt, false);
        }
        _NeedLoadingCount = 0;
        string[] sceneList = GetNeedUpdateFileList(ResourcesPath.ScenePath);
        if (sceneList == null) throw new System.NullReferenceException();
        for (int i = 0; i < sceneList.Length; i++)
        {
            int prev = _SceneBundleSize;
            int sz = CheckSingleNeedDownloadFile(NeedUpdateDatas, NeedLoadDatas, sceneList[i], true);
            _SceneBundleSize = prev + sz;
        }
        _folderCountDownloadFlag = false;
        _ModelBundleSize = 0;
        string[] modelList = GetNeedUpdateFileList(ResourcesPath.ModelPath);
        if (modelList == null) throw new System.NullReferenceException();
        for (int i = 0; i < modelList.Length; i++)
        {
            int prev = _ModelBundleSize;
            int sz = CheckSingleNeedDownloadFile(NeedUpdateDatas, NeedLoadDatas, modelList[i], true);
            _ModelBundleSize = prev + sz;
        }
        _folderCountDownloadFlag = false;
        _MusicBundleSize = 0;
        string[] musicList = GetNeedUpdateFileList(ResourcesPath.MusicPath);
        if (musicList == null) throw new System.NullReferenceException();
        for (int i = 0; i < musicList.Length; i++)
        {
            int prev = _MusicBundleSize;
            int sz = CheckSingleNeedDownloadFile(NeedUpdateDatas, NeedLoadDatas, musicList[i], true);
            _MusicBundleSize = prev + sz;
        }
        _folderCountDownloadFlag = false;
        _MenusBundleSize = 0;
        string[] menusList = GetNeedUpdateFileList(ResourcesPath.MenusPath);
        if (menusList == null) throw new System.NullReferenceException();
        for (int i = 0; i < menusList.Length; i++)
        {
            int prev = _MenusBundleSize;
            int sz = CheckSingleNeedDownloadFile(NeedUpdateDatas, NeedLoadDatas, menusList[i], true);
            _MenusBundleSize = prev + sz;
        }
        _folderCountDownloadFlag = false;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckSingleNeedDownloadFile.c
    // RVA: 0x15B1724 — Compare local hash + resource hash. If both mismatch: put into NeedUpdateDatas
    //                   and add to _TotalNeedBundleSize; if the name Contains lit "scene"(#19635),
    //                   "model"(#18709), "music"(#18759), or "menus"(#18628), gate _NeedDownloadCount
    //                   bump via _folderCountDownloadFlag (set once, then early-return iVar2).
    //                   Else (hashes match): if !bSkipCount, push NeedLoadDatas + bump
    //                   _TotalLoadBundleSize/_NeedLoadingCount and return 0.
    public int CheckSingleNeedDownloadFile(Dictionary<string, int> NeedUpdateDatas, Dictionary<string, int> NeedLoadDatas, string sBundleName, bool bSkipCount = false)
    {
        int size = GetBundleSize(sBundleName);
        AssetBundleManager mgr = AssetBundleManager.Instance;
        if (mgr == null) throw new System.NullReferenceException();
        Hash128 localHash = mgr.GetBundleHash(sBundleName);
        AssetBundleManager mgr2 = AssetBundleManager.Instance;
        if (mgr2 == null) throw new System.NullReferenceException();
        Hash128 resHash = mgr2.GetResourceBundleHash(sBundleName);
        if (!CheckBundleHash(sBundleName, localHash) && !localHash.Equals(resHash))
        {
            if (NeedUpdateDatas == null) throw new System.NullReferenceException();
            NeedUpdateDatas[sBundleName] = size;
            _TotalNeedBundleSize = _TotalNeedBundleSize + (long)size;
            if (sBundleName == null) throw new System.NullReferenceException();
            bool isFolderBucket = sBundleName.Contains("scene")
                || sBundleName.Contains("menus")
                || sBundleName.Contains("music")
                || sBundleName.Contains("model");
            if (isFolderBucket)
            {
                if (_folderCountDownloadFlag) return size;
                _folderCountDownloadFlag = true;
            }
            _NeedDownloadCount = _NeedDownloadCount + 1;
            return size;
        }
        if (!bSkipCount)
        {
            if (NeedLoadDatas == null) throw new System.NullReferenceException();
            NeedLoadDatas[sBundleName] = size;
            _TotalLoadBundleSize = _TotalLoadBundleSize + size;
            _NeedLoadingCount = _NeedLoadingCount + 1;
        }
        return 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckInternalBundleHash.c
    // RVA: 0x15B19E0 — Walk AssetBundleManager.Instance.GetCheckBundleNames(sPath); for each call
    //                   CheckSingleMainBundleHash; track bVar9 = (i+1 < N) AFTER each successful
    //                   iter. If single fails → break (bVar9 keeps prior value, which is true on
    //                   first iter since initial bVar9 = 0 < N). Return ~bVar9 & 1:
    //                   - all pass: loop exits with bVar9 = false → return true.
    //                   - any fail: break with bVar9 = true → return false.
    //                   - empty list: bVar9 = false → return true.
    //                   Ghidra also allocates an unused List<string> at entry (preserved as-is).
    public bool CheckInternalBundleHash(string sPath)
    {
        new List<string>();
        AssetBundleManager mgr = AssetBundleManager.Instance;
        if (mgr == null) throw new System.NullReferenceException();
        string[] names = mgr.GetCheckBundleNames(sPath);
        if (names == null) throw new System.NullReferenceException();
        bool bVar9 = names.Length > 0;
        for (int i = 0; i < names.Length; i++)
        {
            if (!CheckSingleMainBundleHash(names[i]))
            {
                break;
            }
            bVar9 = (i + 1) < names.Length;
        }
        return !bVar9;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckMainBundleHash.c
    // RVA: 0x15B1C70 — Iterate _bundleFolderName: for each call CheckSingleMainBundleHash on
    //                   GetBundleNameWithExt(folder). If any returns false → return false.
    //                   After loop completes, return CheckInternalBundleHash(ResourcesPath.ScenePath).
    public bool CheckMainBundleHash()
    {
        if (_bundleFolderName == null) throw new System.NullReferenceException();
        for (int i = 0; i < _bundleFolderName.Count; i++)
        {
            string folder = _bundleFolderName[i];
            string nameExt = AssetBundleManager.GetBundleNameWithExt(folder);
            if (!CheckSingleMainBundleHash(nameExt))
            {
                return false;
            }
        }
        return CheckInternalBundleHash(ResourcesPath.ScenePath);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BundleHashChecker/CheckSingleMainBundleHash.c
    // RVA: 0x15B1B2C — Get checkHash = GetCheckBundleHash(name); resHash = GetResourceBundleHash(name).
    //                   Returns true iff checkHash.Equals(resHash) OR CheckBundleHash(name, checkHash).
    public bool CheckSingleMainBundleHash(string sBundleName)
    {
        AssetBundleManager mgr = AssetBundleManager.Instance;
        if (mgr == null) throw new System.NullReferenceException();
        Hash128 checkHash = mgr.GetCheckBundleHash(sBundleName);
        AssetBundleManager mgr2 = AssetBundleManager.Instance;
        if (mgr2 == null) throw new System.NullReferenceException();
        Hash128 resHash = mgr2.GetResourceBundleHash(sBundleName);
        if (checkHash.Equals(resHash)) return true;
        if (CheckBundleHash(sBundleName, checkHash)) return true;
        return false;
    }

    // RVA: 0x15B1D94  Ghidra: work/06_ghidra/decompiled_full/BundleHashChecker/.ctor.c
    public BundleHashChecker()
    {
        // TODO: confidence:low — Ghidra body for .ctor not inspected here; expected initialization
        // of _bundleFolderName (List<string>), _CachedBundleHash, _CachedBundleSize and clearing
        // counters. Mirrors typical IL2CPP ctor pattern.
        _bundleFolderName = new List<string>();
        _CachedBundleHash = new Dictionary<string, Hash128>();
        _CachedBundleSize = new Dictionary<string, int>();
    }

}
