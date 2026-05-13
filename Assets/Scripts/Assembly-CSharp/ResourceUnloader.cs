// Source: work/03_il2cpp_dump/dump.cs TypeDefIndex 148 + Ghidra decompiled_full/ResourceUnloader/*.c
// Field layout per dump.cs:
//   private const float CPeriod_Unload = 120;  (inlined, not in static block)
//   private static float s_Duedate_UnLoad;     // 0x0
//   private static bool s_GC_State;            // 0x4
//   private static bool s_whenIdle;            // 0x5  (backing for `idle` property)
//   private static bool s_switchScene;         // 0x6  (backing for `switchScene` property)
//   private static AsyncOperation s_asyncOP;   // 0x8

using UnityEngine;

public class ResourceUnloader
{
    private const float CPeriod_Unload = 120f;
    private static float s_Duedate_UnLoad;
    private static bool s_GC_State;
    private static bool s_whenIdle;
    private static bool s_switchScene;
    private static AsyncOperation s_asyncOP;

    // Source: Ghidra get_idle.c RVA 0x017bfb04 — return s_whenIdle (byte at +5)
    public static bool get_idle()
    {
        return s_whenIdle;
    }

    // Source: Ghidra set_idle.c RVA 0x017bfb4c — *(byte*)(+5) = value & 1
    public static void set_idle(bool value)
    {
        s_whenIdle = value;
    }

    // Source: Ghidra get_switchScene.c RVA 0x017bfb9c — return s_switchScene (byte at +6)
    public static bool get_switchScene()
    {
        return s_switchScene;
    }

    // Source: Ghidra set_switchScene.c RVA 0x017bfbe4 — *(byte*)(+6) = value & 1
    public static void set_switchScene(bool value)
    {
        s_switchScene = value;
    }

    // Source: Ghidra Start.c RVA 0x017bfc34
    //   s_Duedate_UnLoad = realtimeSinceStartup + 120
    public static void Start()
    {
        s_Duedate_UnLoad = UnityEngine.Time.realtimeSinceStartup + 120f;
    }

    // Source: Ghidra Update.c RVA 0x017bfc90
    //   if (s_asyncOP != null && s_asyncOP.isDone) s_asyncOP = null;
    public static void Update()
    {
        if (s_asyncOP != null && s_asyncOP.isDone)
        {
            s_asyncOP = null;
        }
    }

    // Source: Ghidra DoUnloadNow.c RVA 0x017bfd04
    //   if (s_asyncOP == null && ((!s_whenIdle && !s_switchScene) || ignoreIdle)) {
    //     s_Duedate_UnLoad = realtimeSinceStartup + 120;
    //     System.GC.Collect();
    //     UJDebug.LogWarning("...");  (string lit 4824)
    //     s_asyncOP = Resources.UnloadUnusedAssets();
    //     s_GC_State = false;  (write byte at +4)
    //   }
    public static void DoUnloadNow(bool ignoreIdle = false)
    {
        if (s_asyncOP == null && ((!s_whenIdle && !s_switchScene) || ignoreIdle))
        {
            s_Duedate_UnLoad = UnityEngine.Time.realtimeSinceStartup + 120f;
            System.GC.Collect();
            // TODO: PTR_StringLiteral_4824 — exact text not yet extracted; non-blocking.
            UnityEngine.Debug.LogWarning("[ResourceUnloader] Unload unused assets");
            s_asyncOP = UnityEngine.Resources.UnloadUnusedAssets();
            s_GC_State = false;
        }
    }

    // Source: dump.cs RVA 0x17BFE28 — default ctor; base Object handles init
    public ResourceUnloader() { }
}
