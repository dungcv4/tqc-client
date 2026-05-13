// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'SpriteRootMirror'
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

// Source: Il2CppDumper-stub  TypeDefIndex: 8221
public class SpriteRootMirror
{
    public bool managed;
    public SpriteManager manager;
    public int drawLayer;
    public SpriteRoot.SPRITE_PLANE plane;
    public SpriteRoot.WINDING_ORDER winding;
    public float width;
    public float height;
    public Vector2 bleedCompensation;
    public SpriteRoot.ANCHOR_METHOD anchor;
    public Vector3 offset;
    public Color color;
    public bool pixelPerfect;
    public bool autoResize;
    public Camera renderCamera;
    public bool hideAtStart;

    // RVA: 0x1587148  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/Mirror.c
    public virtual void Mirror(SpriteRoot s) { throw new System.NotImplementedException(); }

    // RVA: 0x15871EC  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/Validate.c
    public virtual bool Validate(SpriteRoot s) { throw new System.NotImplementedException(); }

    // RVA: 0x1587214  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/DidChange.c
    public virtual bool DidChange(SpriteRoot s) { throw new System.NotImplementedException(); }

    // RVA: 0x1587454  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/HandleManageState.c
    protected virtual void HandleManageState(SpriteRoot s) { throw new System.NotImplementedException(); }

    // RVA: 0x1587480  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/UpdateManager.c
    public virtual void UpdateManager(SpriteRoot s) { throw new System.NotImplementedException(); }

    // RVA: 0x158757C  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/HandleDrawLayerChange.c
    protected virtual void HandleDrawLayerChange(SpriteRoot s) { throw new System.NotImplementedException(); }

    // RVA: 0x1586FB8  Ghidra: work/06_ghidra/decompiled_full/SpriteRootMirror/.ctor.c
    public SpriteRootMirror() { throw new System.NotImplementedException(); }

}
