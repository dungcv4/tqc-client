// Source: Ghidra work/06_ghidra/decompiled_full/WndFormNode/ (3 .c).
// Source: dump.cs TypeDefIndex 298
// Field offsets:
//  0x10 _canvas, 0x18 _canvasGroup, 0x20 _animator, 0x28 _wndTrans,
//  0x30 _parent, 0x38 _body, 0x40 _front, 0x48 _next,
//  0x50 _postDestroy, 0x51 _top, 0x52 _popup, 0x53 _hasAnimatorParams, 0x58 _subCanvases
// PTR_DAT_03459450 = IWndForm.RemoveNode method handle (vtable slot 3).

using System;
using UnityEngine;

public class WndFormNode
{
    public Canvas _canvas;             // 0x10
    public CanvasGroup _canvasGroup;   // 0x18
    public Animator _animator;         // 0x20
    public RectTransform _wndTrans;    // 0x28
    public IWndForm _parent;           // 0x30
    public WndForm _body;              // 0x38
    public WndFormNode _front;         // 0x40
    public WndFormNode _next;          // 0x48
    public bool _postDestroy;          // 0x50
    public bool _top;                  // 0x51
    public bool _popup;                // 0x52
    public bool _hasAnimatorParams;    // 0x53
    public Canvas[] _subCanvases;      // 0x58

    // Source: Ghidra work/06_ghidra/decompiled_full/WndFormNode/Create.c  RVA 0x1A05884
    // String literals (resolved via work/03_il2cpp_dump/stringliteral.json):
    //   #15840 = "create wndForm:"
    //   #12612 = "WndFormNode.Create: prefab {0} no Canvas"
    //   #5045  = "Enter"
    //   #12611 = "WndFormNode.Create: no {0} prefab"
    // PTR_DAT_* (component type tokens — inferred from offset/order in dump.cs):
    //   PTR_DAT_0346a9b8 = typeof(Canvas)        → _canvas       (0x10)
    //   PTR_DAT_0346a9b0 = typeof(CanvasGroup)   → _canvasGroup  (0x18)
    //   PTR_DAT_03462118 = typeof(Animator)      → _animator     (0x20) + HasParameter target
    //   PTR_DAT_03465b60 = typeof(RectTransform) → _wndTrans     (0x28)
    //   PTR_DAT_0346a9a8 = typeof(Canvas)        → GetComponentsInChildren element
    //   PTR_DAT_0346a9c0 = MethodInfo for CompareFunc (Comparison<Canvas> target)
    public static WndFormNode Create(uint eWndFormID, GameObject objPrefab)
    {
        if (objPrefab == null)
        {
            UJDebug.LogError(string.Format("WndFormNode.Create: no {0} prefab", eWndFormID));
            return null;
        }
        // First check: prefab must already have a Canvas (Ghidra calls GetComponent on prefab
        // using the SAME type token as later assigned to _canvas → typeof(Canvas)).
        if (objPrefab.GetComponent<Canvas>() == null)
        {
            UJDebug.LogError(string.Format("WndFormNode.Create: prefab {0} no Canvas", eWndFormID));
            return null;
        }
        GameObject obj = UnityEngine.Object.Instantiate<GameObject>(objPrefab);
        UJDebug.LogTrace(string.Concat("create wndForm:", objPrefab.name));

        WndFormNode node = new WndFormNode();
        if (obj == null) return node;

        node._canvas      = obj.GetComponent<Canvas>();        // 0x10
        node._canvasGroup = obj.GetComponent<CanvasGroup>();   // 0x18
        node._animator    = obj.GetComponent<Animator>();      // 0x20
        // _hasAnimatorParams = (_animator != null) && WndFormExtensions.HasParameter(_animator, "Enter")
        if (node._animator != null)
        {
            node._hasAnimatorParams = WndFormExtensions.HasParameter(node._animator, "Enter");
        }
        else
        {
            node._hasAnimatorParams = false;
        }
        node._wndTrans = obj.GetComponent<RectTransform>();    // 0x28
        node._parent      = null;  // 0x30
        node._body        = null;  // 0x38
        node._front       = null;  // 0x40
        node._next        = null;  // 0x48
        node._postDestroy = false; // 0x50

        if (node._canvasGroup != null)
        {
            // Ghidra: UnityEngine_CanvasGroup__set_alpha(_canvasGroup, 0). Hide on create.
            node._canvasGroup.alpha = 0f;

            Transform t = obj.transform;
            if (t != null)
            {
                Canvas[] subs = t.GetComponentsInChildren<Canvas>(true);
                if (subs != null)
                {
                    // Ghidra: if (subs.Length < 2) return node;  → no sort needed for 0 or 1 element
                    if (subs.Length < 2)
                    {
                        return node;
                    }
                    System.Array.Sort<Canvas>(subs, new System.Comparison<Canvas>(CompareFunc));
                    node._subCanvases = subs;
                    return node;
                }
            }
        }
        return node;
    }

    // Source: Ghidra CompareFunc.c  RVA 0x1A09870
    public static int CompareFunc(Canvas a, Canvas b)
    {
        if (a != b)
        {
            if (a != null)
            {
                if (b != null)
                {
                    int idA = a.GetInstanceID();
                    int idB = b.GetInstanceID();
                    if (idB <= idA) return -1;
                    return 1;
                }
            }
        }
        return 0;
    }

    // Source: Ghidra Destroy.c  RVA 0x1A073F4
    // Flow:
    //   if _parent == null: return.
    //   _parent.RemoveNode(this)  (Ghidra: vtable slot 3 lookup via PTR_DAT_03459450).
    //   if _canvas == null: nullify (_body, _canvas, _subCanvases) and return.
    //   obj = _canvas.gameObject; if obj == null: same nullification.
    //   else: _wndTrans.SetParent(null) — NRE if _wndTrans null;
    //         obj2 = _canvas.gameObject; Object.Destroy(obj2); nullify; return.
    public void Destroy()
    {
        if (_parent == null) return;

        _parent.RemoveNode(this);

        if (_canvas == null)
        {
            _body = null;
            _canvas = null;
            _subCanvases = null;
            return;
        }

        GameObject obj = _canvas.gameObject;
        if (obj == null)
        {
            _body = null;
            _canvas = null;
            _subCanvases = null;
            return;
        }

        if (_wndTrans == null) throw new System.NullReferenceException();
        _wndTrans.SetParent(null);

        if (_canvas == null) throw new System.NullReferenceException();
        GameObject obj2 = _canvas.gameObject;
        UnityEngine.Object.Destroy(obj2);

        _body = null;
        _canvas = null;
        _subCanvases = null;
    }

    // Source: Ghidra (no .ctor.c) — default ctor inferred. RVA 0x1A09868.
    public WndFormNode() { }
}
