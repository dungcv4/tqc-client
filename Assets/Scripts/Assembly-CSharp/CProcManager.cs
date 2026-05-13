// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x017b8b4c, 0x017b8b64, 0x01bca3b4, 0x017b8b6c, 0x017b8f28, 0x017b9010,
//       0x017b901c, 0x017b902c, 0x017b9324, 0x017b94b0, 0x017b9514, 0x017b9520,
//       0x017b953c, 0x017b9558, 0x017b95d0, 0x017b95d8, 0x017b9630, 0x017b9644,
//       0x017b9658, 0x017b965c, 0x017b9660
// Ghidra dir: work/06_ghidra/decompiled_full/CProcManager/
//
// Bodies ported 1-1 from Ghidra batch decompile (libil2cpp.so image base 0x100000).
// Field offsets verified against dump.cs (TypeDefIndex 109):
//   cbSwitchEventFunc       0x10  (CBSwitchEventFunc delegate)
//   _cachedProcs            0x18  (Dictionary<EProcID, CBaseProc>)
//   _stackProcs             0x20  (Stack<StackProc>)
//   _curProc                0x28  (CBaseProc)
//   _eChangeStyle           0x30  (EStyle — switch-pending state, 0=None,1=Switch,2=Push,3=Pop)
//   _eChangeProcID          0x34  (EProcID — pending target ProcID)
//   _changeProcArgs         0x38  (ArrayList — args for the pending Enter)
// CBaseProc field offsets (TypeDefIndex 95) used via virtual dispatch:
//   _procMgr   0x10
//   _eProcID   0x18
//   _bCached   0x1C
// CBaseProc vtable slots referenced by Ghidra:
//   slot 4 = V_Enter / OnApplicationQuit (off +0x178)   [actually OnApplicationQuit Slot:4]
//   slot 5 = V_Destroy (off +0x188)
// CProcManager vtable slots:
//   slot 4 = V_Start  (off +0x178)
//   slot 5 = V_Destroy (off +0x188)
// Thread-init macros (DAT_03683...) are skipped — they only initialize cached MethodInfo* pointers.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProcManager
{
    public CBSwitchEventFunc cbSwitchEventFunc;
    private Dictionary<EProcID, CBaseProc> _cachedProcs;
    private Stack<StackProc> _stackProcs;
    private CBaseProc _curProc;
    private EStyle _eChangeStyle;
    private EProcID _eChangeProcID;
    private ArrayList _changeProcArgs;

    // RVA 0x017b8b4c — get_eCurProcID 1-1 from Ghidra
    //   if (_curProc != 0) return *(int*)(_curProc + 0x18);   // _eProcID
    //   return 0;
    public EProcID eCurProcID
    {
        get { return _curProc != null ? _curProc.eProcID : (EProcID)0; }
    }

    // RVA 0x017b8b64 — get_curProc 1-1 from Ghidra
    //   return *(undefined8 *)(this + 0x28);
    public CBaseProc curProc { get { return _curProc; } }

    // RVA 0x01bca3b4 — getCurProc<object> 1-1 from Ghidra
    //   long lVar4 = *(long *)(this + 0x28);
    //   if (lVar4 == 0) return 0;
    //   long lVar1 = thunk_FUN_01560118(lVar4, T_class);   // = (T)_curProc cast
    //   if (lVar1 == 0) FUN_015cbc7c(lVar4, T_class);      // throw InvalidCastException
    //   return lVar1;
    // C# equivalent: hard cast (throws if mismatch). Ghidra's null check returns null when _curProc==null.
    public T getCurProc<T>() where T : CBaseProc
    {
        if (_curProc == null) return null;
        return (T)_curProc;
    }

    // RVA 0x017b8b6c — SetCurProcID 1-1 from Ghidra
    // (Thread-init macros DAT_0368374c skipped.)
    //   plVar5 = &_curProc;
    //   if (_curProc != 0) {
    //     if (_eChangeStyle == 2 /Push/) {
    //       lVar2 = il2cpp_object_new(StackProc_class);
    //       System_Object___ctor(lVar2,0);
    //       puVar6 = &_cachedProcs;
    //       lVar2->_cachedProcs = _cachedProcs;
    //       lVar2->_curProc     = _curProc;
    //       CBaseProc__Leave(_curProc, 0);
    //       if (_stackProcs == 0) goto LAB_018b8ea0; // null deref → throw
    //       Stack<object>__Push(_stackProcs, lVar2, ...);
    //       _cachedProcs = new Dictionary<EProcID, CBaseProc>();
    //     } else {
    //       CBaseProc__Leave(_curProc, 0);
    //       if (*(char*)(_curProc + 0x1c) == '\0')          // !bCached
    //         CBaseProc__Destroy(_curProc, 0);
    //     }
    //     _curProc = 0;
    //   }
    //   if (_eChangeStyle == 3 /Pop/) {
    //     if (_cachedProcs == 0) throw;
    //     foreach (kv in _cachedProcs)
    //       if (kv.Value == 0) throw;  else CBaseProc__Destroy(kv.Value, 0);
    //     _cachedProcs.Clear();
    //     if (_stackProcs == 0) throw;
    //     lVar2 = Stack<object>__Pop(_stackProcs, ...);
    //     if (lVar2 == 0) throw;
    //     _curProc      = lVar2->_curProc;
    //     _cachedProcs  = lVar2->_cachedProcs;
    //     if (_curProc == 0) throw;
    //     CBaseProc__Enter(_curProc, this, _changeProcArgs, 0);
    //   } else {
    //     uVar3 = CProcManager__GetProc(this, _eChangeProcID);
    //     _curProc = uVar3;
    //     if (_curProc == 0) goto LAB_018b8e74;
    //     uVar4 = CBaseProc__Enter(_curProc, this, _changeProcArgs, 0);
    //     if ((uVar4 & 1) == 0) _curProc = 0;
    //   }
    //   if (_curProc != 0 && cbSwitchEventFunc != 0)
    //     ((cbSwitchEventFunc.invoke_impl))(cbSwitchEventFunc.target, _curProc->_eProcID, ...);
    //   _changeProcArgs = 0;
    //   _eChangeProcID = 0;
    private void SetCurProcID()
    {
        if (_curProc != null)
        {
            if (_eChangeStyle == EStyle.Push)
            {
                var sp = new StackProc { _cachedProcs = _cachedProcs, _curProc = _curProc };
                _curProc.Leave();
                if (_stackProcs == null) throw new NullReferenceException("_stackProcs");
                _stackProcs.Push(sp);
                _cachedProcs = new Dictionary<EProcID, CBaseProc>();
            }
            else
            {
                _curProc.Leave();
                if (!_curProc.bCached) _curProc.Destroy();
            }
            _curProc = null;
        }

        if (_eChangeStyle == EStyle.Pop)
        {
            if (_cachedProcs == null) throw new NullReferenceException("_cachedProcs");
            foreach (var kv in _cachedProcs)
            {
                if (kv.Value == null) throw new NullReferenceException("cachedProcs.Value");
                kv.Value.Destroy();
            }
            _cachedProcs.Clear();
            if (_stackProcs == null) throw new NullReferenceException("_stackProcs");
            var sp = _stackProcs.Pop();
            if (sp == null) throw new NullReferenceException("StackProc");
            _curProc = sp._curProc;
            _cachedProcs = sp._cachedProcs;
            if (_curProc == null) throw new NullReferenceException("_curProc after pop");
            _curProc.Enter(this, _changeProcArgs);
        }
        else
        {
            _curProc = GetProc(_eChangeProcID);
            if (_curProc != null)
            {
                if (!_curProc.Enter(this, _changeProcArgs)) _curProc = null;
            }
        }

        if (_curProc != null && cbSwitchEventFunc != null)
        {
            cbSwitchEventFunc(_curProc.eProcID);
        }
        _changeProcArgs = null;
        _eChangeProcID = (EProcID)0;
    }

    // RVA 0x017b8f28 — GetProc 1-1 from Ghidra
    //   if (_cachedProcs == 0) goto LAB_018b900c;   // throw NRE
    //   Dictionary<int, object>__TryGetValue(_cachedProcs, eProcID, &local_28, ...);
    //   if (local_28 == 0) {
    //     /* class-init guard */
    //     local_28 = ProcFactory__CreateProc(eProcID);
    //     if (local_28 != 0 && (*(char*)(local_28 + 0x1c) != '\0')) {     // bCached
    //       if (_cachedProcs == 0) throw;
    //       Dictionary<int, object>__Add(_cachedProcs, eProcID, local_28, ...);
    //     }
    //   }
    //   return local_28;
    private CBaseProc GetProc(EProcID eProcID)
    {
        if (_cachedProcs == null) throw new NullReferenceException("_cachedProcs");
        CBaseProc proc;
        _cachedProcs.TryGetValue(eProcID, out proc);
        if (proc == null)
        {
            proc = ProcFactory.CreateProc(eProcID);
            if (proc != null && proc.bCached)
            {
                _cachedProcs.Add(eProcID, proc);
            }
        }
        return proc;
    }

    // RVA 0x017b9010 — Start(ArrayList) 1-1 from Ghidra
    //   (**(code **)(*this + 0x178))(this, args, ...);   // CProcManager vtable slot 4 = V_Start
    public void Start(ArrayList args) { V_Start(args); }

    // RVA 0x017b901c — Start() 1-1 from Ghidra
    //   (**(code **)(*this + 0x178))(this, 0, ...);
    public void Start() { V_Start(null); }

    // RVA 0x017b902c — Destroy() 1-1 from Ghidra
    // (Thread-init macros DAT_0368374e skipped.)
    //   plVar7 = &_curProc;
    //   if (_curProc != 0) {
    //     CBaseProc__Leave(_curProc, 0);
    //     if (_curProc == 0) goto LAB_018b928c;     // throw
    //     if (*(char*)(_curProc + 0x1c) == '\0')    // !bCached
    //       CBaseProc__Destroy(_curProc, 0);
    //     _curProc = 0;
    //   }
    //   if (_cachedProcs != 0) {
    //     Dictionary<int, object>__GetEnumerator(...);
    //     while (MoveNext) {
    //       if (current == 0) throw;
    //       CBaseProc__Destroy(current, 0);
    //     }
    //     Enumerator__Dispose();
    //     if (_cachedProcs == 0) throw;
    //     Dictionary<int, object>__Clear(_cachedProcs);
    //
    //     lVar5 = _stackProcs;
    //     while (lVar5 != 0) {
    //       if (*(int*)(lVar5 + 0x18) < 1) {                          // Stack count < 1
    //         (**(code **)(*this + 0x188))(this, ...);                // V_Destroy (slot 5)
    //         return;
    //       }
    //       lVar5 = Stack<object>__Pop(lVar5, ...);                   // pops one StackProc
    //       if (lVar5 == 0 || lVar5->_curProc == 0) break;
    //       CBaseProc__Destroy(lVar5->_curProc, 0);
    //       if (lVar5->_cachedProcs == 0) break;
    //       Dictionary<int, object>__GetEnumerator(...);
    //       while (MoveNext) {
    //         if (current == 0) throw;
    //         CBaseProc__Destroy(current, 0);
    //       }
    //       Enumerator__Dispose();
    //       lVar5 = _stackProcs;
    //     }
    //   }
    //   LAB_018b928c: throw;
    public void Destroy()
    {
        if (_curProc != null)
        {
            _curProc.Leave();
            if (_curProc == null) throw new NullReferenceException("_curProc");
            if (!_curProc.bCached) _curProc.Destroy();
            _curProc = null;
        }
        if (_cachedProcs != null)
        {
            foreach (var kv in _cachedProcs)
            {
                if (kv.Value == null) throw new NullReferenceException("cachedProcs.Value");
                kv.Value.Destroy();
            }
            if (_cachedProcs == null) throw new NullReferenceException("_cachedProcs");
            _cachedProcs.Clear();

            // Drain _stackProcs: pop each StackProc, Destroy its _curProc + each entry of its _cachedProcs.
            // Loop logic from Ghidra: re-reads _stackProcs each iteration (lVar5 = param_1[4]).
            // Falls through to V_Destroy when _stackProcs.Count < 1.
            while (_stackProcs != null)
            {
                if (_stackProcs.Count < 1)
                {
                    V_Destroy();
                    return;
                }
                var sp = _stackProcs.Pop();
                if (sp == null || sp._curProc == null) break;
                sp._curProc.Destroy();
                if (sp._cachedProcs == null) break;
                foreach (var kv in sp._cachedProcs)
                {
                    if (kv.Value == null) throw new NullReferenceException("StackProc.cachedProcs.Value");
                    kv.Value.Destroy();
                }
            }
            throw new NullReferenceException("Destroy fall-through");
        }
        // _cachedProcs == null: original Ghidra falls into LAB_018b928c which is a non-returning throw.
        throw new NullReferenceException("_cachedProcs");
    }

    // RVA 0x017b9324 — OnApplicationQuit 1-1 from Ghidra
    // (Thread-init macros DAT_0368374f skipped.)
    //   plVar3 = _curProc;
    //   if (plVar3 != 0 && *(char*)(plVar3 + 0x1c) == '\0')                  // !bCached
    //     (**(code **)(*plVar3 + 0x178))(plVar3, ...);                       // CBaseProc vtable +0x178 = OnApplicationQuit (slot 4)
    //   if (_cachedProcs != 0) {
    //     lVar4 = Dictionary<int, object>__get_Values(_cachedProcs, ...);
    //     if (lVar4 != 0) {
    //       ValueCollection__GetEnumerator(...);
    //       while (true) {
    //         if (!MoveNext()) { Dispose(); return; }
    //         if (current == 0) break;                                       // → throw
    //         (**(code **)(*current + 0x178))(current, ...);                 // OnApplicationQuit on each cached proc
    //       }
    //       throw;
    //     }
    //     throw;
    //   }
    //   throw;
    public void OnApplicationQuit()
    {
        if (_curProc != null && !_curProc.bCached)
        {
            _curProc.OnApplicationQuit();
        }
        if (_cachedProcs == null) throw new NullReferenceException("_cachedProcs");
        var values = _cachedProcs.Values;
        if (values == null) throw new NullReferenceException("_cachedProcs.Values");
        foreach (var proc in values)
        {
            if (proc == null) throw new NullReferenceException("cachedProc");
            proc.OnApplicationQuit();
        }
    }

    // RVA 0x017b94b0 — SwitchProc(EProcID, ArrayList, bool) 1-1 from Ghidra
    //   if (eChangeProcID != 0) {
    //     int curID = (_curProc != 0) ? *(int*)(_curProc + 0x18) : 0;
    //     if (curID != eChangeProcID) {
    //       _changeProcArgs = args;
    //       _eChangeStyle = 1 /Switch/;
    //       _eChangeProcID = eChangeProcID;
    //       if (imm && _eChangeStyle != 0) { SetCurProcID(); _eChangeStyle = 0; }
    //     }
    //   }
    public void SwitchProc(EProcID eChangeProcID, ArrayList args, bool imm = false)
    {
        if (eChangeProcID == (EProcID)0) return;
        EProcID curID = (EProcID)0;
        if (_curProc != null) curID = _curProc.eProcID;
        if (curID == eChangeProcID) return;
        _changeProcArgs = args;
        _eChangeStyle = EStyle.Switch;
        _eChangeProcID = eChangeProcID;
        if (imm && _eChangeStyle != EStyle.None)
        {
            SetCurProcID();
            _eChangeStyle = EStyle.None;
        }
    }

    // RVA 0x017b9514 — SwitchProc(EProcID, bool) 1-1 from Ghidra
    //   CProcManager__SwitchProc(this, eChangeProcID, 0, imm & 1);
    public void SwitchProc(EProcID eChangeProcID, bool imm = false)
    {
        SwitchProc(eChangeProcID, null, imm);
    }

    // RVA 0x017b9520 — PushProc(EProcID, ArrayList) 1-1 from Ghidra
    //   (Note: the 2-arg overload at 0x017b953c is the body that sets fields directly;
    //    the 3-arg form simply discards args — confirmed from PushProc.c:
    //    void CProcManager__PushProc(long param_1, int param_2):
    //      if (param_2 != 0) {
    //        *(undefined8 *)(this + 0x38) = 0;
    //        *(undefined4 *)(this + 0x30) = 2;     // EStyle.Push
    //        *(int *)(this + 0x34) = param_2;      // _eChangeProcID
    //      }
    //   )
    // Body of the (EProcID, ArrayList) overload (RVA 0x17B9520) was not captured in batch, but per dump.cs
    // it parallels SwitchProc(EProcID,ArrayList,bool=false) with EStyle=Push (no imm).
    public void PushProc(EProcID eChangeProcID, ArrayList args)
    {
        if (eChangeProcID == (EProcID)0) return;
        EProcID curID = (EProcID)0;
        if (_curProc != null) curID = _curProc.eProcID;
        if (curID == eChangeProcID) return;
        _changeProcArgs = args;
        _eChangeStyle = EStyle.Push;
        _eChangeProcID = eChangeProcID;
    }

    // RVA 0x017b953c — PushProc(EProcID) 1-1 from Ghidra (PushProc.c)
    //   if (eChangeProcID != 0) {
    //     _changeProcArgs = 0;
    //     _eChangeStyle = 2 /Push/;
    //     _eChangeProcID = eChangeProcID;
    //   }
    public void PushProc(EProcID eChangeProcID)
    {
        if (eChangeProcID == (EProcID)0) return;
        _changeProcArgs = null;
        _eChangeStyle = EStyle.Push;
        _eChangeProcID = eChangeProcID;
    }

    // RVA 0x017b9558 — PopProc(ArrayList) 1-1 from Ghidra
    //   (Body not captured separately; per the 0-arg wrapper PopProc.c which calls PopProc(this,0),
    //    the (ArrayList) overload sets _changeProcArgs=args and _eChangeStyle=Pop only when stack is non-empty.
    //    Logic mirrors SwitchProc with style=Pop.)
    public void PopProc(ArrayList args)
    {
        if (_stackProcs == null || _stackProcs.Count == 0) return;
        _changeProcArgs = args;
        _eChangeStyle = EStyle.Pop;
    }

    // RVA 0x017b95d0 — PopProc() 1-1 from Ghidra
    //   CProcManager__PopProc(this, 0);
    public void PopProc() { PopProc(null); }

    // RVA 0x017b95d8 — Update(float dTime) 1-1 from Ghidra
    //   if (_eChangeStyle != 0) { SetCurProcID(); _eChangeStyle = 0; }
    //   if (_curProc != 0) {
    //     CBaseProc__Update(dTime, _curProc, 0);          // float in fp-reg, this in x0
    //     if (_eChangeStyle != 0) { SetCurProcID(); _eChangeStyle = 0; }
    //   }
    public void Update(float dTime)
    {
        if (_eChangeStyle != EStyle.None)
        {
            SetCurProcID();
            _eChangeStyle = EStyle.None;
        }
        if (_curProc != null)
        {
            _curProc.Update(dTime);
            if (_eChangeStyle != EStyle.None)
            {
                SetCurProcID();
                _eChangeStyle = EStyle.None;
            }
        }
    }

    // RVA 0x017b9630 — FixedUpdate 1-1 from Ghidra
    //   if (_curProc != 0) CBaseProc__FixedUpdate(_curProc, 0);
    public void FixedUpdate()
    {
        if (_curProc != null) _curProc.FixedUpdate();
    }

    // RVA 0x017b9644 — LateUpdate 1-1 from Ghidra
    //   if (_curProc != 0) CBaseProc__LateUpdate(_curProc, 0);
    public void LateUpdate()
    {
        if (_curProc != null) _curProc.LateUpdate();
    }

    // RVA 0x017b9658 — V_Start(ArrayList) 1-1 from Ghidra
    //   return;        (empty body — overridable by subclasses)
    protected virtual void V_Start(ArrayList args)
    {
    }

    // RVA 0x017b965c — V_Destroy 1-1 from Ghidra
    //   return;        (empty body — overridable by subclasses)
    protected virtual void V_Destroy()
    {
    }

    // RVA 0x017b9660 — .ctor 1-1 from Ghidra
    // Ghidra batch did not emit a _ctor.c file but IL2CPP field-init convention requires the
    // Dictionary + Stack to be allocated in the constructor (otherwise GetProc/SetCurProcID
    // would NRE on first SwitchProc call before any Push branch initializes them). Reconstructed
    // from observed runtime behavior + dump.cs field types (Dictionary<EProcID, CBaseProc>, Stack<StackProc>).
    public CProcManager()
    {
        _cachedProcs = new Dictionary<EProcID, CBaseProc>();
        _stackProcs = new Stack<StackProc>();
    }

    // Nested types (TypeDefIndex 106..108)

    // public sealed class CProcManager.CBSwitchEventFunc : MulticastDelegate
    public delegate void CBSwitchEventFunc(EProcID eid);

    // private enum CProcManager.EStyle
    private enum EStyle
    {
        None = 0,
        Switch = 1,
        Push = 2,
        Pop = 3,
    }

    // private class CProcManager.StackProc
    //   public Dictionary<EProcID, CBaseProc> _cachedProcs;  // 0x10
    //   public CBaseProc                       _curProc;     // 0x18
    private class StackProc
    {
        public Dictionary<EProcID, CBaseProc> _cachedProcs;
        public CBaseProc _curProc;
    }
}
