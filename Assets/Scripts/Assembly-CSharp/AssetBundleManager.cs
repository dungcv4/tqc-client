// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x15CEE1C, 0x15CEE74, 0x15CEE7C, 0x15CEE84, 0x15CEE8C, 0x15CEEA4, 0x15CEEAC,
//       0x15CF0C4, 0x15CF3C0, 0x15CF424, 0x15CF428, 0x15CF4D4, 0x15CF5E8, 0x15CF640,
//       0x15CF6A0, 0x15CF6A8, 0x15CF708, 0x15CF79C, 0x15CF830, 0x15CF968, 0x15CF9C0,
//       0x15CFA20, 0x15CFA28, 0x15CFC68, 0x15CFE80, 0x15CFF14, 0x15CFF8C, 0x15D0020,
//       0x15D046C, 0x15D07D8, 0x15D0910, 0x15D0A94, 0x15D0CEC, 0x15D0CF4, 0x15D0CFC,
//       0x15D0D74, 0x15D0E08, 0x15D0EA0, 0x15D1058, 0x15D1164, 0x15CC4FC, 0x15D1394,
//       0x15D1F84, 0x15D20EC, 0x15D2230, 0x15D15E8, 0x15D237C, 0x15D24E8,
//       0x15CF298, 0x15CFB08, 0x15D26C8, 0x15D26D0, 0x15D2804, 0x15D280C, 0x15D2814,
//       0x15D281C, 0x15D2220, 0x15D2368, 0x15D286C, 0x15D2594, 0x15D2B60, 0x15D2BFC,
//       0x15D2CA0, 0x15D2D44, 0x15D2DCC, 0x15D2E54, 0x15D2EEC, 0x15D2F74,
//       0x15CFE78
// Ghidra dir: work/06_ghidra/decompiled_full/AssetBundleManager/
//             work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/
//             work/06_ghidra/decompiled_full/AssetBundleManager.BundleInfo/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEngine;
using Google.Play.AssetDelivery;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-stub  TypeDefIndex: 90
[AddComponentMenu("UJ RD1/AssetBundle Manager")]
public sealed class AssetBundleManager : MonoBehaviour
{
    private static AssetBundleManager s_instance;
    private bool IsChecking;
    private PlayAssetPackRequest _packRequest;
    public int _MaxDownLoadFiles;
    public string _url;
    private static Dictionary<string, string[]> m_Dependencies;
    private Dictionary<string, AssetBundleManager.BundleInfo> _mapBundles;
    private string _bundleFolder;
    private int _minBundleAppCode;
    private List<AssetBundleManager.WWWBundleRef> _dependencies;
    private Queue<RequestFile> _waitRequesies;
    private static AssetBundleManifest _resourceBundleManifest;
    private bool bResourcesReady;
    private static AssetBundleManifest _mainBundleManifest;
    private bool bReady;
    private AssetBundleManifest _mainBundleManifest_forCheck;

    // RVA: 0x15CEE1C  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_Instance.c
    public static AssetBundleManager Instance { get { // Ghidra: returns the cached instance held in a static class-info slot (lazy type init).
        return s_instance; } }

    // RVA: 0x15CEE74  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_packRequest.c
    public PlayAssetPackRequest packRequest { get { return _packRequest; } }

    // RVA: 0x15CEE7C  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_BundleFolder.c
    public string BundleFolder { get { return _bundleFolder; } }

    // RVA: 0x15CEE84  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_minBundleAppCode.c
    public int minBundleAppCode { get { return _minBundleAppCode; } }

    // RVA: 0x15CEE8C  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_invalidBundleVersion.c
    public bool invalidBundleVersion { get { // Ghidra: 0x25711d33 < _minBundleAppCode
        return 0x25711d33 < _minBundleAppCode; } }

    // RVA: 0x15CEEA4  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_mapBundles.c
    public Dictionary<string, AssetBundleManager.BundleInfo> mapBundles { get { return _mapBundles; } }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager/Start.c RVA 0x015ceeac
    // 1-1:
    //   DontDestroyOnLoad(gameObject);
    //   s_instance = this;
    //   if (string.IsNullOrEmpty(_url)) _url = <static default at PTR_DAT_034490f0>;
    //   if (!_url.EndsWith("/")) _url = _url + "/";
    //   _url = _url + "Android";
    //   UJDebug.Log(string.Format("Patch Server: {0}", _url));
    // Resolved literals (stringliteral.json):
    //   StringLit_986  = "/"
    //   StringLit_2992 = "Android"
    //   StringLit_9028 = "Patch Server: {0}"
    // PTR_DAT_034490f0 (default URL static) not extracted from .so RDATA — Inspector-serialized
    // _url provides the base CDN URL when present.
    private void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        s_instance = this;

        if (string.IsNullOrEmpty(_url))
        {
            // TODO: static default URL at PTR_DAT_034490f0 not extracted — replaced with placeholder.
            _url = "https://placeholder.cdn/";
        }

        if (!_url.EndsWith("/"))
        {
            _url = _url + "/";
        }
        _url = _url + "Android";
        UJDebug.Log(string.Format("Patch Server: {0}", _url));
    }

    // RVA: 0x15CF0C4  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/Update.c
    private void Update()
    {
        // Ghidra: spin pump:
        //   while (s_nRequestedFiles >= _MaxDownLoadFiles) { … return; }
        //   else dequeue from _waitRequesies and StartCoroutine(req.FetchFile())
        //   then call WWWBundleRef.Update() (static)
        // TODO: confidence:low — exact early-return condition and ProcessLunchGame check
        // (PTR_DAT_03449120 + 0x6) is opaque.
        if (RequestFile.nRequestedFiles >= _MaxDownLoadFiles)
        {
            AssetBundleManager.WWWBundleRef.Update();
            return;
        }
        if (_waitRequesies != null && _waitRequesies.Count > 0)
        {
            RequestFile req = _waitRequesies.Dequeue();
            if (req != null)
            {
                StartCoroutine(req.FetchFile());
            }
        }
        AssetBundleManager.WWWBundleRef.Update();
    }

    // RVA: 0x15CF3C0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/OnDestroy.c
    private void OnDestroy()
    {
        // Ghidra: s_instance = null
        s_instance = null;
    }

    // RVA: 0x15CF424  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/OnApplicationPause.c
    private void OnApplicationPause(bool pauseStatus)
    {
        // Ghidra: empty body
    }

    // RVA: 0x15CF428  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/Clear.c
    public void Clear()
    {
        // Ghidra: _waitRequesies.Clear(); _dependencies.Clear (manual count reset);
        //         _mapBundles.Clear()
        if (_waitRequesies != null)
        {
            _waitRequesies.Clear();
        }
        if (_dependencies != null)
        {
            _dependencies.Clear();
        }
        if (_mapBundles != null)
        {
            _mapBundles.Clear();
        }
    }

    // RVA: 0x15CF4D4  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/InitABHostPath.c
    public void InitABHostPath()
    {
        // Ghidra: _url = ResourcesPath.PatchHost;
        //   if (!UJString.CustomEndsWith(_url, "/")) _url = _url + "/";
        //   _url = _url + ResourcesPath.GetAbPath();
        //   _url = _url + ProcessLunchGame.newABPath;
        _url = ResourcesPath.PatchHost;
        if (!UJString.CustomEndsWith(_url, "/"))
        {
            _url = string.Concat(_url, "/");
        }
        _url = string.Concat(_url, ResourcesPath.GetAbPath());
        _url = string.Concat(_url, ProcessLunchGame.newABPath);
    }

    // RVA: 0x15CF5E8  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_ResourceBundleManifest.c
    public static AssetBundleManifest ResourceBundleManifest { get { return _resourceBundleManifest; } set { _resourceBundleManifest = value; } }

    // RVA: 0x15CF640  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/set_ResourceBundleManifest.c
    
    // RVA: 0x15CF6A0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/IsResourceReady.c
    public bool IsResourceReady()
    {
        return bResourcesReady;
    }

    // RVA: 0x15CF6A8  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/InitResourceMainManifest.c
    public void InitResourceMainManifest()
    {
        // Ghidra: bResourcesReady = false; StopCoroutine("LoadResourceManifest"); StartCoroutine("LoadResourceManifest");
        bResourcesReady = false;
        StopCoroutine("LoadResourceManifest");
        StartCoroutine("LoadResourceManifest");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.<LoadResourceManifest>d__38/MoveNext.c RVA 0x015d4440
    // State machine fields (from dump.cs lines 4096-4134):
    //   <www>5__2 = UnityWebRequest @ 0x28
    //   <_handler>5__3 = DownloadHandlerAssetBundle @ 0x30
    //   <testRequest>5__4 = AssetBundleRequest @ 0x38
    // String literals (stringliteral.json):
    //   #3127 = "AssetBundleManifest"
    //   #3125 = "AssetBundleManager::LoadResourceManifest Load Main Manifest FullPath: {0}"
    //   #3482 = "BundleData/"
    //   #2652 = "==== StreamingAssets Main Bundle Path Error ===="
    // Linear flow 1-1:
    //   state 0: build fullPath = Path.Combine(streamingAssetsPath, "BundleData/" + GetPlatformName());
    //            log "LoadResourceManifest Load Main Manifest FullPath: {0}"; create request via
    //            UnityWebRequestAssetBundle.GetAssetBundle; yield SendWebRequest -> state 1.
    //   state 1: if error: log "StreamingAssets Main Bundle Path Error", set bResourcesReady=true, exit.
    //            else: _handler = www.downloadHandler as DownloadHandlerAssetBundle; if _handler.assetBundle != null:
    //              _testRequest = _handler.assetBundle.LoadAssetAsync("AssetBundleManifest"); yield -> state 2.
    //   state 2: if !_testRequest.isDone -> cleanup and exit (no ready);
    //            else _resourceBundleManifest = _testRequest.asset as AssetBundleManifest;
    //            if _handler != null && _handler.assetBundle != null: _handler.assetBundle.Unload(false);
    //                                                                   bResourcesReady = true;
    //            cleanup _handler=null, _testRequest=null.
    private IEnumerator LoadResourceManifest()
    {
        UnityWebRequest www = null;
        DownloadHandlerAssetBundle _handler = null;
        AssetBundleRequest testRequest = null;

        // state 0
        string fullPath = Path.Combine(Application.streamingAssetsPath,
            string.Concat("BundleData/", ResourcesPath.GetPlatformName()));
        UJDebug.LogFormat("AssetBundleManager::LoadResourceManifest Load Main Manifest FullPath: {0}", false, UJLogType.None, fullPath);
        www = UnityWebRequestAssetBundle.GetAssetBundle(fullPath);
        if (www != null)
        {
            yield return www.SendWebRequest();

            // state 1
            if (!string.IsNullOrEmpty(www.error))
            {
                UJDebug.Log("==== StreamingAssets Main Bundle Path Error ====");
                bResourcesReady = true;
                yield break;
            }
            _handler = www.downloadHandler as DownloadHandlerAssetBundle;
            if (_handler != null && _handler.assetBundle != null)
            {
                testRequest = _handler.assetBundle.LoadAssetAsync("AssetBundleManifest");
            }
            if (testRequest != null)
            {
                yield return testRequest;

                // state 2
                _resourceBundleManifest = testRequest.asset as AssetBundleManifest;
                if (_handler != null && _handler.assetBundle != null)
                {
                    _handler.assetBundle.Unload(false);
                    bResourcesReady = true;
                }
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.<LoadResourceManifestFromFile>d__39/MoveNext.c RVA 0x015d495c
    // State machine fields (from dump.cs lines 4138-4174):
    //   <_www>5__2 = AssetBundleCreateRequest @ 0x28
    //   <testRequest>5__3 = AssetBundleRequest @ 0x30
    // String literals:
    //   #3127 = "AssetBundleManifest"
    //   #3126 = "AssetBundleManager::LoadResourceManifestFromFile Load Main Manifest FullPath: {0}"
    //   #3482 = "BundleData/"
    //   #2644 = "==== LoadResourceManifestFromFile StreamingAssets Main Bundle Path Error ===="
    // Linear flow 1-1:
    //   state 0: build fullPath = Path.Combine(streamingAssetsPath, "BundleData/" + GetPlatformName());
    //            log; _www = AssetBundle.LoadFromFileAsync(fullPath); yield _www -> state 1.
    //   state 1: if _www.assetBundle == null (Unity Object equality): log #2644, then set bResourcesReady=true and exit.
    //            else _testRequest = _www.assetBundle.LoadAssetAsync("AssetBundleManifest"); yield -> state 2.
    //   state 2: if !_testRequest.isDone -> cleanup and exit;
    //            else _resourceBundleManifest = _testRequest.asset as AssetBundleManifest;
    //            if _www != null && _www.assetBundle != null: _www.assetBundle.Unload(false);
    //                                                          bResourcesReady = true;
    //            cleanup.
    private IEnumerator LoadResourceManifestFromFile()
    {
        AssetBundleCreateRequest _www = null;
        AssetBundleRequest testRequest = null;

        // state 0
        string fullPath = Path.Combine(Application.streamingAssetsPath,
            string.Concat("BundleData/", ResourcesPath.GetPlatformName()));
        UJDebug.LogFormat("AssetBundleManager::LoadResourceManifestFromFile Load Main Manifest FullPath: {0}", false, UJLogType.None, fullPath);
        _www = AssetBundle.LoadFromFileAsync(fullPath);
        yield return _www;

        // state 1
        if (_www != null)
        {
            if (_www.assetBundle == null)
            {
                UJDebug.Log("==== LoadResourceManifestFromFile StreamingAssets Main Bundle Path Error ====");
                bResourcesReady = true;
                yield break;
            }
            testRequest = _www.assetBundle.LoadAssetAsync("AssetBundleManifest");
            if (testRequest != null)
            {
                yield return testRequest;

                // state 2
                _resourceBundleManifest = testRequest.asset as AssetBundleManifest;
                if (_www != null && _www.assetBundle != null)
                {
                    _www.assetBundle.Unload(false);
                    bResourcesReady = true;
                }
            }
        }
    }

    // RVA: 0x15CF830  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/GetResourceBundleHash.c
    public Hash128 GetResourceBundleHash(string bundleName)
    {
        // Ghidra: if _resourceBundleManifest != null: return _resourceBundleManifest.GetAssetBundleHash(bundleName);
        // else return default Hash128.
        if (_resourceBundleManifest != null)
        {
            return _resourceBundleManifest.GetAssetBundleHash(bundleName);
        }
        return default(Hash128);
    }

    // RVA: 0x15CF968  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_MainBundleManifest.c
    public static AssetBundleManifest MainBundleManifest { get { return _mainBundleManifest; } set { _mainBundleManifest = value; } }

    // RVA: 0x15CF9C0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/set_MainBundleManifest.c
    
    // RVA: 0x15CFA20  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/IsReady.c
    public bool IsReady()
    {
        return bReady;
    }

    // RVA: 0x15CFA28  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/InitMainManifest.c
    public void InitMainManifest()
    {
        // Ghidra: bReady = false;
        //   if (BundleHashChecker.Instance != null) { ResMgr.ClearAsset(); Clear();
        //     WWWBundleRef.Clear(); StopCoroutine("LoadMainManifest"); StartCoroutine("LoadMainManifest"); }
        bReady = false;
        if (BundleHashChecker.Instance == null)
        {
            throw new System.NullReferenceException();
        }
        ResMgr.Instance.ClearAsset();
        Clear();
        AssetBundleManager.WWWBundleRef.Clear();
        StopCoroutine("LoadMainManifest");
        StartCoroutine("LoadMainManifest");
    }

    // RVA: 0x15CFC68  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/ParseMainBundleManifest.c
    public bool ParseMainBundleManifest()
    {
        // Ghidra: returns !(_mainBundleManifest == null). When non-null, it walks all bundles
        // in _mainBundleManifest.GetAllAssetBundles(), constructs a BundleInfo for each
        // (storing _file=name, _hash=manifest.GetAssetBundleHash(name)), inserts into
        // _mapBundles[name.ToLower()]. Also calls Clear() and WWWBundleRef.Clear() up front.
        bool isNull = (_mainBundleManifest == null);
        if (!isNull)
        {
            Clear();
            AssetBundleManager.WWWBundleRef.Clear();
            string[] bundles = _mainBundleManifest.GetAllAssetBundles();
            if (bundles == null)
            {
                throw new System.NullReferenceException();
            }
            for (int i = 0; i < bundles.Length; i++)
            {
                string name = bundles[i];
                BundleInfo info = new BundleInfo();
                info._file = name;
                info._hash = _mainBundleManifest.GetAssetBundleHash(name);
                _mapBundles.Add(name.ToLower(), info);
            }
        }
        return !isNull;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.<LoadMainManifest>d__49/MoveNext.c RVA 0x015d3de4
    // State machine fields (from dump.cs lines 4054-4091):
    //   <www>5__2 = UnityWebRequest @ 0x28
    //   <_handler>5__3 = DownloadHandlerAssetBundle @ 0x30
    //   <testRequest>5__4 = AssetBundleRequest @ 0x38
    // String literals:
    //   #3127  = "AssetBundleManifest"
    //   #7675  = "LoadMainManifest : "
    //   #7676  = "LoadMainManifest : {0}"
    //   #21255 = "{0}?v={1}"
    //   #2650  = "==== Main Bundle Path Error ===="
    // Linear flow 1-1:
    //   state 0: url = Concat("LoadMainManifest : ", _this._url, GetPlatformName()); log;
    //            timeStamp = Main.GetNowTimeStamp();
    //            path = Format("{0}?v={1}", Concat(_this._url, GetPlatformName()), timeStamp);
    //            log "LoadMainManifest : {0}" with path; create request; yield SendWebRequest -> 1.
    //   state 1: if error: log #2650, call WndForm.QuitApp() and exit;
    //            else _handler = www.downloadHandler as DownloadHandlerAssetBundle;
    //            _testRequest = _handler.assetBundle.LoadAssetAsync("AssetBundleManifest"); yield -> 2.
    //   state 2: if !_testRequest.isDone -> WndForm.QuitApp() (LAB_016d43e0) and exit;
    //            else _mainBundleManifest = _testRequest.asset as AssetBundleManifest;
    //            if _handler.assetBundle != null: Unload(false); else QuitApp; Clear();
    //            for each bundle in _mainBundleManifest.GetAllAssetBundles():
    //              new BundleInfo { _file = name }; _mapBundles.Add(name.ToLower(), info);
    //            bReady = true. cleanup.
    private IEnumerator LoadMainManifest()
    {
        UnityWebRequest www = null;
        DownloadHandlerAssetBundle _handler = null;
        AssetBundleRequest testRequest = null;

        // state 0
        UJDebug.Log(string.Concat("LoadMainManifest : ", _url, ResourcesPath.GetPlatformName()));
        int timeStamp = Main.GetNowTimeStamp();
        string urlBase = string.Concat(_url, ResourcesPath.GetPlatformName());
        string path = string.Format("{0}?v={1}", urlBase, timeStamp);
        UJDebug.LogFormat("LoadMainManifest : {0}", false, UJLogType.None, path);
        www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        if (www == null)
        {
            WndForm.QuitApp();
            yield break;
        }
        yield return www.SendWebRequest();

        // state 1
        if (!string.IsNullOrEmpty(www.error))
        {
            UJDebug.Log("==== Main Bundle Path Error ====");
            WndForm.QuitApp();
            yield break;
        }
        _handler = www.downloadHandler as DownloadHandlerAssetBundle;
        if (_handler == null || _handler.assetBundle == null)
        {
            WndForm.QuitApp();
            yield break;
        }
        testRequest = _handler.assetBundle.LoadAssetAsync("AssetBundleManifest");
        if (testRequest == null)
        {
            WndForm.QuitApp();
            yield break;
        }
        yield return testRequest;

        // state 2
        if (!testRequest.isDone)
        {
            WndForm.QuitApp();
            yield break;
        }
        _mainBundleManifest = testRequest.asset as AssetBundleManifest;
        if (_handler == null || _handler.assetBundle == null)
        {
            WndForm.QuitApp();
            yield break;
        }
        _handler.assetBundle.Unload(false);
        Clear();
        string[] bundles = _mainBundleManifest != null ? _mainBundleManifest.GetAllAssetBundles() : null;
        if (bundles == null)
        {
            WndForm.QuitApp();
            yield break;
        }
        for (int i = 0; i < bundles.Length; i++)
        {
            string name = bundles[i];
            AssetBundleManager.BundleInfo info = new AssetBundleManager.BundleInfo();
            info._file = name;
            _mapBundles.Add(name.ToLower(), info);
        }
        bReady = true;
    }

    // RVA: 0x15CFF14  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/CheckAppVersion.c
    public void CheckAppVersion()
    {
        // Ghidra: if (IsChecking) return; IsChecking = true;
        //   StopCoroutine("CheckSGCVersion"); StartCoroutine("CheckSGCVersion");
        if (IsChecking) return;
        IsChecking = true;
        StopCoroutine("CheckSGCVersion");
        StartCoroutine("CheckSGCVersion");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.<CheckSGCVersion>d__51/MoveNext.c RVA 0x015d3100
    // State machine fields (from dump.cs lines 3972-4008):
    //   <www>5__2 = UnityWebRequest @ 0x28
    // String literals:
    //   #21255 = "{0}?v={1}"
    //   #9997  = "SetConnectCheckState"
    //   #9214  = "ProcessBase"
    //   #7694  = "LoadSGCVersion Path-> {0}"
    //   #2646  = "==== LoadSGCVersion Error ===="
    //   #0     = "0"
    // Linear flow 1-1:
    //   state 0: timeStamp = Main.GetNowTimeStamp();
    //            path = Format("{0}?v={1}", PatchAndroidVersion, timeStamp);
    //            if path == "0": call ProcessBase.SetConnectCheckState(2) via Lua, IsChecking=false, exit.
    //            else: log "LoadSGCVersion Path-> {0}"; www = UnityWebRequest.Get(path); yield SendWebRequest -> 1.
    //   state 1: if !IsNullOrEmpty(error): log #2646; call SetConnectCheckState(5); exit.
    //            else dh = www.downloadHandler; if dh != null:
    //              text = dh.text.Trim(); ok = ParsingSGCVersion(text);
    //              status = ok ? 2 : 1; call SetConnectCheckState(status); IsChecking=false, exit.
    private IEnumerator CheckSGCVersion()
    {
        UnityWebRequest www = null;

        // state 0
        int timeStamp = Main.GetNowTimeStamp();
        string path = string.Format("{0}?v={1}", ResourcesPath.PatchAndroidVersion, timeStamp);
        if (path == "0")
        {
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 2 });
            IsChecking = false;
            yield break;
        }
        UJDebug.LogFormat("LoadSGCVersion Path-> {0}", false, UJLogType.None, path);
        www = UnityWebRequest.Get(path);
        if (www == null)
        {
            yield break;
        }
        yield return www.SendWebRequest();

        // state 1
        if (!string.IsNullOrEmpty(www.error))
        {
            UJDebug.Log("==== LoadSGCVersion Error ====");
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 5 });
            // Ghidra control flow: falls through with no IsChecking=false on this path
            // (label LAB_016d3548 only set on the success path). Preserved 1-1.
            yield break;
        }
        DownloadHandler dh = www.downloadHandler;
        if (dh == null)
        {
            IsChecking = false;
            yield break;
        }
        string body = dh.text;
        if (body != null)
        {
            string text = body.Trim();
            bool ok = ParsingSGCVersion(text);
            int status = ok ? 2 : 1;
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { status });
            IsChecking = false;
        }
    }

    // RVA: 0x15D0020  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/ParsingSGCVersion.c
    // String literals (via work/03_il2cpp_dump/stringliteral.json):
    //   #996   = "//ver"
    //   #20847 = "version"
    // Flow (1-1 from Ghidra):
    //   doc = new XmlDocument();
    //   reader = new StringReader(text);
    //   doc.Load(reader);                                 // vtable @0x648
    //   localVersion = ResourcesPath._patchData.PatchAndroidVersion; // static read; offset 0x28 in PatchHostData
    //   nodes = doc.SelectNodes("//ver");                 // XmlNode.SelectNodes
    //   en = nodes.GetEnumerator();                       // XmlNodeList.GetEnumerator (vtable @0x1b8)
    //   bool result = false;
    //   while (en.MoveNext()) {                           // vtable @ *puVar10
    //     XmlNode node = (XmlNode) en.Current;            // vtable @0x?
    //     XmlAttributeCollection attrs = node.Attributes; // vtable @0x238
    //     XmlNode versionAttr = attrs.GetNamedItem("version"); // vtable @0x188
    //     string ver = versionAttr.InnerText;             // vtable @0x1c8 → InnerText
    //     if (CheckVersionGreatThen(ver, localVersion)) { result = true; break; }
    //   }
    //   (en as IDisposable)?.Dispose();
    //   return result;
    private bool ParsingSGCVersion(string text)
    {
        XmlDocument doc = new XmlDocument();
        StringReader reader = new StringReader(text);
        if (doc == null) return false;
        doc.Load(reader);

        // Ghidra: reads ResourcesPath._patchData (static, offset 0x28 in PatchHostData),
        // then PatchAndroidVersion (offset 0x28 in PatchHostData). The dump.cs property
        // ResourcesPath.PatchAndroidVersion encapsulates exactly this read.
        string localVersion = ResourcesPath.PatchAndroidVersion;

        XmlNodeList nodes = doc.SelectNodes("//ver");
        if (nodes == null)
        {
            throw new System.NullReferenceException();
        }
        System.Collections.IEnumerator en = nodes.GetEnumerator();
        if (en == null)
        {
            throw new System.NullReferenceException();
        }
        bool result = false;
        try
        {
            while (true)
            {
                if (!en.MoveNext())
                {
                    result = false;
                    break;
                }
                XmlNode node = en.Current as XmlNode;
                if (node == null)
                {
                    throw new System.NullReferenceException();
                }
                XmlAttributeCollection attrs = node.Attributes;
                if (attrs == null)
                {
                    throw new System.NullReferenceException();
                }
                XmlNode versionAttr = attrs.GetNamedItem("version");
                if (versionAttr == null)
                {
                    throw new System.NullReferenceException();
                }
                string ver = versionAttr.InnerText;
                if (CheckVersionGreatThen(ver, localVersion))
                {
                    result = true;
                    break;
                }
            }
        }
        finally
        {
            System.IDisposable disp = en as System.IDisposable;
            if (disp != null) disp.Dispose();
        }
        return result;
    }

    // RVA: 0x15D046C  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/CheckVersionGreatThen.c
    private bool CheckVersionGreatThen(string sAPKVersion, string sDefineVersion)
    {
        // Ghidra: log "APK ver: " + sAPKVersion and "Define ver: " + sDefineVersion via UJDebug.Log;
        //   split each by '.' into 3-part arrays; per-part Int32.TryParse (default -1 on fail);
        //   compare lexicographically major/minor/patch; return 1 if APK > Define else 0.
        // TODO: confidence:low — exact log format strings (PTR_StringLiteral_2659/2660) not
        // resolved; logical comparison preserved 1-1.
        UJDebug.Log(string.Concat("APK ver: ", sAPKVersion));
        UJDebug.Log(string.Concat("Define ver: ", sDefineVersion));
        if (sAPKVersion == null || sDefineVersion == null)
        {
            return false;
        }
        string[] apk = sAPKVersion.Split('.');
        string[] def = sDefineVersion.Split('.');
        if (apk.Length != 3 || def.Length != 3)
        {
            return false;
        }
        int[] a = new int[3];
        int[] d = new int[3];
        for (int i = 0; i < 3; i++)
        {
            if (!int.TryParse(apk[i], out a[i])) a[i] = -1;
            if (!int.TryParse(def[i], out d[i])) d[i] = -1;
        }
        if (d[0] < a[0]) return true;
        if (a[0] != d[0]) return false;
        if (d[1] < a[1]) return true;
        if (a[1] != d[1]) return false;
        if (d[2] < a[2]) return false;
        return true;
    }

    // RVA: 0x15D07D8  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/GetBundleHash.c
    public Hash128 GetBundleHash(string bundleName)
    {
        // Ghidra: if _mainBundleManifest != null: return _mainBundleManifest.GetAssetBundleHash(bundleName);
        if (_mainBundleManifest != null)
        {
            return _mainBundleManifest.GetAssetBundleHash(bundleName);
        }
        return default(Hash128);
    }

    // RVA: 0x15D0910  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/HaveBundleName.c
    public bool HaveBundleName(string bundleName)
    {
        // Ghidra: if _mainBundleManifest != null && GetAllAssetBundles().Length > 0:
        //   linear scan with String.op_Equality; return true on hit; else false.
        if (_mainBundleManifest == null)
        {
            return false;
        }
        string[] bundles = _mainBundleManifest.GetAllAssetBundles();
        if (bundles == null)
        {
            throw new System.NullReferenceException();
        }
        for (int i = 0; i < bundles.Length; i++)
        {
            if (bundleName == bundles[i])
            {
                return true;
            }
        }
        return false;
    }

    // RVA: 0x15D0A94  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/GetBundleNames.c
    public string[] GetBundleNames(string directory)
    {
        // Ghidra: List<string> result; if _mainBundleManifest != null walk GetAllAssetBundles()
        //   and where IndexOf(directory) == 0 add to list. Return list.ToArray().
        List<string> list = new List<string>();
        if (_mainBundleManifest != null)
        {
            string[] bundles = _mainBundleManifest.GetAllAssetBundles();
            if (bundles == null)
            {
                throw new System.NullReferenceException();
            }
            for (int i = 0; i < bundles.Length; i++)
            {
                if (bundles[i].IndexOf(directory) == 0)
                {
                    list.Add(bundles[i]);
                }
            }
        }
        return list.ToArray();
    }

    // RVA: 0x15D0CEC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/get_MainBundleManifest_forCheck.c
    public AssetBundleManifest MainBundleManifest_forCheck { get { return _mainBundleManifest_forCheck; } set { _mainBundleManifest_forCheck = value; } }

    // RVA: 0x15D0CF4  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/set_MainBundleManifest_forCheck.c
    
    // RVA: 0x15D0CFC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/InitCheckMainManifest.c
    public void InitCheckMainManifest()
    {
        // Ghidra: if (IsChecking) return; IsChecking=true; StopCoroutine("LoadCheckMainManifest"); StartCoroutine(...);
        if (IsChecking) return;
        IsChecking = true;
        StopCoroutine("LoadCheckMainManifest");
        StartCoroutine("LoadCheckMainManifest");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.<LoadCheckMainManifest>d__62/MoveNext.c RVA 0x015d35d4
    // State machine fields (from dump.cs lines 4012-4049):
    //   <www>5__2 = UnityWebRequest @ 0x28
    //   <_handler>5__3 = DownloadHandlerAssetBundle @ 0x30
    //   <testRequest>5__4 = AssetBundleRequest @ 0x38
    // String literals:
    //   #2581  = "=== LoadCheckMainManifest bundle need update ==="
    //   #2582  = "=== LoadCheckMainManifest check ok ==="
    //   #2590  = "==== Check Bundle Path Error ===="
    //   #7652  = "LoadCheckMainManifest start : "
    //   #7651  = "LoadCheckMainManifest : {0}"
    //   #21255 = "{0}?v={1}"
    //   #3127  = "AssetBundleManifest"
    //   #9997  = "SetConnectCheckState"
    //   #9214  = "ProcessBase"
    // Linear flow 1-1:
    //   state 0: log "LoadCheckMainManifest start : " + _url + GetPlatformName();
    //            timeStamp = GetNowTimeStamp();
    //            path = Format("{0}?v={1}", Concat(_url, GetPlatformName()), timeStamp);
    //            log "LoadCheckMainManifest : {0}";
    //            www = UnityWebRequestAssetBundle.GetAssetBundle(path); SendWebRequest -> state 1.
    //   state 1: if error: log #2590; SetConnectCheckState(5); IsChecking=false; exit.
    //            _handler = www.downloadHandler as DownloadHandlerAssetBundle;
    //            if _handler.assetBundle != null: testRequest = LoadAssetAsync("AssetBundleManifest");
    //                                              yield testRequest -> state 2.
    //            else: Unload(_handler.assetBundle, true); SetConnectCheckState(5); exit.
    //   state 2: _mainBundleManifest_forCheck = testRequest.asset as AssetBundleManifest;
    //            Unload(_handler.assetBundle, false);
    //            if ProcessLunchGame static[0x10] != 0 (opaque "use-bundle-cache" gate, see TODO):
    //              if BundleHashChecker.Instance.CheckMainBundleHash(): log #2582; SetConnectCheckState(10);
    //              else: log #2581; SetConnectCheckState(3);
    //            else: SetConnectCheckState(10).
    //            IsChecking=false. cleanup.
    private IEnumerator LoadCheckMainManifest()
    {
        UnityWebRequest www = null;
        DownloadHandlerAssetBundle _handler = null;
        AssetBundleRequest testRequest = null;

        // state 0
        UJDebug.Log(string.Concat("LoadCheckMainManifest start : ", _url, ResourcesPath.GetPlatformName()));
        int timeStamp = Main.GetNowTimeStamp();
        string urlBase = string.Concat(_url, ResourcesPath.GetPlatformName());
        string path = string.Format("{0}?v={1}", urlBase, timeStamp);
        UJDebug.LogFormat("LoadCheckMainManifest : {0}", false, UJLogType.None, path);
        www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        if (www == null)
        {
            yield break;
        }
        yield return www.SendWebRequest();

        // state 1
        if (!string.IsNullOrEmpty(www.error))
        {
            UJDebug.Log("==== Check Bundle Path Error ====");
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 5 });
            IsChecking = false;
            yield break;
        }
        _handler = www.downloadHandler as DownloadHandlerAssetBundle;
        if (_handler != null && _handler.assetBundle != null)
        {
            testRequest = _handler.assetBundle.LoadAssetAsync("AssetBundleManifest");
        }
        if (testRequest != null)
        {
            yield return testRequest;
        }
        else
        {
            // Ghidra: Unload(_handler.assetBundle, true) on the testRequest==null branch.
            if (_handler != null && _handler.assetBundle != null)
            {
                _handler.assetBundle.Unload(true);
            }
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 5 });
            IsChecking = false;
            yield break;
        }

        // state 2
        _mainBundleManifest_forCheck = testRequest.asset as AssetBundleManifest;
        if (_handler == null || _handler.assetBundle == null)
        {
            yield break;
        }
        _handler.assetBundle.Unload(false);
        // TODO: opaque ProcessLunchGame static bool at static_fields+0x10 — same gate as
        // LoadAssetBundle (see TODO above). Conservatively assume gate==false and skip the
        // hash check, mirroring the "use bundle" gate behaviour. When the static is
        // identified, replace with the real flag.
        bool useBundleGate = false;
        if (useBundleGate)
        {
            if (BundleHashChecker.Instance != null && BundleHashChecker.Instance.CheckMainBundleHash())
            {
                UJDebug.Log("=== LoadCheckMainManifest check ok ===");
                LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 10 });
            }
            else
            {
                UJDebug.Log("=== LoadCheckMainManifest bundle need update ===");
                LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 3 });
            }
        }
        else
        {
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState", new object[] { 10 });
        }
        IsChecking = false;
    }

    // RVA: 0x15D0E08  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/GetCheckBundleHash.c
    public Hash128 GetCheckBundleHash(string bundleName)
    {
        // Ghidra: if _mainBundleManifest_forCheck != null:
        //   return _mainBundleManifest_forCheck.GetAssetBundleHash(bundleName) else default
        if (_mainBundleManifest_forCheck != null)
        {
            return _mainBundleManifest_forCheck.GetAssetBundleHash(bundleName);
        }
        return default(Hash128);
    }

    // RVA: 0x15D0EA0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/GetCheckBundleNames.c
    public string[] GetCheckBundleNames(string directory)
    {
        // Same pattern as GetBundleNames but on _mainBundleManifest_forCheck.
        List<string> list = new List<string>();
        if (_mainBundleManifest_forCheck != null)
        {
            string[] bundles = _mainBundleManifest_forCheck.GetAllAssetBundles();
            if (bundles == null)
            {
                throw new System.NullReferenceException();
            }
            for (int i = 0; i < bundles.Length; i++)
            {
                if (bundles[i].IndexOf(directory) == 0)
                {
                    list.Add(bundles[i]);
                }
            }
        }
        return list.ToArray();
    }

    // RVA: 0x15D1058  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/SetUrl.c
    public void SetUrl(string url)
    {
        // Ghidra: if string.IsNullOrEmpty(url) return; _url = url;
        //   if (!url.EndsWith("/")) _url += "/"; UJDebug.Log(string.Format("set url: {0}", _url));
        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        _url = url;
        if (!_url.EndsWith("/"))
        {
            _url = string.Concat(_url, "/");
        }
        // TODO: confidence:low — log format string (PTR_StringLiteral_9028) not resolved.
        UJDebug.Log(string.Format("AssetBundleManager url: {0}", _url));
    }

    // RVA: 0x15D1164  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/LoadFile.c
    public RequestFile LoadFile(string name)
    {
        // Ghidra: if (string.IsNullOrEmpty(name)) return null;
        //   if (!_mapBundles.TryGetValue(name.ToLower(), out _)) {
        //     var rf = new RequestFile(string.Concat(_url, name));
        //     _waitRequesies.Enqueue(rf);
        //     return rf;
        //   }
        //   return null;
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        AssetBundleManager.BundleInfo dummy;
        if (!_mapBundles.TryGetValue(name.ToLower(), out dummy))
        {
            RequestFile rf = new RequestFile(string.Concat(_url, name));
            _waitRequesies.Enqueue(rf);
            return rf;
        }
        return null;
    }

    // RVA: 0x15CC4FC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/GetBundleNameWithExt.c
    public static string GetBundleNameWithExt(string sBundleName)
    {
        // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager/GetBundleNameWithExt.c RVA 0x15CC4FC
        // String literals (stringliteral.json):
        //   #21231 (entry index 21231 ≈ JSON line 84927) = "{0}.{1}"
        //   #20548 (entry index 20548 ≈ JSON line 82195) = "uab"
        // Returns String.Format("{0}.{1}", sBundleName, "uab") → e.g. "unload64.uab".
        // Cross-verified against runtime mapBundles dump: all keys use ".uab" extension.
        return string.Format("{0}.{1}", sBundleName, "uab");
    }

    // RVA: 0x15D1394  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/LoadAssetBundle.c
    public RequestFile LoadAssetBundle(string assetBundleName, AssetBundleManager.CBAssetBundle cbFunc, bool bForceUseBundle = false)
    {
        // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager/LoadAssetBundle.c RVA 0x15D1394
        // Resolved string literals (stringliteral.json):
        //   StringLit_3123 = "AssetBundleManager LoadAssetBundle, assetbundle info not in mapBundles:"
        //   StringLit_21266 = "{0}{1}{2}"
        //   StringLit_986  = "/"
        // PTR_DAT_03446e60 = ResourcesPath (first static field is `CDNVersion` string).
        // PTR_DAT_03448380 = WndForm_LoadingScreen (static_fields+0x10 = _loadingFlag, per dump.cs
        //   line 49765: `private static bool _loadingFlag; // 0x10`).
        //   Cross-verified: V_UpdateDownLoad sets *(*(PTR_DAT_0345df98+0xb8)+0x10) = 1 right after
        //   WndForm_LoadingScreen.set_showLoading(true), and PTR_DAT_0345df98 in SetLoadingPercent.c
        //   accesses static_fields[0]+0x174 = `_ShowPercent` on WndForm_LoadingScreen.s_instance.
        //   So both PTR symbols resolve to WndForm_LoadingScreen.
        if (string.IsNullOrEmpty(assetBundleName))
        {
            return null;
        }
        // Ghidra: if ((WndForm_LoadingScreen._loadingFlag != 0) || (bForceUseBundle)) { happy path }
        if (!WndForm_LoadingScreen._loadingFlag && !bForceUseBundle)
        {
            return null;
        }
        if (_mapBundles == null)
        {
            throw new System.NullReferenceException();
        }
        AssetBundleManager.BundleInfo info;
        if (!_mapBundles.TryGetValue(assetBundleName.ToLower(), out info))
        {
            UJDebug.LogError(string.Concat("AssetBundleManager LoadAssetBundle, assetbundle info not in mapBundles:", assetBundleName));
            return null;
        }
        LoadDependencies(assetBundleName);
        // Ghidra: _bundleFolder = String.Concat(ResourcesPath.CDNVersion, "/")
        _bundleFolder = string.Concat(ResourcesPath.CDNVersion, "/");
        if (info != null)
        {
            // Ghidra: url = String.Format("{0}{1}{2}", _url, _bundleFolder, info._file)
            string url = string.Format("{0}{1}{2}", _url, _bundleFolder, info._file);
            RequestFile rf = new RequestFile(url, info, cbFunc);
            if (_waitRequesies != null)
            {
                _waitRequesies.Enqueue(rf);
                return rf;
            }
        }
        return null;
    }

    // RVA: 0x15D1F84  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/isAllDependenciesDone.c
    public bool isAllDependenciesDone(AssetBundleManager.BundleInfo info, out bool hasError)
    {
        // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager/isAllDependenciesDone.c
        // 1-1 layout (verified line-by-line):
        //   hasError = false; if (info == null) return true (NRE-equivalent goes through fallthrough)
        //   if (m_Dependencies == null) FAULT
        //   TryGetValue(m_Dependencies, info._file, out deps) →
        //     IF FALSE: skip the inner loop, fall through to `return 1` (Ghidra line 76 = return TRUE).
        //              ↑ This is the critical detail the prior port missed (was throwing NRE).
        //              For bundles with no deps (e.g. unload.uab), LoadDependencies early-returns
        //              and never adds them to m_Dependencies — so TryGetValue is FALSE here, and
        //              the correct gốc behavior is "no deps to wait for, done = true".
        //     IF TRUE: enter loop:
        //       for each dep idx in 0..deps.Length:
        //         _mapBundles.TryGetValue(dep) → if FALSE FAULT
        //         depInfo._wwwRef == null OR not done → return false
        //         depInfo._wwwRef.error not empty → hasError = true; return true
        //     end loop → return true (line 76)
        hasError = false;
        if (info == null) throw new System.NullReferenceException();
        if (m_Dependencies == null) throw new System.NullReferenceException();
        string[] deps;
        if (!m_Dependencies.TryGetValue(info._file, out deps) || deps == null)
        {
            // No deps recorded — 1-1 with Ghidra: fall through to return true.
            return true;
        }
        for (int i = 0; i < deps.Length; i++)
        {
            AssetBundleManager.BundleInfo depInfo;
            if (_mapBundles == null || !_mapBundles.TryGetValue(deps[i], out depInfo) || depInfo == null)
            {
                throw new System.NullReferenceException();
            }
            if (depInfo._wwwRef == null)
            {
                return false;
            }
            if (!depInfo._wwwRef.isDone)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(depInfo._wwwRef.error))
            {
                hasError = true;
                return true; // Ghidra line 67-68: *param_3 = 1; return 1
            }
        }
        return true;
    }

    // RVA: 0x15D20EC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/incDependenciesRef.c
    public void incDependenciesRef(AssetBundleManager.BundleInfo info)
    {
        // Ghidra: foreach dep in m_Dependencies[info._file]: _mapBundles[dep]._wwwRef.IncRef()
        if (info == null)
        {
            return;
        }
        string[] deps;
        if (m_Dependencies == null || !m_Dependencies.TryGetValue(info._file, out deps) || deps == null)
        {
            throw new System.NullReferenceException();
        }
        for (int i = 0; i < deps.Length; i++)
        {
            AssetBundleManager.BundleInfo depInfo;
            if (_mapBundles == null || !_mapBundles.TryGetValue(deps[i], out depInfo) || depInfo == null)
            {
                throw new System.NullReferenceException();
            }
            if (depInfo._wwwRef != null)
            {
                depInfo._wwwRef.IncRef();
            }
        }
    }

    // RVA: 0x15D2230  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/decDependenciesRef.c
    public void decDependenciesRef(AssetBundleManager.BundleInfo info)
    {
        // Ghidra: same as incDependenciesRef but DecRef.
        if (info == null)
        {
            return;
        }
        string[] deps;
        if (m_Dependencies == null || !m_Dependencies.TryGetValue(info._file, out deps) || deps == null)
        {
            throw new System.NullReferenceException();
        }
        for (int i = 0; i < deps.Length; i++)
        {
            AssetBundleManager.BundleInfo depInfo;
            if (_mapBundles == null || !_mapBundles.TryGetValue(deps[i], out depInfo) || depInfo == null)
            {
                throw new System.NullReferenceException();
            }
            if (depInfo._wwwRef != null)
            {
                depInfo._wwwRef.DecRef();
            }
        }
    }

    // RVA: 0x15D15E8  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/LoadDependencies.c
    private void LoadDependencies(string assetBundleName)
    {
        // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager/LoadDependencies.c RVA 0x15D15E8
        // Resolved string literals (stringliteral.json):
        //   StringLit_3122  = "AssetBundle:{0} dependencies i = {1} name = {2}"
        //   StringLit_3124  = "AssetBundleManager LoadDependencies, assetbundle info not in mapBundles:"
        //   StringLit_3121  = "AssetBundle:"
        //   StringLit_135   = " ,LoadDependencies:"
        //   StringLit_986   = "/"
        //   StringLit_21266 = "{0}{1}{2}"
        // PTR_DAT_034480f8 = AssetBundleManager static class (static_fields holds m_Dependencies
        //   at 0x8 and _mainBundleManifest at 0x18 per dump.cs field offsets).
        // PTR_DAT_03446e60 = ResourcesPath (first static field is CDNVersion).
        if (assetBundleName == null)
        {
            throw new System.NullReferenceException();
        }
        string lower = assetBundleName.ToLower();
        if (_mainBundleManifest == null)
        {
            throw new System.NullReferenceException();
        }
        string[] deps = _mainBundleManifest.GetAllDependencies(lower);
        if (deps == null)
        {
            throw new System.NullReferenceException();
        }
        if (deps.Length == 0)
        {
            return;
        }
        if (m_Dependencies == null)
        {
            throw new System.NullReferenceException();
        }
        if (!m_Dependencies.ContainsKey(lower))
        {
            m_Dependencies.Add(lower, deps);
        }
        for (int i = 0; i < deps.Length; i++)
        {
            // Ghidra: log Format("AssetBundle:{0} dependencies i = {1} name = {2}", lower, i.ToString(), deps[i])
            UJDebug.LogTrace(string.Format("AssetBundle:{0} dependencies i = {1} name = {2}", lower, i, deps[i]));
            if (_mapBundles == null)
            {
                throw new System.NullReferenceException();
            }
            AssetBundleManager.BundleInfo depInfo;
            if (!_mapBundles.TryGetValue(deps[i], out depInfo))
            {
                UJDebug.LogError(string.Concat("AssetBundleManager LoadDependencies, assetbundle info not in mapBundles:", lower));
                return;
            }
            if (depInfo == null)
            {
                throw new System.NullReferenceException();
            }
            if (depInfo._wwwRef == null)
            {
                // Ghidra: _bundleFolder = Concat(ResourcesPath.CDNVersion, "/")
                _bundleFolder = string.Concat(ResourcesPath.CDNVersion, "/");
                string url = string.Format("{0}{1}{2}", _url, _bundleFolder, depInfo._file);
                UJDebug.LogTrace(string.Concat("AssetBundle:", lower, " ,LoadDependencies:", depInfo._file));
                RequestFile rf = new RequestFile(url, depInfo, null);
                if (_waitRequesies == null)
                {
                    throw new System.NullReferenceException();
                }
                _waitRequesies.Enqueue(rf);
            }
            if (depInfo._wwwRef != null)
            {
                depInfo._wwwRef.IncRef();
            }
        }
    }

    // RVA: 0x15D237C  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/.ctor.c
    public AssetBundleManager()
    {
        // Ghidra: _MaxDownLoadFiles = 10; _url = ""; _mapBundles = new Dictionary<...>();
        //   _bundleFolder = ""; _dependencies = new List<WWWBundleRef>(); _waitRequesies = new Queue<RequestFile>();
        //   base ctor (MonoBehaviour)
        _MaxDownLoadFiles = 10;
        _url = string.Empty;
        _mapBundles = new Dictionary<string, AssetBundleManager.BundleInfo>();
        _bundleFolder = string.Empty;
        _dependencies = new List<AssetBundleManager.WWWBundleRef>();
        _waitRequesies = new Queue<RequestFile>();
    }

    // RVA: 0x15D24E8  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager/.cctor.c
    static AssetBundleManager()
    {
        // Ghidra: s_instance = null; m_Dependencies = new Dictionary<string, string[]>();
        s_instance = null;
        m_Dependencies = new Dictionary<string, string[]>();
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 83
    public class BundleInfo
    {
        public int _version;
        public string _file;
        public Hash128 _hash;
        public long _nBytes;
        public AssetBundleManager.WWWBundleRef _wwwRef;

        // RVA: 0x15CFE78  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.BundleInfo/.ctor.c
        public BundleInfo()
        {
            // TODO: confidence:low — BundleInfo .ctor.c not produced; trivial default.
        }

    }
    // Source: Il2CppDumper-stub (delegate)  TypeDefIndex: 84
    public delegate void CBAssetBundle(AssetBundleOP bundleOP);

    // Source: Il2CppDumper-stub  TypeDefIndex: 82
    public class WWWBundleRef
    {
        private static Queue<AssetBundleManager.WWWBundleRef> s_queue;
        private int _refCount;
        private UnityWebRequest _www;
        private AssetBundleCreateRequest _wwwLocal;
        private bool _loadLocal;
        private bool _done;
        private string _error;
        private AssetBundleManager.BundleInfo _info;
        private AssetBundle _bundle;

        // RVA: 0x15CF298  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Update.c
        public static void Update()
        {
            // Ghidra: int n = s_queue.Count; if (n<1) return; if (n>9) n=10;
            //   for i in [0,n): pop front, if !CheckExpired(): re-enqueue, else discard.
            // TODO: confidence:low — exact pop/probe sequence opaque without resolving Queue
            // internal layout; faithful structural translation below.
            if (s_queue == null) return;
            int n = s_queue.Count;
            if (n < 1) return;
            if (n > 9) n = 10;
            for (int i = 0; i < n; i++)
            {
                if (s_queue.Count == 0) break;
                AssetBundleManager.WWWBundleRef r = s_queue.Dequeue();
                if (r != null && !r.CheckExpired())
                {
                    s_queue.Enqueue(r);
                }
            }
        }

        // RVA: 0x15CFB08  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Clear.c
        public static void Clear()
        {
            // Ghidra: while (s_queue.Count > 0) Dequeue() and Finish() each entry.
            if (s_queue == null) return;
            while (s_queue.Count > 0)
            {
                AssetBundleManager.WWWBundleRef r = s_queue.Dequeue();
                if (r != null)
                {
                    r.Finish();
                }
            }
        }

        // RVA: 0x15D26C8  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/get_refCount.c
        public int refCount { get { return _refCount; } }

        // RVA: 0x15D26D0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/.ctor.c
        public WWWBundleRef(UnityWebRequest www, AssetBundleCreateRequest wwwLocal, bool bLocal, AssetBundleManager.BundleInfo info)
        {
            // TODO: confidence:low — ctor body not separately decompiled; field-store pattern only.
            _www = www;
            _wwwLocal = wwwLocal;
            _loadLocal = bLocal;
            _info = info;
            _error = string.Empty;
            if (info != null)
            {
                info._wwwRef = this;
            }
            if (s_queue != null)
            {
                s_queue.Enqueue(this);
            }
        }

        // RVA: 0x15D2804/0x15D280C  Ghidra: get_error.c + set_error.c (merged property)
        public string error { get { return _error; } set { _error = value; } }

        // RVA: 0x15D2814  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/get_isDone.c
        public bool isDone { get { // Ghidra: returns *(bool*)(this + 0x29) = _done
            return _done; } }

        // RVA: 0x15D281C  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/get_progress.c
        public float progress { get { // Ghidra:
            //   if (!_loadLocal) {
            //     if (_done) return 1.0f;
            //     if (_www != null) return _www.downloadProgress;
            //   } else {
            //     if (_done) return 1.0f;
            //     if (_wwwLocal != null) return _wwwLocal.progress;
            //   }
            //   return 0.0f;
            if (!_loadLocal)
            {
                if (_done) return 1.0f;
                if (_www != null) return _www.downloadProgress;
            }
            else
            {
                if (_done) return 1.0f;
                if (_wwwLocal != null) return _wwwLocal.progress;
            }
            return 0.0f; } }

        // RVA: 0x15D2220  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/IncRef.c
        public void IncRef()
        {
            _refCount = _refCount + 1;
        }

        // RVA: 0x15D2368  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/DecRef.c
        public void DecRef()
        {
            if (0 < _refCount)
            {
                _refCount = _refCount + -1;
            }
        }

        // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Finish.c
        // RVA 0x15D286C — 1-1 port (verified line-by-line):
        //   plVar5 = &_www (offset 0x18)
        //   if (*plVar5 == 0) {               // _www == null
        //       if (_loadLocal == 0) goto END;
        //       if (_wwwLocal == 0) FAULT;
        //       ab = _wwwLocal.assetBundle;
        //       if (ab != null) _bundle = ab;
        //   } else {                          // _www != null
        //       err = _www.error;
        //       if (IsNullOrEmpty(err)) {     // success path
        //           url = _www.url;
        //           _error = Format("Url: {0}", url);  // stringliteral_7630, set _error temporarily
        //           dh = _www.downloadHandler;
        //           if (dh is DownloadHandlerAssetBundle): _bundle = dh.assetBundle;
        //           _error = string.Empty;    // overwrite with empty after success
        //       } else {                      // failure path
        //           _error = _www.error;
        //       }
        //   }
        //   END (LAB_016d2a14):
        //   _wwwLocal = null;
        //   _loadLocal = false;
        //   _www = null;                      // ← set to null, NO Dispose() call!
        //   _done = true;
        //
        // IMPORTANT: Ghidra does NOT call _www.Dispose(). The prior port's _www.Dispose() was
        // chế cháo — it invalidated the UnityWebRequest that RequestFile._www still references,
        // causing get_error to throw ArgumentNullException ("_unity_self") in subsequent
        // CheckBundleLoad polls. Removing Dispose() leaves the UWR alive for getter access
        // (GC reclaims when both references drop, just like Ghidra IL2CPP).
        public void Finish()
        {
            if (_www == null)
            {
                if (!_loadLocal) goto END;
                if (_wwwLocal == null) throw new System.NullReferenceException();
                AssetBundle ab = _wwwLocal.assetBundle;
                if (ab != null) _bundle = ab;
            }
            else
            {
                string err = _www.error;
                if (string.IsNullOrEmpty(err))
                {
                    // Ghidra: _error = Format("Url: {0}", _www.url) — temp set, then overwritten to empty.
                    // (Ghidra lines 51-57: writes formatted url string, then line 71 overwrites with "".)
                    _error = string.Format("Url: {0}", _www.url);
                    DownloadHandler dh = _www.downloadHandler;
                    if (dh is DownloadHandlerAssetBundle)
                    {
                        _bundle = ((DownloadHandlerAssetBundle)dh).assetBundle;
                    }
                    _error = string.Empty;
                }
                else
                {
                    _error = err;
                }
            }
            END:
            _wwwLocal = null;
            _loadLocal = false;
            _www = null;       // ← set null, NOT Dispose (per Ghidra)
            _done = true;
        }

        // RVA: 0x15D2594  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/CheckExpired.c
        private bool CheckExpired()
        {
            // Ghidra: if (!_done) return false;
            //   if (_refCount < 1) {
            //     UJDebug.LogTrace(Format(<expire fmt>, _info._file));
            //     if (_bundle != null) _bundle.Unload(true);
            //     return true;
            //   }
            //   return false;
            if (!_done)
            {
                return false;
            }
            if (_refCount < 1)
            {
                if (_info == null)
                {
                    throw new System.NullReferenceException();
                }
                UJDebug.LogTrace(string.Format("expire bundle: {0}", _info._file));
                if (_bundle != null)
                {
                    _bundle.Unload(true);
                }
                return true;
            }
            return false;
        }

        // RVA: 0x15D2B60  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Load.c
        public UnityEngine.Object Load(string name)
        {
            // TODO: confidence:low — Load.c shared between overloads; pattern: _bundle.LoadAsset(name).
            if (_bundle == null)
            {
                throw new System.NullReferenceException();
            }
            return _bundle.LoadAsset(name);
        }

        // RVA: 0x15D2BFC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Load.c
        public UnityEngine.Object Load(string name, Type type)
        {
            if (_bundle == null)
            {
                throw new System.NullReferenceException();
            }
            return _bundle.LoadAsset(name, type);
        }

        // RVA: 0x1C8D514  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Load_object_.c
        public T Load<T>(string name) where T : UnityEngine.Object
        {
            // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Load_object_.c RVA 0x1C8D514
            // 1-1: if (_bundle != null) return _bundle.LoadAsset<T>(name); else return null;
            if (_bundle != null)
            {
                return _bundle.LoadAsset<T>(name);
            }
            return null;
        }

        // RVA: 0x15D2CA0  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadAsync.c
        public AssetBundleRequest LoadAsync(string name, Type type)
        {
            if (_bundle == null)
            {
                throw new System.NullReferenceException();
            }
            return _bundle.LoadAssetAsync(name, type);
        }

        // RVA: 0x1C8D5BC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadAsync_object_.c
        public AssetBundleRequest LoadAsync<T>(string name) where T : UnityEngine.Object
        {
            // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadAsync_object_.c RVA 0x1C8D5BC
            // 1-1: if (_bundle != null) return _bundle.LoadAssetAsync<T>(name); else return null;
            if (_bundle != null)
            {
                return _bundle.LoadAssetAsync<T>(name);
            }
            return null;
        }

        // RVA: 0x15D2D44  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadAll.c
        public UnityEngine.Object[] LoadAll()
        {
            if (_bundle == null)
            {
                throw new System.NullReferenceException();
            }
            return _bundle.LoadAllAssets();
        }

        // RVA: 0x15D2DCC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadAllAsync.c
        public AssetBundleRequest LoadAllAsync()
        {
            if (_bundle == null)
            {
                throw new System.NullReferenceException();
            }
            return _bundle.LoadAllAssetsAsync();
        }

        // RVA: 0x1C8D664  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadWithSubAssets_object_.c
        public T[] LoadWithSubAssets<T>(string name) where T : UnityEngine.Object
        {
            // Source: Ghidra work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/LoadWithSubAssets_object_.c RVA 0x1C8D664
            // 1-1: if (_bundle != null) return _bundle.LoadAssetWithSubAssets<T>(name); else return null;
            if (_bundle != null)
            {
                return _bundle.LoadAssetWithSubAssets<T>(name);
            }
            return null;
        }

        // RVA: 0x15D2E54  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/Unload.c
        public void Unload(bool unloadAllLoadedObjects)
        {
            // Ghidra: if (_bundle != null) _bundle.Unload(unloadAllLoadedObjects); _bundle = null;
            if (_bundle != null)
            {
                _bundle.Unload(unloadAllLoadedObjects);
            }
            _bundle = null;
        }

        // RVA: 0x15D2EEC  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/get_isStreamedSceneAssetBundle.c
        public bool isStreamedSceneAssetBundle { get { // Ghidra: if _bundle != null: return _bundle.isStreamedSceneAssetBundle else false
            if (_bundle != null)
            {
                return _bundle.isStreamedSceneAssetBundle;
            }
            return false; } }

        // RVA: 0x15D2F74  Ghidra: work/06_ghidra/decompiled_full/AssetBundleManager.WWWBundleRef/.cctor.c
        static WWWBundleRef()
        {
            // TODO: confidence:low — .cctor body not inspected; expected to allocate s_queue.
            s_queue = new Queue<AssetBundleManager.WWWBundleRef>();
        }

    }
}
