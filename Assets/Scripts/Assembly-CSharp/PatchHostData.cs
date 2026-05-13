// Source: Ghidra work/06_ghidra/decompiled_full/PatchHostData/ (3 methods, 1-1 port)
// Source: dump.cs TypeDefIndex 8222 (Assembly-CSharp-firstpass.dll)
// RVAs: 0x015875b0 (.ctor), 0x015876c8 (IsPatchAllDone), 0x015877a0 (ShowPatchData)

using UnityEngine;

public class PatchHostData
{
    // Field offsets per dump.cs:
    public string Version;                 // 0x10  (NOT inited by .ctor — defaults null per Ghidra)
    public string VersionAndroid;          // 0x18
    public string PatchHostAndroid;        // 0x20
    public string PatchAndroidVersion;     // 0x28
    public string VersionIOS;              // 0x30
    public string PatchHostIOS;            // 0x38
    public string PatchIOSVersion;         // 0x40
    public string VersionPC;               // 0x48
    public string PatchHostPC;             // 0x50
    public string PatchPCVersion;          // 0x58
    public string PatchAnnounce;           // 0x60
    public string PatchDefineVersion;      // 0x68
    public string PatchServerList;         // 0x70
    public string PatchServerListVersion;  // 0x78

    // Source: Ghidra work/06_ghidra/decompiled_full/PatchHostData/.ctor.c
    // RVA: 0x015875b0
    // Note: Ghidra body inits offsets 0x18→0x78 (13 fields) to empty string;
    //       Version field (0x10) is NOT initialized — keeps null default.
    public PatchHostData()
    {
        VersionAndroid = "";
        PatchHostAndroid = "";
        PatchAndroidVersion = "";
        VersionIOS = "";
        PatchHostIOS = "";
        PatchIOSVersion = "";
        VersionPC = "";
        PatchHostPC = "";
        PatchPCVersion = "";
        PatchAnnounce = "";
        PatchDefineVersion = "";
        PatchServerList = "";
        PatchServerListVersion = "";
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/PatchHostData/IsPatchAllDone.c
    // RVA: 0x015876c8
    // Returns true if 7 critical fields (offsets 0x20, 0x28, 0x38, 0x40, 0x60, 0x68, 0x70)
    // are all non-empty (uses String.op_Inequality with "" — equivalent to `!= ""`).
    public bool IsPatchAllDone()
    {
        return PatchHostAndroid != ""
            && PatchAndroidVersion != ""
            && PatchHostIOS != ""
            && PatchIOSVersion != ""
            && PatchAnnounce != ""
            && PatchDefineVersion != ""
            && PatchServerList != "";
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/PatchHostData/ShowPatchData.c
    // RVA: 0x015877a0
    // 13 sequential Debug.LogFormat calls, one per inited field (skips Version field 0x10).
    // Format strings extracted from work/03_il2cpp_dump/stringliteral.json:
    //   StringLit#12483="Version Android: {0}", #2995="Android Bundle : {0}",
    //   #2997="Android Version: {0}",  #12484="Version IOS    : {0}",
    //   #6388="IOS Bundle     : {0}",  #6390="IOS Version    : {0}",
    //   #12485="Version PC    : {0}",  #8913="PC Bundle     : {0}",
    //   #8914="PC Version    : {0}",   #3014="Announce       : {0}",
    //   #4719="DefineVersion  : {0}",  #9947="ServerList     : {0}",
    //   #9949="ServerListVer  : {0}".
    public void ShowPatchData()
    {
        Debug.LogFormat("Version Android: {0}", VersionAndroid);
        Debug.LogFormat("Android Bundle : {0}", PatchHostAndroid);
        Debug.LogFormat("Android Version: {0}", PatchAndroidVersion);
        Debug.LogFormat("Version IOS    : {0}", VersionIOS);
        Debug.LogFormat("IOS Bundle     : {0}", PatchHostIOS);
        Debug.LogFormat("IOS Version    : {0}", PatchIOSVersion);
        Debug.LogFormat("Version PC    : {0}", VersionPC);
        Debug.LogFormat("PC Bundle     : {0}", PatchHostPC);
        Debug.LogFormat("PC Version    : {0}", PatchPCVersion);
        Debug.LogFormat("Announce       : {0}", PatchAnnounce);
        Debug.LogFormat("DefineVersion  : {0}", PatchDefineVersion);
        Debug.LogFormat("ServerList     : {0}", PatchServerList);
        Debug.LogFormat("ServerListVer  : {0}", PatchServerListVersion);
    }
}
