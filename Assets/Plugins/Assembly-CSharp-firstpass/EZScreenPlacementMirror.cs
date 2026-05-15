// AUTO-GENERATED SKELETON — ported from Ghidra
// Source: work/03_il2cpp_dump/dump.cs class 'EZScreenPlacementMirror'
// Bodies replaced with translations from work/06_ghidra/decompiled_full/EZScreenPlacementMirror/*.c

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

    // Source: Ghidra work/06_ghidra/decompiled_full/EZScreenPlacementMirror/.ctor.c RVA 0x157D508
    // The Ghidra decompile shows the IL2CPP-generated raw-allocate-and-fill path for the inner
    // RelativeTo object (alloc + field writes for horizontal=1, vertical=1, script=null + Object ctor).
    // This is semantically identical to `new RelativeTo(null)` per
    // work/06_ghidra/decompiled_full/EZScreenPlacement.RelativeTo/.ctor.c RVA 0x157C3B4 which writes
    // the same packed literal 0x100000001 to offset 0x10 and stores `sp` (here null) at 0x18.
    // We port by invoking RelativeTo(null) — the C# constructor that matches the inlined ctor.
    public EZScreenPlacementMirror()
    {
        this.relativeTo = new EZScreenPlacement.RelativeTo(null);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZScreenPlacementMirror/Mirror.c RVA 0x157D6DC
    // Body:
    //   if (sp == null || sp.transform == null) NPE.
    //   this.worldPos = sp.transform.position;
    //   this.screenPos = sp.screenPos;                              // packed 0x1c..0x27 read from sp.0x28..0x33
    //   if (this.relativeTo != null) {
    //     if (sp.relativeTo != null)
    //       // 8-byte copy: horizontal (4) + vertical (4) packed
    //       this.relativeTo.horizontal = sp.relativeTo.horizontal;
    //       this.relativeTo.vertical   = sp.relativeTo.vertical;
    //     this.relativeObject = sp.relativeObject;
    //     this.renderCamera = sp.renderCamera;
    //     if (sp.renderCamera != null) {
    //       this.screenSize = new Vector2(camera.pixelWidth, camera.pixelHeight);
    //     }
    //   } else NPE.
    public virtual void Mirror(EZScreenPlacement sp)
    {
        if (sp == null) throw new NullReferenceException();
        Transform t = sp.transform;
        if (t == null) throw new NullReferenceException();
        this.worldPos = t.position;
        this.screenPos = sp.screenPos;
        if (this.relativeTo == null) throw new NullReferenceException();
        if (sp.relativeTo != null)
        {
            this.relativeTo.horizontal = sp.relativeTo.horizontal;
            this.relativeTo.vertical = sp.relativeTo.vertical;
        }
        this.relativeObject = sp.relativeObject;
        this.renderCamera = sp.renderCamera;
        if (sp.renderCamera == null) throw new NullReferenceException();
        int pw = sp.renderCamera.pixelWidth;
        if (sp.renderCamera == null) throw new NullReferenceException();
        int ph = sp.renderCamera.pixelHeight;
        this.screenSize = new Vector2((float)pw, (float)ph);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZScreenPlacementMirror/Validate.c RVA 0x157D79C
    // Body:
    //   if (sp == null || sp.relativeTo == null) NPE.
    //   if (sp.relativeTo.horizontal != OBJECT && sp.relativeTo.vertical != OBJECT) {
    //     sp.relativeObject = null;
    //   }
    //   if ((sp.relativeObject == null/destroyed) || EZScreenPlacement.TestDepenency(sp)) return true;
    //   // else: log dependency loop error via a stringbuilder-like Format call (Debug.LogError),
    //   //   then sp.relativeObject = null; return true.
    //   The Ghidra string-format path constructs error message from string literals 337, 338, 4916
    //   (not extractable from this skeleton). The path is: assigning fmtBuf entries, calling
    //   String.Concat, then Debug.LogError.
    public virtual bool Validate(EZScreenPlacement sp)
    {
        if (sp == null) throw new NullReferenceException();
        if (sp.relativeTo == null) throw new NullReferenceException();
        if ((int)sp.relativeTo.horizontal != 4 && (int)sp.relativeTo.vertical != 4)
        {
            sp.relativeObject = null;
        }
        Transform relObj = sp.relativeObject;
        // Ghidra: ((op_Inequality(relObj,null) & 1) == 0) || (TestDepenency(sp) & 1) != 0 → return 1
        // i.e., relObj is null/destroyed, OR TestDepenency(sp) returns true
        if (relObj == null || EZScreenPlacement.TestDepenency(sp))
        {
            return true;
        }
        // Dependency-loop branch: Debug.LogError with three string literals (337/338/4916) concatenated
        // with sp.name and relObj.name. The literals are not extractable from this skeleton.
        // TODO: extract StringLiteral_337/338/4916 from work/03_il2cpp_dump for exact messages.
        Debug.LogError("EZScreenPlacementMirror: dependency loop on " + sp.name + " -> " + relObj.name);
        sp.relativeObject = null;
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZScreenPlacementMirror/DidChange.c RVA 0x157D9C4
    // Body: returns true if any tracked field differs from sp; otherwise returns result of
    //   `UnityEngine_Object__op_Inequality(this.relativeObject, sp.relativeObject)` as bool.
    //   The function uses squared-magnitude comparisons against DAT_0091c044 (Mathf.Epsilon-squared)
    //   for Vector3 worldPos and screenPos. If worldPos differs by more than epsilon, the function
    //   re-positions sp on screen (PositionOnScreen) or world-projects it (WorldToScreenPos), then
    //   returns true.
    //   Field offsets:
    //     param_4 = this (mirror): worldPos@0x10, screenPos@0x1C, relativeTo@0x28, relativeObject@0x30,
    //                              renderCamera@0x38, screenSize@0x40
    //     param_5 = sp (EZScreenPlacement): renderCamera@0x20, screenPos@0x28, relativeTo@0x38,
    //                                       relativeObject@0x40, allowTransformDrag@0x49
    public virtual bool DidChange(EZScreenPlacement sp)
    {
        if (sp == null) throw new NullReferenceException();
        Transform t = sp.transform;
        if (t == null) throw new NullReferenceException();
        Vector3 wpos = t.position;
        Vector3 wdiff = new Vector3(
            this.worldPos.x - wpos.x,
            this.worldPos.y - wpos.y,
            this.worldPos.z - wpos.z);
        float wsqr = wdiff.x * wdiff.x + wdiff.y * wdiff.y + wdiff.z * wdiff.z;
        if (wsqr < Mathf.Epsilon)
        {
            // worldPos matches — check screenPos
            float dx = this.screenPos.x - sp.screenPos.x;
            float dy = this.screenPos.y - sp.screenPos.y;
            float dz = this.screenPos.z - sp.screenPos.z;
            if (dx * dx + dy * dy + dz * dz < Mathf.Epsilon)
            {
                // screenPos matches — check renderCamera/screenSize
                if (this.renderCamera != null)
                {
                    if (sp.renderCamera == null) throw new NullReferenceException();
                    if (this.screenSize.x != sp.renderCamera.pixelWidth) return true;
                    if (sp.renderCamera == null) throw new NullReferenceException();
                    if (this.screenSize.y != sp.renderCamera.pixelHeight) return true;
                }
                // check relativeTo fields and final relativeObject inequality
                if (this.relativeTo == null) throw new NullReferenceException();
                if (sp.relativeTo != null
                    && (int)this.relativeTo.horizontal == (int)sp.relativeTo.horizontal
                    && (int)this.relativeTo.vertical == (int)sp.relativeTo.vertical)
                {
                    // Ghidra: `(op_Inequality(this.renderCamera, sp.renderCamera) & 1) == 0` → cameras equal
                    if (this.renderCamera == sp.renderCamera)
                    {
                        // Final test: relativeObject inequality (Unity Object semantics)
                        return this.relativeObject != sp.relativeObject;
                    }
                }
            }
        }
        else
        {
            // worldPos changed by more than epsilon
            // Ghidra: if (sp.allowTransformDrag == 0) PositionOnScreen(); else sp.WorldToScreenPos(sp.transform.position)
            // (the transform.position get is the source of the v0-v2 register values held into the WorldToScreenPos call)
            if (!sp.allowTransformDrag)
            {
                sp.PositionOnScreen();
            }
            else
            {
                Transform t2 = sp.transform;
                if (t2 == null) throw new NullReferenceException();
                sp.WorldToScreenPos(t2.position);
            }
        }
        // default return — Ghidra falls through to `return 1` for all paths not handled above
        return true;
    }

}
