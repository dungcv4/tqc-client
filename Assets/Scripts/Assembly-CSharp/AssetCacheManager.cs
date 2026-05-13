// Source: Ghidra work/06_ghidra/decompiled_full/AssetCacheManager/ (6 .c) — all 8 methods ported 1-1.
// Source: dump.cs TypeDefIndex 94
// String literal #3131 = format "{0}" log (debug) — kept verbatim.

using System.Collections.Generic;
using UnityEngine;

public class AssetCacheManager
{
    private bool DEBUG;     // 0x10
    private bool _enable;   // 0x11
    private static Dictionary<ResourcesLoader.AssetType, int> CachedAssetTypes;
    private Dictionary<ResourcesLoader.AssetType, QueueDictionary<string, UnityEngine.Object>> CachedAsset;

    // Source: Ghidra getMaxPoolSize.c  RVA 0x15D5D2C
    // Static dict lookup CachedAssetTypes[asType].
    private static int getMaxPoolSize(ResourcesLoader.AssetType asType)
    {
        if (CachedAssetTypes == null) throw new System.NullReferenceException();
        return CachedAssetTypes[asType];
    }

    // Source: Ghidra Init.c  RVA 0x15CA8A0
    // CachedAsset = new Dict; iterate CachedAssetTypes.Keys → add QueueDictionary entry per key.
    public void Init()
    {
        CachedAsset = new Dictionary<ResourcesLoader.AssetType, QueueDictionary<string, UnityEngine.Object>>();
        if (CachedAssetTypes == null) throw new System.NullReferenceException();
        foreach (var key in CachedAssetTypes.Keys)
        {
            CachedAsset.Add(key, new QueueDictionary<string, UnityEngine.Object>());
        }
    }

    // Source: Ghidra Add.c  RVA 0x15D5DAC
    // If !_enable: return. If !IsCachedType: return.
    // If queue.ContainsKey(name): return (no-op already cached).
    // Else: if pool full (count >= getMaxPoolSize) → Dequeue oldest (DEBUG log if enabled).
    //       Enqueue(name, asset).
    public void Add(ResourcesLoader.AssetType asType, string assetName, UnityEngine.Object asset)
    {
        if (!_enable) return;
        if (!IsCachedType(asType)) return;
        if (CachedAsset == null) throw new System.NullReferenceException();
        QueueDictionary<string, UnityEngine.Object> queue = CachedAsset[asType];
        if (queue == null) throw new System.NullReferenceException();
        if (queue.ContainsKey(assetName)) return;
        int count = queue.Count;
        int maxSize = getMaxPoolSize(asType);
        if (maxSize <= count)
        {
            queue.Dequeue();
            if (DEBUG)
            {
                UJDebug.Log(string.Format("{0}", asType), false, UJLogType.None);
            }
        }
        queue.Enqueue(assetName, asset);
    }

    // Source: Ghidra Get.c  RVA 0x15CC370
    // If !_enable: return null. If !CachedAsset.ContainsKey(asType): return null.
    // If !queue.ContainsKey(name): return null. Else: GetByKey(name).
    public UnityEngine.Object Get(ResourcesLoader.AssetType asType, string assetName)
    {
        if (!_enable) return null;
        if (CachedAsset == null) throw new System.NullReferenceException();
        if (!CachedAsset.ContainsKey(asType)) return null;
        QueueDictionary<string, UnityEngine.Object> queue = CachedAsset[asType];
        if (queue == null) throw new System.NullReferenceException();
        if (!queue.ContainsKey(assetName)) return null;
        return queue.GetByKey(assetName);
    }

    // Source: Ghidra IsInCached.c  RVA 0x15D5FE0
    // If !_enable: return false. Else: queue[asType].ContainsKey(name).
    public bool IsInCached(ResourcesLoader.AssetType asType, string assetName)
    {
        if (!_enable) return false;
        if (CachedAsset == null) throw new System.NullReferenceException();
        QueueDictionary<string, UnityEngine.Object> queue = CachedAsset[asType];
        if (queue == null) throw new System.NullReferenceException();
        return queue.ContainsKey(assetName);
    }

    // Source: Ghidra IsCachedType.c  RVA 0x15CC47C
    // Static dict lookup: CachedAssetTypes.ContainsKey(asType).
    public static bool IsCachedType(ResourcesLoader.AssetType asType)
    {
        if (CachedAssetTypes == null) throw new System.NullReferenceException();
        return CachedAssetTypes.ContainsKey(asType);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetCacheManager/.ctor.c RVA 0x15CC848
    // 1-1: *(byte*)(this + 0x11) = 1  →  _enable = true  (line 9 of Ghidra .ctor.c)
    //      System_Object___ctor(this, 0)                  (line 10)
    public AssetCacheManager()
    {
        _enable = true;
    }

    // Source: Ghidra .cctor.c  RVA 0x15D6084 — initialize CachedAssetTypes dict.
    // Specific entries (asset-type → pool size) defined per IL2CPP static initializer.
    // TODO: enumerate exact key/value pairs from .cctor.c (not yet decoded);
    // for now create empty dict so type loads cleanly.
    static AssetCacheManager()
    {
        CachedAssetTypes = new Dictionary<ResourcesLoader.AssetType, int>();
    }
}
