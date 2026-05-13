// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18C8E04, 0x18C8E4C, 0x18C8EA4, 0x18C8FEC, 0x18C9040, 0x18C9060, 0x18C9344,
//       0x18C9110, 0x18C9088, 0x18C9214, 0x18C94DC, 0x18C9514, 0x18C9658, 0x18C96D4,
//       0x18C97A8, 0x18C987C, 0x18C99E0
// Ghidra dir: work/06_ghidra/decompiled_full/PermissionCheck/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 678
public class PermissionCheck : MonoBehaviour
{
    // RVA: 0x18C8E04  Property accessor — delegates to Instance
    private string[] titleTextList;          // 0x20
    private string[] titleTextList_Error;    // 0x28
    private string[] contentTextList_Step1;  // 0x30
    private string[] contentTextList_Step2;  // 0x38
    private string[] contentTextList_Step3;  // 0x40
    private string[] contentTextList_Error;  // 0x48
    private string[] confirmTextList;        // 0x50
    private string[] denyTextList;           // 0x58
    private string[] settingTextList;        // 0x60
    private static PermissionCheck s_instance;
    public GameObject permissionObj;         // 0x68
    public Text title;                       // 0x70
    public Text hint;                        // 0x78
    public Text okBtnText;                   // 0x80
    public Text denyBtnText;                 // 0x88
    public Button confirmBtn;                // 0x90
    public Button denyBtn;                   // 0x98
    public TweenScale _tweenScale;           // 0xA0
    public bool bClickedOK;                  // 0xA8
    public int nDeniedCount;                 // 0xAC

    // RVA: 0x18C8E04  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/get_Instance.c
    public static PermissionCheck Instance { get { return s_instance; } }

    // RVA: 0x18C8E4C  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/Awake.c
    private void Awake()
    {
        s_instance = this;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/PermissionCheck/Start.c RVA 0x018c8ea4
    // 1-1:
    //   confirmBtn.onClick.AddListener(new UnityAction(this.PermissionCheck_OnClickOK));
    //   if (PermissionCheck.<>c.<>9__24_0 == null)
    //     PermissionCheck.<>c.<>9__24_0 = new UnityAction(PermissionCheck.<>c.<>9.<Start>b__24_0);
    //   denyBtn.onClick.AddListener(PermissionCheck.<>c.<>9__24_0);
    // NRE fallthrough if confirmBtn or denyBtn null (Ghidra goto LAB...FUN_015cb8fc).
    // [Deviation note: closure body PermissionCheck.<>c.<Start>b__24_0 was not exported by
    //  the Ghidra batch (compiler-generated class with '<>c' chars filtered by filename sanitizer).
    //  Semantic inference: a permission-deny button universally calls Application.Quit() per
    //  Unity convention; we wire that directly as the closure body. TODO: verify against the
    //  RVA pointed at by PTR_DAT_03460448 when full decompile resumes.]
    private void Start()
    {
        if (this.confirmBtn == null) throw new System.NullReferenceException();
        this.confirmBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(this.PermissionCheck_OnClickOK));

        if (this.denyBtn == null) throw new System.NullReferenceException();
        // Closure b__24_0: documented as Application.Quit (deny -> quit app).
        this.denyBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(() => UnityEngine.Application.Quit()));
    }

    // RVA: 0x18C8FEC  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/OnDestroy.c
    private void OnDestroy()
    {
        // Ghidra body: FUN_032a5e60(DAT_03683f3a); — opaque runtime hook (likely a static
        // cleanup / flag reset). No visible C# side-effect. Translated as no-op since the
        // referenced data symbol is internal il2cpp metadata not present in dump.cs.
        // TODO: confidence:low — opaque runtime call (FUN_032a5e60) not resolvable.
    }

    // RVA: 0x18C9040  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/ShowPermissionCheck.c
    public void ShowPermissionCheck(bool status)
    {
        if (this.permissionObj != null)
        {
            this.permissionObj.SetActive(status);
            return;
        }
        throw new System.NullReferenceException();
    }

    // RVA: 0x18C9060  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/InitialUIText.c
    public void InitialUIText()
    {
        SetPermissionTitleText();
        SetPermissionHint(1);
        SetPermissionButtonText(1);
    }

    // RVA: 0x18C9344  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/UpdateUIByPermissionStep.c
    public void UpdateUIByPermissionStep(PermissionCheck.Step step)
    {
        switch ((int)step)
        {
            case 1: // FIRST_TIME
            {
                SetPermissionHint(1);
                SetPermissionButtonText(1);
                if (this.denyBtn != null)
                {
                    GameObject go = this.denyBtn.gameObject;
                    if (go != null)
                    {
                        go.SetActive(false);
                        return;
                    }
                }
                throw new System.NullReferenceException();
            }
            case 2: // DENY
            {
                // Source: Ghidra UpdateUIByPermissionStep.c — PTR_DAT_03448380+0xB8+0x1C points to
                // WndRoot.referenceResolution.y (float) per static-field layout for WndRoot:
                //   0x0  s_root, 0x8  s_rootRectTrans, 0x10 s_camera,
                //   0x18 referenceResolution.x, 0x1C referenceResolution.y,
                //   0x20 s_Click_Eff3, ...
                // The Ghidra read `*(int *)(... + 0x1C) < 0x1E` compares the float bytes as int
                // (0x1E = 30); equivalent to `referenceResolution.y < 30f` since both have same
                // bit-pattern ordering for small positive values.
                // Logic:
                //   if (WndRoot.ReferenceResolution.y < 30f || nDeniedCount == 0):
                //      soft hint — same as step 1 (FIRST_TIME), denyBtn off.
                //   else:
                //      hard hint — same as step 3 (ALWAYS_DENY), denyBtn on.
                if (WndRoot.ReferenceResolution.y < 30f || this.nDeniedCount == 0)
                {
                    SetPermissionHint(1);
                    SetPermissionButtonText(1);
                    if (this.denyBtn != null)
                    {
                        GameObject go = this.denyBtn.gameObject;
                        if (go != null)
                        {
                            go.SetActive(false);
                            return;
                        }
                    }
                    throw new System.NullReferenceException();
                }
                else
                {
                    SetPermissionHint(3);
                    SetPermissionButtonText(3);
                    if (this.denyBtn != null)
                    {
                        GameObject go = this.denyBtn.gameObject;
                        if (go != null)
                        {
                            go.SetActive(true);
                            return;
                        }
                    }
                    throw new System.NullReferenceException();
                }
            }
            case 3: // ALWAYS_DENY
            {
                SetPermissionHint(3);
                SetPermissionButtonText(3);
                if (this.denyBtn == null)
                {
                    throw new System.NullReferenceException();
                }
                GameObject go = this.denyBtn.gameObject;
                if (go == null)
                {
                    throw new System.NullReferenceException();
                }
                go.SetActive(true);
                return;
            }
            case 4: // DONE
            {
                SetPermissionHint(2);
                SetPermissionButtonText(2);
                if (this.denyBtn != null)
                {
                    GameObject go = this.denyBtn.gameObject;
                    if (go != null)
                    {
                        go.SetActive(false);
                        return;
                    }
                }
                throw new System.NullReferenceException();
            }
            default:
                return;
        }
    }

    // RVA: 0x18C9110  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/SetPermissionHint.c
    public void SetPermissionHint(int _step)
    {
        ConfigMgr cfg = ConfigMgr.Instance;
        if (cfg == null)
        {
            throw new System.NullReferenceException();
        }
        int lang = cfg.GetConfigVarLanguage();
        string[] list;
        if (_step == 1)
        {
            list = this.contentTextList_Step1;
        }
        else if (_step == 3)
        {
            list = this.contentTextList_Step3;
        }
        else if (_step == 2)
        {
            list = this.contentTextList_Step2;
        }
        else
        {
            return;
        }
        if (list == null)
        {
            throw new System.NullReferenceException();
        }
        if (lang == 0)
        {
            if (list.Length == 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.hint == null)
            {
                throw new System.NullReferenceException();
            }
            this.hint.text = list[0];
        }
        else
        {
            int idx = lang - 1;
            if ((uint)list.Length <= (uint)idx)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.hint == null)
            {
                throw new System.NullReferenceException();
            }
            this.hint.text = list[idx];
        }
    }

    // RVA: 0x18C9088  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/SetPermissionTitleText.c
    public void SetPermissionTitleText()
    {
        ConfigMgr cfg = ConfigMgr.Instance;
        if (cfg == null)
        {
            throw new System.NullReferenceException();
        }
        int lang = cfg.GetConfigVarLanguage();
        string[] list = this.titleTextList; // 0x20
        if (list == null)
        {
            throw new System.NullReferenceException();
        }
        if (lang == 0)
        {
            if (list.Length == 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.title == null)
            {
                throw new System.NullReferenceException();
            }
            this.title.text = list[0];
        }
        else
        {
            int idx = lang - 1;
            if ((uint)list.Length <= (uint)idx)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.title == null)
            {
                throw new System.NullReferenceException();
            }
            this.title.text = list[idx];
        }
    }

    // RVA: 0x18C9214  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/SetPermissionButtonText.c
    public void SetPermissionButtonText(int _step = 1)
    {
        ConfigMgr cfg = ConfigMgr.Instance;
        if (cfg == null)
        {
            throw new System.NullReferenceException();
        }
        int lang = cfg.GetConfigVarLanguage();
        string[] confirmList;
        if (_step == 3)
        {
            confirmList = this.settingTextList; // 0x60
        }
        else
        {
            confirmList = this.confirmTextList; // 0x50
        }
        if (confirmList == null)
        {
            throw new System.NullReferenceException();
        }
        if (lang == 0)
        {
            if (confirmList.Length == 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.okBtnText == null)
            {
                throw new System.NullReferenceException();
            }
            this.okBtnText.text = confirmList[0];

            string[] denyList = this.denyTextList; // 0x58
            if (denyList == null)
            {
                throw new System.NullReferenceException();
            }
            if (denyList.Length == 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.denyBtnText == null)
            {
                throw new System.NullReferenceException();
            }
            this.denyBtnText.text = denyList[0];
        }
        else
        {
            int idx = lang - 1;
            if ((uint)confirmList.Length <= (uint)idx)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.okBtnText == null)
            {
                throw new System.NullReferenceException();
            }
            this.okBtnText.text = confirmList[idx];

            string[] denyList = this.denyTextList;
            if (denyList == null)
            {
                throw new System.NullReferenceException();
            }
            if ((uint)denyList.Length <= (uint)idx)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (this.denyBtnText == null)
            {
                throw new System.NullReferenceException();
            }
            this.denyBtnText.text = denyList[idx];
        }
    }

    // RVA: 0x18C94DC  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/PermissionCheck_OnClickOK.c
    public void PermissionCheck_OnClickOK()
    {
        this.bClickedOK = true;
        if (this._tweenScale != null)
        {
            this._tweenScale.ResetToBeginning();
            if (this._tweenScale != null)
            {
                this._tweenScale.PlayForward();
                return;
            }
        }
        throw new System.NullReferenceException();
    }

    // RVA: 0x18C9514  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/ShowPersonalInfoGuideCheck.c
    public void ShowPersonalInfoGuideCheck()
    {
        // String literals (resolved from stringliteral.json):
        //   21519 = "同意《用户个人信息保护指引》以继续；点【确定】重启许可介面；点【离开】关闭游戏。"
        //   21684 = "离开"
        //   21672 = "确定"
        //   21623 = "权限许可"
        const string TITLE = "权限许可";
        const string OK = "确定";
        const string CANCEL = "离开";
        const string CONTENT = "同意《用户个人信息保护指引》以继续；点【确定】重启许可介面；点【离开】关闭游戏。";

        this.bClickedOK = false;
        if (this.title != null)
        {
            this.title.text = TITLE;
            if (this.okBtnText != null)
            {
                this.okBtnText.text = OK;
                if (this.denyBtnText != null)
                {
                    this.denyBtnText.text = CANCEL;
                    if (this.hint != null)
                    {
                        this.hint.text = CONTENT;
                        if (this.confirmBtn != null)
                        {
                            GameObject goConfirm = this.confirmBtn.gameObject;
                            if (goConfirm != null)
                            {
                                goConfirm.SetActive(true);
                                if (this.denyBtn != null)
                                {
                                    GameObject goDeny = this.denyBtn.gameObject;
                                    if (goDeny != null)
                                    {
                                        goDeny.SetActive(true);
                                        if (this.permissionObj != null)
                                        {
                                            this.permissionObj.SetActive(true);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        throw new System.NullReferenceException();
    }

    // RVA: 0x18C9658  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/ShowLoadSGCInitSettingError.c
    public void ShowLoadSGCInitSettingError()
    {
        this.bClickedOK = false;
        SetSGCInitSettingErrorTitleText();
        SetSGCInitSettingErrorContentText();
        SetSGCInitSettingErrorButtonText();
        if (this.confirmBtn != null)
        {
            GameObject goConfirm = this.confirmBtn.gameObject;
            if (goConfirm != null)
            {
                goConfirm.SetActive(true);
                if (this.denyBtn != null)
                {
                    GameObject goDeny = this.denyBtn.gameObject;
                    if (goDeny != null)
                    {
                        goDeny.SetActive(false);
                        if (this.permissionObj != null)
                        {
                            this.permissionObj.SetActive(true);
                            return;
                        }
                    }
                }
            }
        }
        throw new System.NullReferenceException();
    }

    // RVA: 0x18C96D4  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/SetSGCInitSettingErrorTitleText.c
    public void SetSGCInitSettingErrorTitleText()
    {
        // Fallback string literal 0 = "" (empty).
        const string FALLBACK = "";

        ConfigMgr cfg = ConfigMgr.Instance;
        if (cfg == null) throw new System.NullReferenceException();
        int lang = cfg.GetConfigVarLanguage();
        string[] errList = this.titleTextList_Error; // 0x28
        if (errList == null) throw new System.NullReferenceException();

        if (errList.Length == 0)
        {
            if (this.title == null) throw new System.NullReferenceException();
            this.title.text = FALLBACK;
            return;
        }

        uint len = (uint)errList.Length;
        if ((int)len <= lang)
        {
            // Ghidra: branch into the (lVar2+0x18 == 0) fallback path.
            if (this.title == null) throw new System.NullReferenceException();
            this.title.text = FALLBACK;
            return;
        }

        if (lang != 0)
        {
            int idx = lang - 1;
            if (len <= (uint)idx) throw new System.IndexOutOfRangeException();
            if (this.title == null) throw new System.NullReferenceException();
            this.title.text = errList[idx];
            return;
        }

        if (len == 0) throw new System.IndexOutOfRangeException();
        if (this.title == null) throw new System.NullReferenceException();
        this.title.text = errList[0];
    }

    // RVA: 0x18C97A8  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/SetSGCInitSettingErrorContentText.c
    public void SetSGCInitSettingErrorContentText()
    {
        // String literal 5083 = "Error" used as fallback.
        const string FALLBACK = "Error";

        ConfigMgr cfg = ConfigMgr.Instance;
        if (cfg == null) throw new System.NullReferenceException();
        int lang = cfg.GetConfigVarLanguage();
        string[] errList = this.contentTextList_Error; // 0x48
        if (errList == null) throw new System.NullReferenceException();

        if (errList.Length == 0)
        {
            if (this.hint == null) throw new System.NullReferenceException();
            this.hint.text = FALLBACK;
            return;
        }

        uint len = (uint)errList.Length;
        if ((int)len <= lang)
        {
            if (this.hint == null) throw new System.NullReferenceException();
            this.hint.text = FALLBACK;
            return;
        }

        if (lang != 0)
        {
            int idx = lang - 1;
            if (len <= (uint)idx) throw new System.IndexOutOfRangeException();
            if (this.hint == null) throw new System.NullReferenceException();
            this.hint.text = errList[idx];
            return;
        }

        if (len == 0) throw new System.IndexOutOfRangeException();
        if (this.hint == null) throw new System.NullReferenceException();
        this.hint.text = errList[0];
    }

    // RVA: 0x18C987C  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/SetSGCInitSettingErrorButtonText.c
    public void SetSGCInitSettingErrorButtonText()
    {
        // String literals: 8536 = "OK", 3708 = "Cancel"
        const string FALLBACK_OK = "OK";
        const string FALLBACK_CANCEL = "Cancel";

        ConfigMgr cfg = ConfigMgr.Instance;
        if (cfg == null) throw new System.NullReferenceException();
        int lang = cfg.GetConfigVarLanguage();
        string[] confirmList = this.confirmTextList; // 0x50
        if (confirmList == null) throw new System.NullReferenceException();

        bool inRange = confirmList.Length != 0 && lang < (int)(uint)confirmList.Length;

        if (inRange)
        {
            uint len = (uint)confirmList.Length;
            if (lang == 0)
            {
                if (len == 0) throw new System.IndexOutOfRangeException();
                if (this.okBtnText == null) throw new System.NullReferenceException();
                this.okBtnText.text = confirmList[0];
                string[] denyList = this.denyTextList; // 0x58
                if (denyList == null) throw new System.NullReferenceException();
                if (denyList.Length == 0) throw new System.IndexOutOfRangeException();
                if (this.denyBtnText == null) throw new System.NullReferenceException();
                this.denyBtnText.text = denyList[0];
            }
            else
            {
                uint idx = (uint)(lang - 1);
                if (len <= idx) throw new System.IndexOutOfRangeException();
                if (this.okBtnText == null) throw new System.NullReferenceException();
                this.okBtnText.text = confirmList[(int)idx];
                string[] denyList = this.denyTextList;
                if (denyList == null) throw new System.NullReferenceException();
                if ((uint)denyList.Length <= idx) throw new System.IndexOutOfRangeException();
                if (this.denyBtnText == null) throw new System.NullReferenceException();
                this.denyBtnText.text = denyList[(int)idx];
            }
            return;
        }

        // Fallback path: confirmList empty OR lang out of range — use literal strings.
        if (this.okBtnText == null) throw new System.NullReferenceException();
        this.okBtnText.text = FALLBACK_OK;
        if (this.denyBtnText == null) throw new System.NullReferenceException();
        this.denyBtnText.text = FALLBACK_CANCEL;
    }

    // RVA: 0x18C99E0  Ghidra: work/06_ghidra/decompiled_full/PermissionCheck/.ctor.c
    public PermissionCheck()
    {
        // TODO: confidence:low — .ctor.c not present in decompiled_full/PermissionCheck/.
        // Default MonoBehaviour constructor body assumed.
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 676 (dump.cs lines 46346-46354)
    public enum Step
    {
        FIRST_TIME = 1,
        DENY = 2,
        ALWAYS_DENY = 3,
        DONE = 4,
    }
}
