// AUTO-GENERATED SKELETON — partially ported from Ghidra
// Source: work/03_il2cpp_dump/dump.cs class 'Rect3D'
// Bodies replaced with translations from work/06_ghidra/decompiled_full/Rect3D/*.c

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8215
public struct Rect3D
{
    private Vector3 m_tl;
    private Vector3 m_tr;
    private Vector3 m_bl;
    private Vector3 m_br;
    private float m_width;
    private float m_height;

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/get_topLeft.c RVA 0x15837C8
    // Body: return *param_1; (Vector3 at offset 0x0 = m_tl)
    public Vector3 get_topLeft() { return m_tl; }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/get_topRight.c RVA 0x15837D4
    // Body: return *(undefined4 *)(param_1 + 0xc); (Vector3 at offset 0xC = m_tr)
    public Vector3 get_topRight() { return m_tr; }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/get_bottomLeft.c RVA 0x15837E0
    // Body: return *(undefined4 *)(param_1 + 0x18); (Vector3 at offset 0x18 = m_bl)
    public Vector3 get_bottomLeft() { return m_bl; }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/get_bottomRight.c RVA 0x15837EC
    // Body: return *(undefined4 *)(param_1 + 0x24); (Vector3 at offset 0x24 = m_br)
    public Vector3 get_bottomRight() { return m_br; }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/get_width.c RVA 0x15837F8
    // Body: lazy-compute width when cached value is NaN.
    //   Ghidra: `if (0x7f800000 < (uint)ABS(fVar1))` is the bit-pattern test for NaN — a float
    //   whose abs() reinterpreted as uint exceeds the +Infinity encoding 0x7f800000 is NaN.
    //   This is how IL2CPP compiles `float.IsNaN(m_width)`.
    //   if NaN(m_width): m_width = (m_tr - m_tl).magnitude
    //   return m_width;
    public float get_width()
    {
        if (float.IsNaN(m_width))
        {
            m_width = (m_tr - m_tl).magnitude;
        }
        return m_width;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/get_height.c RVA 0x15838AC
    // Body: lazy-compute height when cached value is NaN — same NaN bit-pattern test as get_width.
    //   The vector math (param_1[0]/[1]/[2] minus param_1[6]/[7]/[8]) is (m_tl - m_bl).magnitude.
    //   if NaN(m_height): m_height = (m_tl - m_bl).magnitude
    //   return m_height;
    public float get_height()
    {
        if (float.IsNaN(m_height))
        {
            m_height = (m_tl - m_bl).magnitude;
        }
        return m_height;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/FromPoints.c RVA 0x1583960
    // Body: assigns m_tl, m_tr, m_bl from args; computes m_br = tr + (bl - tl); sets m_width/m_height to NaN
    public void FromPoints(Vector3 tl, Vector3 tr, Vector3 bl)
    {
        m_tl = tl;
        m_tr = tr;
        m_bl = bl;
        m_br = new Vector3(
            tr.x + (bl.x - tl.x),
            tr.y + (bl.y - tl.y),
            tr.z + (bl.z - tl.z));
        m_width = float.NaN;
        m_height = float.NaN;
    }

    // RVA: 0x15839A8
    // TODO: Ghidra .c not generated for ctor(Vector3, Vector3, Vector3) — directory only contains
    // .ctor.c for the (Rect r) overload at RVA 0x1583AB0. Body cannot be ported 1-1 without source.
    public Rect3D(Vector3 tl, Vector3 tr, Vector3 bl)
    {
        m_tl = Vector3.zero; m_tr = Vector3.zero; m_bl = Vector3.zero; m_br = Vector3.zero;
        m_width = 0f; m_height = 0f;
        throw new System.NotImplementedException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Rect3D/.ctor.c RVA 0x1583AB0
    // Body: zero-initialize m_tl, m_tr, m_bl, m_br (from static Vector3.zero ref), then call FromRect(r).
    // The (long)param_5 + 0xc, +0x18, +0x24 writes correspond to m_tr, m_bl, m_br.
    public Rect3D(Rect r)
    {
        m_tl = Vector3.zero;
        m_tr = Vector3.zero;
        m_bl = Vector3.zero;
        m_br = Vector3.zero;
        m_width = 0f;
        m_height = 0f;
        FromRect(r);
    }

    // RVA: 0x1583BB4  Ghidra: work/06_ghidra/decompiled_full/Rect3D/GetRect.c
    // Ghidra decompile is a single empty `return;` with no field reads — the original function body
    // was not recovered usefully. Cannot port 1-1.
    // TODO: re-decompile RVA 0x1583BB4 with deeper analysis or fetch from Il2CppDumper if available.
    public Rect GetRect() { throw new System.NotImplementedException(); }

    // RVA: 0x1583B80  Ghidra: work/06_ghidra/decompiled_full/Rect3D/FromRect.c
    // Ghidra decompile shows: FromPoints(param_1, param_4 + param_2, 0, param_3 + param_1, param_4 + param_2, 0)
    // Only 6 of the 9 Vector3 floats are visible — bl Vector3 (3 floats) is passed on the stack but
    // is not displayed in Ghidra's decompiled call expression. Cannot port 1-1 without seeing bl.
    // TODO: re-decompile RVA 0x1583B80 with stack-argument tracking enabled.
    public void FromRect(Rect r) { throw new System.NotImplementedException(); }

    // RVA: 0x1583BCC
    // TODO: Ghidra .c not generated for instance MultFast(Matrix4x4) — directory's MultFast.c contains
    // only the static overload at RVA 0x1583C88. Body cannot be ported 1-1 without source.
    public void MultFast(Matrix4x4 matrix) { throw new System.NotImplementedException(); }

    // RVA: 0x1583C88  Ghidra: work/06_ghidra/decompiled_full/Rect3D/MultFast.c
    // Ghidra decompile is partial: computes min_x/max_x/max_y across three transformed corners
    // (tl, tr, bl via matrix.MultiplyPoint3x4) and tail-calls Rect3D ctor with 6 visible floats,
    // but does NOT show the min_y computation or the bl Vec3 arg to ctor (stack). Cannot port 1-1.
    // TODO: re-decompile RVA 0x1583C88 with stack-argument tracking enabled.
    public static Rect3D MultFast(Rect3D rect, Matrix4x4 matrix) { throw new System.NotImplementedException(); }

}
