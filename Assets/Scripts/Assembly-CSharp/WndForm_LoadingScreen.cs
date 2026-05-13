// Source: dump.cs class 'WndForm_LoadingScreen' (TypeDefIndex: 787)
// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LoadingScreen/ (31 .c files)

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

using UnityEngine.EventSystems;
using Game.Conf;
// Source: Il2CppDumper-stub  TypeDefIndex: 787
public class WndForm_LoadingScreen : WndForm
{
    // Source: dump.cs TypeDefIndex 787 declares ONE static field `s_instance` (see line 61
    //   below — single backing field). The earlier `_instance` duplicate has been removed
    //   so that V_Create's `s_instance = this` is visible through `Instance` (was a port bug).
    public static WndForm_LoadingScreen Instance { get { return s_instance; } }
    private static bool _showLoading;
    private static float _fProgress;
    // Source: Ghidra get_ready.c RVA 0x18F32CC
    //   1-1: return s_instance != null && s_instance.isDone;
    //   (Ghidra: load PTR_DAT_0345df98 class.static_fields[0] = s_instance; if non-null call WndForm.get_isDone.)
    //   Earlier port had a separate `_ready` static field — NEVER assigned by any method per Ghidra; bug.
    public static bool ready
    {
        get
        {
            var self = s_instance;
            return self != null && self.isDone;
        }
    }
    // Source: Ghidra get_showLoading.c + set_showLoading.c RVA 0x18F3400
    // get: returns _showLoading flag.
    // set: 1-1 from Ghidra (NOT a simple field assign):
    //   if s_instance == null OR s_instance.IsShow() == value → return (idempotent / no-op).
    //   if value == false: SetShow(false).
    //   else (value == true):
    //     if _bg_loadingGO == null → NRE.
    //     If _loadingFlag (static, plVar4[2]) == 0:
    //       _bg_loadingGO.SetActive(false);
    //       _bg_sceneGO.SetActive(true);  if null → NRE;
    //       _randomPic();
    //     Else:
    //       _bg_loadingGO.SetActive(true);
    //       _bg_sceneGO.SetActive(false); if null → NRE;
    //     SetShow(true);              // activate the WndForm GameObject
    //     curAlpha = 1.0f;
    //     _uiSprite_progress.fillAmount = 0;
    //     _PicIndex = 0;              // (+0xe8 byte clear; closest int field is _PicIndex)
    //     TimeCounter = 0;            // (+0x174 byte clear)
    //     _randomOneTip();
    //     _? at +0x100 cleared       (Ghidra clears 8 bytes — likely autoRandTimeCounter + randPicTimeCounter)
    public static bool showLoading
    {
        get { return _showLoading; }
        set
        {
            // Mirror the static flag for direct consumers (e.g. CheckShowFps).
            _showLoading = value;
            var self = s_instance;
            if (self == null) return;
            if (self.IsShow() == value) return;
            if (!value)
            {
                self.SetShow(false);
                return;
            }
            // value == true branch — show loading screen with appropriate BG.
            if (self._bg_loadingGO == null) throw new System.NullReferenceException();
            if (!_loadingFlag)
            {
                self._bg_loadingGO.SetActive(false);
                if (self._bg_sceneGO == null) throw new System.NullReferenceException();
                self._bg_sceneGO.SetActive(true);
                self._randomPic();
            }
            else
            {
                self._bg_loadingGO.SetActive(true);
                if (self._bg_sceneGO == null) throw new System.NullReferenceException();
                self._bg_sceneGO.SetActive(false);
            }
            self.SetShow(true);
            self.curAlpha = 1.0f;
            if (self._uiSprite_progress != null) self._uiSprite_progress.fillAmount = 0f;
            self.TimeCounter = 0f;
            self._barPersent = 0f;
            self._randomOneTip();
            self.autoRandTimeCounter = 0f;
            self.randPicTimeCounter = 0f;
        }
    }
    // Source: Ghidra get_Completed.c RVA 0x18F3CD0 + set_fProgress.c RVA 0x18F3978.
    // Bodies inlined into properties to avoid CS0082 collision with auto-generated get/set methods.
    public static bool Completed
    {
        get
        {
            WndForm_LoadingScreen self = s_instance;
            if (self == null) return false;
            if (self._uiSprite_progress == null) throw new System.NullReferenceException();
            return self._uiSprite_progress.fillAmount == 1.0f;
        }
    }
    public static float fProgress
    {
        get { return _fProgress; }
        set
        {
            WndForm_LoadingScreen self = s_instance;
            if (self == null) return;
            if (value == 0.0f)
            {
                self.TimeCounter = 0f;
                self._barPersent = 0f;
                self._ShowPercent = 0f;
            }
            if (self._uiSprite_progress != null)
            {
                self._uiSprite_progress.fillAmount = value;
                return;
            }
            throw new System.NullReferenceException();
        }
    }
    private static WndForm_LoadingScreen s_instance;
    internal Image _uiSprite_progress;
    private Image _LoadingBG;
    private Text _BarText;
    private Text _SizeText;
    private Text _CheckTitle;
    private Text _CheckContent;
    private Text _CheckBtnText;
    private GameObject Root_FirstLoading;
    private GameObject Root_DownLoadCheck;
    private GameObject Root_VersionCheck;
    private GameObject _downloadBG;
    private GameObject _barGO;
    private GameObject _adultImg;
    private GameObject _adultImg2;
    private Text TXT_ConfirmSize;
    private Text CurVersion;
    private Text BundleTime;
    internal float TimeCounter;
    private float loadingBarSpeed;
    private float fLimitTime;
    private Text _loadingTipText;
    private static readonly int startNo;
    private static readonly int endNo;
    private static int beginTipCount;
    private static int advanceTipCount;
    private static readonly float autoRandTime;
    private float autoRandTimeCounter;
    private static readonly float randPicTime;
    private float randPicTimeCounter;
    private int lastPic;
    private GameObject eff_00;
    private GameObject eff_01;
    private GameObject eff_02;
    private GameObject eff_03;
    private GameObject eff_04;
    private GameObject eff_05;
    private GameObject eff_06;
    private GameObject eff_07;
    private GameObject _bg_loadingGO;
    private GameObject _bg_sceneGO;
    private Text _checkWndBtnOKText;
    private Text _checkWndBtnCancelText;
    // Source: dump.cs WndForm_LoadingScreen static_fields offset 0x10 (line 49765).
    // Promoted to `internal` so AssetBundleManager.LoadAssetBundle can read it as the
    // happy-path gate (Ghidra: *(static_fields+0x10) != 0 || bForceUseBundle).
    internal static bool _loadingFlag;
    private int _PicIndex;
    internal float _ShowPercent;
    internal float _barPersent;
    private static int _characterlevel;

    // Source: Ghidra set_LoadingFlag.c RVA 0x18F327C — writes static field _loadingFlag.
    public static void set_LoadingFlag(bool value) { _loadingFlag = value; }

    // Source: Ghidra set_PicIndex.c RVA 0x18F3328 — writes instance field via s_instance.
    public static void set_PicIndex(int value)
    {
        if (s_instance != null) s_instance._PicIndex = value;
    }

    // Source: Ghidra set_ShowLoadingPercent.c RVA 0x18F391C — writes instance field _ShowPercent via s_instance.
    public static void set_ShowLoadingPercent(float value)
    {
        if (s_instance != null) s_instance._ShowPercent = value;
    }

    // Source: Ghidra set_fProgress.c RVA 0x18F3978 — body inlined in fProgress property above.

    // Source: Ghidra SetBarText.c RVA 0x18F3A08
    // 1. If sizetext != "" → text = text + " " + sizetext (string literal 109 == " ")
    // 2. If s_instance == null → return false.
    // 3. If s_instance._BarText == null → NRE.
    // 4. If s_instance._BarText.text != text → s_instance._BarText.text = text.
    // 5. return true.
    public static bool SetBarText(string text, string sizetext = "")
    {
        if (sizetext != "")
        {
            text = System.String.Concat(text, " ", sizetext);
        }
        WndForm_LoadingScreen self = s_instance;
        if (self == null) return false;
        if (self._BarText == null) throw new System.NullReferenceException();
        string cur = self._BarText.text;
        if (cur != text)
        {
            if (self._BarText == null) throw new System.NullReferenceException();
            self._BarText.text = text;
        }
        return true;
    }

    // Source: Ghidra SetAdultImg.c RVA 0x18F3B1C
    // 1. If _adultImg != null: SetActive(state)
    // 2. If ResourcesPath.IsPreviewVersion() == false → return
    // 3. If _adultImg2 != null: SetActive(state); SetActive(false) on _adultImg (override)
    public void SetAdultImg(bool state)
    {
        if (_adultImg == null) throw new System.NullReferenceException();
        _adultImg.SetActive(state);
        if (!ResourcesPath.IsPreviewVersion()) return;
        if (_adultImg2 != null)
        {
            _adultImg2.SetActive(state);
            if (_adultImg != null)
            {
                _adultImg.SetActive(false);
                return;
            }
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra set_CharacterLevel.c RVA 0x18F3BC4
    // 1. _characterlevel = value (static field)
    // 2. If ResMgr.Instance != null:
    //      beginTipCount = ResMgr.Instance.GetBeginerTipLength()
    //      advanceTipCount = ResMgr.Instance.GetAdvanceTipLength()
    //      if s_instance != null: s_instance._randomOneTip()
    public static void set_CharacterLevel(int value)
    {
        _characterlevel = value;
        ResMgr mgr = ResMgr.Instance;
        if (mgr != null)
        {
            beginTipCount = mgr.GetBeginerTipLength();
            mgr = ResMgr.Instance;
            if (mgr != null)
            {
                advanceTipCount = mgr.GetAdvanceTipLength();
                if (s_instance != null)
                {
                    s_instance._randomOneTip();
                    return;
                }
            }
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra _onClickChangeTip.c RVA 0x18F3CCC — same body as _randomOneTip (both bind to RVA 0x18F3CCC for click + delegated by tip).
    // Identical Ghidra decompile to _randomOneTip — picks a tip via ResMgr.GetBasicUIText, sets _loadingTipText.text, resets autoRandTimeCounter.
    public void _onClickChangeTip(Component btn, PointerEventData data, Action_Type action, int clickValue, string clickString)
    {
        // Logic identical to _randomOneTip — delegate
        _randomOneTip();
    }

    // Source: Ghidra get_Completed.c RVA 0x18F3CD0 — body inlined in Completed property above.

    // Source: Ghidra .ctor.c RVA 0x18F3D40
    // 1. lastPic = -1; _PicIndex = -1 (offset 0x108, 0x170)
    // 2. loadingBarSpeed/fLimitTime packed write — DAT_008e34a0 default (TODO: extract from .data section)
    // 3. base.WndForm(ticked=true)
    public WndForm_LoadingScreen() : base(true)
    {
        lastPic = -1;
        _PicIndex = -1;
        // TODO: extract DAT_008e34a0 packed two-float default (loadingBarSpeed + fLimitTime) from libil2cpp.so .data
    }

    // Source: Ghidra GetPrefab.c RVA 0x18F3D64
    // return "Prefabs/Menus/" + EWndFormIDMapping.GetWndFormString(eWndFormID)
    // (StringLiteral_9184 confirmed via stringliteral.json = "Prefabs/Menus/")
    public override string GetPrefab(uint eWndFormID, ArrayList args)
    {
        return System.String.Concat("Prefabs/Menus/", EWndFormIDMapping.GetWndFormString(eWndFormID));
    }

    // Source: Ghidra IsPrefabInResource.c RVA 0x18F3DC0 — returns true.
    public override bool IsPrefabInResource() { return true; }

    // Source: Ghidra V_Create.c RVA 0x18F3DC8
    // 1. s_instance = this (puVar1+0xB8 ptr write).
    // 2. InitComponents (thunk_FUN_015ee8c4) — serialized-field bind handled by Unity prefab load.
    // 3. If _uiSprite_progress != null: fillAmount=0, reset TimeCounter, _BarText.text="Loading...", SetShow(false).
    // 4. Read Main.Instance._ConfigGeneral.BundleVersion (offset chain 0x40 → 0x28); if non-null + CurVersion non-null:
    //    CurVersion.text = string.Format("Ver : {0}", showVersion(BundleVersion))
    // 5. _barGO.SetActive(false); return true.
    protected override bool V_Create(ArrayList args)
    {
        s_instance = this;
        if (_uiSprite_progress == null) throw new System.NullReferenceException();
        _uiSprite_progress.fillAmount = 0f;
        TimeCounter = 0f;
        if (_BarText != null)
        {
            _BarText.text = "Loading...";
            SetShow(false);
            Main mainInst = Main.Instance;
            if (mainInst != null)
            {
                ConfigGeneral cfg = mainInst._ConfigGeneral;
                if (cfg != null && CurVersion != null)
                {
                    string ver = cfg.BundleVersion;
                    CurVersion.text = System.String.Format("Ver : {0}", showVersion(ver));
                }
            }
            if (_barGO != null)
            {
                _barGO.SetActive(false);
                return true;
            }
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra V_Update.c RVA 0x18F4090
    // if !IsShow() → return
    // if _ShowPercent > 0 && _barPersent <= _ShowPercent:
    //    _barPersent += loadingBarSpeed
    //    _uiSprite_progress.fillAmount = _barPersent / 100
    // autoRandTimeCounter += dTime; if >= 5 → _randomOneTip(); counter = 0
    // randPicTimeCounter += dTime; if >= 6 → _randomPic(); counter = 0
    protected override void V_Update(float dTime)
    {
        if (!IsShow()) return;
        if (_ShowPercent > 0.0f && _barPersent <= _ShowPercent)
        {
            _barPersent = _barPersent + loadingBarSpeed;
            if (_uiSprite_progress == null) throw new System.NullReferenceException();
            _uiSprite_progress.fillAmount = _barPersent / 100.0f;
        }
        autoRandTimeCounter = autoRandTimeCounter + dTime;
        if (5.0f <= autoRandTimeCounter)
        {
            _randomOneTip();
            autoRandTimeCounter = 0f;
        }
        randPicTimeCounter = randPicTimeCounter + dTime;
        if (6.0f <= randPicTimeCounter)
        {
            _randomPic();
            randPicTimeCounter = 0f;
        }
    }

    // Source: Ghidra V_Destroy.c RVA 0x18F4144 — clears s_instance to null.
    protected override void V_Destroy() { s_instance = null; }

    // Source: Ghidra V_ProcessKeyClick.c RVA 0x18F4198 — returns keyCode == 0x1B (Escape).
    protected override bool V_ProcessKeyClick(KeyCode keyCode)
    {
        return (int)keyCode == 0x1B;
    }

    // Source: Ghidra ExitApp.c RVA 0x18F41A4 — if bConfirm == true: WndForm.QuitApp();
    private void ExitApp(bool bConfirm)
    {
        if (bConfirm) WndForm.QuitApp();
    }

    // Source: Ghidra setBarEnable.c RVA 0x18F41B4 — activates _barGO; NRE if null.
    public void setBarEnable()
    {
        if (_barGO == null) throw new System.NullReferenceException();
        _barGO.SetActive(true);
    }

    // Source: Ghidra DownLoadCheck_Click.c RVA 0x18F41D4
    // 1. if action == 2 (Cancel) → SendLoadCheckResult(2)
    //    else if action == 1 (Confirm) → SendLoadCheckResult(1)
    //    else return
    // 2. if Root_DownLoadCheck != null → SetActive(false)
    public void DownLoadCheck_Click(Component btn, PointerEventData data, Action_Type action, int clickValue, string clickString)
    {
        int actInt = (int)action;
        int result;
        if (actInt == 2) result = 2;
        else if (actInt == 1) result = 1;
        else return;
        SendLoadCheckResult(result);
        if (Root_DownLoadCheck != null)
        {
            Root_DownLoadCheck.SetActive(false);
        }
    }

    // Source: Ghidra SendLoadCheckResult.c RVA 0x18F4280
    // 1. cpm = CProcManager (== GameProcMgr.Instance).
    // 2. If cpm == null → NRE.
    // 3. If cpm.eCurProcID == ProcessLunchGame (2): plg = cpm.getCurProc<ProcessLunchGame>(); plg.iConfirmResult = iResult.
    private void SendLoadCheckResult(int iResult)
    {
        GameProcMgr pm = GameProcMgr.Instance;
        if (pm == null) throw new System.NullReferenceException();
        if ((int)pm.eCurProcID == 2)
        {
            ProcessLunchGame plg = pm.getCurProc<ProcessLunchGame>();
            if (plg == null) throw new System.NullReferenceException();
            plg.iConfirmResult = iResult;
        }
    }

    // Source: Ghidra ShowDownloadCheck.c RVA 0x18F4374
    // 1. _checkWndBtnOKText.text = ResMgr.Instance.GetBasicUIText(0x3D)
    // 2. _checkWndBtnCancelText.text = ResMgr.Instance.GetBasicUIText(0x42)
    // 3. Root_DownLoadCheck.SetActive(true)
    // 4. If TXT_ConfirmSize != null:
    //      size = (int)(iSize / 1024)
    //      if size < 1 → TXT_ConfirmSize.text = ResMgr.GetBasicUIText(iTextIDLess)
    //      else → TXT_ConfirmSize.text = string.Format(ResMgr.GetBasicUIText(iTextIDLarge), size)
    public void ShowDownloadCheck(float iSize, int iTextIDLarge, int iTextIDLess)
    {
        ResMgr rm = ResMgr.Instance;
        if (rm == null) throw new System.NullReferenceException();
        if (_checkWndBtnOKText != null)
        {
            _checkWndBtnOKText.text = rm.GetBasicUIText(0x3D);
        }
        if (_checkWndBtnCancelText != null)
        {
            _checkWndBtnCancelText.text = rm.GetBasicUIText(0x42);
        }
        if (Root_DownLoadCheck == null) return;
        Root_DownLoadCheck.SetActive(true);
        if (TXT_ConfirmSize == null) return;
        // float→int truncation; Ghidra checks for INFINITY case (clamps to MinValue).
        int size;
        float kb = iSize * 0.0009765625f; // = iSize / 1024
        if (float.IsInfinity(kb)) size = int.MinValue;
        else size = (int)kb;
        if (size < 1)
        {
            TXT_ConfirmSize.text = rm.GetBasicUIText(iTextIDLess);
        }
        else
        {
            TXT_ConfirmSize.text = System.String.Format(rm.GetBasicUIText(iTextIDLarge), size);
        }
    }

    // Source: Ghidra ShowConfirmCheck.c RVA 0x18F4624
    // 1. _CheckTitle.text = title
    // 2. _CheckContent.text = content
    // 3. _CheckBtnText.text = ResMgr.GetBasicUIText(0x3D)
    // 4. Root_VersionCheck.SetActive(true)
    public void ShowConfirmCheck(string title, string content)
    {
        if (_CheckTitle == null) throw new System.NullReferenceException();
        _CheckTitle.text = title;
        if (_CheckContent == null) throw new System.NullReferenceException();
        _CheckContent.text = content;
        ResMgr rm = ResMgr.Instance;
        if (rm == null) throw new System.NullReferenceException();
        if (_CheckBtnText != null)
        {
            _CheckBtnText.text = rm.GetBasicUIText(0x3D);
        }
        if (Root_VersionCheck != null)
        {
            Root_VersionCheck.SetActive(true);
        }
    }

    // Source: Ghidra VersionCheck_Click.c RVA 0x18F476C
    // if action == 1 (Confirm): SendLoadCheckResult(1); Root_VersionCheck.SetActive(false)
    public void VersionCheck_Click(Component btn, PointerEventData data, Action_Type action, int clickValue, string clickString)
    {
        if ((int)action == 1)
        {
            SendLoadCheckResult(1);
            if (Root_VersionCheck != null)
            {
                Root_VersionCheck.SetActive(false);
            }
        }
    }

    // Source: Ghidra _randomOneTip.c RVA 0x18F3794
    // Logic:
    //   if _characterlevel == 0: low=501, high=1000
    //   else if _characterlevel < 60: low=501, high=beginTipCount+501
    //   else: low=651, high=advanceTipCount+651
    //   Loop up to 99 tries: pick random in [low..high], call ResMgr.GetBasicUIText(id);
    //     if returned text != "" → break.
    //   If text != "" → _loadingTipText.text = text
    //   autoRandTimeCounter = 0
    private void _randomOneTip()
    {
        string tip = "";
        int low;
        int high;
        if (_characterlevel == 0)
        {
            low = 0x1F5; // 501
            high = 1000;
        }
        else if (_characterlevel < 0x3C)
        {
            low = 0x1F5;
            high = beginTipCount + 0x1F5;
        }
        else
        {
            low = 0x28B; // 651
            high = advanceTipCount + 0x28B;
        }
        ResMgr rm = ResMgr.Instance;
        if (rm == null) throw new System.NullReferenceException();
        // Ghidra: while op_Equality("","")==true (always); loop guarded by uVar10<99 + (uVar4&1)==true (text non-empty)
        // i.e. retry until tip != "" or 99 tries.
        for (uint tries = 0; tries < 99; tries++)
        {
            int id = UnityEngine.Random.Range(low, high + 1);
            tip = rm.GetBasicUIText(id);
            if (tip != "") break;
        }
        if (tip != "")
        {
            if (_loadingTipText == null) throw new System.NullReferenceException();
            _loadingTipText.text = tip;
        }
        autoRandTimeCounter = 0f;
    }

    // Source: Ghidra _randomPic.c RVA 0x18F35B0
    // Loop until UnityEngine.Random.Range(0,8) != lastPic:
    //   lastPic = pic;
    //   openEff(pic) -- wait actually openEff(this) param2=0; index isn't used in openEff!
    //   path = (SGCLanguage.UseLanguageSystem ? "_" + ConfigMgr.GetConfigVarLanguage() : "_1")
    //   Resource path = "NotInBundle/loading/loading-" + pic.ToString() + path
    //   spr = Resources.Load<Sprite>(path); _LoadingBG.sprite = spr;
    private void _randomPic()
    {
        int pic = UnityEngine.Random.Range(0, 8);
        // Ghidra's loop: while pic == lastPic, re-roll. Implemented via do-while.
        while (pic == lastPic)
        {
            pic = UnityEngine.Random.Range(0, 8);
        }
        lastPic = pic;
        openEff(pic);
        string suffix;
        if (SGCLanguage.UseLanguageSystem)
        {
            ConfigMgr cm = ConfigMgr.Instance;
            if (cm == null) throw new System.NullReferenceException();
            int lang = cm.GetConfigVarLanguage();
            suffix = "_" + lang.ToString();
        }
        else
        {
            suffix = "_1";
        }
        string path = "NotInBundle/loading/loading-" + pic.ToString() + suffix;
        var spr = UnityEngine.Resources.Load<UnityEngine.Sprite>(path);
        if (_LoadingBG == null) throw new System.NullReferenceException();
        _LoadingBG.sprite = spr;
    }

    // Source: Ghidra SetBundleCreateTime.c RVA 0x18F48E4
    // 1. Read Main.Instance._ConfigGeneral.BundleVersion + BuildTime (Main+0x40→0x28 + Main+0x40→0x40).
    // 2. text = string.Format("ver : {0}",  showVersion(BundleVersion) + "." + BuildTime)
    // 3. BundleTime.text = text
    public void SetBundleCreateTime()
    {
        Main mainInst = Main.Instance;
        if (mainInst == null) throw new System.NullReferenceException();
        ConfigGeneral cfg = mainInst._ConfigGeneral;
        if (cfg == null) throw new System.NullReferenceException();
        string ver = showVersion(cfg.BundleVersion);
        string concat = System.String.Concat(ver, ".", cfg.BuildTime);
        if (BundleTime == null) throw new System.NullReferenceException();
        BundleTime.text = System.String.Format("ver : {0}", concat);
    }

    // Source: Ghidra showVersion.c RVA 0x18F3F94
    // if ver == null → NRE.
    // tokens = ver.Split('.')
    // skip tokens[0], for i in [1..len-1]: result += tokens[i], if i<len-1: result += "."
    private string showVersion(string ver)
    {
        if (ver == null) throw new System.NullReferenceException();
        string[] tokens = ver.Split('.');
        if (tokens == null) throw new System.NullReferenceException();
        string result = "";
        if (tokens.Length > 0)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (i == 0)
                {
                    // Ghidra skips index 0 (only increments and continues)
                    continue;
                }
                result = System.String.Concat(result, tokens[i]);
                if (i < tokens.Length - 1)
                {
                    result = System.String.Concat(result, ".");
                }
            }
        }
        return result;
    }

    // Source: Ghidra openEff.c RVA 0x18F4808
    // Activates eff_00..eff_07; SetActive(index == N) for each (so only one matching index is active).
    private void openEff(int index)
    {
        if (eff_00 == null) throw new System.NullReferenceException();
        eff_00.SetActive(index == 0);
        if (eff_01 == null) throw new System.NullReferenceException();
        eff_01.SetActive(index == 1);
        if (eff_02 == null) throw new System.NullReferenceException();
        eff_02.SetActive(index == 2);
        if (eff_03 == null) throw new System.NullReferenceException();
        eff_03.SetActive(index == 3);
        if (eff_04 == null) throw new System.NullReferenceException();
        eff_04.SetActive(index == 4);
        if (eff_05 == null) throw new System.NullReferenceException();
        eff_05.SetActive(index == 5);
        if (eff_06 == null) throw new System.NullReferenceException();
        eff_06.SetActive(index == 6);
        if (eff_07 == null) throw new System.NullReferenceException();
        eff_07.SetActive(index == 7);
    }

}
