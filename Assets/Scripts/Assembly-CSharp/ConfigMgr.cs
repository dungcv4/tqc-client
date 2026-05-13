// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1A0C680, 0x1A0C728, 0x1A0C7C4, 0x1A0C824, 0x1A0C870, 0x1A0C780, 0x1A0C8B4,
//       0x1A0C8F8, 0x1A0C93C, 0x1A0C950, 0x1A0C964, 0x1A0C988, 0x1A0C9AC, 0x1A0C9C0,
//       0x1A0C9E4, 0x1A0CA00, 0x1A0CA1C, 0x1A0CA38, 0x1A0CA54, 0x1A0CA68, 0x1A0CA70,
//       0x1A0CA84, 0x1A0CAA0, 0x1A0CB4C, 0x1A0CC04, 0x1A0C720
// Ghidra dir: work/06_ghidra/decompiled_full/ConfigMgr/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 312
public class ConfigMgr
{
    private const string CONFIG_PREFIX = "SGCConf_VN_";
    private static ConfigMgr _instance;

    // RVA: 0x1A0C680  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/get_Instance.c
    public static ConfigMgr Instance { get { if (_instance == null)
        {
            _instance = new ConfigMgr();
            if (_instance == null)
            {
                throw new System.NullReferenceException();
            }
            _instance.Init();
        }
        return _instance; } }

    // RVA: 0x1A0C728  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/Init.c
    private void Init()
    {
        CheckDefaultBool(7, true);
        CheckDefaultBool(8, true);
        CheckDefaultBool(5, true);
        CheckDefaultBool(6, true);
        CheckDefaultBool(9, true);
    }

    // RVA: 0x1A0C7C4  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetPlayerPrefName.c
    private string GetPlayerPrefName(int var)
    {
        // TODO: confidence:low — no dedicated GetPlayerPrefName(int).c in decompiled_full;
        // both overloads share the same Ghidra file. Pattern from int callers:
        // they pass `var` directly; the string overload then concats the prefix.
        // Faithful translation: convert int to string then forward.
        return GetPlayerPrefName(var.ToString());
    }

    // RVA: 0x1A0C824  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetPlayerPrefName.c
    private string GetPlayerPrefName(string var)
    {
        return string.Concat(CONFIG_PREFIX, var);
    }

    // RVA: 0x1A0C870  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/CheckDefaultInt.c
    private void CheckDefaultInt(int var, int defValue)
    {
        string key = GetPlayerPrefName(var);
        if (UnityEngine.PlayerPrefs.HasKey(key))
        {
            return;
        }
        UnityEngine.PlayerPrefs.SetInt(key, defValue);
    }

    // RVA: 0x1A0C780  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/CheckDefaultBool.c
    private void CheckDefaultBool(int var, bool defValue)
    {
        string key = GetPlayerPrefName(var);
        if (UnityEngine.PlayerPrefs.HasKey(key))
        {
            return;
        }
        UnityEngine.PlayerPrefs.SetInt(key, defValue ? 1 : 0);
    }

    // RVA: 0x1A0C8B4  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/CheckDefaultStr.c
    private void CheckDefaultStr(int var, string defValue)
    {
        string key = GetPlayerPrefName(var);
        if (UnityEngine.PlayerPrefs.HasKey(key))
        {
            return;
        }
        UnityEngine.PlayerPrefs.SetString(key, defValue);
    }

    // RVA: 0x1A0C8F8  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/CheckDefaultFloat.c
    private void CheckDefaultFloat(int var, float defValue)
    {
        string key = GetPlayerPrefName(var);
        if (UnityEngine.PlayerPrefs.HasKey(key))
        {
            return;
        }
        UnityEngine.PlayerPrefs.SetFloat(key, defValue);
    }

    // RVA: 0x1A0C93C  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarInt.c
    public int GetConfigVarInt(int var)
    {
        string key = GetPlayerPrefName(var);
        return UnityEngine.PlayerPrefs.GetInt(key);
    }

    // RVA: 0x1A0C950  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarStr.c
    public string GetConfigVarStr(int var)
    {
        string key = GetPlayerPrefName(var);
        return UnityEngine.PlayerPrefs.GetString(key);
    }

    // RVA: 0x1A0C964  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarBool.c
    public bool GetConfigVarBool(int var)
    {
        // TODO: confidence:low — no dedicated GetConfigVarBool(int).c in decompiled_full;
        // both overloads share the same Ghidra file. Mirror the (string) overload's
        // logic with int key (which is the established pattern across all int overloads).
        string key = GetPlayerPrefName(var);
        int val = UnityEngine.PlayerPrefs.GetInt(key, 0);
        return val == 1;
    }

    // RVA: 0x1A0C988  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarBool.c
    public bool GetConfigVarBool(string var)
    {
        string key = GetPlayerPrefName(var);
        int val = UnityEngine.PlayerPrefs.GetInt(key, 0);
        return val == 1;
    }

    // RVA: 0x1A0C9AC  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarFloat.c
    public float GetConfigVarFloat(int var)
    {
        string key = GetPlayerPrefName(var);
        return UnityEngine.PlayerPrefs.GetFloat(key);
    }

    // RVA: 0x1A0C9C0  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarFloat.c
    public void SetConfigVarFloat(int var, float value)
    {
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.SetFloat(key, value);
    }

    // RVA: 0x1A0C9E4  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarInt.c
    public void SetConfigVarInt(int var, int value)
    {
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.SetInt(key, value);
    }

    // RVA: 0x1A0CA00  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarStr.c
    public void SetConfigVarStr(int var, string value)
    {
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.SetString(key, value);
    }

    // RVA: 0x1A0CA1C  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarBool.c
    public void SetConfigVarBool(int var, bool value)
    {
        // TODO: confidence:low — no dedicated SetConfigVarBool(int).c in decompiled_full;
        // both overloads share the same Ghidra file. Mirror the (string) overload.
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    // RVA: 0x1A0CA38  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarBool.c
    public void SetConfigVarBool(string var, bool value)
    {
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    // RVA: 0x1A0CA54  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/DeleteConfigVar.c
    public void DeleteConfigVar(int var)
    {
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.DeleteKey(key);
    }

    // RVA: 0x1A0CA68  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/ConfigVarSave.c
    public void ConfigVarSave()
    {
        UnityEngine.PlayerPrefs.Save();
    }

    // RVA: 0x1A0CA70  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarStrbyStr.c
    public string GetConfigVarStrbyStr(string var)
    {
        string key = GetPlayerPrefName(var);
        return UnityEngine.PlayerPrefs.GetString(key);
    }

    // RVA: 0x1A0CA84  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarStrbyStr.c
    public void SetConfigVarStrbyStr(string var, string value)
    {
        string key = GetPlayerPrefName(var);
        UnityEngine.PlayerPrefs.SetString(key, value);
    }

    // RVA: 0x1A0CAA0  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarLanguage.c
    public int GetConfigVarLanguage()
    {
        // local_14 = 0xc; uVar3 = System_Int32__ToString(&local_14)
        int local = 0xc;
        string s = local.ToString();
        // SGCLanguage__GetLanguageConfigStrByVersion()
        string langSuffix = SGCLanguage.GetLanguageConfigStrByVersion();
        // System_String__Concat(uVar3, "_", langSuffix) — separator at 0x34601C0 = "_"
        s = string.Concat(s, "_", langSuffix);
        s = GetPlayerPrefName(s);
        return UnityEngine.PlayerPrefs.GetInt(s, 0);
    }

    // RVA: 0x1A0CB4C  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/SetConfigVarLanguage.c
    public void SetConfigVarLanguage(int value)
    {
        int local = 0xc;
        string s = local.ToString();
        string langSuffix = SGCLanguage.GetLanguageConfigStrByVersion();
        s = string.Concat(s, "_", langSuffix);
        s = GetPlayerPrefName(s);
        UnityEngine.PlayerPrefs.SetInt(s, value);
    }

    // RVA: 0x1A0CC04  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/GetConfigVarRegion.c
    public int GetConfigVarRegion()
    {
        string key = GetPlayerPrefName(0xd);
        return UnityEngine.PlayerPrefs.GetInt(key, -1);
    }

    // RVA: 0x1A0C720  Ghidra: work/06_ghidra/decompiled_full/ConfigMgr/.ctor.c
    public ConfigMgr()
    {
        // TODO: confidence:low — .ctor.c not present in decompiled_full/ConfigMgr/.
        // Default object constructor body assumed.
    }

}
