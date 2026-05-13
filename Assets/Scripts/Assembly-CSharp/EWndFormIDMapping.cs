// Source: Ghidra work/06_ghidra/decompiled_full/EWndFormIDMapping/ (1 .c) — GetWndFormString ported 1-1.
// Source: dump.cs TypeDefIndex 316
// PTR_DAT_0346aab0 = typeof(EWndFormID) handle; PTR_StringLiteral_0_034465a0 = empty string sentinel.

using System;

public class EWndFormIDMapping
{
    // Source: Ghidra GetWndFormString.c  RVA 0x1A0CC20
    // Iterate Enum.GetValues(typeof(EWndFormID)); for each value v:
    //   int iv = Convert.ToInt32(v) (Ghidra: thunk_FUN_01560368)
    //   skip if iv == 0; continue if iv != eWndFormID
    //   on match: return Enum.ToString of that value (Ghidra: System_Enum__ToString(&local_58))
    //     where local_58 = {type=typeof(EWndFormID), value=eWndFormID, -1 sentinel}
    // Fallback: return "" (PTR_StringLiteral_0_034465a0).
    public static string GetWndFormString(uint eWndFormID)
    {
        foreach (object item in Enum.GetValues(typeof(EWndFormID)))
        {
            int iv = Convert.ToInt32(item);
            if (iv == 0) continue;
            if (iv != (int)eWndFormID) continue;
            return ((EWndFormID)iv).ToString();
        }
        return string.Empty;
    }

    // Source: Ghidra (no .ctor.c) — default ctor.
    public EWndFormIDMapping() { }
}
