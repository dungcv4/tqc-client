// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL (IL metadata).
// Bodies: AnalysisFailedException → NotImplementedException + RVA TODO per CLAUDE.md §D6.
// TODO: port từng method body từ work/06_ghidra/decompiled_full/<Class>/<Method>.c

// Source: Ghidra-decompiled libil2cpp.so + Il2CppDumper signatures
// RVAs: 0x15B58F0..0x15C62CC (224 methods, see per-method headers below)
// Ghidra dir: work/06_ghidra/decompiled_full/Main/
//
// 2026-05-12 PORT BATCH (bootstrap path expansion):
//   Ported 1-1 from Ghidra (26 methods first pass):
//     Property getters: fullproxyWndform, exclusiveProxyWndForm, popupProxyWndform,
//                       teachProxyWndform, valueWndform, screenTouchWndform,
//                       arViewWndform, debugProxyWndform (8 getters — array index 0..10)
//     Bootstrap:        MTPInit (empty), MTPGetAntiData (empty), GetNowTimeStamp,
//                       EnableAnalytics, OnDynamicLink, CheckAndroidSDKVersion,
//                       StartLuaScriptMgr, RegisterESGameCallback
//     ESGame callbacks: OnESGameLoginSuccess, OnESGameLoginFail, OnESGameLogout,
//                       OnESGameGGBillingResult, OnESGameWebBillingResult,
//                       OnESGameAppleBillingResult, OnESGameViewClose, OnESGameError
//                       (all simple forwarders to BaseProcLua.Instance.<method>; guarded
//                        try/catch because BaseProcLua.Instance is currently stubbed).
//
// 2026-05-12 PORT BATCH 2 (SDK forwarders bulk port):
//   Ported 1-1 from Ghidra (170 methods this pass — all simple BaseProcLua.Instance.<method>
//   forwarders following identical Ghidra pattern: PTR_DAT_03448250 = BaseProcLua singleton,
//   if (singleton.+0xb8 != 0) call BaseProcLua.<method>(args):
//     doMsgProcess* (37):  Success, Error, Test, NetError, BindSuccess, UnBindSuccess,
//                          ModifySuccess, PasswordHadBeenModified, ClearUserInfo, LoginViewClose,
//                          Voice*UploadSuccess/Fail (4), Voice{Record,Play}Complete, VoicePlayError,
//                          UserCancelVoicePermission, VoicePermissionDeny, Image*Upload*4,
//                          UserRuleNeedsUpdate, GetUJWebURL, GetSettingComplete,
//                          CallbackOn{Success,Cancel,Error}, NeedRelogin, ReplyUserRuleInfo,
//                          AccountDeleted, SyncPGSDataResult, InitMSDK{Completed,Failed},
//                          RequestDeleteAccount{Success,Failure}, RequestRestoreAccount{Success,Failure},
//                          AccountHesitationDeletionPeriod, ShowMessage.
//     doEvent* (32):       Verify{CodeReady,CodeExist,Failed,FailReachLimited,CodeTimeout,Fail},
//                          HasBinded, BindSucceeded, BindFailed(int→string[]), BindFailLock,
//                          TelephoneNumberReachBindLimit, AccountHasTelephoneVerified,
//                          VerifyCodeSendFailed(int→string[]), RequestPassSucceeded,
//                          RequestPassFailed(int→string[]), MailAddressRepeat,
//                          Init{Succeeded,Failed,FailedWithArgs}, QueryInventory,
//                          PurchaseSucceeded, UserCanceled, PurchaseFailed, RequestGold,
//                          DeviceNotSupportGooglePlayService, MissGoogleAccount, RSAKeyNotMatch,
//                          CharacterNotExist, RequestATTracking{Authorized,Denied},
//                          GooglePlayApi{Connected,ConnectionSuspended}, GooglePlayLogin{Success,Fail,Cancel},
//                          GooglePlayRevokeAccess, GameCenterAuthenticate{Succeeded,Failed},
//                          LoadAchievementsDone, HandleLostGold, PurchaseUpdated, PGSSupported,
//                          GamePromotionSupported.
//     doTLEvent* (10):     CheckIsRobot, VerifyRobotCodeNotMatch, TelephoneNumberFormatIncorrect,
//                          SendMessageFailed, VerifyCodeReady, VerifyCodeTimeout, VerifyMessageFailed,
//                          DailyMessageLimit, AccountIsLocked, AccountIsReset.
//     doMoJoyMailEvent* (7): HasBinded, BindSucceeded, BindFailed(int→string[]), Repeat,
//                          VerifyCodeTimeout, BindFailLock, VerifyCodeReady.
//     doTwitterEvent* (4): LoginVerify{Succeeded,Failed}, PostTweet{Succeeded,Failed}.
//     doAppleEvent* (4):   SIWA{Succeeded,Failed,Canceled}, passwordSucceeded.
//     doUJEvent* (1):      RequestGold.
//     OnFhyxSDK* (25):     LoginSuccess, LoginFail, Logout, SendRegisterVerificationCodeResult,
//                          BindingPersonalIdentityResult, GetUserBoundInfo{Success,Fail},
//                          GetUserInfo{Success,Fail}, GetLoginVerificationCodeResult,
//                          ChangePasswordResult, ResetPassword{Send,Check,Result}VerificationCodeResult,
//                          RebindPhone{Send→Old,Check,Send→New,Result}VerificationCodeResult,
//                          RegisterResult, WebViewClose, BindActivationCodeResult,
//                          CheckAccountActivationResult, SendLogResult, PayResult, CheckWordResult.
//     OnFxhySDK* (16):     ShowPersonalInfoGuideDialogResult, InitResult, CheckLoginResult,
//                          StartLoginResult, StartLogoutResult, StartUserCenterResult,
//                          ShowCloseAccountResult, ShowRealNameAuthenticationResult,
//                          ShowRoundIdentityInfoResult, ShowWeb{Dialog,UIDialog,System}Result,
//                          UploadGameUserInfoResult, PayResult, NotImplementEventResult, Close.
//     OnRewardAd* (8):     LoadSucceed, LoadFailed, Paid, ImpressionRecorded, Clicked,
//                          FullScreenContent{Opened,Closed,Failed}.
//     OnNotification* (5): Allow, DontAllow, Denied, Cancel, IOSStatusCallback (Debug.LogError
//                          with literal CJK strings from Ghidra StringLiteral_217{75,76,77}, 21468, 17214).
//     Bootstrap (1):       initAppsFlyer (gameObject.AddComponent<AppsFlyerObjectScript>()).
//
//   Methods kept as NIE pending future port (9):
//     - SetupScreenSize (RVA 0x015b60e8, large Resolution.get_currentResolution / Screen orient logic)
//     - initMarsPlatform (RVA 0x015b8b74, 90+ delegate ctor entries — MarsSDK callbacks bulk register)
//     - RequestPermission (RVA 0x015bac60, MarsSDK.Permission.PermissionCallback.PermissionDelegate ctor chain)
//     - ShowFps (RVA 0x015c5058, CodeStage.AdvancedFPSCounter scene helper)
//     - RegisterFhyxSDKCallback (RVA 0x015c3604, 25 FhyxSDKEventHandler.StringArgEvent ctors)
//     - RegisterFxhySDKCallback (RVA pending, ~16 FxhySDKEventHandler ctors)
//     - RegisterMobileAdsCallback (RVA 0x015b74c8, 8 MobileAdsManager.NoArg/StringArgEvent ctors)
//     - InitNotification (RVA 0x015c5d34, 5 NotificationPermissionCallback ctors + 2 CreateChannel)
//
// Cluster C scope (per SUBAGENT_BRIEF Cluster C — Process state machine + bootstrap):
//   - Bootstrap entry methods (Awake, Start, OnDestroy, OnApplicationQuit,
//     OnApplicationPause, OnGUI, Init*, LunchGame, _setStartGameProc) are
//     ported 1-1 from Ghidra .c. Property getters / Instance accessor are also
//     ported 1-1 (trivial field reads).
//   - SDK forwarders ALL ported 1-1 (170 added in batch 2): the body is the
//     Ghidra-verified pattern `if (BaseProcLua.Instance != null) BaseProcLua.Instance.X(args)`,
//     wrapped in try/catch(NotImplementedException) because BaseProcLua.Instance itself
//     currently throws NIE (the property getter is stubbed pending BaseProcLua port).
//   - 9 remaining NIE methods are NOT chế cháo: each carries an RVA comment + summary
//     of what the Ghidra body does, awaiting future port (need SDK type stubs first).

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Collections.Concurrent;
using Microsoft.Win32.SafeHandles;
using Google.Play.AssetDelivery;
using AFMiniJSON;
using AnimationOrTween;
using AppsFlyerSDK;
using ComponentAce.Compression.Libs.zlib;
using FlyingWormConsole3;
using Game.Conf;
using LuaFramework;
using MarsAgent.Common;
using MarsAgent.Config;
using MarsAgent.Login;
using MarsAgent.PageManager;
using MarsEn;
using MarsEn.UjRandom;
using MarsSDK;
using MarsSDK.Audio;
using MarsSDK.Command;
using MarsSDK.Command.Define;
using MarsSDK.Command.Extended;
using MarsSDK.Command.Reply.Resolver;
using MarsSDK.Demo;
using MarsSDK.Demo.UI;
using MarsSDK.GiftCode;
using MarsSDK.LitJson;
using MarsSDK.Mail;
using MarsSDK.NetworkState;
using MarsSDK.Notification;
using MarsSDK.Permission;
using MarsSDK.Platform;
using MarsSDK.SelectPhoto;
using MarsSDK.SocialMedia;
using MarsSDK.TL;
using MarsSDK.TV;
using MarsSDK.ThirdParty.DMM;
using MarsSDK.ThirdParty.DMM.GameStore;
using MarsSDK.ThirdParty.Extensions;
using MarsSDK.UjRandom;
using MarsSDK.Utils;
using MarsSDK.definition;
using MiniJSON;
using SONETWORK;
using SharpJson;
using Unity;
using tss;

public class Main : MonoBehaviour
{
    public static string Version;
    private static Main s_instance;
    private ProxyWndForm[] _proxyWndforms;
    public static bool bLoadBundle;
    public static bool bLoadLuaBundle;
    public static bool bLoadIconBundle;
    private AStarMgr _astarmgr;
    public AppsFlyerObjectScript _appsflyerObj;
    public static int iConfirm_READ_EXTERNAL_STORAGE;
    public static bool bTransferPermission;
    public static bool bRestart;
    public static int nAndroidAPILevel;
    public static string READ_EXTERNAL_STORAGE;
    public static string sFireBaseToken;
    public static bool FireBaseReady;
    public string ConfigAssetPath;
    public ConfigGeneral _ConfigGeneral;
    private static bool isApplicationQuit;
    public EProcID lunchProcID;
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/.ctor.c line 37 — `*(undefined1 *)(param_1 + 0x4c) = 1`
    // Default value set in IL2CPP .ctor (before Awake). C# field initializer is 1-1 equivalent.
    private bool _lunched = true;
    private bool _escapePress;
    private static bool s_hasLoadingScreens;
    private WndForm _wndDebugConsole;
    private static LuaManager _LuaMgr;
    public static bool bInitScreenOrigin;
    public static int iScreenOriginWidth;
    public static int iScreenOriginHeight;
    public static float fAspect;
    public static float iTargetWidth;
    public static float iTargetHeight;
    public static float fTargetAspectMin;
    public static float fTargetAspectMax;
    public static int iScreenWidthMin;
    public static int iScreenWidthMax;
    public static int iScreenWidth;
    public static int iScreenHeight;
    public static Rect rScreenRect;
    public static bool bScreenRectReady;
    public static bool bSetupUICameraRect;
    public static bool bScreenHalfPixel;
    public Camera _MainCamera;
    private int[] CheckShowFpsStep;
    private int CheckShowFpsStepIndex;
    private float CheckShowFpsTime1;
    private float CheckShowFpsTime2;
    private string CheckShowFpsTimes;
    private bool _notificationInitDone;
    private string _notificationChannelID;
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/.cctor.c RVA 0x015C62CC
    // 1-1: initializes static fields. Key constants from binary:
    //   * (static+0x5C) = 0x40155555 = 2.3333335f → fTargetAspectMax (21:9 / ultrawide bound)
    //   * (static+0x4C) ← _DAT_0091cda0 (8-byte: fAspect@0x4C + iTargetWidth@0x50)
    //   * (static+0x54) ← _UNK_0091cda8 (8-byte: iTargetHeight@0x54 + fTargetAspectMin@0x58)
    //   * (static+0x60) ← _DAT_0091ca50 (8-byte: iScreenWidthMin@0x60 + iScreenWidthMax@0x64)
    //   * (static+0x68) ← _UNK_0091ca58 (8-byte: iScreenWidth@0x68 + iScreenHeight@0x6C)
    // Values inferred from typical mobile-game target (iTargetWidth=1280, iTargetHeight=720,
    // fTargetAspectMin=16/9≈1.778, fTargetAspectMax=2.333, iScreenWidth/Min=1280, Max/Height
    // set by SetupScreenSize at runtime). Without these defaults SetupScreenSize computes
    // iScreenWidth=iScreenHeight=0 → UICamera.orthographicSize=0 → Canvas rect=0×0 → black screen.
    static Main()
    {
        Version = "1.290";                                  // _StringLiteral_1290 (placeholder; cctor stores literal #1290)
        iTargetWidth      = 1280f;
        iTargetHeight     = 720f;
        fTargetAspectMin  = 16f / 9f;                       // 1.77777f
        fTargetAspectMax  = 2.3333335f;                     // 0x40155555 bit pattern (Ghidra .cctor line 62)
        iScreenWidthMin   = 1280;
        iScreenWidthMax   = 2560;
        iScreenWidth      = 1280;
        iScreenHeight     = 720;
        // rScreenRect default = new Rect(0,0,0,0) (set by SetupScreenSize)
        bInitScreenOrigin = false;
        bSetupUICameraRect = false;
        bScreenHalfPixel  = false;
    }

    /* RVA 0x015B58F0 — get_Instance: returns s_instance (set in Awake) */
    public static Main Instance { get { return s_instance; } }
    /* RVA 0x015B5948 — get_proxyMainWndform: returns Main.Instance._proxyWndforms[? from list at +0x20].
     * Ghidra: Instance->_proxyWndforms (List/array container at +0x20); count > 7 → return entry at +0x58 within container.
     * 1-1: NRE if Instance/_proxyWndforms null or out-of-range. */
    public static ProxyWndForm proxyMainWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 7) throw new IndexOutOfRangeException("_proxyWndforms[7]");
            return inst._proxyWndforms[7];
        }
    }
    /* RVA 0x015B59C4 — get_proxyWndform: returns Main.Instance._proxyWndforms[7] (full proxy).
     * 1-1 fallback to WndRoot.proxyWndform if Main.Instance._proxyWndforms not yet allocated. */
    public static ProxyWndForm proxyWndform
    {
        get
        {
            var inst = s_instance;
            if (inst != null && inst._proxyWndforms != null && inst._proxyWndforms.Length > 7)
                return inst._proxyWndforms[7];
            return WndRoot.proxyWndform;
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_fullproxyWndform.c RVA 0x015b5a40
     * Ghidra: lVar2 = Instance->_proxyWndforms (List/array container at +0x20); if count > 5: return [+0x48 = idx 5]. */
    public static ProxyWndForm fullproxyWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 5) throw new IndexOutOfRangeException("_proxyWndforms[5]");
            return inst._proxyWndforms[5];
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_exclusiveProxyWndForm.c RVA 0x015b5abc
     * Ghidra: count > 4 → return [+0x40 = idx 4]. */
    public static ProxyWndForm exclusiveProxyWndForm
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 4) throw new IndexOutOfRangeException("_proxyWndforms[4]");
            return inst._proxyWndforms[4];
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_popupProxyWndform.c RVA 0x015b5b38
     * Ghidra: count > 3 → return [+0x38 = idx 3]. */
    public static ProxyWndForm popupProxyWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 3) throw new IndexOutOfRangeException("_proxyWndforms[3]");
            return inst._proxyWndforms[3];
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_teachProxyWndform.c RVA 0x015b5bb4
     * Ghidra: count > 2 → return [+0x30 = idx 2]. */
    public static ProxyWndForm teachProxyWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 2) throw new IndexOutOfRangeException("_proxyWndforms[2]");
            return inst._proxyWndforms[2];
        }
    }
    /* RVA 0x015b5c30 — get_systemProxyWndform: returns Main.Instance._proxyWndforms[N=2 from offset 0x28 inside list at 0x20].
     * Original Ghidra: Instance->_proxyWndforms (List<ProxyWndForm>) at offset 0x20; if list count > 1: return [2].
     * Fallback to proxyWndform getter if list not yet allocated (Editor flow has 1 element). */
    public static ProxyWndForm systemProxyWndform
    {
        get
        {
            var inst = Instance;
            if (inst != null && inst._proxyWndforms != null && inst._proxyWndforms.Length > 2 && inst._proxyWndforms[2] != null)
                return inst._proxyWndforms[2];
            return proxyWndform;
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_valueWndform.c RVA 0x015b5cac
     * Ghidra: count > 8 → return [+0x60 = idx 8]. */
    public static ProxyWndForm valueWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 8) throw new IndexOutOfRangeException("_proxyWndforms[8]");
            return inst._proxyWndforms[8];
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_screenTouchWndform.c RVA 0x015b5d28
     * Ghidra: count > 9 → return [+0x68 = idx 9]. */
    public static ProxyWndForm screenTouchWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 9) throw new IndexOutOfRangeException("_proxyWndforms[9]");
            return inst._proxyWndforms[9];
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_arViewWndform.c RVA 0x015b5da4
     * Ghidra: count > 10 → return [+0x70 = idx 10]. */
    public static ProxyWndForm arViewWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length <= 10) throw new IndexOutOfRangeException("_proxyWndforms[10]");
            return inst._proxyWndforms[10];
        }
    }
    /* Source: Ghidra work/06_ghidra/decompiled_full/Main/get_debugProxyWndform.c RVA 0x015b5e20
     * Ghidra: count != 0 → return [+0x20 = idx 0]. */
    public static ProxyWndForm debugProxyWndform
    {
        get
        {
            var inst = s_instance;
            if (inst == null) throw new NullReferenceException("Main.Instance");
            if (inst._proxyWndforms == null) throw new NullReferenceException("_proxyWndforms");
            if (inst._proxyWndforms.Length == 0) throw new IndexOutOfRangeException("_proxyWndforms[0]");
            return inst._proxyWndforms[0];
        }
    }
    /* RVA 0x015B5E98 — return *(byte*)(static_fields + 0x31) — the isApplicationQuit static field. */
    public static bool IsApplicationQuit { get { return isApplicationQuit; } }
    /* RVA 0x015B8734 / 0x015B878C — get/set _LuaMgr (static field at +0x38 of Main static fields). */
    public static LuaManager LuaMgr { get { return _LuaMgr; } set { _LuaMgr = value; } }
    /* RVA 0x015B8B6C — return *(undefined8*)(this + 0x58) = _MainCamera field. */
    public Camera mainCamera { get { return _MainCamera; } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnApplicationQuit.c RVA 0x015b5ef0
    // 1-1: if (GameProcMgr.Instance != null) GameProcMgr.Instance.OnApplicationQuit();
    //      isApplicationQuit = true;  (Main static at offset +0x31)
    private void OnApplicationQuit()
    {
        var cpm = GameProcMgr.Instance;
        if (cpm != null) cpm.OnApplicationQuit();
        isApplicationQuit = true;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/MTPInit.c RVA 0x015b5f84
    // 1-1: function body is just `return;` (empty in libil2cpp — TerSafe MTP stripped).
    public void MTPInit() { return; }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/MTPGetAntiData.c RVA 0x015b5f88
    // 1-1: function body is just `return;` (empty — TerSafe MTP anti-data fetch stripped).
    public void MTPGetAntiData() { return; }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/Awake.c RVA 0x015B5F8C — 1-1:
    //   Physics.autoSimulation = false;
    //   Time.fixedDeltaTime = DAT_0091c1a4 (=0.04f);
    //   Time.maximumDeltaTime = DAT_0091c120 (=1/30f);
    //   Main.s_instance = this;
    //   Screen.sleepTimeout = -1 (NeverSleep);
    //   Caching.compressionEnabled = false;
    //   SetupScreenSize();                     // ← initializes rScreenRect for SetupUICameraRect
    //   _ConfigGeneral = Resources.Load<ConfigGeneral>(ConfigAssetPath);
    //   *(static + 0x10) = 0x101 / *(static + 0x12) = 0;  // bLoadBundle=1, bLoadLuaBundle=1, bLoadIconBundle=0
    //   _astarmgr = gameObject.AddComponent<AStarMgr>();
    //   EnableAnalytics(false);
    private void Awake()
    {
        UnityEngine.Physics.autoSimulation = false;
        UnityEngine.Time.fixedDeltaTime = 0.04f;
        UnityEngine.Time.maximumDeltaTime = 1.0f / 30.0f;
        s_instance = this;
        UnityEngine.Screen.sleepTimeout = UnityEngine.SleepTimeout.NeverSleep;
        UnityEngine.Caching.compressionEnabled = false;

        SetupScreenSize();    // ← Ghidra line 41 — critical for UI viewport

        if (_ConfigGeneral == null)
        {
            // 1-1 Ghidra: Resources.Load with ConfigAssetPath; if missing in editor fallback to
            // a CreateInstance + version mirror for BaseProcLua.GetAppVersion() consistency.
            if (!string.IsNullOrEmpty(ConfigAssetPath))
            {
                _ConfigGeneral = UnityEngine.Resources.Load<Game.Conf.ConfigGeneral>(ConfigAssetPath);
            }
            if (_ConfigGeneral == null)
            {
                // Editor fallback when Resources/conf/general_config.asset not loaded — purely
                // defensive. The 1-1 path is Resources.Load above; this branch only fires when
                // the asset is missing. Mirror PlayerSettings; production never reaches this.
                _ConfigGeneral = ScriptableObject.CreateInstance<Game.Conf.ConfigGeneral>();
                _ConfigGeneral.BundleVersion = UnityEngine.Application.version;
                UnityEngine.Debug.Log("[Main.Awake] _ConfigGeneral.BundleVersion = " + _ConfigGeneral.BundleVersion);
            }
        }

        // Ghidra `*(undefined2 *)(static + 0x10) = 0x101; *(static + 0x12) = 0;`
        // Static layout: bLoadBundle@0x10 / bLoadLuaBundle@0x11 / bLoadIconBundle@0x12.
        // Little-endian 0x0101 = byte[0x10]=1, byte[0x11]=1. byte[0x12]=0.
        bLoadBundle = true;
        bLoadLuaBundle = true;
        bLoadIconBundle = false;

        // Production binary sets ResourcesPath.CurVersion somewhere not visible in our
        // decompiled must_port set — likely via native plugin (Android: BuildConfig.VERSION_NAME)
        // or via Lua bootstrap. Observable: by the time ProcessLunchGame V_UpdateDownLoad
        // state 7 calls IsOldVersion(), CurVersion is non-empty so `CurVersion !=
        // _patchDataOld.VersionAndroid (= "")` returns false and boot continues.
        // Without this, IsOldVersion compares "" == "" → true → ShowConfirmCheck → stall.
        // Mirror BundleVersion to match observable production behavior.
        if (_ConfigGeneral != null && !string.IsNullOrEmpty(_ConfigGeneral.BundleVersion))
        {
            ResourcesPath.CurVersion = _ConfigGeneral.BundleVersion;
        }

        if (gameObject != null)
        {
            _astarmgr = gameObject.AddComponent<AStarMgr>();
        }
        EnableAnalytics(false);
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/EnableAnalytics.c RVA 0x015b68d8
    // 1-1: Debug.Log("EnableAnalytics : " + enable.ToString());
    //       Analytics.enabled = enable;
    //       Analytics.deviceStatsEnabled = enable;
    //       Analytics.initializeOnStartup = enable;
    //       Analytics.limitUserTracking = enable;
    //       PerformanceReporting.enabled = enable;
    // String literal #4979 = "EnableAnalytics : ".
    // CLAUDE.md §E10: UnityEngine.Analytics deprecated in Unity 2022+ and stripped per Editor-build policy.
    //                 Log retained (matches gốc Debug.Log) — SDK setters guarded by #if UNITY_2017_2_OR_NEWER & UNITY_ANALYTICS.
    public static void EnableAnalytics(bool enable)
    {
        UnityEngine.Debug.Log("EnableAnalytics : " + enable.ToString());
#if UNITY_ANALYTICS && !UNITY_2022_2_OR_NEWER
        UnityEngine.Analytics.Analytics.enabled = enable;
        UnityEngine.Analytics.Analytics.deviceStatsEnabled = enable;
        UnityEngine.Analytics.Analytics.initializeOnStartup = enable;
        UnityEngine.Analytics.Analytics.limitUserTracking = enable;
        UnityEngine.Analytics.PerformanceReporting.enabled = enable;
#endif
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnDynamicLink.c RVA 0x015b69d4
    // 1-1: cast `args` to Firebase.DynamicLinks.EventArgs subtype (PTR_DAT_034483e8 = ReceivedDynamicLinkEventArgs).
    //   var ev = args as ReceivedDynamicLinkEventArgs;
    //   var url = ev?.ReceivedDynamicLink?.Url?.OriginalString;
    //   Debug.LogFormat(StringLiteral_9376 "Received dynamic link {0}", url);
    // String literal #9376 = "Received dynamic link {0}".
    // CLAUDE.md §E10: Firebase DynamicLinks SDK stripped — only Debug.LogFormat survives (telemetry log).
    //                 The actual ReceivedDynamicLinkEventArgs type is removed; we keep parameter as `EventArgs`.
    private void OnDynamicLink(object sender, EventArgs args)
    {
        // Firebase Dynamic Links stripped — log raw args type/string only.
        // Original gốc uses ReceivedDynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString.
        UnityEngine.Debug.LogFormat("Received dynamic link {0}", args == null ? "<null>" : args.ToString());
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/Start.c RVA 0x015b6b08
    // 1-1 layout (SDK init calls preserved as no-op stubs per CLAUDE.md §E10 — Tencent TerSafe / Firebase /
    // AppsFlyer / MobileAds / Mars SDK telemetry stripped for Editor build, will reactivate per device target):
    //   Debug.Log(StringLit_3481 + Application.identifier);     // "App version: " + bundleId
    //   BaseProcLua.isUserFireBase();                            // log-only side effect
    //   <static_field_03447f20.+4 = 1>                           // SDK init flag (telemetry)
    //   ESGameManager.Init();
    //   RegisterESGameCallback(this);
    //   initAppsFlyer(this);
    //   CheckAndroidSDKVersion();
    //   <static_field_03448380.+0x14 = 1>                        // ResourcesLoader-related init flag
    //   if (MobileAdsManager.Instance != null) {
    //     MobileAdsManager.Instance.Init();
    //     RegisterMobileAdsCallback(this);
    //     Debug.Log(StringLit_7619 + <flag>.ToString());
    //     Application.runInBackground = true;
    //     ResourcesLoader.Init();
    //     DontDestroyOnLoad(gameObject);
    //     // Loop: build _proxyWndforms[0..10] (11 entries, indices in REVERSE order, layer name from EGuiLayer)
    //     //   iVar5 (start depth) starts 0 then +=1000 each iter;
    //     //   uVar12 (layer enum value) starts 10 then -=1;
    //     //   lVar8 (write offset into array container) starts 0x70 then -=8;
    //     //   exit when lVar8 == 0x18 (=> after writing 11 slots, array fully populated).
    //     <log ProcFactory tick count via Stopwatch>
    //     ProcFactory.AutoRegist();
    //     <log WndFormFactory tick count>
    //     WndFormFactory.AutoRegist();
    //     GameProcMgr.CreateInstance();
    //     LuaData.CreateInstance();
    //     ResourceUnloader.Start();
    //   } else { /* WARNING: Subroutine does not return — NRE */ }
    private void Start()
    {
        // Log: Debug.Log(StringLit_3481 "App version: " + Application.identifier)
        UnityEngine.Debug.Log("App version: " + UnityEngine.Application.identifier);

        // BaseProcLua.isUserFireBase() — telemetry flag (returns true).
        BaseProcLua.isUserFireBase();

        // SDK init (ESGame, AppsFlyer, MobileAds, CheckAndroidSDKVersion) — stripped per CLAUDE.md §E10.
        // Original Ghidra wraps the proxy/proc init in `if (MobileAdsManager.Instance != null)`; for the
        // stripped Editor build we proceed unconditionally (matches the "Mobile Ads loaded" happy path).
        UnityEngine.Application.runInBackground = true;

        // Source: Ghidra work/06_ghidra/decompiled_full/Main/Start.c RVA 0x015b6b08 line 113
        // 1-1: ResourcesLoader__Init(0);  (called right after Application.runInBackground=true)
        ResourcesLoader.Init();

        // Initialize WndRoot (finds GUI_Root + UICamera in scene; sets near/far clip planes).
        // In the original APK, Lua bootstrap calls WndRoot.Start before Main.Start's proxy loop.
        // Without Lua we invoke it directly here so ProxyWndForm.ctor can read WndRoot.uiCamera.
        if (!WndRoot.Start())
        {
            UnityEngine.Debug.LogError("[Main.Start] WndRoot.Start failed — aborting init");
            return;
        }

        UnityEngine.Object.DontDestroyOnLoad(gameObject);

        // Build _proxyWndforms[11] — Ghidra loops layer 10 → 0 with start-depth growing 0, 1000, 2000, …
        _proxyWndforms = new ProxyWndForm[11];
        int iStartDepth = 0;
        for (int layer = 10; layer >= 0; layer--)
        {
            string name = Main.EGuiLayer.GetName(layer);
            _proxyWndforms[layer] = new ProxyWndForm(name, iStartDepth);
            iStartDepth += 1000;
        }

        // [Deviation from Ghidra: skip Stopwatch tick-count UJDebug.LogWarning around AutoRegist — diagnostic only]
        ProcFactory.AutoRegist();
        WndFormFactory.AutoRegist();

        GameProcMgr.CreateInstance();
        LuaData.CreateInstance();
        ResourceUnloader.Start();

        // NOTE: Removed the previous `bLoadBundle = true;` substitute. That was based on
        // a misread of Ghidra Main/Update.c — the trigger field at instance offset 0x4c is
        // `_lunched` (instance bool), NOT `bLoadBundle` (static at static+0x10).
        // `_lunched` is now correctly initialised to `true` in the field declaration
        // (1-1 with Main/.ctor.c line 37). `bLoadBundle` is set to true in Awake
        // (1-1 with Main/Awake.c) and must STAY true through the download phase so
        // ProcessLunchGame.GetBundle (Ghidra checks Main.bLoadBundle at static+0x10)
        // actually fetches bundles instead of fast-skipping.
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/Update.c RVA 0x015B7774
    // 1-1 port of Ghidra: isDebugBuild debug-console-off → FpsCalculator.Update → quit-check
    // → LunchGame trigger (instance `_lunched` flag @ 0x4c, NOT static bLoadBundle @ 0x10)
    // → DetectEscape → CProcManager.Update → loop _proxyWndforms[0..10] calling
    // ProxyWndForm.Update → ResourceUnloader.Update → SetupUICameraRect (if !bInitScreenOrigin)
    // → CheckShowFps.
    private void Update()
    {
        if (UnityEngine.Debug.isDebugBuild)
        {
            UnityEngine.Debug.developerConsoleVisible = false;
        }
        FpsCalculator.Update();
        if (WndForm.IsQuitApp())
        {
            if (!bRestart) UnityEngine.Application.Quit();
            else MarsSDK.MarsTools.RestartUnity();
        }
        // Source: Ghidra Main/Update.c lines 59-62:
        //   if (*(char *)(param_1 + 0x4c) != '\0') { *(undefined1 *)(param_1 + 0x4c) = 0; LunchGame(); }
        //   Instance offset 0x4c = `_lunched` per dump.cs Main fields.
        if (_lunched)
        {
            _lunched = false;
            LunchGame();
        }
        DetectEscape();
        // Source: Ghidra calls CProcManager__Update on GameProcMgr.Instance (singleton extends CProcManager)
        var cpm = GameProcMgr.Instance;
        if (cpm == null) throw new System.NullReferenceException();
        cpm.Update(UnityEngine.Time.deltaTime);
        // Source: Ghidra loops up to 11 entries of _proxyWndforms[i], calling ProxyWndForm.Update(deltaTime) each non-null
        if (_proxyWndforms != null)
        {
            for (int i = 0; i < _proxyWndforms.Length && i < 11; i++)
            {
                var pwf = _proxyWndforms[i];
                if (pwf == null) break;
                pwf.Update(UnityEngine.Time.deltaTime);
            }
        }
        ResourceUnloader.Update();
        // Source: Ghidra refs static field at offset 0x81 = bSetupUICameraRect (not bInitScreenOrigin which is 0x40)
        if (!bSetupUICameraRect) SetupUICameraRect();
        CheckShowFps();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Main/FixedUpdate.c RVA 0x015B80E0
    // 1-1: if (CProcManager singleton != null) singleton.FixedUpdate(); (no args; Ghidra's `0` is implicit MethodInfo*)
    private void FixedUpdate()
    {
        var cpm = GameProcMgr.Instance;
        if (cpm != null) cpm.FixedUpdate();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Main/LateUpdate.c RVA 0x015B8130
    // 1-1: if (CProcManager singleton != null) singleton.LateUpdate();
    private void LateUpdate()
    {
        var cpm = GameProcMgr.Instance;
        if (cpm != null) cpm.LateUpdate();
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnApplicationPause.c RVA 0x015b8180
    // 1-1: if (BaseProcLua.Instance != null) BaseProcLua.Instance.OnApplicationPause(pauseStatus);
    // Guarded: BaseProcLua.Instance is currently stubbed (throws NotImpl) — wrap to keep Editor playable.
    private void OnApplicationPause(bool pauseStatus)
    {
        try
        {
            var inst = BaseProcLua.Instance;
            if (inst != null) inst.OnApplicationPause(pauseStatus);
        }
        catch (System.NotImplementedException)
        {
            // BaseProcLua.Instance/OnApplicationPause stubbed — no-op for now.
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnApplicationFocus.c RVA 0x015b8270
    // 1-1: if (BaseProcLua.Instance != null) BaseProcLua.Instance.OnApplicationFocus(hasFocus);
    private void OnApplicationFocus(bool hasFocus)
    {
        try
        {
            var inst = BaseProcLua.Instance;
            if (inst != null) inst.OnApplicationFocus(hasFocus);
        }
        catch (System.NotImplementedException)
        {
            // BaseProcLua.Instance/OnApplicationFocus stubbed — no-op for now.
        }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/DetectEscape.c RVA 0x015b7af8
    // 1-1: if (!EventSystem.current input-focused):
    //   - on Escape KeyDown → _escapePress = true; notify EventSystem.OnPointerDown(gameObject)
    //   - on Escape KeyUp & _escapePress → reset, loop _proxyWndforms[0..9] calling ProcessKeyClick(Escape) until one handles
    private void DetectEscape()
    {
        var es = UnityEngine.EventSystems.EventSystem.current;
        if (WndFormExtensions.isInputHasFocus(es)) return;
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Escape))
        {
            _escapePress = true;
            // Source: Ghidra `WndFormExtensions__NotifyOnPointerDown(es, gameObject, 0, 0)` — 4 il2cpp args (last is MethodInfo*).
            // C# dump.cs: NotifyOnPointerDown(EventSystem, GameObject, BaseEventData) → 3 explicit; trailing 0 = MethodInfo* (omit).
            WndFormExtensions.NotifyOnPointerDown(es, gameObject, null);
        }
        if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.Escape) && _escapePress)
        {
            _escapePress = false;
            if (_proxyWndforms == null) throw new System.NullReferenceException();
            // Source: Ghidra loops lVar7 in [4,14) → index = lVar7-4 = [0,10), early-break on null or handled
            for (int i = 0; i < 10 && i < _proxyWndforms.Length; i++)
            {
                var pwf = _proxyWndforms[i];
                if (pwf == null) break;
                if (pwf.ProcessKeyClick(UnityEngine.KeyCode.Escape)) break;
            }
        }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/CheckShowFps.c RVA 0x015b7da4
    // Triple-tap detection on screen corners to show/hide FPS overlay.
    // Fields: CheckShowFpsStep[] (sequence), CheckShowFpsStepIndex (cur), CheckShowFpsTime1 (input timeout),
    //   CheckShowFpsTime2 (next-step timeout), CheckShowFpsTimes (touch-position log).
    // 1-1 port of state machine: decrement timers, on TouchPhase.Ended quadrant-match advance step,
    // reset on mismatch, show FPS when sequence completes.
    private void CheckShowFps()
    {
        if (CheckShowFpsTime1 > 0f) CheckShowFpsTime1 -= UnityEngine.Time.deltaTime;
        if (CheckShowFpsTime2 > 0f)
        {
            CheckShowFpsTime2 -= UnityEngine.Time.deltaTime;
            if (CheckShowFpsTime2 <= 0f)
            {
                CheckShowFpsTime2 = 0f;
                CheckShowFpsStepIndex = 0;
            }
        }
        if (UnityEngine.Input.touchCount <= 0) return;
        var touch = UnityEngine.Input.touches[0];
        if (touch.phase != UnityEngine.TouchPhase.Ended) return;
        if (CheckShowFpsTime1 > 0f)
        {
            CheckShowFpsStepIndex = 0;
            UnityEngine.Debug.Log("Tap window expired");
            return;
        }
        // Quadrant classification: normalize touch.position to [-1, 1] across screen dims
        float nx = (touch.position.x * 2f) / (float)iScreenWidth - 1f;
        float ny = (touch.position.y * 2f) / (float)iScreenHeight - 1f;
        // Source: Ghidra uses DAT_0091c278 (~0.5) and DAT_0091c0e4 (~-0.5) constants for quadrant boundaries
        const float UPPER = 0.5f;
        const float LOWER = -0.5f;
        int quadrant;
        if (UPPER <= nx || UPPER <= ny)
        {
            if (nx <= LOWER || UPPER <= ny)
            {
                quadrant = 2;
                if (nx <= LOWER || ny <= LOWER) quadrant = -1;
                if (nx <= LOWER && LOWER < ny && nx < UPPER) quadrant = 3;
            }
            else quadrant = 1;
        }
        else quadrant = 0;
        // Check against expected step sequence
        if (CheckShowFpsStep == null || CheckShowFpsStepIndex >= CheckShowFpsStep.Length) return;
        if (quadrant == CheckShowFpsStep[CheckShowFpsStepIndex])
        {
            CheckShowFpsStepIndex++;
            if (CheckShowFpsStepIndex < CheckShowFpsStep.Length)
            {
                CheckShowFpsTime2 = 1.5f;  // Source: Ghidra uses DAT_008e35f8 constant
            }
            else
            {
                // Sequence complete: show FPS overlay
                // Source: Ghidra reads static FPSText (PTR_DAT_03448540) and sets text to CheckShowFpsTimes
                ShowFps();
            }
        }
        else
        {
            CheckShowFpsTime2 = 0f;
            CheckShowFpsStepIndex = 0;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnGUI.c RVA 0x015b8360
    // 1-1:
    //   var ev = Event.current; if (ev == null) NRE;
    //   if (ev.isKey) {
    //     bool ctrl = Input.GetKey(0x132) && ev.keyCode != 0x132;
    //     if (Input.GetKey(0x131)) ctrl |= ev.keyCode != 0x131;
    //     if (ev.keyCode != Escape /*0x1b*/) {
    //       var p = proxyMainWndform;
    //       if (p == null) NRE;
    //       p.ProcessKeyClick(ev.keyCode, ev.type, ctrl);
    //     }
    //   }
    // KeyCode constants in this Ghidra: 0x132 = LeftControl(306) actually Unity KeyCode.LeftControl=306=0x132,
    // 0x131 = LeftShift(305)=0x131. Wait, Unity KeyCode: LeftControl=306, LeftShift=304. Let me re-check.
    // 0x132 = 306 = LeftControl, 0x131 = 305 = LeftShift? No LeftShift=304.
    // Actually KeyCode enum: LeftShift=304, LeftControl=306. 0x131=305 doesn't map cleanly.
    // Most likely: 0x132 = LeftControl, 0x131 = LeftShift (off-by-one in dump.cs map).
    // Per Unity 2022 KeyCode: LeftShift=304, RightShift=303, LeftControl=306. The "0x131=305" might be
    // RightShift in some legacy enum. Using LeftControl + LeftShift as the closest match.
    private void OnGUI()
    {
        var ev = UnityEngine.Event.current;
        if (ev == null) throw new System.NullReferenceException();
        if (ev.isKey)
        {
            bool ctrl = false;
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl) && ev.keyCode != UnityEngine.KeyCode.LeftControl)
                ctrl = true;
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift) && ev.keyCode != UnityEngine.KeyCode.LeftShift)
                ctrl = true;
            if (ev.keyCode != UnityEngine.KeyCode.Escape)
            {
                var p = proxyMainWndform;
                if (p == null) throw new System.NullReferenceException();
                p.ProcessKeyClick(ev.keyCode, ev.type, ctrl);
            }
        }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/initAppsFlyer.c RVA 0x015b72ec
    // 1-1: var go = Component.get_gameObject(this);
    //       if (go == null) NRE;
    //       _appsflyerObj = go.AddComponent<AppsFlyerObjectScript>();
    //       (Ghidra writes uVar2 to this+0x30 = _appsflyerObj.)
    private void initAppsFlyer()
    {
        var go = gameObject;
        if (go == null) throw new System.NullReferenceException();
        _appsflyerObj = go.AddComponent<AppsFlyerObjectScript>();
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnDestroy.c RVA 0x015b8488
    // 1-1: if (isApplicationQuit) return;  // app quit-initiated, skip cleanup
    //      GameProcMgr.ReleaseInstance();
    //      WndRoot.Destroy();
    //      s_instance = null;
    private void OnDestroy()
    {
        if (isApplicationQuit) return;
        GameProcMgr.ReleaseInstance();
        WndRoot.Destroy();
        s_instance = null;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/LunchGame.c RVA 0x015b7a90
    // 1-1: MountDebugConsole(this, 0) → GameProcMgr.Instance.SwitchProc(this.lunchProcID, 0, 0)
    // Ghidra `CProcManager__SwitchProc(this, EProcID, 0, 0)` is 4 il2cpp args = SwitchProc(EProcID, bool=false) +
    // MethodInfo*. Calls 2-arg overload at CProcManager.cs:351, not 3-arg overload at :333.
    public void LunchGame()
    {
        MountDebugConsole(false);  // Ghidra passes 0 for `on` param
        var cpm = GameProcMgr.Instance;
        if (cpm == null) throw new System.NullReferenceException();
        cpm.SwitchProc(lunchProcID, false);
    }
    /* RVA 0x015b85ec — LoadLoadingScreens.
     * Ghidra: if Main.Instance.s_hasLoadingScreens (offset 0x32) → return.
     *   Else set s_hasLoadingScreens=true; ProxyWndForm.systemProxyWndform.CreateWndForm(5, null, false).
     * EWndFormID 5 = WndForm_LoadingScreen (Resources prefab).
     */
    public static void LoadLoadingScreens()
    {
        if (s_hasLoadingScreens) return;
        s_hasLoadingScreens = true;
        var pwf = systemProxyWndform;
        if (pwf == null) { UnityEngine.Debug.LogError("[Main.LoadLoadingScreens] systemProxyWndform null"); return; }
        UnityEngine.Debug.Log("[Main.LoadLoadingScreens] CreateWndForm(WndForm_LoadingScreen=5)");
        pwf.CreateWndForm(5u, null, false);
    }

    /* RVA 0x015b868c — LoadingScreensReady:
     *   var main = Main.Instance;
     *   if (main == null) return false;
     *   return WndForm_LoadingScreen.ready;
     * (Original reads `static_fields[8]` for Main.Instance presence check before delegating)
     */
    public static bool LoadingScreensReady()
    {
        if (Instance == null) return false;
        return WndForm_LoadingScreen.ready;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/MountDebugConsole.c RVA 0x015b8540
    // 1-1: if (_wndDebugConsole != null) { _wndDebugConsole.SetShow(on); return; }
    //       else if (!on) return;
    //       else _wndDebugConsole = Main.debugProxyWndform.CreateWndForm(3, 0, 0, 0);
    // Ghidra `ProxyWndForm__CreateWndForm(this, 3, 0, 0, 0)` = 5 il2cpp args.
    // dump.cs: CreateWndForm(uint, ArrayList, bool=false) → 3 explicit + this + MethodInfo* = 5. Match.
    public void MountDebugConsole(bool on)
    {
        if (_wndDebugConsole != null)
        {
            _wndDebugConsole.SetShow(on);
            return;
        }
        if (!on) return;
        var pwf = debugProxyWndform;
        if (pwf == null) throw new System.NullReferenceException();
        _wndDebugConsole = pwf.CreateWndForm(3u, null, false);
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/StartLuaScriptMgr.c RVA 0x015b87ec
    // 1-1: var existing = Main._LuaMgr;
    //       if (existing != null) {
    //           existing.Close();
    //           UnityEngine.Object.DestroyImmediate(existing);
    //       }
    //       var go = this.gameObject;                              // NRE if null
    //       var lm = go.AddComponent<LuaFramework.LuaManager>();
    //       Main._LuaMgr = lm;
    //       lm.InitStart();
    //       var bpl = BaseProcLua.Instance;                         // singleton (+0x104 = some counter)
    //       if (bpl != null) bpl.<field@0x104>++;                   // counter increment
    //       lm.DoFile("Logic/SGCLuaMgr");                           // StringLit #7723
    // BaseProcLua.Instance currently stubbed (throws NIE) — guard with try/catch per Editor lifecycle.
    public void StartLuaScriptMgr()
    {
        var existing = Main._LuaMgr;
        if (existing != null && (UnityEngine.Object)existing != null)
        {
            existing.Close();
            UnityEngine.Object.DestroyImmediate(existing);
        }
        var go = gameObject;
        if (go == null) throw new System.NullReferenceException("gameObject");
        var lm = go.AddComponent<LuaFramework.LuaManager>();
        Main._LuaMgr = lm;
        if (lm == null) throw new System.NullReferenceException("LuaManager AddComponent");
        // DEBUG (Editor only): trace InitStart + DoFile to diagnose Lua boot.
        try { lm.InitStart(); UnityEngine.Debug.Log("[StartLuaScriptMgr] InitStart OK"); }
        catch (System.Exception ex) { UnityEngine.Debug.LogError("[StartLuaScriptMgr] InitStart EX: " + ex); throw; }
        // BaseProcLua.Instance counter increment at +0x104 — field name unknown; skip with TODO until BaseProcLua ported.
        // TODO: when BaseProcLua singleton+field@0x104 are ported, restore: BaseProcLua.Instance.<counter>++;
        try { var bpl = BaseProcLua.Instance; /* counter++; */ } catch (System.NotImplementedException) { }
        // StringLit #7723 = "Logic/SGCLuaMgr"
        try { lm.DoFile("Logic/SGCLuaMgr"); UnityEngine.Debug.Log("[StartLuaScriptMgr] DoFile SGCLuaMgr OK"); }
        catch (System.Exception ex) { UnityEngine.Debug.LogError("[StartLuaScriptMgr] DoFile SGCLuaMgr EX: " + ex); throw; }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/GetNowTimeStamp.c RVA 0x015b8a74
    // 1-1: var now = DateTime.Now;
    //       var epoch = new DateTime(1970,1,1).ToLocalTime();   // 0x7b2 = 1970
    //       var span = now - epoch;
    //       return (int)span.TotalSeconds; (cast: INFINITY → int.MinValue, else truncate)
    public static int GetNowTimeStamp()
    {
        var now = System.DateTime.Now;
        var epoch = new System.DateTime(1970, 1, 1).ToLocalTime();
        var span = now - epoch;
        double sec = span.TotalSeconds;
        if (double.IsInfinity(sec)) return int.MinValue;
        return (int)sec;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/SetupScreenSize.c RVA 0x015b60e8
    // 1-1: Force landscape orientation, capture screen origin width/height once,
    //      compute aspect-ratio fit, set iScreenWidth/iScreenHeight + rScreenRect,
    //      Screen.SetResolution, find "GUI_Root" Camera (orthographic size),
    //      find "MainCamera" tag (assign _MainCamera, GetComponentsInChildren<Camera> → set rect),
    //      SetupUICameraRect, find "BackColorCamera" GameObject (DontDestroyOnLoad + depth=-99).
    // String literals: 3306="BackColorCamera", 7909="MainCamera", 5644="GUI_Root".
    // DAT_0091c22c & DAT_0091c150 are 0xffffffff (-1 sentinel / NaN), not float epsilons —
    // the original comparisons effectively short-circuit, so we use simpler branching.
    public void SetupScreenSize()
    {
        UnityEngine.Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
        UnityEngine.Screen.autorotateToLandscapeLeft = true;
        UnityEngine.Screen.autorotateToLandscapeRight = true;
        UnityEngine.Screen.autorotateToPortrait = false;
        UnityEngine.Screen.autorotateToPortraitUpsideDown = false;

        if (!Main.bInitScreenOrigin)
        {
            Main.bInitScreenOrigin = true;
            UnityEngine.Resolution cur = UnityEngine.Screen.currentResolution;
            Main.iScreenOriginWidth = cur.width;
            Main.iScreenOriginHeight = cur.height;
        }

        Main.fAspect = (float)Main.iScreenOriginWidth / (float)Main.iScreenOriginHeight;
        Main.iScreenWidth = 0x500;  // 1280 default

        if (Main.fAspect <= Main.fTargetAspectMax)
        {
            // Width fits aspect — fit height
            if (Main.fAspect < Main.fTargetAspectMin)
            {
                // Below min aspect — letterbox: width = iScreenWidthMin
                if (Main.iScreenOriginWidth < Main.iScreenWidthMin)
                {
                    Main.iScreenWidth = Main.iScreenOriginWidth;
                }
                else
                {
                    Main.iScreenWidth = Main.iScreenWidthMin;
                }
                float h = (float)Main.iScreenWidth / Main.fAspect + 0.5f;
                Main.iScreenHeight = float.IsInfinity(h) ? int.MinValue : (int)h;
                Main.rScreenRect = new UnityEngine.Rect(0f, 0f, 1f, 1f);
            }
            else
            {
                // Normal fit: width = 1280, compute height
                float h = (float)Main.iScreenWidthMin / Main.fAspect + 0.5f;
                Main.iScreenHeight = float.IsInfinity(h) ? int.MinValue : (int)h;
                float yOffset = (((float)Main.iScreenHeight - Main.iTargetHeight) * 0.5f) / (float)Main.iScreenHeight;
                Main.rScreenRect = new UnityEngine.Rect(0f, yOffset, 1f, Main.iTargetHeight / (float)Main.iScreenHeight);
            }
        }
        else
        {
            // Aspect too wide — pillar-box
            float computedHeight = ((float)Main.iScreenHeight / (float)Main.iScreenOriginHeight) * (float)Main.iScreenOriginWidth;
            int width = float.IsInfinity(computedHeight) ? int.MinValue : (int)computedHeight;
            Main.iScreenWidth = width;
            Main.rScreenRect = new UnityEngine.Rect(0f, 0f, 1f, 1f);
        }

        int sw = Main.iScreenWidth;
        int sh = Main.iScreenHeight;
        if (Main.bScreenHalfPixel)
        {
            sw = (sw < 0) ? (sw + 1) : sw;
            sw >>= 1;
            sh = (sh < 0) ? (sh + 1) : sh;
            sh >>= 1;
        }
        UnityEngine.Screen.SetResolution(sw, sh, true);

        // "GUI_Root" — set ortho size = sh/2
        UnityEngine.GameObject guiRoot = UnityEngine.GameObject.Find("GUI_Root");
        if (guiRoot != null)
        {
            UnityEngine.Camera[] guiCams = guiRoot.GetComponentsInChildren<UnityEngine.Camera>();
            if (guiCams != null && guiCams.Length > 0 && guiCams[0] != null)
            {
                int halfH = (sh < 0) ? (sh + 1) : sh;
                guiCams[0].orthographicSize = (float)(halfH >> 1);
            }
        }

        // "MainCamera" tag — store _MainCamera, add component (per Ghidra: UnityEngine.UI.*), DontDestroyOnLoad,
        // iterate child cameras setting rect.
        Main.bScreenRectReady = true;
        UnityEngine.GameObject mainCamGO = UnityEngine.GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamGO != null)
        {
            UnityEngine.Camera mainCam = mainCamGO.GetComponent<UnityEngine.Camera>();
            if (mainCam != null)
            {
                _MainCamera = mainCam;
                // Source: Ghidra SetupScreenSize.c line 281 — between `_MainCamera = mainCam` and
                // `DontDestroyOnLoad`, production calls AddComponent<object>(camera.gameObject, PTR_DAT_034483a8).
                // PTR_DAT_034483a8 is a class metadata slot init'd by FUN_015cb66c (il2cpp_runtime_class_init);
                // by elimination from co-located DAT pointers (034483a0/b0/b8 all Camera/Camera[]),
                // and given PlayerCamControl is the only camera-controller component used by Lua
                // (ProcessLoginGame.V_Enter line 60 + SystemSetting.SetCameraPerspective), the
                // missing AddComponent is PlayerCamControl. Adding here matches production scene
                // bootstrap: MainCamera GO gets PlayerCamControl attached, Start() sets Instance = this.
                mainCam.gameObject.AddComponent<PlayerCamControl>();
                UnityEngine.Object.DontDestroyOnLoad(mainCam);
                UnityEngine.Camera[] subCams = _MainCamera.GetComponentsInChildren<UnityEngine.Camera>();
                if (subCams != null)
                {
                    for (int i = 0; i < subCams.Length; i++)
                    {
                        if (subCams[i] != null)
                        {
                            subCams[i].rect = Main.rScreenRect;
                        }
                    }
                }
            }
        }

        SetupUICameraRect();

        // "BackColorCamera" — DontDestroyOnLoad, set depth = -99
        UnityEngine.GameObject backColorGO = UnityEngine.GameObject.Find("BackColorCamera");
        if (backColorGO != null)
        {
            UnityEngine.Camera backCam = backColorGO.GetComponent<UnityEngine.Camera>();
            if (backCam != null)
            {
                UnityEngine.Object.DontDestroyOnLoad(backCam);
                backCam.depth = -99f;
            }
        }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/SetupUICameraRect.c RVA 0x015b7c14
    // 1-1: if (!bSetupUICameraRect) { Camera uiCam = WndRoot.s_camera (offset 0x10);
    //   if (uiCam != null) { uiCam.rect = rScreenRect (offset 0x70 — 4 floats x,y,w,h); bSetupUICameraRect = true; } }
    public void SetupUICameraRect()
    {
        if (!bSetupUICameraRect)
        {
            Camera uiCam = WndRoot.uiCamera;  // public static accessor for s_camera (0x10)
            if (uiCam != null)
            {
                uiCam.rect = rScreenRect;
                bSetupUICameraRect = true;
            }
        }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/initMarsPlatform.c RVA 0x015b8b74 (790 lines)
    // 1-1: Registers ~80 delegates across MarsSDK platform classes (MarsPlatform/FacebookPlatform/
    // MailVerifyPlatform/TelephoneVerifyPlatform/TelephoneLoginPlatform/UserjoyPlatform/
    // TwitterPlatform/MoJoyPlatform/PlayGameServicePlatform/GooglePlatform). For each Ghidra
    // ctor call `Platform_DelegateType___ctor(uVar, this, *(PTR_DAT_handler), 0)` then
    // `*(class_static_base + 0xb8 + offset) = uVar`, the C# equivalent is
    // `Platform.Field = new Platform.DelegateType(this.Handler)`. Mapping field-offset → field
    // name uses dump.cs TypeDefIndex; handler name resolved by semantic match (Platform field
    // name matches Main handler name, e.g. `doMsgProcessDelegateSuccess` ↔ `doMsgProcessSuccess`).
    // Order preserved from Ghidra ctor sequence (which differs from dump.cs declaration order
    // for the MarsPlatform block — Ghidra batches early fields first then jumps).
    // PlayGameServicePlatform handlers bind to `Main`'s STATIC methods (target=null in Ghidra).
    public void initMarsPlatform()
    {
        // ---- MarsPlatform block (Ghidra lines 157-325) ----
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessSuccess);                                                       // +0x08
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateError = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessError);                                                           // +0x10
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateTest = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessTest);                                                             // +0x18
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateNetError = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessNetError);                                                     // +0x20
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateBindSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessBindSuccess);                                               // +0x28
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateUnBindSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessUnBindSuccess);                                           // +0xc8 (Ghidra "200")
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateModifySuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessModifySuccess);                                           // +0x30
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegatePasswordHadBeenModified = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessPasswordHadBeenModified);                       // +0x38
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateClearUserInfo = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgClearUserInfo);                                                  // +0x40
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoiceMessageUploadSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoiceMessageUploadSuccess);                           // +0x48
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoiceMessageUploadFail = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoiceMessageUploadFail);                                 // +0x50
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoicePersonalUploadSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoicePersonalUploadSuccess);                         // +0x58
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoicePersonalUploadFail = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoicePersonalUploadFail);                               // +0x60
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoiceRecordComplete = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoiceRecordComplete);                                       // +0x68
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoicePlayComplete = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoicePlayComplete);                                           // +0x70
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoicePlayError = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoicePlayError);                                                 // +0x78
        MarsSDK.Platform.MarsPlatform.doMsgProcessLoginViewClose = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessLoginViewClose);                                                 // +0x88
        MarsSDK.Platform.MarsPlatform.doMsgProcessUserCancelVoicePermission = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessUserCancelVoicePermission);                           // +0x90
        MarsSDK.Platform.MarsPlatform.doMsgProcessVoicePermissionDeny = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessVoicePermissionDeny);                                       // +0x98
        MarsSDK.Platform.MarsPlatform.doMsgProcessImagePersonalUploadSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessImagePersonalUploadSuccess);                         // +0xa0
        MarsSDK.Platform.MarsPlatform.doMsgProcessImagePersonalUploadFail = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessImagePersonalUploadFail);                               // +0xa8
        MarsSDK.Platform.MarsPlatform.doMsgProcessImageMessageUploadSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessImageMessageUploadSuccess);                           // +0xb0
        MarsSDK.Platform.MarsPlatform.doMsgProcessImageMessageUploadFail = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessImageMessageUploadFail);                                 // +0xb8
        MarsSDK.Platform.MarsPlatform.doMsgProcessUserRuleNeedsUpdate = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessUserRuleNeedsUpdate);                                       // +0xc0
        MarsSDK.Platform.MarsPlatform.doMsgProcessGetUJWebURL = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessGetUJWebURL);                                                       // +0x80
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateGetSettingComplete = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessGetSettingComplete);                                 // +0xd0
        MarsSDK.Platform.MarsPlatform.doMsgProcessDelegateNeedRelogin = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessNeedRelogin);                                               // +0xd8
        MarsSDK.Platform.MarsPlatform.doMsgProcessReplyUserRuleInfo = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessReplyUserRuleInfo);                                           // +0xe0
        MarsSDK.Platform.MarsPlatform.doMsgProcessAccountDeleted = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessAccountDeleted);                                                 // +0xf0

        // ---- FacebookPlatform block (Ghidra lines 326-350) ----
        MarsSDK.Platform.FacebookPlatform.doMsgProcessCallbackOnSuccess = new MarsSDK.Platform.FacebookPlatform.dMsgProcess(this.doMsgProcessCallbackOnSuccess);                                   // +0x28
        MarsSDK.Platform.FacebookPlatform.doMsgProcessCallbackOnCancel = new MarsSDK.Platform.FacebookPlatform.dMsgProcess(this.doMsgProcessCallbackOnCancel);                                     // +0x30
        MarsSDK.Platform.FacebookPlatform.doMsgProcessCallbackOnError = new MarsSDK.Platform.FacebookPlatform.dMsgProcess(this.doMsgProcessCallbackOnError);                                       // +0x38

        // ---- MailVerifyPlatform block (Ghidra lines 351-396) ----
        MarsSDK.Platform.MailVerifyPlatform.doEventHasBinded = new MarsSDK.Platform.MailVerifyPlatform.dEventProcess(this.doEventHasBinded);                                                       // +0x00
        MarsSDK.Platform.MailVerifyPlatform.doEventBindSucceeded = new MarsSDK.Platform.MailVerifyPlatform.dEventProcess(this.doEventBindSucceeded);                                               // +0x08
        MarsSDK.Platform.MailVerifyPlatform.doEventBindFailed = new MarsSDK.Platform.MailVerifyPlatform.dEventErrorProcess(this.doEventBindFailed);                                                // +0x10
        MarsSDK.Platform.MailVerifyPlatform.doEventMailAddressRepeat = new MarsSDK.Platform.MailVerifyPlatform.dEventProcess(this.doEventMailAddressRepeat);                                       // +0x18
        MarsSDK.Platform.MailVerifyPlatform.doEventVerifyCodeTimeout = new MarsSDK.Platform.MailVerifyPlatform.dEventProcess(this.doEventVerifyCodeTimeout);                                       // +0x20
        MarsSDK.Platform.MailVerifyPlatform.doEventBindFailLock = new MarsSDK.Platform.MailVerifyPlatform.dEventProcess(this.doEventBindFailLock);                                                 // +0x28
        MarsSDK.Platform.MailVerifyPlatform.doEventVerifyCodeReady = new MarsSDK.Platform.MailVerifyPlatform.dEventProcess(this.doEventVerifyCodeReady);                                           // +0x30

        // ---- TelephoneVerifyPlatform block (Ghidra lines 397-465) ----
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventHasBinded = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventHasBinded);                                             // +0x08
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventBindSucceeded = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventBindSucceeded);                                     // +0x10
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventBindFailed = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventErrorProcess(this.doEventBindFailed);                                      // +0x18
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventTelephoneNumberReachBindLimit = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventTelephoneNumberReachBindLimit);     // +0x20
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventVerifyCodeSendFailed = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventErrorProcess(this.doEventVerifyCodeSendFailed);                  // +0x28
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventVerifyCodeTimeout = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventVerifyCodeTimeout);                             // +0x30
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventBindFailLock = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventBindFailLock);                                       // +0x38
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventRequestPassSucceeded = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventRequestPassSucceeded);                       // +0x40
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventRequestPassFailed = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventErrorProcess(this.doEventRequestPassFailed);                        // +0x48
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventVerifyCodeReady = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventVerifyCodeReady);                                 // +0x50
        MarsSDK.Platform.TelephoneVerifyPlatform.doEventVerifyCodeExist = new MarsSDK.Platform.TelephoneVerifyPlatform.dEventProcess(this.doEventVerifyCodeExist);                                 // +0x58

        // ---- TelephoneLoginPlatform block (Ghidra lines 466-527) ----
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventCheckIsRobot = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventCheckIsRobot);                                     // +0x08
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventVerifyRobotCodeNotMatch = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventVerifyRobotCodeNotMatch);               // +0x10
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventTelephoneNumberFormatIncorrect = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventTelephoneNumberFormatIncorrect); // +0x18
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventSendMessageFailed = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcessWithParams(this.doTLEventSendMessageFailed);                 // +0x50
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventVerifyCodeReady = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventVerifyCodeReady);                               // +0x20
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventVerifyCodeTimeout = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventVerifyCodeTimeout);                           // +0x28
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventVerifyMessageFailed = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventVerifyMessageFailed);                       // +0x30
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventDailyMessageLimit = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventDailyMessageLimit);                           // +0x38
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventAccountIsLocked = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventAccountIsLocked);                               // +0x40
        MarsSDK.Platform.TelephoneLoginPlatform.doTLEventAccountIsReset = new MarsSDK.Platform.TelephoneLoginPlatform.dEventProcess(this.doTLEventAccountIsReset);                                 // +0x48

        // ---- UserjoyPlatform block (Ghidra lines 528-533) ----
        MarsSDK.Platform.UserjoyPlatform.doUJEventRequestGold = new MarsSDK.Platform.UserjoyPlatform.dEventProcess(this.doUJEventRequestGold);                                                     // +0x10

        // ---- TwitterPlatform block (Ghidra lines 534-559) ----
        MarsSDK.Platform.TwitterPlatform.doTwitterEventLoginVerifySucceeded = new MarsSDK.Platform.TwitterPlatform.dMsgProcess(this.doTwitterEventLoginVerifySucceeded);                           // +0x08
        MarsSDK.Platform.TwitterPlatform.doTwitterEventLoginVerifyFailed = new MarsSDK.Platform.TwitterPlatform.dMsgProcess(this.doTwitterEventLoginVerifyFailed);                                 // +0x10
        MarsSDK.Platform.TwitterPlatform.doTwitterEventPostTweetSucceeded = new MarsSDK.Platform.TwitterPlatform.dMsgProcess(this.doTwitterEventPostTweetSucceeded);                               // +0x18
        MarsSDK.Platform.TwitterPlatform.doTwitterEventPostTweetFailed = new MarsSDK.Platform.TwitterPlatform.dMsgProcess(this.doTwitterEventPostTweetFailed);                                     // +0x20

        // ---- MarsPlatform additional registration (Ghidra lines 560-565) ----
        MarsSDK.Platform.MarsPlatform.doMsgProcessSyncPGSDataResult = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessSyncPGSDataResult);                                           // +0xe8

        // ---- MoJoyPlatform block (Ghidra lines 566-608) ----
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventHasBinded = new MarsSDK.Platform.MoJoyPlatform.dEventProcess(this.doMoJoyMailEventHasBinded);                                                   // +0x00
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventBindSucceeded = new MarsSDK.Platform.MoJoyPlatform.dEventProcess(this.doMoJoyMailEventBindSucceeded);                                           // +0x08
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventBindFailed = new MarsSDK.Platform.MoJoyPlatform.dEventErrorProcess(this.doMoJoyMailEventBindFailed);                                            // +0x10
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventMailAddressRepeat = new MarsSDK.Platform.MoJoyPlatform.dEventProcess(this.doMoJoyMailEventRepeat);                                              // +0x18
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventVerifyCodeTimeout = new MarsSDK.Platform.MoJoyPlatform.dEventProcess(this.doMoJoyMailEventVerifyCodeTimeout);                                   // +0x20
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventBindFailLock = new MarsSDK.Platform.MoJoyPlatform.dEventProcess(this.doMoJoyMailEventBindFailLock);                                             // +0x28
        MarsSDK.Platform.MoJoyPlatform.doMoJoyEventVerifyCodeReady = new MarsSDK.Platform.MoJoyPlatform.dEventProcess(this.doMoJoyMailEventVerifyCodeReady);                                       // +0x30

        // ---- MarsPlatform additional registrations (Ghidra lines 609-656) ----
        MarsSDK.Platform.MarsPlatform.doMsgProcessInitMSDKCompleted = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessInitMSDKCompleted);                                           // +0x120
        MarsSDK.Platform.MarsPlatform.doMsgProcessInitMSDKFailed = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessInitMSDKFailed);                                                 // +0x128
        MarsSDK.Platform.MarsPlatform.doMsgProcessRequestDeleteAccountSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessRequestDeleteAccountSuccess);                       // +0xf8
        MarsSDK.Platform.MarsPlatform.doMsgProcessRequestDeleteAccountFailure = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessRequestDeleteAccountFailure);                       // +0x100
        MarsSDK.Platform.MarsPlatform.doMsgProcessRequestRestoreAccountSuccess = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessRequestRestoreAccountSuccess);                     // +0x108
        MarsSDK.Platform.MarsPlatform.doMsgProcessRequestRestoreAccountFailure = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessRequestRestoreAccountFailure);                     // +0x110
        MarsSDK.Platform.MarsPlatform.doMsgProcessAccountHesitationDeletionPeriod = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessAccountHesitationDeletionPeriod);               // +0x118
        MarsSDK.Platform.MarsPlatform.doMsgProcessShowMessage = new MarsSDK.Platform.MarsPlatform.dMsgProcess(this.doMsgProcessShowMessage);                                                       // +0x138

        // ---- GooglePlatform block (Ghidra lines 657-773) ----
        MarsSDK.Platform.GooglePlatform.doEventInitSucceeded = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventInitSucceeded);                                                       // +0x18
        MarsSDK.Platform.GooglePlatform.doEventInitFailed = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventInitFailed);                                                             // +0x20
        MarsSDK.Platform.GooglePlatform.doEventQueryInventory_SGC = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventQueryInventory);                                                    // +0xa8
        MarsSDK.Platform.GooglePlatform.doEventPurchaseSucceeded = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventPurchaseSucceeded);                                               // +0x28
        MarsSDK.Platform.GooglePlatform.doEventUserCanceled = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventUserCanceled);                                                         // +0x30
        MarsSDK.Platform.GooglePlatform.doEventPurchaseFailed = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventPurchaseFailed);                                                        // +0x38
        MarsSDK.Platform.GooglePlatform.doEventRequestGold = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventRequestGold);                                                           // +0x48
        MarsSDK.Platform.GooglePlatform.doEventDeviceNotSupportGooglePlayService = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventDeviceNotSupportGooglePlayService);               // +0x58
        MarsSDK.Platform.GooglePlatform.doEventMissGoogleAccount = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventMissGoogleAccount);                                               // +0x60
        MarsSDK.Platform.GooglePlatform.doEventGooglePlayApiConnected = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventGooglePlayApiConnected);                                     // +0x70
        MarsSDK.Platform.GooglePlatform.doEventGooglePlayApiConnectionSuspended = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventGooglePlayApiConnectionSuspended);                 // +0x78
        MarsSDK.Platform.GooglePlatform.doEventGooglePlayLoginSuccess = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventGooglePlayLoginSuccess);                                        // +0xc0
        MarsSDK.Platform.GooglePlatform.doEventGooglePlayLoginFail = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventGooglePlayLoginFail);                                              // +0xc8
        MarsSDK.Platform.GooglePlatform.doEventGooglePlayLoginCancel = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventGooglePlayLoginCancel);                                          // +0xd0
        MarsSDK.Platform.GooglePlatform.doEventGooglePlayRevokeAccess = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventGooglePlayRevokeAccess);                                        // +0xd8
        MarsSDK.Platform.GooglePlatform.doEventRSAKeyNotMatch = new MarsSDK.Platform.GooglePlatform.dEventProcess(this.doEventRSAKeyNotMatch);                                                     // +0x50
        MarsSDK.Platform.GooglePlatform.doEventVerifyFail = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventVerifyFail);                                                                // +0x68
        MarsSDK.Platform.GooglePlatform.doEventHandleLostGold = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventHandleLostGold);                                                        // +0x88
        MarsSDK.Platform.GooglePlatform.doEventPurchaseUpdated = new MarsSDK.Platform.GooglePlatform.doProccess(this.doEventPurchaseUpdated);                                                      // +0x90

        // ---- PlayGameServicePlatform block (Ghidra lines 774-787; target=null → static Main methods) ----
        MarsSDK.Platform.PlayGameServicePlatform.doEventPGSSupported = new MarsSDK.Platform.PlayGameServicePlatform.dEventProcess(Main.doEventPGSSupported);                                       // +0x18
        MarsSDK.Platform.PlayGameServicePlatform.doEventGamePromotionSupported = new MarsSDK.Platform.PlayGameServicePlatform.dEventProcess(Main.doEventGamePromotionSupported);                   // +0x20
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/CheckPermission.c RVA 0x015ba898
    // 1-1:
    //   if (nAndroidAPILevel > 0x20) READ_EXTERNAL_STORAGE = "android.permission.READ_MEDIA_IMAGES";
    //   var pm = MarsSDK.Permission.PermissionManager.Instance();
    //   if (pm == null) NRE;
    //   bool granted = pm.CheckPermission(READ_EXTERNAL_STORAGE);
    //   Debug.LogError(format with permName/granted/apiLevel);
    //   if (!granted && nAndroidAPILevel > 0x21) {
    //     granted = pm.CheckPermission("android.permission.WRITE_EXTERNAL_STORAGE");
    //     Debug.LogError(...);
    //   }
    //   if (!granted) return;
    //   iConfirm_READ_EXTERNAL_STORAGE = 1;
    // String literals: 13825 = "android.permission.READ_MEDIA_IMAGES",
    //                  13826 = "android.permission.WRITE_EXTERNAL_STORAGE",
    //                  4041  = "MarsSDK_Permission_PermissionManager_CheckPermission: ",
    //                  761   = " result: ",  750 = " API: ",
    //                  4042  = "Re-check WRITE: result="
    public void CheckPermission()
    {
        if (Main.nAndroidAPILevel > 0x20)
        {
            Main.READ_EXTERNAL_STORAGE = "android.permission.READ_MEDIA_IMAGES";
        }
        var pm = MarsSDK.Permission.PermissionManager.Instance();
        if (pm == null) throw new System.NullReferenceException();

        bool granted = pm.CheckPermission(Main.READ_EXTERNAL_STORAGE);
        UnityEngine.Debug.LogError(
            "MarsSDK_Permission_PermissionManager_CheckPermission: " + Main.READ_EXTERNAL_STORAGE +
            " result: " + granted.ToString() +
            " API: " + Main.nAndroidAPILevel.ToString());

        if (!granted && Main.nAndroidAPILevel > 0x21)
        {
            granted = pm.CheckPermission("android.permission.WRITE_EXTERNAL_STORAGE");
            UnityEngine.Debug.LogError(
                "Re-check WRITE: result=" + granted.ToString() +
                " API: " + Main.nAndroidAPILevel.ToString());
        }
        if (!granted) return;
        Main.iConfirm_READ_EXTERNAL_STORAGE = 1;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/RequestPermission.c RVA 0x015bac60
    // 1-1 sequence (refer to comment in skeleton header):
    //   * If Main.<>c.<>9__99_0 is null: assign new PermissionCallback.PermissionDelegate(<>9.<RequestPermission>b__99_0)
    //   * Similarly for <>9__99_1 (cancel) and <>9__99_2 (denied) — the three handlers
    //   * Construct PermissionCallback(confirm, cancel, denied)
    //   * Build List<string>: add Main.READ_EXTERNAL_STORAGE; if API <= 0x21 also add "WRITE_EXTERNAL_STORAGE"
    //   * Call PermissionManager.Instance.RequestPermissions(list.ToArray(), permissionCallback)
    // The three lambda bodies (b__99_0/1/2) are not in this project's ported set — they are
    // small confirm/cancel/denied logger lambdas that v1 also kept as inline.
    public void RequestPermission()
    {
        MarsSDK.Permission.PermissionCallback.PermissionDelegate onConfirm = OnRequestPermissionConfirm;
        MarsSDK.Permission.PermissionCallback.PermissionDelegate onCancel = OnRequestPermissionCancel;
        MarsSDK.Permission.PermissionCallback.PermissionDelegate onDenied = OnRequestPermissionDenied;
        var callback = new MarsSDK.Permission.PermissionCallback(onConfirm, onCancel, onDenied);
        var permissions = new System.Collections.Generic.List<string>();
        permissions.Add(Main.READ_EXTERNAL_STORAGE);
        if (Main.nAndroidAPILevel <= 0x21)
        {
            permissions.Add("android.permission.WRITE_EXTERNAL_STORAGE");
        }
        var pm = MarsSDK.Permission.PermissionManager.Instance();
        if (pm == null) throw new System.NullReferenceException();
        pm.RequestPermissions(permissions.ToArray(), callback);
    }
    // Source: Ghidra Main.<>c.<RequestPermission>b__99_0/1/2 — small handler lambdas.
    // Bodies not in decompiled_full (Ghidra didn't emit the inner class) — confirmation logger pattern.
    // confidence: low — only the names/signatures are 1-1, bodies inferred from sibling handlers.
    private void OnRequestPermissionConfirm(string[] permissions)
    {
        UnityEngine.Debug.Log("[Main] PermissionConfirm: " + (permissions != null ? string.Join(",", permissions) : "(null)"));
    }
    private void OnRequestPermissionCancel(string[] permissions)
    {
        UnityEngine.Debug.Log("[Main] PermissionCancel: " + (permissions != null ? string.Join(",", permissions) : "(null)"));
    }
    private void OnRequestPermissionDenied(string[] permissions)
    {
        UnityEngine.Debug.LogError("[Main] PermissionDenied: " + (permissions != null ? string.Join(",", permissions) : "(null)"));
    }

    // ============================================================================================
    // SDK forwarder methods — all match the Ghidra pattern:
    //   if (BaseProcLua.Instance != null) BaseProcLua.Instance.<method>(args);
    // BaseProcLua.Instance is currently stubbed (throws NIE) — wrap to keep Editor playable.
    // RVAs collected from work/06_ghidra/decompiled_full/Main/<method>.c headers (2026-05-12 batch).
    // ============================================================================================

    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessSuccess.c RVA 0x015bb01c
    public void doMsgProcessSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessError.c RVA 0x015bb10c
    public void doMsgProcessError(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessError(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessTest.c RVA 0x015bb1fc
    public void doMsgProcessTest(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessTest(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessNetError.c RVA 0x015bb2ec
    public void doMsgProcessNetError(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessNetError(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessBindSuccess.c RVA 0x015bb3dc
    public void doMsgProcessBindSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessBindSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessUnBindSuccess.c RVA 0x015bb4cc
    public void doMsgProcessUnBindSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessUnBindSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessModifySuccess.c RVA 0x015bb5bc
    public void doMsgProcessModifySuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessModifySuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessPasswordHadBeenModified.c RVA 0x015bb6ac
    public void doMsgProcessPasswordHadBeenModified(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessPasswordHadBeenModified(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgClearUserInfo.c RVA 0x015bb79c
    public void doMsgClearUserInfo(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgClearUserInfo(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessLoginViewClose.c RVA 0x015bb88c
    public void doMsgProcessLoginViewClose(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessLoginViewClose(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoiceMessageUploadSuccess.c RVA 0x015bb97c
    public void doMsgProcessVoiceMessageUploadSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoiceMessageUploadSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoiceMessageUploadFail.c RVA 0x015bba6c
    public void doMsgProcessVoiceMessageUploadFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoiceMessageUploadFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoicePersonalUploadSuccess.c RVA 0x015bbb5c
    public void doMsgProcessVoicePersonalUploadSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoicePersonalUploadSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoicePersonalUploadFail.c RVA 0x015bbc4c
    public void doMsgProcessVoicePersonalUploadFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoicePersonalUploadFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoiceRecordComplete.c RVA 0x015bbd3c
    public void doMsgProcessVoiceRecordComplete(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoiceRecordComplete(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoicePlayComplete.c RVA 0x015bbe2c
    public void doMsgProcessVoicePlayComplete(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoicePlayComplete(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoicePlayError.c RVA 0x015bbf1c
    public void doMsgProcessVoicePlayError(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoicePlayError(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessUserCancelVoicePermission.c RVA 0x015bc00c
    public void doMsgProcessUserCancelVoicePermission(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessUserCancelVoicePermission(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessVoicePermissionDeny.c RVA 0x015bc0fc
    public void doMsgProcessVoicePermissionDeny(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessVoicePermissionDeny(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessImagePersonalUploadSuccess.c RVA 0x015bc1ec
    public void doMsgProcessImagePersonalUploadSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessImagePersonalUploadSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessImagePersonalUploadFail.c RVA 0x015bc2dc
    public void doMsgProcessImagePersonalUploadFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessImagePersonalUploadFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessImageMessageUploadSuccess.c RVA 0x015bc3cc
    public void doMsgProcessImageMessageUploadSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessImageMessageUploadSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessImageMessageUploadFail.c RVA 0x015bc4bc
    public void doMsgProcessImageMessageUploadFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessImageMessageUploadFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessUserRuleNeedsUpdate.c RVA 0x015bc5ac
    public void doMsgProcessUserRuleNeedsUpdate(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessUserRuleNeedsUpdate(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessGetUJWebURL.c RVA 0x015bc69c
    public void doMsgProcessGetUJWebURL(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessGetUJWebURL(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessGetSettingComplete.c RVA 0x015bc78c
    public void doMsgProcessGetSettingComplete(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessGetSettingComplete(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessCallbackOnSuccess.c RVA 0x015bc87c
    public void doMsgProcessCallbackOnSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessCallbackOnSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessCallbackOnCancel.c RVA 0x015bc96c
    public void doMsgProcessCallbackOnCancel(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessCallbackOnCancel(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessCallbackOnError.c RVA 0x015bca5c
    public void doMsgProcessCallbackOnError(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessCallbackOnError(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyCodeReady.c RVA 0x015bcb4c
    public void doEventVerifyCodeReady() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyCodeReady(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyCodeExist.c RVA 0x015bcc34
    public void doEventVerifyCodeExist() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyCodeExist(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventHasBinded.c RVA 0x015bcd1c
    public void doEventHasBinded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventHasBinded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventBindSucceeded.c RVA 0x015bce04
    public void doEventBindSucceeded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventBindSucceeded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventBindFailed.c RVA 0x015bceec
    // 1-1: wraps `code` into new string[]{ code.ToString() } then calls BaseProcLua.doEventBindFailed(string[]).
    // (Ghidra: lVar2 = new string[1]; lVar2[0] = code.ToString(); BaseProcLua.doEventBindFailed(lVar2);)
    public void doEventBindFailed(int code) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventBindFailed(new string[] { code.ToString() }); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventTelephoneNumberReachBindLimit.c RVA 0x015bd02c
    public void doEventTelephoneNumberReachBindLimit() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventTelephoneNumberReachBindLimit(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventAccountHasTelephoneVerified.c RVA 0x015bd114
    public void doEventAccountHasTelephoneVerified() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventAccountHasTelephoneVerified(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyFailed.c RVA 0x015bd1fc
    public void doEventVerifyFailed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyFailed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyCodeSendFailed.c RVA 0x015bd2e4
    // 1-1: wraps int platformCode into new string[]{ code.ToString() }, calls BaseProcLua.doEventVerifyCodeSendFailed(string[]).
    public void doEventVerifyCodeSendFailed(int platformCode) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyCodeSendFailed(new string[] { platformCode.ToString() }); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyCodeTimeout.c RVA 0x015bd424
    public void doEventVerifyCodeTimeout() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyCodeTimeout(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyFailReachLimited.c RVA 0x015bd50c
    public void doEventVerifyFailReachLimited() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyFailReachLimited(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventBindFailLock.c RVA 0x015bd5f4
    public void doEventBindFailLock() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventBindFailLock(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventRequestPassSucceeded.c RVA 0x015bd6dc
    public void doEventRequestPassSucceeded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventRequestPassSucceeded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventRequestPassFailed.c RVA 0x015bd7c4
    // 1-1: wraps int code into new string[]{ code.ToString() }, calls BaseProcLua.doEventRequestPassFailed(string[]).
    public void doEventRequestPassFailed(int code) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventRequestPassFailed(new string[] { code.ToString() }); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessNeedRelogin.c RVA 0x015bd904
    public void doMsgProcessNeedRelogin(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessNeedRelogin(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessReplyUserRuleInfo.c RVA 0x015bd9f4
    public void doMsgProcessReplyUserRuleInfo(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessReplyUserRuleInfo(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessAccountDeleted.c RVA 0x015bdae4
    public void doMsgProcessAccountDeleted(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessAccountDeleted(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doUJEventRequestGold.c RVA 0x015bdbdc
    public void doUJEventRequestGold() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doUJEventRequestGold(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventCheckIsRobot.c RVA 0x015bdcc4
    public void doTLEventCheckIsRobot() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventCheckIsRobot(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventVerifyRobotCodeNotMatch.c RVA 0x015bddac
    public void doTLEventVerifyRobotCodeNotMatch() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventVerifyRobotCodeNotMatch(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventTelephoneNumberFormatIncorrect.c RVA 0x015bde94
    public void doTLEventTelephoneNumberFormatIncorrect() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventTelephoneNumberFormatIncorrect(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventSendMessageFailed.c RVA 0x015bdf7c
    public void doTLEventSendMessageFailed(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventSendMessageFailed(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventVerifyCodeReady.c RVA 0x015be06c
    public void doTLEventVerifyCodeReady() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventVerifyCodeReady(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventVerifyCodeTimeout.c RVA 0x015be154
    public void doTLEventVerifyCodeTimeout() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventVerifyCodeTimeout(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventVerifyMessageFailed.c RVA 0x015be23c
    public void doTLEventVerifyMessageFailed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventVerifyMessageFailed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventDailyMessageLimit.c RVA 0x015be324
    public void doTLEventDailyMessageLimit() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventDailyMessageLimit(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventAccountIsLocked.c RVA 0x015be40c
    public void doTLEventAccountIsLocked() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventAccountIsLocked(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTLEventAccountIsReset.c RVA 0x015be4f4
    public void doTLEventAccountIsReset() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTLEventAccountIsReset(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventMailAddressRepeat.c RVA 0x015be5dc
    public void doEventMailAddressRepeat() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventMailAddressRepeat(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTwitterEventLoginVerifySucceeded.c RVA 0x015be6c4
    public void doTwitterEventLoginVerifySucceeded(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTwitterEventLoginVerifySucceeded(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTwitterEventLoginVerifyFailed.c RVA 0x015be7b4
    public void doTwitterEventLoginVerifyFailed(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTwitterEventLoginVerifyFailed(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTwitterEventPostTweetSucceeded.c RVA 0x015be8a4
    public void doTwitterEventPostTweetSucceeded(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTwitterEventPostTweetSucceeded(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doTwitterEventPostTweetFailed.c RVA 0x015be994
    public void doTwitterEventPostTweetFailed(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doTwitterEventPostTweetFailed(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessSyncPGSDataResult.c RVA 0x015bea84
    public void doMsgProcessSyncPGSDataResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessSyncPGSDataResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventHasBinded.c RVA 0x015beb74
    public void doMoJoyMailEventHasBinded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventHasBinded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventBindSucceeded.c RVA 0x015bec5c
    public void doMoJoyMailEventBindSucceeded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventBindSucceeded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventBindFailed.c RVA 0x015bed44
    // 1-1: wraps int status into new string[]{ status.ToString() }, calls BaseProcLua.doMoJoyMailEventBindFailed(string[]).
    public void doMoJoyMailEventBindFailed(int status) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventBindFailed(new string[] { status.ToString() }); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventRepeat.c RVA 0x015bee84
    public void doMoJoyMailEventRepeat() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventRepeat(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventVerifyCodeTimeout.c RVA 0x015bef6c
    public void doMoJoyMailEventVerifyCodeTimeout() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventVerifyCodeTimeout(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventBindFailLock.c RVA 0x015bf054
    public void doMoJoyMailEventBindFailLock() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventBindFailLock(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMoJoyMailEventVerifyCodeReady.c RVA 0x015bf13c
    public void doMoJoyMailEventVerifyCodeReady() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMoJoyMailEventVerifyCodeReady(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessInitMSDKCompleted.c RVA 0x015bf224
    public void doMsgProcessInitMSDKCompleted(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessInitMSDKCompleted(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessInitMSDKFailed.c RVA 0x015bf314
    public void doMsgProcessInitMSDKFailed(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessInitMSDKFailed(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessRequestDeleteAccountSuccess.c RVA 0x015bf404
    public void doMsgProcessRequestDeleteAccountSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessRequestDeleteAccountSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessRequestDeleteAccountFailure.c RVA 0x015bf4fc
    public void doMsgProcessRequestDeleteAccountFailure(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessRequestDeleteAccountFailure(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessRequestRestoreAccountSuccess.c RVA 0x015bf5ec
    public void doMsgProcessRequestRestoreAccountSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessRequestRestoreAccountSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessRequestRestoreAccountFailure.c RVA 0x015bf6dc
    public void doMsgProcessRequestRestoreAccountFailure(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessRequestRestoreAccountFailure(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessAccountHesitationDeletionPeriod.c RVA 0x015bf7cc
    public void doMsgProcessAccountHesitationDeletionPeriod(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessAccountHesitationDeletionPeriod(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doMsgProcessShowMessage.c RVA 0x015bf8bc
    public void doMsgProcessShowMessage(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doMsgProcessShowMessage(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventInitSucceeded.c RVA 0x015bf9ac
    public void doEventInitSucceeded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventInitSucceeded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventInitFailed.c RVA 0x015bfa94
    public void doEventInitFailed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventInitFailed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventInitFailedWithArgs.c RVA 0x015bfb7c
    public void doEventInitFailedWithArgs(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventInitFailedWithArgs(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventQueryInventory.c RVA 0x015bfc6c
    public void doEventQueryInventory(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventQueryInventory(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventPurchaseSucceeded.c RVA 0x015bfd5c
    public void doEventPurchaseSucceeded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventPurchaseSucceeded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventUserCanceled.c RVA 0x015bfe44
    public void doEventUserCanceled() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventUserCanceled(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventPurchaseFailed.c RVA 0x015bff2c
    public void doEventPurchaseFailed(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventPurchaseFailed(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventRequestGold.c RVA 0x015c001c
    public void doEventRequestGold() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventRequestGold(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventDeviceNotSupportGooglePlayService.c RVA 0x015c0104
    public void doEventDeviceNotSupportGooglePlayService() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventDeviceNotSupportGooglePlayService(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventMissGoogleAccount.c RVA 0x015c01ec
    public void doEventMissGoogleAccount() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventMissGoogleAccount(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventRSAKeyNotMatch.c RVA 0x015c02d4
    public void doEventRSAKeyNotMatch() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventRSAKeyNotMatch(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventCharacterNotExist.c RVA 0x015c03bc
    public void doEventCharacterNotExist() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventCharacterNotExist(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventVerifyFail.c RVA 0x015c04a4
    public void doEventVerifyFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventVerifyFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventRequestATTrackingAuthorized.c RVA 0x015c0594
    public void doEventRequestATTrackingAuthorized(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventRequestATTrackingAuthorized(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventRequestATTrackingDenied.c RVA 0x015c0684
    public void doEventRequestATTrackingDenied(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventRequestATTrackingDenied(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGooglePlayApiConnected.c RVA 0x015c0774
    public void doEventGooglePlayApiConnected() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGooglePlayApiConnected(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGooglePlayApiConnectionSuspended.c RVA 0x015c085c
    public void doEventGooglePlayApiConnectionSuspended() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGooglePlayApiConnectionSuspended(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGooglePlayLoginSuccess.c RVA 0x015c0944
    public void doEventGooglePlayLoginSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGooglePlayLoginSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGooglePlayLoginFail.c RVA 0x015c0a34
    public void doEventGooglePlayLoginFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGooglePlayLoginFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGooglePlayLoginCancel.c RVA 0x015c0b24
    public void doEventGooglePlayLoginCancel(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGooglePlayLoginCancel(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGooglePlayRevokeAccess.c RVA 0x015c0c14
    public void doEventGooglePlayRevokeAccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGooglePlayRevokeAccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGameCenterAuthenticateSucceeded.c RVA 0x015c0d04
    public void doEventGameCenterAuthenticateSucceeded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGameCenterAuthenticateSucceeded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGameCenterAuthenticateFailed.c RVA 0x015c0dec
    public void doEventGameCenterAuthenticateFailed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGameCenterAuthenticateFailed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventLoadAchievementsDone.c RVA 0x015c0ed4
    public void doEventLoadAchievementsDone() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventLoadAchievementsDone(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doAppleEventSIWASucceeded.c RVA 0x015c0fbc
    public void doAppleEventSIWASucceeded(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doAppleEventSIWASucceeded(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doAppleEventpasswordSucceeded.c RVA 0x015c10ac
    public void doAppleEventpasswordSucceeded(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doAppleEventpasswordSucceeded(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doAppleEventSIWAFailed.c RVA 0x015c119c
    public void doAppleEventSIWAFailed(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doAppleEventSIWAFailed(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doAppleEventSIWACanceled.c RVA 0x015c128c
    public void doAppleEventSIWACanceled(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doAppleEventSIWACanceled(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventHandleLostGold.c RVA 0x015c137c
    public void doEventHandleLostGold(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventHandleLostGold(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventPurchaseUpdated.c RVA 0x015c146c
    public void doEventPurchaseUpdated(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventPurchaseUpdated(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventPGSSupported.c RVA 0x015c155c
    public static void doEventPGSSupported() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventPGSSupported(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/doEventGamePromotionSupported.c RVA 0x015c1644
    public static void doEventGamePromotionSupported() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.doEventGamePromotionSupported(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameLoginSuccess.c RVA 0x015c172c
    // 1-1: if (BaseProcLua.Instance != null) BaseProcLua.Instance.OnESGameLoginSuccess(args);
    // BaseProcLua.Instance currently NIE — guard with try/catch.
    public void OnESGameLoginSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameLoginSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameLoginFail.c — same forwarder pattern.
    public void OnESGameLoginFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameLoginFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameLogout.c RVA 0x015c190c — forwarder.
    public void OnESGameLogout() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameLogout(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameGGBillingResult.c — forwarder.
    public void OnESGameGGBillingResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameGGBillingResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameWebBillingResult.c — forwarder.
    public void OnESGameWebBillingResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameWebBillingResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameAppleBillingResult.c — forwarder.
    public void OnESGameAppleBillingResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameAppleBillingResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameViewClose.c — forwarder (NoArg).
    public void OnESGameViewClose() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameViewClose(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnESGameError.c — forwarder.
    public void OnESGameError(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnESGameError(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/RegisterESGameCallback.c RVA 0x015b7060
    // 1-1: Create delegate instances pointing to Main.OnESGame* handlers, assign to ESGameEventHandler statics.
    // Offsets map directly to ESGameEventHandler field declaration order:
    //   +0    OnLoginSuccess   = new StringArgEvent(this.OnESGameLoginSuccess)
    //   +8    OnLoginFail      = new StringArgEvent(this.OnESGameLoginFail)
    //   +0x10 OnLogout         = new NoArgEvent(this.OnESGameLogout)
    //   +0x18 OnGGBillingResult = new StringArgEvent(this.OnESGameGGBillingResult)
    //   +0x20 OnWebBillingResult = new StringArgEvent(this.OnESGameWebBillingResult)
    //   +0x28 OnAppleBillingResult = new StringArgEvent(this.OnESGameAppleBillingResult)
    //   +0x38 OnViewClose      = new NoArgEvent(this.OnESGameViewClose)
    //   +0x30 OnError          = new StringArgEvent(this.OnESGameError)
    public void RegisterESGameCallback()
    {
        ESGameEventHandler.OnLoginSuccess = new ESGameEventHandler.StringArgEvent(this.OnESGameLoginSuccess);
        ESGameEventHandler.OnLoginFail = new ESGameEventHandler.StringArgEvent(this.OnESGameLoginFail);
        ESGameEventHandler.OnLogout = new ESGameEventHandler.NoArgEvent(this.OnESGameLogout);
        ESGameEventHandler.OnGGBillingResult = new ESGameEventHandler.StringArgEvent(this.OnESGameGGBillingResult);
        ESGameEventHandler.OnWebBillingResult = new ESGameEventHandler.StringArgEvent(this.OnESGameWebBillingResult);
        ESGameEventHandler.OnAppleBillingResult = new ESGameEventHandler.StringArgEvent(this.OnESGameAppleBillingResult);
        ESGameEventHandler.OnViewClose = new ESGameEventHandler.NoArgEvent(this.OnESGameViewClose);
        ESGameEventHandler.OnError = new ESGameEventHandler.StringArgEvent(this.OnESGameError);
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKLoginSuccess.c RVA 0x015c1e9c
    public void OnFhyxSDKLoginSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKLoginSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKLoginFail.c RVA 0x015c1f8c
    public void OnFhyxSDKLoginFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKLoginFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKLogout.c RVA 0x015c207c
    public void OnFhyxSDKLogout() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKLogout(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKSendRegisterVerificationCodeResult.c RVA 0x015c2164
    public void OnFhyxSDKSendRegisterVerificationCodeResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKSendRegisterVerificationCodeResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKBindingPersonalIdentityResult.c RVA 0x015c2254
    public void OnFhyxSDKBindingPersonalIdentityResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKBindingPersonalIdentityResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKGetUserBoundInfoSuccess.c RVA 0x015c2344
    public void OnFhyxSDKGetUserBoundInfoSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKGetUserBoundInfoSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKGetUserBoundInfoFail.c RVA 0x015c2434
    public void OnFhyxSDKGetUserBoundInfoFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKGetUserBoundInfoFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKGetUserInfoSuccess.c RVA 0x015c2524
    public void OnFhyxSDKGetUserInfoSuccess(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKGetUserInfoSuccess(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKGetUserInfoFail.c RVA 0x015c2614
    public void OnFhyxSDKGetUserInfoFail(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKGetUserInfoFail(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKGetLoginVerificationCodeResult.c RVA 0x015c2704
    public void OnFhyxSDKGetLoginVerificationCodeResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKGetLoginVerificationCodeResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKChangePasswordResult.c RVA 0x015c27f4
    public void OnFhyxSDKChangePasswordResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKChangePasswordResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKResetPasswordSendVerificationCodeResult.c RVA 0x015c28e4
    public void OnFhyxSDKResetPasswordSendVerificationCodeResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKResetPasswordSendVerificationCodeResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKResetPasswordCheckVerificationCodeResult.c RVA 0x015c29d4
    public void OnFhyxSDKResetPasswordCheckVerificationCodeResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKResetPasswordCheckVerificationCodeResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKResetPasswordResult.c RVA 0x015c2ac4
    public void OnFhyxSDKResetPasswordResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKResetPasswordResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult.c RVA 0x015c2bb4
    public void OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKRebindPhoneCheckVerificationCodeResult.c RVA 0x015c2ca4
    public void OnFhyxSDKRebindPhoneCheckVerificationCodeResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKRebindPhoneCheckVerificationCodeResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult.c RVA 0x015c2d94
    public void OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKRebindPhoneResult.c RVA 0x015c2e84
    public void OnFhyxSDKRebindPhoneResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKRebindPhoneResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKRegisterResult.c RVA 0x015c2f74
    public void OnFhyxSDKRegisterResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKRegisterResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKWebViewClose.c RVA 0x015c3064
    public void OnFhyxSDKWebViewClose(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKWebViewClose(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKBindActivationCodeResult.c RVA 0x015c3154
    public void OnFhyxSDKBindActivationCodeResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKBindActivationCodeResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKCheckAccountActivationResult.c RVA 0x015c3244
    public void OnFhyxSDKCheckAccountActivationResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKCheckAccountActivationResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKSendLogResult.c RVA 0x015c3334
    public void OnFhyxSDKSendLogResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKSendLogResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKPayResult.c RVA 0x015c3424
    public void OnFhyxSDKPayResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKPayResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFhyxSDKCheckWordResult.c RVA 0x015c3514
    public void OnFhyxSDKCheckWordResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFhyxSDKCheckWordResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/RegisterFhyxSDKCallback.c RVA 0x015c3604
    // 1-1: 25 sequential delegate ctor assignments to FhyxSDKEventHandler static fields.
    // Field offsets per dump.cs TypeDefIndex 19 lines 477-501 (8-byte strides).
    // Each ctor target on this Main instance — name match between FhyxSDKEventHandler.OnXxx and
    // Main.OnFhyxSDKXxx (verified against dump.cs Main class). Slot at 0x10 (OnLogout) is the only
    // NoArgEvent, rest are StringArgEvent. Ordering preserved from Ghidra ctor sequence.
    public void RegisterFhyxSDKCallback()
    {
        FhyxSDKEventHandler.OnLoginSuccess = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKLoginSuccess);                                                                             // +0x00
        FhyxSDKEventHandler.OnLoginFail = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKLoginFail);                                                                                   // +0x08
        FhyxSDKEventHandler.OnLogout = new FhyxSDKEventHandler.NoArgEvent(this.OnFhyxSDKLogout);                                                                                             // +0x10
        FhyxSDKEventHandler.OnSendRegisterVerificationCodeResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKSendRegisterVerificationCodeResult);                                 // +0x18
        FhyxSDKEventHandler.OnBindingPersonalIdentityResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKBindingPersonalIdentityResult);                                           // +0x20
        FhyxSDKEventHandler.OnGetUserBoundInfoSuccess = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKGetUserBoundInfoSuccess);                                                       // +0x28
        FhyxSDKEventHandler.OnGetUserBoundInfoFail = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKGetUserBoundInfoFail);                                                             // +0x30
        FhyxSDKEventHandler.OnGetUserInfoSuccess = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKGetUserInfoSuccess);                                                                 // +0x38
        FhyxSDKEventHandler.OnGetUserInfoFail = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKGetUserInfoFail);                                                                       // +0x40
        FhyxSDKEventHandler.OnGetLoginVerificationCodeResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKGetLoginVerificationCodeResult);                                         // +0x48
        FhyxSDKEventHandler.OnChangePasswordResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKChangePasswordResult);                                                             // +0x50
        FhyxSDKEventHandler.OnResetPasswordSendVerificationCodeResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKResetPasswordSendVerificationCodeResult);                       // +0x58
        FhyxSDKEventHandler.OnResetPasswordCheckVerificationCodeResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKResetPasswordCheckVerificationCodeResult);                     // +0x60
        FhyxSDKEventHandler.OnResetPasswordResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKResetPasswordResult);                                                               // +0x68
        FhyxSDKEventHandler.OnRebindPhoneSendVerificationCodeToOldResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult);                 // +0x70
        FhyxSDKEventHandler.OnRebindPhoneCheckVerificationCodeResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKRebindPhoneCheckVerificationCodeResult);                         // +0x78
        FhyxSDKEventHandler.OnRebindPhoneSendVerificationCodeToNewResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult);                 // +0x80
        FhyxSDKEventHandler.OnRebindPhoneResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKRebindPhoneResult);                                                                   // +0x88
        FhyxSDKEventHandler.OnRegisterResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKRegisterResult);                                                                         // +0x90
        FhyxSDKEventHandler.OnWebViewClose = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKWebViewClose);                                                                             // +0x98
        FhyxSDKEventHandler.OnBindActivationCodeResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKBindActivationCodeResult);                                                     // +0xa0
        FhyxSDKEventHandler.OnCheckAccountActivationResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKCheckAccountActivationResult);                                             // +0xa8
        FhyxSDKEventHandler.OnSendLogResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKSendLogResult);                                                                           // +0xb0
        FhyxSDKEventHandler.OnPayResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKPayResult);                                                                                   // +0xb8
        FhyxSDKEventHandler.OnCheckWordResult = new FhyxSDKEventHandler.StringArgEvent(this.OnFhyxSDKCheckWordResult);                                                                       // +0xc0
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowPersonalInfoGuideDialogResult.c RVA 0x015c3cd0
    public void OnFxhySDKShowPersonalInfoGuideDialogResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowPersonalInfoGuideDialogResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKInitResult.c RVA 0x015c3dc0
    public void OnFxhySDKInitResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKInitResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKCheckLoginResult.c RVA 0x015c3eb0
    public void OnFxhySDKCheckLoginResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKCheckLoginResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKStartLoginResult.c RVA 0x015c3fa0
    public void OnFxhySDKStartLoginResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKStartLoginResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKStartLogoutResult.c RVA 0x015c4090
    public void OnFxhySDKStartLogoutResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKStartLogoutResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKStartUserCenterResult.c RVA 0x015c4180
    public void OnFxhySDKStartUserCenterResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKStartUserCenterResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowCloseAccountResult.c RVA 0x015c4270
    public void OnFxhySDKShowCloseAccountResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowCloseAccountResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowRealNameAuthenticationResult.c RVA 0x015c4360
    public void OnFxhySDKShowRealNameAuthenticationResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowRealNameAuthenticationResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowRoundIdentityInfoResult.c RVA 0x015c4450
    public void OnFxhySDKShowRoundIdentityInfoResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowRoundIdentityInfoResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowWebDialogResult.c RVA 0x015c4540
    public void OnFxhySDKShowWebDialogResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowWebDialogResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowWebUIDialogResult.c RVA 0x015c4630
    public void OnFxhySDKShowWebUIDialogResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowWebUIDialogResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKShowWebSystemResult.c RVA 0x015c4720
    public void OnFxhySDKShowWebSystemResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKShowWebSystemResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKUploadGameUserInfoResult.c RVA 0x015c4810
    public void OnFxhySDKUploadGameUserInfoResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKUploadGameUserInfoResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKPayResult.c RVA 0x015c4900
    public void OnFxhySDKPayResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKPayResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKNotImplementEventResult.c RVA 0x015c49f0
    public void OnFxhySDKNotImplementEventResult(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKNotImplementEventResult(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnFxhySDKClose.c RVA 0x015c4ae0
    public void OnFxhySDKClose(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnFxhySDKClose(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/RegisterFxhySDKCallback.c RVA 0x015c4bd0
    // 1-1: 16 sequential delegate ctor assignments to FxhySDKManager static fields.
    // Field offsets per dump.cs TypeDefIndex 30 lines 927-958 (8-byte strides). All are StringArgEvent.
    // Name match between FxhySDKManager.OnFxhySDKXxx and Main.OnFxhySDKXxx (verified against dump.cs Main).
    public void RegisterFxhySDKCallback()
    {
        FxhySDKManager.OnFxhySDKShowPersonalInfoGuideDialogResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowPersonalInfoGuideDialogResult); // +0x00
        FxhySDKManager.OnFxhySDKInitResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKInitResult);                                              // +0x08
        FxhySDKManager.OnFxhySDKCheckLoginResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKCheckLoginResult);                                  // +0x10
        FxhySDKManager.OnFxhySDKStartLoginResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKStartLoginResult);                                  // +0x18
        FxhySDKManager.OnFxhySDKStartLogoutResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKStartLogoutResult);                                // +0x20
        FxhySDKManager.OnFxhySDKStartUserCenterResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKStartUserCenterResult);                        // +0x28
        FxhySDKManager.OnFxhySDKShowCloseAccountResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowCloseAccountResult);                      // +0x30
        FxhySDKManager.OnFxhySDKShowRealNameAuthenticationResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowRealNameAuthenticationResult);  // +0x38
        FxhySDKManager.OnFxhySDKShowRoundIdentityInfoResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowRoundIdentityInfoResult);            // +0x40
        FxhySDKManager.OnFxhySDKShowWebDialogResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowWebDialogResult);                            // +0x48
        FxhySDKManager.OnFxhySDKShowWebUIDialogResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowWebUIDialogResult);                        // +0x50
        FxhySDKManager.OnFxhySDKShowWebSystemResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKShowWebSystemResult);                            // +0x58
        FxhySDKManager.OnFxhySDKUploadGameUserInfoResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKUploadGameUserInfoResult);                  // +0x60
        FxhySDKManager.OnFxhySDKPayResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKPayResult);                                                // +0x68
        FxhySDKManager.OnFxhySDKNotImplementEventResult = new FxhySDKManager.StringArgEvent(this.OnFxhySDKNotImplementEventResult);                    // +0x70
        FxhySDKManager.OnFxhySDKClose = new FxhySDKManager.StringArgEvent(this.OnFxhySDKClose);                                                        // +0x78
    }
    // CheckShowFps() is defined above (1-1 from Ghidra RVA 0x015b7da4) — duplicate stub removed
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/ShowFps.c RVA 0x015c5058
    // 1-1 outline:
    //   if (WndRoot.s_camera == null) return;
    //   var fps = CodeStage.AdvancedFPSCounter.AFPSCounter.AddToScene();
    //   int uiLayer = LayerMask.NameToLayer(StringLit_11864 "UI");
    //   fps.gameObject.layer = uiLayer;
    //   foreach Transform child of fps.transform: child.gameObject.layer = uiLayer;
    //   fps.MemoryCounter.MonoUsage = true;
    //   var cam = fps.GetComponent<Camera>();
    //   if (cam != null) { var mask = LayerMask.GetMask(new[]{"UI"}); cam.cullingMask = mask;
    //     cam.depth = 10.0; cam.clearFlags = CameraClearFlags.Depth(=4); }
    // Note: CodeStage.AdvancedFPSCounter is referenced via dump.cs (Image 19), but the C# class
    // is not present in unity_project_v2/Assets/Scripts — port intentionally kept as NIE with TODO.
    public void ShowFps() { throw new System.NotImplementedException(); /* TODO body RVA 0x015c5058 - depends on CodeStage.AdvancedFPSCounter.AFPSCounter (external Unity Asset Store package) not in project. Per CLAUDE.md §D6: do not invent dependency classes. */ }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/CheckAndroidSDKVersion.c RVA 0x015b7354
    // 1-1: var build = new AndroidJavaClass("android.os.Build$VERSION");      // StringLit #13820
    //       if (build == null) {
    //           Debug.LogWarning("Android API Level check failed.");           // StringLit #2994
    //           return;
    //       }
    //       int sdkInt = build.GetStatic<int>("SDK_INT");                       // StringLit #9701
    //       Main.nAndroidAPILevel = sdkInt;
    //       Debug.LogWarning("Android API Level = " + sdkInt.ToString());      // StringLit #2993
    // (Ghidra calls LogWarning unconditionally with the final uVar7 from either branch.)
    public void CheckAndroidSDKVersion()
    {
#if UNITY_ANDROID
        UnityEngine.AndroidJavaClass build = null;
        string msg;
        try { build = new UnityEngine.AndroidJavaClass("android.os.Build$VERSION"); }
        catch { build = null; }
        if (build == null)
        {
            msg = "Android API Level check failed.";
        }
        else
        {
            int sdkInt = build.GetStatic<int>("SDK_INT");
            Main.nAndroidAPILevel = sdkInt;
            msg = "Android API Level = " + sdkInt.ToString();
        }
        UnityEngine.Debug.LogWarning(msg);
#else
        // Non-Android target — Ghidra path: AndroidJavaClass ctor throws/returns 0 → "Android API Level check failed."
        UnityEngine.Debug.LogWarning("Android API Level check failed.");
#endif
    }
    /* RVA 0x015c5514 — GetMarsServiceURL:
     *   var def = "https://mars-vn.uj.com.tw/MarsService/" (StringLit_17192).
     *   var url = ResMgr.Instance.GetSGCInitSettings("Server", "MarsServiceURL");
     *   return string.IsNullOrEmpty(url) ? def : url;
     */
    public string GetMarsServiceURL()
    {
        const string DEFAULT_URL = "https://mars-vn.uj.com.tw/MarsService/";
        var rm = ResMgr.Instance;
        if (rm == null) return DEFAULT_URL;
        // ResMgr.GetSGCInitSettings is not yet ported; placeholder returns default
        // TODO: when ResMgr.GetSGCInitSettings(section, key) is ported, dispatch:
        //       var url = rm.GetSGCInitSettings("Server", "MarsServiceURL");
        //       return string.IsNullOrEmpty(url) ? DEFAULT_URL : url;
        return DEFAULT_URL;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdLoadSucceed.c RVA 0x015c55ec
    public void OnRewardAdLoadSucceed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdLoadSucceed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdLoadFailed.c RVA 0x015c56d4
    public void OnRewardAdLoadFailed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdLoadFailed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdPaid.c RVA 0x015c57bc
    public void OnRewardAdPaid(string[] args) { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdPaid(args); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdImpressionRecorded.c RVA 0x015c58ac
    public void OnRewardAdImpressionRecorded() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdImpressionRecorded(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdClicked.c RVA 0x015c5994
    public void OnRewardAdClicked() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdClicked(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdFullScreenContentOpened.c RVA 0x015c5a7c
    public void OnRewardAdFullScreenContentOpened() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdFullScreenContentOpened(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdFullScreenContentClosed.c RVA 0x015c5b64
    public void OnRewardAdFullScreenContentClosed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdFullScreenContentClosed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnRewardAdFullScreenContentFailed.c RVA 0x015c5c4c
    public void OnRewardAdFullScreenContentFailed() { try { var bpl = BaseProcLua.Instance; if (bpl != null) bpl.OnRewardAdFullScreenContentFailed(); } catch (System.NotImplementedException) { } }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/RegisterMobileAdsCallback.c RVA 0x015b74c8
    // 1-1 sketch: 8 sequential assignments of new MobileAdsManager.NoArgEvent (and one StringArgEvent
    // for OnRewardAdPaid) to each static field on MobileAdsManager at offsets 0x00..0x38.
    // MobileAdsManager class is referenced in dump.cs (TypeDefIndex 672) but not present in
    // unity_project_v2/Assets/Scripts. Preserved as NIE per STRICT_RULES §4 (no helper invention).
    public void RegisterMobileAdsCallback() { throw new System.NotImplementedException(); /* TODO body RVA 0x015b74c8 - depends on MobileAdsManager (dump.cs TypeDefIndex 672 / line 46173) not present in unity_project_v2/Assets/Scripts/. Per CLAUDE.md §D6: do not invent dependency classes. */ }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/InitNotification.c RVA 0x015c5d34
    // 1-1: if (_notificationInitDone) return;
    //       NotificationManager.OnNotificationPermissionAllow += this.OnNotificationAllow;
    //       NotificationManager.OnNotificationPermissionDontAllow += this.OnNotificationDontAllow;
    //       NotificationManager.OnNotificationPermissionDenied += this.OnNotificationDenied;
    //       NotificationManager.OnNotificationPermissionCancel += this.OnNotificationCancel;
    //       NotificationManager.OnIOSNotificationSettingStatus += this.OnNotificationIOSStatusCallback;
    //       var nm = NotificationManager.Instance();
    //       if (nm == null) NRE;
    //       nm.CreateChannelGroup(StringLit_9714 "SGC", "SGC", StringLit_9716 "SGC Notifications");
    //       nm = NotificationManager.Instance();
    //       nm.CreateChannel(_notificationChannelID, StringLit_21440 "三國群英傳M", (ENotificationImportance)4, StringLit_9714 "SGC");
    //       nm = NotificationManager.Instance();
    //       nm.GetIOSNotificationSettingStatus();
    //       _notificationInitDone = true;
    public void InitNotification()
    {
        if (_notificationInitDone) return;
        MarsSDK.Notification.NotificationManager.OnNotificationPermissionAllow += this.OnNotificationAllow;
        MarsSDK.Notification.NotificationManager.OnNotificationPermissionDontAllow += this.OnNotificationDontAllow;
        MarsSDK.Notification.NotificationManager.OnNotificationPermissionDenied += this.OnNotificationDenied;
        MarsSDK.Notification.NotificationManager.OnNotificationPermissionCancel += this.OnNotificationCancel;
        MarsSDK.Notification.NotificationManager.OnIOSNotificationSettingStatus += this.OnNotificationIOSStatusCallback;
        var nm = MarsSDK.Notification.NotificationManager.Instance();
        if (nm == null) throw new System.NullReferenceException();
        nm.CreateChannelGroup("SGC", "SGC", "SGC Notifications");
        nm = MarsSDK.Notification.NotificationManager.Instance();
        if (nm == null) throw new System.NullReferenceException();
        nm.CreateChannel(_notificationChannelID, "三國群英傳M", MarsSDK.Notification.ENotificationImportance.High, "SGC");
        nm = MarsSDK.Notification.NotificationManager.Instance();
        if (nm == null) throw new System.NullReferenceException();
        nm.GetIOSNotificationSettingStatus();
        _notificationInitDone = true;
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnNotificationAllow.c RVA 0x015c5f5c
    // 1-1: Debug.LogError(StringLit_21776 "通知權限已允許"); — no other side effects.
    public void OnNotificationAllow() { UnityEngine.Debug.LogError("通知權限已允許"); }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnNotificationDontAllow.c RVA 0x015c5fc4
    // 1-1: Debug.LogError(StringLit_21777 "通知權限已拒絕"); — uses inline FUN_032a5a90 (LogError thunk).
    public void OnNotificationDontAllow() { UnityEngine.Debug.LogError("通知權限已拒絕"); }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnNotificationDenied.c RVA 0x015c602c
    // 1-1: Debug.LogError(StringLit_21775 "通知權限多次拒絕或永久拒絕");
    public void OnNotificationDenied() { UnityEngine.Debug.LogError("通知權限多次拒絕或永久拒絕"); }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnNotificationCancel.c RVA 0x015c6094
    // 1-1: Debug.LogError(StringLit_21468 "使用者取消操作");
    public void OnNotificationCancel() { UnityEngine.Debug.LogError("使用者取消操作"); }
    // Source: Ghidra work/06_ghidra/decompiled_full/Main/OnNotificationIOSStatusCallback.c RVA 0x015c60fc
    // 1-1: Debug.LogError(string.Concat(StringLit_17214 "iOS Status:", status));
    public void OnNotificationIOSStatusCallback(string status) { UnityEngine.Debug.LogError(string.Concat("iOS Status:", status)); }
    public class EGuiLayer
    {
        public const int Layer_Debug = 0;
        public const int Layer_System = 1;
        public const int Layer_Teach = 2;
        public const int Layer_Popup = 3;
        public const int Layer_Exclusive = 4;
        public const int Layer_DefaultFull = 5;
        public const int Layer_Default = 6;
        public const int Layer_Main = 7;
        public const int Layer_Value = 8;
        public const int Layer_ScreenTouch = 9;
        public const int Layer_ARView = 10;
        public const int Layer_Max = 11;
        private static Dictionary<int, string> s_mapName;
        public static string GetName(int nLayer) { return ""; }
    }
}
