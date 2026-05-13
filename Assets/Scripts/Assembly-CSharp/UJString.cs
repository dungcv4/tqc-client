// Source: Ghidra work/06_ghidra/decompiled_full/UJString/ (2 .c) — both methods ported 1-1.
// Source: dump.cs TypeDefIndex 706

public class UJString
{
    // Source: Ghidra CustomEndsWith.c  RVA 0x18CE96C
    // Walk from end of both strings; if any pair differs → false. After loop,
    // if b ran out first and len(b) <= len(a): true; if a still has chars: false;
    // otherwise true iff len(a) <= len(b).
    public static bool CustomEndsWith(string a, string b)
    {
        if (a == null || b == null) throw new System.NullReferenceException();
        int iA = a.Length - 1;
        int iB = b.Length - 1;
        while ((iA | iB) >= 0)
        {
            if (a[iA] != b[iB]) return false;
            iA--;
            iB--;
        }
        if (iB < 0 && b.Length <= a.Length) return true;
        if (iA >= 0) return false;
        return a.Length <= b.Length;
    }

    // Source: Ghidra CustomStartsWith.c  RVA 0x18CEA24
    // Walk forward up to min(len(a), len(b)). Track last matching index. After loop,
    // if a is shorter than b OR last index != len(b): return (len(a) <= len(b) && last == len(a));
    // otherwise true.
    public static bool CustomStartsWith(string a, string b)
    {
        if (a == null || b == null) throw new System.NullReferenceException();
        int lenA = a.Length;
        int lenB = b.Length;
        int last = 0;
        if (lenB > 0 && lenA > 0)
        {
            int k = 0;
            int loopMax = (lenB - 1U <= (uint)(lenA - 1)) ? lenB : lenA;
            do
            {
                last = k;
                if (a[k] != b[k]) break;
                k++;
                last = loopMax;
            } while (loopMax != k);
        }
        if (lenA < lenB || last != lenB)
        {
            return lenA <= lenB && last == lenA;
        }
        return true;
    }

    // Source: Ghidra (no .ctor.c) — default empty ctor. RVA 0x18CEAEC.
    public UJString() { }
}
