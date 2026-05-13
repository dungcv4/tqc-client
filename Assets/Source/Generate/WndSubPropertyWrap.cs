// HAND-WRITTEN wrap for WndSubProperty (dump.cs TypeDefIndex 218).
// Lua usage: GetComponent(typeof(WndSubProperty)) in WndForm_LoginGame:RefreshServerList,
// then `wndSubProperty._properties[i]._name/_index/_object` reads via reflection.
// Lua also calls `WndForm:InitWndSubProperty(o, wndSubProperty)` which iterates _properties.

using System;
using LuaInterface;

public class WndSubPropertyWrap
{
    public static void Register(LuaState L)
    {
        // WndSubProperty extends MonoBehaviour directly (not IWndComponent).
        L.BeginClass(typeof(WndSubProperty), typeof(UnityEngine.MonoBehaviour));
        L.RegFunction("New", _CreateWndSubProperty);
        L.RegFunction("__eq", op_Equality);
        L.RegFunction("__tostring", ToLua.op_ToString);
        // Expose _properties field for Lua read access (InitWndSubProperty iterates it).
        L.RegVar("_properties", get__properties, null);
        L.EndClass();

        // Nested class WndSubProperty.PropertyDecl — Lua reads _name/_index/_object per iteration.
        L.BeginClass(typeof(WndSubProperty.PropertyDecl), typeof(object), "PropertyDecl");
        L.RegFunction("New", _CreatePropertyDecl);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.RegVar("_name", get_PropertyDecl__name, null);
        L.RegVar("_index", get_PropertyDecl__index, null);
        L.RegVar("_object", get_PropertyDecl__object, null);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateWndSubProperty(IntPtr L)
    {
        try
        {
            WndSubProperty o = new WndSubProperty();
            ToLua.PushObject(L, o);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get__properties(IntPtr L)
    {
        try
        {
            WndSubProperty obj = (WndSubProperty)ToLua.CheckObject(L, 1, typeof(WndSubProperty));
            ToLua.Push(L, obj._properties);
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

    // === PropertyDecl nested class accessors ===

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreatePropertyDecl(IntPtr L)
    {
        try
        {
            WndSubProperty.PropertyDecl o = new WndSubProperty.PropertyDecl();
            ToLua.PushObject(L, o);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_PropertyDecl__name(IntPtr L)
    {
        try
        {
            WndSubProperty.PropertyDecl obj = (WndSubProperty.PropertyDecl)ToLua.CheckObject(L, 1, typeof(WndSubProperty.PropertyDecl));
            LuaDLL.lua_pushstring(L, obj._name);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_PropertyDecl__index(IntPtr L)
    {
        try
        {
            WndSubProperty.PropertyDecl obj = (WndSubProperty.PropertyDecl)ToLua.CheckObject(L, 1, typeof(WndSubProperty.PropertyDecl));
            LuaDLL.lua_pushinteger(L, obj._index);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_PropertyDecl__object(IntPtr L)
    {
        try
        {
            WndSubProperty.PropertyDecl obj = (WndSubProperty.PropertyDecl)ToLua.CheckObject(L, 1, typeof(WndSubProperty.PropertyDecl));
            ToLua.Push(L, obj._object);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}
