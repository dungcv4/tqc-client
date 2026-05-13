// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15B2B08, 0x15B2B50, 0x15B2BF4, 0x15B2BEC, 0x15B2C6C, 0x15B2C70
// Ghidra dir: work/06_ghidra/decompiled_full/GameProcMgr/

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

// Source: Il2CppDumper-decompiled  TypeDefIndex: 56
// Field: s_instance (static GameProcMgr) accessed via PTR_DAT_03448210 → static field +0xb8 of class
public class GameProcMgr : CProcManager
{
    private static GameProcMgr s_instance;

    // RVA: 0x15B2B08  Ghidra: work/06_ghidra/decompiled_full/GameProcMgr/get_Instance.c
    //   return *(undefined8 *)(class_static_fields + 0xb8);    // = s_instance
    public static GameProcMgr Instance
    {
        get { return s_instance; }
    }
    // Instance removed — duplicate of Instance property accessor

    // RVA: 0x15B2B50  Ghidra: work/06_ghidra/decompiled_full/GameProcMgr/CreateInstance.c
    //   if (s_instance != 0) return;
    //   uVar2 = il2cpp_object_new(GameProcMgr_class);
    //   CProcManager___ctor(uVar2, 0);                  // calls base CProcManager()
    //   s_instance = uVar2;
    //   if (s_instance == 0) /* unreachable */ FUN_015cb8fc();
    //   CProcManager__Start(s_instance, 0);             // starts the proc manager
    public static void CreateInstance()
    {
        if (s_instance != null) return;
        s_instance = new GameProcMgr();
        if (s_instance == null) throw new NullReferenceException("GameProcMgr alloc failed");
        s_instance.Start();
    }

    // RVA: 0x15B2BF4  Ghidra: work/06_ghidra/decompiled_full/GameProcMgr/ReleaseInstance.c
    //   if (s_instance != 0) {
    //     CProcManager__Destroy(s_instance, 0);
    //     s_instance = 0;
    //   }
    public static void ReleaseInstance()
    {
        if (s_instance != null)
        {
            s_instance.Destroy();
            s_instance = null;
        }
    }

    // RVA: 0x15B2BEC  Ghidra: work/06_ghidra/decompiled_full/GameProcMgr/.ctor.c
    //   CProcManager___ctor(this, 0);
    private GameProcMgr() : base()
    {
    }

    // RVA: 0x15B2C6C  Ghidra: work/06_ghidra/decompiled_full/GameProcMgr/V_Start.c
    // empty body (no-op override)
    protected override void V_Start(ArrayList args)
    {
    }

    // RVA: 0x15B2C70  Ghidra: work/06_ghidra/decompiled_full/GameProcMgr/V_Destroy.c
    // empty body (no-op override)
    protected override void V_Destroy()
    {
    }
}
