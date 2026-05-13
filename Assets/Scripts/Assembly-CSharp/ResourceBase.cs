// Source: Ghidra work/06_ghidra/decompiled_full/ResourceBase/ (5 .c) — all methods ported 1-1.
// Source: dump.cs TypeDefIndex 772
// Field layout: name@0x10 (string), _isLoad@0x18 (bool), _isDone@0x19 (bool).

using System;
using UnityEngine;

[Serializable]
public class ResourceBase
{
    public string name;            // 0x10
    protected bool _isLoad;        // 0x18
    protected bool _isDone;        // 0x19

    // Source: Ghidra get_isLoad.c  RVA 0x18F268C — return *(byte*)(this+0x18).
    public bool get_isLoad() { return _isLoad; }

    // Source: Ghidra get_isDone.c  RVA 0x18F2694 — return *(byte*)(this+0x19).
    public bool get_isDone() { return _isDone; }

    // Source: Ghidra Load.c  RVA 0x18F269C
    // Ghidra writes a 16-bit word at 0x18: *(undefined2*)(this+0x18) = 1;
    // That sets _isLoad=1 AND _isDone=0 in one store.
    public virtual bool Load()
    {
        _isLoad = true;
        _isDone = false;
        return true;
    }

    // Source: Ghidra Unload.c  RVA 0x18F26B0
    // Ghidra writes a 16-bit zero at 0x18: *(undefined2*)(this+0x18) = 0;
    // That clears _isLoad AND _isDone in one store.
    public virtual void Unload()
    {
        _isLoad = false;
        _isDone = false;
    }

    // Source: Ghidra IsLoadFinish.c  RVA 0x18F1470 — return *(byte*)(this+0x19) i.e. _isDone.
    public virtual bool IsLoadFinish() { return _isDone; }

    // RVA: 0x18F26B8 — default ctor (no .ctor.c in decompiled_full/).
    public ResourceBase() { }
}
