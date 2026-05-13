// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18CB4D0, 0x18CB528, 0x18CB59C, 0x18CBBCC, 0x18CBBD4, 0x18CBBDC, 0x18CBBE4, 0x18CBC24, 0x18CBCEC, 0x18CBD98, 0x18CBDA0
// Ghidra dir: work/06_ghidra/decompiled_full/SGCLanguage/
// dump.cs class 'SGCLanguage' (TypeDefIndex: 681)

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

public class SGCLanguage
{
    // RVA: 0x18CBBD4  Property accessor — delegates to UseLanguageSystem
    public const int Chinese_Trad = 1;
    public const int Chinese_Sim = 2;
    public const int English = 3;
    public const int Vietnam = 4;
    public const int Indonesia = 5;
    public const int Thailand = 6;
    public const int Japan = 7;
    public const int China = 8;
    public const int Asia = 9;
    public const int Max = 9;
    private static string[] languageString;

    // RVA: 0x18CB4D0  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/get_GetLanguageString.c
    public static string[] GetLanguageString { get { return languageString; } }

    // RVA: 0x18CB528  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/get_ver_defaultUseLanguageList.c
    // Ghidra: alloc int[2] at typeinfo 0x03446b28; arr[0] = 1; arr[1] = 4;
    public static int[] ver_defaultUseLanguageList
    {
        get
        {
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 4;
            return arr;
        }
    }

    // RVA: 0x18CB59C  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/getBuildBundleLanguageVersion.c
    // [NoToLua]
    // Original Ghidra: 5-branch String.op_Equality cascade against bundle name path strings.
    // Branch 1 ("SEA_*") → int[2]{ -, 4 }
    // Branch 2 ("VIN_*") → int[2]{ 1, 4 }
    // Branch 3 ("OBT_*"/"OFFICIAL_*"/"PREVIEW_*") → int[3] (defaults)
    // Branch 4 ("JP_*") → int[2]{ 1, 7 }
    // Branch 5 ("CHINA_*") → int[1]{ 8 }
    // Branch 6 ("ASIA_*") → int[3] (defaults)
    // Else → int[9] (defaults)
    // Default arrays (puVar7=PTR_DAT_03460670/0678/0688) initialized via RuntimeHelpers.InitializeArray
    // — exact element values not extractable from Ghidra without RDATA dump.
    public static int[] getBuildBundleLanguageVersion(string path)
    {
        if (string.Equals(path, "SEA_OBT_Android") || string.Equals(path, "SEA_OBT_IOS") ||
            string.Equals(path, "SEA_OFFICIAL_Android") || string.Equals(path, "SEA_OFFICIAL_IOS"))
        {
            // Source: Ghidra getBuildBundleLanguageVersion.c — array of size 4 from PTR_DAT_03460680.
            // FieldRVA blob in libil2cpp.so contains the language IDs; sequences {3,4,5,6}
            // (English, Vietnam, Indonesia, Thailand) found at multiple offsets — best-fit guess
            // for SEA region target languages (English+local for VN/ID/TH).
            // TODO: confidence:medium — verify exact 4-element values when Ghidra re-decompile available.
            int[] arr = new int[4];
            arr[0] = 3;  // English
            arr[1] = 4;  // Vietnam
            arr[2] = 5;  // Indonesia
            arr[3] = 6;  // Thailand
            return arr;
        }
        if (string.Equals(path, "VIN_OBT_Android") || string.Equals(path, "VIN_OBT_IOS") ||
            string.Equals(path, "VIN_OFFICIAL_Android") || string.Equals(path, "VIN_OFFICIAL_IOS"))
        {
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 4;
            return arr;
        }
        if (string.Equals(path, "OBT_Android") || string.Equals(path, "OBT_IOS") ||
            string.Equals(path, "OBT_Steam") || string.Equals(path, "OFFICIAL_Android") ||
            string.Equals(path, "OFFICIAL_IOS") || string.Equals(path, "OFFICIAL_Steam") ||
            string.Equals(path, "PREVIEW_Android") || string.Equals(path, "PREVIEW_IOS"))
        {
            // Source: Ghidra getBuildBundleLanguageVersion.c — array of size 3 from PTR_DAT_03460670.
            // Likely {1, 2, 3} (Trad Chinese, Sim Chinese, English) — generic Asian release default.
            // TODO: confidence:medium — verify exact 3-element values via FieldRVA blob extraction.
            int[] arr = new int[3];
            arr[0] = 1;  // Chinese_Trad
            arr[1] = 2;  // Chinese_Sim
            arr[2] = 3;  // English
            return arr;
        }
        if (string.Equals(path, "JP_OBT_Android") || string.Equals(path, "JP_OBT_IOS") ||
            string.Equals(path, "JP_OFFICIAL_Android") || string.Equals(path, "JP_OFFICIAL_IOS"))
        {
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 7;
            return arr;
        }
        if (string.Equals(path, "CHINA_OBT_Android") || string.Equals(path, "CHINA_OBT_IOS") ||
            string.Equals(path, "CHINA_OBT_PC") || string.Equals(path, "CHINA_OFFICIAL_Android") ||
            string.Equals(path, "CHINA_OFFICIAL_IOS") || string.Equals(path, "CHINA_OFFICIAL_PC"))
        {
            int[] arr = new int[1];
            arr[0] = 8;
            return arr;
        }
        if (string.Equals(path, "ASIA_OBT_Android") || string.Equals(path, "ASIA_OBT_IOS") ||
            string.Equals(path, "ASIA_OFFICIAL_Android") || string.Equals(path, "ASIA_OFFICIAL_IOS"))
        {
            // Source: Ghidra getBuildBundleLanguageVersion.c — array of size 3 from PTR_DAT_03460678.
            // Likely {1, 7, 9} (Chinese_Trad, Japan, Asia) — ASIA build target group.
            // TODO: confidence:medium — verify exact 3-element values via FieldRVA blob.
            int[] arr = new int[3];
            arr[0] = 1;  // Chinese_Trad
            arr[1] = 7;  // Japan
            arr[2] = 9;  // Asia (region marker)
            return arr;
        }
        // Else branch (default): int[9] from PTR_DAT_03460688
        // Source: Ghidra — DEFAULT array of size 9 contains all language IDs {1..9}.
        // Found at libil2cpp.so offset 0x836dd0 and 0x86ee40: {1,2,3,4,5,6,7,8,9}.
        {
            int[] arr = new int[9];
            arr[0] = 1;
            arr[1] = 2;
            arr[2] = 3;
            arr[3] = 4;
            arr[4] = 5;
            arr[5] = 6;
            arr[6] = 7;
            arr[7] = 8;
            arr[8] = 9;
            return arr;
        }
    }

    // RVA: 0x18CBBCC  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/get_ver_defaultUseLanguage.c
    public static int ver_defaultUseLanguage { get { return 4; } }

    // RVA: 0x18CBBD4  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/get_UseLanguageSystem.c
    public static bool UseLanguageSystem { get { return false; } }

    // RVA: 0x18CBBDC  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/get_DebugVersionLanguage.c
    public static int DebugVersionLanguage { get { return 1; } }

    // RVA: 0x18CBBE4  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/GetLanguageConfigStrByVersion.c
    // [NoToLua]
    // Returns StringLiteral_12394 = "VIN"
    public static string GetLanguageConfigStrByVersion()
    {
        return "VIN";
    }

    // RVA: 0x18CBC24  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/CheckConfigVarIsValid.c
    // [NoToLua]
    public static bool CheckConfigVarIsValid(int value)
    {
        if (value == 0)
        {
            return false;
        }
        bool result = false;
        ulong i = 0;
        while (true)
        {
            int[] list = ver_defaultUseLanguageList;
            if (list == null) break;
            if ((long)list.Length <= (long)i)
            {
                return result;
            }
            list = ver_defaultUseLanguageList;
            if (list == null) break;
            if ((ulong)list.Length <= i)
            {
                throw new IndexOutOfRangeException();
            }
            int element = list[i];
            i = i + 1;
            if (element == value)
            {
                result = true;
            }
        }
        throw new NullReferenceException();
    }

    // RVA: 0x18CBCEC  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/SetConfigDefaultLanguage.c
    // [NoToLua]
    public static void SetConfigDefaultLanguage(int value)
    {
        if (value == 0)
        {
            ConfigMgr inst = ConfigMgr.Instance;
            if (inst != null)
            {
                inst.SetConfigVarLanguage(4);
                inst = ConfigMgr.Instance;
                if (inst != null)
                {
                    inst.ConfigVarSave();
                    return;
                }
            }
            throw new NullReferenceException();
        }
        return;
    }

    // RVA: 0x18CBD98  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/.ctor.c
    public SGCLanguage()
    {
    }

    // RVA: 0x18CBDA0  Ghidra: work/06_ghidra/decompiled_full/SGCLanguage/.cctor.c
    // Allocates string[9] at typeinfo PTR_DAT_03446380, fills indexes 0..8 with
    // the 9 language code string literals, then stores into static field at offset 0xb8.
    static SGCLanguage()
    {
        languageString = new string[9];
        languageString[0] = "TW";    // StringLiteral_10809  (Chinese_Trad)
        languageString[1] = "CN";    // StringLiteral_3552   (Chinese_Sim)
        languageString[2] = "EN";    // StringLiteral_4896   (English)
        languageString[3] = "VIET";  // StringLiteral_12391  (Vietnam)
        languageString[4] = "IND";   // StringLiteral_6380   (Indonesia)
        languageString[5] = "THAI";  // StringLiteral_10746  (Thailand)
        languageString[6] = "JP";    // StringLiteral_7205   (Japan)
        languageString[7] = "CHINA"; // StringLiteral_3536   (China)
        languageString[8] = "ASIA";  // StringLiteral_2770   (Asia)
    }
}
