// HAND-WRITTEN wrap for WndProperty (dump.cs TypeDefIndex 217).
// Cannot use ToLuaMenu auto-gen because broader regen crashes on Spine.Unity.SpineSkin
// (AnalysisFailedException). Minimal wrap — Lua only uses typeof(WndProperty) for
// GetComponent(typeof(WndProperty)) lookups; no method calls from Lua side.

using System;
using LuaInterface;

public class WndPropertyWrap
{
    public static void Register(LuaState L)
    {
        // Parent = IWndComponent (which is MonoBehaviour subclass).
        // For typeof() to work in Lua, just BeginClass with proper hierarchy.
        L.BeginClass(typeof(WndProperty), typeof(IWndComponent));
        L.RegFunction("New", _CreateWndProperty);
        L.RegFunction("__eq", op_Equality);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateWndProperty(IntPtr L)
    {
        try
        {
            // WndProperty is a MonoBehaviour — created via AddComponent or prefab serialization,
            // not direct new(). Match Ghidra .ctor.c which is empty (parameterless).
            WndProperty o = new WndProperty();
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
            UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
            UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
            LuaDLL.lua_pushboolean(L, arg0 == arg1);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}
