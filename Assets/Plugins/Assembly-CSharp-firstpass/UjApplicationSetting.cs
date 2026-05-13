// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'UjApplicationSetting'
// To port a method: replace `throw new System.NotImplementedException();`
// with body translated from the listed Ghidra .c file.
// Move ported file to unity_project/Assets/Scripts/Ported/<area>/

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
    private static UjApplicationSetting.aPlayModeAttr _abBuildMode;
    private static UjApplicationSetting.anAttr<int> _targetFps;

    // RVA: 0x15889B0/0x1589830  Property merged (1-1 with dump.cs prop syntax)
    public static e_PlayMode abBuildMode { get { throw new System.NotImplementedException(); } set { throw new System.NotImplementedException(); } }

    // RVA: 0x1589898  Ghidra: work/06_ghidra/decompiled_full/UjApplicationSetting/get_TargetFps.c
    public static int get_TargetFps() { throw new System.NotImplementedException(); }

    // RVA: 0x1589908  Ghidra: work/06_ghidra/decompiled_full/UjApplicationSetting/.ctor.c
    public UjApplicationSetting() { throw new System.NotImplementedException(); }

    // RVA: 0x1589910  Ghidra: work/06_ghidra/decompiled_full/UjApplicationSetting/.cctor.c
    static UjApplicationSetting() { throw new System.NotImplementedException(); }

    // Source: Il2CppDumper-stub  TypeDefIndex: 8227
    private class aPlayModeAttr
    {
        private e_PlayMode _data;

        // RVA: 0x1589A04  Ghidra: work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/.ctor.c
        public aPlayModeAttr(string def) { throw new System.NotImplementedException(); }

        // RVA: 0x1589B74  Ghidra: work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/get_data.c
        public e_PlayMode get_data() { throw new System.NotImplementedException(); }

        // RVA: 0x1589B7C  Ghidra: work/06_ghidra/decompiled_full/UjApplicationSetting.aPlayModeAttr/set_data.c
        public void set_data(e_PlayMode value) { throw new System.NotImplementedException(); }

    }
    // Source: Il2CppDumper-stub  TypeDefIndex: 8226
    private class anAttr<T>
    {
        private T _data;

        // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
        public anAttr(T def) { throw new System.NotImplementedException(); }

        // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
        public T get_data() { throw new System.NotImplementedException(); }

    }
}
