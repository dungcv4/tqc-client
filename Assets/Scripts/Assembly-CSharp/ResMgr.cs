// Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/ (23 .c, + .ctor inferred)
// Source: dump.cs TypeDefIndex 68
// 24 methods: 13 fully ported 1-1, 10 stub-with-RVA per CLAUDE.md §D6 (complex bodies).

using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class ResMgr
{
    public string sVersionTag;                                                   // 0x10
    public List<ServerListData> serverListData;                                  // 0x18
    private Dictionary<string, Dictionary<string, string>> _sgcInitSettingsDict; // 0x20
    [NoToLua] public AssetBundleOP LuaBundleOP;                                  // 0x28
    [NoToLua] public AssetBundleOP FontBundleOP;                                 // 0x30
    [NoToLua] public AssetBundleOP MainUIBundleOP;                               // 0x38
    [NoToLua] public AssetBundleOP ItemIconBundleOP;                             // 0x40
    [NoToLua] public AssetBundleOP HeadIconBundleOP;                             // 0x48
    [NoToLua] public AssetBundleOP SkillIconBundleOP;                            // 0x50
    [NoToLua] public AssetBundleOP ModelBundleOP;                                // 0x58
    [NoToLua] public AssetBundleOP FXBundleOP;                                   // 0x60
    [NoToLua] public AssetBundleOP SMapBundleOP;                                 // 0x68
    [NoToLua] public AssetBundleOP MapDataBundleOP;                              // 0x70
    [NoToLua] public AssetBundleOP MusicBundleOP;                                // 0x78
    [NoToLua] public AssetBundleOP SoundBundleOP;                                // 0x80
    [NoToLua] public AssetBundleOP UIParticleOP;                                 // 0x88
    [NoToLua] public AssetBundleOP MagicDataBundleOP;                            // 0x90
    [NoToLua] public AssetBundleOP MagicFxBundleOP;                              // 0x98
    [NoToLua] public AssetBundleOP CardIconBundleOP;                             // 0xA0
    [NoToLua] public AssetBundleOP EmojiBundleOP;                                // 0xA8
    private LuaFileListHolder _LuaFileList;                                      // 0xB0
    private Dictionary<string, LuaFileData> _LuaFileLists;                       // 0xB8
    [NoToLua] public bool bFileListReady;                                        // 0xC0
    private Dictionary<string, IUJObjectOperation> _abSceneResults;              // 0xC8
    private bool bLoadBaseUIText;                                                // 0xD0
    private Dictionary<int, string> _dBaseUIString;                              // 0xD8

    // Source: dump.cs TypeDefIndex 68 declares `public static ResMgr Instance { get; }`
    // — read-only auto-property whose backing field is set via the implicit static
    // constructor `new ResMgr()`. The IL2CPP .cctor.c was not exported by Ghidra (auto-
    // property init compiled into .cctor IL; Ghidra dropped the trivial body). Ghidra
    // get_Instance.c shows `thunk_FUN_015e4ba4()` invoking cctor on first access then
    // returning the singleton; with our explicit cctor below we replicate the same
    // observable behavior — get_Instance returns the eagerly-allocated singleton.
    private static ResMgr s_instance;

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/get_Instance.c
    // RVA: 0x15C6E94 — Body literally returns static field [type+0xb8] after type init.
    public static ResMgr Instance => s_instance;

    // Implicit static initializer for the read-only auto-property dump.cs declares —
    // re-injecting `new ResMgr()` here is the standard IL2CPP fingerprint for
    // `static T Prop { get; } = new T();`. ResMgr is exposed to Lua via ResMgrWrap
    // but the get_Instance call site at e.g. ProcessLunchGame.ParsingSGCInitSettings
    // (Ghidra RVA 0x019033d8) returns non-null on first use → cctor must publish a
    // singleton.
    static ResMgr()
    {
        s_instance = new ResMgr();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/.ctor.c — RVA: 0x15CA1BC
    // 1-1: sVersionTag = ""; serverListData = new List(); _sgcInitSettingsDict /
    //   _LuaFileLists / _abSceneResults / _dBaseUIString = new Dictionary(); then base().
    public ResMgr()
    {
        sVersionTag = "";
        serverListData = new List<ServerListData>();
        _sgcInitSettingsDict = new Dictionary<string, Dictionary<string, string>>();
        _LuaFileLists = new Dictionary<string, LuaFileData>();
        _abSceneResults = new Dictionary<string, IUJObjectOperation>();
        _dBaseUIString = new Dictionary<int, string>();
    }

    // ─── 1-1 PORTED METHODS ────────────────────────────────────────────────

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/IsUIFXReady.c
    // RVA: 0x15C8164
    // One-liner: return UIParticleOP (offset 0x88) != null. Param sAssetName unused.
    public bool IsUIFXReady(string sAssetName)
    {
        return UIParticleOP != null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/LoadBaseUIText.c
    // RVA: 0x15C8C64
    // Guard !bLoadBaseUIText, then clear _dBaseUIString, LoadBaseUITextFromResource(), set flag.
    public void LoadBaseUIText()
    {
        if (!bLoadBaseUIText)
        {
            if (_dBaseUIString == null) throw new System.NullReferenceException();
            _dBaseUIString.Clear();
            LoadBaseUITextFromResource();
            bLoadBaseUIText = true;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/LoadBaseUITextAgain.c
    // RVA: 0x15C9368
    // Force-clear _dBaseUIString and reload — used when language changes.
    public void LoadBaseUITextAgain()
    {
        if (_dBaseUIString == null) throw new System.NullReferenceException();
        _dBaseUIString.Clear();
        LoadBaseUITextFromResource();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetBasicUIText.c
    // RVA: 0x15C94A4
    // Dict[id] then Replace("\\n", "\n") (string literals #13164 and #42).
    // Returns "" (literal #0) if key missing.
    public string GetBasicUIText(int id)
    {
        if (_dBaseUIString == null) throw new System.NullReferenceException();
        if (!_dBaseUIString.ContainsKey(id))
        {
            return "";
        }
        string raw = _dBaseUIString[id];
        if (raw == null) throw new System.NullReferenceException();
        return raw.Replace("\\n", "\n");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetUIParticle.c
    // RVA: 0x15C8174
    // Ghidra calls UIParticleOP._wwwRef.Load(name, typeof(GameObject)) directly.
    // Equivalent via public AssetBundleOP.Load(name, Type) RVA 0x15C72F0 (delegates to _wwwRef).
    // Returns null if loaded asset's type marker (PTR_DAT_03446a18) doesn't match GameObject.
    public GameObject GetUIParticle(string sAssetName)
    {
        if (UIParticleOP == null) throw new System.NullReferenceException();
        return UIParticleOP.Load(sAssetName, typeof(GameObject)) as GameObject;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetSGCInitSettings.c
    // RVA: 0x15CA0A4
    // Nested dict lookup _sgcInitSettingsDict[node][attr]; "" if either key missing.
    public string GetSGCInitSettings(string node, string attr)
    {
        if (_sgcInitSettingsDict == null) throw new System.NullReferenceException();
        if (!_sgcInitSettingsDict.ContainsKey(node)) return "";
        var inner = _sgcInitSettingsDict[node];
        if (inner == null) throw new System.NullReferenceException();
        if (!inner.ContainsKey(attr)) return "";
        return inner[attr];
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetLuaBundleName.c
    // RVA: 0x15C7068
    // Original Ghidra logic: Is64BitProcess ? "unload64" : "unload"
    //   - string literals #20701 = "unload64", #20700 = "unload"
    //   - Body also gets CultureInfo + processor type / "ARM" literal but result discarded (dead).
    //
    // TEMPORARY override (2026-05-12, per user request): force 32-bit branch → "unload".
    // Rationale: Mac Editor maps to iOS platform per Ghidra GetPlatformName.c (OSXEditor=0 → "iOS").
    //   CDN iOS manifest (verified via UnityPy live download) contains 4226 entries, NO "unload64.uab".
    //   The 64-bit branch only matches Android production manifest (4227 entries, has unload64.uab).
    //   Without this override, boot stalls at WaitCloseCheck (Lua bundle lookup fails in mapBundles).
    // TODO: revert to Ghidra-strict `Environment.Is64BitProcess ? "unload64" : "unload"` once one of:
    //   (a) Windows Editor used (maps to Android platform → Android manifest has unload64.uab)
    //   (b) Android device build tested
    //   (c) GetPlatformName Editor-only Build-Target override (Option H in conversation log)
    public string GetLuaBundleName()
    {
        // Dead JNI call kept for behavior parity (result discarded in original):
        var _discard1 = System.Globalization.CultureInfo.InvariantCulture;
        var _discard2 = SystemInfo.processorType;
        // return System.Environment.Is64BitProcess ? "unload64" : "unload";  // ← Ghidra-strict
        return "unload";  // ← temp force 32-bit branch, see comment above
    }

    // ─── STUB-WITH-RVA (CLAUDE.md §D6 — complex bodies, port deferred) ─────

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/ClearAsset.c
    // RVA: 0x15C6EEC
    // Ghidra body literally nulls bundle-OP fields at offsets 0x28..0xB0 (excluding
    // 0x90 MagicDataBundleOP and 0x98 MagicFxBundleOP — these are NOT touched).
    // thunk_FUN_015ee8c4 is the GC write barrier (mono_gc_wbarrier_set_field), not
    // a managed call → no .Unload() invoked here. After nulling, Clear()s the two
    // Dictionary fields (_LuaFileLists @0xB8, _abSceneResults @0xC8) and sets
    // bFileListReady (@0xC0) = 0. Order of field nulls preserved from Ghidra.
    [NoToLua]
    public void ClearAsset()
    {
        LuaBundleOP = null;        // 0x28
        FontBundleOP = null;       // 0x30
        MainUIBundleOP = null;     // 0x38
        ItemIconBundleOP = null;   // 0x40
        HeadIconBundleOP = null;   // 0x48
        SkillIconBundleOP = null;  // 0x50
        ModelBundleOP = null;      // 0x58
        FXBundleOP = null;         // 0x60
        SMapBundleOP = null;       // 0x68
        MapDataBundleOP = null;    // 0x70
        MusicBundleOP = null;      // 0x78
        SoundBundleOP = null;      // 0x80
        UIParticleOP = null;       // 0x88
        // NOTE: MagicDataBundleOP (0x90) + MagicFxBundleOP (0x98) intentionally
        //   NOT nulled here — confirmed absent from Ghidra body.
        CardIconBundleOP = null;   // 0xA0
        EmojiBundleOP = null;      // 0xA8
        _LuaFileList = null;       // 0xB0
        if (_LuaFileLists == null) throw new System.NullReferenceException();
        _LuaFileLists.Clear();     // 0xB8
        bFileListReady = false;    // 0xC0
        if (_abSceneResults == null) throw new System.NullReferenceException();
        _abSceneResults.Clear();   // 0xC8
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/LoadLuaScript.c
    // RVA: 0x15C7158
    // Body 1-1:
    //   bFileListReady = false;
    //   if (LuaBundleOP == null) return false;
    //   typeHandle = typeof(LuaFileListHolder)
    //   _LuaFileList = LuaBundleOP._wwwRef.Load("filelist.asset", typeof(LuaFileListHolder)) as LuaFileListHolder;
    //   if (UnityEngine.Object.op_Equality(_LuaFileList, null)) return false;
    //   return _LuaFileList.files != null;
    // Note: AssetBundleOP._wwwRef is private; use public AssetBundleOP.Load(name, Type) at RVA 0x15C72F0
    // which delegates to _wwwRef.Load.
    [NoToLua]
    public bool LoadLuaScript()
    {
        bFileListReady = false;
        if (LuaBundleOP == null) return false;
        _LuaFileList = LuaBundleOP.Load("filelist.asset", typeof(LuaFileListHolder)) as LuaFileListHolder;
        if (_LuaFileList == null) return false;
        return _LuaFileList.files != null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/IsLoadLuaFinish.c
    // RVA: 0x15C7308
    // Body 1-1: enumerate _LuaFileLists. For each value: if !value.bLoad, log error
    //   "ResMgr::IsLoadLuaFinish File Not Require -> {0} " with value.sRealName. Else iVar6++.
    //   Sets *iCur = iVar6, *iMax = dict.Count. Returns iCur == iMax.
    [NoToLua]
    public bool IsLoadLuaFinish(out int iCur, out int iMax)
    {
        if (_LuaFileLists == null) throw new System.NullReferenceException();
        int count = 0;
        foreach (var kv in _LuaFileLists)
        {
            LuaFileData val = kv.Value;
            if (val == null) throw new System.NullReferenceException();
            if (!val.bLoad)
            {
                UJDebug.LogErrorFormat("ResMgr::IsLoadLuaFinish File Not Require -> {0} ", false, UJLogType.None, val.sRealName);
            }
            else
            {
                count++;
            }
        }
        iCur = count;
        iMax = _LuaFileLists.Count;
        return iCur == iMax;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/ShowUnloadLuaFile.c
    // RVA: 0x15C7598
    // Body 1-1:
    //   LogErrorFormat("_LuaFileLists count {0} ", dict.Count)
    //   foreach kv in dict: if !kv.Value.bLoad: LogErrorFormat("_LuaFileLists Lua File Not Require -> {0} ", kv.Value.sRealName)
    [NoToLua]
    public void ShowUnloadLuaFile()
    {
        if (_LuaFileLists == null) throw new System.NullReferenceException();
        UJDebug.LogErrorFormat("_LuaFileLists count {0} ", false, UJLogType.None, _LuaFileLists.Count);
        foreach (var kv in _LuaFileLists)
        {
            LuaFileData val = kv.Value;
            if (val == null) throw new System.NullReferenceException();
            if (!val.bLoad)
            {
                UJDebug.LogErrorFormat("_LuaFileLists Lua File Not Require -> {0} ", false, UJLogType.None, val.sRealName);
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/ProcessFileList.c
    // RVA: 0x15C78B0
    // Body 1-1:
    //   _LuaFileLists.Clear()
    //   if (_LuaFileList == null) NRE
    //   arr = _LuaFileList.files (struct[] of {data1, data2, data3})
    //   ok = true
    //   for i in 0..arr.Length:
    //     holder = arr[i]
    //     data2 = holder.data2 (byte[]); data3 = holder.data3; data1 = holder.data1 (hashname)
    //     if data2 == null break
    //     uVar3 = data2[0] % 10
    //     raw = data2[uVar3]
    //     uVar3 = raw % 10
    //     key = data2[uVar3]
    //     for j in 0..data3.Length: data3[j] ^= key
    //     realName = Encoding.Default.GetString(data3)
    //     if realName == "" : ok = false
    //     else:
    //       lfd = new LuaFileData { sRealName=realName, sHashName=data1, bLoad=false }
    //       if realName.StartsWith("ToLua.") (via UJString.CustomStartsWith) -> lfd.bLoad = true
    //       _LuaFileLists[realName] = lfd
    //   return ok
    [NoToLua]
    public bool ProcessFileList()
    {
        if (_LuaFileLists == null) throw new System.NullReferenceException();
        _LuaFileLists.Clear();
        if (_LuaFileList == null) throw new System.NullReferenceException();
        var arr = _LuaFileList.files;
        if (arr == null) return true;
        bool ok = true;
        for (int i = 0; i < arr.Length; i++)
        {
            var holder = arr[i];
            byte[] data2 = holder.data2;
            byte[] data3 = holder.data3;
            string data1 = holder.data1;
            if (data2 == null) break;
            if (data2.Length == 0) throw new System.IndexOutOfRangeException();
            int uVar3 = data2[0] % 10;
            if ((uint)uVar3 >= (uint)data2.Length) throw new System.IndexOutOfRangeException();
            byte raw = data2[uVar3];
            uVar3 = raw % 10;
            if ((uint)uVar3 >= (uint)data2.Length) throw new System.IndexOutOfRangeException();
            byte key = data2[uVar3];
            if (data3 == null) break;
            for (int j = 0; j < data3.Length; j++) data3[j] ^= key;
            string realName = System.Text.Encoding.Default.GetString(data3);
            if (realName != "")
            {
                var lfd = new LuaFileData();
                lfd.sRealName = realName;
                lfd.sHashName = data1;
                lfd.bLoad = false;
                if (UJString.CustomStartsWith(realName, "ToLua."))
                {
                    lfd.bLoad = true;
                }
                _LuaFileLists[realName] = lfd;
            }
            else
            {
                ok = false;
            }
        }
        return ok;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetLuaScript.c + ProcessFileList.c
    // RVA: 0x15C7B68
    //
    // ⚠️ Ghidra decompiled GetLuaScript as single-step `key = data1[data1[0]]` (ARM64 at
    // 0x15c7d00-0x15c7d18: ldrb w10,[x9,#0x20]; add x9,x9,x10; ldrb w9,[x9,#0x20]). However
    // empirical analysis of the production AssetBundle reveals this CANNOT be the actual
    // algorithm — extracted bundles have data1.Length=10 with data1[0]={31,76,214} all ≥10,
    // which would unconditionally OOB-abort.
    //
    // Brute-force XOR of data2 against all 256 keys on 3 sample files reveals correct keys
    // sit at data1[idx2] where idx2 is derived via two-step modulo — IDENTICAL to the
    // pattern that Ghidra correctly decompiled for ResMgr.ProcessFileList (RVA 0x15c78b0,
    // see ProcessFileList.c lines 51-57): `uVar3 = data[0] - (data[0]/10)*10` = data[0] % 10.
    //
    // Empirical validation (data1[0] → idx1=data1[0]%10 → raw=data1[idx1] → idx2=raw%10 → key=data1[idx2]):
    //   ToLua.tolua.lua:       data1=[1F E9 24 BE…] data1[0]=31 → 1 → 0xE9 → 3 → 0xBE ✓ (decrypts to "----…" Lua comment)
    //   Common.GameDef.lua:    data1=[D6 2B D1 2B 32…] data1[0]=214 → 4 → 0x32 → 0 → 0xD6 ✓ ("--#region FakeEnum")
    //   Common.SgzFunctions.lua: data1=[4C C5 34 2A BF 7F 8F…] data1[0]=76 → 6 → 0x8F → 3 → 0x2A ✓ ("Mathf.Mod = math.fmod")
    //
    // Conclusion: Ghidra/IDA missed the `% 10` divmod in GetLuaScript (likely from compiler inlining
    // a helper). Real algo mirrors ProcessFileList. See work/06_ghidra/decompiled_full/ResMgr/{GetLuaScript,ProcessFileList}.c.
    //
    // Body 1-1 (corrected):
    //   if (_LuaFileLists == null) NRE
    //   if (!_LuaFileLists.ContainsKey(sRealName)) return null
    //   lfd = _LuaFileLists[sRealName]; hashName = lfd.sHashName
    //   if (LuaBundleOP == null) NRE
    //   script = LuaBundleOP.Load(hashName, typeof(LuaScriptHolder)) as LuaScriptHolder
    //   if (script == null) return null
    //   data1 = script.data1; data2 = script.data2
    //   idx1 = data1[0] % 10; raw = data1[idx1]; idx2 = raw % 10; key = data1[idx2]
    //   for j in 0..data2.Length: data2[j] ^= key
    //   _LuaFileLists[sRealName].bLoad = true
    //   return data2
    [NoToLua]
    public byte[] GetLuaScript(string sRealName)
    {
        if (_LuaFileLists == null) throw new System.NullReferenceException();
        if (!_LuaFileLists.ContainsKey(sRealName)) return null;
        if (LuaBundleOP == null) throw new System.NullReferenceException();
        LuaFileData lfd = _LuaFileLists[sRealName];
        if (lfd == null) throw new System.NullReferenceException();
        LuaScriptHolder script = LuaBundleOP.Load(lfd.sHashName, typeof(LuaScriptHolder)) as LuaScriptHolder;
        if (script == null) return null;
        byte[] data1 = script.data1;
        byte[] data2 = script.data2;
        if (data1 == null || data1.Length == 0) throw new System.IndexOutOfRangeException();
        // Two-step modulo key derivation — see header comment.
        int idx1 = data1[0] % 10;
        if ((uint)idx1 >= (uint)data1.Length) throw new System.IndexOutOfRangeException();
        byte raw = data1[idx1];
        int idx2 = raw % 10;
        if ((uint)idx2 >= (uint)data1.Length) throw new System.IndexOutOfRangeException();
        byte key = data1[idx2];
        if (data2 == null) return null;
        // Editor-safety deviation from Ghidra: production XORs data2 in-place because each app
        // launch has fresh bundle memory and GetLuaScript is called exactly once per file (Lua
        // package.loaded cache). In Editor, AssetBundles persist across stop_game/play_game cycles,
        // so in-place XOR would re-encrypt data2 on the second call → "Partial byte sequence" error
        // from luaL_loadbuffer. Clone to a working copy so the bundle's data2 stays in its original
        // (encrypted) state across plays.
        byte[] result = new byte[data2.Length];
        for (int j = 0; j < data2.Length; j++)
        {
            result[j] = (byte)(data2[j] ^ key);
        }
        // Strip UTF-8 BOM (EF BB BF) if present. Production Lua source files in the bundle
        // are stored WITH BOM, but tolua_runtime's Lua 5.1.5 luaL_loadbuffer does NOT auto-strip
        // BOM (auto-strip was added in Lua 5.2 via skip_BOM). When BOM is present, lua_load
        // reports "unexpected symbol near '\\xef'" — the error string contains raw 0xEF byte
        // which is an INCOMPLETE UTF-8 sequence. Marshal.PtrToStringAnsi (used by lua_tostring
        // wrapper) throws "Partial byte sequence" on this. Production likely uses a custom
        // luaL_loadbuffer wrapper that strips BOM, or a different Lua build with BOM-skip
        // back-ported. Strip here as the cleanest single-point fix.
        if (result.Length >= 3 && result[0] == 0xEF && result[1] == 0xBB && result[2] == 0xBF)
        {
            byte[] stripped = new byte[result.Length - 3];
            System.Array.Copy(result, 3, stripped, 0, stripped.Length);
            result = stripped;
        }
        _LuaFileLists[sRealName].bLoad = true;
        return result;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/ShowLuaFileList.c
    // RVA: 0x15C7D90
    // Body 1-1:
    //   loaded = new List<string>(); unloaded = new List<string>();
    //   foreach kv in _LuaFileLists: if kv.Value.bLoad -> loaded.Add(kv.Value.sRealName) else unloaded.Add(kv.Value.sRealName)
    //   LogWarning("以下檔案已載入") + Log(string.Join("\n", loaded))
    //   LogWarning("以下檔案未載入") + Log(string.Join("\n", unloaded))
    [NoToLua]
    public void ShowLuaFileList()
    {
        if (_LuaFileLists == null) throw new System.NullReferenceException();
        var loaded = new List<string>();
        var unloaded = new List<string>();
        foreach (var kv in _LuaFileLists)
        {
            LuaFileData val = kv.Value;
            if (val == null) throw new System.NullReferenceException();
            string name = val.sRealName;
            if (val.bLoad) loaded.Add(name);
            else unloaded.Add(name);
        }
        UJDebug.LogWarning("以下檔案已載入");
        UJDebug.Log(string.Join("\n", loaded.ToArray()));
        UJDebug.LogWarning("以下檔案未載入");
        UJDebug.Log(string.Join("\n", unloaded.ToArray()));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/IsSceneReady.c
    // RVA: 0x15C823C
    // Body 1-1:
    //   if (s_instance.sVersionTag == null) return true;  // version-tagged mode off, scenes preloaded
    //   if (_abSceneResults == null) NRE;
    //   if (!_abSceneResults.TryGetValue(sceneName, out var op) || op == null) return false;
    //   return op.isDone;
    public bool IsSceneReady(string sceneName)
    {
        if (s_instance == null || s_instance.sVersionTag == null) return true;
        if (_abSceneResults == null) throw new System.NullReferenceException();
        IUJObjectOperation op;
        if (!_abSceneResults.TryGetValue(sceneName, out op) || op == null) return false;
        return op.isDone;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/LoadScene.c
    // RVA: 0x15C8368
    // Body 1-1:
    //   if (s_instance.sVersionTag == null) return 1.0f;     // version-tagged mode off, scene preloaded
    //   if (_abSceneResults == null) NRE;
    //   if (!_abSceneResults.TryGetValue(sceneName, out op)) {
    //     op = ResourcesLoader.GetObjectTypeAssetDynamic(SCENE=16, sceneName, null);
    //     _abSceneResults[sceneName] = op;
    //   }
    //   if (op != null && op.isDone) {
    //     if (string.IsNullOrEmpty(op.error)) return 1.0f;
    //   }
    //   return Math.Min(1.0f, op.progress);
    public float LoadScene(string sceneName)
    {
        if (s_instance == null || s_instance.sVersionTag == null) return 1.0f;
        if (_abSceneResults == null) throw new System.NullReferenceException();
        IUJObjectOperation op;
        if (!_abSceneResults.TryGetValue(sceneName, out op))
        {
            op = ResourcesLoader.GetObjectTypeAssetDynamic(ResourcesLoader.AssetType.SCENE, sceneName, null);
            _abSceneResults[sceneName] = op;
        }
        if (op != null && op.isDone)
        {
            if (string.IsNullOrEmpty(op.error)) return 1.0f;
        }
        if (op == null) throw new System.NullReferenceException();
        return System.Math.Min(1.0f, op.progress);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/UnloadScene.c
    // RVA: 0x15C8B20
    // Body 1-1:
    //   if (s_instance.sVersionTag == null) return;  // version-tagged mode off
    //   if (_abSceneResults == null) NRE;
    //   if (_abSceneResults.TryGetValue(sceneName, out op) && op != null) op.ImmDestroy();
    //   _abSceneResults.Remove(sceneName);
    public void UnloadScene(string sceneName)
    {
        if (s_instance == null || s_instance.sVersionTag == null) return;
        if (_abSceneResults == null) throw new System.NullReferenceException();
        IUJObjectOperation op;
        if (_abSceneResults.TryGetValue(sceneName, out op))
        {
            if (op != null) op.ImmDestroy();
            _abSceneResults.Remove(sceneName);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/LoadBaseUITextFromResource.c
    // RVA: 0x15C8CD0
    // Body 1-1:
    //   text = Resources.Load("NotInBundle/loading/basicstring", typeof(TextAsset)) as TextAsset
    //   xml = new XmlDocument()
    //   if text != null:
    //     reader = new StringReader(text.text); xml.Load(reader)
    //     nodes = xml.SelectNodes("//string")
    //     foreach node in nodes (via enumerator MoveNext()):
    //       attribs = node.Attributes
    //       idAttr = attribs["id"]
    //       if idAttr != null and !IsNullOrEmpty(idAttr.Value) and int.TryParse(idAttr.Value, out _):
    //         id = int.Parse(idAttr.Value)
    //         keyName = "text_" + getLanVersionStr()
    //         valAttr = attribs[keyName]
    //         valueStr = "" (StringLiteral_0)
    //         if valAttr != null and valAttr.Value != "0":
    //           valueStr = valAttr.Value.Trim()
    //         if !_dBaseUIString.ContainsKey(id): _dBaseUIString[id] = valueStr
    private void LoadBaseUITextFromResource()
    {
        UnityEngine.TextAsset text = UnityEngine.Resources.Load("NotInBundle/loading/basicstring", typeof(UnityEngine.TextAsset)) as UnityEngine.TextAsset;
        System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
        if (text == null) return;
        System.IO.StringReader reader = new System.IO.StringReader(text.text);
        xml.Load(reader);
        System.Xml.XmlNodeList nodes = xml.SelectNodes("//string");
        if (nodes == null) return;
        if (_dBaseUIString == null) throw new System.NullReferenceException();
        string keyName = "text_" + getLanVersionStr();
        foreach (System.Xml.XmlNode node in nodes)
        {
            if (!(node is System.Xml.XmlElement)) continue;
            var attribs = node.Attributes;
            if (attribs == null) continue;
            var idAttr = attribs["id"];
            if (idAttr == null) continue;
            if (string.IsNullOrEmpty(idAttr.Value)) continue;
            int idVal;
            if (!int.TryParse(idAttr.Value, out idVal)) continue;
            int id = int.Parse(idAttr.Value);
            var valAttr = attribs[keyName];
            string valueStr = "";
            if (valAttr != null)
            {
                if (valAttr.Value != "0")
                {
                    if (valAttr.Value == null) throw new System.NullReferenceException();
                    valueStr = valAttr.Value.Trim();
                }
            }
            if (!_dBaseUIString.ContainsKey(id))
            {
                _dBaseUIString[id] = valueStr;
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetBeginerTipLength.c
    // RVA: 0x15C9588
    // Iterates Dictionary<int,string> _dBaseUIString (offset 0xD8). For each (key,value)
    // pair where (uint)(key - 0x1F5) < 0x96 (i.e. key in [501..651)) and value != ""
    // (StringLiteral_0 = empty string), increments counter. Throws NRE if dict null.
    [NoToLua]
    public int GetBeginerTipLength()
    {
        if (_dBaseUIString == null) throw new System.NullReferenceException();
        int count = 0;
        foreach (var kv in _dBaseUIString)
        {
            if ((uint)(kv.Key - 0x1F5) < 0x96)
            {
                if (kv.Value != "") count++;
            }
        }
        return count;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/GetAdvanceTipLength.c
    // RVA: 0x15C9718
    // Same pattern as GetBeginerTipLength but key range is [0x28B..0x3E9) i.e.
    // (uint)(key - 0x28B) < 0x15E. Value must be != "" (StringLiteral_0).
    [NoToLua]
    public int GetAdvanceTipLength()
    {
        if (_dBaseUIString == null) throw new System.NullReferenceException();
        int count = 0;
        foreach (var kv in _dBaseUIString)
        {
            if ((uint)(kv.Key - 0x28B) < 0x15E)
            {
                if (kv.Value != "") count++;
            }
        }
        return count;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/getLanVersionStr.c
    // RVA: 0x15C93C0
    // 1. cm = ConfigMgr.Instance; if null → FUN_015cb8fc (NRE)
    // 2. iVar2 = cm.GetConfigVarLanguage()
    // 3. arr = SGCLanguage.GetLanguageString (static field at type offset 0xb8)
    // 4. if iVar2 == 0: iVar2 = SGCLanguage.ver_defaultUseLanguage
    // 5. if arr null → NRE
    // 6. bounds: (iVar2 - 1U) < arr.Length else FUN_015cb904 (IndexOutOfRange)
    // 7. return arr[iVar2 - 1]   (Ghidra: lVar3 + (iVar2-1)*8 + 0x20)
    [NoToLua]
    private string getLanVersionStr()
    {
        ConfigMgr cm = ConfigMgr.Instance;
        if (cm == null) throw new System.NullReferenceException();
        int lang = cm.GetConfigVarLanguage();
        string[] arr = SGCLanguage.GetLanguageString;
        if (lang == 0)
        {
            lang = SGCLanguage.ver_defaultUseLanguage;
        }
        if (arr == null) throw new System.NullReferenceException();
        if ((uint)(lang - 1) >= (uint)arr.Length)
        {
            throw new System.IndexOutOfRangeException();
        }
        return arr[lang - 1];
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ResMgr/ParsingSGCInitSettings.c
    // RVA: 0x15C98A8
    // Body 1-1 from Ghidra:
    //   1. Create XmlDocument, LoadXml from StringReader(text)
    //   2. SelectSingleNode("root") (literal #19565) — if null: UJDebug.LogError(literal #9718), return false
    //   3. Clear _sgcInitSettingsDict
    //   4. For each XmlElement child of root (outer cast type = XmlElement):
    //      - Build inner Dictionary<string,string>
    //      - Iterate outerElem.Attributes (vtable[0x238] returns XmlAttributeCollection)
    //        then GetEnumerator (vtable[0x1c8]); each item cast to XmlAttribute (PTR_DAT_03448e30):
    //        innerDict[attr.Name] = attr.Value   (vtable[0x1b8] = get_Name, vtable[0x1c8] = get_Value)
    //      - _sgcInitSettingsDict[outerNode.Name] = innerDict
    //   5. Return (rootNode != null) — always true if reached end of parse loop
    //
    // Earlier (incorrect) port iterated outerNode.ChildNodes — CDN response
    //   `<mars serviceUrl="..."/>` is attribute-only, so child iteration yielded empty
    //   innerDict and GetSGCInitSettings("mars","serviceUrl") returned "" downstream.
    [NoToLua]
    public bool ParsingSGCInitSettings(string text)
    {
        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
        System.IO.StringReader reader = new System.IO.StringReader(text);
        xmlDoc.Load(reader);

        System.Xml.XmlNode rootNode = xmlDoc.SelectSingleNode("root");
        if (rootNode == null)
        {
            UJDebug.LogError("SGCInitSettings root node not found");
            return false;
        }

        if (_sgcInitSettingsDict == null) throw new System.NullReferenceException();
        _sgcInitSettingsDict.Clear();

        // Iterate first-level children (each is one settings group, e.g. <mars .../>)
        foreach (System.Xml.XmlNode outerNode in rootNode.ChildNodes)
        {
            if (!(outerNode is System.Xml.XmlElement outerElem)) continue;

            var innerDict = new Dictionary<string, string>();
            // 1-1 with Ghidra: iterate Attributes of the outer element, each attribute's
            //   Name → Value goes into innerDict.
            if (outerElem.Attributes != null)
            {
                foreach (System.Xml.XmlAttribute attr in outerElem.Attributes)
                {
                    innerDict[attr.Name] = attr.Value;
                }
            }
            _sgcInitSettingsDict[outerNode.Name] = innerDict;
        }

        return true;
    }
}
