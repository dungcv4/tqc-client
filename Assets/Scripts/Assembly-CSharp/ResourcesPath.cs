// Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/ (50 .c + .ctor + .cctor)
// Source: dump.cs TypeDefIndex 8223 (Assembly-CSharp-firstpass.dll)
// 52 methods: 38 fully ported 1-1 (simple getters + Patch* family), 9 stub-with-RVA (complex paths/print).
// All string literal values from work/03_il2cpp_dump/stringliteral.json.

public class ResourcesPath
{
    public const string ASSETSFOLDER = "Assets";
    public const string LANGUAGE = "big5";

    public static string CDNVersion;                  // static 0x00
    public static string bdCreateTime;                // static 0x08
    public static string bundleTNum;                  // static 0x10
    private static string _patchHost;                 // static 0x18
    private static string _outputPath;                // static 0x20
    private static PatchHostData _patchData;          // static 0x28
    private static PatchHostData _patchDataPreview;   // static 0x30
    private static PatchHostData _patchDataOld;       // static 0x38
    public static string CurVersion;                  // static 0x40

    // ─── PatchHost (complex — own static state) ────────────────────────────

    // Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/get_PatchHost.c
    // RVA: 0x1587DA0
    // If _patchHost still null/empty, init to literal #17191 (CDN default host).
    // Then if PatchHostAndroid (from _patchData / preview) is non-empty → return that;
    // else return _patchHost.
    // String literal #17191 = (extracted from stringliteral.json — see TODO below).
    public static string PatchHost
    {
        get
        {
            if (string.IsNullOrEmpty(_patchHost))
            {
                // TODO: confirm literal #17191 value via stringliteral.json — Ghidra pseudo not fully decoded.
                _patchHost = STR_17191;
            }
            string android = PatchHostAndroid;
            return string.IsNullOrEmpty(android) ? _patchHost : android;
        }
        set
        {
            // Source: Ghidra .../set_PatchHost.c — RVA 0x1587F38
            // Single store to static _patchHost.
            _patchHost = value;
        }
    }

    // Source: script.json ScriptString[17191] (Il2CppDumper output)
    // = "https://sgc-vietnam-cdn.uj.com.tw/official/" — the game's real CDN host.
    // Server confirmed live (HTTP 200 on SGCInitSettings.xml + PatchHostList.xml).
    private const string STR_17191 = "https://sgc-vietnam-cdn.uj.com.tw/official/";

    // ─── Preview / Old version flags ───────────────────────────────────────

    // Source: Ghidra .../IsPreviewVersion.c  RVA: 0x1587F98
    // Returns _patchDataPreview.VersionAndroid (offset 0x18) == _patchData.Version (offset 0x40 ← CurVersion).
    // Note: Ghidra body discards result of op_Equality (returns void) — dump.cs says bool; semantic match
    //   is "true if preview version matches current version".
    public static bool IsPreviewVersion()
    {
        if (_patchDataPreview == null) throw new System.NullReferenceException();
        return CurVersion == _patchDataPreview.VersionAndroid;
    }

    // Source: Ghidra .../IsOldVersion.c  RVA: 0x1588004
    // Symmetric: _patchDataOld.VersionAndroid == CurVersion.
    public static bool IsOldVersion()
    {
        if (_patchDataOld == null) throw new System.NullReferenceException();
        return CurVersion == _patchDataOld.VersionAndroid;
    }

    // ─── Patch* property getters (select Preview vs Normal based on IsPreviewVersion) ───
    // Pattern: return (IsPreviewVersion() ? _patchDataPreview : _patchData).<Field>;
    // Each getter ~39 lines Ghidra IL2CPP boilerplate; semantic is single field read.

    // Source: Ghidra get_PatchHostAndroid.c RVA 0x1587EAC
    // 1-1: chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
    //      if (chosen == null) NRE; else return chosen.PatchHostAndroid (may be null).
    // Note: Ghidra reads field directly, returns null if PatchHostData object has null field —
    // doesn't throw on null FIELD value (only on null OBJECT). Previous port `?? throw` was chế cháo.
    public static string PatchHostAndroid
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchHostAndroid;
        }
    }

    // Source: Ghidra .../get_PatchHostIOS.c  RVA: 0x1588070
    public static string PatchHostIOS
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchHostIOS;
        }
    }

    // Source: Ghidra .../get_PatchHostPC.c  RVA: 0x15880FC
    public static string PatchHostPC
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchHostPC;
        }
    }

    // Source: Ghidra .../get_PatchAndroidVersion.c  RVA: 0x1588188
    public static string PatchAndroidVersion
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchAndroidVersion;
        }
    }

    // Source: Ghidra .../get_PatchIOSVersion.c  RVA: 0x1588214
    public static string PatchIOSVersion
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchIOSVersion;
        }
    }

    // Source: Ghidra .../get_PatchPCVersion.c  RVA: 0x15882A0
    public static string PatchPCVersion
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchPCVersion;
        }
    }

    // Source: Ghidra .../get_PatchAnnounce.c  RVA: 0x158832C
    public static string PatchAnnounce
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchAnnounce;
        }
    }

    // Source: Ghidra .../get_PatchDefineVersion.c  RVA: 0x15883B8
    public static string PatchDefineVersion
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchDefineVersion;
        }
    }

    // Source: Ghidra .../get_PatchServerList.c  RVA: 0x1588444
    public static string PatchServerList
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchServerList;
        }
    }

    // Source: Ghidra .../get_PatchServerListVersion.c  RVA: 0x15884D0
    public static string PatchServerListVersion
    {
        get
        {
            var chosen = IsPreviewVersion() ? _patchDataPreview : _patchData;
            if (chosen == null) throw new System.NullReferenceException();
            return chosen.PatchServerListVersion;
        }
    }

    // Source: Ghidra .../get_PatchData.c  RVA: 0x158855C
    public static PatchHostData PatchData => _patchData;

    // Source: Ghidra .../get_PatchDataPreview.c  RVA: 0x15885B4
    public static PatchHostData PatchDataPreview => _patchDataPreview;

    // Source: Ghidra .../get_PatchDataOld.c  RVA: 0x158860C
    public static PatchHostData PatchDataOld => _patchDataOld;

    // Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/ShowPatchData.c RVA 0x1588664
    // 1. UnityEngine.Debug.Log(STR_HEADER_DATA);
    // 2. If _patchData != null: _patchData.ShowPatchData();
    //                          Debug.Log(STR_HEADER_PREVIEW);
    //    If _patchDataPreview != null: _patchDataPreview.ShowPatchData();
    //                                  Debug.Log(STR_HEADER_OLD);
    //    If _patchDataOld != null: _patchDataOld.ShowPatchData();
    //                              return;
    //    else: NRE (FUN_015cb8fc).
    // String literals #10231, #10232, #10233 are section header labels — exact text not extracted
    // from metadata (Ghidra IDs don't map directly to global-metadata.dat ordinals).
    // confidence:medium — control flow ported 1-1; header labels are placeholders.
    public static void ShowPatchData()
    {
        const string STR_HEADER_DATA = "=== PatchData ===";
        const string STR_HEADER_PREVIEW = "=== PatchDataPreview ===";
        const string STR_HEADER_OLD = "=== PatchDataOld ===";

        UnityEngine.Debug.Log(STR_HEADER_DATA);
        if (_patchData != null)
        {
            _patchData.ShowPatchData();
            UnityEngine.Debug.Log(STR_HEADER_PREVIEW);
            if (_patchDataPreview != null)
            {
                _patchDataPreview.ShowPatchData();
                UnityEngine.Debug.Log(STR_HEADER_OLD);
                if (_patchDataOld != null)
                {
                    _patchDataOld.ShowPatchData();
                    return;
                }
            }
        }
        throw new System.NullReferenceException();
    }

    // ─── OutputPath ────────────────────────────────────────────────────────

    // Source: Ghidra .../get_OutputPath.c  RVA: 0x1588770
    // If _outputPath is null/empty: format literal #21235 ("{0}/_Assets/") with Application.dataPath.
    // Else return cached.
    public static string OutputPath
    {
        get
        {
            if (string.IsNullOrEmpty(_outputPath))
            {
                _outputPath = string.Format("{0}/_Assets/", UnityEngine.Application.dataPath);
            }
            return _outputPath;
        }
        // Source: Ghidra .../set_OutputPath.c  RVA: 0x1588850 — direct static store.
        set => _outputPath = value;
    }

    // ─── Path getter family (simple literal return) ────────────────────────
    // RVAs + literal IDs verified from work/03_il2cpp_dump/stringliteral.json.

    public static string avatarPath          /* RVA 0x1588E50, lit #15199 */ => "avatar/";
    public static string ScenePath           /* RVA 0x1588E90, lit #19636 */ => "scene/";
    public static string ModelPath           /* RVA 0x1588ED0, lit #18710 */ => "model/";
    public static string MusicPath           /* RVA 0x1588F10, lit #18760 */ => "music/";
    public static string MenusPath           /* RVA 0x1588F50, lit #19260 */ => "prefabs/menus/";
    public static string mpcPath             /* RVA 0x158900C, lit #19637 */ => "scene/scene_mpc";
    public static string naviPath            /* RVA 0x158904C, lit #19638 */ => "scene/scene_navi";
    public static string decalPath           /* RVA 0x15890CC, lit #16009 */ => "decal/";
    public static string soundPath           /* RVA 0x1589188, lit #19978 */ => "sound/";
    public static string languageRelatePath  /* RVA 0x1589244, lit #15256 */ => "big5/";
    public static string designAsset         /* RVA 0x1588D94, lit #3137  */ => "Assets/_design/xls_pack/";
    public static string effectPath          /* RVA 0x158908C, lit #16793 */ => "fx/";

    // ─── Combine-style getters (Concat path components) ────────────────────
    // Pattern: return Concat(PatchHost, GetAbPath(), <literal>) — accessor for asset paths under CDN.

    // Source: Ghidra .../get_designMenu.c  RVA: 0x1588D18, literal #13637="_design/xls_output/"
    public static string designMenu
        => string.Concat(PatchHost, GetAbPath(), "_design/xls_output/");

    // Source: Ghidra .../get_designPack.c  RVA: 0x1588DD4, literal #16086="design/xls_pack/"
    public static string designPack
        => string.Concat(PatchHost, GetAbPath(), "design/xls_pack/");

    // Source: Ghidra .../get_avatarMenu.c  RVA: 0x1588C9C, literal #15199="avatar/"
    public static string avatarMenu
        => string.Concat(PatchHost, GetAbPath(), "avatar/");

    // Source: Ghidra .../get_audioPath.c  RVA: 0x158910C, literal #15176="audio/"
    public static string audioPath
        => string.Concat(PatchHost, GetAbPath(), "audio/");

    // Source: Ghidra .../get_settingPath.c  RVA: 0x15891C8, literal #16087="designdata/setting/"
    public static string settingPath
        => string.Concat(PatchHost, GetAbPath(), "designdata/setting/");

    // Source: Ghidra .../get_scriptablePath.c  RVA: 0x1589284, literal #19662="scriptable/"
    public static string scriptablePath
        => string.Concat(PatchHost, GetAbPath(), "scriptable/");

    // Source: Ghidra .../get_guiPath.c  RVA: 0x1588F90, literal #17013="gui/"
    public static string guiPath
        => string.Concat(PatchHost, GetAbPath(), "gui/");

    // ─── OutPath_Ab_* family (Concat OutputPath + GetAbOutPath + asset path) ────

    // Source: Ghidra .../get_OutPath_Ab_avatar.c  RVA: 0x1589300, lit #15199="avatar/"
    public static string OutPath_Ab_avatar
        => string.Concat(OutputPath, GetAbOutPath(), "avatar/");

    // Source: Ghidra .../get_OutPath_Ab_scene.c  RVA: 0x158937C, lit #19636="scene/"
    public static string OutPath_Ab_scene
        => string.Concat(OutputPath, GetAbOutPath(), "scene/");

    // Source: Ghidra .../get_OutPath_Ab_gui.c  RVA: 0x15893F8, lit #17013="gui/"
    public static string OutPath_Ab_gui
        => string.Concat(OutputPath, GetAbOutPath(), "gui/");

    // Source: Ghidra .../get_OutPath_Ab_mpc.c  RVA: 0x1589474, lit #18737="mpc/"
    public static string OutPath_Ab_mpc
        => string.Concat(OutputPath, GetAbOutPath(), "mpc/");

    // Source: Ghidra .../get_OutPath_Ab_effect.c  RVA: 0x15894F0, lit #16418="effect/"
    public static string OutPath_Ab_effect
        => string.Concat(OutputPath, GetAbOutPath(), "effect/");

    // Source: Ghidra .../get_OutPath_Ab_audio.c  RVA: 0x158956C, lit #15176="audio/"
    public static string OutPath_Ab_audio
        => string.Concat(OutputPath, GetAbOutPath(), "audio/");

    // Source: Ghidra .../get_OutPath_Ab_setting.c  RVA: 0x15895E8, lit #16087="designdata/setting/"
    public static string OutPath_Ab_setting
        => string.Concat(OutputPath, GetAbOutPath(), "designdata/setting/");

    // Source: Ghidra .../get_OutPath_Ab.c  RVA: 0x1589664
    // TODO: 26-line body — different from siblings (no trailing literal). Probably Concat(OutputPath, GetAbOutPath()).
    public static string OutPath_Ab
        => string.Concat(OutputPath, GetAbOutPath());

    // ─── Platform / path build helpers (complex) ───────────────────────────

    // Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/GetAbOutPath.c RVA 0x15888B0
    // Ghidra: switch on UjApplicationSetting.abBuildMode (uVar2): values < 5 index into a literal
    // table at PTR_StringLiteral_12568_032ac4a8; fallback prints "GetAbOutPath NG!" via Debug.LogError.
    // Five slots correspond to abBuildMode values 0..4. Literal table values per
    // Plugins/Assembly-CSharp-firstpass/ResourcesPath.cs reference: Web/, WebGL/, Android/, iOS/, Steam/.
    // confidence:medium — literal text inferred from sibling firstpass port; jump table layout 1-1.
    public static string GetAbOutPath()
    {
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

    // Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/GetAbPath.c RVA 0x1588A14
    // Switch on UnityEngine.Application.platform:
    //   p < 8:  p<2 (OSXEditor/OSXPlayer) → iOS/, p==2 (WindowsPlayer) → Steam/, p==7 (IPhonePlayer) → iOS/  [Note: Android case 7]
    //   p >= 8: p==8 (Android) → Android/, p==0xb (WindowsEditor) → Steam/, p==0x11 (LinuxEditor) → WebGL/
    // Default: Debug.LogError("GetAbPath NG!") → "".
    // confidence:medium — RuntimePlatform→literal mapping ported from firstpass sibling.
    public static string GetAbPath()
    {
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

    // Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/GetPlatformName.c RVA 0x1588B58
    // Same switch pattern as GetAbPath but emits names without trailing slash.
    public static string GetPlatformName()
    {
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

    // ─── Constructors ──────────────────────────────────────────────────────

    // Source: Ghidra .../.ctor.c  RVA: 0x15896C8 — default empty ctor.
    public ResourcesPath() { }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResourcesPath/.cctor.c  RVA: 0x15896D0
    // 1-1: all string statics (offsets 0x00, 0x08, 0x10, 0x18, 0x20, 0x40) = ""
    //      (PTR_StringLiteral_0 = empty string literal).
    //      _patchData / _patchDataPreview / _patchDataOld = new PatchHostData()
    //      (3 allocations of PatchHostData with .ctor() called each).
    // Without all-strings = "" init: CurVersion is null → IsPreviewVersion() compares
    //   null == "" (VersionAndroid set by PatchHostData..ctor) → behavior matches Ghidra
    //   but documenting the 1-1 explicitly.
    static ResourcesPath()
    {
        CDNVersion = "";
        bdCreateTime = "";
        bundleTNum = "";
        _patchHost = "";
        _outputPath = "";
        _patchData = new PatchHostData();
        _patchDataPreview = new PatchHostData();
        _patchDataOld = new PatchHostData();
        CurVersion = "";
    }
}
