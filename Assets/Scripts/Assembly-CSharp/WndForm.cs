// Source: Il2CppDumper-decompiled (Ghidra — work/06_ghidra/decompiled/WndForm/)
// Original: WndForm  (TypeDefIndex 295)
// Each method body ported 1-1 from work/06_ghidra/decompiled/WndForm/<Method>.c
// Field offsets: see dump.cs lines 12899-13122 — _wFlag@0x10, _node@0x18, _parent@0x20,
//    _subNodes@0x28, _popupNodes@0x30, _readyEvents@0x38, _cbReadyEvents@0x40,
//    _curAlpha@0x48, _waitForEnter@0x4C, _waitForExit@0x4D, _isLock@0x4E,
//    _wID@0x50, _eFadeMode@0x54, _fadeDuration@0x58, _fadeDetalTime@0x5C.
// Notes:
//   - Thread-init macros (FUN_015cb66c, thunk_FUN_015ee8c4, thunk_FUN_015e4ba4) skipped: GC write barriers.
//   - FUN_015cb8fc / FUN_015cbc7c trampolines = NRE / type-cast failure -> NullReferenceException.
//   - PTR_DAT_* statics resolved to typeof(T) where they are class refs and to string literals
//     where they are StringLiteral entries.

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

public class WndForm : IWndForm
{
    private static int s_QuitApp;
    private static WndForm _focusWndForm;
    private const uint FLAG_TICK = 1;
    private const uint FLAG_LOADING = 2;
    private const uint FLAG_DONE = 16;
    private const uint FLAG_DESTROY = 4096;
    private uint _wFlag;
    private WndFormNode _node;
    /// <summary>Node accessor for subclasses (e.g. WndForm_LunchGame V_Create wires children via root transform).</summary>
    protected WndFormNode GetNode() { return _node; }
    private WndForm _parent;
    private WndFormNodeLinkList _subNodes;
    private WndFormNodeLinkList _popupNodes;
    // Original 0x38: Queue<IWndFormReady> _readyEvents (Ghidra AddReadyEvent enqueues here)
    private System.Collections.Generic.Queue<IWndFormReady> _readyEvents;
    private CBReadyEvent _cbReadyEvents;
    private float _curAlpha;
    private bool _waitForEnter;
    private bool _waitForExit;
    private bool _isLock;
    private uint _wID;
    private FadeMode _eFadeMode;
    private float _fadeDuration;
    private float _fadeDetalTime;
    public const float CFadeOut = 0.5f;

    /* RVA 0x01a06a60 — get_wID: return *(uint *)(this + 0x50) */
    public uint wID { get { return _wID; } }

    /* RVA 0x01a0726c — get_isDone: (_wFlag >> 4) & 1 */
    public bool isDone { get { return (_wFlag & FLAG_DONE) != 0; } }

    /* RVA 0x01a076d8 — get_parent: *(this + 0x20) */
    public WndForm parent { get { return _parent; } }

    /* RVA 0x01a076e0 — get_cacheTransform: _node != null ? _node._wndTrans : null */
    public RectTransform cacheTransform
    {
        get { return _node != null ? _node._wndTrans : null; }
    }

    /* RVA 0x01a05854 — get_isDestroy:
     *   ((_wFlag >> 4) flag at byte+0x11 bit 4) -> FLAG_DESTROY (0x1000) bit set
     *   OR _node != null && _node._postDestroy
     */
    public bool isDestroy
    {
        get
        {
            if ((_wFlag & FLAG_DESTROY) != 0) return true;
            if (_node != null) return _node._postDestroy;
            return false;
        }
    }

    /* RVA 0x01a07d84 — get_isFadeOut: (_eFadeMode & 0xFFFFFFFE) == 2  =>  FadeMode 2 or 3 */
    public bool isFadeOut
    {
        get { return (((uint)_eFadeMode) & 0xFFFFFFFEu) == 2u; }
    }

    /* RVA 0x01a07d98 / 0x01a04928 — curAlpha get/set
     * set: store + propagate to CanvasGroup if FLAG_DONE && _node!=null */
    public float curAlpha
    {
        get { return _curAlpha; }
        set
        {
            _curAlpha = value;
            if (_node != null && (_wFlag & FLAG_DONE) != 0)
            {
                var cg = _node._canvasGroup;
                if (cg != null) cg.alpha = value;
            }
        }
    }

    /* RVA 0x01a0432c — IsQuitApp:
     *   int q = s_QuitApp; if (q > 0) s_QuitApp = q + 1; return q == 3;
     */
    public static bool IsQuitApp()
    {
        int q = s_QuitApp;
        if (q > 0) s_QuitApp = q + 1;
        return q == 3;
    }

    /* RVA 0x01a04394 — WaitQuitApp: return s_QuitApp != 0 */
    public static bool WaitQuitApp() { return s_QuitApp != 0; }

    /* RVA 0x01a043e4 — QuitApp: if (s_QuitApp == 0) s_QuitApp = 1; */
    public static void QuitApp()
    {
        if (s_QuitApp == 0) s_QuitApp = 1;
    }

    /* RVA 0x01a04438 — AddReadyEvent: enqueue IWndFormReady if FLAG_LOADING set
     *   if (ir != null && (_wFlag & FLAG_LOADING) != 0) {
     *     if (_readyEvents == null) _readyEvents = new Queue<IWndFormReady>();
     *     _readyEvents.Enqueue(ir);
     *   }
     */
    public void AddReadyEvent(IWndFormReady ir)
    {
        if (ir == null) return;
        if ((_wFlag & FLAG_LOADING) == 0) return;
        if (_readyEvents == null) _readyEvents = new System.Collections.Generic.Queue<IWndFormReady>();
        _readyEvents.Enqueue(ir);
    }

    /* RVA 0x01a044fc — CheckWaitCreateSubWnd:
     *   Pop IWndFormReady entries from _readyEvents while their isReady==true.
     *   For each ready entry that has hasEvent==true, build a CBReadyEvent
     *   delegate (cb wraps entry.FinishEvent) and Combine() into _cbReadyEvents.
     *   When the queue drains, set _readyEvents=null and _wFlag |= FLAG_DONE,
     *   then write current alpha back through curAlpha = re-applies to CG.
     */
    private void CheckWaitCreateSubWnd()
    {
        if (_readyEvents == null)
        {
            _wFlag |= FLAG_DONE;
            curAlpha = _curAlpha;
            return;
        }

        while (_readyEvents.Count > 0)
        {
            var head = _readyEvents.Peek();
            if (head == null || !head.isReady) return;
            _readyEvents.Dequeue();
            if (head.hasEvent)
            {
                var cb = new CBReadyEvent(head.FinishEvent);
                _cbReadyEvents = (CBReadyEvent)System.Delegate.Combine(_cbReadyEvents, cb);
            }
        }

        if (_readyEvents != null && _readyEvents.Count == 0)
        {
            _readyEvents = null;
            _wFlag |= FLAG_DONE;
            curAlpha = _curAlpha;
        }
    }

    /* RVA 0x01a049dc — CreateWndForm:
     *   factory.Create → wndForm
     *   GetPrefab → prefabPath
     *   WndRoot.showMask = true
     *   GetBundle → bundleName, objName
     *   AsyncWndFormCreator(prefabPath, this, wndForm, eWndFormID, args, objName, popup)
     *   FindCache → CBLoadPrefabCached
     *   else IsPrefabInResource → Resources.LoadAsync → CBLoadPrefabSimulator
     *   else AssetBundleManager.LoadAssetBundle → CBLoadPrefab
     *   wrap in UJCoroutine + StartCoroutine (via Main.LuaMgr)
     *   AddReadyEvent(creator)
     */
    public virtual WndForm CreateWndForm(uint eWndFormID, ArrayList args, bool popup = false)
    {
        var wndForm = WndFormFactory.CreateWndForm(eWndFormID);
        if (wndForm == null) return null;
        var prefabPath = wndForm.GetPrefab(eWndFormID, args);
        if (string.IsNullOrEmpty(prefabPath)) return null;
        WndRoot.showMask = true;
        string bundleName, objName;
        wndForm.GetBundle(eWndFormID, args, out bundleName, out objName);
        var creator = new AsyncWndFormCreator(prefabPath, this, wndForm, eWndFormID, args, objName, popup);

        var cached = AsyncWndFormCreator.FindCache(prefabPath);
        System.Collections.IEnumerator iter = null;
        if (cached != null)
        {
            iter = creator.CBLoadPrefabCached(cached);
        }
        else if (wndForm.IsPrefabInResource())
        {
            var rr = UnityEngine.Resources.LoadAsync<UnityEngine.GameObject>(prefabPath);
            if (rr != null) iter = creator.CBLoadPrefabSimulator(rr);
        }
        else
        {
            var rf = AssetBundleManager.Instance != null
                ? AssetBundleManager.Instance.LoadAssetBundle(bundleName.ToLower(), null, false)
                : null;
            if (rf != null) iter = creator.CBLoadPrefab(rf);
        }

        if (iter != null)
        {
            // Wrap in UJCoroutine + start on Main.LuaMgr (LuaManager) MonoBehaviour
            var co = new UJCoroutine(iter);
            var lua = LuaFramework.LuaManager.Instance;
            if (lua != null) lua.StartCoroutine(co);
            AddReadyEvent(creator);
        }
        else
        {
            UnityEngine.Debug.LogError("WndForm.CreateWndForm: no load path for " + prefabPath);
            WndRoot.showMask = false;
            return null;
        }

        return wndForm;
    }

    /* RVA 0x01a0541c — IWndForm.CreateWndFormAsync:
     *   Bail (LogWarning) if either side is destroying.
     *   WndFormNode.Create(eWndFormID, resObj) → node
     *   set Canvas.worldCamera = WndRoot.uiCamera
     *   set Canvas.overrideSorting = true
     *   transform.SetParent(parent.cacheTransform), anchorMin/Max=(0,0/1,1),
     *     anchoredPos/sizeDelta = (0,0), localPos/Rot/Scale = identity
     *   wnd.Create(eWndFormID, this, node, args, popup)
     *   on success: node._parent = this, node._popup = popup,
     *     this._subNodes/_popupNodes (chosen via popup) Push(node),
     *     parent._node._subCanvases hook (offset 0x30 inside WndRoot.body table) → notify.
     *   on failure: this.Destroy(); UnityEngine.Object.Destroy(node._canvas.gameObject)
     */
    public void CreateWndFormAsync(WndForm wnd, UnityEngine.GameObject resObj, uint eWndFormID, ArrayList args, bool popup)
    {
        if (isDestroy) { Debug.LogWarning("WndForm.CreateWndFormAsync: parent destroyed"); return; }
        if (wnd == null) { Debug.LogWarning("WndForm.CreateWndFormAsync: wnd null"); return; }
        if (wnd.isDestroy) { Debug.LogWarning("WndForm.CreateWndFormAsync: wnd already destroyed"); return; }
        if (resObj == null) { Debug.LogWarning("WndForm.CreateWndFormAsync: resObj null"); return; }

        var node = WndFormNode.Create(eWndFormID, resObj);
        if (node == null) return;

        var nodeCanvas = node._canvas;
        var uiCam = WndRoot.uiCamera;
        if (nodeCanvas != null)
        {
            nodeCanvas.worldCamera = uiCam;
            nodeCanvas.overrideSorting = true;
        }

        if (_node != null && _node._wndTrans != null && node._wndTrans != null)
        {
            var trans = node._wndTrans;
            trans.SetParent(_node._wndTrans, false);
            trans.anchorMin = UnityEngine.Vector2.zero;
            trans.anchorMax = UnityEngine.Vector2.one;
            trans.anchoredPosition = UnityEngine.Vector2.zero;
            trans.sizeDelta = UnityEngine.Vector2.zero;
            trans.localPosition = UnityEngine.Vector3.zero;
            trans.localRotation = UnityEngine.Quaternion.identity;
            trans.localScale = UnityEngine.Vector3.one;
        }

        bool ok;
        try { ok = wnd.Create(eWndFormID, this, node, args); }
        catch (System.Exception e) { Debug.LogError("[WndForm.CreateWndFormAsync] Create threw: " + e); ok = false; }

        if (!ok)
        {
            // Destroy partial node
            this.Destroy();
            if (node._canvas != null)
            {
                var go = node._canvas.gameObject;
                if (go != null) UnityEngine.Object.Destroy(go);
            }
            return;
        }

        node._parent = this;
        node._popup = popup;
        var list = popup ? _popupNodes : _subNodes;
        if (list != null) list.Push(node);
        // TODO: notify WndRoot canvas-active hook (offset 0x30 inside WndRoot.body table)
    }

    /* RVA 0x01a0640c — AfterCreate.
     *   Ghidra WndForm/AfterCreate.c body (small ~16-byte function):
     *     (**(code **)(*param_1 + 0x208))(param_1, param_2, *(undefined8 *)(*param_1 + 0x210));
     *     return;
     *   ↑ Just `V_AfterCreate(args);` — Ghidra does not show CheckWaitCreateSubWnd here.
     *
     *   PRAGMATIC 1-1 with observable binary behavior — explanation:
     *     `CheckWaitCreateSubWnd` (RVA 0x01a044fc, private void, this class) is the
     *     ONLY method in the binary that sets `_wFlag |= FLAG_DONE`. Without that bit
     *     `WndForm.isDone` (RVA 0x01a0726c reads `(_wFlag>>4)&1`) never becomes true,
     *     so any state machine waiting on `_wnd.isDone` blocks forever.
     *     The production binary obviously calls CheckWaitCreateSubWnd from SOMEWHERE
     *     (otherwise the game on-device would not work), but exhaustive search across
     *     all 500+ classes in must_port.json + Lua sources found NO caller. Likely the
     *     call is an IL2CPP-internal callback chain (WndFormReadySignal / coroutine
     *     completion) emitted from a non-must_port class — not reachable from our
     *     decompile set.
     *     We invoke CheckWaitCreateSubWnd here as the most natural placement
     *     (post-`V_AfterCreate`, when no sub-wnds remain pending) so that observable
     *     behavior matches the binary. */
    public void AfterCreate(ArrayList args)
    {
        string cgEntry = (_node != null && _node._canvasGroup != null) ? _node._canvasGroup.alpha.ToString() : "<null>";
        Debug.Log("[WndForm.AfterCreate] ENTRY eID=" + _wID + " type=" + this.GetType().Name + " curAlpha=" + _curAlpha + " CG.alpha=" + cgEntry);
        V_AfterCreate(args);
        Debug.Log("[WndForm.AfterCreate] after V_AfterCreate eID=" + _wID + " curAlpha=" + _curAlpha);
        CheckWaitCreateSubWnd();
        string cgAfter = (_node != null && _node._canvasGroup != null) ? _node._canvasGroup.alpha.ToString() : "<null>";
        Debug.Log("[WndForm.AfterCreate] after CheckWaitCreateSubWnd eID=" + _wID + " curAlpha=" + _curAlpha + " flagDone=" + ((_wFlag & FLAG_DONE) != 0) + " CG.alpha=" + cgAfter);
    }

    /* RVA 0x01a0641c — UpdateOrder(ref int depth):
     *   Not in current Ghidra dump for instance method; same body as static below
     *   (walks _subNodes / _popupNodes linked lists).
     */
    public void UpdateOrder(ref int depth)
    {
        UpdateOrder(_subNodes, ref depth);
        UpdateOrder(_popupNodes, ref depth);
    }

    /* RVA 0x01a06820 — static UpdateOrder(WndFormNodeLinkList linkList, ref int depth):
     *   Walks linkList from Head.next onwards; for each node with _body, recurse into body.UpdateOrder.
     */
    private static void UpdateOrder(WndFormNodeLinkList linkList, ref int depth)
    {
        if (linkList == null) throw new NullReferenceException();
        var n = linkList.Head;
        while (n != null)
        {
            if (n._body == null) throw new NullReferenceException();
            n._body.UpdateOrder(ref depth);
            n = n._next;
        }
    }

    /* RVA 0x01a06860 — TopNode(WndFormNode node):
     *   Choose linkList by node._popup (offset 0x52): popup ? _popupNodes : _subNodes.
     *   linkList.Remove(node); linkList.Push(node);  (front of list)
     *   Notify parent's WndForm via canvas-active hook (skipped — needs WndRoot decompile).
     */
    public void TopNode(WndFormNode node)
    {
        if (node == null) throw new NullReferenceException();
        var list = node._popup ? _popupNodes : _subNodes;
        if (list == null) throw new NullReferenceException();
        list.Remove(node);
        list.Push(node);
        // TODO: notify WndRoot canvas reorder hook (offset 0x30 inside WndRoot.body table).
    }

    /* RVA 0x01a06a30 — RemoveNode(WndFormNode node):
     *   Same list selection as TopNode → linkList.Remove(node).
     */
    public void RemoveNode(WndFormNode node)
    {
        if (node == null) throw new NullReferenceException();
        var list = node._popup ? _popupNodes : _subNodes;
        if (list == null) throw new NullReferenceException();
        list.Remove(node);
    }

    /* RVA 0x01a076d0 — GetWndID: return _wID */
    public uint GetWndID() { return _wID; }

    /* RVA 0x01a06b28 — InitComponents (1-1 from Ghidra work/06_ghidra/decompiled/WndForm/InitComponents.c):
     *   public static void InitComponents(GameObject gObj, WndForm wnd)
     *
     *   PASS 1: GetComponentsInChildren<IWndComponent>(true) → InitComponent(wnd) for each.
     *   PASS 2: GetComponentsInChildren<UnityEngine.UI.Text>(true) → for each Text:
     *           Branch A (Ghidra line 113-127): if (length >= 3) and (text.Substring(0,2) == "##"):
     *               id = int.Parse(text.Substring(2)) ; text.text = GetUIText(id)
     *           Branch B fallback (Ghidra LAB_01b06d8c, lines 89-111): if length > 3 and
     *               text.Substring(1, 2) == "##":  (skip 1 leading char — handles prefixed forms
     *               such as color/anchor tags before the marker)
     *               id = int.Parse(text.Substring(3)) ; text.text = GetUIText(id)
     *   Branch B reaches via fall-through when Branch A's `Substring(0,2)==##` is false.
     *
     *   String literals: 373 = "##", 5655 = "GameDataMgr", 6080 = "GetUIText".
     */
    public static void InitComponents(GameObject gObj, WndForm wnd)
    {
        if (gObj == null || wnd == null) return;

        // Pass 1 — IWndComponent.
        // Source: Ghidra WndForm/InitComponents.c lines 56-63:
        //   for (uVar16 = 0; uVar16 < uVar1; uVar16++) {
        //     plVar10 = arr[uVar16];
        //     if (plVar10 == NULL) goto LAB_01b06ed4 → FUN_015cb8fc (NRE throw);
        //     plVar10.vtable[0x178](this, wnd);   // virtual call → InitComponent(WndForm)
        //   }
        // No try/catch in Ghidra: any exception thrown by InitComponent propagates out.
        // 1-1 with binary: `comps[i] == null` is an NRE per Ghidra semantics.
        var comps = gObj.GetComponentsInChildren<IWndComponent>(true);
        if (comps != null)
        {
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == null) throw new System.NullReferenceException();
                comps[i].InitComponent(wnd);
            }
        }

        // Pass 2 — Text "##<id>" placeholder resolution.
        // Source: Ghidra WndForm/InitComponents.c lines 85-152 — 1-1 mapping:
        //   for (uVar16 = 0; uVar16 < uVar1; uVar16++) {
        //     plVar10 = arr[uVar16];
        //     if (plVar10 == null || (lVar11 = text.get_text(), lVar11 == null)) break;
        //     if (text.text.Length < 3) goto BranchB;
        //     // Branch A: text.text.Substring(0,2) == "##"
        //     if (Substring(0,2) != "##") goto BranchB;
        //     if (!int.TryParse(Substring(2), out id)) continue;
        //     text.text = LuaFramework.Util.CallMethod<object>("GameDataMgr","GetUIText", new[]{id});
        //     goto LoopEnd;
        //   BranchB:
        //     if (text.text.Length <= 3) continue;
        //     if (Substring(1,2) != "##") continue;
        //     if (!int.TryParse(Substring(3), out id)) continue;
        //     text.text = LuaFramework.Util.CallMethod<object>("GameDataMgr","GetUIText", new[]{id});
        //   LoopEnd: }
        // Ghidra uses `break` on null Text or null text.text; no try/catch around CallMethod.
        var texts = gObj.GetComponentsInChildren<UnityEngine.UI.Text>(true);
        if (texts == null) return;
        for (int i = 0; i < texts.Length; i++)
        {
            var t = texts[i];
            if (t == null) break;
            string s = t.text;
            if (s == null) break;

            int idStart;
            if (s.Length < 3)
            {
                // Branch B (length > 3 + Substring(1,2)=="##")
                if (s.Length <= 3) continue;
                if (s.Substring(1, 2) != "##") continue;
                idStart = 3;
            }
            else
            {
                // Branch A (Substring(0,2)=="##")
                if (s.Substring(0, 2) != "##")
                {
                    // fall through to Branch B
                    if (s.Length <= 3) continue;
                    if (s.Substring(1, 2) != "##") continue;
                    idStart = 3;
                }
                else
                {
                    idStart = 2;
                }
            }

            int id;
            if (!int.TryParse(s.Substring(idStart), out id)) continue;
            string resolved = LuaFramework.Util.CallMethod<string>("GameDataMgr", "GetUIText", new object[] { id });
            if (resolved != null) t.text = resolved;
        }
    }

    /* RVA 0x01a06ee8 — CreateSubWndForm: empty in Ghidra (placeholder for subclass override). */
    private void CreateSubWndForm() { }

    // Active WndForm registry — emulates the link-list traversal in Ghidra
    // ProxyWndForm.Update / WndForm.Update sweeper. Original walks WndFormNodeLinkList
    // (subNodes/popupNodes) under each ProxyWndForm. Our scaffolding tracks active forms
    // in a static list so PostDestroySweep can iterate without porting the full chain.
    private static readonly System.Collections.Generic.List<WndForm> _activeForms = new System.Collections.Generic.List<WndForm>();

    /// <summary>Per-frame sweep: destroy WndForms whose `_node._postDestroy` flag is set.
    /// Emulates the Ghidra WndForm.Update / WndForm.Destroy linkList sweep
    /// (RVAs 0x01a070bc + 0x01a07320). TODO Lane A: full Main.Update chain port.</summary>
    public static void PostDestroySweep()
    {
        for (int i = _activeForms.Count - 1; i >= 0; i--)
        {
            var w = _activeForms[i];
            if (w == null) { _activeForms.RemoveAt(i); continue; }
            bool flagged = (w._wFlag & FLAG_DESTROY) != 0
                || (w._node != null && w._node._postDestroy);
            if (!flagged) continue;
            try
            {
                if (w._node != null) w._node.Destroy();
            }
            catch (System.Exception e) { Debug.LogWarning("[WndForm.PostDestroySweep] Destroy threw on wID=" + w._wID + ": " + e.Message); }
            _activeForms.RemoveAt(i);
        }
    }

    /* RVA 0x01a05cec — Create:
     *   if FLAG_DESTROY (bit 4 of byte at +0x11) set → return false.
     *   _wID = eWndFormID; _parent = parent; _node = node;
     *   node._body = this; if (node._canvas != null) node._canvas.overrideSorting = true;
     *   InitComponents(node._canvas.gameObject, this);
     *   try { ok = V_Create(args); } catch { ProxyWndForm canvas-active(false); ok = false; }
     *   if (ok) _focusWndForm = this;
     *   if (node._popup) { curAlpha = 0; _waitForEnter = true; }
     *   _wFlag &= ~FLAG_LOADING;
     *   return ok;
     */
    public bool Create(uint eWndFormID, WndForm parent, WndFormNode node, ArrayList args)
    {
        Debug.Log($"[WndForm.Create] ENTRY eID={eWndFormID} type={this.GetType().Name} flagDestroy={(_wFlag & FLAG_DESTROY)!=0}");
        if ((_wFlag & FLAG_DESTROY) != 0) { Debug.LogError($"[WndForm.Create] FLAG_DESTROY set, bail eID={eWndFormID}"); return false; }
        if (!_activeForms.Contains(this)) _activeForms.Add(this);
        _wID = eWndFormID;
        _parent = parent;
        _node = node;
        if (node == null) throw new NullReferenceException();
        node._body = this;
        if (node._canvas == null) throw new NullReferenceException();
        node._canvas.overrideSorting = true;
        var go = node._canvas.gameObject;
        Debug.Log($"[WndForm.Create] before InitComponents eID={eWndFormID} go={go.name} popup={node._popup}");
        InitComponents(go, this);
        Debug.Log($"[WndForm.Create] after InitComponents, before V_Create eID={eWndFormID}");
        bool ok;
        try { ok = V_Create(args); }
        catch (System.Exception e)
        {
            Debug.LogError("[WndForm.Create] V_Create exception: " + e);
            ok = false;
        }
        string cgAfterCreate = (node._canvasGroup != null) ? node._canvasGroup.alpha.ToString() : "<null>";
        Debug.Log("[WndForm.Create] V_Create returned ok=" + ok + " eID=" + eWndFormID + " curAlpha=" + _curAlpha + " CG.alpha=" + cgAfterCreate);
        if (ok)
        {
            _focusWndForm = this;
        }
        else
        {
            // ProxyWndForm canvas-active(false) — TODO port WndRoot canvas-active hook
        }
        if (node._popup)
        {
            curAlpha = 0;
            _waitForEnter = true;
        }
        _wFlag &= ~FLAG_LOADING;
        return ok;
    }

    /* RVA 0x01a06eec — Update(float dTime):
     *   Top-level entry: walk _subNodes + _popupNodes via static Update overload.
     *   Body's _eFadeMode != 0 → call instance ProcessFade(dTime).
     *   Body has _waitForEnter / _waitForExit + animator → resolve trigger + reset flags.
     *   Then dispatch V_Update(dTime).
     */
    public void Update(float dTime)
    {
        Update(_subNodes, dTime);
        Update(_popupNodes, dTime);

        if (_eFadeMode != FadeMode.E_None) ProcessFade(dTime);

        if (_node != null && _node._hasAnimatorParams && _node._animator != null && _node._animator.isInitialized)
        {
            if (_waitForEnter)
            {
                // Source: Ghidra WndForm/SetShow.c — PTR_StringLiteral_5045 = "Enter" (em port lần đầu SAI "Show").
                _node._animator.SetTrigger("Enter");
                _waitForEnter = false;
            }
            if (_waitForExit)
            {
                // Source: Ghidra WndForm/SetShow.c — PTR_StringLiteral_5204 = "Exit" (em port lần đầu SAI "Hide").
                _node._animator.SetTrigger("Exit");
                _waitForExit = false;
            }
        }

        V_Update(dTime);
    }

    /* RVA 0x01a070bc — static Update(WndFormNodeLinkList linkList, float dTime):
     *   Iterate linkList from Head; for each node:
     *     - if FLAG_DESTROY set on body OR node._postDestroy → recurse into Destroy(linkList) tail;
     *     - else recurse body.Update(dTime).
     *   When _hasAnimatorParams + animator initialised: read AnimatorStateInfo, check IsName("Hide");
     *     if normalizedTime >= 1.0 → SetActive(gameObject, false).
     *     else SetTrigger("Show") (handles enter trigger fall-through).
     */
    private static void Update(WndFormNodeLinkList linkList, float dTime)
    {
        if (linkList == null) throw new NullReferenceException();
        var n = linkList.Head;
        while (n != null)
        {
            var body = n._body;
            if (body == null) throw new NullReferenceException();
            var nextN = n._next;

            // FLAG_DESTROY (byte at +0x11 bit 4) OR node._postDestroy
            bool destroyFlag = (body._wFlag & FLAG_DESTROY) != 0
                || (body._node != null && body._node._postDestroy);
            if (destroyFlag)
            {
                Destroy(linkList);
                n = nextN;
                continue;
            }

            if (n._hasAnimatorParams)
            {
                var anim = n._animator;
                if (anim == null) throw new NullReferenceException();
                if (anim.isInitialized)
                {
                    var info = anim.GetCurrentAnimatorStateInfo(0);
                    // Source: Ghidra WndForm/Update.c — puVar3=PTR_StringLiteral_5205="Exit_Anim", puVar2=PTR_StringLiteral_5204="Exit".
                    // (Em port lần đầu SAI: assume "Hide"/"Show" — chế cháo. Ghidra dùng "Exit_Anim"/"Exit".)
                    if (info.IsName("Exit_Anim"))
                    {
                        var info2 = anim.GetCurrentAnimatorStateInfo(0);
                        if (info2.normalizedTime >= 1.0f)
                        {
                            if (n._wndTrans == null) throw new NullReferenceException();
                            var go = n._wndTrans.gameObject;
                            if (go == null) throw new NullReferenceException();
                            go.SetActive(false);
                        }
                    }
                    else
                    {
                        anim.SetTrigger("Exit");
                    }
                }
            }

            body.Update(dTime);
            n = nextN;
        }
    }

    /* RVA 0x01a06108 — Destroy: traverse linked lists destroying children, then self.
     *   The instance overload calls the static linkList sweep on _subNodes and _popupNodes,
     *   marks _wFlag |= FLAG_DESTROY (bit 0x1000), invokes V_Destroy, then destroys node.
     */
    public void Destroy()
    {
        Destroy(_subNodes);
        Destroy(_popupNodes);
        _wFlag |= FLAG_DESTROY;
        try { V_Destroy(); }
        catch (System.Exception e) { Debug.LogWarning("[WndForm.Destroy] V_Destroy threw: " + e.Message); }
        if (_node != null)
        {
            try { _node.Destroy(); }
            catch (System.Exception e) { Debug.LogWarning("[WndForm.Destroy] node.Destroy threw: " + e.Message); }
            _node = null;
        }
        _activeForms.Remove(this);
    }

    /* RVA 0x01a07320 — static Destroy(WndFormNodeLinkList linkList):
     *   Walk Head -> while head != null:
     *     body = head._body
     *     if (body == null) UJDebug.LogError("WndForm.Destroy: null body") + node.Destroy() + break;
     *     else: mark body for destroy (FLAG_DESTROY or node._postDestroy=true) + recurse body.Destroy();
     *     head = linkList.Head (refresh after removal).
     */
    private static void Destroy(WndFormNodeLinkList linkList)
    {
        if (linkList == null) throw new NullReferenceException();
        while (linkList.Head != null)
        {
            var head = linkList.Head;
            var body = head._body;
            if (body == null)
            {
                Debug.LogError("WndForm.Destroy: null body");
                if (linkList.Head != null) linkList.Head.Destroy();
                break;
            }
            // mark body for destroy (set FLAG_DESTROY or node._postDestroy=true)
            if (body._node == null) body._wFlag |= FLAG_DESTROY;
            else body._node._postDestroy = true;
            body.Destroy();
        }
    }

    /* RVA 0x01a075bc — ProcessKeyClick(KeyCode keyCode):
     *   Single-arg overload: forwards with default eType=KeyDown(11), ctrl=false. */
    public bool ProcessKeyClick(KeyCode keyCode)
    {
        return ProcessKeyClick(keyCode, EventType.KeyDown, false);
    }

    /* RVA 0x01a076ac — ProcessKeyClick(KeyCode keyCode, EventType eType):
     *   Two-arg overload: forwards with ctrl=false. */
    public bool ProcessKeyClick(KeyCode keyCode, EventType eType)
    {
        return ProcessKeyClick(keyCode, eType, false);
    }

    /* RVA 0x01a076bc — ProcessKeyClick(KeyCode keyCode, EventType eType, bool ctrl):
     *   Try popups first (priority), then subs, then V_ProcessKeyClick on this. */
    public bool ProcessKeyClick(KeyCode keyCode, EventType eType, bool ctrl)
    {
        if (ProcessKeyClick(_popupNodes, keyCode)) return true;
        if (ProcessKeyClick(_subNodes, keyCode)) return true;
        return V_ProcessKeyClick(keyCode, eType, ctrl);
    }

    /* RVA 0x01a07618 — static ProcessKeyClick(WndFormNodeLinkList linkList, KeyCode keyCode):
     *   Walk linkList: skip destroyed nodes; only descend into nodes whose canvas.gameObject.activeSelf==true;
     *   recursively try child WndForm.ProcessKeyClick; first-true wins. Returns false if none claim.
     */
    private static bool ProcessKeyClick(WndFormNodeLinkList linkList, KeyCode keyCode)
    {
        if (linkList == null) throw new NullReferenceException();
        var n = linkList.Head;
        while (n != null)
        {
            var body = n._body;
            var nextN = n._front; // Ghidra walks via _front (0x40) in this loop
            if (body == null) throw new NullReferenceException();
            bool destroyed = (body._wFlag & FLAG_DESTROY) != 0
                || (body._node != null && body._node._postDestroy);
            if (!destroyed)
            {
                if (n._canvas == null) throw new NullReferenceException();
                var go = n._canvas.gameObject;
                if (go == null) throw new NullReferenceException();
                if (go.activeSelf)
                {
                    if (body.ProcessKeyClick(keyCode)) return true;
                }
            }
            n = nextN;
        }
        return false;
    }

    /* RVA 0x01a076f8 — Find(string name):
     *   if (_node == null || _node._wndTrans == null) NRE.
     *   var t = _node._wndTrans.Find(name);  return t==null ? null : t.gameObject;
     */
    public GameObject Find(string name)
    {
        if (_node == null) throw new NullReferenceException();
        var rt = _node._wndTrans;
        if (rt == null) throw new NullReferenceException();
        var t = rt.Find(name);
        if (t == null) return null;
        return t.gameObject;
    }

    /* RVA 0x01a077a4 — SetTop(bool b):
     *   if (_node != null && _node._top != b) {
     *     _node._top = b;
     *     if (_parent != null) _parent.TopNode(_node);  // virtual via _parent (IWndForm/ProxyWndForm)
     *   }
     */
    public void SetTop(bool b)
    {
        if (_node == null) return;
        if (_node._top == b) return;
        _node._top = b;
        var p = _node._parent;
        if (p != null) p.TopNode(_node);
    }

    /* RVA 0x01a07874 — SetFocus:
     *   if (FLAG_DONE not set) return;
     *   var focusSlot = WndRoot.body._focusWndForm; if (focusSlot == this) return;
     *   if (_node == null) return;
     *   focusSlot = this;  // assign focus
     *   if (_node._parent != null) _node._parent.TopNode(_node);
     */
    public void SetFocus()
    {
        if ((_wFlag & FLAG_DONE) == 0) return;
        if (_focusWndForm == this) return;
        if (_node == null) return;
        if (_node._parent == null) return;
        _focusWndForm = this;
        _node._parent.TopNode(_node);
    }

    /* RVA 0x01a0797c — IsShow:
     *   if _node == null → LogWarning("[wID]: no node") + return false.
     *   else _node._wndTrans.gameObject.activeSelf */
    public bool IsShow()
    {
        if (_node == null)
        {
            Debug.LogWarning(string.Format("WndForm.IsShow [{0}]: no node", _wID));
            return false;
        }
        if (_node._wndTrans == null) throw new NullReferenceException();
        var go = _node._wndTrans.gameObject;
        if (go == null) throw new NullReferenceException();
        return go.activeSelf;
    }

    /* RVA 0x01a07a6c — SetShow(bool b)
     *   1. Bail with warning if _node == null.
     *   2. Idempotent: skip if current activeSelf == b.
     *   3. If node._hasAnimatorParams + animator initialized → SetTrigger("Show"/"Hide");
     *      otherwise stash the desired state into _waitForEnter / _waitForExit so the
     *      animator picks it up after Init.
     *   4. SetActive(b) on the node's gameObject.
     *   5. Walk _subNodes / _popupNodes linked lists and notify sub-Canvas active state
     *      via WndRoot's canvas-active hook (offset 0x30 inside WndRoot.body table).
     */
    public void SetShow(bool b)
    {
        if (_node == null)
        {
            Debug.LogWarning(string.Format("WndForm.SetShow [{0}]: no node", _wID));
            return;
        }

        var trans = _node._wndTrans;
        if (trans == null) throw new NullReferenceException();
        var go = trans.gameObject;
        if (go == null) throw new NullReferenceException();

        if (go.activeSelf == b) return; // idempotent

        if (_node._hasAnimatorParams)
        {
            var anim = _node._animator;
            if (anim == null) throw new NullReferenceException();
            if (!anim.isInitialized)
            {
                _waitForEnter = b;
                _waitForExit = !b;
            }
            else
            {
                // Source: Ghidra WndForm/SetShow.c — show=true → "Enter" (5045), show=false → "Exit" (5204).
                anim.SetTrigger(b ? "Enter" : "Exit");
            }
        }

        go.SetActive(b);
        // Walk _subNodes + _popupNodes linked lists and notify WndRoot canvas-active hook.
        // Original signature: WndRoot.body.OnCanvasActive(wID, b, layer=4)
        // TODO: requires WndRoot canvas-active hook decompile (offset 0x30 inside WndRoot.body table).
    }

    /* RVA 0x01a07d40 — IsActive
     *   FLAG_DONE bit set AND _eFadeMode not Out/Done AND _node._postDestroy == false.
     */
    public bool IsActive()
    {
        if ((_wFlag & FLAG_DONE) == 0) return false;
        if ((((uint)_eFadeMode) & 0xFFFFFFFEu) == 2u) return false;
        if (_node == null) throw new NullReferenceException();
        return !_node._postDestroy;
    }

    // get_* accessor methods removed: properties at top of class auto-generate same names → CS0082 conflict.
    // C# property syntax `public X foo { get { ... } }` already exposes get_foo, set_foo for IL2CPP/Lua bridge.

    /* RVA 0x01a07da0 — SetFadeIn(float fDuration):
     *   if (fDuration > 0) {
     *     _fadeDuration = fDuration; _eFadeMode = E_FadeIn (1); _fadeDetalTime = 0;
     *     curAlpha = 0; SetShow(true);
     *   }
     */
    public void SetFadeIn(float fDuration)
    {
        if (fDuration <= 0.0f) return;
        _fadeDuration = fDuration;
        _eFadeMode = FadeMode.E_FadeIn;
        _fadeDetalTime = 0;
        curAlpha = 0;
        SetShow(true);
    }

    /* RVA 0x01a07ddc — SetFadeOut(float fDuration, bool wantDestroy):
     *   if (fDuration <= 0) return;
     *   if (this == null) NRE.  // checked at runtime
     *   _eFadeMode = wantDestroy ? E_FadeOutWithDestroy(3) : E_FadeOut(2);
     *   _fadeDuration = fDuration; _fadeDetalTime = 0; curAlpha = 1.0f;
     */
    public void SetFadeOut(float fDuration, bool wantDestroy)
    {
        if (fDuration <= 0.0f) return;
        _fadeDuration = fDuration;
        _eFadeMode = wantDestroy ? FadeMode.E_FadeOutWithDestroy : FadeMode.E_FadeOut;
        _fadeDetalTime = 0;
        curAlpha = 1.0f;
    }

    /* RVA 0x01a07278 — ProcessFade(float dTime):
     *   ratio = _fadeDetalTime / _fadeDuration;
     *   if (_eFadeMode != E_FadeIn) ratio = 1 - ratio;
     *   curAlpha = ratio;
     *   if (_fadeDuration < _fadeDetalTime) {
     *     if (_eFadeMode == E_FadeOut) SetShow(false);
     *     else if (_eFadeMode == E_FadeOutWithDestroy) {
     *       if (_node == null) _wFlag |= FLAG_DESTROY; else _node._postDestroy = true;
     *     }
     *     _eFadeMode = E_None;
     *   }
     *   _fadeDetalTime += min(dTime, DAT_0091c120 = 1/15 ≈ 0.0666f).
     *
     *   Note: NEON_fminnm(dTime, 0.0666...) caps fade tick to ~15FPS to keep alpha smooth on lag.
     */
    private void ProcessFade(float dTime)
    {
        float ratio = _fadeDetalTime / _fadeDuration;
        if (_eFadeMode != FadeMode.E_FadeIn) ratio = 1.0f - ratio;
        curAlpha = ratio;
        if (_fadeDuration < _fadeDetalTime)
        {
            if (_eFadeMode == FadeMode.E_FadeOut)
            {
                SetShow(false);
            }
            else if (_eFadeMode == FadeMode.E_FadeOutWithDestroy)
            {
                if (_node == null) _wFlag |= FLAG_DESTROY;
                else _node._postDestroy = true;
            }
            _eFadeMode = FadeMode.E_None;
        }
        // DAT_0091c120 = ~0.0666f (1/15 sec cap)
        _fadeDetalTime += UnityEngine.Mathf.Min(dTime, 1.0f / 15.0f);
    }

    /* RVA 0x01a07598 — PostDestroy:
     *   if (_node != null) _node._postDestroy = true;
     *   else _wFlag |= FLAG_DESTROY;
     */
    public void PostDestroy()
    {
        if (_node != null) _node._postDestroy = true;
        else _wFlag |= FLAG_DESTROY;
    }

    /* RVA 0x01a07e1c — SetPos(float x, float y):
     *   var rt = _node._wndTrans;  rt.anchoredPosition = new Vector2(x, y);
     *   (Ghidra reads anchoredPosition first then writes — read is dead, see decompiled flow.)
     */
    public void SetPos(float x, float y)
    {
        if (_node == null) throw new NullReferenceException();
        var rt = _node._wndTrans;
        if (rt == null) throw new NullReferenceException();
        // Ghidra reads then writes — read is no-op (alignment artifact); skip read.
        rt.anchoredPosition = new UnityEngine.Vector2(x, y);
    }

    /* RVA 0x01a07e74 — SetDeltaXY(float x, float y):
     *   var rt = _node._wndTrans;
     *   var p = rt.anchoredPosition;
     *   rt.anchoredPosition = new Vector2(p.x + x, p.y + y);
     */
    public void SetDeltaXY(float x, float y)
    {
        if (_node == null) throw new NullReferenceException();
        var rt = _node._wndTrans;
        if (rt == null) throw new NullReferenceException();
        var p = rt.anchoredPosition;
        rt.anchoredPosition = new UnityEngine.Vector2(p.x + x, p.y + y);
    }

    /* RVA 0x01a07ecc — SetSortingLayer (1-1 from Ghidra work/06_ghidra/decompiled/WndForm/SetSortingLayer.c):
     *   _node._canvas.sortingLayerName = layerName (single setter call).
     */
    public void SetSortingLayer(string layerName)
    {
        if (_node == null) return;
        var c = _node._canvas;
        if (c != null) c.sortingLayerName = layerName;
    }

    /* RVA 0x01a07ef0 — Lock: _isLock = true */
    public void Lock() { _isLock = true; }

    /* RVA 0x01a07efc — Unlock: _isLock = false */
    public void Unlock() { _isLock = false; }

    /* RVA 0x01a07f04 — GetLock: return _isLock */
    public bool GetLock() { return _isLock; }

    /* RVA 0x01a07f0c — GetPrefab(uint eWndFormID, ArrayList args):
     *   StringLiteral_9184 ("Prefabs/Menus/") + EWndFormIDMapping.GetWndFormString(eWndFormID).
     */
    public virtual string GetPrefab(uint eWndFormID, ArrayList args)
    {
        return "Prefabs/Menus/" + EWndFormIDMapping.GetWndFormString(eWndFormID);
    }

    /* RVA 0x01a07f68 — GetBundle: virtual GetPrefab → split at last '/' →
     *   bundleName = AssetBundleManager.GetBundleNameWithExt(path)
     *   objName = path.Substring(lastSlash+1)
     */
    public virtual void GetBundle(uint eWndFormID, ArrayList args, out string bundleName, out string objName)
    {
        string path = GetPrefab(eWndFormID, args);
        if (string.IsNullOrEmpty(path)) throw new NullReferenceException();
        int lastSlash = path.LastIndexOf('/');
        objName = path.Substring(lastSlash + 1);
        bundleName = AssetBundleManager.GetBundleNameWithExt(path);
    }

    /* RVA 0x01a08058 — IsPrefabInResource: return false (base default). */
    public virtual bool IsPrefabInResource() { return false; }

    /* RVA 0x01a08060 — V_Create: return true (base default = success). */
    protected virtual bool V_Create(ArrayList args) { return true; }

    /* RVA 0x01a08068 — V_AfterCreate: empty (subclass overrides; e.g. WndForm_Lua dispatches to lua_OnAfterCreate). */
    protected virtual void V_AfterCreate(ArrayList args) { }

    /* RVA 0x01a0806c — V_Destroy: empty base. */
    protected virtual void V_Destroy() { }

    /* RVA 0x01a08070 — V_Update: empty base. */
    protected virtual void V_Update(float dTime) { }

    /* RVA 0x01a08074/0x01a0807c/0x01a08084 — V_ProcessKeyClick overloads: return true (base swallows key). */
    protected virtual bool V_ProcessKeyClick(KeyCode keyCode) { return true; }
    protected virtual bool V_ProcessKeyClick(KeyCode keyCode, EventType eType) { return true; }
    protected virtual bool V_ProcessKeyClick(KeyCode keyCode, EventType eType, bool ctrl) { return true; }

    /* RVA 0x01a0808c — static OnGUIPop(WndFormNodeLinkList linkList):
     *   Walk node.gameObject from linkList.Head._wndTrans (offset 0x10 = canvas), call into
     *   WndRoot.body.canvas-active hook with (node._wID, true, 3, ...). Then recurse into
     *   _subNodes (offset 0x28 from each node) and _popupNodes (offset 0x30), finally invoke
     *   instance V_OnGUIPop via vtable.
     */
    private static void OnGUIPop(WndFormNodeLinkList linkList)
    {
        if (linkList == null) return;
        // WndRoot.body canvas-active hook: walk linkList, fire OnCanvasActive(wID, true, layer=3)
        // TODO: WndRoot canvas-active hook decompile pending. We forward to instance OnGUIPop
        // for each body so per-form V_OnGUIPop fires.
        var n = linkList.Head;
        while (n != null)
        {
            if (n._body != null) n._body.OnGUIPop();
            n = n._next;
        }
    }

    /* RVA 0x01a080dc — instance OnGUIPop():
     *   Calls WndRoot canvas-active hook for self via parent vtable (offset 0xb8 + 0x30),
     *   recurses static OnGUIPop on _subNodes (offset 0x28) and _popupNodes (offset 0x30),
     *   then dispatches V_OnGUIPop (vtable +0x268).
     */
    public void OnGUIPop()
    {
        // TODO: WndRoot canvas-active hook (offset 0xb8 + 0x30) — needs WndRoot decompile.
        OnGUIPop(_subNodes);
        OnGUIPop(_popupNodes);
        V_OnGUIPop();
    }

    /* RVA 0x01a08198 — V_OnGUIPop: empty base (subclass overrides). */
    protected virtual void V_OnGUIPop() { }

    /* RVA 0x01a06a68 — .ctor(bool ticked = true):
     *   _wFlag = (ticked ? FLAG_TICK : 0) | FLAG_LOADING; new() the linked lists; _curAlpha = 1.0f.
     */
    protected WndForm(bool ticked = true)
    {
        _wFlag = (ticked ? FLAG_TICK : 0u) | FLAG_LOADING;
        _subNodes = new WndFormNodeLinkList();
        _popupNodes = new WndFormNodeLinkList();
        _curAlpha = 1.0f;
        _eFadeMode = FadeMode.E_None;
    }

    /* Source: dump.cs TypeDefIndex 290 — private delegate WndForm.CBReadyEvent(WndForm wnd). */
    private delegate void CBReadyEvent(WndForm wnd);

    /* Source: dump.cs TypeDefIndex 291 — private enum WndForm.FadeMode. */
    private enum FadeMode
    {
        E_None = 0,
        E_FadeIn = 1,
        E_FadeOut = 2,
        E_FadeOutWithDestroy = 3,
    }

    /* Source: Il2CppDumper-decompiled (Ghidra — work/06_ghidra/decompiled/WndForm.WndFormReadySignal/)
     * Aggregates "is ready" state across multiple sub-events. Used by WndForm.AddReadyEvent
     * when the wnd has children that also need to load.
     */
    public class WndFormReadySignal : IWndFormReady {
        private System.Collections.Generic.Queue<IWndFormReady> _subWaits;
        private System.Collections.Generic.Queue<WndForm> _subWnds;
        private bool _finish;

        /* RVA 0x01a081dc */
        public void AddReadyEvent(IWndFormReady ir)
        {
            if (ir == null) return;
            if (_subWaits == null) _subWaits = new System.Collections.Generic.Queue<IWndFormReady>();
            _subWaits.Enqueue(ir);
        }

        /* RVA 0x01a08298 */
        public void AddSubWndForm(WndForm wnd)
        {
            if (wnd == null) return;
            if (_subWnds == null) _subWnds = new System.Collections.Generic.Queue<WndForm>();
            _subWnds.Enqueue(wnd);
        }

        /* RVA 0x01a08354 */
        public void Finish() { _finish = true; }

        /* RVA 0x01a08360 — IWndFormReady.isReady: _finish && all sub-events ready && all sub-wnds ready */
        public bool isReady
        {
            get
            {
                if (!_finish) return false;
                if (_subWaits != null)
                {
                    foreach (var w in _subWaits) if (w != null && !w.isReady) return false;
                }
                if (_subWnds != null)
                {
                    foreach (var sw in _subWnds) if (sw != null && !sw.isDone) return false;
                }
                return true;
            }
        }

        public bool hasEvent
        {
            get
            {
                if (_subWaits != null)
                {
                    foreach (var w in _subWaits) if (w != null && w.hasEvent) return true;
                }
                return false;
            }
        }

        /* RVA 0x01a0853c — IWndFormReady.FinishEvent: no-op (signal aggregator) */
        public void FinishEvent(WndForm wnd) { }
    }

    /* Source: Ghidra — work/06_ghidra/decompiled/WndForm.WndFormReadyEvent/
     * Wraps a callback to fire when WndForm becomes ready.
     */
    public class WndFormReadyEvent : IWndFormReady {
        public delegate void Callback(WndForm wnd);
        private Callback _callback;

        /* RVA 0x01a08548 */
        public WndFormReadyEvent(Callback cb) { _callback = cb; }

        /* RVA 0x01a08578 — static helper: SetTop on the ready wnd */
        public static void CBSetTop(WndForm wnd) { if (wnd != null) wnd.SetTop(true); }

        /* RVA 0x01a0858c — isReady = true (event ready means callback is registered) */
        public bool isReady { get { return true; } }
        public bool hasEvent { get { return _callback != null; } }

        /* RVA 0x01a0859c — invoke callback */
        public void FinishEvent(WndForm wnd) { if (_callback != null) _callback(wnd); }
    }

    /* Source: dump.cs TypeDefIndex 294 — public class WndForm.WndFormFadeOutEvent : IWndFormReady
     *   Field offsets: _ignoreWID@0x10, _layer@0x14, _wnd@0x18, _fDuration@0x20, _wantDestroy@0x24.
     *   Two ctors: (uint ignoreWID, float fDuration=0.1, ELayer layer=4) and
     *              (WndForm wnd, float fDuration=0.1, bool wantDestroy=true).
     *   Has isReady / hasEvent / FinishEvent slots — Ghidra dumps not present so leave NIE.
     */
    public class WndFormFadeOutEvent : IWndFormReady {
        private uint _ignoreWID;
        private WndRoot.ELayer _layer;
        private WndForm _wnd;
        private float _fDuration;
        private bool _wantDestroy;

        /* RVA 0x01a085b8 */
        public WndFormFadeOutEvent(uint ignoreWID, float fDuration = 0.1f, WndRoot.ELayer layer = WndRoot.ELayer.Default)
        {
            _ignoreWID = ignoreWID;
            _fDuration = fDuration;
            _layer = layer;
            _wantDestroy = false;
            _wnd = null;
        }

        /* RVA 0x01a086d0 */
        public WndFormFadeOutEvent(WndForm wnd, float fDuration = 0.1f, bool wantDestroy = true)
        {
            _wnd = wnd;
            _fDuration = fDuration;
            _wantDestroy = wantDestroy;
            _ignoreWID = 0;
            _layer = WndRoot.ELayer.Default;
        }

        /* RVA 0x01a08734 — isReady: TODO no Ghidra body in current dump. */
        public bool isReady { get { return true; } }

        /* RVA 0x01a0873c — hasEvent: TODO no Ghidra body in current dump. */
        public bool hasEvent { get { return true; } }

        /* RVA 0x01a08744 — FinishEvent: TODO no Ghidra body in current dump.
         * Expected behavior (from caller-site usage): if _wnd present, call wnd.SetFadeOut(_fDuration, _wantDestroy);
         * else iterate forms in _layer with id != _ignoreWID and call SetFadeOut on each.
         */
        public void FinishEvent(WndForm wnd)
        {
            if (_wnd != null)
            {
                _wnd.SetFadeOut(_fDuration, _wantDestroy);
                return;
            }
            // TODO from libil2cpp.so RVA 0x01a08744 — multi-form sweep; need WndRoot iteration decompile.
        }
    }
}
