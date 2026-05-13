// Source: work/03_il2cpp_dump/dump.cs class 'TResObject' (TypeDefIndex 779)
// Bodies ported 1-1 from Ghidra decompiled_rva/TResObject_oo__*.c (object,object instantiation).

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

// Source: Il2CppDumper-stub  TypeDefIndex: 779
public class TResObject<K, T>
{
    public K key;
    public T value;
    public float time;

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObject_oo__Compare.c RVA 0x02460330
    // Ghidra body: resolves vtable method on `this.key` and invokes with `_key` — this is the
    // interface dispatch for `IComparable.CompareTo(object)`. Returns the comparison result.
    public int Compare(K _key)
    {
        if (key == null)
        {
            throw new NullReferenceException();
        }
        return ((IComparable)key).CompareTo(_key);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObject_oo___ctor.c RVA 0x024603d8
    // Body: only `System_Object___ctor(this, 0)` — empty default constructor.
    public TResObject()
    {
    }
}
