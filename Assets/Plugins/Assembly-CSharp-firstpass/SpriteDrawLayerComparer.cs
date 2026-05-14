// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'SpriteDrawLayerComparer'
// To port a method: replace `throw new System.NotImplementedException();`
// with body translated from the listed Ghidra .c file.
// Move ported file to unity_project/Assets/Scripts/Ported/<area>/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8171
public class SpriteDrawLayerComparer : IComparer<SpriteMesh_Managed>
{
    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteDrawLayerComparer/Compare.c RVA 0x15774FC
    // 1-1:
    //   if (a == null || b == null) NRE;
    //   if (b.drawLayer < a.drawLayer) return  1;       // a comes after b (larger drawLayer drawn later)
    //   if (a.drawLayer < b.drawLayer) return -1;
    //   return 0;
    // Ascending sort by SpriteMesh_Managed.drawLayer (field at +0x20).
    public int Compare(SpriteMesh_Managed a, SpriteMesh_Managed b)
    {
        if (a == null) throw new System.NullReferenceException();
        if (b == null) throw new System.NullReferenceException();
        if (b.drawLayer < a.drawLayer) return 1;
        if (a.drawLayer < b.drawLayer) return -1;
        return 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteDrawLayerComparer/.ctor.c RVA 0x15774F4
    // 1-1: System.Object..ctor only. No fields.
    public SpriteDrawLayerComparer() { }

}
