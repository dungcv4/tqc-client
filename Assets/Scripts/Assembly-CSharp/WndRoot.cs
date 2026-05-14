// Source: Il2CppDumper-stub (auto-generated from dump.cs)
// Original: WndRoot
// TODO: decompile bodies via Ghidra (work/06_ghidra/decompiled/)

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

public class WndRoot : MonoBehaviour
{
    private const float CMinFarClipPlane = 200;
    private static GameObject s_root;
    private static RectTransform s_rootRectTrans;
    private static Camera s_camera;
    private static Vector2 referenceResolution;
    private static GameObject s_Click_Eff3;
    private static ProxyWndForm[] s_proxyWndforms;
    public static CBWndFormStatus cbWndFormStatus;
    public Canvas _mask;
    private static GameObject s_maskObj;
    private static bool s_active_lock;
    private static int s_nLock;
    private static CanvasScaler s_rootCanvasScaler;
    /* RVA 0x01a0a444 — get_root: 1-1 with Ghidra get_root.c (returns s_root, no lazy init).
     * The previous lazy-init via EnsureRoot() was chế cháo — it synthesised "WndRootCanvas"
     * when accessed before WndRoot.Start populated s_root from the scene's GUI_Root, which then
     * broke WndRoot.Start (s_root != null → skip scene Find → Find("UICamera") on synthetic
     * canvas → null → "Could not find UICamera" failure). */
    public static GameObject root { get { return s_root; } }
    public static GameObject click_Eff3 { get { return s_Click_Eff3; } }
    public static Vector2 ReferenceResolution { get { return referenceResolution; } }
    public static Camera uiCamera { get { return s_camera; } }
    public static RectTransform rectTrans { get { return s_rootRectTrans; } }

    /* RVA 0x01a0a7a4 — get_proxyWndform = s_proxyWndforms[Default=4].
     * 1-1 with Ghidra (returns array entry or NRE on null) — previous EnsureProxies() lazy
     * init was chế cháo (auto-built synthetic proxies; same root-cause as EnsureRoot above). */
    public static ProxyWndForm proxyWndform { get { return s_proxyWndforms != null && s_proxyWndforms.Length > 4 ? s_proxyWndforms[4] : null; } }
    public static ProxyWndForm teachProxyWndform { get { return s_proxyWndforms != null && s_proxyWndforms.Length > 2 ? s_proxyWndforms[2] : null; } }
    public static ProxyWndForm systemProxyWndform { get { return s_proxyWndforms != null && s_proxyWndforms.Length > 1 ? s_proxyWndforms[1] : null; } }
    public static ProxyWndForm debugProxyWndform { get { return s_proxyWndforms != null && s_proxyWndforms.Length > 0 ? s_proxyWndforms[0] : null; } }
    public static ProxyWndForm controlProxyWndform { get { return s_proxyWndforms != null && s_proxyWndforms.Length > 3 ? s_proxyWndforms[3] : null; } }
    public static bool ActiveLock { get { return s_active_lock; } set { s_active_lock = value; } }

    /* RVA 0x01a052b8 — set_showMask: toggles s_maskObj.SetActive(value) (skipped during ActiveLock) */
    private static bool s_showMaskValue;
    public static bool showMask
    {
        get { return s_showMaskValue; }
        set
        {
            s_showMaskValue = value;
            if (s_maskObj != null) s_maskObj.SetActive(value);
        }
    }
    public static Vector2 resolution { get { return new Vector2(Screen.width, Screen.height); } }

    /* RVA 0x01a08654 — GetProxyWndForm(layer) returns s_proxyWndforms[(int)layer] (1-1 Ghidra, no lazy init). */
    public static ProxyWndForm GetProxyWndForm(ELayer layer)
    {
        int idx = (int)layer;
        if (s_proxyWndforms != null && idx >= 0 && idx < s_proxyWndforms.Length) return s_proxyWndforms[idx];
        return null;
    }

    private static void EnsureRoot()
    {
        if (s_root != null) return;
        s_root = new GameObject("WndRootCanvas");
        UnityEngine.Object.DontDestroyOnLoad(s_root);
        var canvas = s_root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = s_root.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        // Original APK design reference = 1280x720 (verified by spMaskLoading 1280x720
        // size in WndForm_LoadingScreen prefab — the design's "1:1 viewport" mask).
        scaler.referenceResolution = new Vector2(1280, 720);
        // match=0.5 = balanced: UI scales by avg of width+height ratios → fills both dims on any aspect.
        scaler.matchWidthOrHeight = 0.5f;
        s_rootCanvasScaler = scaler;
        s_root.AddComponent<GraphicRaycaster>();
        s_rootRectTrans = s_root.GetComponent<RectTransform>();
        if (s_rootRectTrans == null) s_rootRectTrans = s_root.AddComponent<RectTransform>();
        // Ensure EventSystem
        if (UnityEngine.Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            var es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            es.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            UnityEngine.Object.DontDestroyOnLoad(es);
        }
    }

    private static void EnsureProxies()
    {
        if (s_proxyWndforms != null && s_proxyWndforms.Length >= 5) return;
        EnsureRoot();
        s_proxyWndforms = new ProxyWndForm[5];
        string[] names = { "Debug", "System", "Teach", "Control", "Default" };
        for (int i = 0; i < 5; i++)
        {
            var go = new GameObject("ProxyWnd_" + names[i]);
            go.transform.SetParent(s_root.transform, false);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;
            var p = new ProxyWndForm();
            p._name = names[i];
            p._nStartDepth = i * 100;
            p._root = go;
            p._cacheTrans = rt;
            s_proxyWndforms[i] = p;
        }
    }
    public static void Update(float dTime) { }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndRoot/Awake.c RVA 0x01a0acc8
    // 1-1:
    //   var go = this.gameObject;
    //   UnityEngine.Object.DontDestroyOnLoad(go);
    //   WndRoot.InitGUI(go, false);
    //   _ = Object.op_Inequality(this._someField@+0x20, null);  // dead expression (return val unused)
    private void Awake()
    {
        GameObject go = gameObject;
        UnityEngine.Object.DontDestroyOnLoad(go);
        InitGUI(go, false);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndRoot/InitGUI.c RVA 0x01a0ad88
    // 1-1 (StringLits resolved via stringliteral.json):
    //   11873 = "UICamera"
    //   11921 = "UI_eff_point_3"
    //   12613 = "WndFormSystem: Not found UICamera for UIRoot"
    // Body:
    //   s_root = gObj
    //   if (s_root != null) {
    //     s_rootRectTrans = s_root.GetComponent<RectTransform>()                   // offset 8
    //     if (s_camera == null) {                                                  // offset 0x10
    //       var t = s_root.transform.Find("UICamera")
    //       if (t != null) s_camera = t.gameObject.GetComponent<Camera>()
    //     }
    //     if (s_camera == null) { UJDebug.LogError("...Not found UICamera..."); return; }
    //     var ct = s_root.transform.Find("UI_eff_point_3")
    //     if (ct != null) {
    //       s_Click_Eff3 = ct.gameObject                                           // offset 0x20
    //       if (s_camera.nearClipPlane > -1f) s_camera.nearClipPlane = -1f         // 0xbf800000
    //       if (s_camera.farClipPlane  < 200f) s_camera.farClipPlane = 200f       // 0x43480000
    //       SetupScreenSize()
    //     }
    //   }
    private static void InitGUI(GameObject gObj, bool isEditor = false)
    {
        s_root = gObj;
        if (s_root == null) return;
        s_rootRectTrans = s_root.GetComponent<RectTransform>();
        if (s_camera == null)
        {
            var t = s_root.transform.Find("UICamera");
            if (t != null) s_camera = t.gameObject.GetComponent<Camera>();
        }
        if (s_camera == null)
        {
            UJDebug.LogError("WndFormSystem: Not found UICamera for UIRoot");
            return;
        }
        var ct = s_root.transform.Find("UI_eff_point_3");
        if (ct != null)
        {
            s_Click_Eff3 = ct.gameObject;
            if (s_camera.nearClipPlane > -1f) s_camera.nearClipPlane = -1f;
            if (s_camera.farClipPlane < 200f) s_camera.farClipPlane = 200f;
            SetupScreenSize();
        }
    }
    // Source: Ghidra work/06_ghidra/decompiled_full/WndRoot/Start.c RVA 0x01a0b304
    // 1-1:
    //   if (s_root == null) {
    //     s_root = GameObject.Find("GUI_Root");                       // StringLit_5644
    //     if (s_root != null) {
    //       s_rootRectTrans = s_root.GetComponent<RectTransform>();   // stored at static +0x08
    //       DontDestroyOnLoad(s_root);
    //     }
    //   }
    //   if (s_root == null) { LogError("Could not find Root"); return false; }     // StringLit_8470
    //   if (s_camera == null) {
    //     var t = s_root.transform.Find("UICamera");                  // StringLit_11873
    //     if (t != null) s_camera = t.gameObject.GetComponent<Camera>();  // stored at static +0x10
    //   }
    //   if (s_camera == null) { LogError("Could not find UICamera"); return false; } // StringLit_8469
    //   if (s_camera.nearClipPlane > -1f) s_camera.nearClipPlane = -1f;             // 0xbf800000
    //   if (s_camera.farClipPlane  < 200f) s_camera.farClipPlane  = 200f;            // 0x43480000
    //   return true;
    public static bool Start()
    {
        if (s_root == null)
        {
            s_root = GameObject.Find("GUI_Root");
            if (s_root != null)
            {
                s_rootRectTrans = s_root.GetComponent<RectTransform>();
                UnityEngine.Object.DontDestroyOnLoad(s_root);
            }
        }
        if (s_root == null)
        {
            UnityEngine.Debug.LogError("[WndRoot.Start] Could not find Root GameObject (expected 'GUI_Root')");
            return false;
        }
        if (s_camera == null)
        {
            var t = s_root.transform.Find("UICamera");
            if (t != null) s_camera = t.gameObject.GetComponent<Camera>();
        }
        if (s_camera == null)
        {
            UnityEngine.Debug.LogError("[WndRoot.Start] Could not find UICamera under GUI_Root");
            return false;
        }
        if (s_camera.nearClipPlane > -1f) s_camera.nearClipPlane = -1f;
        if (s_camera.farClipPlane < 200f) s_camera.farClipPlane = 200f;

        // EDITOR FIX: GameEntry.unity's GUI_Root RectTransform has LocalScale=(0,0,0) in the
        // serialized scene (AssetRipper export artifact). For a ScreenSpaceCamera Canvas Unity
        // would normally auto-fix the scale at runtime via CanvasScaler, but the (0,0,0)
        // serialized value survives in some Editor situations → every UI child renders at
        // scale 0 → invisible (canvasRenderer.cull=true, world positions degenerate).
        // Force scale to (1,1,1) if zero so CanvasScaler can take over.
        if (s_rootRectTrans != null)
        {
            Vector3 sc = s_rootRectTrans.localScale;
            if (sc == Vector3.zero)
            {
                UnityEngine.Debug.LogWarning("[WndRoot.Start] GUI_Root LocalScale was (0,0,0) — forcing to (1,1,1)");
                s_rootRectTrans.localScale = Vector3.one;
            }
        }
        return true;
    }
    public static void Lunch() { }
    public static void Destroy() { }
    public static CanvasScaler GetCanvasScaler() { return default(CanvasScaler); }
    private static void SetupScreenSize() { }

    public enum ELayer
    {
        Debug = 0,
        System = 1,
        Teach = 2,
        Control = 3,
        Default = 4,
    }

    public delegate void CBWndFormStatus(uint wid, bool visual, WndFormDisplayStatus status);
}
