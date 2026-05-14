// HAND-WRITTEN tolua wrap for SMapTextureMgr.
// dump.cs lists SMapTextureMgrWrap at TypeDefIndex 439 in the original binary, so the production
// build had this wrap auto-generated. Our regen is blocked by the SpineSkin clash that breaks
// the upstream tolua# generator for ~20 classes; restoring this 1-1 from dump.cs surface.
//
// Lua usage: WndForm/WndForm_MainSMap.lua:601
//   self._smapImg.sprite = SMapTextureMgr.Instance:GetSMapSprite(tostring(stageData.smap_pic))
//
// Without this wrap, the Lua global `SMapTextureMgr` resolves to nil and every WndForm_MainSMap
// V_Create fires "attempt to index global 'SMapTextureMgr' (a nil value)" — which cascades up to
// PostProcessSceneObjects (already past the typeof fix) and blocks the rest of the in-map UI.

using System;
using LuaInterface;

public class SMapTextureMgrWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(SMapTextureMgr), typeof(System.Object));
        L.RegFunction("GetSMapSprite", GetSMapSprite);
        L.RegFunction("New",        _CreateSMapTextureMgr);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.RegVar("Instance", get_Instance, null);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateSMapTextureMgr(IntPtr L)
    {
        try
        {
            SMapTextureMgr o = new SMapTextureMgr();
            ToLua.PushObject(L, o);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_Instance(IntPtr L)
    {
        try
        {
            ToLua.PushObject(L, SMapTextureMgr.Instance);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetSMapSprite(IntPtr L)
    {
        try
        {
            ToLua.CheckArgsCount(L, 2);
            SMapTextureMgr obj = (SMapTextureMgr)ToLua.CheckObject<SMapTextureMgr>(L, 1);
            string smapName = ToLua.CheckString(L, 2);
            UnityEngine.Sprite ret = obj.GetSMapSprite(smapName);
            ToLua.Push(L, ret);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}
