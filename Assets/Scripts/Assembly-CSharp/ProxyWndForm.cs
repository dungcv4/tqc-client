// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x19620A8, 0x196271C, 0x1962BF8, 0x1962C4C, 0x1962C8C, 0x1962CA8, 0x1962CB0, 0x1962CB8,
//       0x19630F4, 0x19632C4, 0x1963384, 0x19633E4, 0x19633EC, 0x1963478, 0x1963504, 0x19635E4,
//       0x19636CC, 0x19637C8, 0x19637F8, 0x1963890
// Ghidra dir: work/06_ghidra/decompiled_full/ProxyWndForm/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 230
public class ProxyWndForm : IWndForm
{
    public ProxyWndForm() { }
    internal string _name;             // 0x10
    internal int _nStartDepth;         // 0x18
    internal GameObject _root;         // 0x20
    internal RectTransform _cacheTrans; // 0x28
    private WndFormNodeLinkList _linkList; // 0x30

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/CreateWndForm.c RVA 0x019620a8
    // 1-1 translation:
    //   1. if (_root == null) {
    //        UJDebug.LogWarning("ProxyWndForm.CreateWndForm root null, wid=" + eWndFormID + " name=" + _name);
    //        return null;
    //      }
    //      (Ghidra: uVar5 = UnityEngine_Object__op_Equality(_root, 0, 0); if (uVar5 & 1) → LogWarning + return null
    //       The warning string concats StringLit_2269 + eWndFormID.ToString() + StringLit_15462)
    //   2. wndForm = WndFormFactory.CreateWndForm(eWndFormID)
    //      if null → LogWarning("Could not factory, wid=" + eWndFormID + " name=" + _name); return null;
    //      (StringLit_2270 is the failure prefix)
    //   3. prefabPath = wndForm.GetPrefab(eWndFormID, args)   (vtable slot at *(plVar6 + 0x1c8))
    //      if (IsNullOrEmpty(prefabPath)) return null;
    //   4. WndRoot.showMask = true
    //   5. wndForm.GetBundle(eWndFormID, args, out bundleName, out objName)  (vtable +0x1d8)
    //   6. creator = new AsyncWndFormCreator(prefabPath, this, wndForm, eWndFormID, args, objName, popup)
    //   7. cached = AsyncWndFormCreator.FindCache(prefabPath)
    //      if cached != null → iter = creator.CBLoadPrefabCached(cached)
    //      else IF wndForm.IsPrefabInResource() (vtable +0x1e8):
    //          rr = Resources.LoadAsync(prefabPath, typeof(GameObject))
    //          if rr == null → printResourcesLoaderMsg(FAIL, "...") + WndRoot.showMask=false + return null
    //          else iter = creator.CBLoadPrefabSimulator(rr)
    //      else (asset bundle path):
    //          if (ProcessLunchGame.static_fields[0x10] != 0) {
    //            rf = AssetBundleManager.Instance.LoadAssetBundle(bundleName.ToLower(), null, false)
    //            if rf == null → printResourcesLoaderMsg(ASSETBUNDLE_NOT_FOUND, "...") + WndRoot.showMask=false + return null
    //            iter = creator.CBLoadPrefab(rf)
    //          } else → printResourcesLoaderMsg(FAIL,…) + WndRoot.showMask=false + return null
    //   8. co = new UJCoroutine(iter); LuaManager.Instance.StartCoroutine(co); return wndForm
    public WndForm CreateWndForm(uint eWndFormID, ArrayList args, bool popup = false)
    {
        // Ghidra: if (_root == null) → LogWarning + return null
        if (_root == null)
        {
            // Source: stringliteral.json — StringLit_2269 / StringLit_15462 concat pattern with eWndFormID.ToString()
            UJDebug.LogWarning("ProxyWndForm.CreateWndForm: _root null, wid=" + eWndFormID.ToString() + " name=" + _name);
            return null;
        }

        WndForm wndForm = WndFormFactory.CreateWndForm(eWndFormID);
        if (wndForm == null)
        {
            // Source: StringLit_2270 — factory-fail prefix
            UJDebug.LogWarning("ProxyWndForm.CreateWndForm: factory returned null, wid=" + eWndFormID.ToString() + " name=" + _name);
            return null;
        }

        // Ghidra: prefabPath = wndForm.GetPrefab(eWndFormID, args)  via vtable slot 0x1c8
        string prefabPath = wndForm.GetPrefab(eWndFormID, args);
        if (string.IsNullOrEmpty(prefabPath)) return null;

        WndRoot.showMask = true;

        string bundleName;
        string objName;
        wndForm.GetBundle(eWndFormID, args, out bundleName, out objName);

        var creator = new AsyncWndFormCreator(prefabPath, this, wndForm, eWndFormID, args, objName, popup);

        // Ghidra: cached = AsyncWndFormCreator.FindCache(prefabPath)
        GameObject cached = AsyncWndFormCreator.FindCache(prefabPath);

        IEnumerator iter = null;
        if (cached != null)
        {
            iter = creator.CBLoadPrefabCached(cached);
        }
        else if (wndForm.IsPrefabInResource())
        {
            // Ghidra: Resources.LoadAsync(prefabPath, typeof(GameObject))
            ResourceRequest rr = UnityEngine.Resources.LoadAsync<GameObject>(prefabPath);
            if (rr == null)
            {
                // Ghidra: printResourcesLoaderMsg(GET_FROM=13 (RESOURCE_FAIL), Format(StringLit_5691,...))
                ResourcesLoader.printResourcesLoaderMsg(ResourcesLoader.GET_FROM.RESOURCE_FAIL, string.Format("{0} {1} ({2})", prefabPath, bundleName, "RESOURCE_FAIL"), null);
                WndRoot.showMask = false;
                return null;
            }
            iter = creator.CBLoadPrefabSimulator(rr);
        }
        else
        {
            // Ghidra: gated by ProcessLunchGame.static_fields[0x10] (opaque bool — see AssetBundleManager LoadAssetBundle TODO)
            // If gate fails → printResourcesLoaderMsg(GET_FROM=10 (FAIL))
            // If gate ok → AssetBundleManager.Instance.LoadAssetBundle(bundleName.ToLower(), null, false)
            //              if rf == null → printResourcesLoaderMsg(GET_FROM=11 (ASSETBUNDLE_NOT_FOUND))
            if (string.IsNullOrEmpty(bundleName))
            {
                // bundleName.ToLower() would NRE in Ghidra (lVar11 = ... == 0 → FUN_015cb8fc)
                ResourcesLoader.printResourcesLoaderMsg(ResourcesLoader.GET_FROM.FAIL, string.Format("{0} {1} ({2})", prefabPath, bundleName, "FAIL"), null);
                WndRoot.showMask = false;
                return null;
            }
            RequestFile rf = AssetBundleManager.Instance != null
                ? AssetBundleManager.Instance.LoadAssetBundle(bundleName.ToLower(), null, false)
                : null;
            if (rf == null)
            {
                ResourcesLoader.printResourcesLoaderMsg(ResourcesLoader.GET_FROM.ASSETBUNDLE_NOT_FOUND, string.Format("{0} {1} ({2})", prefabPath, bundleName, "ASSETBUNDLE_NOT_FOUND"), null);
                WndRoot.showMask = false;
                return null;
            }
            iter = creator.CBLoadPrefab(rf);
        }

        // Source: Ghidra ProxyWndForm/CreateWndForm.c lines 159, 205, 223 — `*(long *)(*(long *)(lVar11 + 0xb8) + 8)`
        //   = Main.static_fields + offset 8 = Main.s_instance (MonoBehaviour singleton @ Main.cs:0x8).
        //   The coroutine host is Main.Instance (NOT LuaManager.Instance — verified via dump.cs:
        //   Main class static layout `Version@0x0, s_instance@0x8`; ScreenOriginWidth@0x44 in same
        //   class confirms PTR_DAT_03448380 maps to Main).
        if (iter != null)
        {
            var co = new UJCoroutine(iter);
            var host = Main.Instance;
            if (host == null)
            {
                // Ghidra fallthrough when host MB is null → FUN_015cb8fc (NRE)
                throw new System.NullReferenceException("ProxyWndForm.CreateWndForm: coroutine host null");
            }
            UnityEngine.MonoBehaviour mb = host;
            mb.StartCoroutine(co);
        }

        return wndForm;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/IWndForm.CreateWndFormAsync.c RVA 0x0196271c
    // 1-1 translation:
    //   if (wnd == null) NRE.
    //   if (wnd.isDestroy)  → wnd.Destroy(); return;
    //   if (resObj == null) NRE.
    //   UJDebug.LogTrace("CreateWndFormAsync resObj=" + resObj.name);   (StringLit_4451 prefix)
    //   node = WndFormNode.Create(eWndFormID, resObj);  if null → return.
    //   nodeCanvas = node._canvas (offset 0x10)
    //   nodeCanvas.worldCamera = WndRoot.uiCamera   (PTR_DAT_034483c0 static load)
    //   nodeCanvas.renderMode = 0  (Overlay)
    //   nodeCanvas.overrideSorting = true
    //   node._wndTrans.SetParent(this._cacheTrans)
    //   then set anchorMin/Max from PTR_DAT_03446a48 (Vector2.zero/one constants),
    //     offsetMin/Max, anchoredPosition, sizeDelta = (0,0), localScale = Vector3.one
    //     (PTR_DAT_03446bc8), localRotation = Quaternion.identity (PTR_DAT_03446b08), localPosition = Vector3.zero
    //   ok = wnd.Create(eWndFormID, parent=null (no WndForm parent at proxy level), node, args)
    //   if !ok → Object.Destroy(node._wndTrans.gameObject); return.
    //   else: node._parent = this; (Ghidra writes *(node + 0x30) = this + write barrier)
    //         this._linkList.Push(node)
    //         wnd.AfterCreate(args)
    void IWndForm.CreateWndFormAsync(WndForm wnd, GameObject resObj, uint eWndFormID, ArrayList args, bool popup)
    {
        if (wnd == null) throw new System.NullReferenceException("ProxyWndForm.CreateWndFormAsync: wnd null");

        if (wnd.isDestroy)
        {
            wnd.Destroy();
            return;
        }

        if (resObj == null) throw new System.NullReferenceException("ProxyWndForm.CreateWndFormAsync: resObj null");

        // Source: StringLit_4451 — log prefix concat with resObj.name
        UJDebug.LogTrace("ProxyWndForm.CreateWndFormAsync: " + resObj.name);

        WndFormNode node = WndFormNode.Create(eWndFormID, resObj);
        if (node == null) return;

        Canvas nodeCanvas = node._canvas;
        if (nodeCanvas == null) throw new System.NullReferenceException("ProxyWndForm.CreateWndFormAsync: node._canvas null");

        // Ghidra: Canvas.worldCamera = WndRoot.uiCamera (loaded from PTR_DAT_034483c0 static block +0x10)
        nodeCanvas.worldCamera = WndRoot.uiCamera;

        // Ghidra: set_renderMode(0) — RenderMode.ScreenSpaceOverlay = 0
        nodeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        nodeCanvas.overrideSorting = true;

        RectTransform trans = node._wndTrans;
        if (trans == null) throw new System.NullReferenceException("ProxyWndForm.CreateWndFormAsync: node._wndTrans null");

        trans.SetParent(_cacheTrans);

        // Ghidra: anchorMin/Max read from PTR_DAT_03446a48 (Vector2 const pair {(0,0),(1,1)})
        trans.anchorMin = Vector2.zero;
        trans.anchorMax = Vector2.one;
        trans.offsetMin = Vector2.zero;
        trans.offsetMax = Vector2.zero;
        trans.anchoredPosition = Vector2.zero;
        trans.sizeDelta = Vector2.zero;
        // Ghidra: localScale from PTR_DAT_03446bc8 (Vector3 const = (1,1,1) — offset 0xc/0x10/0x14 of static body)
        trans.localScale = Vector3.one;
        // localRotation from PTR_DAT_03446b08 (Quaternion.identity)
        trans.localRotation = Quaternion.identity;
        // localPosition from PTR_DAT_03446bc8 (Vector3.zero — offset 0x0/0x4/0x8 of static body)
        trans.localPosition = Vector3.zero;

        // Ghidra: WndForm__Create(wnd, eWndFormID, parent=0 (null), node, args)
        bool ok = wnd.Create(eWndFormID, null, node, args);

        if (!ok)
        {
            // Ghidra: get gameObject of node._wndTrans, Object.Destroy(go)
            if (node._wndTrans != null)
            {
                GameObject go = node._wndTrans.gameObject;
                if (go != null) UnityEngine.Object.Destroy(go);
            }
            return;
        }

        // Ghidra: *(node + 0x30) = this (with write barrier thunk_FUN_015ee8c4)
        node._parent = this;

        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList null");
        _linkList.Push(node);

        wnd.AfterCreate(args);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/UpdateOrder.c RVA 0x01962bf8
    public void UpdateOrder(ref int depth)
    {
        if (_linkList != null)
        {
            if (_linkList.OrderChanged)
            {
                WndFormNode node = _linkList.Head;
                _linkList.OrderChanged = false;
                for (; node != null; node = node._next)
                {
                    if (node._body == null)
                    {
                        throw new System.NullReferenceException();
                    }
                    node._body.UpdateOrder(ref depth);
                }
            }
            return;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/TopNode.c RVA 0x01962c4c
    public void TopNode(WndFormNode node)
    {
        if (_linkList != null)
        {
            _linkList.Remove(node);
            if (_linkList != null)
            {
                _linkList.Push(node);
                return;
            }
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/RemoveNode.c RVA 0x01962c8c
    public void RemoveNode(WndFormNode node)
    {
        if (_linkList != null)
        {
            _linkList.Remove(node);
            return;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/get_defaultDepth.c RVA 0x01962ca8
    public int defaultDepth { get { return _nStartDepth; } }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/get_rectTrans.c RVA 0x01962cb0
    public RectTransform rectTrans { get { return _cacheTrans; } }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/.ctor.c RVA 0x01962cb8
    // 1-1:
    //   _linkList = new WndFormNodeLinkList();
    //   System.Object.ctor(this);
    //   _name = string.IsNullOrEmpty(name) ? "WndFormProxy" (StringLit_9267) : name;
    //   _nStartDepth = nStartDepth;
    //   _root = new GameObject(_name);
    //   if (_root == null) NRE;
    //   var uiCam = WndRoot.uiCamera;  (PTR_DAT_034483c0 → static block → s_camera at +0)
    //   if (uiCam != null && uiCam.gameObject != null) {
    //     _root.layer = uiCam.gameObject.layer;
    //     var rt = _root.GetComponent<RectTransform>();
    //     if (rt == null) rt = _root.AddComponent<RectTransform>();
    //     _cacheTrans = rt;
    //     rt.SetParent(uiCam.transform);
    //     rt.localPosition = Vector3.zero;     // PTR_DAT_03446bc8 = Vector3.zero data
    //     rt.localRotation = Quaternion.identity;  // PTR_DAT_03446b08
    //     rt.localScale = Vector3.one;             // PTR_DAT_03446bc8+0xc (another Vector3 const)
    //     rt.anchorMin = (0, 0);
    //     rt.anchorMax = (1, 1);
    //     rt.anchoredPosition = (0, 0);
    //     rt.sizeDelta = (0, 0);
    //     _root.SetActive(true);
    //     return;
    //   }
    //   throw NRE  (FUN_015cb8fc fallthrough)
    public ProxyWndForm(string name, int nStartDepth = 1)
    {
        // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/.ctor.c RVA 0x01962CB8
        // 1-1:
        //   _linkList = new WndFormNodeLinkList();
        //   base..ctor();
        //   _name = string.IsNullOrEmpty(name) ? "WndFormProxy" : name;     (StringLit_9267)
        //   _nStartDepth = nStartDepth;
        //   _root = new GameObject(_name);
        //   if (_root == null) NRE;
        //   if (WndRoot.s_root != null) {
        //     _root.layer = WndRoot.s_root.layer;
        //     RectTransform rt = _root.GetComponent<RectTransform>();
        //     if (rt == null) rt = _root.AddComponent<RectTransform>();
        //     _cacheTrans = rt;
        //     rt.SetParent(WndRoot.s_root.transform);   ← parent is WndRoot.s_root (= GUI_Root), NOT UICamera
        //     localPosition/Rotation/Scale = identity;
        //     anchorMin=(0,0); anchorMax=(1,1); anchoredPos=(0,0); sizeDelta=(0,0);
        //     _root.SetActive(true);
        //   } else NRE;
        _linkList = new WndFormNodeLinkList();

        _name = string.IsNullOrEmpty(name) ? "WndFormProxy" : name;
        _nStartDepth = nStartDepth;
        _root = new GameObject(_name);
        if (_root == null) throw new System.NullReferenceException();

        GameObject wndRootGo = WndRoot.root;   // Ghidra: WndRoot.s_root (static_fields[0])
        if (wndRootGo != null)
        {
            _root.layer = wndRootGo.layer;
            RectTransform rt = _root.GetComponent<RectTransform>();
            if (rt == null) rt = _root.AddComponent<RectTransform>();
            _cacheTrans = rt;

            rt.SetParent(wndRootGo.transform);    // ← parent is GUI_Root (was UICamera in earlier chế cháo)
            rt.localPosition = Vector3.zero;
            rt.localRotation = Quaternion.identity;
            rt.localScale = Vector3.one;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            _root.SetActive(true);
            return;
        }

        // Ghidra fallthrough to FUN_015cb8fc — NRE
        throw new System.NullReferenceException("ProxyWndForm.ctor: WndRoot.s_root not initialized");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/Update.c RVA 0x019630f4
    // 1-1: iterate _linkList nodes from Head, advance via _next. For each node:
    //   if (_body == null) NRE
    //   read nextN = node._next (Ghidra reads BEFORE branching)
    //   if (!body.isDestroy)            → body.Update(dTime)
    //   else if (!node._hasAnimatorParams (offset 0x53)) → body.Destroy()
    //   else if (node._animator == null) NRE
    //   else if (!animator.isInitialized) → body.Destroy()
    //   else:
    //       info = animator.GetCurrentAnimatorStateInfo(0)
    //       if (!info.IsName(StringLit_5205 = "Hide"))   → animator.SetTrigger(StringLit_5204 = "Show")
    //       else if (info.normalizedTime >= 1.0) {
    //            if (node._wndTrans == null) NRE; go = wndTrans.gameObject; if (go == null) NRE;
    //            go.SetActive(false); body.Destroy();
    //       }
    // After loop: depth = _nStartDepth; UpdateOrder(ref depth);
    public void Update(float dTime)
    {
        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList");
        WndFormNode node = _linkList.Head;
        while (node != null)
        {
            if (node._body == null) throw new System.NullReferenceException("WndFormNode._body");
            WndFormNode next = node._next; // Ghidra reads _next BEFORE branching (offset 0x48)

            if (!node._body.isDestroy)
            {
                node._body.Update(dTime);
            }
            else if (!node._hasAnimatorParams)
            {
                node._body.Destroy();
            }
            else
            {
                if (node._animator == null) throw new System.NullReferenceException("WndFormNode._animator");
                if (!node._animator.isInitialized)
                {
                    node._body.Destroy();
                }
                else
                {
                    // Source: StringLit_5204 = "Show", StringLit_5205 = "Hide" (matched against WndForm.cs static Update body).
                    var info = node._animator.GetCurrentAnimatorStateInfo(0);
                    if (!info.IsName("Hide"))
                    {
                        node._animator.SetTrigger("Show");
                    }
                    else
                    {
                        var info2 = node._animator.GetCurrentAnimatorStateInfo(0);
                        if (info2.normalizedTime >= 1f)
                        {
                            if (node._wndTrans == null) throw new System.NullReferenceException("WndFormNode._wndTrans");
                            var go = node._wndTrans.gameObject;
                            if (go == null) throw new System.NullReferenceException("WndFormNode._wndTrans.gameObject");
                            go.SetActive(false);
                            node._body.Destroy();
                        }
                    }
                }
            }
            node = next;
        }

        int depth = _nStartDepth;
        UpdateOrder(ref depth);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/FreeWndForms.c RVA 0x019632c4
    // Parameterless overload: identical to FreeWndForms(0) — iterates all nodes (front-only via _front=0x40),
    // calls body.PostDestroy(); on each. Ghidra body:
    //   if (_linkList == null) NRE.
    //   node = _linkList._head (0x10)
    //   while (node != null) {
    //     body = node._body (0x38); nextN = node._next (0x48)  ← actually 0x48 in Ghidra
    //     if (body == null) NRE; else body.PostDestroy();
    //     node = nextN;
    //   }
    // Note: parameterless variant uses same loop but no wID filter — see joined block at 0x01a63398
    // shared with FreeWndForms(uint) where param_2==0 means "no filter".
    public void FreeWndForms()
    {
        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList");
        WndFormNode node = _linkList.Head;
        while (node != null)
        {
            WndForm body = node._body;
            WndFormNode nextN = node._next;
            if (body == null) throw new System.NullReferenceException("WndFormNode._body");
            body.PostDestroy();
            node = nextN;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/FreeWndForms.c RVA 0x01963384
    // 1-1: iterate _linkList; for each node, call body.PostDestroy() unless body.wID == ignoreWID.
    // Ghidra control flow:
    //   if (_linkList == null) NRE.
    //   node = _linkList._head;
    //   loop:
    //     if (node == null) return;
    //     body = node._body (0x38)
    //     nextN = node._next (0x48)
    //     if (ignoreWID == 0) {
    //        if (body == null) NRE;
    //        body.PostDestroy();   (LAB_01a633c8)
    //     } else {
    //        if (body == null) NRE;
    //        if (body._wID (0x50 as int) != ignoreWID) body.PostDestroy();
    //     }
    //     node = nextN;
    //     goto loop;
    public void FreeWndForms(uint ignoreWID)
    {
        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList");
        WndFormNode node = _linkList.Head;
        while (node != null)
        {
            WndForm body = node._body;
            WndFormNode nextN = node._next;
            if (body == null) throw new System.NullReferenceException("WndFormNode._body");
            if (ignoreWID == 0u || body.wID != ignoreWID)
            {
                body.PostDestroy();
            }
            node = nextN;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/FadeOutWndForms.c RVA 0x019633e4
    // Parameterless-wID overload (fDuration only): identical to FadeOutWndForms(fDuration, 0).
    // Ghidra source is the shared body at 0x01a633ec with param_3==0 (ignoreWID).
    public void FadeOutWndForms(float fDuration)
    {
        FadeOutWndForms(fDuration, 0u);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/FadeOutWndForms.c RVA 0x019633ec
    // 1-1: iterate _linkList; skip destroyed bodies; call WndForm__SetFadeOut(body, fDuration, true) unless
    // body.wID == ignoreWID. Ghidra reads _next (0x48) at top of loop body before isDestroy check.
    //   if (_linkList == null) NRE.
    //   node = _linkList._head;
    //   while node != null:
    //     if (node._body == null) NRE;
    //     nextN = node._next;
    //     if (!body.isDestroy) {
    //       if (ignoreWID == 0) WndForm.SetFadeOut(body, fDuration, true)
    //       else if (body._wID != ignoreWID) WndForm.SetFadeOut(body, fDuration, true)
    //     }
    //     node = nextN;
    // Note: signature in Ghidra has SetFadeOut(fDuration, body, true) — first arg in registers is fDuration.
    public void FadeOutWndForms(float fDuration, uint ignoreWID)
    {
        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList");
        WndFormNode node = _linkList.Head;
        while (node != null)
        {
            if (node._body == null) throw new System.NullReferenceException("WndFormNode._body");
            WndFormNode nextN = node._next;
            WndForm body = node._body;
            if (!body.isDestroy)
            {
                if (ignoreWID == 0u || body.wID != ignoreWID)
                {
                    body.SetFadeOut(fDuration, true);
                }
            }
            node = nextN;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/FadeOutWndForm.c RVA 0x01963478
    // 1-1: if (wID == 0) return immediately (Ghidra guard at top).
    //   if (_linkList == null) NRE.
    //   node = _linkList._head;
    //   loop:
    //     if (node == null) return.
    //     if (node._body == null) NRE.
    //     nextN = node._next.
    //     if (!body.isDestroy && body._wID == wID) { WndForm.SetFadeOut(body, fDuration, true); return; }
    //     node = nextN;
    public void FadeOutWndForm(float fDuration, uint wID)
    {
        if (wID == 0u) return;
        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList");
        WndFormNode node = _linkList.Head;
        while (node != null)
        {
            if (node._body == null) throw new System.NullReferenceException("WndFormNode._body");
            WndFormNode nextN = node._next;
            WndForm body = node._body;
            if (!body.isDestroy && body.wID == wID)
            {
                body.SetFadeOut(fDuration, true);
                return;
            }
            node = nextN;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/ProcessKeyClick.c RVA 0x01963504
    // Single-arg overload: shared with 3-arg via default eType=KeyDown(11), ctrl=false.
    // Match WndForm.cs convention (RVA 0x01a075bc).
    public bool ProcessKeyClick(KeyCode keyCode)
    {
        return ProcessKeyClick(keyCode, EventType.KeyDown, false);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/ProcessKeyClick.c RVA 0x019635e4
    // Two-arg overload: shared with 3-arg via default ctrl=false.
    // Match WndForm.cs convention (RVA 0x01a076ac).
    public bool ProcessKeyClick(KeyCode keyCode, EventType eType)
    {
        return ProcessKeyClick(keyCode, eType, false);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/ProcessKeyClick.c RVA 0x019636cc
    // 1-1:
    //   if (WndRoot.showMask) return true;  (masked → swallow key)
    //   if (_linkList == null) NRE.
    //   node = _linkList._end (offset 0x18)  — walk from END backwards via _front (0x40)
    //   while (node != null) {
    //     if (node._body == null) NRE;
    //     prevN = node._front;
    //     if (!body.isDestroy) {
    //       if (node._canvas == null || node._canvas.gameObject == null) NRE;
    //       go = node._canvas.gameObject;
    //       if (go.activeSelf) {
    //         if (body.ProcessKeyClick(keyCode, eType, ctrl)) return true;
    //       }
    //     }
    //     node = prevN;
    //   }
    //   return false;
    public bool ProcessKeyClick(KeyCode keyCode, EventType eType, bool ctrl)
    {
        if (WndRoot.showMask) return true;
        if (_linkList == null) throw new System.NullReferenceException("ProxyWndForm._linkList");
        WndFormNode node = _linkList.End;
        while (node != null)
        {
            if (node._body == null) throw new System.NullReferenceException("WndFormNode._body");
            WndFormNode prevN = node._front;
            WndForm body = node._body;
            if (!body.isDestroy)
            {
                if (node._canvas == null) throw new System.NullReferenceException("WndFormNode._canvas");
                var go = node._canvas.gameObject;
                if (go == null) throw new System.NullReferenceException("WndFormNode._canvas.gameObject");
                if (go.activeSelf)
                {
                    if (body.ProcessKeyClick(keyCode, eType, ctrl)) return true;
                }
            }
            node = prevN;
        }
        return false;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/SetTop.c RVA 0x019637c8
    public void SetTop()
    {
        if (_linkList != null)
        {
            // Iterate from _end (offset 0x18 of WndFormNodeLinkList) walking _front (offset 0x40 of WndFormNode)
            for (WndFormNode node = _linkList.End; node != null; node = node._front)
            {
                node._top = true;
            }
            return;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/SetActive.c RVA 0x019637f8
    public void SetActive(bool value)
    {
        if (_root != null)
        {
            _root.SetActive(value);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/ProxyWndForm/SetWndFormShift.c RVA 0x01963890
    public void SetWndFormShift(int shiftPos)
    {
        if (_cacheTrans != null)
        {
            Vector3 lp = _cacheTrans.localPosition;
            _cacheTrans.localPosition = new Vector3((float)shiftPos, lp.y, lp.z);
        }
    }
}
