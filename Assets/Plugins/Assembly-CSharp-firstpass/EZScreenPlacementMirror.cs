// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'EZScreenPlacementMirror'
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

// Source: Il2CppDumper-stub  TypeDefIndex: 8184
public class EZScreenPlacementMirror
{
    public Vector3 worldPos;
    public Vector3 screenPos;
    public EZScreenPlacement.RelativeTo relativeTo;
    public Transform relativeObject;
    public Camera renderCamera;
    public Vector2 screenSize;

    // RVA: 0x157D508  Ghidra: work/06_ghidra/decompiled_full/EZScreenPlacementMirror/.ctor.c
    public EZScreenPlacementMirror() { throw new System.NotImplementedException(); }

    // RVA: 0x157D6DC  Ghidra: work/06_ghidra/decompiled_full/EZScreenPlacementMirror/Mirror.c
    public virtual void Mirror(EZScreenPlacement sp) { throw new System.NotImplementedException(); }

    // RVA: 0x157D79C  Ghidra: work/06_ghidra/decompiled_full/EZScreenPlacementMirror/Validate.c
    public virtual bool Validate(EZScreenPlacement sp) { throw new System.NotImplementedException(); }

    // RVA: 0x157D9C4  Ghidra: work/06_ghidra/decompiled_full/EZScreenPlacementMirror/DidChange.c
    public virtual bool DidChange(EZScreenPlacement sp) { throw new System.NotImplementedException(); }

}
