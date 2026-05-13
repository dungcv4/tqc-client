// Ported 2026-05-12 from work/06_ghidra/decompiled_full/TResource2/ (3 .c: Load/OnLoaded/Unload).
// Signatures preserved 1-1 from dump.cs TypeDefIndex 776.

using System;
using UnityEngine;

public class TResource2 : ResourceBase
{
    public delegate void cbFunction(TResource2 res);

    public TResource2.cbFunction callback;  // offset 0x20
    public Type type;                       // offset 0x28
    public object data;                     // offset 0x30
    public int assetType;                   // offset 0x38

    // Source: Ghidra work/06_ghidra/decompiled_full/TResource2/OnLoaded.c RVA 0x018f2710
    // 1. _isDone = true (offset 0x19).
    // 2. If objs == null: skip the assign block.
    // 3. obj0 = objs[0] (with bounds check via param_2+0x18==0 -> throw).
    // 4. If UnityEngine.Object.op_Inequality(obj0, null): data = Convert.ChangeType(obj0, this.type).
    // 5. Invoke callback(this) if callback != null.
    public virtual void OnLoaded(UnityEngine.Object[] objs)
    {
        _isDone = true;
        if (objs != null)
        {
            if (objs.Length == 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            UnityEngine.Object obj0 = objs[0];
            if (obj0 != null)
            {
                data = System.Convert.ChangeType(obj0, this.type);
            }
        }
        if (callback != null)
        {
            callback(this);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/TResource2/Load.c RVA 0x018f280c
    // 1. If !_isLoad:
    //      _isLoad = true (& _isDone = false via word-write at offset 0x18).
    //      If !string.IsNullOrEmpty(name):
    //         loader = new CBNewObjectLoader(this, this.OnLoaded);
    //         op = ResourcesLoader.GetObjectTypeAssetDynamic((int)assetType, name, loader);
    //         goto FINAL — return op != null.
    //      Else: _isDone = true (fall through).
    // 2. (fall-through) return data != null.
    public override bool Load()
    {
        if (!_isLoad)
        {
            _isLoad = true;
            _isDone = false;
            if (!string.IsNullOrEmpty(name))
            {
                CBNewObjectLoader loader = new CBNewObjectLoader(this.OnLoaded);
                IUJObjectOperation op = ResourcesLoader.GetObjectTypeAssetDynamic((int)assetType, name, loader);
                return op != null;
            }
            _isDone = true;
        }
        return data != null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/TResource2/Unload.c RVA 0x018f28ec
    // data = null; _isLoad = false; _isDone = false (combined word-store at offset 0x18).
    public override void Unload()
    {
        data = null;
        _isLoad = false;
        _isDone = false;
    }

    // Source: Ghidra (no .ctor.c) — default ctor inferred. RVA 0x18f2910.
    public TResource2() { }
}
