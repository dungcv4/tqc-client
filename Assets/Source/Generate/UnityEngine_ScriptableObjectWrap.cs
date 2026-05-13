// HAND-WRITTEN wrap for UnityEngine.ScriptableObject (production: dump.cs TypeDefIndex 566).
// Cannot use ToLuaMenu auto-gen because broader regen crashes on Spine.Unity.SpineSkin
// attribute (AnalysisFailedException). Manual wrap follows auto-gen template.
//
// Lua usage (PostProcessMgr.lua:27):
//   ScriptableObject.CreateInstance(typeof(UnityEngine.PostProcessing.PostProcessingProfile))

using System;
using LuaInterface;

public class UnityEngine_ScriptableObjectWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(UnityEngine.ScriptableObject), typeof(UnityEngine.Object));
        L.RegFunction("CreateInstance", CreateInstance);
        L.RegFunction("New", _CreateUnityEngine_ScriptableObject);
        L.RegFunction("__eq", op_Equality);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateUnityEngine_ScriptableObject(IntPtr L)
    {
        try
        {
            // ScriptableObject ctor exists but Unity advises against direct new(); the proper path
            // is CreateInstance. We forward to that to match production behavior.
            return LuaDLL.luaL_throw(L, "Use ScriptableObject.CreateInstance(type) instead of new ScriptableObject()");
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int CreateInstance(IntPtr L)
    {
        try
        {
            int count = LuaDLL.lua_gettop(L);
            if (count == 1)
            {
                // Static call: arg1 is Type
                Type arg0 = (Type)ToLua.CheckObject(L, 1, typeof(Type));
                UnityEngine.ScriptableObject o = UnityEngine.ScriptableObject.CreateInstance(arg0);
                ToLua.Push(L, o);
                return 1;
            }
            else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
            {
                // ScriptableObject.CreateInstance(string) overload
                string arg0 = ToLua.ToString(L, 2);
                UnityEngine.ScriptableObject o = UnityEngine.ScriptableObject.CreateInstance(arg0);
                ToLua.Push(L, o);
                return 1;
            }
            else
            {
                return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ScriptableObject.CreateInstance");
            }
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
