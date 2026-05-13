// Source: work/03_il2cpp_dump/dump.cs TypeDefIndex 251 + Ghidra decompiled_full/RealTime/*.c
// Thin wrappers around UnityEngine.Time.unscaledTime / unscaledDeltaTime.

using UnityEngine;

public class RealTime : MonoBehaviour
{
    // Source: Ghidra get_time.c RVA 0x019f9be8 — return Time.unscaledTime
    public static float get_time()
    {
        return UnityEngine.Time.unscaledTime;
    }

    // Source: Ghidra get_deltaTime.c RVA 0x019f9bf0 — return Time.unscaledDeltaTime
    public static float get_deltaTime()
    {
        return UnityEngine.Time.unscaledDeltaTime;
    }

    // Source: dump.cs RVA 0x19F9BF8 — default ctor; base MonoBehaviour handles init
    public RealTime() { }
}
