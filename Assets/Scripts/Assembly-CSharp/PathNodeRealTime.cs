// Source: dump.cs TypeDefIndex 38 — public class PathNodeRealTime : IPathNode<PathNodeRealTime>
// Source: Ghidra work/06_ghidra/decompiled_full/PathNodeRealTime/ (16 .c) — all simple getters/setters ported 1-1.
// Field offsets: connections@0x10, _parent@0x18, position.x@0x20, position.y@0x24,
//                _Invalid@0x28, _invalidConNum@0x2C, _gn@0x30, _hn@0x34.

using System;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeRealTime : IPathNode<PathNodeRealTime>
{
    public List<PathNodeRealTime> connections;   // 0x10
    private PathNodeRealTime _parent;            // 0x18
    private Vector2 position;                    // 0x20 (x), 0x24 (y)
    private bool _Invalid;                       // 0x28
    public int _invalidConNum;                   // 0x2C
    private float _gn;                           // 0x30
    private float _hn;                           // 0x34

    // Source: Ghidra get_parent.c  RVA 0x15AE2B4 — returns field@0x18.
    public PathNodeRealTime get_parent() { return _parent; }

    // Source: Ghidra set_parent.c  RVA 0x15AE2BC — stores value at field@0x18.
    public void set_parent(PathNodeRealTime value) { _parent = value; }

    // Source: Ghidra get_invalidConNum.c  RVA 0x15AE2C4 — returns field@0x2C.
    public int get_invalidConNum() { return _invalidConNum; }

    // Source: Ghidra set_invalidConNum.c  RVA 0x15AE2CC — stores value at field@0x2C.
    public void set_invalidConNum(int value) { _invalidConNum = value; }

    // Source: Ghidra get_Invalid.c  RVA 0x15AE2D4 — returns field@0x28 (byte).
    public bool get_Invalid() { return _Invalid; }

    // Source: Ghidra set_Invalid.c  RVA 0x15AE2DC — stores value & 1 at field@0x28.
    public void set_Invalid(bool value) { _Invalid = value; }

    // Source: Ghidra get_Connections.c  RVA 0x15AE2E8 — returns field@0x10.
    public List<PathNodeRealTime> get_Connections() { return connections; }

    // Source: Ghidra get_Position.c  RVA 0x15AE2F0 — returns 8 bytes at field@0x20 (Vector2).
    public Vector2 get_Position() { return position; }

    // Source: Ghidra set_Position.c  RVA 0x15AE2F8 — stores Vector2 at field@0x20.
    public void set_Position(Vector2 value) { position = value; }

    // Source: Ghidra DistanceTo.c  RVA 0x15AE300
    // Euclidean distance: sqrt(dx*dx + dy*dy) between this.position and goal.position.
    public float DistanceTo(PathNodeRealTime goal)
    {
        if (goal == null) throw new System.NullReferenceException();
        float dx = position.x - goal.position.x;
        float dy = position.y - goal.position.y;
        return (float)System.Math.Sqrt(dx * dx + dy * dy);
    }

    // Source: Ghidra get_gn.c  RVA 0x15AE36C — returns field@0x30.
    public float get_gn() { return _gn; }

    // Source: Ghidra set_gn.c  RVA 0x15AE374 — stores value at field@0x30.
    public void set_gn(float value) { _gn = value; }

    // Source: Ghidra get_fn.c  RVA 0x15AE37C — returns _gn + _hn (offsets 0x30 + 0x34).
    public float get_fn() { return _gn + _hn; }

    // Source: Ghidra sethn.c  RVA 0x15AE388
    // _hn = sqrt(dx*dx + dy*dy) where dx,dy = position deltas to goal.
    public void sethn(PathNodeRealTime goal)
    {
        if (goal == null) throw new System.NullReferenceException();
        float dx = position.x - goal.position.x;
        float dy = position.y - goal.position.y;
        _hn = (float)System.Math.Sqrt(dx * dx + dy * dy);
    }

    // Source: Ghidra getClientPos.c  RVA 0x15AE418
    // Returns Vector3 — Ghidra signature shows only s0 (x). Inferred convention:
    // x = position.x, y = WrdFileMgr.Instance.getMapHeight(), z = position.y. NRE if singleton null.
    public Vector3 getClientPos()
    {
        WrdFileMgr inst = WrdFileMgr.Instance;
        if (inst == null) throw new System.NullReferenceException();
        float h = inst.getMapHeight();
        return new Vector3(position.x, h, position.y);
    }

    // Source: Ghidra getServerPos.c  RVA 0x15AE460 — returns Vector2(position.x * 64, position.y * 64).
    public Vector2 getServerPos()
    {
        return new Vector2(position.x * 64f, position.y * 64f);
    }

    // Source: Ghidra (no .ctor.c) — default ctor.
    public PathNodeRealTime() { }

    // Explicit interface implementation — delegate to public method form.
    List<PathNodeRealTime> IPathNode<PathNodeRealTime>.Connections => get_Connections();
    Vector2 IPathNode<PathNodeRealTime>.Position => get_Position();
    bool IPathNode<PathNodeRealTime>.Invalid => get_Invalid();
    int IPathNode<PathNodeRealTime>.invalidConNum => get_invalidConNum();
    float IPathNode<PathNodeRealTime>.gn { get => get_gn(); set => set_gn(value); }
    float IPathNode<PathNodeRealTime>.fn => get_fn();
}
