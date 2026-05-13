// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18CDD88, 0x18CDDD0, 0x18CDDD8, 0x18CDE30, 0x18CDE34, 0x18CDE38, 0x18CDF4C, 0x18CDFB8, 0x18CDFD8, 0x18CE174, 0x18CE1D4
// Ghidra dir: work/06_ghidra/decompiled_full/SGCRegionSelect/
// dump.cs class 'SGCRegionSelect' (TypeDefIndex: 686)

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

public class SGCRegionSelect : MonoBehaviour
{
    // RVA: 0x18CDDD0  Property accessor — delegates to setFlag
    private static SGCRegionSelect s_instance;
    public GameObject _regionSelWnd;
    public RegionNodeObject _gpRegionBoard;
    public RegionNodeObject _thaiRegionBoard;
    public RectTransform _regionPanelTrans;
    public Button _btnConfirm;
    public Text _regionTitleText;
    public Text _btnConfirmText;
    public AdvImage advImg;
    private Dictionary<int, RegionNodeObject> _selObjPanelDict;
    private int _selRegion;
    private bool _setFlag;
    private string[] titleTextList;
    private string[] confirmTextList;

    // RVA: 0x18CDD88  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/get_Instance.c
    public static SGCRegionSelect Instance { get { return s_instance; } }

    // RVA: 0x18CDDD0  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/get_setFlag.c
    public bool setFlag { get { return _setFlag; } }

    // RVA: 0x18CDDD8  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/Awake.c
    private void Awake()
    {
        s_instance = this;
    }

    // RVA: 0x18CDE30  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/Start.c
    private void Start()
    {
        return;
    }

    // RVA: 0x18CDE34  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/Update.c
    private void Update()
    {
        return;
    }

    // RVA: 0x18CDE38  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/InitRegionComponent.c
    // Ghidra body: ConfigMgr.Instance.GetConfigVarLanguage() — value discarded —
    // then UJDebug.LogError(StringLiteral_2661) and return.
    public void InitRegionComponent()
    {
        ConfigMgr inst = ConfigMgr.Instance;
        if (inst == null)
        {
            throw new NullReferenceException();
        }
        inst.GetConfigVarLanguage();
        UJDebug.LogError("StringLiteral_2661");  // TODO: confidence:medium — resolve StringLiteral_2661 via stringliteral.json (RDATA addr 0x03460a38)
    }

    // RVA: 0x18CDFB8  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/setSelRegionWndEnable.c
    public void setSelRegionWndEnable(bool enable)
    {
        if (_regionSelWnd != null)
        {
            _regionSelWnd.SetActive(enable);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x18CDFD8  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/_onSelRegion.c
    // Ghidra: enumerate _selObjPanelDict; for each KeyValuePair{key, RegionNodeObject node}
    // set node.spCheck (offset 0x20) active iff key == region; finally _selRegion = region;
    // then call _checkBtnState().
    private void _onSelRegion(int region)
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
                RegionNodeObject node = kv.Value;
                if (node == null)
                {
                    throw new NullReferenceException();
                }
                if (node.spCheck == null)
                {
                    throw new NullReferenceException();
                }
                node.spCheck.SetActive(kv.Key == region);
            }
        }
        finally
        {
            enumerator.Dispose();
        }
        _selRegion = region;
        _checkBtnState();
    }

    // RVA: 0x18CE174  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/_confirmSelRegion.c
    // Ghidra: ConfigMgr.SetConfigVarInt(0xd /* ConfigVar.SGCRegion */, _selRegion);
    //         ConfigMgr.ConfigVarSave(); _regionSelWnd.SetActive(false); _setFlag = true;
    private void _confirmSelRegion()
    {
        ConfigMgr inst = ConfigMgr.Instance;
        if (inst != null)
        {
            inst.SetConfigVarInt(0xd, _selRegion);
            inst = ConfigMgr.Instance;
            if (inst != null)
            {
                inst.ConfigVarSave();
                if (_regionSelWnd != null)
                {
                    _regionSelWnd.SetActive(false);
                    _setFlag = true;
                    return;
                }
            }
        }
        throw new NullReferenceException();
    }

    // RVA: 0x18CDF4C  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/_checkBtnState.c
    // Ghidra: if _selRegion == -1: advImg.GrayScaleAmount = 1.0f; _btnConfirm.interactable = false;
    //         else:                advImg.GrayScaleAmount = 0.0f; _btnConfirm.interactable = true;
    // (The 0x3f800000 magic in Ghidra is float bits for 1.0f.)
    private void _checkBtnState()
    {
        if (advImg != null)
        {
            if (_selRegion == -1)
            {
                advImg.GrayScaleAmount = 1.0f;
                if (_btnConfirm == null)
                {
                    throw new NullReferenceException();
                }
                _btnConfirm.interactable = false;
            }
            else
            {
                advImg.GrayScaleAmount = 0.0f;
                if (_btnConfirm == null)
                {
                    throw new NullReferenceException();
                }
                _btnConfirm.interactable = true;
            }
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x18CE1D4  Ghidra: work/06_ghidra/decompiled_full/SGCRegionSelect/.ctor.c
    public SGCRegionSelect()
    {
    }
}
