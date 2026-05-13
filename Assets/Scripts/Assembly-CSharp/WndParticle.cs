// Source: Ghidra work/06_ghidra/decompiled_full/WndParticle/ (5 .c, all 1-1)
// Source: dump.cs TypeDefIndex 211 — WndParticle : IWndComponent
// Field offsets:
//   iStartLayer@0x20, iMaxLayer@0x24, sAssetName@0x28, sLuaPropertyName@0x30,
//   sLuaCallbackName@0x38, iChoose@0x40, _Wnd@0x48, _bLoadParticle@0x50,
//   _UIFXObjInst@0x58, _UIFXObjInstRender@0x60

using System;
using LuaInterface;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Progs/Particle Assign")]
public class WndParticle : IWndComponent
{
    [SerializeField] [HideInInspector] public int iStartLayer;       // 0x20
    [SerializeField] [HideInInspector] public int iMaxLayer;         // 0x24
    [HideInInspector] [SerializeField] public string sAssetName;     // 0x28
    [SerializeField] [HideInInspector] public string sLuaPropertyName;  // 0x30
    [SerializeField] [HideInInspector] public string sLuaCallbackName;  // 0x38
    [SerializeField] [HideInInspector] public int iChoose;           // 0x40
    [NonSerialized] private WndForm _Wnd;                            // 0x48
    [NonSerialized] private bool _bLoadParticle;                     // 0x50
    [NonSerialized] private GameObject _UIFXObjInst;                 // 0x58
    [NonSerialized] private SetParticlesRender _UIFXObjInstRender;   // 0x60

    // Source: Ghidra InitComponent.c RVA 0x195A9F8
    // 1-1: if (wnd != null) _Wnd = wnd;
    //      if (sAssetName != "" && _UIFXObjInst == null (Unity null)) _bLoadParticle = true;
    public override void InitComponent(WndForm wnd)
    {
        if (wnd != null)
        {
            _Wnd = wnd;
        }
        if (sAssetName != "")
        {
            if (_UIFXObjInst == null)   // Unity Object.op_Equality(_UIFXObjInst, null)
            {
                _bLoadParticle = true;
            }
        }
    }

    // Source: Ghidra DinitComponent.c RVA 0x195AAA8
    // 1-1: if (_UIFXObjInst != null) { UnityEngine.Object.Destroy(_UIFXObjInst); _UIFXObjInst = null; }
    //      _Wnd = null;
    public override void DinitComponent(WndForm wnd)
    {
        if (_UIFXObjInst != null)
        {
            UnityEngine.Object.Destroy(_UIFXObjInst);
            _UIFXObjInst = null;
        }
        _Wnd = null;
    }

    // Source: Ghidra ChangeParticle.c RVA 0x195AB58
    // 1-1: if (assetName == "") return;
    //      if (assetName == sAssetName) return;
    //      if (_UIFXObjInst != null) { Object.Destroy(_UIFXObjInst); _UIFXObjInst = null; }
    //      sAssetName = assetName; _bLoadParticle = true;
    public void ChangeParticle(string assetName)
    {
        if (assetName == "") return;
        if (assetName == sAssetName) return;
        if (_UIFXObjInst != null)
        {
            UnityEngine.Object.Destroy(_UIFXObjInst);
            _UIFXObjInst = null;
        }
        sAssetName = assetName;
        _bLoadParticle = true;
    }

    // Source: Ghidra Update.c RVA 0x195AC60
    // 1-1: if (!_bLoadParticle) return.
    //      rm = ResMgr.Instance; if rm == null → NRE.
    //      if (!rm.IsUIFXReady(sAssetName)) return.
    //      uifx = rm.GetUIParticle(sAssetName);
    //      if (uifx != null):
    //        _UIFXObjInst = Object.Instantiate(uifx, this.transform);
    //        _UIFXObjInstRender = _UIFXObjInst.GetComponent<SetParticlesRender>();
    //        _UIFXObjInst.transform.SetParent(this.gameObject.transform);
    //        _UIFXObjInst.transform.localPosition = Vector3.zero;
    //        _UIFXObjInst.transform.localRotation = Quaternion.identity;
    //        _UIFXObjInst.transform.localScale    = Vector3.one;
    //        if (_Wnd != null):
    //          if (sLuaPropertyName != "" && _Wnd is WndForm_Lua wndLua):
    //            WndForm_Lua.WndPropertyData(wndLua, 1, sLuaPropertyName, 0, _UIFXObjInst, 0, 0);
    //          if (sLuaCallbackName != "" && _Wnd is WndForm_Lua wndLua2):
    //            LuaFramework.Util.CallMethod(wndLua2.lua_obj_table, sLuaCallbackName, wndLua2);
    //      _bLoadParticle = false.
    [NoToLua]
    public void Update()
    {
        if (!_bLoadParticle) return;
        ResMgr rm = ResMgr.Instance;
        if (rm == null) throw new System.NullReferenceException();
        if (!rm.IsUIFXReady(sAssetName)) return;
        GameObject uifx = rm.GetUIParticle(sAssetName);
        if (uifx != null)
        {
            _UIFXObjInst = UnityEngine.Object.Instantiate<GameObject>(uifx, this.transform);
            if (_UIFXObjInst == null) throw new System.NullReferenceException();
            _UIFXObjInstRender = _UIFXObjInst.GetComponent<SetParticlesRender>();
            var instTrans = _UIFXObjInst.transform;
            var thisGoTrans = this.gameObject != null ? this.gameObject.transform : null;
            if (instTrans == null || thisGoTrans == null) throw new System.NullReferenceException();
            instTrans.SetParent(thisGoTrans);
            instTrans.localPosition = Vector3.zero;
            instTrans.localRotation = Quaternion.identity;
            instTrans.localScale    = Vector3.one;
            if (_Wnd != null)
            {
                // Ghidra branches both gated by `(GetType(_Wnd) == typeof(WndForm_Lua))` cast check.
                if (sLuaPropertyName != "")
                {
                    if (_Wnd.GetType() == typeof(WndForm_Lua))
                    {
                        WndForm_Lua wndLua = (WndForm_Lua)_Wnd;
                        // Ghidra: WndForm_Lua.WndPropertyData(wndLua, 1, sLuaPropertyName, 0, _UIFXObjInst, 0, 0)
                        //   → wndLua.WndPropertyData(EAct=1, name, index=0, obj=_UIFXObjInst, clear=false)
                        wndLua.WndPropertyData((WndProperty.EAct)1, sLuaPropertyName, 0, _UIFXObjInst, false);
                    }
                }
                if (sLuaCallbackName != "")
                {
                    if (_Wnd.GetType() == typeof(WndForm_Lua))
                    {
                        WndForm_Lua wndLua = (WndForm_Lua)_Wnd;
                        // Ghidra: `lVar3 = plVar11[0xe]` reads field at offset 0x70 = _sWndFormID
                        //   (string module name), then CallMethod(module, sLuaCallbackName, [wndLua]).
                        string moduleName = wndLua.sWndFormID;
                        LuaFramework.Util.CallMethod(moduleName, sLuaCallbackName, new object[] { wndLua });
                    }
                }
            }
        }
        _bLoadParticle = false;
    }

    // Source: Ghidra ResetStartOrder.c RVA 0x195B1E0
    // 1-1: if (iStartLayer != value): iStartLayer = value;
    //        if (_UIFXObjInstRender != null) _UIFXObjInstRender.ForceUpdateOrder();
    public void ResetStartOrder(int value)
    {
        if (iStartLayer != value)
        {
            iStartLayer = value;
            if (_UIFXObjInstRender != null)
            {
                _UIFXObjInstRender.ForceUpdateOrder();
            }
        }
    }

    // Source: Ghidra .ctor not exported (likely empty body).
    public WndParticle()
    {
    }
}
