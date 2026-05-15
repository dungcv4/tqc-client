// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1587DA0, 0x1587F38, 0x1587F98, 0x1588004, 0x1587EAC, 0x1588070, 0x15880FC,
//       0x1588188, 0x1588214, 0x15882A0, 0x158832C, 0x15883B8, 0x1588444, 0x15884D0,
//       0x158855C, 0x15885B4, 0x158860C, 0x1588664, 0x1588770, 0x1588850, 0x15888B0,
//       0x1588A14, 0x1588B58, 0x1588C9C, 0x1588D18, 0x1588D94, 0x1588DD4, 0x1588E50,
//       0x1588E90, 0x1588ED0, 0x1588F10, 0x1588F50, 0x1588F90, 0x158900C, 0x158904C,
//       0x158908C, 0x15890CC, 0x158910C, 0x1589188, 0x15891C8, 0x1589244, 0x1589284,
//       0x1589300, 0x158937C, 0x15893F8, 0x1589474, 0x15894F0, 0x158956C, 0x15895E8,
//       0x1589664, 0x15896C8, 0x15896D0
// Ghidra dir: work/06_ghidra/decompiled_full/ResourcesPath/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8223
public class ResourcesPath
{
    // [REMOVED chế cháo stub fields — replaced by auto-converted properties below
    //  (PatchHost from get_PatchHost.c RVA 0x1587DA0 etc.)]

    public const string ASSETSFOLDER = "_Assets";
    public const string LANGUAGE = "vn";
    public static string CDNVersion;
    public static string bdCreateTime;
    public static string bundleTNum;
    private static string _patchHost;
    private static string _outputPath;
    private static PatchHostData _patchData;
    private static PatchHostData _patchDataPreview;
    private static PatchHostData _patchDataOld;
    public static string CurVersion;

    // RVA: 0x1587DA0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchHost.c
    public static string PatchHost { get { // Field at offset 0x18 in static layout = _patchHost.
        // If empty, default it to PTR_StringLiteral_17191 = "https://sgc-vietnam-cdn.uj.com.tw/official/".
        if (string.IsNullOrEmpty(_patchHost))
        {
            _patchHost = "https://sgc-vietnam-cdn.uj.com.tw/official/";
        }
        // If PatchHostAndroid == "" return _patchHost else return PatchHostAndroid.
        string android = PatchHostAndroid;
        if (android == string.Empty)
        {
            return _patchHost;
        }
        return android; } set { _patchHost = value; } }

    // RVA: 0x1587F38  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/set_PatchHost.c
    
    // RVA: 0x1587F98  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/IsPreviewVersion.c
    public static bool IsPreviewVersion()
    {
        // Ghidra: returns String.op_Equality(static@0x40, static@0x30 + 0x18)
        //         = CurVersion == _patchDataPreview.Version
        if (_patchDataPreview == null)
        {
            throw new System.NullReferenceException();
        }
        return CurVersion == _patchDataPreview.Version;
    }

    // RVA: 0x1588004  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/IsOldVersion.c
    public static bool IsOldVersion()
    {
        // Ghidra: CurVersion == _patchDataOld.Version
        if (_patchDataOld == null)
        {
            throw new System.NullReferenceException();
        }
        return CurVersion == _patchDataOld.Version;
    }

    // RVA: 0x1587EAC  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchHostAndroid.c
    public static string PatchHostAndroid { get { // Ghidra: pick _patchData (0x28) when !IsPreviewVersion(), else _patchDataPreview (0x30);
        // return picked.<field@0x20> = VersionAndroid (offset 0x20 in PatchHostData object).
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.VersionAndroid; } }

    // RVA: 0x1588070  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchHostIOS.c
    public static string PatchHostIOS { get { // Ghidra: same pick; returns offset 0x38 = VersionIOS
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.VersionIOS; } }

    // RVA: 0x15880FC  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchHostPC.c
    public static string PatchHostPC { get { // Ghidra: returns offset 0x50 = VersionPC
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.VersionPC; } }

    // RVA: 0x1588188  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchAndroidVersion.c
    public static string PatchAndroidVersion { get { // Ghidra: offset 0x28 = PatchHostAndroid
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchHostAndroid; } }

    // RVA: 0x1588214  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchIOSVersion.c
    public static string PatchIOSVersion { get { // Ghidra: offset 0x40 = PatchHostIOS  (TODO: confidence:low — overload mirrors pattern from .c)
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchHostIOS; } }

    // RVA: 0x15882A0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchPCVersion.c
    public static string PatchPCVersion { get { // Ghidra: offset 0x58 = PatchHostPC
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchHostPC; } }

    // RVA: 0x158832C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchAnnounce.c
    public static string PatchAnnounce { get { // Ghidra: offset 0x68 = PatchAnnounce
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchAnnounce; } }

    // RVA: 0x15883B8  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchDefineVersion.c
    public static string PatchDefineVersion { get { // Ghidra: offset 0x70 = PatchDefineVersion
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchDefineVersion; } }

    // RVA: 0x1588444  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchServerList.c
    public static string PatchServerList { get { // Ghidra: offset 0x78 = PatchServerList
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchServerList; } }

    // RVA: 0x15884D0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchServerListVersion.c
    // Body (from Ghidra):
    //   uVar2 = IsPreviewVersion();
    //   data = (uVar2 & 1) == 0 ? _patchData (offset 0x28) : _patchDataPreview (offset 0x30);
    //   if (data == null) NRE;
    //   return *(data + 0x78) = data.PatchServerListVersion.
    // Cross-check: PatchHostData.PatchServerListVersion exists at dump.cs line 511980 offset 0x78.
    public static string PatchServerListVersion { get {
        PatchHostData data;
        if (!IsPreviewVersion())
        {
            data = _patchData;
        }
        else
        {
            data = _patchDataPreview;
        }
        if (data == null)
        {
            throw new System.NullReferenceException();
        }
        return data.PatchServerListVersion;
    } }

    // RVA: 0x158855C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchData.c
    public static PatchHostData PatchData { get { return _patchData; } }

    // RVA: 0x15885B4  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchDataPreview.c
    public static PatchHostData PatchDataPreview { get { return _patchDataPreview; } }

    // RVA: 0x158860C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_PatchDataOld.c
    public static PatchHostData PatchDataOld { get { return _patchDataOld; } }

    // RVA: 0x1588664  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/ShowPatchData.c
    public static void ShowPatchData()
    {
        // TODO: confidence:low — Ghidra body iterates PatchHostData.ShowPatchData() across the three
        // static instances with intermixed Debug.LogFormat calls; pattern is opaque without resolving
        // PTR_StringLiteral_* labels.
        if (_patchData != null) _patchData.ShowPatchData();
        if (_patchDataPreview != null) _patchDataPreview.ShowPatchData();
        if (_patchDataOld != null) _patchDataOld.ShowPatchData();
    }

    // RVA: 0x1588770  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutputPath.c
    public static string OutputPath { get { // Ghidra: if _outputPath (offset 0x20) is non-empty return it, else format
        // PTR_StringLiteral_21235 = "{0}/_Assets/" with Application.dataPath.
        if (!string.IsNullOrEmpty(_outputPath))
        {
            return _outputPath;
        }
        return string.Format("{0}/_Assets/", UnityEngine.Application.dataPath); } set { _outputPath = value; } }

    // RVA: 0x1588850  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/set_OutputPath.c
    
    // RVA: 0x15888B0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/GetAbOutPath.c
    public static string GetAbOutPath()
    {
        // Ghidra: switch on UjApplicationSetting.abBuildMode (uVar2):
        // values < 5 index into a literal table at offset (PTR_StringLiteral_12568 ...);
        // fallback => Debug.LogError("GetAbOutPath NG!") and return "".
        // Literal table per stringliteral.json: [12568=Web/, 12572=WebGL/, 2998=Android/,
        // 17215=iOS/, 10488=Steam/]
        int mode = (int)UjApplicationSetting.abBuildMode;
        if ((uint)mode < 5)
        {
            switch (mode)
            {
                case 0: return "Web/";
                case 1: return "WebGL/";
                case 2: return "Android/";
                case 3: return "iOS/";
                case 4: return "Steam/";
            }
        }
        UnityEngine.Debug.LogError("GetAbOutPath NG!");
        return string.Empty;
    }

    // RVA: 0x1588A14  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/GetAbPath.c
    public static string GetAbPath()
    {
        // Ghidra: switch on UnityEngine.Application.platform.
        // Mapping observed (RuntimePlatform values):
        //   < 8: 1=PS3->iOS/, 2=Android/(*) — actual: 1->iOS/?,2->iOS, 7->Android/
        //   else: 0x11=WebGL/, 0xb=Android/, 8=iOS/
        // Default: Debug.LogError("GetAbPath NG!") return "".
        // TODO: confidence:low — exact RuntimePlatform enum to literal mapping requires
        // resolving the IL2CPP indexed-jump table; preserved as best-effort 1-1.
        UnityEngine.RuntimePlatform plat = UnityEngine.Application.platform;
        int p = (int)plat;
        if (p < 8)
        {
            if (p < 2) return "iOS/";
            if (p == 2) return "Steam/";
            if (p == 7) return "Android/";
        }
        else
        {
            if (p == 0x11) return "WebGL/";
            if (p == 0xb) return "Android/";
            if (p == 8) return "iOS/";
        }
        UnityEngine.Debug.LogError("GetAbPath NG!");
        return string.Empty;
    }

    // RVA: 0x1588B58  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/GetPlatformName.c
    public static string GetPlatformName()
    {
        // Ghidra: same switch pattern but emits names without trailing slash:
        // Android, iOS, Steam, WebGL.
        // TODO: confidence:low — same caveat as GetAbPath re jump table mapping.
        UnityEngine.RuntimePlatform plat = UnityEngine.Application.platform;
        int p = (int)plat;
        if (p < 8)
        {
            if (p < 2) return "iOS";
            if (p == 2) return "Steam";
            if (p == 7) return "Android";
        }
        else
        {
            if (p == 0x11) return "WebGL";
            if (p == 0xb) return "Android";
            if (p == 8) return "iOS";
        }
        UnityEngine.Debug.LogError("GetPlatformName NG!");
        return string.Empty;
    }

    // RVA: 0x1588C9C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_avatarMenu.c
    public static string avatarMenu { get { // Ghidra: Concat(PatchHost, GetAbPath(), "avatar/")
        return string.Concat(PatchHost, GetAbPath(), "avatar/"); } }

    // RVA: 0x1588D18  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_designMenu.c
    public static string designMenu { get { // Ghidra: Concat(PatchHost, GetAbPath(), "_design/xls_output/")
        return string.Concat(PatchHost, GetAbPath(), "_design/xls_output/"); } }

    // RVA: 0x1588D94  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_designAsset.c
    public static string designAsset { get { return "Assets/_design/xls_pack/"; } }

    // RVA: 0x1588DD4  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_designPack.c
    public static string designPack { get { return string.Concat(PatchHost, GetAbPath(), "design/xls_pack/"); } }

    // RVA: 0x1588E50  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_avatarPath.c
    public static string avatarPath { get { return "avatar/"; } }

    // RVA: 0x1588E90  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_ScenePath.c
    public static string ScenePath { get { return "scene/"; } }

    // RVA: 0x1588ED0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_ModelPath.c
    public static string ModelPath { get { return "model/"; } }

    // RVA: 0x1588F10  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_MusicPath.c
    public static string MusicPath { get { return "music/"; } }

    // RVA: 0x1588F50  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_MenusPath.c
    public static string MenusPath { get { return "prefabs/menus/"; } }

    // RVA: 0x1588F90  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_guiPath.c
    public static string guiPath { get { return string.Concat(PatchHost, GetAbPath(), "gui/"); } }

    // RVA: 0x158900C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_mpcPath.c
    public static string mpcPath { get { return "scene/scene_mpc"; } }

    // RVA: 0x158904C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_naviPath.c
    public static string naviPath { get { return "scene/scene_navi"; } }

    // RVA: 0x158908C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_effectPath.c
    public static string effectPath { get { return "fx/"; } }

    // RVA: 0x15890CC  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_decalPath.c
    public static string decalPath { get { return "decal/"; } }

    // RVA: 0x158910C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_audioPath.c
    public static string audioPath { get { return string.Concat(PatchHost, GetAbPath(), "audio/"); } }

    // RVA: 0x1589188  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_soundPath.c
    public static string soundPath { get { return "sound/"; } }

    // RVA: 0x15891C8  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_settingPath.c
    public static string settingPath { get { return string.Concat(PatchHost, GetAbPath(), "designdata/setting/"); } }

    // RVA: 0x1589244  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_languageRelatePath.c
    public static string languageRelatePath { get { return "big5/"; } }

    // RVA: 0x1589284  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_scriptablePath.c
    public static string scriptablePath { get { return string.Concat(PatchHost, GetAbPath(), "scriptable/"); } }

    // RVA: 0x1589300  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_avatar.c
    public static string OutPath_Ab_avatar { get { return string.Concat(OutputPath, GetAbOutPath(), "avatar/"); } }

    // RVA: 0x158937C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_scene.c
    public static string OutPath_Ab_scene { get { return string.Concat(OutputPath, GetAbOutPath(), "scene/"); } }

    // RVA: 0x15893F8  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_gui.c
    public static string OutPath_Ab_gui { get { return string.Concat(OutputPath, GetAbOutPath(), "gui/"); } }

    // RVA: 0x1589474  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_mpc.c
    public static string OutPath_Ab_mpc { get { return string.Concat(OutputPath, GetAbOutPath(), "mpc/"); } }

    // RVA: 0x15894F0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_effect.c
    public static string OutPath_Ab_effect { get { return string.Concat(OutputPath, GetAbOutPath(), "effect/"); } }

    // RVA: 0x158956C  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_audio.c
    public static string OutPath_Ab_audio { get { return string.Concat(OutputPath, GetAbOutPath(), "audio/"); } }

    // RVA: 0x15895E8  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab_setting.c
    public static string OutPath_Ab_setting { get { // TODO: confidence:low — exact literal index for setting subpath not extracted; mirroring
        // pattern from sibling get_OutPath_Ab_* methods.
        return string.Concat(OutputPath, GetAbOutPath(), "designdata/setting/"); } }

    // RVA: 0x1589664  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/get_OutPath_Ab.c
    public static string OutPath_Ab { get { return string.Concat(OutputPath, GetAbOutPath()); } }

    // RVA: 0x15896C8  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/.ctor.c
    public ResourcesPath()
    {
        // base() — System_Object___ctor
    }

    // RVA: 0x15896D0  Ghidra: work/06_ghidra/decompiled_full/ResourcesPath/.cctor.c
    static ResourcesPath()
    {
        // Ghidra static field layout (offsets in static struct):
        //   0x00 CDNVersion = ""
        //   0x08 bdCreateTime = ""
        //   0x10 bundleTNum = ""
        //   0x18 _patchHost = ""
        //   0x20 _outputPath = ""
        //   0x28 _patchData = new PatchHostData()
        //   0x30 _patchDataPreview = new PatchHostData()
        //   0x38 _patchDataOld = new PatchHostData()
        //   0x40 CurVersion = ""
        CDNVersion = string.Empty;
        bdCreateTime = string.Empty;
        bundleTNum = string.Empty;
        _patchHost = string.Empty;
        _outputPath = string.Empty;
        _patchData = new PatchHostData();
        _patchDataPreview = new PatchHostData();
        _patchDataOld = new PatchHostData();
        CurVersion = string.Empty;
    }

}
