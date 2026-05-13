// HAND-WRITTEN wrap for UnityEngine.RectTransform (extends UnityEngine.Transform).
// Auto-gen blocked by upstream tolua# generator (Spine.Unity.SpineSkin clash — same reason as
// WndProperty/WndSubProperty wraps).
//
// Lua usage hot paths:
//   1. ProcessBase.lua V_Update (line 130): self.clickEff.transform.anchoredPosition = Vector3.New(...)
//      → Triggered every UI click. Without this wrap, throws "field or property anchoredPosition
//        does not exist" because GameObject.transform returns Transform (base type, no anchoredPosition).
//      → Fix: tolua's PushUserObject uses GetType() at runtime → if transform is actually a
//        RectTransform, it picks up this wrap and exposes anchoredPosition correctly.
//   2. WndForm_Joystick / WndForm_Map / WndForm_Combine / WndForm_Auto: rect.width / rect.height
//   3. WndClickMethod.InitComponent (C# side): rt.pivot / rt.rect / rt.anchoredPosition — already
//      uses C# types directly (no Lua wrap needed for that path).
//
// Properties exposed (covers every Lua-side access found in grep):
//   anchoredPosition (Vector2 R/W), anchorMin/Max (Vector2 R/W),
//   offsetMin/Max (Vector2 R/W), pivot (Vector2 R/W), sizeDelta (Vector2 R/W),
//   rect (Rect read-only — used as .width / .height in Lua).

using System;
using LuaInterface;

public class UnityEngine_RectTransformWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(UnityEngine.RectTransform), typeof(UnityEngine.Transform));
        L.RegFunction("New", _CreateUnityEngine_RectTransform);
        L.RegFunction("__eq", op_Equality);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.RegVar("anchoredPosition", get_anchoredPosition, set_anchoredPosition);
        L.RegVar("anchorMin",        get_anchorMin,        set_anchorMin);
        L.RegVar("anchorMax",        get_anchorMax,        set_anchorMax);
        L.RegVar("offsetMin",        get_offsetMin,        set_offsetMin);
        L.RegVar("offsetMax",        get_offsetMax,        set_offsetMax);
        L.RegVar("pivot",            get_pivot,            set_pivot);
        L.RegVar("sizeDelta",        get_sizeDelta,        set_sizeDelta);
        L.RegVar("rect",             get_rect,             null);  // read-only — Lua uses .rect.width/.height
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateUnityEngine_RectTransform(IntPtr L)
    {
        try
        {
            UnityEngine.RectTransform o = new UnityEngine.RectTransform();
            ToLua.PushObject(L, o);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int op_Equality(IntPtr L)
    {
        try
        {
            UnityEngine.Object a0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
            UnityEngine.Object a1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
            LuaDLL.lua_pushboolean(L, a0 == a1);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    // ─── Vector2 getter/setter pattern (DRY via helper) ─────────────────────

    static int GetVec2(IntPtr L, Func<UnityEngine.RectTransform, UnityEngine.Vector2> read, string field)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.RectTransform obj = (UnityEngine.RectTransform)o;
            UnityEngine.Vector2 v = read(obj);
            ToLua.Push(L, v);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index " + field + " on a nil value"); }
    }

    static int SetVec2(IntPtr L, Action<UnityEngine.RectTransform, UnityEngine.Vector2> write, string field)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.RectTransform obj = (UnityEngine.RectTransform)o;
            UnityEngine.Vector2 v = ToLua.ToVector2(L, 2);
            write(obj, v);
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index " + field + " on a nil value"); }
    }

    // anchoredPosition (Vector2)
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_anchoredPosition(IntPtr L) { return GetVec2(L, r => r.anchoredPosition, "anchoredPosition"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_anchoredPosition(IntPtr L) { return SetVec2(L, (r, v) => r.anchoredPosition = v, "anchoredPosition"); }

    // anchorMin / anchorMax (Vector2)
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_anchorMin(IntPtr L) { return GetVec2(L, r => r.anchorMin, "anchorMin"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_anchorMin(IntPtr L) { return SetVec2(L, (r, v) => r.anchorMin = v, "anchorMin"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_anchorMax(IntPtr L) { return GetVec2(L, r => r.anchorMax, "anchorMax"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_anchorMax(IntPtr L) { return SetVec2(L, (r, v) => r.anchorMax = v, "anchorMax"); }

    // offsetMin / offsetMax (Vector2)
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_offsetMin(IntPtr L) { return GetVec2(L, r => r.offsetMin, "offsetMin"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_offsetMin(IntPtr L) { return SetVec2(L, (r, v) => r.offsetMin = v, "offsetMin"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_offsetMax(IntPtr L) { return GetVec2(L, r => r.offsetMax, "offsetMax"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_offsetMax(IntPtr L) { return SetVec2(L, (r, v) => r.offsetMax = v, "offsetMax"); }

    // pivot (Vector2)
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_pivot(IntPtr L) { return GetVec2(L, r => r.pivot, "pivot"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_pivot(IntPtr L) { return SetVec2(L, (r, v) => r.pivot = v, "pivot"); }

    // sizeDelta (Vector2)
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_sizeDelta(IntPtr L) { return GetVec2(L, r => r.sizeDelta, "sizeDelta"); }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_sizeDelta(IntPtr L) { return SetVec2(L, (r, v) => r.sizeDelta = v, "sizeDelta"); }

    // rect (Rect, read-only). Lua usage: rt.rect.width, rt.rect.height
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_rect(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.RectTransform obj = (UnityEngine.RectTransform)o;
            UnityEngine.Rect ret = obj.rect;
            ToLua.PushValue(L, ret);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index rect on a nil value"); }
    }
}
