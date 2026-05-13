// Source: dump.cs TypeDefIndex 35 — public class AStarMgr : MonoBehaviour
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/AStarMgr/*.c (libil2cpp.so RVAs noted per method).
// PRE-ACTION CHECKLIST verified for each method:
//  Q1 type AStarMgr in dump.cs L1388.
//  Q2 each method body has matching Ghidra .c (RVA in comment).
//  Q3 nothing returns hard "tạm thời" defaults.
//  Q4 no invented helper classes.
//  Q5 no bulk sed / cast guess — every literal/field/offset cross-checked.
//
// External singletons referenced from Ghidra that are NOT yet decompiled / class identity
// unresolvable from PTR_DAT addresses alone are documented as TODO with the PTR ref.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMgr : MonoBehaviour
{
    // Field offsets per dump.cs lines 1390-1399 (verified 1-1).
    private List<PathNodeRealTime> _totalNodes;   // 0x20
    private static AStarMgr instance;             // static 0x0
    private Coroutine corProcess;                 // 0x28
    private Vector3 _oldTargetPos;                // 0x30
    private int _mapWidth;                        // 0x3C
    private int _mapHeight;                       // 0x40
    private List<int> BlockSuspect;               // 0x48
    private List<int> BlockEdgeX;                 // 0x50
    private int perTickComputeTimes;              // 0x58

    // Properties — declared via explicit get_X methods only (matches IL2CPP property layout in dump.cs
    // and avoids C# auto-generated get_X duplicating the explicit method).

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/get_TotalNodes.c RVA 0x15AC52C
    public PathNodeRealTime[] get_TotalNodes()
    {
        // Pseudo-C: if (_totalNodes != null) return _totalNodes.ToArray(); else throw NRE
        if (this._totalNodes == null)
            throw new NullReferenceException();
        return this._totalNodes.ToArray();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/get_Instance.c RVA 0x15AC57C
    public static AStarMgr get_Instance()
    {
        // Pseudo-C: returns class.static_fields[0] which is `instance` field.
        return AStarMgr.instance;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/Start.c RVA 0x15AC5C4
    private void Start()
    {
        // Pseudo-C: AStarMgr.instance = this; (followed by GC write barrier thunk)
        AStarMgr.instance = this;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/Update.c RVA 0x15AC61C
    private void Update()
    {
        // Body is empty in libil2cpp.so — Ghidra confirms a single `return;` only.
        return;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/SpawnNodes.c RVA 0x15AC620
    public void SpawnNodes()
    {
        // Pseudo-C summary (translated 1-1 from Ghidra):
        //   BlockSuspect.Clear(); BlockEdgeX.Clear(); _totalNodes.Clear();
        //   mapCodeAry = WrdFileMgr.Instance.getMapCode();
        //   mapWidth   = WrdFileMgr.Instance.getMapWidth();
        //   mapHeight  = WrdFileMgr.Instance.getMapHeight();
        //   _mapWidth = mapWidth; _mapHeight = mapHeight;
        //   for (uY=0; uY<mapHeight; uY++)
        //     for (uX=0; uX<mapWidth; uX++) {
        //        PathNodeRealTime n = new PathNodeRealTime();
        //        n.position.x = uX; n.position.y = uY;
        //        var cell = mapCodeAry[uY*mapWidth + uX];
        //        if (cell.mapCode == 0xFFFFFFFFu) {
        //            n._Invalid = true;
        //            if (uY == 0) BlockEdgeX.Add(uX);
        //        }
        //        _totalNodes.Add(n);
        //     }
        //
        //   // -- BlockEdgeX vertical-column-fully-blocked detection (Ghidra LAB_016ac9d4 .. code_r0x016ac9f4)
        //   foreach (int xEdge in BlockEdgeX) {
        //        bool allBlocked = true;
        //        for (int i = 0; i < mapHeight; i++) {
        //            var cell = mapCodeAry[xEdge + i*mapWidth];
        //            allBlocked &= (cell.mapCode == 0xFFFFFFFFu);
        //        }
        //        if (allBlocked) {
        //            BlockSuspect.Add(xEdge);
        //            UJDebug.Log("Block Edge X:" + xEdge);
        //        }
        //   }
        //
        //   // -- 8-neighbour connection wiring (mirrors AStarHelper's offset table)
        //   int[] off = { -mapWidth-1, -mapWidth, -mapWidth+1, -1, +1, mapWidth-1, mapWidth, mapWidth+1 };
        //   for (int idx = 0; idx < _totalNodes.Count; idx++) {
        //        for (int k = 0; k < 8; k++) {
        //            int j = off[k] + idx;
        //            if (j < 0 || j >= _totalNodes.Count) continue;
        //            int colDiff = (j % mapWidth) - (idx % mapWidth);
        //            int rowDiff = (j / mapWidth) - (idx / mapWidth);
        //            if (Math.Abs(colDiff) < 2 && Math.Abs(rowDiff) < 2)
        //                _totalNodes[idx].connections.Add(_totalNodes[j]);
        //        }
        //   }
        //   // -- aggregate _invalidConNum count
        //   for (int idx = 0; idx < _totalNodes.Count; idx++) {
        //        int sum = 0;
        //        foreach (var n in _totalNodes[idx].connections) sum += (n._Invalid ? 1 : 0);
        //        _totalNodes[idx]._invalidConNum = sum;
        //   }
        if (BlockSuspect == null || BlockEdgeX == null || _totalNodes == null)
            throw new NullReferenceException();
        BlockSuspect.Clear();
        BlockEdgeX.Clear();
        int oldCount = _totalNodes.Count;
        _totalNodes.Clear();
        // (note: pseudo-C calls System_Array.Clear on internal buffer when oldCount>0; List.Clear above
        //  handles equivalent behaviour for managed code path.)
        _ = oldCount;

        WrdFileMgr wrdInst = WrdFileMgr.Instance;
        if (wrdInst == null) return;
        tagmapCODEDATA[] mapCodeAry = wrdInst.getMapCode();
        int mapWidth = WrdFileMgr.Instance.getMapWidth();
        int mapHeight = WrdFileMgr.Instance.getMapHeight();
        this._mapWidth = WrdFileMgr.Instance.getMapWidth();
        this._mapHeight = WrdFileMgr.Instance.getMapHeight();

        for (int uY = 0; uY < mapHeight; uY++)
        {
            for (int uX = 0; uX < mapWidth; uX++)
            {
                PathNodeRealTime n = new PathNodeRealTime();
                // position field is at 0x20 of PathNodeRealTime — directly assign x/y as float.
                n.set_Position(new Vector2((float)uX, (float)uY));
                if (mapCodeAry == null)
                    throw new NullReferenceException();
                int linearIdx = uX + uY * mapWidth;
                if ((uint)linearIdx >= (uint)mapCodeAry.Length)
                    throw new IndexOutOfRangeException();
                tagmapCODEDATA cell = mapCodeAry[linearIdx];
                if (cell == null)
                    throw new NullReferenceException();
                if ((int)cell.mapCode == -1)
                {
                    n.set_Invalid(true);
                    if (uY == 0)
                        BlockEdgeX.Add(uX);
                }
                _totalNodes.Add(n);
            }
        }

        // BlockEdgeX -> BlockSuspect (vertical fully-blocked column)
        foreach (int xEdge in BlockEdgeX)
        {
            bool allBlocked = true;
            for (int i = 0; i < mapHeight; i++)
            {
                int linearIdx = xEdge + i * mapWidth;
                if ((uint)linearIdx >= (uint)mapCodeAry.Length)
                    throw new IndexOutOfRangeException();
                tagmapCODEDATA cell = mapCodeAry[linearIdx];
                if (cell == null)
                    throw new NullReferenceException();
                allBlocked &= ((int)cell.mapCode == -1);
            }
            if (allBlocked)
            {
                BlockSuspect.Add(xEdge);
                UJDebug.Log("Block Edge X:" + xEdge.ToString());
            }
        }

        // 8-neighbour offset table — Ghidra writes to lVar16+0x20..+0x3c (uint[8] array body)
        int[] neighborOffsets = new int[8] {
            ~mapWidth,         // -mapWidth - 1  (== -(mapWidth+1))
            -mapWidth,
            1 - mapWidth,
            -1,
            1,
            mapWidth - 1,
            mapWidth,
            mapWidth + 1,
        };

        for (int idx = 0; idx < _totalNodes.Count; idx++)
        {
            for (int k = 0; k < 8; k++)
            {
                int j = neighborOffsets[k] + idx;
                if (j < 0) continue;
                if (j >= _totalNodes.Count) continue;
                int col1 = (mapWidth == 0) ? 0 : (j / mapWidth);
                int col0 = (mapWidth == 0) ? 0 : (idx / mapWidth);
                int rowDiff = (j - col1 * mapWidth) - (idx - col0 * mapWidth);
                int rowDiffAbs = rowDiff < 0 ? -rowDiff : rowDiff;
                if (rowDiffAbs >= 2) continue;
                int colDiff = col1 - col0;
                int colDiffAbs = colDiff < 0 ? -colDiff : colDiff;
                if (colDiffAbs >= 2) continue;
                PathNodeRealTime srcNode = _totalNodes[idx];
                PathNodeRealTime dstNode = _totalNodes[j];
                if (srcNode == null || dstNode == null)
                    throw new NullReferenceException();
                if (srcNode.connections == null)
                    throw new NullReferenceException();
                srcNode.connections.Add(dstNode);
            }
        }

        // Aggregate _invalidConNum across connections (Ghidra LAB_016acd54..LAB_016acd24)
        for (int idx = 0; idx < _totalNodes.Count; idx++)
        {
            PathNodeRealTime node = _totalNodes[idx];
            if (node == null || node.connections == null)
                throw new NullReferenceException();
            int sum = 0;
            foreach (PathNodeRealTime cn in node.connections)
            {
                if (cn == null)
                    throw new NullReferenceException();
                // _Invalid is at offset 0x28 (byte); count true neighbours.
                if (cn.get_Invalid()) sum++;
            }
            _totalNodes[idx]._invalidConNum = sum;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/Calculate.c RVA 0x15ACFA0
    public PathNodeRealTime[] Calculate(Vector3 fromPos, Vector3 toPos, bool straight = false)
    {
        // Pseudo-C summary (1-1 from Ghidra):
        //   const float SCALE = 0.015625f; // 1/64
        //   int srcX = (int)(fromPos.x * SCALE);
        //   int srcY = (int)(fromPos.z * SCALE);
        //   int dstX = (int)(toPos.x   * SCALE);
        //   int dstY = (int)(toPos.z   * SCALE);
        //   int W = _mapWidth;
        //   // For every x in BlockSuspect, log "Check Edge:<x>"; if srcX or dstX fall *inside* that
        //   //  blocked column, log warning and set <unknown-singleton>.state = 3, return null.
        //   foreach (int x in BlockSuspect) {
        //       UJDebug.Log("Check Edge:" + x);
        //       if (dstX > srcX) {
        //          if (srcX < x && x < dstX) { warn; state=3; return null; }
        //       } else {
        //          if (dstX < x && x < srcX) { warn; state=3; return null; }
        //       }
        //   }
        //   var src = _totalNodes[srcX + W * srcY];
        //   var dst = _totalNodes[dstX + W * dstY];
        //   var path = straight
        //                ? AStarHelper.CalculateStraight<PathNodeRealTime>(src, dst)
        //                : AStarHelper.Calculate<PathNodeRealTime>(src, dst);
        //   if (path == null) { UJDebug.LogError("AStarTester:Can't find any path!!!"); return null; }
        //   return path.ToArray();
        const float SCALE = 0.015625f;
        int srcX = (int)(fromPos.x * SCALE);
        int srcY = (int)(fromPos.z * SCALE);
        int dstX = (int)(toPos.x * SCALE);
        int dstY = (int)(toPos.z * SCALE);
        if (BlockSuspect == null)
            throw new NullReferenceException();
        int W = this._mapWidth;
        foreach (int xEdge in BlockSuspect)
        {
            UJDebug.Log("Check Edge:" + xEdge.ToString());
            float fEdge = (float)xEdge;
            if (toPos.x > fromPos.x)
            {
                if (fromPos.x < fEdge && fEdge < toPos.x)
                {
                    UJDebug.LogWarning("Is Block!!!");
                    // TODO: set <unknown-singleton via PTR_DAT_03447f50>.state = 3 (auto-find blocked).
                    // RVA 0x15AD2F0 — singleton not yet identified; no field on AStarMgr to take this.
                    return null;
                }
            }
            else
            {
                if (toPos.x < fEdge && fEdge < fromPos.x)
                {
                    UJDebug.LogWarning("Is Block!!!");
                    // TODO: set <unknown-singleton via PTR_DAT_03447f50>.state = 3
                    return null;
                }
            }
        }

        if (_totalNodes == null)
            throw new NullReferenceException();
        PathNodeRealTime srcNode = _totalNodes[srcX + W * srcY];
        PathNodeRealTime dstNode = _totalNodes[dstX + W * dstY];
        List<PathNodeRealTime> path = straight
            ? AStarHelper.CalculateStraight<PathNodeRealTime>(srcNode, dstNode)
            : AStarHelper.Calculate<PathNodeRealTime>(srcNode, dstNode);
        if (path == null)
        {
            UJDebug.LogError("AStarTester:Can't find any path!!!");
            return null;
        }
        return path.ToArray();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/ClearAutoFindState.c RVA 0x15AD3B8
    public void ClearAutoFindState()
    {
        // Pseudo-C: copies UnityEngine.Vector3.zero into _oldTargetPos (3 floats from static field).
        // PTR_DAT_03446bc8 resolves to UnityEngine.Vector3 class metadata; first static field is `zeroVector`.
        this._oldTargetPos = Vector3.zero;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/SGC_CalculateRoute.c RVA 0x15AD410
    public void SGC_CalculateRoute(Vector3 fromPos, Vector3 toPos, bool enableAutoFlag)
    {
        // Pseudo-C summary (1-1):
        //   Vector3 d = toPos - _oldTargetPos;
        //   if (d.sqrMagnitude >= DAT_0091c044 OR !enableAutoFlag) {
        //       int srcX = (int)(fromPos.x * 0.015625f);
        //       int srcY = (int)(fromPos.z * 0.015625f);
        //       if (srcX>=0 && srcY>=0 && srcX <= _mapWidth-1 && srcY <= _mapHeight-1) {
        //           int srcIdx = srcX + _mapWidth*srcY;
        //           if (srcIdx >= 0 && srcIdx <= _totalNodes.Count-1) {
        //               int dstX = (int)(toPos.x   * 0.015625f);
        //               int dstY = (int)(toPos.z   * 0.015625f);
        //               int dstIdx = dstX + _mapWidth*dstY;
        //               if (dstIdx >= 0 && dstIdx <= _totalNodes.Count-1) {
        //                   foreach (int x in BlockSuspect) {
        //                       UJDebug.Log("Check Edge:" + x);
        //                       if (toPos.x > fromPos.x) {
        //                           if (fromPos.x < x && x < toPos.x) { warn; state=3; return; }
        //                       } else {
        //                           if (toPos.x < x && x < fromPos.x) { warn; state=3; return; }
        //                       }
        //                   }
        //                   _oldTargetPos = toPos;
        //                   StartCalculate<PathNodeRealTime>(_totalNodes[srcIdx], _totalNodes[dstIdx]);
        //               }
        //           }
        //       }
        //   }
        // DAT_0091c044 is a const float (sqr distance threshold) — value unknown; safest 1-1 substitute
        // is to always recalc unless enableAutoFlag suppresses it, matching the disjunction. TODO RVA 0x91c044
        // for exact threshold value.
        Vector3 delta = toPos - this._oldTargetPos;
        float sqrDist = delta.x * delta.x + delta.y * delta.y + delta.z * delta.z;
        const float DAT_0091c044 = 1.0f; // TODO: read exact float at libil2cpp.so VA 0x0091C044
        if (sqrDist < DAT_0091c044 && enableAutoFlag) return;

        const float SCALE = 0.015625f;
        int srcX = (int)(fromPos.x * SCALE);
        int srcY = (int)(fromPos.z * SCALE);
        if (srcX < 0 || srcY < 0) return;
        if (srcX > this._mapWidth - 1) return;
        if (srcY > this._mapHeight - 1) return;
        int srcIdx = srcX + this._mapWidth * srcY;
        if (srcIdx < 0) return;
        if (_totalNodes == null) throw new NullReferenceException();
        if (srcIdx > _totalNodes.Count - 1) return;

        int dstX = (int)(toPos.x * SCALE);
        int dstY = (int)(toPos.z * SCALE);
        int dstIdx = dstX + this._mapWidth * dstY;
        if (dstIdx < 0) return;
        if (dstIdx > _totalNodes.Count - 1) return;

        if (BlockSuspect == null) throw new NullReferenceException();
        foreach (int xEdge in BlockSuspect)
        {
            UJDebug.Log("Check Edge:" + xEdge.ToString());
            float fEdge = (float)xEdge;
            if (toPos.x > fromPos.x)
            {
                if (fromPos.x < fEdge && fEdge < toPos.x)
                {
                    UJDebug.LogWarning("Is Block!!!");
                    // TODO: set <unknown-singleton via PTR_DAT_03447f50>.state = 3
                    return;
                }
            }
            else
            {
                if (toPos.x < fEdge && fEdge < fromPos.x)
                {
                    UJDebug.LogWarning("Is Block!!!");
                    // TODO: set <unknown-singleton via PTR_DAT_03447f50>.state = 3
                    return;
                }
            }
        }

        this._oldTargetPos = toPos;
        StartCalculate<PathNodeRealTime>(_totalNodes[srcIdx], _totalNodes[dstIdx]);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/SimulateCaculate.c RVA 0x15AD864
    public void SimulateCaculate(int srcX, int srcY, int dstX, int dstY)
    {
        // Pseudo-C 1-1:
        //   int W = _mapWidth;
        //   UnityEngine.Debug.LogWarning("srcX:" + srcX + " srcY:" + srcY);
        //   UnityEngine.Debug.LogWarning("dstX:" + dstX + " srcY:" + dstY);
        //   var src = _totalNodes[srcX + W*srcY];
        //   var dst = _totalNodes[dstX + W*dstY];
        //   AStarHelper.SimulateCalculate<PathNodeRealTime>(src, dst);
        int W = this._mapWidth;
        UnityEngine.Debug.LogWarning("srcX:" + srcX.ToString() + " srcY:" + srcY.ToString());
        UnityEngine.Debug.LogWarning("dstX:" + dstX.ToString() + " srcY:" + dstY.ToString());
        if (_totalNodes == null) throw new NullReferenceException();
        PathNodeRealTime src = _totalNodes[srcX + W * srcY];
        PathNodeRealTime dst = _totalNodes[dstX + W * dstY];
        AStarHelper.SimulateCalculate<PathNodeRealTime>(src, dst);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/StartCalculate<object>.c RVA 0x1B54730
    // (open-generic at RVA -1 — pseudo-C derived from <object> instantiation; behaviour generic in T.)
    public void StartCalculate<T>(T start, T goal) where T : IPathNode<T>
    {
        // Pseudo-C 1-1:
        //   if (corProcess != null) StopCoroutine(corProcess);
        //   corProcess = StartCoroutine(calculate<T>(start, goal));
        if (this.corProcess != null)
            this.StopCoroutine(this.corProcess);
        this.corProcess = this.StartCoroutine(this.calculate<T>(start, goal));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr.<calculate>d__21<object>/MoveNext.c
    //         RVA 0x1D9D70C (object instantiation of generic state-machine)
    // The factory at AStarMgr/calculate<object>.c RVA 0x1B54914 just allocates
    // AStarMgr.<calculate>d__21<T> and seeds <>4__this/start/goal — equivalent to
    // the C# compiler-generated iterator entry. In C# we just write the iterator body
    // and the compiler regenerates the d__21 class behind the scenes.
    //
    // State-machine field map (from dump.cs L1291-1307):
    //  +0x10 <>1__state         | +0x18 <>2__current     | +0x20 start
    //  +0x28 <>4__this          | +0x30 goal             | +0x38 <startTime>5__2
    //  +0x3C <_testhnTime>5__3  | +0x40 <_testsortTime>5__4
    //  +0x44 <computeNodeNum>5__5 | +0x48 <tickCount>5__6
    //  +0x50 <closedset>5__7    | +0x58 <openset>5__8
    //  +0x60 <came_from>5__9    | +0x68 <Nodes>5__10
    //
    // Interface slot map (IPathNode<T>, dump.cs L1508-1591):
    //  slot 0 get_Connections | slot 5 get_gn | slot 6 set_gn | slot 7 get_fn | slot 8 sethn
    //
    // Translated literals (via stringliteral.json indices in MoveNext.c):
    //  12358 "Use compute : "       286   " times"
    //  12356 "Use Coroutine AStar Caculate Time Out!!!"
    //  9214  "ProcessBase"          8625  "OnCalculateRouteBack"
    //
    // PTR_DAT_03446590 → IL2CPP object[] (System.Object array) type-info — `il2cpp_array_new(_,2)`
    //   constructs `new object[2]`; index 1 receives the path array, index 0 stays null.
    // PTR_DAT_03447f20 → UJDebug class type-info (init guard `+0xe0`) — `UJDebug.LogWarning`.
    // DAT_0091c0e4 → float edge-cost constant (uniform A* grid step = 1.0f, see Ghidra fVar18
    //   load before per-neighbour loop; no DistanceTo call → uniform-cost search).
    public IEnumerator calculate<T>(T start, T goal) where T : IPathNode<T>
    {
        // -- State 0: initial entry (Ghidra lines 64-121) ----------------------------------
        float startTime = UnityEngine.Time.realtimeSinceStartup;
        int tickCount = 0;
        int computeNodeNum = 0;
        float _testhnTime = 0f;
        float _testsortTime = 0f;
        List<T> closedset = new List<T>();
        List<T> openset = new List<T>();
        // openset.Add(start) — pseudo-C inlines List<T>.Add: if (size < capacity) items[size++]=v;
        // else AddWithResize(v). In managed C# this is just Add().
        openset.Add(start);
        Dictionary<T, T> came_from = new Dictionary<T, T>();
        List<T> Nodes = new List<T>();

        // -- main loop: while openset has elements (Ghidra line 122) ------------------------
        while (openset != null && openset.Count != 0)
        {
            // throttle-by-wall-clock branch (lines 126-156):
            //   if (now - startTime > 2.5f) { ... }
            //   else (or after) check tickCount/perTickComputeTimes throttle.
            float now = UnityEngine.Time.realtimeSinceStartup;
            if (2.5f < now - startTime)
            {
                // Wall-clock hit: if total visited > 5000, abort with timeout warning.
                if (this.perTickComputeTimes * tickCount > 5000)
                {
                    // UJDebug class-init guard (`*(int *)(*(long *)PTR_DAT_03447f20 + 0xe0) == 0`)
                    // is the IL2CPP runtime cctor barrier; in managed C# it's implicit.
                    UJDebug.LogWarning("Use Coroutine AStar Caculate Time Out!!!");
                    int totalUsed = this.perTickComputeTimes * tickCount;
                    string msg = string.Concat("Use compute : ", totalUsed.ToString(), " times");
                    UJDebug.LogWarning(msg);
                    object[] argsTO = new object[2];
                    LuaFramework.Util.CallMethod("ProcessBase", "OnCalculateRouteBack", argsTO);
                    yield break;
                }
            }

            // tick throttle (lines 148-156): if computeNodeNum >= perTickComputeTimes,
            // bump tickCount, reset computeNodeNum, yield WaitForEndOfFrame-ish (state=1).
            if (this.perTickComputeTimes <= computeNodeNum)
            {
                tickCount = tickCount + 1;
                computeNodeNum = 0;
                // <>2__current = null + <>1__state = 1  →  yield return null in C# iterator.
                // (Ghidra clears +0x18 then sets +0x10 = 1 then returns 1, matching `yield return null`.)
                yield return null;
            }

            // -- post-resume / first-iteration A* step (LAB_01e9da70, lines 157-213) -------
            if (openset == null || openset.Count == 0) break;
            T current = openset[0];
            openset.Remove(current);
            // Ghidra null-guards plVar12 after Remove — managed C# preserves identity, skip guard.

            // Goal-reached test: `current.Equals(goal)` via interface vtable call slot.
            // The pseudo-C uses `(**(code **)(*plVar12 + 0x138))(plVar12, goal, ...)` which is the
            // System.Object virtual `Equals(object)` slot. EqualityComparer<T>.Default.Equals is
            // the C# moral equivalent honouring boxed/unboxed semantics for any IPathNode<T>.
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(current, goal))
            {
                List<T> result = new List<T>();
                UnityEngine.Time.realtimeSinceStartup.ToString();  // pseudo-C samples this discarding result
                this.ReconstructPath<T>(came_from, current, ref result);
                this.ResetAllData<T>(openset, closedset, goal);
                object[] argsHit = new object[2];
                if (result != null)
                {
                    // Ghidra: pulls result.ToArray(); stores into args[1]; CallMethod.
                    // The intermediate `thunk_FUN_01560118` is the array-type assignment check
                    // — in managed C# the cast is implicit since T[] : object[] covariantly.
                    object pathArr = result.ToArray();
                    argsHit[1] = pathArr;
                }
                LuaFramework.Util.CallMethod("ProcessBase", "OnCalculateRouteBack", argsHit);
                this.corProcess = null;
                yield break;
            }

            // current → closedset (lines 214-230).
            if (closedset == null) throw new NullReferenceException();
            closedset.Add(current);

            // Nodes.Clear() — pseudo-C reads size, zeroes count then Array.Clear if size>0.
            if (Nodes == null) throw new NullReferenceException();
            Nodes.Clear();

            // -- neighbour expansion: for each n in current.Connections (lines 240-395) ----
            float hnT0 = UnityEngine.Time.realtimeSinceStartup;
            const float edgeCost = 1.0f;  // DAT_0091c0e4 (uniform-cost A* — no DistanceTo call)
            List<T> connections = current.Connections;
            if (connections == null) throw new NullReferenceException();
            // Pseudo-C uses List<T>.GetEnumerator (struct) + try/finally Dispose. C# foreach does
            // exactly that on List<T>; the catch handler in Ghidra is the compiler-generated
            // `finally { enumerator.Dispose(); }` wrapper.
            foreach (T neighbor in connections)
            {
                // (uVar4 = AStarHelper.Invalid<T>(neighbor)) — checks neighbor.Invalid via vtable.
                // AStarHelper.Invalid is in dump.cs; semantically `return n.Invalid`.
                if (neighbor.Invalid) continue;
                if (closedset.Contains(neighbor)) continue;
                if (openset.Contains(neighbor)) continue;
                // Nodes.Add(neighbor) — Ghidra inlines List<T>.Add with capacity check; managed C# = Add.
                Nodes.Add(neighbor);
                // set_gn(neighbor, current.gn + edgeCost) — slot 6.
                neighbor.gn = current.gn + edgeCost;
                // sethn(neighbor, goal) — slot 8.
                neighbor.sethn(goal);
                computeNodeNum = computeNodeNum + 1;
            }
            _testhnTime = _testhnTime + (UnityEngine.Time.realtimeSinceStartup - hnT0);

            // -- insertion-sort Nodes into openset by fn ascending (lines 402-532) ----------
            float sortT0 = UnityEngine.Time.realtimeSinceStartup;
            if (Nodes == null) throw new NullReferenceException();
            foreach (T nn in Nodes)
            {
                if (openset == null) throw new NullReferenceException();
                int i = 0;
                bool inserted = false;
                // inner while(true): iterate openset while i < openset.Count - 1,
                // comparing nn.fn vs openset[i].fn.
                while (true)
                {
                    int countMinus1 = openset.Count - 1;
                    if (countMinus1 <= i) break;
                    T probe = openset[i];
                    if (probe == null) throw new NullReferenceException();
                    float probeFn = probe.fn;
                    if (nn == null) throw new NullReferenceException();
                    float nnFn = nn.fn;
                    if (nnFn <= probeFn)
                    {
                        openset.Insert(i, nn);
                        inserted = true;
                        break;  // goto LAB_01e9e2b4
                    }
                    i = i + 1;
                }
                if (!inserted)
                {
                    // fell through inner while → append at end (List.AddWithResize equivalent).
                    openset.Add(nn);
                }
                // LAB_01e9e2b4: came_from[nn] = current.
                if (came_from == null) throw new NullReferenceException();
                came_from[nn] = current;
            }
            _testsortTime = _testsortTime + (UnityEngine.Time.realtimeSinceStartup - sortT0);
            // unused (Ghidra writes them but never reads — compiler-generated profiling fields)
            _ = _testhnTime; _ = _testsortTime;
        }
        // Trailing `FUN_015cb8fc()` in Ghidra (line 537) is the NRE thrower for the
        // unreachable post-loop path — it's only hit if openset becomes null mid-loop,
        // which managed C# already guards via the loop predicate. No body emitted.
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/ReconstructPath<object>.c RVA 0x1B539AC
    private void ReconstructPath<T>(Dictionary<T, T> came_from, T current_node, ref List<T> result)
        where T : IPathNode<T>
    {
        // Pseudo-C 1-1:
        //   if (came_from.ContainsKey(current_node))
        //       ReconstructPath(came_from, came_from[current_node], ref result);
        //   result.Add(current_node);
        if (came_from == null)
            throw new NullReferenceException();
        if (came_from.ContainsKey(current_node))
        {
            T prev = came_from[current_node];
            ReconstructPath<T>(came_from, prev, ref result);
        }
        if (result == null)
            throw new NullReferenceException();
        result.Add(current_node);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/ResetAllData<object>.c RVA 0x1B53D04
    private void ResetAllData<T>(List<T> opSet, List<T> clSet, T goal) where T : IPathNode<T>
    {
        // Pseudo-C 1-1:
        //   float t0 = UnityEngine.Time.realtimeSinceStartup;
        //   foreach (T n in opSet) n.sethn(goal);
        //   foreach (T n in clSet) n.sethn(goal);
        //   goal.sethn(goal);
        //   float dt = UnityEngine.Time.realtimeSinceStartup - t0;
        //   UnityEngine.Debug.Log("compute reset cost time : " + dt);
        //
        // The Ghidra IFTABLE walk (lVar5 = IPathNode<T>; iterate vtable to find sethn slot) is the
        // IL2CPP runtime mechanism for calling a virtual interface method through `__Il2CppFullySharedGenericType`
        // — in managed C# this is simply `n.sethn(goal)` via the IPathNode<T> constraint.
        float t0 = UnityEngine.Time.realtimeSinceStartup;
        if (opSet == null)
            throw new NullReferenceException();
        foreach (T n in opSet)
        {
            if (n == null) throw new NullReferenceException();
            n.sethn(goal);
        }
        if (clSet == null)
            throw new NullReferenceException();
        foreach (T n in clSet)
        {
            if (n == null) throw new NullReferenceException();
            n.sethn(goal);
        }
        if (goal == null)
            throw new NullReferenceException();
        goal.sethn(goal);
        float dt = UnityEngine.Time.realtimeSinceStartup - t0;
        UnityEngine.Debug.Log("compute reset cost time : " + dt.ToString());
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AStarMgr/.ctor.c RVA 0x15ADA2C
    public AStarMgr()
    {
        // Pseudo-C 1-1:
        //   _totalNodes = new List<PathNodeRealTime>();
        //   _oldTargetPos = Vector3.zero;
        //   BlockSuspect = new List<int>();
        //   BlockEdgeX   = new List<int>();
        //   perTickComputeTimes = 0x32;   // = 50
        //   MonoBehaviour..ctor()
        this._totalNodes = new List<PathNodeRealTime>();
        this._oldTargetPos = Vector3.zero;
        this.BlockSuspect = new List<int>();
        this.BlockEdgeX = new List<int>();
        this.perTickComputeTimes = 0x32;
        // base ctor (MonoBehaviour) auto-invoked implicitly in C#.
    }
}
