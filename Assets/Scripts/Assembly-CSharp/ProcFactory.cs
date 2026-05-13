// Source: work/03_il2cpp_dump/dump.cs TypeDefIndex 105 + Ghidra decompiled_full/ProcFactory/*.c
// 1-1 ports of get_count, AutoRegist, CreateProc, .ctor, .cctor from libil2cpp.so.

using System;
using System.Collections.Generic;
using System.Reflection;

public class ProcFactory
{
    private static Dictionary<int, Type> _mapCreator;

    // Source: Ghidra get_count.c RVA 0x017b7e88 — return _mapCreator.Count (NRE if null)
    public static int get_count()
    {
        if (_mapCreator == null) throw new System.NullReferenceException("ProcFactory._mapCreator");
        return _mapCreator.Count;
    }

    // Source: Ghidra AutoRegist.c RVA 0x017b7f00
    // 1-1: For each EProcID enum value, look up C# class by name and register in _mapCreator
    //   if it inherits from CBaseProc. The Ghidra `while (uVar1 < 5 && (1<<uVar1) & 0x19 != 0)`
    //   filter (continue-on-true) skips enum values 0, 3, 4 — these are Lua-only procs handled
    //   in CreateProc's fallback path. Values 1 (ProcessDelay), 2 (ProcessLunchGame), 5+ are
    //   registered here.
    // Bit-mask 0x19 = 0b00011001 → bits 0, 3, 4 set. So skipped: NULL, ProcessLoginGame, ProcessInMap.
    public static void AutoRegist()
    {
        Type baseType = typeof(CBaseProc);
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        Assembly asm = typeof(ProcFactory).Assembly;
        Array enumValues = Enum.GetValues(typeof(EProcID));
        foreach (object boxed in enumValues)
        {
            EProcID e = (EProcID)boxed;
            int v = (int)e;

            // Ghidra skip-filter: continue (skip) when (v < 5 && (1 << v) & 0x19 != 0)
            // i.e., skip values {0, 3, 4} — Lua-only procs handled by CreateProc fallback.
            if (v < 5 && ((1 << v) & 0x19) != 0) continue;

            string name = e.ToString();
            // Ghidra calls Type.GetType wrapper (FUN_015cbad8) with (typeName, assemblyName, ?).
            // Use Assembly.GetType for direct lookup in Assembly-CSharp.
            Type t = asm.GetType(name);
            if (t != null && baseType.IsAssignableFrom(t))
            {
                _mapCreator.Add(v, t);
            }
            else
            {
                UnityEngine.Debug.LogError("[ProcFactory.AutoRegist] type not found or not CBaseProc subclass: " + name);
            }
        }

        sw.Stop();
        UnityEngine.Debug.LogWarning("[ProcFactory] Registered " + _mapCreator.Count + " procs in " + sw.Elapsed.TotalSeconds + "s");
    }

    // Source: Ghidra CreateProc.c RVA 0x017b87c4
    // 1-1:
    //   if (_mapCreator.TryGetValue(eProcID, out type)) {
    //     proc = Activator.CreateInstance(type);
    //     if (proc is CBaseProc) return proc;
    //     return null;
    //   }
    //   // Fallback: create BaseProcLua with [eProcID] args, call CreateLuaProc()
    //   var ctorArgs = new object[1] { eProcID };
    //   proc = Activator.CreateInstance(typeof(BaseProcLua), ctorArgs);
    //   if (proc is CBaseProc && proc is BaseProcLua) {
    //     if (!((BaseProcLua)proc).CreateLuaProc()) {
    //       Debug.LogError("Could not create Lua proc: " + eProcID);
    //       return null;
    //     }
    //     return proc;
    //   }
    //   throw NRE
    public static CBaseProc CreateProc(EProcID eProcID)
    {
        if (_mapCreator == null) throw new System.NullReferenceException("ProcFactory._mapCreator");

        Type t;
        if (_mapCreator.TryGetValue((int)eProcID, out t))
        {
            object proc = Activator.CreateInstance(t);
            if (proc is CBaseProc cb) return cb;
            return null;
        }

        // Lua fallback
        try
        {
            // BaseProcLua ctor signature is (EProcID eID), not (int). Activator.CreateInstance
            // does not auto-cast int→enum; pass eProcID directly.
            object lua = Activator.CreateInstance(typeof(BaseProcLua), new object[] { eProcID });
            if (lua is BaseProcLua bpl)
            {
                if (!bpl.CreateLuaProc())
                {
                    UnityEngine.Debug.LogError("[ProcFactory.CreateProc] Could not create Lua proc for EProcID: " + (int)eProcID);
                    return null;
                }
                return bpl;
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("[ProcFactory.CreateProc] Lua fallback failed for EProcID " + (int)eProcID + ": " + ex.Message);
        }
        return null;
    }

    // Source: dump.cs RVA 0x17B8AAC — default ctor; base Object handles init
    public ProcFactory() { }

    // Source: Ghidra .cctor.c RVA 0x017b8ab4 — _mapCreator = new Dictionary<int, Type>();
    static ProcFactory()
    {
        _mapCreator = new Dictionary<int, Type>();
    }
}
