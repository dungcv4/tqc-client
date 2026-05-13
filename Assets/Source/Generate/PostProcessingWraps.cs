// HAND-WRITTEN wraps for UnityEngine.PostProcessing.* stub types.
// Cannot use ToLuaMenu.GenerateClassWraps for these because the broader regen path
// crashes on Spine.Unity.SpineSkin attribute (Cpp2IlInjected.AnalysisFailedException
// when reading custom attributes via reflection). Manual wraps follow the auto-gen
// pattern from WndForm_LoadingScreenWrap.cs etc. Registered in LuaBinder.cs below.
//
// Source: dump.cs TypeDefIndex 1029-1066 (production wraps 517-541) — only the types
// touched by PostProcessMgr.lua are wrapped (PostProcessingBehaviour, Profile, Vignette
// + DepthOfField models + nested Settings structs).

using System;
using UnityEngine.PostProcessing;
using LuaInterface;

public class UnityEngine_PostProcessing_PostProcessingBehaviourWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(PostProcessingBehaviour), typeof(UnityEngine.MonoBehaviour));
        L.RegVar("profile", get_profile, set_profile);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_profile(IntPtr L)
    {
        try
        {
            PostProcessingBehaviour obj = (PostProcessingBehaviour)ToLua.CheckObject(L, 1, typeof(PostProcessingBehaviour));
            ToLua.Push(L, obj.profile);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_profile(IntPtr L)
    {
        try
        {
            PostProcessingBehaviour obj = (PostProcessingBehaviour)ToLua.CheckObject(L, 1, typeof(PostProcessingBehaviour));
            PostProcessingProfile arg0 = (PostProcessingProfile)ToLua.CheckObject(L, 2, typeof(PostProcessingProfile));
            obj.profile = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}

public class UnityEngine_PostProcessing_PostProcessingProfileWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(PostProcessingProfile), typeof(UnityEngine.ScriptableObject));
        L.RegVar("vignette", get_vignette, set_vignette);
        L.RegVar("depthOfField", get_depthOfField, set_depthOfField);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_vignette(IntPtr L)
    {
        try
        {
            PostProcessingProfile obj = (PostProcessingProfile)ToLua.CheckObject(L, 1, typeof(PostProcessingProfile));
            ToLua.Push(L, obj.vignette);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_vignette(IntPtr L)
    {
        try
        {
            PostProcessingProfile obj = (PostProcessingProfile)ToLua.CheckObject(L, 1, typeof(PostProcessingProfile));
            VignetteModel arg0 = (VignetteModel)ToLua.CheckObject(L, 2, typeof(VignetteModel));
            obj.vignette = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_depthOfField(IntPtr L)
    {
        try
        {
            PostProcessingProfile obj = (PostProcessingProfile)ToLua.CheckObject(L, 1, typeof(PostProcessingProfile));
            ToLua.Push(L, obj.depthOfField);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_depthOfField(IntPtr L)
    {
        try
        {
            PostProcessingProfile obj = (PostProcessingProfile)ToLua.CheckObject(L, 1, typeof(PostProcessingProfile));
            DepthOfFieldModel arg0 = (DepthOfFieldModel)ToLua.CheckObject(L, 2, typeof(DepthOfFieldModel));
            obj.depthOfField = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}

public class UnityEngine_PostProcessing_VignetteModelWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(VignetteModel), typeof(PostProcessingModel));
        L.RegVar("enabled", get_enabled, set_enabled);
        L.RegVar("settings", get_settings, set_settings);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_enabled(IntPtr L)
    {
        try
        {
            VignetteModel obj = (VignetteModel)ToLua.CheckObject(L, 1, typeof(VignetteModel));
            LuaDLL.lua_pushboolean(L, obj.enabled);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_enabled(IntPtr L)
    {
        try
        {
            VignetteModel obj = (VignetteModel)ToLua.CheckObject(L, 1, typeof(VignetteModel));
            obj.enabled = LuaDLL.luaL_checkboolean(L, 2);
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_settings(IntPtr L)
    {
        try
        {
            VignetteModel obj = (VignetteModel)ToLua.CheckObject(L, 1, typeof(VignetteModel));
            ToLua.PushValue(L, obj.settings);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_settings(IntPtr L)
    {
        try
        {
            VignetteModel obj = (VignetteModel)ToLua.CheckObject(L, 1, typeof(VignetteModel));
            VignetteModel.Settings arg0 = (VignetteModel.Settings)ToLua.CheckObject(L, 2, typeof(VignetteModel.Settings));
            obj.settings = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}

public class UnityEngine_PostProcessing_DepthOfFieldModelWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(DepthOfFieldModel), typeof(PostProcessingModel));
        L.RegVar("enabled", get_enabled, set_enabled);
        L.RegVar("settings", get_settings, set_settings);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_enabled(IntPtr L)
    {
        try
        {
            DepthOfFieldModel obj = (DepthOfFieldModel)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel));
            LuaDLL.lua_pushboolean(L, obj.enabled);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_enabled(IntPtr L)
    {
        try
        {
            DepthOfFieldModel obj = (DepthOfFieldModel)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel));
            obj.enabled = LuaDLL.luaL_checkboolean(L, 2);
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_settings(IntPtr L)
    {
        try
        {
            DepthOfFieldModel obj = (DepthOfFieldModel)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel));
            ToLua.PushValue(L, obj.settings);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_settings(IntPtr L)
    {
        try
        {
            DepthOfFieldModel obj = (DepthOfFieldModel)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel));
            DepthOfFieldModel.Settings arg0 = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 2, typeof(DepthOfFieldModel.Settings));
            obj.settings = arg0;
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}

public class UnityEngine_PostProcessing_PostProcessingModelWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(PostProcessingModel), null);
        L.RegVar("enabled", get_enabled, set_enabled);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_enabled(IntPtr L)
    {
        try
        {
            PostProcessingModel obj = (PostProcessingModel)ToLua.CheckObject(L, 1, typeof(PostProcessingModel));
            LuaDLL.lua_pushboolean(L, obj.enabled);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_enabled(IntPtr L)
    {
        try
        {
            PostProcessingModel obj = (PostProcessingModel)ToLua.CheckObject(L, 1, typeof(PostProcessingModel));
            obj.enabled = LuaDLL.luaL_checkboolean(L, 2);
            return 0;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }
}

public class UnityEngine_PostProcessing_VignetteModel_SettingsWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(VignetteModel.Settings), null);
        L.RegFunction("New", _Create);
        L.RegVar("intensity", get_intensity, set_intensity);
        L.RegVar("smoothness", get_smoothness, set_smoothness);
        L.RegVar("roundness", get_roundness, set_roundness);
        L.RegVar("rounded", get_rounded, set_rounded);
        L.RegVar("color", get_color, set_color);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _Create(IntPtr L)
    {
        VignetteModel.Settings o = default(VignetteModel.Settings);
        ToLua.PushValue(L, o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_intensity(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); LuaDLL.lua_pushnumber(L, o.intensity); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_intensity(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); o.intensity = (float)LuaDLL.luaL_checknumber(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_smoothness(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); LuaDLL.lua_pushnumber(L, o.smoothness); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_smoothness(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); o.smoothness = (float)LuaDLL.luaL_checknumber(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_roundness(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); LuaDLL.lua_pushnumber(L, o.roundness); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_roundness(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); o.roundness = (float)LuaDLL.luaL_checknumber(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_rounded(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); LuaDLL.lua_pushboolean(L, o.rounded); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_rounded(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); o.rounded = LuaDLL.luaL_checkboolean(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_color(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); ToLua.PushValue(L, o.color); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_color(IntPtr L) { try { var o = (VignetteModel.Settings)ToLua.CheckObject(L, 1, typeof(VignetteModel.Settings)); o.color = (UnityEngine.Color)ToLua.CheckObject(L, 2, typeof(UnityEngine.Color)); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
}

public class UnityEngine_PostProcessing_DepthOfFieldModel_SettingsWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(DepthOfFieldModel.Settings), null);
        L.RegFunction("New", _Create);
        L.RegVar("focusDistance", get_focusDistance, set_focusDistance);
        L.RegVar("aperture", get_aperture, set_aperture);
        L.RegVar("focalLength", get_focalLength, set_focalLength);
        L.RegVar("useCameraFov", get_useCameraFov, set_useCameraFov);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _Create(IntPtr L) { DepthOfFieldModel.Settings o = default(DepthOfFieldModel.Settings); ToLua.PushValue(L, o); return 1; }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_focusDistance(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); LuaDLL.lua_pushnumber(L, o.focusDistance); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_focusDistance(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); o.focusDistance = (float)LuaDLL.luaL_checknumber(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_aperture(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); LuaDLL.lua_pushnumber(L, o.aperture); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_aperture(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); o.aperture = (float)LuaDLL.luaL_checknumber(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_focalLength(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); LuaDLL.lua_pushnumber(L, o.focalLength); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_focalLength(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); o.focalLength = (float)LuaDLL.luaL_checknumber(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_useCameraFov(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); LuaDLL.lua_pushboolean(L, o.useCameraFov); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_useCameraFov(IntPtr L) { try { var o = (DepthOfFieldModel.Settings)ToLua.CheckObject(L, 1, typeof(DepthOfFieldModel.Settings)); o.useCameraFov = LuaDLL.luaL_checkboolean(L, 2); ToLua.SetBack(L, 1, o); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e); } }
}
