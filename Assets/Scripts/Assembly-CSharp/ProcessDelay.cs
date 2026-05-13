// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x17B7B4C, 0x17B7B60, 0x17B7E08
// Ghidra dir: work/06_ghidra/decompiled_full/ProcessDelay/

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

// Source: Il2CppDumper-decompiled  TypeDefIndex: 104
// Field offsets (CBaseProc lays 0x10..0x1F so subclass starts at 0x20):
//   _waitFrame      0x20  int
//   _waitTime       0x24  float
//   _runTime        0x28  float
//   _nextProcID     0x2C  EProcID
//   _nextProcArgs   0x30  ArrayList
public class ProcessDelay : CBaseProc
{
    private int _waitFrame;
    private float _waitTime;
    private float _runTime;
    private EProcID _nextProcID;
    private ArrayList _nextProcArgs;

    // RVA: 0x17B7B4C  Ghidra: work/06_ghidra/decompiled_full/ProcessDelay/.ctor.c
    //   *(undefined4 *)(this + 0x20) = 0xffffffff;     // _waitFrame = -1
    //   CBaseProc___ctor(this, 1, 0);                   // base(EProcID.ProcessDelay=1, bCached=false)
    public ProcessDelay() : base(EProcID.ProcessDelay, false)
    {
        _waitFrame = -1;
    }

    // RVA: 0x17B7B60  Ghidra: work/06_ghidra/decompiled_full/ProcessDelay/V_Enter.c
    // Args inspection (ArrayList args):
    //   if args==null → throw NRE
    //   if args.Count < 2 → return false
    //   o = args[0]; switch on its runtime type:
    //     - if o is int  → _waitFrame = (int)o; if (_waitFrame < 1) return false
    //     - else if o is float → _waitTime = (float)o; if (_waitTime <= 0) return false
    //     - else if o is double → _waitTime = (float)(double)o; if (_waitTime <= 0) return false
    //     - else (mismatch) → return 0 if null, or fall through
    //   _nextProcID = (EProcID)(int)args[1]   (must be int — else InvalidCastException)
    //   if args.Count > 2 → _nextProcArgs = args.GetRange(2, args.Count-2)
    //   _runTime = 0; return true
    protected override bool V_Enter(ArrayList args)
    {
        if (args == null) throw new NullReferenceException("args");
        if (args.Count < 2) return false;

        object o = args[0];
        if (o is int)
        {
            _waitFrame = (int)o;
            if (_waitFrame < 1) return false;
        }
        else if (o is float)
        {
            _waitTime = (float)o;
            if (_waitTime <= 0.0f) return false;
        }
        else if (o is double)
        {
            // PTR_DAT_034464b0 path: read as double then narrow to float (preserves Ghidra's *(double*)
            // → *pdVar6 fetch followed by single-precision store at +0x24).
            if (o == null) return false;
            _waitTime = (float)(double)o;
            if (_waitTime <= 0.0f) return false;
        }
        else
        {
            // Type mismatch on args[0] — Ghidra falls through to FUN_015cbc7c (InvalidCastException).
            throw new InvalidCastException("ProcessDelay.V_Enter: args[0] must be int|float|double");
        }

        object o1 = args[1];
        if (o1 == null) throw new NullReferenceException("args[1]");
        // Ghidra: thunk_FUN_01560368 unbox after class-equality check against PTR_DAT_03449428 (System.Int32)
        _nextProcID = (EProcID)(int)o1;

        if (args.Count > 2)
        {
            _nextProcArgs = args.GetRange(2, args.Count - 2);
        }

        _runTime = 0.0f;
        return true;
    }

    // RVA: 0x17B7E08  Ghidra: work/06_ghidra/decompiled_full/ProcessDelay/V_Update.c
    //   iVar1 = _waitFrame;
    //   if (iVar1 >= 0) {                       // -1 < iVar1 → iVar1 != -1 (sign-extended)
    //     if (iVar1 == 0) { SwitchProc(_nextProcID, _nextProcArgs); iVar1 = _waitFrame; }
    //     _waitFrame = iVar1 - 1;
    //   }
    //   if (_waitTime >= 0.0f) {
    //     fVar2 = _runTime;
    //     if (_waitTime <= fVar2) { SwitchProc(_nextProcID, _nextProcArgs); fVar2 = _runTime; }
    //     _runTime = fVar2 + dTime;
    //   }
    protected override void V_Update(float dTime)
    {
        int iVar1 = _waitFrame;
        if (iVar1 >= 0)
        {
            if (iVar1 == 0)
            {
                SwitchProc(_nextProcID, _nextProcArgs);
                iVar1 = _waitFrame;
            }
            _waitFrame = iVar1 - 1;
        }

        if (_waitTime >= 0.0f)
        {
            float fVar2 = _runTime;
            if (_waitTime <= fVar2)
            {
                SwitchProc(_nextProcID, _nextProcArgs);
                fVar2 = _runTime;
            }
            _runTime = fVar2 + dTime;
        }
    }
}
