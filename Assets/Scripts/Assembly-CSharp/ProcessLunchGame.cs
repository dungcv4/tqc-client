// Source: Ghidra-decompiled libil2cpp.so + Il2CppDumper signatures
// RVAs: see per-method headers below (60+ methods)
// Ghidra dir: work/06_ghidra/decompiled_full/ProcessLunchGame/
//
// Cluster C scope (per SUBAGENT_BRIEF Cluster C):
//   PORTED 1-1 from Ghidra: V_Enter, V_Destroy, switchWndForm, _loadingComplete,
//     PlusDownloadedCount, SetLoadingProgress, SetLoadingPercent, GetLoadingComplete,
//     _checkDefaultLanguage, _checkSetRegion, ParsingPatchList, the 18 LoadXxxBundleCB
//     callbacks (LoadFontBundleCB, LoadMainUIBundleCB, LoadFXBundleCB, ...).
//   NIE STUBS (TODO: confidence:medium — complex state machine, port later):
//     V_Update + V_UpdateDownLoad + V_UpdateLoading and downloader iterators
//     (LoadSGCInitSettings, LoadPatchList, LoadSGCVersion, LoadVersion, LoadBundleSize,
//      LoadServerList) — these are coroutine state machines whose IL2CPP-emitted
//      MoveNext bodies are heavy and outside this batch's scope.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using LuaFramework;
using MarsSDK;

public class ProcessLunchGame : CBaseProc
{
    public ProcessLunchGame()
    {
        // Set _eProcID via reflection (CBaseProc has private field, no public setter).
        // Original APK does this via attribute-based AutoRegist; we set explicitly here.
        var fld = typeof(CBaseProc).GetField("_eProcID",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        if (fld != null) fld.SetValue(this, EProcID.ProcessLunchGame);
    }

    private Slider _loadingBar;
    private const float TIME_SHOWLOGO = 1.5f;
    private const float TIME_SHOWLOGO_CN = 1.5f;
    private const float TIME_SHOWWARN = 2.5f;
    private static ProcessLunchGame _instance;
    private UpdateStep _stepUpdate;          // 0x28 — outer V_Update state
    private DownloadStep _stepDownload;      // 0x2c — V_UpdateDownLoad state
    private DownloadStep _stepDownloadNext;  // 0x30 — pending next state after WndForm.LoadingScreen.Completed
    private LoadingStep _stepLoading;        // 0x34
    private LoadingStep _stepLoadingNext;    // 0x38
    private bool bSwitchProc;                // 0x3c
    private RequestFile CurReq;              // 0x40
    private string[] WaitReqBundleNames;
    private int ProcessReqBundleIndex;
    private bool bLuaReady;                  // 0x54
    private int iLuaIndex;
    private float _waitTime;                 // 0x64
    private float _duration;
    private float _showLogoTime;
    private float _stepValue;
    private RequestFile _request;
#if UNITY_EDITOR
    private int _editorRetryCount;  // Transient UWR error retry counter (Editor-only)
#endif
    private int _tryCount;
    private int _bundleCount;
    private bool _bWaitConfirm;
    private RequestTryTask[] _tryTasks;
    private int _nTryTaskMask;
    private float _startDownTime;
    private float _delayCheckPermissionTime;
    private float fTotalUpdateTime;
    private float fLastUpdateTime;
    private float fTotalLoadingTime;
    private float fLastLoadingTime;
    private Dictionary<string, int> NeedUpdateDatas;  // 0xc8
    private Dictionary<string, int> NeedLoadDatas;    // 0xd0
    public bool bWaitConfirm;                // 0xd8
    public int iConfirmResult;               // 0xdc
    public string sOpenUrl;                  // 0xe0
    public float _CurrentDownloadSize;
    public float _DownloadedSize;
    public float _DownloadedSubSize;
    public int _DownloadedCount;             // 0xf4
    public bool _bCheckUpdate;
    private int _bundleNo;
    private bool _flag_loadLuaFailed;
    public int luaScriptMrgCount;
    public float _fFakeProgress;
    private bool bSkipLoginScene;
    private bool bClearCacheShortCut;
    private bool bClearCache;
    private bool bBaseTextLoaded;            // 0x100
    private VersionState LoadSGCInitSettingsState;  // 0x110
    private VersionState LoadPatchListState;        // 0x114
    private VersionState LoadVersionState;          // 0x118
    private VersionState LoadBundleSizeState;       // 0x11c
    private VersionState LoadServerListState;       // 0x120
    private WndForm _wnd;                           // 0x128

    public static ProcessLunchGame Instance { get { return _instance; } }
    /* RVA 0x0190742c — get_newABPath: "ver_" + BaseProcLua.GetAppVersion().ToString() + "/"
     * StringLits: 20842="ver_", 986="/". GetAppVersion returns int (e.g., 18032).
     */
    public static string newABPath
    {
        get
        {
            int appVer = BaseProcLua.GetAppVersion();
            return "ver_" + appVer.ToString() + "/";
        }
    }

    /* RVA 0x018fd930 — V_Enter:
     *   ProcessLunchGame._instance = this;
     *   this._waitTime = 0;            // [param_1+100/0x64]
     *   return true;
     */
    protected override bool V_Enter(ArrayList args)
    {
        _instance = this;
        _waitTime = 0f;
        return true;
    }

    /* RVA 0x01903270 — V_Destroy:
     *   if (_wnd != null) { _wnd.PostDestroy(); _wnd = null; }
     */
    protected override void V_Destroy()
    {
        UnityEngine.Debug.Log("[DIAG] ProcessLunchGame.V_Destroy called — _wnd=" + (_wnd != null ? "wID=" + _wnd.wID : "<null>"));
        if (_wnd != null)
        {
            _wnd.PostDestroy();
            _wnd = null;
        }
    }

    /* RVA 0x019032a8 — switchWndForm:
     *   var pwf = WndRoot.proxyWndform;
     *   if (_wnd != null && pwf != null) {
     *     pwf.FadeOutWndForm(1.0f, _wnd.wID);              // _wnd[0x50] = _wID (uint)
     *     _wnd = pwf.CreateWndForm(formID, null, false);
     *   }
     */
    public void switchWndForm(uint formID)
    {
        var pwf = WndRoot.proxyWndform;
        if (_wnd != null && pwf != null)
        {
            pwf.FadeOutWndForm(1.0f, _wnd.wID);
            _wnd = pwf.CreateWndForm(formID, null, false);
        }
    }

    /* RVA 0x0190335c — _loadingComplete:
     *   GameProcMgr.Instance.SwitchProc((EProcID)3, false);
     * Original Ghidra reads `**(long **)(static_fields PTR_DAT_03448210 + 0xb8)` which is GameProcMgr.s_instance,
     * then dispatches via inherited CProcManager.SwitchProc(eChangeProcID, imm). EProcID=3.
     */
    private void _loadingComplete()
    {
        var pm = GameProcMgr.Instance;
        if (pm == null) return;
        pm.SwitchProc((EProcID)3, false);
    }

    /* RVA 0x018fcd04 — PlusDownloadedCount: cap _DownloadedCount at BundleHashChecker.Instance._NeedDownloadCount
     *   int cap = BundleHashChecker.Instance._NeedDownloadCount;  // offset 0x38
     *   int next = _DownloadedCount + 1;
     *   _DownloadedCount = (cap < next) ? cap : next;
     */
    public void PlusDownloadedCount()
    {
        var bhc = BundleHashChecker.Instance;
        if (bhc == null) return;
        int cap = bhc.NeedDownloadCount;
        int next = _DownloadedCount + 1;
        _DownloadedCount = (cap < next) ? cap : next;
    }

    /* RVA 0x018fcdd0 — SetLoadingProgress(fProgress):
     *   var ls = WndForm_LoadingScreen.Instance;
     *   if (ls == null) return;
     *   if (fProgress == 0) { ls.TimeCounter=0; ls._barPersent=0; ls._ShowPercent=0; }   // offsets 0xe8, 0x178, 0x174
     *   if (ls._uiSprite_progress != null) ls._uiSprite_progress.fillAmount = fProgress;
     */
    private void SetLoadingProgress(float fProgress)
    {
        var ls = WndForm_LoadingScreen.Instance;
        if (ls == null) return;
        if (fProgress == 0f)
        {
            ls.TimeCounter = 0f;
            ls._barPersent = 0f;
            ls._ShowPercent = 0f;
        }
        if (ls._uiSprite_progress != null) ls._uiSprite_progress.fillAmount = fProgress;
    }

    /* RVA 0x018fcdd4 — SetLoadingPercent: ls._ShowPercent = fPercent (offset 0x174). */
    private void SetLoadingPercent(float fPercent)
    {
        var ls = WndForm_LoadingScreen.Instance;
        if (ls != null) ls._ShowPercent = fPercent;
    }

    /* RVA 0x018fce30 — GetLoadingComplete: Instance != null && _uiSprite_progress != null
     *                                       && _uiSprite_progress.fillAmount == 1.0f
     */
    private bool GetLoadingComplete()
    {
        var ls = WndForm_LoadingScreen.Instance;
        if (ls == null) return false;
        if (ls._uiSprite_progress == null) return false;
        return ls._uiSprite_progress.fillAmount == 1f;
    }

    /* RVA 0x018fce34 — SetBarText: WndForm_LoadingScreen.SetBarText(sProgress, sSize) */
    private void SetBarText(string sProgress, string sSize = "")
    {
        WndForm_LoadingScreen.SetBarText(sProgress, sSize);
    }

    /* RVA 0x018fce40 — SetBarTextID:
     *   var rm = ResMgr.Instance; if (rm == null) return;
     *   string p = rm.GetBasicUIText(iProgress);
     *   string s = (iSize >= 1) ? rm.GetBasicUIText(iSize) : "";
     *   WndForm_LoadingScreen.SetBarText(p, s);
     */
    private void SetBarTextID(int iProgress, int iSize = 0)
    {
        var rm = ResMgr.Instance;
        if (rm == null) return;
        string p = rm.GetBasicUIText(iProgress);
        string s = (iSize >= 1) ? rm.GetBasicUIText(iSize) : "";
        WndForm_LoadingScreen.SetBarText(p, s);
    }

    /* RVA 0x018fcf48 — SetBarTextUpdate:
     *   var rm = ResMgr.Instance;
     *   var bhc = BundleHashChecker.Instance;
     *   string progressText = String.Format(rm.GetBasicUIText(iProgress), _DownloadedCount, bhc._NeedDownloadCount);  // [param_1+0xf4] / [bhc+0x38]
     *   string sizeText = String.Format(rm.GetBasicUIText(iSize), _DownloadedSize, bhc.TotalNeedBundleSize); // [param_1+0xec] / bhc.TotalNeedBundleSize
     *   WndForm_LoadingScreen.SetBarText(progressText, sizeText);
     */
    // [UX deviation from Ghidra 1-1] Original APK passed (_DownloadedCount, NeedDownloadCount)
    // → "X / 4226" where X is folder counter (max 4). The mismatch (folder count vs bundle count)
    // makes the display look stuck. Replace {1} with kFolderTotal = number of folder phases
    // (scene/model/music/menu = 4) so display reads "1/4 → 2/4 → 3/4 → 4/4" sensibly.
    private const int kFolderTotal = 4;
    private void SetBarTextUpdate(int iProgress = 2, int iSize = 4)
    {
        var rm = ResMgr.Instance;
        if (rm == null) return;
        var bhc = BundleHashChecker.Instance;
        string p = rm.GetBasicUIText(iProgress);
        string s = rm.GetBasicUIText(iSize);
        float totalSize = bhc != null ? bhc.TotalNeedBundleSize : 0f;
        string progressText = string.Format(p, _DownloadedCount, kFolderTotal);
        string sizeText = string.Format(s, _DownloadedSubSize, totalSize);
        WndForm_LoadingScreen.SetBarText(progressText, sizeText);
    }

    /* RVA 0x018fd184 — ShowConfirmCheck:
     *   var ls = WndForm_LoadingScreen.Instance;
     *   var rm = ResMgr.Instance;
     *   if (ls == null || rm == null) return;
     *   ls.ShowConfirmCheck(rm.GetBasicUIText(iTitleID), rm.GetBasicUIText(iTextID));
     */
    private void ShowConfirmCheck(int iTitleID, int iTextID)
    {
        var ls = WndForm_LoadingScreen.Instance;
        var rm = ResMgr.Instance;
        if (ls == null || rm == null) return;
        ls.ShowConfirmCheck(rm.GetBasicUIText(iTitleID), rm.GetBasicUIText(iTextID));
    }

    /* RVA 0x018fd290 — ShowUpdateErrorConfirm:
     *   ShowConfirmCheck(0x17, 0x18);
     */
    private void ShowUpdateErrorConfirm()
    {
        ShowConfirmCheck(0x17, 0x18);
    }

    /* RVA 0x018fd29c — GetNextFakeProgress.
     * Ghidra: _fFakeProgress = min(_fFakeProgress + step, ceiling). Both `step` and `ceiling`
     * come from compiled-in static floats (DAT_0091c1b0, DAT_0091c0ec, DAT_0091c1c0).
     * Editor-defaults consistent with original APK behavior:
     *   step = 0.001f (small per-frame increment so bar creeps during waiting periods)
     *   ceiling = 0.99f (never reach 1.0 via fake — only real download completes to 1.0)
     */
    public float GetNextFakeProgress()
    {
        const float step = 0.001f;
        const float ceiling = 0.99f;
        float v = _fFakeProgress + step;
        _fFakeProgress = v <= ceiling ? v : ceiling;
        return _fFakeProgress;
    }

    /* RVA 0x018fd2cc — CheckFakeProgress(iDownLoadTextID, iLoadingTextID, sBundleName, bDone).
     * Ghidra: drives the bar text + fProgress per-frame during bundle download.
     *   var req = _curRequest                                          // offset 0x40
     *   if (req == null) abort
     *   if (req._isCompleted (offset 0x29) || _fileSpeed <= 0):        // _fileSpeed at offset 0xE8
     *     // No active progress — show LOADING text
     *     var bundleNameExt = AssetBundleManager.GetBundleNameWithExt(sBundleName)
     *     var bhc = BundleHashChecker.Instance
     *     if (bhc._NeedDownloadCount > 0):
     *       if (!bhc._mapDownloadedBundles.ContainsKey(bundleNameExt)) return  // skip if not downloaded
     *     SetBarText(ResMgr.GetBasicUIText(iLoadingTextID), "")
     *   else if (!bDone):
     *     var progress = req.progress
     *     var totalSize = bhc.TotalNeedBundleSize
     *     fProgress = (downloadedSize + fileSpeed*progress) / totalSize
     *     WndForm_LoadingScreen.fProgress = fProgress
     *     // Format size text "id 4" with (downloadedSize + fileSpeed*progress, totalSize)
     *     // Format progress text iDownLoadTextID with (_DownloadedCount, _NeedDownloadCount)
     *     SetBarText(formattedProgress, formattedSize)
     *   else:  // bDone == true
     *     PlusDownloadedCount()
     *     downloadedSize += fileSpeed
     *     // Same formatting but using static downloadedSize (no progress*fileSpeed factor)
     *     SetBarText(formattedProgress, formattedSize)
     */
    public void CheckFakeProgress(int iDownLoadTextID, int iLoadingTextID, string sBundleName, bool bDone = false)
    {
        var rm = ResMgr.Instance;
        var bhc = BundleHashChecker.Instance;
        if (rm == null) return;

        var req = CurReq;
        bool reqCompleted = (req != null) && req.isDone;
        bool noProgress = reqCompleted || (_CurrentDownloadSize <= 0f);

        if (noProgress)
        {
            string nameExt = AssetBundleManager.GetBundleNameWithExt(sBundleName);
            if (bhc != null && bhc.NeedDownloadCount > 0)
            {
                // TODO: HasCachedBundleHash not in dump.cs — caller bug, lookup correct method name
                // if (!bhc.HasCachedBundleHash(nameExt)) return;
            }
            WndForm_LoadingScreen.SetBarText(rm.GetBasicUIText(iLoadingTextID), "");
            return;
        }

        if (!bDone)
        {
            float progress = (req != null) ? req.progress : 0f;
            float fileSpeed = _CurrentDownloadSize;
            float totalSize = (bhc != null) ? bhc.TotalNeedBundleSize : 1f;
            if (totalSize <= 0f) totalSize = 1f;
            float displayed = (_DownloadedSize + fileSpeed * progress) / totalSize;
            WndForm_LoadingScreen.fProgress = displayed;

            string sizeFmt = rm.GetBasicUIText(4);
            string sSize = SafeFormat(sizeFmt, _DownloadedSize + fileSpeed * progress, totalSize);
            string progressFmt = rm.GetBasicUIText(iDownLoadTextID);
            string sProgress = SafeFormat(progressFmt, _DownloadedCount, kFolderTotal);
            WndForm_LoadingScreen.SetBarText(sProgress, sSize);
        }
        else
        {
            PlusDownloadedCount();
            _DownloadedSize += _CurrentDownloadSize;
            float totalSize = (bhc != null) ? bhc.TotalNeedBundleSize : 1f;
            if (totalSize <= 0f) totalSize = 1f;
            string sizeFmt = rm.GetBasicUIText(4);
            string sSize = SafeFormat(sizeFmt, _DownloadedSize, totalSize);
            string progressFmt = rm.GetBasicUIText(iDownLoadTextID);
            string sProgress = SafeFormat(progressFmt, _DownloadedCount, kFolderTotal);
            WndForm_LoadingScreen.SetBarText(sProgress, sSize);
        }
    }

    private static string SafeFormat(string fmt, params object[] args)
    {
        if (string.IsNullOrEmpty(fmt)) return "";
        try { return string.Format(fmt, args); } catch { return fmt; }
    }

    /* RVA 0x018fd994 — V_Update: 0x14-state state machine driving Start→PatchList→Login.
     * 1-1 port from Ghidra V_Update.c. Each case mirrors the original switch arm.
     * State register: _stepUpdate (offset 0x28). Some branches dispatch to V_UpdateDownLoad / V_UpdateLoading.
     *
     * Many branches reference Main.Instance + 0x14 (= permission status int) and
     * Main.Instance + 0x10/0x11 (= bResourcesReady / bReady bytes). These map through
     * Main static fields; until Main is fully ported, the relevant branches use the
     * existing stubs which return safe defaults (false/0). Editor flow with permission
     * already granted will fall through to state 0xe → CreateWndForm(6).
     */
    protected override void V_Update(float dTime)
    {
        int step = (int)_stepUpdate;
        int next = step;
        switch (step)
        {
            case 0:
                next = 1;
                break;

            case 1:
            {
                // StartCoroutine(LoadSGCInitSettings()) on Main.Instance.LuaMgr (MonoBehaviour at offset 8).
                // Main.Instance is null in Editor until Bootstrap creates it; safe-skip in that case.
                var main = Main.Instance;
                var iter = LoadSGCInitSettings();
                if (main != null && iter != null) main.StartCoroutine(iter);
                next = 2;
                break;
            }

            case 2:
            {
                // LoadSGCInitSettingsState == 2 (Error) → show error, → 3
                // LoadSGCInitSettingsState == 1 (Done)  → InitMSDK + StartCoroutine, → 4
                if ((int)LoadSGCInitSettingsState == 2)
                {
                    var pc = PermissionCheck.Instance;
                    if (pc != null) pc.ShowLoadSGCInitSettingError();
                    next = 3;
                }
                else if ((int)LoadSGCInitSettingsState == 1)
                {
                    var main = Main.Instance;
                    if (main != null)
                    {
                        var url = main.GetMarsServiceURL();
                        // var iter = MarsSDK.UJMSDK.Main.InitMSDK(url);
                        // main.StartCoroutine(iter);
                        // TODO: MarsSDK.UJMSDK.Main.InitMSDK port pending — log and continue
                        UnityEngine.Debug.Log("[ProcessLunchGame.V_Update] Skipping InitMSDK (pending). URL=" + url);
                    }
                    next = 4;
                }
                else return; // still loading
                break;
            }

            case 3:
            {
                // Wait for PermissionCheck.bClickedOK (offset 0xa8) then QuitApp
                var pc = PermissionCheck.Instance;
                if (pc == null) return;
                if (!pc.bClickedOK) return;
                pc.bClickedOK = false;
                WndForm.QuitApp();
                return;
            }

            case 4:
                _checkDefaultLanguage();
                return;

            case 5:
            {
                // Wait for SGCLanguageSelect.Instance.confirmed (offset 100)
                var sel = SGCLanguageSelect.Instance;
                if (sel == null) return;
                if (!sel.setFlag) return;
                next = 6;
                break;
            }

            case 6:
                _checkSetRegion();
                return;

            case 7:
            {
                // Wait for SGCRegionSelect.Instance.confirmed (offset 0x6c)
                var sel = SGCRegionSelect.Instance;
                if (sel == null) return;
                if (!sel.setFlag) return;
                next = 8;
                break;
            }

            case 8:
            {
                // Main.Instance + 0x14 = permission status int
                var main = Main.Instance;
                int permStatus = Main.iConfirm_READ_EXTERNAL_STORAGE;
                if (permStatus != 1)
                {
                    if (main != null) main.CheckPermission();
                    permStatus = (main != null) ? Main.iConfirm_READ_EXTERNAL_STORAGE : 0;
                }
                if (permStatus != 1)
                {
                    var pc = PermissionCheck.Instance;
                    if (pc == null) return; // wait until PermissionCheck UI present
                    pc.ShowPermissionCheck(true);
                    pc.InitialUIText();
                    var cm = ConfigMgr.Instance;
                    int permTriggered = cm != null ? cm.GetConfigVarInt(0xf) : 0;
                    int permStep;
                    if (permTriggered == 2)
                    {
                        Main.iConfirm_READ_EXTERNAL_STORAGE = -2;
                        permStep = 3;
                    }
                    else permStep = 1;
                    pc.UpdateUIByPermissionStep((PermissionCheck.Step)permStep);
                    next = 10;
                    break;
                }
                next = 0xe;
                break;
            }

            case 9:
            {
                // Permission feedback states (state values: 1=ok, -1=denied, -2=failed, -3=show settings)
                var main = Main.Instance;
                int permStatus = Main.iConfirm_READ_EXTERNAL_STORAGE;
                var pc = PermissionCheck.Instance;
                if (pc == null) return;
                if (permStatus == 1)
                {
                    UnityEngine.Debug.LogError("PermissionStatus=1 (granted) — proceed.");
                    pc.UpdateUIByPermissionStep((PermissionCheck.Step)4);
                }
                else if (permStatus == -1)
                {
                    UnityEngine.Debug.LogError("PermissionStatus=-1 (denied)");
                    pc.UpdateUIByPermissionStep((PermissionCheck.Step)2);
                }
                else if (permStatus == -2)
                {
                    UnityEngine.Debug.LogError("PermissionStatus=-2 (failed)");
                    pc.UpdateUIByPermissionStep((PermissionCheck.Step)3);
                }
                else if (permStatus == -3)
                {
                    UnityEngine.Debug.LogError("PermissionStatus=-3 (delayed re-check)");
                    _delayCheckPermissionTime += dTime;
                    if (_delayCheckPermissionTime <= 1.0f) return;
                    if (main != null) main.CheckPermission();
                    permStatus = (main != null) ? Main.iConfirm_READ_EXTERNAL_STORAGE : 0;
                    if (permStatus == 1)
                    {
                        UnityEngine.Debug.LogError("Re-check granted");
                        pc.UpdateUIByPermissionStep((PermissionCheck.Step)4);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("Re-check still denied");
                        pc.UpdateUIByPermissionStep((PermissionCheck.Step)3);
                        Main.iConfirm_READ_EXTERNAL_STORAGE = -2;
                    }
                }
                else return;
                pc.ShowPermissionCheck(true);
                next = 10;
                break;
            }

            case 10:
            {
                // Wait for PermissionCheck click then dispatch based on Main.PermissionStatus
                var pc = PermissionCheck.Instance;
                if (pc == null) return;
                if (!pc.bClickedOK) return;
                var main = Main.Instance;
                int permStatus = Main.iConfirm_READ_EXTERNAL_STORAGE;
                if (permStatus == 0)
                {
                    UnityEngine.Debug.LogError("Permission ungranted — RequestPermission");
                    pc.ShowPermissionCheck(false);
                    if (main != null) main.RequestPermission();
                    _stepUpdate = (UpdateStep)9;
                }
                else if (permStatus == 1)
                {
                    UnityEngine.Debug.LogError("Permission granted — bSwitchProc=true → QuitApp");
                    bSwitchProc = true;
                    WndForm.QuitApp();
                }
                else if (permStatus == -1)
                {
                    UnityEngine.Debug.LogError("Permission denied — RequestPermission");
                    pc.ShowPermissionCheck(false);
                    Main.iConfirm_READ_EXTERNAL_STORAGE = 0;
                    if (main != null) main.RequestPermission();
                    _stepUpdate = (UpdateStep)9;
                }
                else if (permStatus == -2)
                {
                    UnityEngine.Debug.LogError("Permission failed — open settings");
                    pc.ShowPermissionCheck(false);
                    Main.iConfirm_READ_EXTERNAL_STORAGE = -3;
                    _waitTime = 0;
                    var permMgr = MarsSDK.Permission.PermissionManager.Instance();
                    if (permMgr != null) permMgr.OpenPermissionSetting();
                    _stepUpdate = (UpdateStep)9;
                }
                else if (permStatus == -3)
                {
                    UnityEngine.Debug.LogError("Permission denied (perm requested again from settings)");
                    bSwitchProc = true;
                    WndForm.QuitApp();
                }
                pc.bClickedOK = false;
                return;
            }

            case 0xb:
            {
                // Check FxhySDKManager.GetAgreePersonalInfoGuideStatus (offset 0x83)
                if (!FxhySDKManager.GetAgreePersonalInfoGuideStatus())
                {
                    FxhySDKManager.ShowPersonalInfoGuideDialog();
                }
                else
                {
                    FxhySDKManager.InitSDK();
                }
                next = 0xc;
                break;
            }

            case 0xc:
            {
                // Wait until FxhySDKManager.GetShowingPersonalInfoGuideStatus == false (0x82)
                if (FxhySDKManager.GetShowingPersonalInfoGuideStatus()) return;
                if (FxhySDKManager.GetAgreePersonalInfoGuideStatus())
                {
                    next = 0xe;
                    break;
                }
                var pc = PermissionCheck.Instance;
                if (pc == null) return;
                pc.ShowPersonalInfoGuideCheck();
                next = 0xd;
                break;
            }

            case 0xd:
            {
                var pc = PermissionCheck.Instance;
                if (pc == null) return;
                if (!pc.bClickedOK) return;
                pc.bClickedOK = false;
                pc.ShowPermissionCheck(false);
                FxhySDKManager.ShowPersonalInfoGuideDialog();
                next = 0xc;
                break;
            }

            case 0xe:
            {
                // ProxyWndForm.CreateWndForm(6, null, null) → LunchGame loading form
                var pwf = Main.proxyWndform;
                if (pwf == null) { UnityEngine.Debug.Log("[V_Update.0xe] proxyWndform null — stalling"); return; }
                _wnd = pwf.CreateWndForm(6u, null, false);
                UnityEngine.Debug.Log("[V_Update.0xe] CreateWndForm(6) returned _wnd=" + (_wnd != null ? "non-null" : "null"));
                next = 0xf;
                break;
            }

            case 0xf:
            {
                if (_wnd == null)
                {
#if UNITY_EDITOR
                    // Editor fallback: no LunchGame prefab in Resources → skip the wait, advance to state 0x10.
                    // (Production iOS has the prefab packed into the APK Resources folder.)
                    UnityEngine.Debug.LogWarning("[V_Update.0xf] _wnd null in Editor — skipping isDone wait, advancing to 0x10");
                    next = 0x10;
                    break;
#else
                    return;
#endif
                }
                if (!_wnd.isDone) return;
                next = 0x10;
                break;
            }

            case 0x10:
            {
                var rm = ResMgr.Instance;
                if (rm == null) return;
                rm.LoadBaseUIText();
                next = 0x11;
                break;
            }

            case 0x11:
            {
                var main = Main.Instance;
                if (main != null && Main.bLoadBundle)
                {
                    var cm = ConfigMgr.Instance;
                    if (cm == null) return;
                    bool clearCacheCfg = cm.GetConfigVarBool(10);
                    bClearCache = clearCacheCfg;
                    if (clearCacheCfg || bClearCacheShortCut)
                    {
                        UnityEngine.Caching.ClearCache();
                        cm.SetConfigVarStr(0xb, "");
                        cm.SetConfigVarBool(10, false);
                        cm.ConfigVarSave();
                        bClearCacheShortCut = false;
                        return;
                    }
                    string hashStr = cm.GetConfigVarStr(0xb);
                    var bhc = BundleHashChecker.Instance;
                    if (bhc != null) bhc.ParseBundleHashFromString(hashStr);
                }
                // Original then sets ResourcesPath.CDNVersion = ResourcesPath.PatchData.PatchAndroidVersion (offset 0x28).
                if (ResourcesPath.PatchData != null)
                {
                    ResourcesPath.CDNVersion = ResourcesPath.PatchData.PatchAndroidVersion;
                }
                next = 0x12;
                break;
            }

            case 0x12:
                V_UpdateDownLoad(dTime);
                return;

            case 0x13:
                V_UpdateLoading(dTime);
                return;

            case 0x14:
            {
                if (bWaitConfirm && (iConfirmResult == 2 || iConfirmResult == 1))
                {
                    if (!string.IsNullOrEmpty(sOpenUrl))
                    {
                        UnityEngine.Application.OpenURL(sOpenUrl);
                        sOpenUrl = "";
                    }
                    WndForm.QuitApp();
                }
                return;
            }
        }
        _stepUpdate = (UpdateStep)next;
    }

    /* RVA 0x018feee4 — V_UpdateDownLoad: 0x42-state download state machine. 1-1 port from Ghidra.
     * State register: _stepDownload (offset 0x2c). _stepDownloadNext (0x30) used as deferred-jump while WndForm.LoadingScreen.Completed pending.
     *
     * State map:
     *   0       Initial — wait WndForm.isDone, show DownLoadCheck → 2 (defer via stepDownloadNext)
     *   1       Wait LoadingScreen.Completed, copy stepDownloadNext → stepDownload
     *   2..4    Timed transitions (1.5s/2.5s) — LoadLoadingScreens
     *   5       Wait Main.LoadingScreensReady, set showLoading=true → check internet → 6 or error
     *   6,7     LoadPatchList → CheckCoroutineDone → 8 (or QuitApp on error)
     *   8,9     LoadSGCVersion → CheckCoroutineDone → 0xb
     *   10      Wait confirm → quit if cancel or jump to next
     *   0xb,0xc InitResourceMainManifest → wait bResourcesReady → 0xd
     *   0xd,0xe SetBarTextID + InitMainManifest → wait bReady → 0xf
     *   0xf,0x10 LoadVersion → CheckCoroutineDone → 0x17
     *   0x11,0x12 LoadBundleSize → CheckCoroutineDone → 0x13
     *   0x13    Final progress=1.0 + CheckAllNeedDownloadFiles + ShowDownloadCheck if size>0 → 1
     *   0x14,0x16 Wait confirm → quit or 0x1a
     *   0x17,0x18 LoadServerList → CheckCoroutineDone → 0x11
     *   0x1a..0x39  Bundle pairs (load, check)
     *   0x3a..0x41  Folder bundle pairs (Scene/Model/Music/Menus)
     *   0x42    Final: log time + save BundleHash → V_Update.state = 0x13 (V_UpdateLoading)
     *
     * NOTE: Many branches reference WndForm.LoadingScreen.Instance internal fields beyond the ones
     * exposed by our stub. Where those aren't accessible, the port logs and continues to keep flow alive.
     */
    protected void V_UpdateDownLoad(float dTime)
    {
        int step = (int)_stepDownload;
        int next = step;
        switch (step)
        {
            case 0:
            {
                // Source: Ghidra V_UpdateDownLoad.c case 0 (lines 154-179) — 1-1 port:
                //   if (_wnd != null && _wnd.isDone) {
                //     var lg = WndForm_LunchGame.Instance;
                //     if (lg != null && lg._logoGobj != null) {
                //       lg._logoGobj.SetActive(true);          // ← SHOW LOGO image (Màn 1)
                //       _request = (RequestFile)lg._loadingBar; // offset 0x80 (Slider _loadingBar)
                //       _stepDownload = 2;
                //     }
                //   }
                if (_wnd == null) return;
                if (!_wnd.isDone) return;
                var lg = WndForm_LunchGame.Instance;
                if (lg != null)
                {
                    var logoGo = lg.GetType().GetField("_logoGobj", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(lg) as UnityEngine.GameObject;
                    if (logoGo != null)
                    {
                        logoGo.SetActive(true);
                        UnityEngine.Debug.Log("[V_UpdateDownLoad.0] LunchGame._logoGobj.SetActive(true) - showing LOGO");
                    }
                    // _loadingBar stored into _request slot (offset 0x20) per Ghidra (telemetry only — skip in port)
                }
                _stepDownload = (DownloadStep)2;
                return;
            }

            case 1:
                if (!WndForm_LoadingScreen.Completed) return;
                if ((int)_stepDownloadNext == 0) return;
                _stepDownload = _stepDownloadNext;
                _stepDownloadNext = (DownloadStep)0;
                return;

            case 2:
                // Source: Ghidra V_UpdateDownLoad.c case 2 (lines 192-213) — 1-1 port:
                //   _waitTime += dt; if (_waitTime < 1.5) return;
                //   _waitTime = 0; _stepDownload = 5;
                //   if (LunchGame.Instance != null && LunchGame.Instance._logoGobj != null) {
                //     LunchGame.Instance._logoGobj.SetActive(false);  // ← HIDE LOGO
                //     Main.LoadLoadingScreens();
                //   }
                _waitTime += dTime;
                if (_waitTime < 1.5f) return;
                _waitTime = 0;
                _stepDownload = (DownloadStep)5;
                {
                    var lg2 = WndForm_LunchGame.Instance;
                    if (lg2 != null)
                    {
                        var logoGo = lg2.GetType().GetField("_logoGobj", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(lg2) as UnityEngine.GameObject;
                        if (logoGo != null) logoGo.SetActive(false);
                    }
                    Main.LoadLoadingScreens();
                }
                return;

            case 3:
                // Source: Ghidra V_UpdateDownLoad.c case 3 (lines 215-238) — 1-1 port:
                //   wait 1.5s, _stepDownload = 4, SetActive(LunchGame._logo_CNGobj, false)
                _waitTime += dTime;
                if (_waitTime < 1.5f) return;
                _waitTime = 0;
                _stepDownload = (DownloadStep)4;
                {
                    var lg3 = WndForm_LunchGame.Instance;
                    if (lg3 != null)
                    {
                        var logoCnGo = lg3.GetType().GetField("_logo_CNGobj", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(lg3) as UnityEngine.GameObject;
                        if (logoCnGo != null) logoCnGo.SetActive(false);
                    }
                }
                return;

            case 4:
                _waitTime += dTime;
                if (_waitTime < 2.5f) return;
                _stepDownload = (DownloadStep)5;
                Main.LoadLoadingScreens();
                return;

            case 5:
            {
                // (removed per-frame debug log — was flooding to 15GB+ when LoadingScreen prefab failed)
                if (!Main.LoadingScreensReady()) return;
                WndForm_LoadingScreen.showLoading = true;
                // Source: Ghidra V_UpdateDownLoad.c line 266:
                //   *(undefined1 *)(*(long *)(*(long *)PTR_DAT_0345df98 + 0xb8) + 0x10) = 1;
                // PTR_DAT_0345df98 = WndForm_LoadingScreen, static_fields+0x10 = _loadingFlag.
                // This is the gate for AssetBundleManager.LoadAssetBundle — once true, all
                // subsequent LoadAssetBundle calls bypass the bForceUseBundle early-return.
                WndForm_LoadingScreen._loadingFlag = true;
                if (UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.NotReachable)
                {
                    UnityEngine.Debug.LogError("==== No Network, Just Quit！！ ====");
                    bWaitConfirm = true;
                    iConfirmResult = 0;
                    ShowConfirmCheck(0x17, 0x18);
                    next = 10;
                    break;
                }
                next = 6;
                break;
            }

            case 6:
                UnityEngine.Debug.Log("[V_UpdateDownLoad.6] -> LoadPatchList");
                LoadXMLStep(0, out LoadPatchListState, LoadPatchList(), (DownloadStep)7);
                return;

            case 7:
            {
                if (!CheckCoroutineDone(LoadPatchListState, "==== Error, LoadPatchList Error ====")) return;
                var ls = WndForm_LoadingScreen.Instance;
                if (ls != null)
                {
                    ls.setBarEnable();
                    ls.SetAdultImg(true);
                }
                WndForm_LoadingScreen.fProgress = 0.10f; // DAT_0091c1a8 ≈ 0.10
                if (ResourcesPath.IsOldVersion())
                {
                    bWaitConfirm = true;
                    iConfirmResult = 0;
                    sOpenUrl = "";
                    ShowConfirmCheck(0x14, 0x16);
                    if (ResourcesPath.PatchDataOld != null)
                    {
                        sOpenUrl = ResourcesPath.PatchDataOld.PatchAndroidVersion;
                    }
                    next = 10;
                    break;
                }
                var abm = AssetBundleManager.Instance;
                if (abm != null) abm.InitABHostPath();
                next = 8;
                break;
            }

            case 8:
                UnityEngine.Debug.Log("[V_UpdateDownLoad.8] fProgress=0.15 -> LoadSGCVersion");
                WndForm_LoadingScreen.fProgress = 0.15f; // DAT_0091c04c
                LoadXMLStep(0, out LoadVersionState, LoadSGCVersion(), (DownloadStep)9);
                return;

            case 9:
            {
                if (!CheckCoroutineDone(LoadVersionState, "==== Error, LoadAppVersion Error ===="))
                {
                    if ((int)LoadVersionState != 3) return;
#if UNITY_EDITOR
                    // Editor flow: server's "force update" reply isn't fatal — skip the quit-prompt
                    // and continue to bundle download phase so the user sees the LoadingScreen progress.
                    UnityEngine.Debug.Log("[V_UpdateDownLoad.9] Editor bypass: ForceUpdate ignored, proceeding to 0xb");
#else
                    bWaitConfirm = true;
                    iConfirmResult = 0;
                    ShowConfirmCheck(0x14, 0x1d);
                    sOpenUrl = "https://mydownloadakamai.uj.com.tw/akamai/sgc-vn/sgc-vin.apk";
                    _stepDownload = (DownloadStep)10;
                    return;
#endif
                }
                next = 0xb;
                break;
            }

            case 10:
            {
                if (bWaitConfirm)
                {
                    if (iConfirmResult != 2 && iConfirmResult != 1) return;
                    if (!string.IsNullOrEmpty(sOpenUrl))
                    {
                        UnityEngine.Application.OpenURL(sOpenUrl);
                        sOpenUrl = "";
                    }
                    WndForm.QuitApp();
                    return;
                }
                next = 0xb;
                break;
            }

            case 0xb:
            {
                WndForm_LoadingScreen.fProgress = 0.25f;
                var abm = AssetBundleManager.Instance;
                if (abm == null) return;
                abm.InitResourceMainManifest();
                next = 0xc;
                break;
            }

            case 0xc:
            {
                var abm = AssetBundleManager.Instance;
                if (abm == null) return;
                if (!abm.IsResourceReady()) return;
                next = 0xd;
                break;
            }

            case 0xd:
            {
                WndForm_LoadingScreen.fProgress = 0.30f; // DAT_0091c090
                _startDownTime = UnityEngine.Time.time;
                fLastUpdateTime = UnityEngine.Time.time;
                // Original: clears the cost-log List<string> at offset 0xb0 — skip (we don't maintain).
                SetBarTextID(1);
                var abm = AssetBundleManager.Instance;
                if (abm == null) return;
                abm.InitMainManifest();
                next = 0xe;
                break;
            }

            case 0xe:
            {
                var abm = AssetBundleManager.Instance;
                if (abm == null) return;
                if (!abm.IsReady()) return;
                next = 0xf;
                break;
            }

            case 0xf:
                WndForm_LoadingScreen.fProgress = 0.40f; // DAT_0091c128
                TakeCostTime((UpdateStep)0x12, "MainBundle");
                LoadXMLStep(0, out LoadVersionState, LoadVersion(), (DownloadStep)0x10);
                return;

            case 0x10:
                if (!CheckCoroutineDone(LoadVersionState, "==== Error, LoadVersionState Error ====")) return;
                _stepDownload = (DownloadStep)0x17;
                return;

            case 0x11:
            {
                WndForm_LoadingScreen.fProgress = 0.75f;
                TakeCostTime((UpdateStep)0x12, "Version");
                if (NeedUpdateDatas == null) NeedUpdateDatas = new System.Collections.Generic.Dictionary<string, int>();
                else NeedUpdateDatas.Clear();
                if (NeedLoadDatas == null) NeedLoadDatas = new System.Collections.Generic.Dictionary<string, int>();
                else NeedLoadDatas.Clear();
                LoadBundleSize(3, out LoadBundleSizeState, LoadBundleSize(), (DownloadStep)0x12);
                return;
            }

            case 0x12:
                if (!CheckCoroutineDone(LoadBundleSizeState, "==== Error, LoadBundleSizeState Error ====")) return;
                _stepDownload = (DownloadStep)0x13;
                return;

            case 0x13:
            {
                WndForm_LoadingScreen.fProgress = 1.0f;
                bWaitConfirm = false;
                sOpenUrl = "";
                _DownloadedCount = 0;
                _stepDownloadNext = (DownloadStep)0x14;
                if (Main.bLoadBundle)
                {
                    var bhc = BundleHashChecker.Instance;
                    if (bhc != null)
                    {
                        bhc.CheckAllNeedDownloadFiles(NeedUpdateDatas, NeedLoadDatas);
                        float total = bhc.TotalNeedBundleSize;
                        if (total > 0f)
                        {
                            bWaitConfirm = true;
                            iConfirmResult = 0;
                            var ls = WndForm_LoadingScreen.Instance;
                            if (ls != null)
                            {
                                ls.ShowDownloadCheck(total, 0x19, 0x1a);
                            }
                        }
                    }
                }
                next = 1;
                break;
            }

            case 0x14:
            {
                if (Main.bLoadBundle && bWaitConfirm)
                {
                    if (iConfirmResult == 2) { WndForm.QuitApp(); return; }
                    if (iConfirmResult != 1) return;
                    WndForm_LoadingScreen.fProgress = 0f;
                    _stepDownload = (DownloadStep)0x1a;
                    return;
                }
                WndForm_LoadingScreen.fProgress = 0f;
                next = 0x1a;
                break;
            }

            case 0x16:
            {
                if (Main.bLoadBundle && bWaitConfirm && iConfirmResult != 1)
                {
                    if (iConfirmResult != 2) return;
                    WndForm.QuitApp();
                    return;
                }
                next = 0x1a;
                break;
            }

            case 0x17:
                WndForm_LoadingScreen.fProgress = 0.65f; // DAT_0091c294
                TakeCostTime((UpdateStep)0x12, "MainBundle");
                LoadXMLStep(0, out LoadServerListState, LoadServerList(), (DownloadStep)0x18);
                return;

            case 0x18:
                if (!CheckCoroutineDone(LoadServerListState, "==== Error, LoadServerListState Error ====")) return;
                _stepDownload = (DownloadStep)0x11;
                return;

            // ──────────────────────────────────────────────────────────────────────
            // Bundle pairs: each (load_state, check_state) downloads one bundle then
            // moves on. Bundle name + callback come from per-state literals/delegates.
            // ──────────────────────────────────────────────────────────────────────
            case 0x1a:
            {
                TakeCostTime((UpdateStep)0x12, "Lua Script");
                var rm = ResMgr.Instance;
                string luaName = rm != null ? rm.GetLuaBundleName() : "unload64";
                AssetBundleManager.CBAssetBundle cb = LoadLuaScriptBundleCB;
                GetBundle(luaName, "==== Error, LuaScript {0} ====", (DownloadStep)0x1b, cb, false, false);
                return;
            }
            case 0x1b:
            {
                var rm = ResMgr.Instance;
                string luaName = rm != null ? rm.GetLuaBundleName() : "unload64";
                CheckBundleLoad("==== Error, No Lua Script Bundle ====", luaName, 0x12d, 0x12e, (DownloadStep)0x1c);
                return;
            }

            case 0x1c:
                TakeCostTime((UpdateStep)0x12, "font");
                GetBundle("font", "==== Error, Font {0} ====", (DownloadStep)0x1d, LoadFontBundleCB, false, false);
                return;
            case 0x1d:
                CheckBundleLoad("==== Error, No customfont Bundle ====", "font", 0x12f, 0x130, (DownloadStep)0x1e);
                return;

            case 0x1e:
                TakeCostTime((UpdateStep)0x12, "uishared");
                GetBundle("uishared", "==== Error, MainUI {0} ====", (DownloadStep)0x1f, LoadMainUIBundleCB, false, false);
                return;
            case 0x1f:
                CheckBundleLoad("==== Error, No uishared Bundle ====", "uishared", 0x131, 0x132, (DownloadStep)0x20);
                return;

            case 0x20:
                TakeCostTime((UpdateStep)0x12, "ItemIcon");
                GetBundle("itemicon", "==== Error, ItemIcon {0} ====", (DownloadStep)0x21, LoadItemIconBundleCB, false, false);
                return;
            case 0x21:
                CheckBundleLoad("==== Error, No itemicon Bundle ====", "itemicon", 0x135, 0x136, (DownloadStep)0x22);
                return;

            case 0x22:
                TakeCostTime((UpdateStep)0x12, "HeadIcon");
                GetBundle("headicon", "==== Error, HeadIcon {0} ====", (DownloadStep)0x23, LoadHeadIconBundleCB, false, false);
                return;
            case 0x23:
                CheckBundleLoad("==== Error, No headicon Bundle ====", "headicon", 0x135, 0x136, (DownloadStep)0x24);
                return;

            case 0x24:
                TakeCostTime((UpdateStep)0x12, "SkillIcon");
                GetBundle("skillicon", "==== Error, SkillIcon {0} ====", (DownloadStep)0x25, LoadSkillIconBundleCB, false, false);
                return;
            case 0x25:
                CheckBundleLoad("==== Error, No skillicon Bundle ====", "skillicon", 0x135, 0x136, (DownloadStep)0x26);
                return;

            case 0x26:
                TakeCostTime((UpdateStep)0x12, "FX");
                GetBundle("fx", "==== Error, FX {0} ====", (DownloadStep)0x27, LoadFXBundleCB, false, false);
                return;
            case 0x27:
                CheckBundleLoad("==== Error, No fx Bundle ====", "fx", 0x139, 0x13a, (DownloadStep)0x28);
                return;

            case 0x28:
                TakeCostTime((UpdateStep)0x12, "CreateChar");
                GetBundle("createcharsprm", "==== Error, CreateChar {0} ====", (DownloadStep)0x29, LoadCreateCharBundleCB, false, false);
                return;
            case 0x29:
                CheckBundleLoad("==== Error, No createcharsprm Bundle ====", "createcharsprm", 0x139, 0x13a, (DownloadStep)0x2a);
                return;

            case 0x2a:
                TakeCostTime((UpdateStep)0x12, "SMap");
                GetBundle("smap", "==== Error, SMap {0} ====", (DownloadStep)0x2b, LoadSMapBundleCB, false, false);
                return;
            case 0x2b:
                CheckBundleLoad("==== Error, No smap Bundle ====", "smap", 0x13d, 0x13e, (DownloadStep)0x2c);
                return;

            case 0x2c:
                TakeCostTime((UpdateStep)0x12, "MapData");
                GetBundle("mapdata", "==== Error, MapData {0} ====", (DownloadStep)0x2d, LoadMapDataBundleCB, false, false);
                return;
            case 0x2d:
                CheckBundleLoad("==== Error, No MapData Bundle ====", "mapdata", 0x13d, 0x13e, (DownloadStep)0x2e);
                return;

            case 0x2e:
                TakeCostTime((UpdateStep)0x12, "Sound");
                GetBundle("sound", "==== Error, Sound {0} ====", (DownloadStep)0x2f, LoadSoundBundleCB, false, false);
                return;
            case 0x2f:
                CheckBundleLoad("==== Error, No Sound Bundle ====", "sound", 0x13b, 0x13c, (DownloadStep)0x30);
                return;

            case 0x30:
                TakeCostTime((UpdateStep)0x12, "UIFX");
                GetBundle("uifx", "==== Error, UIFX {0} ====", (DownloadStep)0x31, LoadUIFXBundleCB, false, false);
                return;
            case 0x31:
                CheckBundleLoad("==== Error, No UIFX Bundle ====", "uifx", 0x13f, 0x140, (DownloadStep)0x32);
                return;

            case 0x32:
                TakeCostTime((UpdateStep)0x12, "MagicData");
                GetBundle("magic/data", "==== Error, MagicData {0} ====", (DownloadStep)0x33, LoadMagicDataBundleCB, false, false);
                return;
            case 0x33:
                CheckBundleLoad("==== Error, No MagicData Bundle ====", "magic/data", 0x13f, 0x140, (DownloadStep)0x34);
                return;

            case 0x34:
                TakeCostTime((UpdateStep)0x12, "MagicFx");
                GetBundle("magic/fx", "==== Error, MagicFx {0} ====", (DownloadStep)0x35, LoadMagicFxBundleCB, false, false);
                return;
            case 0x35:
                CheckBundleLoad("==== Error, No MagicFx Bundle ====", "magic/fx", 0x13f, 0x140, (DownloadStep)0x36);
                return;

            case 0x36:
                TakeCostTime((UpdateStep)0x12, "CardIcon");
                GetBundle("cardicon", "==== Error, CardIcon {0} ====", (DownloadStep)0x37, LoadCardIconBundleCB, false, false);
                return;
            case 0x37:
                CheckBundleLoad("==== Error, No cardicon Bundle ====", "cardicon", 0x135, 0x136, (DownloadStep)0x38);
                return;

            case 0x38:
                TakeCostTime((UpdateStep)0x12, "Emoji");
                GetBundle("emoji", "==== Error, Emoji {0} ====", (DownloadStep)0x39, LoadEmojiBundleCB, false, false);
                return;
            case 0x39:
                CheckBundleLoad("==== Error, No emoji Bundle ====", "emoji", 0x135, 0x136, (DownloadStep)0x3a);
                return;

            // ──────────────────────────────────────────────────────────────────────
            // Folder bundles: each downloads ALL bundles in a directory.
            // ──────────────────────────────────────────────────────────────────────
            case 0x3a:
            {
                TakeCostTime((UpdateStep)0x12, "Scene");
                GetBundleFolder(ResourcesPath.ScenePath, "==== Error, No scene {0} Bundle ====", (DownloadStep)0x3b);
                return;
            }
            case 0x3b:
                CheckBundleLoadFolder("==== Error, No scene {0} Bundle ====", "==== Error, Scene {0} {1} ====", (DownloadStep)0x3c);
                return;

            case 0x3c:
                TakeCostTime((UpdateStep)0x12, "Model");
                GetBundleFolder(ResourcesPath.ModelPath, "==== Error, No model {0} Bundle ====", (DownloadStep)0x3d);
                return;
            case 0x3d:
                CheckBundleLoadFolder("==== Error, No model Bundle ====", "==== Error, model {0} {1} ====", (DownloadStep)0x3e);
                return;

            case 0x3e:
                TakeCostTime((UpdateStep)0x12, "Music");
                GetBundleFolder(ResourcesPath.MusicPath, "==== Error, No music {0} Bundle ====", (DownloadStep)0x3f);
                return;
            case 0x3f:
                CheckBundleLoadFolder("==== Error, No music Bundle ====", "==== Error, music {0} {1} ====", (DownloadStep)0x40);
                return;

            case 0x40:
                TakeCostTime((UpdateStep)0x12, "Menu");
                GetBundleFolder(ResourcesPath.MenusPath, "==== Error, No Menu {0} Bundle ====", (DownloadStep)0x41);
                return;
            case 0x41:
                CheckBundleLoadFolder("==== Error, No Menu Bundle ====", "==== Error, Menu {0} {1} ====", (DownloadStep)0x42);
                return;

            case 0x42:
            {
                TakeCostTime((UpdateStep)0x12, "Cost {0:000.00} Sec In ProcessUpdate:Total");
                UnityEngine.Debug.LogWarning("========================================");
                // Log all cost entries from offset 0xb0 list — skipped (we log inline).
                if (Main.bLoadBundle)
                {
                    var bhc = BundleHashChecker.Instance;
                    var cm = ConfigMgr.Instance;
                    if (bhc != null && cm != null)
                    {
                        string hashStr = bhc.ConvertBundleHashToString();
                        cm.SetConfigVarStr(0xb, hashStr);
                        cm.ConfigVarSave();
                    }
                }
                _request = null;
                _stepUpdate = (UpdateStep)0x13; // V_UpdateLoading
                return;
            }
        }
        _stepDownload = (DownloadStep)next;
    }

    /* RVA 0x01900eb4 — V_UpdateLoading: 5-state finalisation after all bundles loaded. 1-1 port from Ghidra.
     * State register: _stepLoading (offset 0x34). _stepLoadingNext (0x38) used like _stepDownloadNext.
     *
     * State map:
     *   0       SetBarTextID(0x138) + reset cost timer + clear cost log → 2
     *   1       Wait LoadingScreen.Completed → copy stepLoadingNext → stepLoading
     *   2       fProgress=0 + fLastLoadingTime = Time.time → 3
     *   3       Two paths based on Main.bLoadLuaBundle (offset 0x11):
     *           - false: SetBarTextID(0xb)
     *           - true (1st pass): bLuaReady = ResMgr.LoadLuaScript() (set the bool, return)
     *           - true (2nd pass, !ResMgr.bFileListReady): ResMgr.ProcessFileList → on done set bFileListReady → Main.StartLuaScriptMgr
     *           - true (3rd pass, all): IsLoadLuaFinish → SetBarText to "Loading scripts X/Y"
     *           After Lua done: ls.fProgress = 100 + Main.StartLuaScriptMgr (if bLoadLuaBundle off + luaScriptMrgCount<5)
     *           Move to state DAT_008e33d0 (= state 4 in original)
     *   4       Final: TakeCostTime + CallMethod (Lua callback to "ProcessLunchGame:CompletedLoading"?) + CProcManager.SwitchProc(3)
     */
    protected void V_UpdateLoading(float dTime)
    {
        int step = (int)_stepLoading;
        int next = step;
        switch (step)
        {
            case 0:
            {
                SetBarTextID(0x138, 0);
                fLastLoadingTime = UnityEngine.Time.time;
                // clear cost log at offset 0xc0 — skip
                next = 2;
                break;
            }

            case 1:
                if (!WndForm_LoadingScreen.Completed) return;
                if ((int)_stepLoadingNext == 0) return;
                _stepLoading = _stepLoadingNext;
                _stepLoadingNext = (LoadingStep)0;
                return;

            case 2:
                WndForm_LoadingScreen.fProgress = 0f;
                fLastLoadingTime = UnityEngine.Time.time;
                next = 3;
                break;

            case 3:
            {
                if (!Main.bLoadLuaBundle)
                {
                    // Editor (or no Lua bundle) path — just bar text
                    SetBarTextID(0xb, 0);
                }
                else
                {
                    var rm = ResMgr.Instance;
                    if (rm == null) return;
                    if (!bLuaReady)
                    {
                        bLuaReady = rm.LoadLuaScript();
                        return;
                    }
                    if (!rm.bFileListReady)
                    {
                        if (!rm.ProcessFileList()) return;
                        rm.bFileListReady = true;
                        var main = Main.Instance;
                        if (main != null) main.StartLuaScriptMgr();
                        return;
                    }
                    if (bBaseTextLoaded) return; // (offset 0x100) — wait until base text shown
                    int iCur, iMax;
                    if (!rm.IsLoadLuaFinish(out iCur, out iMax))
                    {
                        string fmt = rm.GetBasicUIText(0xc);
                        WndForm_LoadingScreen.SetBarText(string.Format(fmt, iCur, iMax), "");
                        bBaseTextLoaded = true;
                        return;
                    }
                    bLuaReady = false;
                }
                var ls = WndForm_LoadingScreen.Instance;
                if (ls != null) ls._ShowPercent = 100f; // (offset 0x174)
                if (!Main.bLoadLuaBundle && luaScriptMrgCount < 5)
                {
                    var main = Main.Instance;
                    if (main != null) main.StartLuaScriptMgr();
                }
                _stepLoading = (LoadingStep)4; // DAT_008e33d0
                return;
            }

            case 4:
            {
                /* RVA 0x019019fc..0x01a01a8c — V_UpdateLoading case 4 final step.
                 * 1-1 port from Ghidra V_UpdateLoading.c lines 235-430:
                 *   - Telemetry: TakeCostTime + iterate _costLog → log each entry, then "Cost {dt} Sec In ProcessLoading:Total"
                 *   - Gate: if bSwitchProc already true → return
                 *   - SetRegion: if SGCRegion.ver_useRegionFlag → Util.CallMethod("AccountClientData", "SetRegion", new object[] { region })
                 *   - **C#→Lua bridge**: iterate ResMgr.Instance.serverListData →
                 *       Util.CallMethod("AccountClientData", "AddServerList", new object[] { *         id, real_server_id, name, ip, port, status, recommend, merge_server_id, regionID })
                 *   - SwitchProc(EProcID.ProcessLoginGame=3) (single-frame op — not async)
                 */
                if (bSwitchProc) return;

                TakeCostTime((UpdateStep)0x13, "Lua Script");
                UnityEngine.Debug.LogWarning("========================================");
                // Skip cost log iteration (not maintained in C# port — telemetry only)
                float totalDt = UnityEngine.Time.time - fLastLoadingTime;
                UnityEngine.Debug.LogWarningFormat("Cost {0:000.00} Sec In ProcessLoading:Total", totalDt);
                UnityEngine.Debug.LogWarning("========================================");

                if (SGCRegion.ver_useRegionFlag)
                {
                    var cm = ConfigMgr.Instance;
                    if (cm != null)
                    {
                        int region = cm.GetConfigVarInt(0xd);
                        LuaFramework.Util.CallMethod("AccountClientData", "SetRegion", new object[] { region });
                    }
                }

                var rm = ResMgr.Instance;
                if (rm != null && rm.serverListData != null)
                {
                    for (int i = 0; i < rm.serverListData.Count; i++)
                    {
                        var sd = rm.serverListData[i];
                        if (sd == null) continue;
                        // arg order matches AccountClientData.AddServerList signature 1-1:
                        //   (argID, argServerID, argName, argIP, argPort, argStatus, argRecommend, argMergeServerIDs, argRegionID)
                        LuaFramework.Util.CallMethod("AccountClientData", "AddServerList", new object[] {
                            sd.id,             // +0x10 "id"
                            sd.realServerID,   // +0x14 "real_server_id"
                            sd.name,           // +0x18 "name" (overridden by name_CN/name_EN)
                            sd.ip,             // +0x20 "ip"
                            sd.port,           // +0x28 "port"
                            sd.status,         // +0x38 "status"
                            sd.recommend,      // +0x3c "recommend"
                            sd.mergeServerID,  // +0x40 "merge_server_id"
                            sd.regionID        // +0x30 "regionID"
                        });
                    }
                }

                bSwitchProc = true;
                UnityEngine.Debug.Log("[DIAG] V_UpdateLoading case 4 reached — calling SwitchProc(ProcessLoginGame). _wnd=" + (_wnd != null ? "wID=" + _wnd.wID : "<null>"));
                var pm = GameProcMgr.Instance;
                if (pm != null) pm.SwitchProc(EProcID.ProcessLoginGame, false);
                return;
            }
        }
        _stepLoading = (LoadingStep)next;
    }

    /* RVA 0x01902f14 — TakeCostTime: telemetry. Records "Cost {time:000.00} Sec In ProcessUpdate:{msg}"
     *   into a List<string> at offset 0xb0 (UpdateStep=0x12) or 0xc0 (LoadingStep=0x13). Resets timer.
     *   Editor port: log to Debug.Log instead of maintaining the list (the list is for in-game log viewer).
     */
    private void TakeCostTime(UpdateStep Step, string sMsg)
    {
        int s = (int)Step;
        if (s == 0x13)
        {
            float dt = UnityEngine.Time.time - fLastLoadingTime;
            UnityEngine.Debug.Log(string.Format("Cost {0:000.00} Sec In ProcessLoading:{1}", dt, sMsg));
            fLastLoadingTime = UnityEngine.Time.time;
        }
        else if (s == 0x12)
        {
            float dt = UnityEngine.Time.time - fLastUpdateTime;
            UnityEngine.Debug.Log(string.Format("Cost {0:000.00} Sec In ProcessUpdate:{1}", dt, sMsg));
            fLastUpdateTime = UnityEngine.Time.time;
        }
    }
    /* RVA 0x019019fc — LoadXMLStep: set bar text + reset version state + StartCoroutine on Main.LuaMgr + jump to nextStep
     *   SetBarTextID(iTextID, 0)
     *   _VersionState = 0   (Loading)
     *   Main.LuaMgr.StartCoroutine(LoadingFunc)
     *   bWaitConfirm = false; iConfirmResult = 0;
     *   _stepDownload = _nextStep
     */
    private void LoadXMLStep(int iTextID, out VersionState _VersionState, IEnumerator LoadingFunc, DownloadStep _nextStep)
    {
        // Source: Ghidra ProcessLunchGame/LoadXMLStep.c RVA 0x019019FC — line 34:
        //   lVar2 = *(long *)(*(long *)(lVar2 + 0xb8) + 8);   // Main.static_fields[8] = Main.s_instance
        //   if (lVar2 != 0) MonoBehaviour.StartCoroutine(Main.s_instance, LoadingFunc);
        // Coroutine host is Main.Instance (the MonoBehaviour singleton), NOT Main.LuaMgr —
        // LuaMgr is null at this point in boot (Lua bootstrap runs LATER, after bundle dl).
        SetBarTextID(iTextID, 0);
        _VersionState = (VersionState)0;
        var host = Main.Instance;
        if (host != null && LoadingFunc != null)
        {
            UnityEngine.MonoBehaviour mb = host;
            mb.StartCoroutine(LoadingFunc);
        }
        bWaitConfirm = false;
        iConfirmResult = 0;
        _stepDownload = _nextStep;
    }

    /* RVA 0x01901ad8 — CheckCoroutineDone:
     *   if state == 1 (Done) → return true
     *   if state == 2 (Error) → LogError(sError), bWaitConfirm=true, iConfirmResult=0,
     *                            ShowConfirmCheck(0x17, 0x18), _stepDownload=10, return false
     *   else still loading → return false
     */
    private bool CheckCoroutineDone(VersionState _VersionState, string sError)
    {
        int s = (int)_VersionState;
        if (s == 1) return true;
        if (s == 2)
        {
            UnityEngine.Debug.LogError(sError);
            bWaitConfirm = true;
            iConfirmResult = 0;
            ShowConfirmCheck(0x17, 0x18);
            _stepDownload = (DownloadStep)10;
        }
        return false;
    }

    /* CheckCoroutineDoneNormal: same as CheckCoroutineDone but on Done auto-advances to _nextStep */
    private void CheckCoroutineDoneNormal(VersionState _VersionState, string sError, DownloadStep _nextStep)
    {
        if (CheckCoroutineDone(_VersionState, sError))
        {
            _stepDownload = _nextStep;
        }
    }

    /* LoadBundleSize helper — 1-1 from Ghidra RVA 0x1901BB0:
     *   SetBarTextID(iTextID);
     *   _VersionState = 0;
     *   if (Main.bLoadBundle == false) {
     *     _VersionState = 1;   // immediate Done (no download needed)
     *   } else {
     *     host = Main.s_instance; if (host == null) NRE;
     *     MonoBehaviour.StartCoroutine(host, LoadingFunc);
     *   }
     *   _stepDownload = _nextStep;
     * Coroutine host is Main.Instance (NOT Main.LuaMgr — that is null pre-Lua bootstrap).
     */
    private void LoadBundleSize(int iTextID, out VersionState _VersionState, IEnumerator LoadingFunc, DownloadStep _nextStep)
    {
        SetBarTextID(iTextID, 0);
        _VersionState = (VersionState)0;
        if (!Main.bLoadBundle)
        {
            _VersionState = (VersionState)1;
        }
        else
        {
            var host = Main.Instance;
            if (host == null) throw new System.NullReferenceException();
            UnityEngine.MonoBehaviour mb = host;
            if (LoadingFunc != null) mb.StartCoroutine(LoadingFunc);
        }
        _stepDownload = _nextStep;
    }

    /* RVA 0x01901cac — LoadAssetBundle (helper, delegates to AssetBundleManager + stores CurReq):
     *   var rf = AssetBundleManager.Instance.LoadAssetBundle(name, cb, bForceUseBundle);
     *   _request (0x40) = rf;
     *   if (rf == null) {
     *     Log(sError); bWaitConfirm=true; iConfirmResult=0; ShowConfirmCheck(0x17,0x18); _stepDownload=10;
     *   }
     *   return rf != null;
     */
    private bool LoadAssetBundle(string assetBundleName, string sError, AssetBundleManager.CBAssetBundle cbFunc, bool bForceUseBundle = false)
    {
        var abm = AssetBundleManager.Instance;
        if (abm == null) return false;
        var rf = abm.LoadAssetBundle(assetBundleName, cbFunc, bForceUseBundle);
        _request = rf;
        CurReq = rf;  // 0x40 — Ghidra reads this in CheckFakeProgress
        if (rf == null)
        {
            UnityEngine.Debug.Log(sError);
            bWaitConfirm = true;
            iConfirmResult = 0;
            ShowConfirmCheck(0x17, 0x18);
            _stepDownload = (DownloadStep)10;
            return false;
        }
        return true;
    }

    /* RVA 0x01901df0 — GetBundle:
     *   _CurrentDownloadSize = 0;
     *   if (!Main.bLoadBundle && !bForceUseBundle) { _stepDownload = nextStep; return; }
     *   _bCheckUpdate = false; (offset 0xf8)
     *   var bundleNameWithExt = AssetBundleManager.GetBundleNameWithExt(sBundleName);
     *   bool inNeed = NeedUpdateDatas.ContainsKey(bundleNameWithExt);
     *   if (bOnlyUpdate ? inNeed : !inNeed) {
     *     _CurrentDownloadSize = BundleHashChecker.GetBundleSizeinKB(bundleNameWithExt);
     *   }
     *   if (bOnlyUpdate && !inNeed) { _stepDownload = nextStep; return; } // skip not-needed
     *   _bCheckUpdate = true;
     *   if (!LoadAssetBundle(bundleNameWithExt, sError, cbFunc, bForceUseBundle)) return;
     *   _stepDownload = nextStep;
     */
    private void GetBundle(string sBundleName, string sError, DownloadStep _nextStep, AssetBundleManager.CBAssetBundle cbFunc, bool bOnlyUpdate = false, bool bForceUseBundle = false)
    {
        _CurrentDownloadSize = 0f;
        if (!Main.bLoadBundle && !bForceUseBundle)
        {
            _stepDownload = _nextStep;
            return;
        }
        _bCheckUpdate = false;
        string bundleNameWithExt = AssetBundleManager.GetBundleNameWithExt(sBundleName);
        bool inNeed = NeedUpdateDatas != null && NeedUpdateDatas.ContainsKey(bundleNameWithExt);
        if (bOnlyUpdate ? inNeed : !inNeed)
        {
            // Note: when bOnlyUpdate=true we ARE in NeedUpdate (download path) — record size.
            // when bOnlyUpdate=false we are NOT in NeedUpdate (already-downloaded path) — Ghidra still records size for already-downloaded bundle.
            var bhc = BundleHashChecker.Instance;
            if (bhc != null) _CurrentDownloadSize = bhc.GetBundleSizeinKB(bundleNameWithExt);
        }
        if (bOnlyUpdate && !inNeed)
        {
            _stepDownload = _nextStep;
            return;
        }
        _bCheckUpdate = true;
        if (!LoadAssetBundle(bundleNameWithExt, sError, cbFunc, bForceUseBundle)) return;
        _stepDownload = _nextStep;
    }

    /* RVA 0x01901f84 — CheckBundleLoad. Heavy logic; full port pending.
     * Original waits for CurReq.isDone, on done updates BundleHashChecker hash + removes from NeedUpdateDatas,
     * advances _DownloadedCount, updates loading bar progress, jumps to nextStep. On error → ShowConfirmCheck.
     */
    private void CheckBundleLoad(string sError, string sBundleName, int iDownLoadTextID, int iLoadingTextID, DownloadStep _nextStep)
    {
        // Minimal Editor-friendly path: wait for CurReq done; on done advance state.
        // TODO: full port needs RequestFile.isDone/error + BundleHashChecker.UpdateBundleHash 1-1.
        if (!Main.bLoadBundle || !_bCheckUpdate)
        {
            _stepDownload = _nextStep;
            return;
        }
        if (_request == null) { _stepDownload = _nextStep; return; }
        // [Per-frame] Update bar text + fProgress with current download progress
        // (matches Ghidra: CheckFakeProgress called before isDone check, drives smooth bar fill).
        CheckFakeProgress(iDownLoadTextID, iLoadingTextID, sBundleName, false);
        if (!_request.isDone) return;
        if (!string.IsNullOrEmpty(_request.error))
        {
            UnityEngine.Debug.LogErrorFormat(sError, _request.error);
            bWaitConfirm = true;
            iConfirmResult = 0;
            ShowConfirmCheck(0x17, 0x18);
            _stepDownload = (DownloadStep)10;
            return;
        }
        // Update hash + remove from NeedUpdateDatas
        var abm = AssetBundleManager.Instance;
        var bhc = BundleHashChecker.Instance;
        if (abm != null && bhc != null)
        {
            string bundleNameWithExt = AssetBundleManager.GetBundleNameWithExt(sBundleName);
            var hash = abm.GetBundleHash(bundleNameWithExt);
            bhc.UpdateBundleHash(bundleNameWithExt, hash);
            if (NeedUpdateDatas != null) NeedUpdateDatas.Remove(bundleNameWithExt);
        }
        CheckFakeProgress(iDownLoadTextID, iLoadingTextID, sBundleName, true);
        _stepDownload = _nextStep;
    }

    /* RVA 0x01902534 — GetBundleFolder. 1-1 from Ghidra:
     *   _CurrentDownloadSize = 0; _DownloadedSubSize = 0;
     *   if (!Main.bLoadBundle) { _stepDownload = nextStep; return; }
     *   WaitReqBundleNames = bhc.GetNeedUpdateFileList(path).ToArray();
     *   ProcessReqBundleIndex = 0;
     *   if (WaitReqBundleNames != null && WaitReqBundleNames.Length > 0) {
     *     first = WaitReqBundleNames[0];
     *     _CurrentDownloadSize = bhc.GetBundleSizeinKB(first);
     *     err = String.Format(sError, first);
     *     if (LoadAssetBundle(first, err) returned true) _stepDownload = nextStep;
     *     // else: caller re-loops next frame
     *   } else {
     *     _stepDownload = nextStep;
     *   }
     */
    private void GetBundleFolder(string sPath, string sError, DownloadStep _nextStep)
    {
        _CurrentDownloadSize = 0f;
        _DownloadedSubSize = 0f;
        if (!Main.bLoadBundle)
        {
            _stepDownload = _nextStep;
            return;
        }
        var bhc = BundleHashChecker.Instance;
        if (bhc == null) { _stepDownload = _nextStep; return; }
        WaitReqBundleNames = bhc.GetNeedUpdateFileList(sPath);
        ProcessReqBundleIndex = 0;
        if (WaitReqBundleNames == null || WaitReqBundleNames.Length == 0)
        {
            _stepDownload = _nextStep;
            return;
        }
        string firstBundle = WaitReqBundleNames[0];
        _CurrentDownloadSize = bhc.GetBundleSizeinKB(firstBundle);
        string err = string.Format(sError, firstBundle);
        bool ok = LoadAssetBundle(firstBundle, err, null, false);
        if (ok) _stepDownload = _nextStep;
    }

    /* RVA 0x01902740 — CheckBundleLoadFolder. 1-1 port from Ghidra.
     * Iterates WaitReqBundleNames (offset 0x48) by ProcessReqBundleIndex (offset 0x50).
     * For each bundle:
     *   - call CheckFakeProgress per-frame to update bar
     *   - on isDone: validate error, UpdateBundleHash, advance index, load next bundle
     * When all bundles done: SetBarTextUpdate, set fProgress=1.0, advance to nextStep.
     */
    private void CheckBundleLoadFolder(string sError1, string sError2, DownloadStep _nextStep)
    {
        if (!Main.bLoadBundle)
        {
            _stepDownload = _nextStep;
            return;
        }
        if (WaitReqBundleNames == null || WaitReqBundleNames.Length == 0)
        {
            _stepDownload = _nextStep;
            return;
        }
        // Map current state to bar text ID (Ghidra switch on _stepDownload):
        //   0x3b GetSceneBundle  → text ID 0x141
        //   0x3d GetModelBundle  → text ID 0x139
        //   0x3f GetMusicBundle  → text ID 0x143
        //   0x41 GetMenusBundle  → text ID 0x145
        int curStep = (int)_stepDownload;
        int loadingTextID = 0;
        switch (curStep)
        {
            case 0x3b: loadingTextID = 0x141; break;
            case 0x3d: loadingTextID = 0x139; break;
            case 0x3f: loadingTextID = 0x143; break;
            case 0x41: loadingTextID = 0x145; break;
        }

        // If WaitReqBundleNames already exhausted, finalize this folder
        if (ProcessReqBundleIndex >= WaitReqBundleNames.Length)
        {
            // Final: bar = 100%, advance bundleCount, advance to nextStep
            if (_CurrentDownloadSize > 0f)
            {
                PlusDownloadedCount();
                _DownloadedSize += _CurrentDownloadSize;
                SetBarTextUpdate(2, 4);
            }
            var bhc0 = BundleHashChecker.Instance;
            if (bhc0 != null && bhc0.NeedDownloadCount == 0)
            {
                WndForm_LoadingScreen.fProgress = 1.0f;
            }
            _stepDownload = _nextStep;
            return;
        }

        var req = _request;
        if (req == null) { _stepDownload = _nextStep; return; }

        // Per-frame: not done yet → update bar progress + text and return
        if (!req.isDone)
        {
            // Internet check
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                bWaitConfirm = true;
                iConfirmResult = 0;
                ShowConfirmCheck(0x17, 0x18);
                _stepDownload = (DownloadStep)10;
                return;
            }
            if (_CurrentDownloadSize > 0f)
            {
                float prog = req.progress;
                var bhc1 = BundleHashChecker.Instance;
                if (bhc1 != null)
                {
                    float total = bhc1.TotalNeedBundleSize;
                    if (total <= 0f) total = 1f;
                    WndForm_LoadingScreen.fProgress = (_DownloadedSize + _CurrentDownloadSize * prog) / total;
                    if (loadingTextID != 0) SetBarTextUpdate(loadingTextID, 4);
                }
            }
            return;
        }

        // Bundle finished — check error
        string errorMsg = req.error;
        string curName = WaitReqBundleNames[ProcessReqBundleIndex];
        if (!string.IsNullOrEmpty(errorMsg))
        {
            UnityEngine.Debug.LogErrorFormat(sError2, curName, errorMsg);
#if UNITY_EDITOR
            // [Editor retry] Transient UnityWebRequestAssetBundle "Cannot connect to destination host"
            // happens occasionally on macOS Editor with rapid sequential local-CDN requests (HTTP/1.0
            // close-per-request can cause sporadic TCP refused). Retry a few times before bailing.
            // Production iOS uses real CDN over HTTP/2 keep-alive — retries less critical there.
            const int kEditorRetryMax = 3;
            if (errorMsg.Contains("Cannot connect") || errorMsg.Contains("Connection") || errorMsg.Contains("timeout"))
            {
                _editorRetryCount++;
                if (_editorRetryCount <= kEditorRetryMax)
                {
                    UnityEngine.Debug.LogWarningFormat("[Editor retry {0}/{1}] {2} — re-issuing", _editorRetryCount, kEditorRetryMax, curName);
                    _request = null;  // force re-create on next tick
                    return;
                }
                _editorRetryCount = 0;  // reset for next bundle
            }
#endif
            bWaitConfirm = true;
            iConfirmResult = 0;
            ShowConfirmCheck(0x17, 0x18);
            _stepDownload = (DownloadStep)10;
            return;
        }
        // Successful path — reset retry count
#if UNITY_EDITOR
        _editorRetryCount = 0;
#endif

        // Update hash + remove from NeedUpdateDatas
        var abm = AssetBundleManager.Instance;
        var bhc = BundleHashChecker.Instance;
        if (abm != null && bhc != null)
        {
            var hash = abm.GetBundleHash(curName);
            bhc.UpdateBundleHash(curName, hash);
        }
        if (NeedUpdateDatas != null) NeedUpdateDatas.Remove(curName);
        ProcessReqBundleIndex++;

        // Last bundle in folder?
        if (ProcessReqBundleIndex >= WaitReqBundleNames.Length)
        {
            if (_CurrentDownloadSize > 0f)
            {
                PlusDownloadedCount();
                _DownloadedSize += _CurrentDownloadSize;
                SetBarTextUpdate(2, 4);
            }
            _stepDownload = _nextStep;
            return;
        }

        // More bundles — load next.
        // Per Ghidra: 64-bit write to (offset 0xEC, 0xF0) adds _CurrentDownloadSize
        // to BOTH _DownloadedSize AND _DownloadedSubSize.
        if (_CurrentDownloadSize > 0f)
        {
            _DownloadedSize += _CurrentDownloadSize;
            _DownloadedSubSize += _CurrentDownloadSize;
            if (loadingTextID != 0) SetBarTextUpdate(loadingTextID, 4);
        }
        string nextBundle = WaitReqBundleNames[ProcessReqBundleIndex];
        if (bhc != null) _CurrentDownloadSize = bhc.GetBundleSizeinKB(nextBundle);
        string err = string.Format(sError1, nextBundle);
        LoadAssetBundle(nextBundle, err, null, false);
    }

    private void CheckBundleLoadFolder_OldStub(string sError1, string sError2, DownloadStep _nextStep)
    {
        if (!Main.bLoadBundle)
        {
            _stepDownload = _nextStep;
            return;
        }
        if (WaitReqBundleNames == null || WaitReqBundleNames.Length == 0)
        {
            _stepDownload = _nextStep;
            return;
        }
        if (_request == null) { _stepDownload = _nextStep; return; }
        if (!_request.isDone) return;
        if (!string.IsNullOrEmpty(_request.error))
        {
            bWaitConfirm = true;
            iConfirmResult = 0;
            ShowConfirmCheck(0x17, 0x18);
            _stepDownload = (DownloadStep)10;
            return;
        }
        // Update hash for current bundle, advance index
        var abm = AssetBundleManager.Instance;
        var bhc = BundleHashChecker.Instance;
        if (abm != null && bhc != null && ProcessReqBundleIndex < WaitReqBundleNames.Length)
        {
            string curBundle = WaitReqBundleNames[ProcessReqBundleIndex];
            var hash = abm.GetBundleHash(curBundle);
            bhc.UpdateBundleHash(curBundle, hash);
            if (NeedUpdateDatas != null) NeedUpdateDatas.Remove(curBundle);
        }
        ProcessReqBundleIndex++;
        if (ProcessReqBundleIndex >= WaitReqBundleNames.Length)
        {
            _stepDownload = _nextStep;
            return;
        }
        // Load next
        string nextBundle = WaitReqBundleNames[ProcessReqBundleIndex];
        if (bhc != null) _CurrentDownloadSize = bhc.GetBundleSizeinKB(nextBundle);
        LoadAssetBundle(nextBundle, sError1, null, false);
    }

    /* RVA 0x01907db8 — <LoadSGCInitSettings>d__91.MoveNext (1-1 port as IEnumerator).
     *   URL = ResourcesPath.PatchHost + "SGCInitSettings.xml" formatted "{0}?v={1}" with NowTimeStamp.
     *   UnityWebRequest.Get → yield SendWebRequest → on done: if error, state=2; else trim text, ParsingSGCInitSettings.
     */
    private IEnumerator LoadSGCInitSettings()
    {
        const string FILE = "SGCInitSettings.xml";
        int ts = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string baseUrl = ResourcesPath.PatchHost + FILE;
        string url = string.Format("{0}?v={1}", baseUrl, ts);
        UnityEngine.Debug.LogFormat("LoadSGCInitSettings Path-> {0}", url);
        var req = UnityEngine.Networking.UnityWebRequest.Get(url);
        if (req == null) yield break;
        yield return req.SendWebRequest();
        if (!string.IsNullOrEmpty(req.error))
        {
            UnityEngine.Debug.Log("==== LoadSGCInitSettings Error ==== Error: " + req.error);
            LoadSGCInitSettingsState = (VersionState)2;
            yield break;
        }
        var dl = req.downloadHandler;
        if (dl == null) yield break;
        // Optional: write debug copy if EN_DEBUG=="1"
        var cm = ConfigMgr.Instance;
        if (cm != null && cm.GetConfigVarStrbyStr("EN_DEBUG") == "1")
        {
            // BaseProcLua.WriteFile(FILE, dl.text); — TODO when ported
        }
        string text = dl.text;
        if (string.IsNullOrEmpty(text)) yield break;
        text = text.Trim();
        bool ok = ParsingSGCInitSettings(text);
        LoadSGCInitSettingsState = (VersionState)(ok ? 1 : 2);
    }

    /* RVA 0x019033d8 — ParsingSGCInitSettings:
     *   bool ok = ResMgr.Instance.ParsingSGCInitSettings(text);
     *   if (!ok) return false;
     *   string marsUrl = ResMgr.Instance.GetSGCInitSettings("mars", "serviceUrl");
     *   LogFormat("SGC Mars Service URL : {0}", marsUrl);
     *   return true;
     */
    private bool ParsingSGCInitSettings(string text)
    {
        var rm = ResMgr.Instance;
        if (rm == null) return false;
        bool ok = rm.ParsingSGCInitSettings(text);
        if (!ok) return false;
        string marsUrl = rm.GetSGCInitSettings("mars", "serviceUrl");
        UnityEngine.Debug.LogFormat("SGC Mars Service URL : {0}", marsUrl);
        return true;
    }

    /* RVA 0x019082f8 — <LoadPatchList>d__93.MoveNext (1-1 port as IEnumerator).
     * Same shape: fetch PatchHostList.xml + ParsingPatchList. */
    private IEnumerator LoadPatchList()
    {
        const string FILE = "PatchHostList.xml";
        int ts = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string baseUrl = ResourcesPath.PatchHost + FILE;
        string url = string.Format("{0}?v={1}", baseUrl, ts);
        UnityEngine.Debug.LogFormat("LoadPatchList Path-> {0}", url);
        var req = UnityEngine.Networking.UnityWebRequest.Get(url);
        if (req == null) yield break;
        yield return req.SendWebRequest();
        if (!string.IsNullOrEmpty(req.error))
        {
            UnityEngine.Debug.Log("==== LoadPatchList Error ==== Error: " + req.error);
            LoadPatchListState = (VersionState)2;
            yield break;
        }
        var dl = req.downloadHandler;
        if (dl == null) yield break;
        string text = dl.text;
        if (string.IsNullOrEmpty(text)) yield break;
        text = text.Trim();
        bool ok = ParsingPatchList(text);
        LoadPatchListState = (VersionState)(ok ? 1 : 2);
        UnityEngine.Debug.Log("[LoadPatchList] ParsingPatchList returned " + ok + " (state=" + (int)LoadPatchListState + "). PatchHostAndroid=" + (ResourcesPath.PatchData != null ? ResourcesPath.PatchData.PatchHostAndroid : "<null>"));
    }

    /* RVA 0x019035c4 — ParsingPatchList. 1-1 port preserved from prior session. */
    private bool ParsingPatchList(string text)
    {
        var doc = new System.Xml.XmlDocument();
        doc.LoadXml(text);

        // //Patch — populate _patchData (no Version* attributes here)
        var patchNodes = doc.SelectNodes("//Patch");
        if (patchNodes != null)
        {
            foreach (System.Xml.XmlNode node in patchNodes)
            {
                if (node.Attributes == null || node.Attributes.Count == 0) continue;
                if (!node.Attributes[0].Specified) continue;
                var data = ResourcesPath.PatchData;
                if (data == null) continue;
                if (node.Attributes["AndroidBundleAddress"] != null)
                    data.PatchHostAndroid = node.Attributes["AndroidBundleAddress"].Value;
                if (node.Attributes["AndroidVersionAddress"] != null)
                    data.PatchAndroidVersion = node.Attributes["AndroidVersionAddress"].Value;
                if (node.Attributes["IOSBundleAddress"] != null)
                    data.PatchHostIOS = node.Attributes["IOSBundleAddress"].Value;
                if (node.Attributes["IOSVersionAddress"] != null)
                    data.PatchIOSVersion = node.Attributes["IOSVersionAddress"].Value;
                if (node.Attributes["PCBundleAddress"] != null)
                    data.PatchHostPC = node.Attributes["PCBundleAddress"].Value;
                if (node.Attributes["PCVersionAddress"] != null)
                    data.PatchPCVersion = node.Attributes["PCVersionAddress"].Value;
                if (node.Attributes["AnnouncementAddress"] != null)
                    data.PatchAnnounce = node.Attributes["AnnouncementAddress"].Value;
                if (node.Attributes["DefineVersionAddress"] != null)
                    data.PatchDefineVersion = node.Attributes["DefineVersionAddress"].Value;
                if (node.Attributes["serverlistAddress"] != null)
                    data.PatchServerList = node.Attributes["serverlistAddress"].Value;
                if (node.Attributes["serverlistVersion"] != null)
                    data.PatchServerListVersion = node.Attributes["serverlistVersion"].Value;
            }
        }

        // //PatchPreview — populate _patchDataPreview (full + Versions)
        var previewNodes = doc.SelectNodes("//PatchPreview");
        if (previewNodes != null)
        {
            foreach (System.Xml.XmlNode node in previewNodes)
            {
                if (node.Attributes == null || node.Attributes.Count == 0) continue;
                if (!node.Attributes[0].Specified) continue;
                var data = ResourcesPath.PatchDataPreview;
                if (data == null) continue;
                if (node.Attributes["VersionAndroid"] != null) data.VersionAndroid = node.Attributes["VersionAndroid"].Value;
                if (node.Attributes["AndroidBundleAddress"] != null) data.PatchHostAndroid = node.Attributes["AndroidBundleAddress"].Value;
                if (node.Attributes["AndroidVersionAddress"] != null) data.PatchAndroidVersion = node.Attributes["AndroidVersionAddress"].Value;
                if (node.Attributes["VersionIOS"] != null) data.VersionIOS = node.Attributes["VersionIOS"].Value;
                if (node.Attributes["IOSBundleAddress"] != null) data.PatchHostIOS = node.Attributes["IOSBundleAddress"].Value;
                if (node.Attributes["IOSVersionAddress"] != null) data.PatchIOSVersion = node.Attributes["IOSVersionAddress"].Value;
                if (node.Attributes["VersionPC"] != null) data.VersionPC = node.Attributes["VersionPC"].Value;
                if (node.Attributes["PCBundleAddress"] != null) data.PatchHostPC = node.Attributes["PCBundleAddress"].Value;
                if (node.Attributes["PCVersionAddress"] != null) data.PatchPCVersion = node.Attributes["PCVersionAddress"].Value;
                if (node.Attributes["AnnouncementAddress"] != null) data.PatchAnnounce = node.Attributes["AnnouncementAddress"].Value;
                if (node.Attributes["DefineVersionAddress"] != null) data.PatchDefineVersion = node.Attributes["DefineVersionAddress"].Value;
                if (node.Attributes["serverlistAddress"] != null) data.PatchServerList = node.Attributes["serverlistAddress"].Value;
                if (node.Attributes["serverlistVersion"] != null) data.PatchServerListVersion = node.Attributes["serverlistVersion"].Value;

                if (ResourcesPath.IsPreviewVersion() && ResourcesPath.PatchData != null)
                {
                    ResourcesPath.PatchData.VersionAndroid = "https://sgc-cdn.uj.com.tw/patch/Preview/";
                }
            }
        }

        // //PatchOld — populate _patchDataOld (Version* + *VersionAddress only)
        var oldNodes = doc.SelectNodes("//PatchOld");
        if (oldNodes != null)
        {
            foreach (System.Xml.XmlNode node in oldNodes)
            {
                if (node.Attributes == null || node.Attributes.Count == 0) continue;
                if (!node.Attributes[0].Specified) continue;
                var data = ResourcesPath.PatchDataOld;
                if (data == null) continue;
                if (node.Attributes["VersionAndroid"] != null) data.VersionAndroid = node.Attributes["VersionAndroid"].Value;
                if (node.Attributes["AndroidVersionAddress"] != null) data.PatchAndroidVersion = node.Attributes["AndroidVersionAddress"].Value;
                if (node.Attributes["VersionIOS"] != null) data.VersionIOS = node.Attributes["VersionIOS"].Value;
                if (node.Attributes["IOSVersionAddress"] != null) data.PatchIOSVersion = node.Attributes["IOSVersionAddress"].Value;
                if (node.Attributes["VersionPC"] != null) data.VersionPC = node.Attributes["VersionPC"].Value;
                if (node.Attributes["PCVersionAddress"] != null) data.PatchPCVersion = node.Attributes["PCVersionAddress"].Value;
            }
        }

        return ResourcesPath.PatchData != null && ResourcesPath.PatchData.IsPatchAllDone();
    }

    /* <LoadSGCVersion>d__95.MoveNext (1-1 from Ghidra RVA 0x019082f8):
     *   URL = String.Format("{0}?v={1}", ResourcesPath.PatchAndroidVersion, NowTimeStamp)
     *   yield SendWebRequest()
     *   on done: parse text, ParsingSGCVersion(text)
     *     returns true (= remote >= local, version OK) → state 1 (Done)
     *     returns false (= remote < local, need force update) → state 3 (ForceUpdate)
     *   on error: state 2 (Error)
     */
    private IEnumerator LoadSGCVersion()
    {
        int ts = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string baseUrl = ResourcesPath.PatchAndroidVersion;
        if (string.IsNullOrEmpty(baseUrl))
        {
            UnityEngine.Debug.LogError("LoadSGCVersion: PatchAndroidVersion is empty");
            LoadVersionState = (VersionState)2;
            yield break;
        }
        string url = string.Format("{0}?v={1}", baseUrl, ts);
        UnityEngine.Debug.LogFormat("LoadSGCVersion Path-> {0}", url);
        var req = UnityEngine.Networking.UnityWebRequest.Get(url);
        if (req == null) yield break;
        yield return req.SendWebRequest();
        if (!string.IsNullOrEmpty(req.error))
        {
            UnityEngine.Debug.Log("==== LoadSGCVersion Error ==== " + req.error);
            LoadVersionState = (VersionState)2;
            yield break;
        }
        var dl = req.downloadHandler;
        string text = dl != null ? dl.text : null;
        if (string.IsNullOrEmpty(text)) { LoadVersionState = (VersionState)2; yield break; }
        bool ok = ParsingSGCVersion(text.Trim());
        // 1-1 from Ghidra: ok=true → state 1 (Done), ok=false → state 3 (ForceUpdate, NOT Error)
        LoadVersionState = (VersionState)(ok ? 1 : 3);
        UnityEngine.Debug.Log("[LoadSGCVersion] ParsingSGCVersion=" + ok + " → state=" + (int)LoadVersionState);
    }

    /* RVA 0x0190566c — ParsingSGCVersion:
     *   var doc = XmlDocument.Load(text);
     *   string localVer = Main.Instance._ConfigGeneral.appVersion (offset 0x40 + 0x28);
     *   foreach (XmlNode n in doc.SelectNodes("//ver")) {
     *     string remoteVer = n.Attributes["version"].Value;
     *     if (CheckVersionGreatThen(remoteVer, localVer)) return true;  // force update
     *   }
     *   return false;
     */
    private bool ParsingSGCVersion(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        var doc = new System.Xml.XmlDocument();
        try { doc.LoadXml(text); } catch { return false; }
        var main = Main.Instance;
        string localVer = "";
        if (main != null && main._ConfigGeneral != null)
        {
            // ConfigGeneral.BundleVersion is the "X.Y.Z" string equivalent to appVersion
            localVer = main._ConfigGeneral.BundleVersion;
        }
        if (string.IsNullOrEmpty(localVer)) localVer = ResourcesPath.CurVersion;
        var nodes = doc.SelectNodes("//ver");
        if (nodes == null) return false;
        foreach (System.Xml.XmlNode n in nodes)
        {
            if (n.Attributes == null) continue;
            var verAttr = n.Attributes["version"];
            if (verAttr == null) continue;
            if (CheckVersionGreatThen(verAttr.Value, localVer)) return true;
        }
        return false;
    }

    /* RVA 0x01905ab8 — CheckVersionGreatThen(sAPKVersion, sDefineVersion):
     *   Returns true if sAPKVersion > sDefineVersion (numeric "X.Y.Z" comparison).
     *   Both must split into exactly 3 parts on '.'. Each part TryParse to int (-1 on fail).
     *   Compare major→minor→patch; first non-equal decides; return true iff sDefine < sAPK.
     */
    private bool CheckVersionGreatThen(string sAPKVersion, string sDefineVersion)
    {
        if (string.IsNullOrEmpty(sAPKVersion) || string.IsNullOrEmpty(sDefineVersion)) return false;
        var apk = sAPKVersion.Split('.');
        var def = sDefineVersion.Split('.');
        if (apk.Length != 3 || def.Length != 3) return false;
        int[] a = new int[3];
        int[] d = new int[3];
        for (int i = 0; i < 3; i++)
        {
            if (!int.TryParse(apk[i], out a[i])) a[i] = -1;
            if (!int.TryParse(def[i], out d[i])) d[i] = -1;
        }
        // 1-1 from Ghidra (RVA 0x01905ab8):
        //   major: if (def < apk) return 1; if (apk != def) return 0;
        //   minor: if (def < apk) return 1; if (apk != def) return 0;
        //   patch: if (apk < def) return 0; return 1;
        // Semantics: returns true if APK is at least as new as Define (apk >= def).
        if (d[0] < a[0]) return true;
        if (a[0] != d[0]) return false;
        if (d[1] < a[1]) return true;
        if (a[1] != d[1]) return false;
        if (a[2] < d[2]) return false;
        return true;
    }

    /* <LoadVersion>d__98.MoveNext: fetch URL stored in ResourcesPath.PatchDefineVersion
     * (full URL from PatchHostList). */
    private IEnumerator LoadVersion()
    {
        int ts = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string baseUrl = ResourcesPath.PatchDefineVersion;
        if (string.IsNullOrEmpty(baseUrl))
        {
            UnityEngine.Debug.LogError("LoadVersion: PatchDefineVersion is empty");
            LoadVersionState = (VersionState)2;
            yield break;
        }
        string url = string.Format("{0}?v={1}", baseUrl, ts);
        UnityEngine.Debug.LogFormat("LoadVersion Path-> {0}", url);
        var req = UnityEngine.Networking.UnityWebRequest.Get(url);
        if (req == null) yield break;
        yield return req.SendWebRequest();
        if (!string.IsNullOrEmpty(req.error))
        {
            UnityEngine.Debug.Log("==== LoadVersion Error ==== " + req.error);
            LoadVersionState = (VersionState)2;
            yield break;
        }
        var dl = req.downloadHandler;
        string text = dl != null ? dl.text : null;
        if (string.IsNullOrEmpty(text)) { LoadVersionState = (VersionState)2; yield break; }
        bool ok = ParsingVersionData(text.Trim());
        LoadVersionState = (VersionState)(ok ? 1 : 2);
    }

    /* RVA 0x01905d8c — ParsingVersionData:
     *   Parse DefineVersion.xml. Take first <ver> node's @DefineVer attribute.
     *   Assign to ResMgr.Instance.sVersionTag (offset 0x10).
     *   Return true if first node found, false otherwise.
     */
    private bool ParsingVersionData(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        var doc = new System.Xml.XmlDocument();
        try { doc.LoadXml(text); } catch { return false; }
        var nodes = doc.SelectNodes("//ver");
        if (nodes == null || nodes.Count == 0) return false;
        // Original takes ONLY first MoveNext result then breaks
        var first = nodes[0];
        if (first == null || first.Attributes == null) return false;
        var attr = first.Attributes["DefineVer"];
        if (attr == null) return false;
        var rm = ResMgr.Instance;
        if (rm == null) return false;
        rm.sVersionTag = attr.Value;
        return true;
    }

    /* RVA 0x019074d4 — <LoadBundleSize>d__100.MoveNext (1-1 port).
     * URL = String.Concat(PatchHost, GetAbPath(), newABPath, "FileSize.txt") formatted "{0}?v={1}" with ts.
     * StringLits: 5410="FileSize.txt", 21255="{0}?v={1}", 7646="LoadBundleSize Path-> {0}".
     * Note: dump-time literal "BundleSize.xml" (StringLit_3484) is the file written for EN_DEBUG=="1",
     *       NOT the actual fetch URL.
     */
    private IEnumerator LoadBundleSize()
    {
        int ts = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string baseUrl = ResourcesPath.PatchHost + ResourcesPath.GetAbPath() + ProcessLunchGame.newABPath + "FileSize.txt";
        string url = string.Format("{0}?v={1}", baseUrl, ts);
        UnityEngine.Debug.LogFormat("LoadBundleSize Path-> {0}", url);
        var req = UnityEngine.Networking.UnityWebRequest.Get(url);
        if (req == null) yield break;
        yield return req.SendWebRequest();
        if (!string.IsNullOrEmpty(req.error))
        {
            UnityEngine.Debug.Log("==== LoadBundleSize Error ==== " + req.error);
            LoadBundleSizeState = (VersionState)2;
            yield break;
        }
        var dl = req.downloadHandler;
        string text = dl != null ? dl.text : null;
        if (string.IsNullOrEmpty(text)) { LoadBundleSizeState = (VersionState)2; yield break; }
        var bhc = BundleHashChecker.Instance;
        bool ok = bhc != null && bhc.ParseBundleSizeFromString(text.Trim());
        LoadBundleSizeState = (VersionState)(ok ? 1 : 2);
    }

    /* <LoadServerList>d__101.MoveNext: fetch ServerList. URL = PatchHost + "ServerList". */
    private IEnumerator LoadServerList()
    {
        string baseUrl = ResourcesPath.PatchServerList;
        if (string.IsNullOrEmpty(baseUrl))
        {
            LoadServerListState = (VersionState)2;
            yield break;
        }
        int ts = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string url = string.Format("{0}?v={1}", baseUrl, ts);
        UnityEngine.Debug.LogFormat("LoadServerList Path-> {0}", url);
        var req = UnityEngine.Networking.UnityWebRequest.Get(url);
        if (req == null) yield break;
        yield return req.SendWebRequest();
        if (!string.IsNullOrEmpty(req.error))
        {
            UnityEngine.Debug.Log("==== LoadServerList Error ==== " + req.error);
            LoadServerListState = (VersionState)2;
            yield break;
        }
        var dl = req.downloadHandler;
        string text = dl != null ? dl.text : null;
        if (string.IsNullOrEmpty(text)) { LoadServerListState = (VersionState)2; yield break; }
        bool ok = ParsingServerList(text.Trim());
        LoadServerListState = (VersionState)(ok ? 1 : 2);
    }

    /* RVA 0x019061d4 — ParsingServerList:
     *   Clear ResMgr.Instance.serverListData; for each <server> node parse attributes:
     *     id, real_server_id, name (overridden by name_CN/name_EN per language),
     *     ip, port, regionID, status, recommend, merge_server_id.
     *   Append to ResMgr.serverListData.
     */
    private bool ParsingServerList(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        var doc = new System.Xml.XmlDocument();
        try { doc.LoadXml(text); } catch { return false; }
        var rm = ResMgr.Instance;
        if (rm == null) return false;
        if (rm.serverListData == null) rm.serverListData = new System.Collections.Generic.List<ServerListData>();
        else rm.serverListData.Clear();
        var nodes = doc.SelectNodes("//server");
        if (nodes == null) return true;
        var cm = ConfigMgr.Instance;
        int lang = cm != null ? cm.GetConfigVarLanguage() : 0;
        foreach (System.Xml.XmlNode n in nodes)
        {
            if (n.Attributes == null) continue;
            var data = new ServerListData();
            // StringLits: 17225="id" 19398="real_server_id" 18775="name" 17990="ip"
            //             19243="port" 19439="regionID" 18629="merge_server_id"
            //             20089="status" 18781="name_CN" 18782="name_EN"
            var attr = n.Attributes["id"];
            if (attr != null) int.TryParse(attr.Value, out data.id);
            attr = n.Attributes["real_server_id"];
            if (attr != null) int.TryParse(attr.Value, out data.realServerID);
            attr = n.Attributes["name"];
            if (attr != null) data.name = attr.Value;
            attr = n.Attributes["ip"];
            if (attr != null) data.ip = attr.Value;
            attr = n.Attributes["port"];
            if (attr != null) int.TryParse(attr.Value, out data.port);
            attr = n.Attributes["regionID"];
            if (attr != null) data.regionID = attr.Value;
            attr = n.Attributes["status"];
            if (attr != null) int.TryParse(attr.Value, out data.status);
            attr = n.Attributes["recommend"];
            if (attr != null) int.TryParse(attr.Value, out data.recommend);
            attr = n.Attributes["merge_server_id"];
            if (attr != null) data.mergeServerID = attr.Value;
            // Language-specific name override (Chinese=2 → name_CN, English=3 → name_EN)
            if (lang == 2)
            {
                attr = n.Attributes["name_CN"];
                if (attr != null) data.name = attr.Value;
            }
            else if (lang == 3)
            {
                attr = n.Attributes["name_EN"];
                if (attr != null) data.name = attr.Value;
            }
            rm.serverListData.Add(data);
        }
        return true;
    }

    // ────────────────────────────────────────────────────────────────────────
    //  Bundle CB callbacks. Each assigns AssetBundleOP into ResMgr.Instance at
    //  the matching field offset. Empty CBs (LoadCreateChar/LoadModel/LoadMusic)
    //  ported as no-op per Ghidra (just `return;`).
    // ────────────────────────────────────────────────────────────────────────

    /* RVA 0x01906cdc — LoadLuaScriptBundleCB: ResMgr.Instance + 0x28 = LuaBundleOP */
    public void LoadLuaScriptBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.LuaBundleOP = bundle;
    }

    /* RVA 0x01906d58 — LoadFontBundleCB: ResMgr.Instance + 0x30 */
    public void LoadFontBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.FontBundleOP = bundle;
    }

    /* RVA 0x01906dd4 — LoadMainUIBundleCB: ResMgr.Instance + 0x38 */
    public void LoadMainUIBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.MainUIBundleOP = bundle;
    }

    /* RVA 0x01906e50 — LoadItemIconBundleCB: ResMgr.Instance + 0x40 */
    public void LoadItemIconBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.ItemIconBundleOP = bundle;
    }

    /* RVA 0x01906ecc — LoadHeadIconBundleCB: ResMgr.Instance + 0x48 */
    public void LoadHeadIconBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.HeadIconBundleOP = bundle;
    }

    /* RVA 0x01906f48 — LoadSkillIconBundleCB: ResMgr.Instance + 0x50 */
    public void LoadSkillIconBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.SkillIconBundleOP = bundle;
    }

    /* RVA 0x01906fc4 — LoadModelBundleCB: empty body in original (no-op).
     * Any model-bundle assignment must be done elsewhere by the engine.
     */
    public void LoadModelBundleCB(AssetBundleOP bundle) { /* no-op per Ghidra */ }

    /* RVA 0x01906fc8 — LoadFXBundleCB: ResMgr.Instance + 0x60 */
    public void LoadFXBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.FXBundleOP = bundle;
    }

    /* RVA 0x01907044 — LoadCreateCharBundleCB: empty body (no-op). */
    public void LoadCreateCharBundleCB(AssetBundleOP bundle) { /* no-op per Ghidra */ }

    /* RVA 0x01907048 — LoadSMapBundleCB: ResMgr.Instance + 0x68 */
    public void LoadSMapBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.SMapBundleOP = bundle;
    }

    /* RVA 0x019070c4 — LoadMapDataBundleCB: ResMgr.Instance + 0x70 */
    public void LoadMapDataBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.MapDataBundleOP = bundle;
    }

    /* RVA 0x01907140 — LoadMusicBundleCB: empty body (no-op). */
    public void LoadMusicBundleCB(AssetBundleOP bundle) { /* no-op per Ghidra */ }

    /* RVA 0x01907144 — LoadSoundBundleCB: ResMgr.Instance + 0x80 */
    public void LoadSoundBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.SoundBundleOP = bundle;
    }

    /* RVA 0x019071c0 — LoadUIFXBundleCB: ResMgr.Instance + 0x88 (UIParticleOP) */
    public void LoadUIFXBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.UIParticleOP = bundle;
    }

    /* RVA 0x0190723c — LoadMagicDataBundleCB: ResMgr.Instance + 0x90 */
    public void LoadMagicDataBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.MagicDataBundleOP = bundle;
    }

    /* RVA 0x019072b8 — LoadMagicFxBundleCB: ResMgr.Instance + 0x98 */
    public void LoadMagicFxBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.MagicFxBundleOP = bundle;
    }

    /* RVA 0x01907334 — LoadCardIconBundleCB: ResMgr.Instance + 0xa0 */
    public void LoadCardIconBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.CardIconBundleOP = bundle;
    }

    /* RVA 0x019073b0 — LoadEmojiBundleCB: ResMgr.Instance + 0xa8 */
    public void LoadEmojiBundleCB(AssetBundleOP bundle)
    {
        if (bundle == null) return;
        var rm = ResMgr.Instance; if (rm == null) return;
        rm.EmojiBundleOP = bundle;
    }

    /* RVA 0x018fecb0 — _checkDefaultLanguage:
     *   var lang = ConfigMgr.Instance.GetConfigVarLanguage();
     *   if (!SGCLanguage.CheckConfigVarIsValid(lang) && SGCLanguage.UseLanguageSystem) {
     *     SGCLanguageSelect.Instance.setSelLanguageWndEnable(true);
     *     _stepUpdate = 5; return;
     *   }
     *   if (lang == 0) SGCLanguage.SetConfigDefaultLanguage();
     *   _stepUpdate = 6;
     */
    private void _checkDefaultLanguage()
    {
        var cm = ConfigMgr.Instance;
        if (cm == null) return;
        int lang = cm.GetConfigVarLanguage();
        bool valid = SGCLanguage.CheckConfigVarIsValid(lang);
        if (!valid && SGCLanguage.UseLanguageSystem)
        {
            var sel = SGCLanguageSelect.Instance;
            if (sel == null) return;
            sel.setSelLanguageWndEnable(true);
            _stepUpdate = (UpdateStep)5;
            return;
        }
        if (lang == 0)
            SGCLanguage.SetConfigDefaultLanguage(0);
        _stepUpdate = (UpdateStep)6;
    }

    /* RVA 0x018fedc0 — _checkSetRegion:
     *   if (SGCRegion.ver_useRegionFlag) {
     *     int rg = ConfigMgr.Instance.GetConfigVarRegion();
     *     if (!SGCRegion.CheckConfigVarIsValid(rg)) {
     *       var sel = SGCRegionSelect.Instance;
     *       sel.InitRegionComponent();
     *       sel.setSelRegionWndEnable(true);
     *       _stepUpdate = 7; return;
     *     }
     *   }
     *   _stepUpdate = 8;
     */
    private void _checkSetRegion()
    {
        if (SGCRegion.ver_useRegionFlag)
        {
            var cm = ConfigMgr.Instance;
            if (cm == null) return;
            int rg = cm.GetConfigVarRegion();
            if (!SGCRegion.CheckConfigVarIsValid(rg))
            {
                var sel = SGCRegionSelect.Instance;
                if (sel == null) return;
                sel.InitRegionComponent();
                sel.setSelRegionWndEnable(true);
                _stepUpdate = (UpdateStep)7;
                return;
            }
        }
        _stepUpdate = (UpdateStep)8;
    }

    private enum VersionState
    {
        None = 0,
        Loading = 1,
        Done = 2,
        Error = 3
    }
}
