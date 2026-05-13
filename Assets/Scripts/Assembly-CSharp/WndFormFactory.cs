// Source: Ghidra-decompiled libil2cpp.so (work/06_ghidra/decompiled_full/WndFormFactory/*.c)
// 1-1 ports of count, AutoRegist, CreateWndForm, .ctor, .cctor.

using System;
using System.Collections.Generic;
using System.Reflection;

public class WndFormFactory
{
    private static Dictionary<uint, Type> _mapCreator;

    // Source: Ghidra get_count.c RVA 0x01a09088 — return _mapCreator.Count (NRE if null)
    public static int count
    {
        get
        {
            if (_mapCreator == null) throw new System.NullReferenceException("WndFormFactory._mapCreator");
            return _mapCreator.Count;
        }
    }

    // Source: Ghidra AutoRegist.c RVA 0x01a09100
    // 1-1: Similar pattern to ProcFactory.AutoRegist but for EWndFormID enum + WndForm base.
    //   Filter `while (uVar1 | 4 == 4)` — continue-on-true when uVar1 is in {0, 4}.
    //   So skipped: NULL, StartBundleDownLoad — these go through CreateWndForm's Lua fallback.
    //   Values 3 (WndForm_DebugConsole), 5 (WndForm_LoadingScreen), 6 (WndForm_LunchGame) get registered.
    public static void AutoRegist()
    {
        Type baseType = typeof(WndForm);
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        Assembly asm = typeof(WndFormFactory).Assembly;
        Array enumValues = Enum.GetValues(typeof(EWndFormID));
        foreach (object boxed in enumValues)
        {
            EWndFormID e = (EWndFormID)boxed;
            uint v = (uint)e;

            // Ghidra skip-filter: `while ((uVar1 | 4) == 4)` continues (skips) when uVar1 ∈ {0, 4}.
            //   uVar1 == 0 → 0 | 4 == 4 → skip
            //   uVar1 == 4 → 4 | 4 == 4 → skip
            //   uVar1 == anything else → != 4 → don't skip
            if ((v | 4u) == 4u) continue;

            string name = e.ToString();
            Type t = asm.GetType(name);
            if (t != null && baseType.IsAssignableFrom(t))
            {
                _mapCreator.Add(v, t);
            }
            else
            {
                UnityEngine.Debug.LogError("[WndFormFactory.AutoRegist] type not found or not WndForm subclass: " + name);
            }
        }

        sw.Stop();
        UnityEngine.Debug.LogWarning("[WndFormFactory] Registered " + _mapCreator.Count + " wndforms in " + sw.Elapsed.TotalSeconds + "s");
    }

    // Source: Ghidra CreateWndForm.c RVA 0x01a04f5c
    // 1-1:
    //   if (eWndFormID != 0 && _mapCreator.TryGetValue(eWndFormID, out type)) {
    //     // Ghidra calls WndRoot.<+0x30> precreate hook (offset 0x30 field — likely event/manager)
    //     // [Deviation: omit WndRoot precall; not yet identified; non-blocking for basic flow]
    //     var wnd = Activator.CreateInstance(type);
    //     if (wnd is WndForm) return wnd;
    //     return null;
    //   }
    //   // Fallback: create WndForm_Lua with [eWndFormID] args, call CreateLuaWnd()
    //   var ctorArgs = new object[1] { eWndFormID };
    //   var wnd = Activator.CreateInstance(typeof(WndForm_Lua), ctorArgs);
    //   if (wnd is WndForm && wnd is WndForm_Lua) {
    //     if (!((WndForm_Lua)wnd).CreateLuaWnd()) {
    //       Debug.LogError("Could not create Lua wnd: " + eWndFormID);
    //       return null;
    //     }
    //     return wnd;
    //   }
    //   throw NRE
    public static WndForm CreateWndForm(uint eWndFormID)
    {
        if (_mapCreator == null) throw new System.NullReferenceException("WndFormFactory._mapCreator");

        if (eWndFormID != 0u)
        {
            Type t;
            if (_mapCreator.TryGetValue(eWndFormID, out t))
            {
                // TODO: WndRoot.<+0x30 hook>(eWndFormID, 1) — pre-create hook; deviation pending identification.
                object wnd = Activator.CreateInstance(t);
                if (wnd is WndForm wf) return wf;
                return null;
            }
        }

        // Lua fallback
        try
        {
            object lua = Activator.CreateInstance(typeof(WndForm_Lua), new object[] { eWndFormID });
            if (lua is WndForm_Lua wfl)
            {
                if (!wfl.CreateLuaWnd())
                {
                    UnityEngine.Debug.LogError("[WndFormFactory.CreateWndForm] Could not create Lua wnd for EWndFormID: " + eWndFormID);
                    return null;
                }
                return wfl;
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("[WndFormFactory.CreateWndForm] Lua fallback failed for EWndFormID " + eWndFormID + ": " + ex.Message);
        }
        return null;
    }

    // RVA: 0x1A097C8 — default ctor
    public WndFormFactory() { }

    // Source: Ghidra .cctor.c RVA 0x01a097d0 — _mapCreator = new Dictionary<uint, Type>();
    static WndFormFactory()
    {
        _mapCreator = new Dictionary<uint, Type>();
    }
}
