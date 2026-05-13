// Source: Il2CppDumper-stub (auto-generated from dump.cs)
// Original: AsyncWndFormCreator
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

/* Source: Il2CppDumper-decompiled (Ghidra — work/06_ghidra/decompiled/AsyncWndFormCreator/)
 * Implements IWndFormReady to signal when async prefab load + form creation is done.
 * Owned by WndForm.CreateWndForm; orchestrates 3 load paths: bundle, cached, simulator (Resources).
 */
public class AsyncWndFormCreator : IWndFormReady {
    private static int s_max_size = 32;
    private static bool s_enabled = true;
    private static LinkedList<CacheInfo> s_caches;

    private string _resName;
    private IWndForm _parent;
    private WndForm _wnd;
    private uint _eWndFormID;
    private ArrayList _args;
    private string _name;
    private bool _popup;
    private bool _done;

    /* RVA 0x017c612c — ctor */
    public AsyncWndFormCreator(string resName, IWndForm parent, WndForm wnd, uint eWndFormID, ArrayList args, string name, bool popup)
    {
        _resName = resName;
        _parent = parent;
        _wnd = wnd;
        _eWndFormID = eWndFormID;
        _args = args;
        _name = name;
        _popup = popup;
    }

    /* IWndFormReady — isReady when the parent has finished CreateWndFormAsync */
    public bool isReady { get { return _done; } }
    public bool hasEvent { get { return false; } }
    public void FinishEvent(WndForm wnd) { }

    public static int CacheSize { get { return s_max_size; } set { s_max_size = value; } }
    public static bool CacheEnabled { get { return s_enabled; } set { s_enabled = value; } }

    /* RVA 0x017c5de0 — FindCache: search s_caches for entry with matching name */
    public static GameObject FindCache(string name)
    {
        if (s_caches == null || string.IsNullOrEmpty(name)) return null;
        for (var node = s_caches.First; node != null; node = node.Next)
        {
            if (node.Value.name == name) return node.Value.asset;
        }
        return null;
    }

    /* RVA 0x017c5f10 — AddCache: bounded LRU. Add to head, drop tail if over s_max_size */
    private static void AddCache(string name, GameObject asset)
    {
        if (!s_enabled || string.IsNullOrEmpty(name) || asset == null) return;
        if (s_caches == null) s_caches = new LinkedList<CacheInfo>();
        // Move existing to head if present
        for (var node = s_caches.First; node != null; node = node.Next)
        {
            if (node.Value.name == name) { s_caches.Remove(node); break; }
        }
        s_caches.AddFirst(new CacheInfo { name = name, asset = asset });
        while (s_caches.Count > s_max_size) s_caches.RemoveLast();
    }

    /* RVA 0x017c61d8 — CBLoadPrefab: returns iterator d__20 */
    public IEnumerator CBLoadPrefab(RequestFile fr) { return new CBLoadPrefab_d__20(this, fr); }

    /* RVA 0x017c6294 — CBLoadPrefabSimulator: returns iterator d__21 (Resources.LoadAsync path) */
    public IEnumerator CBLoadPrefabSimulator(ResourceRequest rr) { return new CBLoadPrefabSimulator_d__21(this, rr); }

    /* RVA 0x017c6350 — CBLoadPrefabCached: returns iterator d__22 (already-cached prefab) */
    public IEnumerator CBLoadPrefabCached(GameObject asset) { return new CBLoadPrefabCached_d__22(this, asset); }

    private struct CacheInfo
    {
        public string name;
        public GameObject asset;
    }

    /* Iterator state machines — ported from Ghidra <CBLoadPrefab*>d__N classes */

    /* d__20: bundle path */
    private class CBLoadPrefab_d__20 : IEnumerator
    {
        private int _state;
        private object _current;
        private AsyncWndFormCreator _this;
        private RequestFile _fr;
        private AssetBundleRequest _abr;

        public CBLoadPrefab_d__20(AsyncWndFormCreator self, RequestFile fr) { _this = self; _fr = fr; _state = 0; }
        public object Current { get { return _current; } }
        public void Reset() { throw new NotSupportedException(); }

        public bool MoveNext()
        {
            switch (_state)
            {
                case 0:
                    _state = -1;
                    // Poll for RequestFile to finish (FetchFile coroutine sets _bFinished)
                    if (_fr == null || _fr.isDone) goto loadAsset;
                    _current = null;  // poll-yield (next frame)
                    _state = 0;
                    return true;
                loadAsset:
                    if (_fr == null || _fr.bundleOP == null)
                    {
                        WndRoot.showMask = false;
                        _this._done = true;
                        return false;
                    }
                    _abr = _fr.bundleOP.LoadAsync<GameObject>(_this._name);
                    if (_abr == null)
                    {
                        // Editor fallback: try sync Load<T>
                        var prefabSync = _fr.bundleOP.Load<GameObject>(_this._name) as GameObject;
                        if (prefabSync != null)
                        {
                            AddCache(_this._resName, prefabSync);
                            if (_this._parent != null)
                                _this._parent.CreateWndFormAsync(_this._wnd, prefabSync, _this._eWndFormID, _this._args, _this._popup);
                        }
                        WndRoot.showMask = false;
                        _this._done = true;
                        return false;
                    }
                    _current = _abr;
                    _state = 2;
                    return true;
                case 2:
                    _state = -1;
                    if (_abr != null && _abr.asset != null)
                    {
                        var prefab = _abr.asset as GameObject;
                        AddCache(_this._resName, prefab);
                        if (_this._parent != null)
                            _this._parent.CreateWndFormAsync(_this._wnd, prefab, _this._eWndFormID, _this._args, _this._popup);
                    }
                    _abr = null;
                    WndRoot.showMask = false;
                    _this._done = true;
                    return false;
                default:
                    return false;
            }
        }
    }

    /* d__21: Resources.LoadAsync path */
    private class CBLoadPrefabSimulator_d__21 : IEnumerator
    {
        private int _state;
        private object _current;
        private AsyncWndFormCreator _this;
        private ResourceRequest _rr;

        public CBLoadPrefabSimulator_d__21(AsyncWndFormCreator self, ResourceRequest rr) { _this = self; _rr = rr; }
        public object Current { get { return _current; } }
        public void Reset() { throw new NotSupportedException(); }

        public bool MoveNext()
        {
            switch (_state)
            {
                case 0:
                    _state = -1;
                    if (_rr == null) goto fin;
                    if (!_rr.isDone) { _current = _rr; _state = 0; return true; }
                    goto fin;
                fin:
                    if (_rr != null && _rr.asset != null && _this._parent != null)
                    {
                        var prefab = _rr.asset as GameObject;
                        _this._parent.CreateWndFormAsync(_this._wnd, prefab, _this._eWndFormID, _this._args, _this._popup);
                    }
                    WndRoot.showMask = false;
                    _this._done = true;
                    return false;
                default:
                    return false;
            }
        }
    }

    /* d__22: cached prefab (sync but yields once for next-frame) */
    private class CBLoadPrefabCached_d__22 : IEnumerator
    {
        private int _state;
        private object _current;
        private AsyncWndFormCreator _this;
        private GameObject _asset;

        public CBLoadPrefabCached_d__22(AsyncWndFormCreator self, GameObject asset) { _this = self; _asset = asset; }
        public object Current { get { return _current; } }
        public void Reset() { throw new NotSupportedException(); }

        public bool MoveNext()
        {
            switch (_state)
            {
                case 0:
                    _state = 1;
                    _current = null;
                    return true;
                case 1:
                    _state = -1;
                    if (_this._parent != null && _asset != null)
                        _this._parent.CreateWndFormAsync(_this._wnd, _asset, _this._eWndFormID, _this._args, _this._popup);
                    WndRoot.showMask = false;
                    _this._done = true;
                    return false;
                default:
                    return false;
            }
        }
    }
}
