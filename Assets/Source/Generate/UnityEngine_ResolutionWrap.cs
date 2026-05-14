// HAND-WRITTEN wrap for UnityEngine.Resolution (value type / struct).
// Auto-gen blocked by upstream tolua# generator (same SpineSkin clash as Camera/Light wraps).
//
// Lua usage hot path:
//   WndForm/UISafeArea:92  local currentResolution = Screen.currentResolution
//                          UISafeArea.CurrentResolutionWidth  = currentResolution.width
//                          UISafeArea.CurrentResolutionHeight = currentResolution.height
//
// Without this wrap, `Screen.currentResolution` pushes a userdata with no metatable for
// width/height fields, so PostProcessSceneObjects → InitSafeArea throws
// "field or property width does not exist" and every UI form that fires CheckSafeArea
// (Joystick, MainSMap, MainSkillIcon, Chat, etc.) fails to create.
//
// dump.cs (TypeDefIndex 5952):
//   private int m_Width;              // 0x0
//   private int m_Height;             // 0x4
//   private RefreshRate m_RefreshRate;// 0x8
//   public int width { get; set; }
//   public int height { get; set; }
//   public RefreshRate refreshRateRatio { get; set; }
//
// Lua only reads width/height; refreshRateRatio omitted because it returns RefreshRate struct
// which is yet another value type without a wrap and not referenced in any .lua.

using System;
using LuaInterface;

public class UnityEngine_ResolutionWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(UnityEngine.Resolution), null);
        L.RegFunction("New",        _CreateUnityEngine_Resolution);
        L.RegFunction("ToString",   ToString);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.RegVar("width",  get_width,  set_width);
        L.RegVar("height", get_height, set_height);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateUnityEngine_Resolution(IntPtr L)
    {
        try
        {
            UnityEngine.Resolution obj = new UnityEngine.Resolution();
            ToLua.PushValue(L, obj);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ToString(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.Resolution obj = (UnityEngine.Resolution)o;
            LuaDLL.lua_pushstring(L, obj.ToString());
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to call ToString on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_width(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.Resolution obj = (UnityEngine.Resolution)o;
            LuaDLL.lua_pushinteger(L, obj.width);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index width on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_width(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.Resolution obj = (UnityEngine.Resolution)o;
            obj.width = (int)LuaDLL.luaL_checknumber(L, 2);
            ToLua.SetBack(L, 1, obj);
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index width on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_height(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.Resolution obj = (UnityEngine.Resolution)o;
            LuaDLL.lua_pushinteger(L, obj.height);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index height on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_height(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.Resolution obj = (UnityEngine.Resolution)o;
            obj.height = (int)LuaDLL.luaL_checknumber(L, 2);
            ToLua.SetBack(L, 1, obj);
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index height on a nil value"); }
    }
}
