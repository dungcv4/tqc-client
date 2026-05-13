// HAND-WRITTEN wrap for UnityEngine.AsyncOperation.
// Auto-gen blocked by upstream tolua# generator (same SpineSkin / Spine.Unity clash that blocks
// RectTransform wrap). Production binary ships its own auto-gen wrap; this file restores parity.
//
// Lua usage hot paths (grep "\.isDone\|\.progress" over Lua):
//   Manager/SceneMgr.lua:160  self.TestLoadSceneOp.isDone
//   Manager/SceneMgr.lua:181  self.TestUnloadSceneOp.isDone
//   Manager/SceneMgr.lua:252  state.asynLoadOp.isDone
//   Manager/SceneMgr.lua:254  state.asynLoadOp.progress
//   Manager/SceneMgr.lua:364  state.asynLoadOp.isDone
//   Manager/SceneMgr.lua:375  state.asynUnLoadOp.isDone
//
// All four references read .isDone / .progress on the return value of
//   SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive)  → UnityEngine.AsyncOperation
//   SceneManager.UnloadSceneAsync(name)                        → UnityEngine.AsyncOperation
// Without this wrap, tolua throws "field or property isDone does not exist" because
// AsyncOperation has no compile-time wrap and tolua's reflection fallback doesn't expose
// the auto-properties of UnityEngine native types by default.
//
// Properties exposed (the four Lua actually touches):
//   isDone (bool, read-only)
//   progress (float, read-only)
//   allowSceneActivation (bool, R/W) — included since LoadSceneAsync flow may toggle it
//   priority (int, R/W)
//
// AsyncOperation is abstract — no public ctor. We omit the New function; objects flow into Lua
// only via tolua return-marshaling from C# calls (e.g. SceneManager.LoadSceneAsync).

using System;
using LuaInterface;

public class UnityEngine_AsyncOperationWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(UnityEngine.AsyncOperation), typeof(System.Object));
        L.RegFunction("__eq", op_Equality);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.RegVar("isDone",               get_isDone,               null);
        L.RegVar("progress",             get_progress,             null);
        L.RegVar("allowSceneActivation", get_allowSceneActivation, set_allowSceneActivation);
        L.RegVar("priority",             get_priority,             set_priority);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int op_Equality(IntPtr L)
    {
        try
        {
            object a0 = ToLua.ToObject(L, 1);
            object a1 = ToLua.ToObject(L, 2);
            LuaDLL.lua_pushboolean(L, object.ReferenceEquals(a0, a1));
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_isDone(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
            LuaDLL.lua_pushboolean(L, obj.isDone);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index isDone on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_progress(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
            LuaDLL.lua_pushnumber(L, obj.progress);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index progress on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_allowSceneActivation(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
            LuaDLL.lua_pushboolean(L, obj.allowSceneActivation);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index allowSceneActivation on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_allowSceneActivation(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
            bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
            obj.allowSceneActivation = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index allowSceneActivation on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_priority(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
            LuaDLL.lua_pushinteger(L, obj.priority);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index priority on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_priority(IntPtr L)
    {
        object o = null;
        try
        {
            o = ToLua.ToObject(L, 1);
            UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
            int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
            obj.priority = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index priority on a nil value"); }
    }
}
