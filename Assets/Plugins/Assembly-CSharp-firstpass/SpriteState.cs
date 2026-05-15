// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'SpriteState'
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

// Source: Il2CppDumper-stub  TypeDefIndex: 8209
[Serializable]
public class SpriteState
{
    public string name;
    public string imgPath;
    public CSpriteFrame frameInfo;

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteState/.ctor.c RVA 0x158363C
    // 1-1: System_Object___ctor(this, 0); this->name = n; this->imgPath = p;
    // (field offsets: name=0x10, imgPath=0x18 per dump.cs; thunk_FUN_015ee8c4 is GC write barrier)
    public SpriteState(string n, string p)
    {
        this.name = n;
        this.imgPath = p;
    }

}
