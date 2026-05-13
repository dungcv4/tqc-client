// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18CBFF0, 0x18CC038, 0x18CC040, 0x18CC098, 0x18CC09C, 0x18CC50C, 0x18CC518, 0x18CC538, 0x18CC6CC, 0x18CC728
// Ghidra dir: work/06_ghidra/decompiled_full/SGCLanguageSelect/
// dump.cs class 'SGCLanguageSelect' (TypeDefIndex: 683)

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

public class SGCLanguageSelect : MonoBehaviour
{
    // RVA: 0x18CC038  Property accessor — delegates to setFlag
    private static SGCLanguageSelect s_instance;
    public GameObject _lanSelWnd;
    public LanguageNodeObject _gpLanBoard;
    public LanguageNodeObject _thaiBoardBtn;
    public Dictionary<int, LanguageNodeObject> _selObjPanelDict;
    public RectTransform _lanPanelTrans;
    public Button _btnChange;
    public Text _lanTitleText;
    public Text _btnChangeText;
    private int _selLan;
    private bool _setFlag;
    private string[] titleTextList;
    private string[] contentTextList;
    private string[] confirmTextList;

    // RVA: 0x18CBFF0  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/get_Instance.c
    // Ghidra body is an indirect tail-call (FUN_032a5e68) — actual return decompiled
    // through SGCRegionSelect peer pattern: returns the static `s_instance` field.
    public static SGCLanguageSelect Instance { get { return s_instance; } }

    // RVA: 0x18CC038  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/get_setFlag.c
    public bool setFlag { get { return _setFlag; } }

    // RVA: 0x18CC040  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/Awake.c
    // Ghidra: assigns __this to the static field at PTR_DAT_034607c8 + 0xb8 (s_instance).
    private void Awake()
    {
        s_instance = this;
    }

    // RVA: 0x18CC098  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/Start.c
    // Ghidra Start.c body is identical bytecode to InitLanguageComponent.c (both share
    // RVA-marked label LAB_019cc174). dump.cs declares both as separate methods.
    private void Start()
    {
        InitLanguageComponent();
    }

    // RVA: 0x18CC50C  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/Update.c
    private void Update()
    {
        return;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SGCLanguageSelect/InitLanguageComponent.c RVA 0x018cc09c
    // (Start.c is identical bytecode to InitLanguageComponent.c — Start just delegates.)
    // 1-1: iterate SGCLanguage.ver_defaultUseLanguageList ({1, 4}). For each language id `lan`:
    //   alloc DisplayClass21_0 closure (captures `this` + `lan`)
    //   if (lan != 1) {                               // skip Chinese_Trad
    //     if (lan == 6) {                             // Thailand path
    //       _thaiBoardBtn.clickArea.onClick.AddListener(closure.b__1);
    //       _thaiBoardBtn.gameObject.SetActive(true);
    //       _thaiBoardBtn.transform.SetAsLastSibling();
    //       _selObjPanelDict.Add(lan, _thaiBoardBtn);
    //     } else {                                     // generic instantiation path
    //       var node = Instantiate(_gpLanBoard);
    //       node.clickArea.onClick.AddListener(closure.b__0);
    //       node.lbText.text = titleTextList[lan-1];
    //       node.spCheck.SetActive(lan == 4);          // default Vietnam highlighted
    //       node.transform.SetParent(_lanPanelTrans);
    //       node.transform.localScale = Vector3.one;   // PTR_DAT_03446bc8
    //       node.gameObject.SetActive(true);
    //       _selObjPanelDict.Add(lan, node);
    //     }
    //   }
    // After loop:
    //   _btnChange.onClick.AddListener(_confirmSelLanguage);
    //   _selLan = 4;
    //   _btnChangeText.text = confirmTextList[3];
    //   _lanTitleText.text = titleTextList[3];
    // Closures b__0/b__1 invoke `_onSelLanguage(this.lan)` capturing the per-iteration id.
    private void InitLanguageComponent()
    {
        if (_selObjPanelDict == null) _selObjPanelDict = new Dictionary<int, LanguageNodeObject>();
        int[] langList = SGCLanguage.ver_defaultUseLanguageList;
        if (langList == null) throw new System.NullReferenceException();

        for (int i = 0; i < langList.Length; i++)
        {
            int lan = langList[i];
            if (lan == 1) continue;  // Chinese_Trad has no panel

            LanguageNodeObject node;
            if (lan == 6)
            {
                // Thailand reuses the pre-placed _thaiBoardBtn
                if (_thaiBoardBtn == null) throw new System.NullReferenceException();
                if (_thaiBoardBtn.clickArea == null) throw new System.NullReferenceException();
                int capturedLan = lan;
                _thaiBoardBtn.clickArea.onClick.AddListener(new UnityAction(() => _onSelLanguage(capturedLan)));
                if (_thaiBoardBtn.gameObject == null) throw new System.NullReferenceException();
                _thaiBoardBtn.gameObject.SetActive(true);
                if (_thaiBoardBtn.transform == null) throw new System.NullReferenceException();
                _thaiBoardBtn.transform.SetAsLastSibling();
                node = _thaiBoardBtn;
            }
            else
            {
                if (_gpLanBoard == null) throw new System.NullReferenceException();
                node = UnityEngine.Object.Instantiate<LanguageNodeObject>(_gpLanBoard);
                if (node == null || node.clickArea == null) throw new System.NullReferenceException();
                int capturedLan = lan;
                node.clickArea.onClick.AddListener(new UnityAction(() => _onSelLanguage(capturedLan)));
                if (titleTextList == null) throw new System.NullReferenceException();
                if ((uint)titleTextList.Length <= (uint)(lan - 1)) throw new System.IndexOutOfRangeException();
                if (node.lbText == null) throw new System.NullReferenceException();
                node.lbText.text = titleTextList[lan - 1];
                if (node.spCheck == null) throw new System.NullReferenceException();
                node.spCheck.SetActive(lan == 4);
                if (node.transform == null) throw new System.NullReferenceException();
                node.transform.SetParent(_lanPanelTrans);
                node.transform.localScale = Vector3.one;
                if (node.gameObject == null) throw new System.NullReferenceException();
                node.gameObject.SetActive(true);
            }
            if (_selObjPanelDict == null) throw new System.NullReferenceException();
            _selObjPanelDict.Add(lan, node);
        }

        if (_btnChange == null) throw new System.NullReferenceException();
        _btnChange.onClick.AddListener(new UnityAction(_confirmSelLanguage));
        _selLan = 4;

        if (titleTextList == null) throw new System.NullReferenceException();
        if (titleTextList.Length < 4) throw new System.IndexOutOfRangeException();
        if (confirmTextList == null) throw new System.NullReferenceException();
        if (confirmTextList.Length < 4) throw new System.IndexOutOfRangeException();
        if (_btnChangeText == null) throw new System.NullReferenceException();
        _btnChangeText.text = confirmTextList[3];
        if (_lanTitleText == null) throw new System.NullReferenceException();
        _lanTitleText.text = titleTextList[3];
    }

    // RVA: 0x18CC518  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/setSelLanguageWndEnable.c
    // [NoToLua]
    public void setSelLanguageWndEnable(bool enable)
    {
        if (_lanSelWnd != null)
        {
            _lanSelWnd.SetActive(enable);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x18CC538  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/_onSelLanguage.c
    // Ghidra: enumerate _selObjPanelDict; for each KeyValuePair{key,obj} set obj.spCheck
    // (LanguageNodeObject offset 0x20) GameObject active iff key == lan; finally _selLan = lan.
    private void _onSelLanguage(int lan)
    {
        if (_selObjPanelDict == null)
        {
            throw new NullReferenceException();
        }
        var enumerator = _selObjPanelDict.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var kv = enumerator.Current;
                LanguageNodeObject node = kv.Value;
                if (node == null)
                {
                    throw new NullReferenceException();
                }
                if (node.spCheck == null)
                {
                    throw new NullReferenceException();
                }
                node.spCheck.SetActive(kv.Key == lan);
            }
        }
        finally
        {
            enumerator.Dispose();
        }
        _selLan = lan;
    }

    // RVA: 0x18CC6CC  Ghidra: work/06_ghidra/decompiled_full/SGCLanguageSelect/_confirmSelLanguage.c
    private void _confirmSelLanguage()
    {
        ConfigMgr inst = ConfigMgr.Instance;
        if (inst != null)
        {
            inst.SetConfigVarLanguage(_selLan);
            inst = ConfigMgr.Instance;
            if (inst != null)
            {
                inst.ConfigVarSave();
                if (_lanSelWnd != null)
                {
                    _lanSelWnd.SetActive(false);
                    _setFlag = true;
                    return;
                }
            }
        }
        throw new NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SGCLanguageSelect/.ctor.c RVA 0x018cc728
    // 1-1 (verified via stringliteral.json lookups for StringLiteral_XXXXX):
    //   _selObjPanelDict = new Dictionary<int, LanguageNodeObject>();        // +0x38
    //   titleTextList    = new string[9] {                                    // +0x68
    //     "語言",   "语言",   "Language", "語言",  "Bahasa",
    //     "ภาษา",  "言語",  "语言",   "语言" };
    //   contentTextList  = new string[9] {                                    // +0x70
    //     "繁體中文","简体中文","English","Tiếng Việt","Bahasa Indonesia",
    //     "ภาษาไทย","日本語","简体中文","简体中文" };
    //   confirmTextList  = new string[9] {                                    // +0x78
    //     "確定","确定","Confirm","確定","Konfirmasi",
    //     "ยืนยัน","確定","确定","确定" };
    //   base.MonoBehaviour.ctor(this);
    // StringLit IDs (from stringliteral.json): titleList→21735,21755,7517,21735,3324,21352,21725,21755,21755
    //                                          contentList→21710,21690,5023,11657,3325,21353,21603,21690,21690
    //                                          confirmList→21673,21672,4215,21673,7493,21355,21673,21672,21672
    public SGCLanguageSelect()
    {
        _selObjPanelDict = new System.Collections.Generic.Dictionary<int, LanguageNodeObject>();
        titleTextList = new string[9] {
            "語言",   "语言",   "Language", "語言",  "Bahasa",
            "ภาษา",  "言語",  "语言",   "语言"
        };
        contentTextList = new string[9] {
            "繁體中文", "简体中文", "English", "Tiếng Việt", "Bahasa Indonesia",
            "ภาษาไทย", "日本語", "简体中文", "简体中文"
        };
        confirmTextList = new string[9] {
            "確定", "确定", "Confirm", "確定", "Konfirmasi",
            "ยืนยัน", "確定", "确定", "确定"
        };
    }
}
