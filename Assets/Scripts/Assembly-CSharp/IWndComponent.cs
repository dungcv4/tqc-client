// Source: dump.cs class 'IWndComponent' (TypeDefIndex: 227)
// Source: Ghidra work/06_ghidra/decompiled_full/IWndComponent/.ctor.c RVA 0x01956f04

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

// Source: Il2CppDumper-stub  TypeDefIndex: 227
public abstract class IWndComponent : MonoBehaviour
{
    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public abstract void InitComponent(WndForm wnd);

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public abstract void DinitComponent(WndForm wnd);

    // Source: Ghidra work/06_ghidra/decompiled_full/IWndComponent/.ctor.c RVA 0x01956f04
    // 1-1: empty body (only base MonoBehaviour.ctor — implicit in C#).
    protected IWndComponent() { }

}
