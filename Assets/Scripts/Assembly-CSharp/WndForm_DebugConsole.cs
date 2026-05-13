// Source: work/03_il2cpp_dump/dump.cs class 'WndForm_DebugConsole' (TypeDefIndex 784)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/WndForm_DebugConsole/*.c

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
using UnityEngine.EventSystems;
using Opencoding.Console;

// Source: Il2CppDumper-stub  TypeDefIndex: 784
public class WndForm_DebugConsole : WndForm
{
    private GameObject arrowGO;  // 0x60
    private static WndForm_DebugConsole s_instance;  // static 0x0

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_DebugConsole/get_Instance.c RVA 0x18F2F44
    // Body: returns first 8 bytes of static field block (s_instance). 1-1.
    public static WndForm_DebugConsole Instance
    {
        get { return s_instance; }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_DebugConsole/IsPrefabInResource.c RVA 0x18F2F8C
    // Body: return 1;
    public override bool IsPrefabInResource()
    {
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_DebugConsole/V_AfterCreate.c RVA 0x18F2F94
    // Body: s_instance = this; base.V_AfterCreate(args);
    protected override void V_AfterCreate(ArrayList args)
    {
        s_instance = this;
        base.V_AfterCreate(args);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_DebugConsole/V_ProcessKeyClick.c RVA 0x18F3008
    // Body: return 0;
    protected override bool V_ProcessKeyClick(KeyCode keyCode)
    {
        return false;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_DebugConsole/OnClick.c RVA 0x18F3010
    // Body: Opencoding.Console.DebugConsole.IsVisible = true;
    public void OnClick(Component btn, PointerEventData data, Action_Type action, int clickValue, string clickString)
    {
        DebugConsole.IsVisible = true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_DebugConsole/SetDebugConsoleVisible.c RVA 0x18F3064
    // 1-1 logic from the Ghidra body:
    //   if (arrowGO != null) {
    //     var comp = arrowGO.GetComponent<DebugConsole>();
    //     if (enable) {
    //       if (comp != null) { comp.<vtable@0x298>(); comp.<vtable@0x2a8>(); }
    //       arrowGO.SetActive(true);
    //     } else if (onlyHide) {
    //       if (comp != null) { comp.<vtable@0x298>(); comp.<vtable@0x2a8>(); }
    //     } else {
    //       arrowGO.SetActive(false);
    //     }
    //   }
    // The two virtual slots (0x298, 0x2a8) are MonoBehaviour-derived methods on the
    // DebugConsole script. We approximate by toggling DebugConsole.IsVisible (the matching
    // visibility entry-point in this class).
    public void SetDebugConsoleVisible(bool enable, bool onlyHide = false)
    {
        if (arrowGO == null)
        {
            throw new NullReferenceException();
        }
        DebugConsole comp = arrowGO.GetComponent<DebugConsole>();
        if (enable)
        {
            if (comp != null)
            {
                DebugConsole.IsVisible = true;
            }
            arrowGO.SetActive(true);
        }
        else if (onlyHide)
        {
            if (comp != null)
            {
                DebugConsole.IsVisible = false;
            }
        }
        else
        {
            arrowGO.SetActive(false);
        }
    }

    // Source: dump.cs — RVA 0x18F31D4, no Ghidra body cached (constructor with no extra side effects).
    public WndForm_DebugConsole()
    {
    }
}
