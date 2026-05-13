// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15D611C, 0x15D6148, 0x15D6178, 0x15D6180, 0x15D6188, 0x15D6190,
//       0x15D62A8, 0x15D6390, 0x15D639C, 0x15D63A8, 0x15D63B4, 0x15D63C0,
//       0x15D63C4, 0x15D63E8, 0x15D6408, 0x15D6608, 0x15D6610, 0x15D6618,
//       0x15D661C, 0x15D6620, 0x15D6624
// Ghidra dir: work/06_ghidra/decompiled_full/CBaseProc/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-decompiled  TypeDefIndex: 95
// Field offsets verified from dump.cs:
//   _procMgr   0x10  (CProcManager)
//   _eProcID   0x18  (EProcID = int32)
//   _bCached   0x1C  (bool)
public class CBaseProc
{
    private CProcManager _procMgr;
    private EProcID _eProcID;
    private bool _bCached;

    // Property surface used by callers (e.g. CProcManager) — IL2CPP exposes get_procMgr/get_eProcID/get_bCached
    // as property getters; signatures.json lists matching properties.
    public CProcManager procMgr
    {
        // RVA: 0x15D6178  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/get_procMgr.c
        // return *(undefined8 *)(this + 0x10);
        get { return _procMgr; }
    }

    public EProcID eProcID
    {
        // RVA: 0x15D6180  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/get_eProcID.c
        // return *(undefined4 *)(this + 0x18);
        get { return _eProcID; }
    }

    public bool bCached
    {
        // RVA: 0x15D6188  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/get_bCached.c
        // return *(undefined1 *)(this + 0x1c);
        get { return _bCached; }
    }

    // RVA: 0x15D611C  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/.ctor.c
    // CBaseProc(EProcID eID) — Ghidra batch only emitted the 3-arg .ctor body; the 1-arg
    // form per dump.cs forwards with bCached=false.
    public CBaseProc() : this((EProcID)0) { }
    public CBaseProc(EProcID eID) : this(eID, false)
    {
    }

    // RVA: 0x15D6148  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/.ctor.c
    //   System_Object___ctor(this, 0);
    //   *(undefined4 *)(this + 0x18) = eID;
    //   *(byte *)(this + 0x1c) = bCached & 1;
    public CBaseProc(EProcID eID, bool bCached)
    {
        _eProcID = eID;
        _bCached = bCached;
    }

    // RVA: 0x15D6190  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/Enter.c
    // Sets _procMgr = mgr, calls virtual V_Enter(args). If V_Enter returns false:
    //   - clears _procMgr
    //   - logs "[CBaseProc::Enter] eProcID:{0} V_Enter false" via UJDebug.LogError
    public bool Enter(CProcManager mgr, ArrayList args)
    {
        _procMgr = mgr;
        bool ok = V_Enter(args);
        if (!ok)
        {
            _procMgr = null;
            UJDebug.LogError(string.Format("[CBaseProc::Enter] eProcID:{0} V_Enter false", _eProcID));
        }
        return ok;
    }

    // RVA: 0x15D62A8  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/Leave.c
    // Calls virtual V_Leave(); if false → log error; finally clears _procMgr.
    public void Leave()
    {
        bool ok = V_Leave();
        if (!ok)
        {
            UJDebug.LogError(string.Format("[CBaseProc::Leave] eProcID:{0} V_Leave false", _eProcID));
        }
        _procMgr = null;
    }

    // RVA: 0x15D6390  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/Update.c
    // (**(code **)(*this + 0x1a8))(this, dTime);   // virtual V_Update slot
    public void Update(float dTime)
    {
        V_Update(dTime);
    }

    // RVA: 0x15D639C  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/FixedUpdate.c
    // (**(code **)(*this + 0x1b8))(this);          // virtual V_FixedUpdate slot
    public void FixedUpdate()
    {
        V_FixedUpdate();
    }

    // RVA: 0x15D63A8  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/LateUpdate.c
    // (**(code **)(*this + 0x1c8))(this);          // virtual V_LateUpdate slot
    public void LateUpdate()
    {
        V_LateUpdate();
    }

    // RVA: 0x15D63B4  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/Destroy.c
    // (**(code **)(*this + 0x1d8))(this);          // virtual V_Destroy slot
    public void Destroy()
    {
        V_Destroy();
    }

    // RVA: 0x15D63C0  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/OnApplicationQuit.c
    // empty body — virtual hook for subclasses.
    public virtual void OnApplicationQuit()
    {
    }

    // RVA: 0x15D63C4  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/SwitchProc.c
    // SwitchProc(EProcID) — Ghidra batch only captured the 2-arg form; 1-arg per dump.cs
    // forwards with args=null.
    protected void SwitchProc(EProcID eChangeProcID)
    {
        SwitchProc(eChangeProcID, null);
    }

    // RVA: 0x15D63E8  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/SwitchProc.c
    //   if (_procMgr != 0) { CProcManager__SwitchProc(...); return; }
    //   /* WARNING: Subroutine does not return */ FUN_015cb8fc();   // throw NRE
    protected void SwitchProc(EProcID eChangeProcID, ArrayList args)
    {
        if (_procMgr != null)
        {
            _procMgr.SwitchProc(eChangeProcID, args);
            return;
        }
        throw new NullReferenceException("_procMgr");
    }

    // RVA: 0x15D6408  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/CallMethod.c
    //   if (!String.IsNullOrEmpty(funcName)) {
    //     Type t = GetType();
    //     if (t == 0) throw;
    //     MethodInfo mi = t.GetMethod(funcName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance); // 0x14
    //     if (mi == null) {
    //       UJDebug.LogError(string.Format("[CBaseProc::CallMethod] eProcID:{0} funcName:{1} method not found", _eProcID, funcName));
    //     } else {
    //       mi.Invoke(this, args);
    //     }
    //   }
    public void CallMethod(string funcName, object[] args)
    {
        if (string.IsNullOrEmpty(funcName)) return;
        Type t = GetType();
        if (t == null) throw new NullReferenceException("GetType()==null");
        // 0x14 = BindingFlags.Public(0x10) | BindingFlags.Instance(0x4)
        MethodInfo mi = t.GetMethod(funcName, BindingFlags.Public | BindingFlags.Instance);
        if (mi == null)
        {
            UJDebug.LogError(string.Format("[CBaseProc::CallMethod] eProcID:{0} funcName:{1} method not found", _eProcID, funcName));
        }
        else
        {
            mi.Invoke(this, args);
        }
    }

    // RVA: 0x15D6608  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/V_Enter.c
    // return 1;   (default: succeed)
    protected virtual bool V_Enter(ArrayList args)
    {
        return true;
    }

    // RVA: 0x15D6610  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/V_Leave.c
    // return 1;
    protected virtual bool V_Leave()
    {
        return true;
    }

    // RVA: 0x15D6618  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/V_Update.c
    // empty
    protected virtual void V_Update(float dTime)
    {
    }

    // RVA: 0x15D661C  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/V_FixedUpdate.c
    // empty
    protected virtual void V_FixedUpdate()
    {
    }

    // RVA: 0x15D6620  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/V_LateUpdate.c
    // empty
    protected virtual void V_LateUpdate()
    {
    }

    // RVA: 0x15D6624  Ghidra: work/06_ghidra/decompiled_full/CBaseProc/V_Destroy.c
    // empty
    protected virtual void V_Destroy()
    {
    }
}
