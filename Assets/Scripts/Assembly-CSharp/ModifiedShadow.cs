// Source: dump.cs class ModifiedShadow : Shadow (TypeDefIndex 173)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/ModifiedShadow/{ApplyShadow,ModifyMesh,ModifyVertices}.c
// RVAs: ApplyShadow=0x17C6F80, ModifyMesh=0x17C84B4, ModifyVertices=0x17C857C, .ctor=0x17C7288

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifiedShadow : Shadow
{
    // Source: Ghidra ApplyShadow.c RVA 0x17C6F80
    // 1-1: iterates from `start` to `end`, for each vertex copies it to the end of the list
    // (extending capacity if needed). The copy retains the original alpha when useGraphicAlpha
    // is set (alpha = color.a * original.a / 255), then translates by (x, y) and writes back
    // to the original index. Net effect: original vertices are translated by (x, y); copies of
    // pre-translation vertices are appended at the end of the list with the shadow color.
    //
    // Note: this method hides Shadow.ApplyShadow (same protected signature); `new` keyword.
    protected new void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
    {
        if (verts == null) throw new System.NullReferenceException();

        int neededCapacity = verts.Count + (end - start);
        if (verts.Capacity < neededCapacity)
        {
            verts.Capacity = neededCapacity;
        }

        for (int i = start; i < end; i++)
        {
            UIVertex vt = verts[i];
            // Save pre-translation position because Ghidra writes the saved (x,y) + offset
            // back into the original slot AFTER appending a copy.
            float savedX = vt.position.x;
            float savedY = vt.position.y;

            // Append a copy of `vt` to the end of the list (shadow vertex, untranslated).
            verts.Add(vt);

            // Decide alpha for the original slot's color overwrite.
            byte alpha = color.a;
            if (this.useGraphicAlpha)
            {
                // Ghidra: re-reads list[i] (still the un-translated vert since we just Added a copy
                // at the end, not at index i). Uses (color.a * vt.color.a) / 255.
                UIVertex vt2 = verts[i];
                alpha = (byte)(((int)color.a * (int)vt2.color.a) / 255);
            }

            // Overwrite the original slot: translate position by (x, y), apply tinted color.
            vt.position = new Vector3(savedX + x, savedY + y, vt.position.z);
            vt.color = new Color32(color.r, color.g, color.b, alpha);
            verts[i] = vt;
        }
    }

    // Source: Ghidra ModifyMesh.c RVA 0x17C84B4
    // 1-1: if (!IsActive()) return; var list = new List<UIVertex>();
    //      vh.GetUIVertexStream(list); ModifyVertices(list); vh.AddUIVertexTriangleStream(list);
    // (virtual dispatch on this+0x1c8 = IsActive, this+0x288 = ModifyVertices)
    public override void ModifyMesh(VertexHelper vh)
    {
        if (!this.IsActive())
        {
            return;
        }
        List<UIVertex> list = new List<UIVertex>();
        if (vh == null) throw new System.NullReferenceException();
        vh.GetUIVertexStream(list);
        ModifyVertices(list);
        vh.AddUIVertexTriangleStream(list);
    }

    // Source: Ghidra ModifyVertices.c RVA 0x17C857C
    // 1-1: empty body — Ghidra: `return;` only. Subclasses override.
    public virtual void ModifyVertices(List<UIVertex> verts)
    {
    }

    // Source: Ghidra .ctor.c RVA 0x17C7288 — UnityEngine.UI.Shadow..ctor() chain (implicit base()).
    public ModifiedShadow()
    {
    }
}
