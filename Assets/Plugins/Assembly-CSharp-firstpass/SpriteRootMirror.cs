// AUTO-GENERATED SKELETON — ported from Ghidra
// Source: work/03_il2cpp_dump/dump.cs class 'SpriteRootMirror'
// Bodies replaced with translations from work/06_ghidra/decompiled_full/SpriteRootMirror/*.c

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

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/Mirror.c RVA 0x1587148
    // Body: copies all mirrored fields from s into this. Each Ghidra `*(...)(param_1+X)=*(...)(param_2+Y)`
    //   assignment maps directly to a field copy (verified offsets against dump.cs SpriteRoot @0x511554
    //   and SpriteRootMirror @0x511920). FUN_015cb8fc() is the IL2CPP NullReferenceException path.
    public virtual void Mirror(SpriteRoot s)
    {
        if (s == null) throw new NullReferenceException();
        this.managed = s.managed;
        this.manager = s.manager;
        this.drawLayer = s.drawLayer;
        this.plane = s.plane;
        this.winding = s.winding;
        this.width = s.width;
        this.height = s.height;
        this.bleedCompensation = s.bleedCompensation;
        this.anchor = s.anchor;
        this.offset = s.offset;
        this.color = s.color;
        this.pixelPerfect = s.pixelPerfect;
        this.autoResize = s.autoResize;
        this.renderCamera = s.renderCamera;
        this.hideAtStart = s.hideAtStart;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/Validate.c RVA 0x15871EC
    // Body: if (s != null) { if (s.pixelPerfect /* offset 0x58 */) s.autoResize /* 0x59 */ = true; return 1; }
    //   else NullReferenceException.
    public virtual bool Validate(SpriteRoot s)
    {
        if (s == null) throw new NullReferenceException();
        if (s.pixelPerfect)
        {
            s.autoResize = true;
        }
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/DidChange.c RVA 0x1587214
    // Body: tail-call dispatch on virtual slots 0x1a8/0x1b8/0x1c8 of SpriteRootMirror's vtable when
    //   the corresponding primary field (managed/manager/drawLayer) mismatches, then return true.
    //   The slot offsets are consecutive (delta 0x10 between adjacent entries) which matches
    //   Il2CppClass.vtable[] layout, mapping in dump.cs slot order (4=Mirror, 5=Validate, 6=DidChange,
    //   7=HandleManageState, 8=UpdateManager, 9=HandleDrawLayerChange):
    //     0x1a8 → HandleManageState (slot 7)
    //     0x1b8 → UpdateManager     (slot 8)
    //     0x1c8 → HandleDrawLayerChange (slot 9)
    //   The deep field-check block runs only when all three primary fields match. Differences in
    //   plane/winding/width/height/bleed/anchor/offset/color/pixelPerfect/autoResize/renderCamera
    //   each return true; equality returns false. Vector2 bleedCompensation and Vector3 offset use
    //   squared-magnitude comparison against DAT_0091c044 (Mathf.Epsilon-squared).
    //   hideAtStart mismatch invokes virtual slot 0x378 on SpriteRoot (parameter `param_2`); this is
    //   a SpriteRoot virtual method taking (bool) — unresolved without SpriteRoot vtable mapping.
    //   TODO: identify SpriteRoot slot 0x378 (likely set_Hidden or OnVisibilityChange) and port that
    //   branch. For now the hideAtStart branch returns true (matches the visible `return 1`) but
    //   skips the virtual invoke.
    public virtual bool DidChange(SpriteRoot s)
    {
        if (s == null) throw new NullReferenceException();
        if (s.managed != this.managed)
        {
            this.HandleManageState(s);
            return true;
        }
        if (s.manager != this.manager)
        {
            this.UpdateManager(s);
            return true;
        }
        if (s.drawLayer != this.drawLayer)
        {
            this.HandleDrawLayerChange(s);
            return true;
        }
        // deep field check — all primary fields match
        if ((int)s.plane != (int)this.plane) return true;
        if ((int)s.winding != (int)this.winding) return true;
        if (s.width != this.width) return true;
        if (s.height != this.height) return true;
        {
            float dx = s.bleedCompensation.x - this.bleedCompensation.x;
            float dy = s.bleedCompensation.y - this.bleedCompensation.y;
            if (dx * dx + dy * dy >= Mathf.Epsilon) return true;
        }
        if ((int)s.anchor != (int)this.anchor) return true;
        {
            float dx = s.offset.x - this.offset.x;
            float dy = s.offset.y - this.offset.y;
            float dz = s.offset.z - this.offset.z;
            if (dx * dx + dy * dy + dz * dz >= Mathf.Epsilon) return true;
        }
        if (s.color.r != this.color.r) return true;
        if (s.color.g != this.color.g) return true;
        if (s.color.b != this.color.b) return true;
        if (s.color.a != this.color.a) return true;
        if (s.pixelPerfect != this.pixelPerfect) return true;
        if (s.autoResize != this.autoResize) return true;
        if (s.renderCamera != this.renderCamera) return true;
        if (s.hideAtStart != this.hideAtStart)
        {
            // TODO: invoke SpriteRoot virtual slot 0x378 with (bool)s.hideAtStart.
            //       Cannot port 1-1 without resolving the SpriteRoot vtable.
            return true;
        }
        return false;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/HandleManageState.c RVA 0x1587454
    // Body: if (s != null) { bool m = this.managed; s.managed = m; s.Managed = (m == 0); return; } else NPE.
    //   Note Ghidra `SpriteRoot__set_Managed(param_2, cVar1 == '\0')` — `cVar1` is this.managed.
    //   It calls the property setter `s.Managed = !this.managed`.
    //   The direct field assignment `s.managed = this.managed` happens first.
    protected virtual void HandleManageState(SpriteRoot s)
    {
        if (s == null) throw new NullReferenceException();
        bool m = this.managed;
        s.managed = m;
        s.Managed = !m;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/UpdateManager.c RVA 0x1587480
    // Body: if (s != null) {
    //   if (!s.managed) { s.manager = null; return; }    // offset 0x28 = s.manager
    //   if (this.manager != null/destroyed) { this.manager.RemoveSprite(s); }
    //   if (s.manager != null/destroyed) { s.manager.AddSprite(s); }
    // } else NPE.
    public virtual void UpdateManager(SpriteRoot s)
    {
        if (s == null) throw new NullReferenceException();
        if (!s.managed)
        {
            s.manager = null;
            return;
        }
        if (this.manager != null)
        {
            this.manager.RemoveSprite(s);
        }
        if (s.manager != null)
        {
            s.manager.AddSprite(s);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/HandleDrawLayerChange.c RVA 0x158757C
    // Body: if (s == null) NPE.
    //   if (s.managed) s.SetDrawLayer(s.drawLayer); else s.drawLayer = 0;
    //   Ghidra: `if (*(char *)(param_2 + 0x20) != '\0')` reads s.managed (offset 0x20).
    //           `SpriteRoot__SetDrawLayer(param_2, *(undefined4 *)(param_2 + 0x34))` calls with s.drawLayer.
    //           else branch zeroes `*(undefined4 *)(param_2 + 0x34)` which is s.drawLayer.
    protected virtual void HandleDrawLayerChange(SpriteRoot s)
    {
        if (s == null) throw new NullReferenceException();
        if (s.managed)
        {
            s.SetDrawLayer(s.drawLayer);
        }
        else
        {
            s.drawLayer = 0;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteRootMirror/.ctor.c RVA 0x1586FB8
    // Body: System_Object___ctor(__this, 0) — just the base Object ctor call, nothing else.
    public SpriteRootMirror() { }

}
