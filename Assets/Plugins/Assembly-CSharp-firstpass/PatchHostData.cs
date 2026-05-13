// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15875B0, 0x15876C8, 0x15877A0
// Ghidra dir: work/06_ghidra/decompiled_full/PatchHostData/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8222
public class PatchHostData
{
    public string Version;
    public string VersionAndroid;
    public string PatchHostAndroid;
    public string PatchAndroidVersion;
    public string VersionIOS;
    public string PatchHostIOS;
    public string PatchIOSVersion;
    public string VersionPC;
    public string PatchHostPC;
    public string PatchPCVersion;
    public string PatchAnnounce;
    public string PatchDefineVersion;
    public string PatchServerList;
    public string PatchServerListVersion;

    // RVA: 0x15875B0  Ghidra: work/06_ghidra/decompiled_full/PatchHostData/.ctor.c
    public PatchHostData()
    {
        // Ghidra .ctor inits all 13 string fields at offsets 0x18..0x78 to PTR_StringLiteral_0
        // (= System.String.Empty).
        Version = string.Empty;
        VersionAndroid = string.Empty;
        PatchHostAndroid = string.Empty;
        PatchAndroidVersion = string.Empty;
        VersionIOS = string.Empty;
        PatchHostIOS = string.Empty;
        PatchIOSVersion = string.Empty;
        VersionPC = string.Empty;
        PatchHostPC = string.Empty;
        PatchPCVersion = string.Empty;
        PatchAnnounce = string.Empty;
        PatchDefineVersion = string.Empty;
        PatchServerList = string.Empty;
    }

    // RVA: 0x15876C8  Ghidra: work/06_ghidra/decompiled_full/PatchHostData/IsPatchAllDone.c
    public bool IsPatchAllDone()
    {
        // Ghidra checks string.op_Inequality(field, "") at offsets:
        // 0x20=VersionAndroid, 0x28=PatchHostAndroid, 0x38=VersionIOS,
        // 0x40=PatchHostIOS, 0x60=PatchPCVersion, 0x68=PatchAnnounce, 0x70=PatchDefineVersion
        if (VersionAndroid != string.Empty &&
            PatchHostAndroid != string.Empty &&
            VersionIOS != string.Empty &&
            PatchHostIOS != string.Empty &&
            PatchPCVersion != string.Empty &&
            PatchAnnounce != string.Empty &&
            PatchDefineVersion != string.Empty)
        {
            return true;
        }
        return false;
    }

    // RVA: 0x15877A0  Ghidra: work/06_ghidra/decompiled_full/PatchHostData/ShowPatchData.c
    public void ShowPatchData()
    {
        // Ghidra emits a sequence of UnityEngine.Debug.LogFormat(<format>, <field>) calls
        // for each field at offsets 0x18..0x78 in declaration order (after the first one
        // pulls 0x18 into the format value slot). The exact ordering Ghidra reveals:
        // 0x18, 0x20, 0x28, 0x30, 0x38, 0x40, 0x48, 0x50, 0x58, 0x60, 0x68, 0x70, 0x78
        // with corresponding format string literals.
        // TODO: confidence:low — exact format string literals (PTR_StringLiteral_*) not
        // resolved here; faithful field traversal preserved.
        UnityEngine.Debug.LogFormat("Version: {0}", new object[] { Version });
        UnityEngine.Debug.LogFormat("VersionAndroid: {0}", new object[] { VersionAndroid });
        UnityEngine.Debug.LogFormat("PatchHostAndroid: {0}", new object[] { PatchHostAndroid });
        UnityEngine.Debug.LogFormat("PatchAndroidVersion: {0}", new object[] { PatchAndroidVersion });
        UnityEngine.Debug.LogFormat("VersionIOS: {0}", new object[] { VersionIOS });
        UnityEngine.Debug.LogFormat("PatchHostIOS: {0}", new object[] { PatchHostIOS });
        UnityEngine.Debug.LogFormat("PatchIOSVersion: {0}", new object[] { PatchIOSVersion });
        UnityEngine.Debug.LogFormat("VersionPC: {0}", new object[] { VersionPC });
        UnityEngine.Debug.LogFormat("PatchHostPC: {0}", new object[] { PatchHostPC });
        UnityEngine.Debug.LogFormat("PatchPCVersion: {0}", new object[] { PatchPCVersion });
        UnityEngine.Debug.LogFormat("PatchAnnounce: {0}", new object[] { PatchAnnounce });
        UnityEngine.Debug.LogFormat("PatchDefineVersion: {0}", new object[] { PatchDefineVersion });
        UnityEngine.Debug.LogFormat("PatchServerList: {0}", new object[] { PatchServerList });
    }

}
