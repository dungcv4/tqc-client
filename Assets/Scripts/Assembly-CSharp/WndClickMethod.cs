// Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/ (7 .c + default .ctor)
// Source: dump.cs TypeDefIndex 202
// RVAs: 0x19570F4 (InitComponent), 0x1957BB8 (DinitComponent), 0x1956ABC (OnPointerClick),
//       0x1957BF0 (OnPointerDown), 0x1957D40 (OnPointerUp), 0x1957DE8 (OnEnable),
//       0x1957E90 (SetBtnAction), 0x1957E98 (.ctor)

using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Click Method")]
public class WndClickMethod : IWndComponent, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public string _methodName;          // 0x20
    public Component _comp;             // 0x28
    private WndForm _wnd;               // 0x30
    private MethodInfo _method;         // 0x38
    private object[] _methodParams;     // 0x40
    public TweenScale _TweenScale;      // 0x48
    private Vector3 _DefaultLocalScale; // 0x50
    public bool UseTween;               // 0x5C
    public Action_Type btnAction;       // 0x60
    public int _ClickValue;             // 0x64
    public string _ClickString;         // 0x68
    private bool isInitTweenScale;      // 0x70
    private WndAudioClip wndClip;       // 0x78

    // Source: Ghidra (implicit — no .c generated; default ctor)
    // RVA: 0x1957E98
    public WndClickMethod() { }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/InitComponent.c
    // RVA: 0x19570F4
    // Body 1-1 (resolved via OnPointerClick.c parameter pack pattern):
    //   if (_comp != null && !IsNullOrEmpty(_methodName)):
    //     type = _comp.GetType()
    //     if (type != typeof(string)):
    //       types = new Type[5] { typeof(Component), typeof(PointerEventData), typeof(Action_Type), typeof(int), typeof(string) }
    //       _method = type.GetMethod(_methodName, types)
    //       if _method != null:
    //         _wnd = wnd
    //         _methodParams = new object[5] { _comp, null, null, null, null }
    //     else:
    //       types = new Type[6] { typeof(string), typeof(Component), typeof(PointerEventData), typeof(Action_Type), typeof(int), typeof(string) }
    //       _method = type.GetMethod("BtnClick_CallBack", types)
    //       if _method != null:
    //         _wnd = wnd
    //         _methodParams = new object[6] { _methodName, _comp, null, null, null, null }
    //   if _method == null:
    //     // log error "no method!!! " + _methodName (when _comp == null)
    //     // else "no method!!! WndID : {0} : {1} : {2}" string-builder of _wnd.WndID/_methodName/" : " + gameObject.name
    //     Debug.LogError(...); UnityEngine.Object.Destroy(this); return
    //   // Pivot adjustment to (0.5, 0.5) with anchoredPosition compensation
    //   rt = transform as RectTransform
    //   if rt != null:
    //     pivot = rt.pivot
    //     if pivot != (0.5, 0.5):
    //       rect = rt.rect; oldPivot = rt.pivot
    //       rt.pivot = new Vector2(0.5f, 0.5f)
    //       anch = rt.anchoredPosition
    //       rt.anchoredPosition = new Vector2(anch.x - rect.width*(oldPivot.x - 0.5f), anch.y - rect.height*(oldPivot.y - 0.5f))
    //   // TweenScale init (once per instance)
    //   if (!isInitTweenScale):
    //     isInitTweenScale = true
    //     _DefaultLocalScale = transform.localScale
    //     if _TweenScale == null:
    //       _TweenScale = gameObject.AddComponent<TweenScale>()
    //       _TweenScale.style = (UITweener.Style)0
    //       _TweenScale.duration = 0.1f
    //       _TweenScale.from = _DefaultLocalScale
    //       _TweenScale.to = _DefaultLocalScale * 0.95f
    //     _TweenScale.enabled = false
    //   wndClip = GetComponent<WndAudioClip>()
    public override void InitComponent(WndForm wnd)
    {
        // FIX 2026-05-12 (1-1 with Ghidra InitComponent.c): re-read of decompile shows
        // `param_6` is `wnd` (the method parameter), NOT `_comp`. The reflection target type
        // is `wnd.GetType()`, and _wnd field is set to wnd. _comp is only used as
        // _methodParams[0] (a passthrough payload — can legitimately be null, which matches
        // production prefab where _comp PPtr is (0, 0)/null verified via UnityPy parse of
        // APK file 8c754ee25529db948b10cfb22e0bee92 path_ids 282/296/318).
        //
        // Ghidra branching (param_6 == wnd):
        //   if (wnd == null) {
        //     if (_method == null) { log "no method!!! " + _methodName + Destroy(this); }
        //   } else if (!IsNullOrEmpty(_methodName)) {
        //     wndType = wnd.GetType();
        //     if (wndType != typeof(string)) {
        //       types = [Component, PointerEventData, Action_Type, int, string]
        //       _method = wndType.GetMethod(_methodName, types);
        //       if (_method != null) {
        //         _wnd = wnd;
        //         _methodParams = new object[5] { _comp, null, null, null, null }
        //       }
        //     } else {
        //       types = [string, Component, PointerEventData, Action_Type, int, string]
        //       _method = wndType.GetMethod("BtnClick_CallBack", types);
        //       _wnd = wnd;
        //       _methodParams = new object[6] { _methodName, _comp, null, null, null, null }
        //     }
        //   }
        //   if (_method == null) error+Destroy.
        if (wnd != null && !string.IsNullOrEmpty(_methodName))
        {
            System.Type wndType = wnd.GetType();
            if (wndType != typeof(string))
            {
                var types = new System.Type[]
                {
                    typeof(Component),
                    typeof(UnityEngine.EventSystems.PointerEventData),
                    typeof(Action_Type),
                    typeof(int),
                    typeof(string),
                };
                _method = wndType.GetMethod(_methodName, types);
                if (_method != null)
                {
                    _wnd = wnd;
                    _methodParams = new object[5];
                    _methodParams[0] = _comp;  // null OK — passed to method's `btn` arg
                }
            }
            else
            {
                var types = new System.Type[]
                {
                    typeof(string),
                    typeof(Component),
                    typeof(UnityEngine.EventSystems.PointerEventData),
                    typeof(Action_Type),
                    typeof(int),
                    typeof(string),
                };
                _method = wndType.GetMethod("BtnClick_CallBack", types);
                if (_method != null)
                {
                    _wnd = wnd;
                    _methodParams = new object[6];
                    _methodParams[0] = _methodName;
                    _methodParams[1] = _comp;
                }
            }
        }
        if (_method == null)
        {
            if (wnd == null)
            {
                Debug.LogError("no method!!! " + _methodName);
            }
            else
            {
                // String literal #18863 = "no method!!! WndID : " + wnd.wID + " : " + _methodName + " : " + gameObject.name
                // Per Ghidra param_6 + 0x50 access — offset 0x50 on WndForm = wID field (per dump.cs).
                uint id = 0;
                try { id = wnd.wID; } catch { }
                Debug.LogError("no method!!! WndID : " + id.ToString() + " : " + _methodName + " : " + gameObject.name);
            }
            UnityEngine.Object.Destroy(this);
            return;
        }
        // Pivot adjustment
        RectTransform rt = transform as RectTransform;
        if (rt != null)
        {
            Vector2 pivot = rt.pivot;
            if (pivot.x != 0.5f || pivot.y != 0.5f)
            {
                Rect rect = rt.rect;
                Vector2 oldPivot = rt.pivot;
                rt.pivot = new Vector2(0.5f, 0.5f);
                Vector2 anch = rt.anchoredPosition;
                rt.anchoredPosition = new Vector2(
                    anch.x - rect.width * (oldPivot.x - 0.5f),
                    anch.y - rect.height * (oldPivot.y - 0.5f));
            }
        }
        // TweenScale init
        if (!isInitTweenScale)
        {
            isInitTweenScale = true;
            _DefaultLocalScale = transform.localScale;
            if (_TweenScale == null)
            {
                _TweenScale = gameObject.AddComponent<TweenScale>();
                if (_TweenScale == null) throw new System.NullReferenceException();
                _TweenScale.style = (UITweener.Style)0;
                _TweenScale.duration = 0.1f;
                _TweenScale.from = _DefaultLocalScale;
                _TweenScale.to = _DefaultLocalScale * 0.95f;
            }
            if (_TweenScale == null) throw new System.NullReferenceException();
            _TweenScale.enabled = false;
        }
        wndClip = GetComponent<WndAudioClip>();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/DinitComponent.c
    // RVA: 0x1957BB8
    // Clears 3 reflection fields (offsets 0x30, 0x38, 0x40).
    public override void DinitComponent(WndForm wnd)
    {
        _wnd = null;
        _method = null;
        _methodParams = null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/OnPointerClick.c
    // RVA: 0x1956ABC
    // 1-1 (re-read 2026-05-12 to fix port bug):
    //   if (WaitQuitApp() || _wnd == null || !_wnd.IsActive() || _method == null) return;
    //   wndType = _wnd.GetType();
    //   isStringType = (wndType == typeof(string));
    //   if (_methodParams == null) NRE.
    //   if (!isStringType):  // 5-arg form
    //     _methodParams[1] = eventData
    //     _methodParams[2] = (object)btnAction
    //     _methodParams[3] = (object)_ClickValue
    //     _methodParams[4] = _ClickString
    //   else:  // 6-arg form (BtnClick_CallBack)
    //     _methodParams[2] = eventData
    //     _methodParams[3] = (object)btnAction
    //     _methodParams[4] = (object)_ClickValue
    //     _methodParams[5] = _ClickString
    //   _method.Invoke(_wnd, _methodParams)  ← TARGET IS _wnd (offset 0x30), NOT _comp!
    //
    // Ghidra confirms (line 122-127):
    //   System_Reflection_MethodBase__Invoke(
    //     *(param_1 + 0x38),  // _method
    //     *(param_1 + 0x30),  // _wnd ← target
    //     *(param_1 + 0x40),  // _methodParams
    //     0);
    public void OnPointerClick(PointerEventData eventData)
    {
        if (WndForm.WaitQuitApp()) return;
        if (_wnd == null) return;
        if (!_wnd.IsActive()) return;
        if (_method == null) return;
        System.Type wndType = _wnd.GetType();
        bool isStringType = (wndType == typeof(string));
        if (_methodParams == null) throw new System.NullReferenceException();
        if (!isStringType)
        {
            if (_methodParams.Length < 5) throw new System.IndexOutOfRangeException();
            _methodParams[1] = eventData;
            _methodParams[2] = (object)btnAction;
            _methodParams[3] = (object)_ClickValue;
            _methodParams[4] = _ClickString;
        }
        else
        {
            if (_methodParams.Length < 6) throw new System.IndexOutOfRangeException();
            _methodParams[2] = eventData;
            _methodParams[3] = (object)btnAction;
            _methodParams[4] = (object)_ClickValue;
            _methodParams[5] = _ClickString;
        }
        _method.Invoke(_wnd, _methodParams);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/OnPointerDown.c
    // RVA: 0x1957BF0
    // Guards: !WndForm.WaitQuitApp() && _wnd != null && _wnd.IsActive().
    // Plays sound: if wndClip != null → wndClip.PlaySound(); else SoundProxy.Instance.PlaySoundByName(literal#4107).
    // If UseTween && _TweenScale != null: _TweenScale.PlayForward().
    // String literal #4107 value extracted from stringliteral.json: see RVA 0x01957bf0 trace.
    public void OnPointerDown(PointerEventData eventData)
    {
        if (WndForm.WaitQuitApp()) return;
        if (_wnd == null) return;
        if (!_wnd.IsActive()) return;

        if (wndClip != null)
        {
            wndClip.PlaySound();
        }
        else
        {
            // Ghidra: SoundProxy__PlaySoundByName(volume=1.0f, pitch=1.0f, this=SoundProxy.main, name=literal#4107, fullPath=0, methodInfo=0)
            // C# signature: PlaySoundByName(string name, float volume=1f, float pitch=1f, bool fullPath=false)
            SoundProxy.main.PlaySoundByName(STR_BTN_CLICK_SOUND, 1f, 1f, false);
        }

        if (UseTween)
        {
            if (_TweenScale != null)
            {
                _TweenScale.PlayForward();
            }
        }
    }

    // String literal #4107 from work/03_il2cpp_dump/stringliteral.json
    private const string STR_BTN_CLICK_SOUND = "ClickUp";

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/OnPointerUp.c
    // RVA: 0x1957D40
    // Guards: !WaitQuitApp() && _wnd != null && _wnd.IsActive() && UseTween && _TweenScale != null
    // Action: _TweenScale.PlayReverse()
    public void OnPointerUp(PointerEventData eventData)
    {
        if (WndForm.WaitQuitApp()) return;
        if (_wnd == null) return;
        if (!_wnd.IsActive()) return;
        if (!UseTween) return;
        if (_TweenScale != null)
        {
            _TweenScale.PlayReverse();
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/OnEnable.c
    // RVA: 0x1957DE8
    // If _TweenScale != null: Toggle(), set enabled=false, ResetToBeginning().
    private void OnEnable()
    {
        if (_TweenScale != null)
        {
            _TweenScale.Toggle();
            _TweenScale.enabled = false;
            _TweenScale.ResetToBeginning();
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndClickMethod/SetBtnAction.c
    // RVA: 0x1957E90
    // Single store: btnAction = (Action_Type)action.
    public void SetBtnAction(int action)
    {
        btnAction = (Action_Type)action;
    }
}
