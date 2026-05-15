// Source: Ghidra-decompiled libil2cpp.so
// RVAs:
//   UjApplicationSetting             : 0x15889B0 get_abBuildMode, 0x1589830 set_abBuildMode,
//                                       0x1589898 get_TargetFps, 0x1589908 .ctor, 0x1589910 .cctor
//   UjApplicationSetting.aPlayModeAttr : 0x1589A04 .ctor(string), 0x1589B74 get_data, 0x1589B7C set_data
//   UjApplicationSetting.anAttr<T>     : RVA -1 (generic, instantiated as anAttr<int> at 0x26A8C48 ctor, 0x26A8C70 get_data)
// Ghidra dir: work/06_ghidra/decompiled_full/UjApplicationSetting/
//             work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8228
public class UjApplicationSetting
{
    // Source: dump.cs line 512301-512302 (offsets 0x0/0x8 in static layout).
    private static UjApplicationSetting.aPlayModeAttr _abBuildMode;
    private static UjApplicationSetting.anAttr<int> _targetFps;

    // RVA: 0x15889B0/0x1589830  Property merged (1-1 with dump.cs prop syntax)
    // Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting/get_abBuildMode.c
    //         Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting/set_abBuildMode.c
    // Body: getter reads *(_abBuildMode + 0x10) = _abBuildMode._data (e_PlayMode).
    //       setter writes value into *(_abBuildMode + 0x10) = _abBuildMode._data.
    //       Ghidra dereferences raw cctor-init slot; if null, IL2CPP raises NullReference.
    public static e_PlayMode abBuildMode
    {
        get
        {
            if (_abBuildMode == null) { throw new System.NullReferenceException(); }
            return _abBuildMode.get_data();
        }
        set
        {
            if (_abBuildMode == null) { throw new System.NullReferenceException(); }
            _abBuildMode.set_data(value);
        }
    }

    // RVA: 0x1589898  Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting/get_TargetFps.c
    // Body: returns *(_targetFps + 0x10) = anAttr<int>._data. If _targetFps null, IL2CPP raises NRE.
    public static int get_TargetFps()
    {
        if (_targetFps == null) { throw new System.NullReferenceException(); }
        return _targetFps.get_data();
    }

    // RVA: 0x1589908  Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting/.ctor.c
    // Body: System_Object___ctor(this, 0) — empty base() call.
    public UjApplicationSetting()
    {
        // base() — System_Object___ctor
    }

    // RVA: 0x1589910  Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting/.cctor.c
    // Body:
    //   _abBuildMode = new aPlayModeAttr(PTR_StringLiteral_12567 = "Web");
    //   _targetFps   = new anAttr<int>(0x3c = 60);
    // (write barriers thunk_FUN_015ee8c4 elided — runtime GC concern, not state.)
    static UjApplicationSetting()
    {
        _abBuildMode = new UjApplicationSetting.aPlayModeAttr("Web");
        _targetFps = new UjApplicationSetting.anAttr<int>(60);
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 8227
    private class aPlayModeAttr
    {
        // Source: dump.cs line 512280 (offset 0x10 inside instance).
        private e_PlayMode _data;

        // RVA: 0x1589A04  Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/.ctor.c
        // Body: chained string equality test against PTR_StringLiteral_*:
        //   12567 "Web"     -> _data = 0 (e_PlayMode.Web)
        //   2992  "Android" -> _data = 1 (e_PlayMode.Android)
        //   17213 "iOS"     -> _data = 2 (e_PlayMode.iOS)
        //   10487 "Steam"   -> _data = 3 (e_PlayMode.PC)
        //   else            -> _data = 0; Debug.LogError(string.Format("NG!, {0}", def))
        //                      (PTR_StringLiteral_8242 = "NG!, {0}").
        public aPlayModeAttr(string def)
        {
            // base() — System_Object___ctor
            // Ghidra chains 4 System_String__op_Equality tests; first non-match falls through to
            // default branch with LogError. C# == on string is op_Equality.
            if (def == "Web")
            {
                _data = (e_PlayMode)0;
            }
            else if (def == "Android")
            {
                _data = (e_PlayMode)1;
            }
            else if (def == "iOS")
            {
                _data = (e_PlayMode)2;
            }
            else if (def == "Steam")
            {
                _data = (e_PlayMode)3;
            }
            else
            {
                _data = (e_PlayMode)0;
                UnityEngine.Debug.LogError(string.Format("NG!, {0}", def));
            }
        }

        // RVA: 0x1589B74  Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/get_data.c
        // Body: return *(this + 0x10) = _data.
        public e_PlayMode get_data()
        {
            return _data;
        }

        // RVA: 0x1589B7C  Source: Ghidra work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/set_data.c
        // Body: *(this + 0x10) = value.
        public void set_data(e_PlayMode value)
        {
            _data = value;
        }

    }
    // Source: Il2CppDumper-stub  TypeDefIndex: 8226
    private class anAttr<T>
    {
        // Source: dump.cs line 512246 (offset 0x0 inside instance).
        private T _data;

        // RVA: -1  Generic body not in Ghidra decompiled_full (only anAttr<int> instantiation at RVA 0x26A8C48).
        // The generic open-form .ctor body would normally just assign `this._data = def;` plus base(),
        // since there is no other field. Mirroring System_Object___ctor + field assign.
        // TODO: confidence:medium — generic body inferred from instantiated form + lone field; if more
        // logic appears in 0x26A8C48 disassembly, re-port from that RVA.
        public anAttr(T def)
        {
            // base() — System_Object___ctor
            _data = def;
        }

        // RVA: -1  Generic body not in Ghidra decompiled_full. By symmetry with aPlayModeAttr.get_data
        // (offset 0x10 in that class) the field is the lone payload; trivial accessor.
        // TODO: confidence:medium — verify against anAttr<int> instantiation at RVA 0x26A8C70.
        public T get_data()
        {
            return _data;
        }

    }
}
