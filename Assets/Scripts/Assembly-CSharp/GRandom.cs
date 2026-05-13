// Source: Ghidra work/06_ghidra/decompiled_full/GRandom/ (5 .c) — all methods ported 1-1.
// Source: dump.cs TypeDefIndex 121
// Static field layout: s_init@0 (byte/bool), s_seed@4 (int).
// LCG constants 0x343FD and 0x269EC3 match Microsoft Visual C++ rand() implementation
// (Park-Miller-style with mask 0x7FFFFFFF). XOR seed mix uses constant 0x654A231C.

using UnityEngine;

public class GRandom
{
    private static bool s_init;
    private static int s_seed;

    // Source: Ghidra GenerateSeed.c  RVA 0x17BAF10
    // return UnityEngine.Random.Range(0, int.MaxValue) ^ 0x654A231C;
    private static int GenerateSeed()
    {
        return Random.Range(0, 0x7FFFFFFF) ^ 0x654A231C;
    }

    // Source: Ghidra get_Seed.c  RVA 0x17BAF38
    // Lazy init: if !s_init, generate s_seed = (Random.Range(0,MAX) & MASK) ^ XOR_CONST and mark init.
    // Return current s_seed.
    public static int Seed
    {
        get
        {
            if (!s_init)
            {
                s_seed = (Random.Range(0, 0x7FFFFFFF) & 0x7FFFFFFF) ^ 0x654A231C;
                s_init = true;
            }
            return s_seed;
        }
        // Source: Ghidra set_Seed.c  RVA 0x17BAFC0
        // s_seed = value & 0x7FFFFFFF; s_init = true.
        set
        {
            s_seed = value & 0x7FFFFFFF;
            s_init = true;
        }
    }

    // Source: Ghidra GetRandom.c  RVA 0x17BB018
    // Lazy init same as get_Seed. Apply LCG: s_seed = (s_seed * 0x343FD + 0x269EC3) & 0x7FFFFFFF.
    // Return new s_seed (Ghidra's `void` signature is decompilation artifact — w0 still holds new value).
    public static int GetRandom()
    {
        if (!s_init)
        {
            s_seed = (Random.Range(0, 0x7FFFFFFF) & 0x7FFFFFFF) ^ 0x654A231C;
            s_init = true;
        }
        s_seed = unchecked((s_seed * 0x343FD + 0x269EC3) & 0x7FFFFFFF);
        return s_seed;
    }

    // Source: Ghidra GetRange.c  RVA 0x17BB0C0
    // Normalize ordering: min = smaller of (nMin, nMax), max = larger. Range = max - min + 1.
    // Return GetRandom() mod range, offset by min (manual mod via division).
    public static int GetRange(int nMin, int nMax)
    {
        int min, max;
        if (nMin <= nMax) { min = nMin; max = nMax; }
        else              { min = nMax; max = nMin; }
        int range = max - min + 1;
        int r = GetRandom();
        int q = (range != 0) ? r / range : 0;
        return r - q * range + min;
    }

    // Source: Ghidra (no .ctor.c) — default empty ctor. RVA 0x17BB0F8.
    public GRandom() { }
}
