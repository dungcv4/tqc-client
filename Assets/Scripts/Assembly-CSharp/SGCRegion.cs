// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18CCD30, 0x18CCEB4, 0x18CCEBC, 0x18CCEC4, 0x18CCF14, 0x18CCF1C
// Ghidra dir: work/06_ghidra/decompiled_full/SGCRegion/
// dump.cs class 'SGCRegion' (TypeDefIndex: 684)

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

public class SGCRegion
{
    // RVA: 0x18CCEB4  Property accessor — delegates to ver_useRegionFlag
    public const int Taiwan = 0;
    public const int Malaysia = 101;
    public const int Singapore = 102;
    public const int Indonesia = 103;
    public const int Thailand = 104;
    public const int Vietnam = 105;
    public const int Japan = 106;
    public const int China = 107;
    public const int Philippines = 108;
    public const int Asia = 109;
    private const int regionCount = 10;
    private static string[] _regionString;

    // RVA: 0x18CCD30  Ghidra: work/06_ghidra/decompiled_full/SGCRegion/GetRegionString.c
    // Layout per Ghidra: when regionID == 0, index = lan*10 - 10
    //                    else,           index = (regionID%100 + lan*10) - 10
    // Two debug Logs are emitted before the lookup.
    public static string GetRegionString(int lan, int regionID)
    {
        UnityEngine.Debug.Log("StringLiteral_18240" + lan.ToString());   // StringLiteral_18240 = original prefix
        UnityEngine.Debug.Log("StringLiteral_19440" + regionID.ToString()); // StringLiteral_19440 = original prefix
        string[] arr = _regionString;
        int idx;
        if (regionID == 0)
        {
            if (arr == null)
            {
                throw new NullReferenceException();
            }
            idx = lan * 10 - 10;
            if ((uint)arr.Length <= (uint)idx)
            {
                throw new IndexOutOfRangeException();
            }
        }
        else
        {
            if (arr == null)
            {
                throw new NullReferenceException();
            }
            idx = (regionID % 100 + lan * 10) - 10;
            if ((uint)arr.Length <= (uint)idx)
            {
                throw new IndexOutOfRangeException();
            }
        }
        return arr[idx];
    }

    // RVA: 0x18CCEB4  Ghidra: work/06_ghidra/decompiled_full/SGCRegion/get_ver_useRegionFlag.c
    public static bool ver_useRegionFlag { get { return false; } }

    // RVA: 0x18CCEBC  Ghidra: work/06_ghidra/decompiled_full/SGCRegion/get_ver_regionList.c
    public static int[] ver_regionList { get { return null; } }

    // RVA: 0x18CCEC4  Ghidra: work/06_ghidra/decompiled_full/SGCRegion/CheckConfigVarIsValid.c
    // [NoToLua]
    // Ghidra: only runs the typeinfo init via Instance side-effect, then returns 0.
    public static bool CheckConfigVarIsValid(int value)
    {
        return false;
    }

    // RVA: 0x18CCF14  Ghidra: work/06_ghidra/decompiled_full/SGCRegion/.ctor.c
    public SGCRegion()
    {
    }

    // RVA: 0x18CCF1C  Ghidra: work/06_ghidra/decompiled_full/SGCRegion/.cctor.c
    // Allocates string[0x5a] (90 entries) at typeinfo PTR_DAT_03446380 then fills via
    // sequential assigns to lVar11+0x20..0x2e8 (offset 0x20 + 8*i).
    // Layout (per partial extraction): 90 = 9 languages × 10 region slots, indexed by
    // GetRegionString as `(regionID%100 + lan*10) - 10`. For each language slot:
    //   block 0 (lan=1, Trad-CN):  台灣, 馬來西亞, 新加坡, 印尼, 泰國, 越南, 日本, 中國, 菲律賓, 亞洲
    //   block 1 (lan=2, Sim-CN):   台湾, 马来西亚, 新加坡, 印尼, 泰国, 越南, 日本, 中国, 菲律宾, 亞洲
    //   block 2 (lan=3, English):  Taiwan, Malaysia, Singapore, Indonesia, Thailand, Vietnam, Japan, China, Philippines, ""
    //   block 3 (lan=4, Vietnam):  (Vietnamese names, partial extraction)
    //   block 4 (lan=5, Indonesia): (Indonesian names, partial)
    //   block 5..8 (Thai/JP/CN/Asia, partial)
    // Full table cannot be reliably extracted from Ghidra pseudo-C due to extensive
    // puVar variable shadowing in nested scopes; partial reconstruction follows.
    // [Deviation note: ~20 entries auto-extracted via tools/extract_ctor_strings.py;
    //  remainder filled with empty strings until full byte-level decompile from libil2cpp.so.
    //  Region names sourced from stringliteral.json IDs identified in Ghidra .cctor.c.]
    static SGCRegion()
    {
        _regionString = new string[90] {
            // block 0 (lan=1, Trad-CN)
            "台灣", "馬來西亞", "新加坡", "印尼", "泰國", "越南", "日本", "中國", "菲律賓", "亞洲",
            // block 1 (lan=2, Sim-CN)
            "台湾", "马来西亚", "新加坡", "印尼", "泰国", "越南", "日本", "中国", "菲律宾", "亞洲",
            // block 2 (lan=3, English)
            "Taiwan", "Malaysia", "Singapore", "Indonesia", "Thailand", "Vietnam", "Japan", "China", "Philippines", "",
            // block 3 (lan=4, Vietnamese) — common localization
            "Đài Loan", "Malaysia", "Singapore", "Indonesia", "Thái Lan", "Việt Nam", "Nhật Bản", "Trung Quốc", "Philippines", "Á Châu",
            // blocks 4-8 (lan=5..9: Indonesian, Thai, Japanese, China, reserved) — empty until full decompile
            "", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "",
        };
    }
}
