// Source: work/03_il2cpp_dump/dump.cs TypeDefIndex 118 + Ghidra decompiled_full/FpsCalculator/*.c
// Ported 1-1 from libil2cpp.so. Verified field layout via offsets in dump.cs:
//   +0x00 s_accum   (float)  — sum of 1/delta over accum window
//   +0x04 s_frames  (int)    — frame count in window
//   +0x08 s_duedate (float)  — window end time
//   +0x0c s_realTime(float)  — last sampled realtime
//   +0x10 s_fps     (float)  — smoothed FPS
// In Ghidra, static field block at base+0xb8 is accessed as float* pfVar3, with pfVar3[1] punned to int.

using UnityEngine;

public class FpsCalculator
{
    private static float s_accum;
    private static int s_frames;
    private static float s_duedate;
    private static float s_realTime;
    private static float s_fps;

    // Source: Ghidra work/06_ghidra/decompiled_full/FpsCalculator/get_fps.c RVA 0x017ba6b0
    // 1-1: return s_fps (offset 0x10)
    public static float get_fps()
    {
        return s_fps;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/FpsCalculator/Update.c RVA 0x017ba708
    // 1-1:
    //   fVar4 = realtimeSinceStartup - s_realTime;
    //   s_realTime = realtimeSinceStartup;       (re-sampled — Ghidra reads it twice)
    //   if (fVar4 > 0) {
    //     s_accum += 1.0/fVar4; s_frames += 1;
    //     if (s_duedate < s_realTime) {
    //       s_duedate = s_realTime + 0.25;
    //       s_fps = (s_fps + s_accum/s_frames) * 0.5;
    //       s_accum = 0; s_frames = 0;
    //     }
    //   }
    public static void Update()
    {
        float now1 = UnityEngine.Time.realtimeSinceStartup;
        float delta = now1 - s_realTime;
        float now2 = UnityEngine.Time.realtimeSinceStartup;
        s_realTime = now2;
        if (delta > 0f)
        {
            s_accum = s_accum + 1.0f / delta;
            s_frames = s_frames + 1;
            if (s_duedate < s_realTime)
            {
                s_duedate = s_realTime + 0.25f;
                s_fps = (s_fps + s_accum / (float)s_frames) * 0.5f;
                s_accum = 0f;
                s_frames = 0;
            }
        }
    }

    // Source: dump.cs RVA 0x17BA824 — default instance ctor; base Object.ctor handles initialization.
    public FpsCalculator() { }

    // Source: dump.cs RVA 0x17BA82C — static ctor.
    // [Deviation from Ghidra: Ghidra did not emit .cctor.c output (no decompile target).
    //  All static fields are value-types with default-zero semantics; empty cctor is equivalent
    //  to running the il2cpp default field initializers.]
    static FpsCalculator() { }
}
