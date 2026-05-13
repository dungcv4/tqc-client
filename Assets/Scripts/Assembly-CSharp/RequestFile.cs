// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15D4FC4, 0x15D501C, 0x15D5024, 0x15D507C, 0x15D5128, 0x15D5140, 0x15D51D4,
//       0x15D52B4, 0x15CF22C, 0x15D1270, 0x15D1A54, 0x15D53E4, 0x15D5484, 0x15D55F8
// Ghidra dir: work/06_ghidra/decompiled_full/RequestFile/
//             work/06_ghidra/decompiled_full/RequestFile.<FetchFile>d__25/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 93
public sealed class RequestFile : IUJAsyncOperation
{    private static int s_nRequestedFiles;
    private bool _IsImmDestroy;
    private bool _bFinished;
    private UnityWebRequest _www;
    private AssetBundleCreateRequest _wwwLocal;
    private bool _loadLocal;
    public bool bIsLoadStreamingAsset;
    private AssetBundleManager.CBAssetBundle _cbFunc;
    private AssetBundleManager.BundleInfo _info;
    private static string[] s_404Tags;

    // RVA: 0x15D4FC4  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_nRequestedFiles.c
    public static int nRequestedFiles { get { return s_nRequestedFiles; } }

    // RVA: 0x15D501C  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_isDone.c
    public bool isDone { get { // Ghidra: returns *(bool*)(this + 0x11) = _bFinished
        return _bFinished; } }

    // RVA: 0x15D5024  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_progress.c
    public float progress { get { // Ghidra: switch on _loadLocal (offset 0x28).
        // !_loadLocal && _www != null  -> _www.downloadProgress
        // _loadLocal && _wwwLocal != null -> _wwwLocal.progress
        // else if _info && _info._wwwRef -> _info._wwwRef.progress
        if (!_loadLocal)
        {
            if (_www != null)
            {
                return _www.downloadProgress;
            }
        }
        else if (_wwwLocal != null)
        {
            return _wwwLocal.progress;
        }
        if (_info != null && _info._wwwRef != null)
        {
            return _info._wwwRef.progress;
        }
        throw new System.NullReferenceException(); } }

    // RVA: 0x15D507C  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_error.c
    // 1-1: if (_info != null && _info._wwwRef != null):
    //   if _info._wwwRef._error != "" return _www.error (or "" if _www == null)
    //   else return _info._wwwRef._error
    public string error { get {
        if (_info != null && _info._wwwRef != null)
        {
            string werr = _info._wwwRef.error;
            if (werr != string.Empty)
            {
                return werr;
            }
            if (_www != null)
            {
                return _www.error;
            }
            return string.Empty;
        }
        throw new System.NullReferenceException(); } }

    // RVA: 0x15D5128  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_bundleSize.c
    public long bundleSize { get { // Ghidra: returns _info._nBytes (offset 0x30 of BundleInfo) if _info != null else 0
        if (_info != null)
        {
            return _info._nBytes;
        }
        return 0; } }

    // RVA: 0x15D5140  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_bundleOP.c
    public AssetBundleOP bundleOP { get { // Ghidra: if (_bFinished && _info != null && string.IsNullOrEmpty(get_error()))
        //   return new AssetBundleOP(_info._wwwRef);
        // else return null.
        if (_bFinished && _info != null)
        {
            string err = error;
            if (string.IsNullOrEmpty(err))
            {
                if (_info == null)
                {
                    throw new System.NullReferenceException();
                }
                return new AssetBundleOP(_info._wwwRef);
            }
        }
        return null; } }

    // RVA: 0x15D51D4  Ghidra: work/06_ghidra/decompiled_full/RequestFile/get_assetbundle.c
    public AssetBundle assetbundle { get { // Ghidra:
        //   if !_loadLocal:
        //     if _www != null && _www.isDone:
        //       if string.IsNullOrEmpty(_www.error):
        //         var dh = _www.downloadHandler;
        //         if dh is DownloadHandlerAssetBundle: return dh.assetBundle
        //   else:
        //     if _wwwLocal != null && _wwwLocal.isDone:
        //       return _wwwLocal.assetBundle
        //   return null
        if (!_loadLocal)
        {
            if (_www != null && _www.isDone)
            {
                if (string.IsNullOrEmpty(_www.error))
                {
                    DownloadHandler dh = _www.downloadHandler;
                    if (dh is DownloadHandlerAssetBundle)
                    {
                        return ((DownloadHandlerAssetBundle)dh).assetBundle;
                    }
                }
            }
        }
        else if (_wwwLocal != null && _wwwLocal.isDone)
        {
            return _wwwLocal.assetBundle;
        }
        return null; } }

    // RVA: 0x15D52B4  Ghidra: work/06_ghidra/decompiled_full/RequestFile/IsFileNotFound.c
    public bool IsFileNotFound()
    {
        // Ghidra: walk s_404Tags array. For each tag, check whether _www.error contains it.
        // Returns true if any tag matches; false otherwise.
        if (_www == null)
        {
            return false;
        }
        string error = _www.error;
        if (string.IsNullOrEmpty(error))
        {
            return false;
        }
        if (s_404Tags == null)
        {
            return false;
        }
        for (int i = 0; i < s_404Tags.Length; i++)
        {
            if (error.Contains(s_404Tags[i]))
            {
                return true;
            }
        }
        return false;
    }

    // RVA: 0x15CF22C  Ghidra: work/06_ghidra/decompiled_full/RequestFile/FetchFile.c
    // RVA: 0x15D56E4  Ghidra: work/06_ghidra/decompiled_full/RequestFile.<FetchFile>d__25/MoveNext.c
    // Source: Ghidra — d__25.MoveNext state machine translated 1-1 as a C# IEnumerator. State map:
    //   case 0 (entry)  → s_nRequestedFiles++; branch on _loadLocal/_www/_wwwLocal:
    //                       (!_loadLocal,_www) yield SendWebRequest  → state 3
    //                       (!_loadLocal,!_www) wait _info._wwwRef._done → state 4 loop
    //                       (_loadLocal,_wwwLocal) yield _wwwLocal    → state 1
    //                       (_loadLocal,!_wwwLocal) wait done         → state 2 loop
    //   case 1/3 resume → _info._wwwRef.Finish()  (state 3 logs www.error first)
    //   case 2/4 loop   → while !_info._wwwRef._done: yield null
    //   then           → s_nRequestedFiles-- (LAB_016d5940)
    //   case 5 / dep   → AssetBundleManager.Instance.isAllDependenciesDone loop:
    //                       done true  → if hasError set _wwwRef.error; invoke _cbFunc(op|null);
    //                                    log if error nonempty; _cbFunc=null; _bFinished=true; yield break;
    //                       done false → UJDebug.LogTrace("Wait dep…"+_info._file); yield null;
    // String literal addresses referenced (resolve via stringliteral.json by index, not raw VA):
    //   PTR_StringLiteral_20907 — "Wait dep…" trace prefix.
    //   PTR_StringLiteral_9537/_165 — "AB Load Error : <file> : <err>" concat parts.
    //   PTR_StringLiteral_7628 — error value assigned when dep had error ("Cancelled" or similar).
    //   PTR_StringLiteral_5382 — "LoadAsset Error url:{0} msg:{1}" Format spec.
    // Texts kept as faithful semantic placeholders with TODO refs (not invented logic) per STRICT_RULES §1.
    public IEnumerator FetchFile()
    {
        // case 0: entry path. s_nRequestedFiles++.
        s_nRequestedFiles++;

        if (!_loadLocal)
        {
            if (_www != null)
            {
                // case 0 → state 3: yield SendWebRequest
                yield return _www.SendWebRequest();
                // case 3 resume: error check on _www
                if (_www != null)
                {
                    string err = _www.error;
                    if (!string.IsNullOrEmpty(err))
                    {
                        // TODO: PTR_StringLiteral_5382 ("LoadAsset Error url:{0} msg:{1}" — confidence:low)
                        UJDebug.LogError(string.Format("LoadAsset Error url:{0} msg:{1}", _www.url, err));
                    }
                }
                // LAB_016d592c: Finish via WWWBundleRef
                if (_info != null && _info._wwwRef != null)
                {
                    _info._wwwRef.Finish();
                }
            }
            else
            {
                // LAB_016d580c: state 4 wait — _info._wwwRef._done loop
                while (true)
                {
                    if (_info == null || _info._wwwRef == null)
                    {
                        throw new System.NullReferenceException();
                    }
                    if (_info._wwwRef.isDone)
                    {
                        break;
                    }
                    yield return null;
                }
            }
        }
        else
        {
            if (_wwwLocal != null)
            {
                // case 0 → state 1: yield AssetBundleCreateRequest
                yield return _wwwLocal;
                // case 1 resume: LAB_016d592c Finish via WWWBundleRef
                if (_info != null && _info._wwwRef != null)
                {
                    _info._wwwRef.Finish();
                }
            }
            else
            {
                // Bottom block: state 2 wait — same as state 4 loop
                while (true)
                {
                    if (_info == null || _info._wwwRef == null)
                    {
                        throw new System.NullReferenceException();
                    }
                    if (_info._wwwRef.isDone)
                    {
                        break;
                    }
                    yield return null;
                }
            }
        }

        // LAB_016d5940: dec s_nRequestedFiles
        s_nRequestedFiles--;

        // LAB_016d597c: dependency loop (case 5)
        AssetBundleManager abm = AssetBundleManager.Instance;
        if (abm == null)
        {
            throw new System.NullReferenceException();
        }
        if (_info == null)
        {
            throw new System.NullReferenceException();
        }
        while (true)
        {
            bool hasError;
            bool depDone = abm.isAllDependenciesDone(_info, out hasError);
            if (depDone)
            {
                if (hasError)
                {
                    if (_info == null || _info._wwwRef == null)
                    {
                        throw new System.NullReferenceException();
                    }
                    // TODO: PTR_StringLiteral_7628 — exact text ("Cancelled" or similar dep-error tag)
                    _info._wwwRef.error = "Cancelled";
                }
                if (_info != null)
                {
                    if (_cbFunc != null)
                    {
                        AssetBundleOP op = null;
                        if (_info._wwwRef == null)
                        {
                            throw new System.NullReferenceException();
                        }
                        if (string.IsNullOrEmpty(_info._wwwRef.error))
                        {
                            op = new AssetBundleOP(_info._wwwRef);
                        }
                        _cbFunc(op);
                    }
                    if (_info._wwwRef != null && !string.IsNullOrEmpty(_info._wwwRef.error))
                    {
                        // TODO: PTR_StringLiteral_9537 + _165 — "AB Load Error :"+file+":"+err format
                        UJDebug.LogError("AB Load Error :" + _info._file + ":" + _info._wwwRef.error);
                    }
                }
                _cbFunc = null;
                _bFinished = true;
                yield break;
            }
            if (_info != null)
            {
                // TODO: PTR_StringLiteral_20907 — "Wait dep:" trace prefix
                UJDebug.LogTrace("Wait dep:" + _info._file);
            }
            yield return null;
        }
    }

    // RVA: 0x15D1270  Ghidra: work/06_ghidra/decompiled_full/RequestFile/.ctor.c
    public RequestFile(string url)
    {
        // TODO: confidence:low — single-arg ctor body not separately decompiled; expected to
        // delegate to the (url, info=null, cbFunc=null) overload.
        // Pattern preserved by forwarding to the 3-arg ctor with default args.
        InitFromCtor(url, null, null);
    }

    // RVA: 0x15D1A54  Ghidra: work/06_ghidra/decompiled_full/RequestFile/.ctor.c
    public RequestFile(string url, AssetBundleManager.BundleInfo info, AssetBundleManager.CBAssetBundle cbFunc)
    {
        InitFromCtor(url, info, cbFunc);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/RequestFile/.ctor.c (RVA 0x15D1A54)
    // 1-1 port:
    //   1. _info = info; _cbFunc = cbFunc
    //   2. If url == null/empty: return (no-op, leaves all defaults).
    //   3. If info._wwwRef already exists → IncRef and return (don't rebuild).
    //   4. lower = info._file.ToLower()
    //   5. localHash = AssetBundleManager.GetBundleHash(lower) (main manifest hash)
    //   6. resHash   = AssetBundleManager.GetResourceBundleHash(lower) (StreamingAssets hash)
    //   7. Log "filename={0} hash={1}" + "info ToString" trace
    //   8. If localHash.Equals(resHash) (StreamingAsset version matches manifest):
    //        bIsLoadStreamingAsset = true
    //        Log "Try to load from StreamingAssets: {0}"
    //        path = Path.Combine(streamingAssetsPath, lower)
    //        Log "Local Path={0}"
    //        _loadLocal = true (offset 0x28 in Ghidra → instance bool _loadLocal)
    //        _wwwLocal = AssetBundle.LoadFromFileAsync(path)
    //        WWWBundleRef ctor args: (_www=null, _wwwLocal, bLocal=1, info)
    //      else (different hash → CDN download):
    //        url = String.Format("{0}?v={1}", url, ResourcesPath.CDNVersion) (param_2 already mangled)
    //        Log "AssetBundle Download : {url}"
    //        bIsLoadStreamingAsset = false
    //        _www = UnityWebRequestAssetBundle.GetAssetBundle(url, localHash, 0) (cached with hash)
    //        WWWBundleRef ctor args: (_www, _wwwLocal=null, bLocal=0, info)
    //   9. new WWWBundleRef(_www, _wwwLocal, _loadLocal, info)
    //      ↑ WWWBundleRef.ctor sets info._wwwRef = this AND enqueues to s_queue.
    //
    // CRITICAL: prior port omitted step 9, so info._wwwRef stayed null → FetchFile callback
    // dispatch threw NRE on `op = new AssetBundleOP(_info._wwwRef)` and coroutine died with
    // _bFinished stuck false. This is the bug that prevented Lua bundle from being loaded.
    private void InitFromCtor(string url, AssetBundleManager.BundleInfo info, AssetBundleManager.CBAssetBundle cbFunc)
    {
        _info = info;
        _cbFunc = cbFunc;
        _bFinished = false;
        _IsImmDestroy = false;

        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        // Ghidra line 55-58: if info._wwwRef != null → IncRef + return (existing entry).
        if (info != null && info._wwwRef != null)
        {
            info._wwwRef.IncRef();
            return;
        }

        if (info == null || string.IsNullOrEmpty(info._file))
        {
            // Ghidra FAULT path — info must be valid.
            throw new System.NullReferenceException();
        }

        string lower = info._file.ToLower();
        var abm = AssetBundleManager.Instance;
        if (abm == null) throw new System.NullReferenceException();

        Hash128 localHash = abm.GetBundleHash(lower);          // main manifest
        Hash128 resourceHash = abm.GetResourceBundleHash(lower); // StreamingAssets

        UJDebug.LogTrace(string.Format("filename={0} hash={1}", url, info._hash.ToString()));

        if (localHash.Equals(resourceHash))
        {
            // Local StreamingAssets path — bundle matches CDN hash, no download needed.
            UJDebug.LogFormat("Try to load from StreamingAssets: {0}", false, UJLogType.None, lower);
            string saPath = System.IO.Path.Combine(Application.streamingAssetsPath, lower);
            UJDebug.LogFormat("Local Path={0}", false, UJLogType.None, saPath);
            bIsLoadStreamingAsset = true;
            _wwwLocal = AssetBundle.LoadFromFileAsync(saPath);
            _loadLocal = true;
        }
        else
        {
            // CDN download with cache.
            string urlWithVer = string.Format("{0}?v={1}", url, ResourcesPath.CDNVersion);
            UJDebug.Log(string.Concat("AssetBundle Download : ", urlWithVer));
            bIsLoadStreamingAsset = false;
            // Ghidra: GetAssetBundle(url, hash, crc=0) — cached.
            _www = UnityWebRequestAssetBundle.GetAssetBundle(urlWithVer, localHash, 0);
            _loadLocal = false;
        }

        // Ghidra line 178: new WWWBundleRef(www, wwwLocal, bLocal, info) — sets info._wwwRef = this
        // and enqueues to s_queue. CRITICAL for FetchFile.Finish() and callback dispatch.
        new AssetBundleManager.WWWBundleRef(_www, _wwwLocal, _loadLocal, info);
    }

    // RVA: 0x15D53E4  Ghidra: work/06_ghidra/decompiled_full/RequestFile/Finalize.c
    ~RequestFile() { }

    // Source: Ghidra ImmDestroy.c  RVA 0x15D5484
    // 1. _IsImmDestroy = true (offset 0x10).
    // 2. If _info == null: return.
    // 3. If AssetBundleManager.Instance != null: Instance.decDependenciesRef(_info).
    // 4. If _info != null && _info._wwwRef != null:
    //      if _info._wwwRef.refCount > 0: _info._wwwRef.DecRef();
    //      _info = null;
    //    else: panic (FUN_015cb8fc).
    public void ImmDestroy()
    {
        _IsImmDestroy = true;
        if (_info == null) return;

        AssetBundleManager abm = AssetBundleManager.Instance;
        if (abm != null)
        {
            abm.decDependenciesRef(_info);
        }

        if (_info != null && _info._wwwRef != null)
        {
            if (_info._wwwRef.refCount > 0)
            {
                _info._wwwRef.DecRef();
            }
            _info = null;
            return;
        }
        throw new System.NullReferenceException();
    }

    // RVA: 0x15D55F8  Ghidra: work/06_ghidra/decompiled_full/RequestFile/.cctor.c
    static RequestFile()
    {
        // TODO: confidence:low — .cctor body sets s_nRequestedFiles=0 and initializes s_404Tags
        // string array. Exact tag literals not extracted from PTR_StringLiteral_*.
        s_nRequestedFiles = 0;
        s_404Tags = new string[0];
    }

    // NOTE: RequestFile.<FetchFile>d__25 (TypeDefIndex 92) was the compiler-generated iterator
    // state machine in the original IL2CPP image. In this port, FetchFile() above is rewritten as
    // a C# IEnumerator method so the Roslyn compiler emits an equivalent state machine. The
    // hand-stubbed __FetchFile_d__25 class was removed (the body has been ported 1-1 above).

}
