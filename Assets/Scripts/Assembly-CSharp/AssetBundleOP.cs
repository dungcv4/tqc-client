// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15D4E14, 0x15D4E60, 0x15D4F00, 0x15D4F38, 0x15C72F0, 0x15D4F50, 0x15D4F68,
//       0x15D4F80, 0x15D4F98, 0x15D4FB4
// Ghidra dir: work/06_ghidra/decompiled_full/AssetBundleOP/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 91
public sealed class AssetBundleOP
{
    private AssetBundleManager.WWWBundleRef _wwwRef;
    private bool _IsImmDestroy;

    // RVA: 0x15D4E14  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/.ctor.c
    public AssetBundleOP(AssetBundleManager.WWWBundleRef wwwRef)
    {
        // TODO: confidence:low — .ctor.c not produced for this class; signature and field layout
        // suggest the ctor stores wwwRef and increments its ref count via IncRef().
        _wwwRef = wwwRef;
        if (wwwRef != null)
        {
            wwwRef.IncRef();
        }
    }

    // RVA: 0x15D4E60  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/Finalize.c
    ~AssetBundleOP() { }

    // RVA: 0x15D4F00  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/ImmDestroy.c
    public void ImmDestroy()
    {
        AssetBundleManager.WWWBundleRef wwwRef = _wwwRef;
        _IsImmDestroy = true;
        if (wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        if (0 < wwwRef.refCount)
        {
            wwwRef.DecRef();
        }
        _wwwRef = null;
    }

    // RVA: 0x15D4F38  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/Load.c
    public UnityEngine.Object Load(string name)
    {
        // TODO: confidence:low — Load.c shows only one branch for the (name,type) overload;
        // single-arg overload mirrors the same null-check forward pattern.
        if (_wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        return _wwwRef.Load(name);
    }

    // Source: IL2CPP generic instantiation Load<object> at RVA 0x1BC42E8.
    // Pattern: delegate to non-generic Load(name, typeof(T)) and cast.
    public T Load<T>(string name) where T : UnityEngine.Object
    {
        return Load(name, typeof(T)) as T;
    }

    // RVA: 0x15C72F0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/Load.c
    public UnityEngine.Object Load(string name, Type type)
    {
        if (_wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        return _wwwRef.Load(name, type);
    }

    // Source: IL2CPP generic instantiation LoadWithSubAssets<object> at RVA 0x1BC4378.
    // Delegates to _wwwRef.LoadWithSubAssets<T>(name) (WWWBundleRef has matching generic at RVA 0x1C8D664).
    public T[] LoadWithSubAssets<T>(string name) where T : UnityEngine.Object
    {
        if (_wwwRef == null) throw new System.NullReferenceException();
        return _wwwRef.LoadWithSubAssets<T>(name);
    }

    // RVA: 0x15D4F50  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/LoadAsync.c
    public AssetBundleRequest LoadAsync(string name, Type type)
    {
        if (_wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        return _wwwRef.LoadAsync(name, type);
    }

    // Source: IL2CPP generic instantiation LoadAsync<object> at RVA 0x1BC4330.
    // Pattern: delegate to non-generic with typeof(T).
    public AssetBundleRequest LoadAsync<T>(string name) where T : UnityEngine.Object
    {
        return LoadAsync(name, typeof(T));
    }

    // RVA: 0x15D4F68  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/LoadAllAsync.c
    public AssetBundleRequest LoadAllAsync()
    {
        if (_wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        return _wwwRef.LoadAllAsync();
    }

    // RVA: 0x15D4F80  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/LoadAll.c
    public UnityEngine.Object[] LoadAll()
    {
        if (_wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        return _wwwRef.LoadAll();
    }

    // RVA: 0x15D4F98  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/Unload.c
    public void Unload(bool unloadAllLoadedObjects)
    {
        if (_wwwRef == null)
        {
            throw new System.NullReferenceException();
        }
        _wwwRef.Unload(unloadAllLoadedObjects);
    }

    // RVA: 0x15D4FB4  Ghidra: work/06_ghidra/decompiled_full/AssetBundleOP/get_isStreamedSceneAssetBundle.c
    public bool isStreamedSceneAssetBundle { get { if (_wwwRef == null)
        {
            return false;
        }
        return _wwwRef.isStreamedSceneAssetBundle; } }

}
