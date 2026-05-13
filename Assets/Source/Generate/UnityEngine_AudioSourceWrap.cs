// HAND-WRITTEN wrap for UnityEngine.AudioSource (Component subclass).
// Auto-gen blocked by upstream tolua# generator (same SpineSkin clash that blocks
// AsyncOperation/RectTransform wraps).
//
// Lua usage (grep over Lua):
//   Manager/SceneMgr.lua:414  GetComponentsInChildren(typeof(UnityEngine.AudioSource))
//   Manager/SceneMgr.lua:606  self._audioSource[i].volume = soundVolume
//
// Without this wrap, `typeof(UnityEngine.AudioSource)` resolves to nil and tolua throws
// "attempt to call typeof on type nil" — blocks SceneMgr:PostProcessSceneObjects which
// fires once the scene finishes loading (line 292 of SceneMgr.lua).
//
// Properties exposed:
//   volume (float R/W) — used by UpdateAudioSource to apply global sound-volume setting
//   isPlaying (bool R) — used by AudioManager
//   mute (bool R/W), loop (bool R/W), pitch (float R/W), time (float R/W) — common controls
//
// Methods exposed:
//   Play() / Stop() / Pause() / UnPause() — standard playback controls
//
// AudioSource extends Behaviour which extends Component (registered in TransformWrap stack).

using System;
using LuaInterface;

public class UnityEngine_AudioSourceWrap
{
    public static void Register(LuaState L)
    {
        L.BeginClass(typeof(UnityEngine.AudioSource), typeof(UnityEngine.Behaviour));
        L.RegFunction("Play",    Play);
        L.RegFunction("Stop",    Stop);
        L.RegFunction("Pause",   Pause);
        L.RegFunction("UnPause", UnPause);
        L.RegFunction("__eq",       op_Equality);
        L.RegFunction("__tostring", ToLua.op_ToString);
        L.RegVar("volume",    get_volume,    set_volume);
        L.RegVar("isPlaying", get_isPlaying, null);
        L.RegVar("mute",      get_mute,      set_mute);
        L.RegVar("loop",      get_loop,      set_loop);
        L.RegVar("pitch",     get_pitch,     set_pitch);
        L.RegVar("time",      get_time,      set_time);
        L.RegVar("clip",      get_clip,      set_clip);
        L.EndClass();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int op_Equality(IntPtr L)
    {
        try
        {
            UnityEngine.Object a0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
            UnityEngine.Object a1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
            bool eq = a0 == a1;
            LuaDLL.lua_pushboolean(L, eq);
            return 1;
        }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Play(IntPtr L)
    {
        object o = null;
        try { o = ToLua.CheckObject<UnityEngine.AudioSource>(L, 1); ((UnityEngine.AudioSource)o).Play(); return 0; }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to call Play on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Stop(IntPtr L)
    {
        object o = null;
        try { o = ToLua.CheckObject<UnityEngine.AudioSource>(L, 1); ((UnityEngine.AudioSource)o).Stop(); return 0; }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to call Stop on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Pause(IntPtr L)
    {
        object o = null;
        try { o = ToLua.CheckObject<UnityEngine.AudioSource>(L, 1); ((UnityEngine.AudioSource)o).Pause(); return 0; }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to call Pause on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int UnPause(IntPtr L)
    {
        object o = null;
        try { o = ToLua.CheckObject<UnityEngine.AudioSource>(L, 1); ((UnityEngine.AudioSource)o).UnPause(); return 0; }
        catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to call UnPause on a nil value"); }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_volume(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); LuaDLL.lua_pushnumber(L, ((UnityEngine.AudioSource)o).volume); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index volume on a nil value"); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_volume(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ((UnityEngine.AudioSource)o).volume = (float)LuaDLL.luaL_checknumber(L, 2); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index volume on a nil value"); } }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_isPlaying(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); LuaDLL.lua_pushboolean(L, ((UnityEngine.AudioSource)o).isPlaying); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index isPlaying on a nil value"); } }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_mute(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); LuaDLL.lua_pushboolean(L, ((UnityEngine.AudioSource)o).mute); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index mute on a nil value"); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_mute(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ((UnityEngine.AudioSource)o).mute = LuaDLL.luaL_checkboolean(L, 2); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index mute on a nil value"); } }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_loop(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); LuaDLL.lua_pushboolean(L, ((UnityEngine.AudioSource)o).loop); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index loop on a nil value"); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_loop(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ((UnityEngine.AudioSource)o).loop = LuaDLL.luaL_checkboolean(L, 2); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index loop on a nil value"); } }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_pitch(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); LuaDLL.lua_pushnumber(L, ((UnityEngine.AudioSource)o).pitch); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index pitch on a nil value"); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_pitch(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ((UnityEngine.AudioSource)o).pitch = (float)LuaDLL.luaL_checknumber(L, 2); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index pitch on a nil value"); } }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_time(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); LuaDLL.lua_pushnumber(L, ((UnityEngine.AudioSource)o).time); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index time on a nil value"); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_time(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ((UnityEngine.AudioSource)o).time = (float)LuaDLL.luaL_checknumber(L, 2); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index time on a nil value"); } }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_clip(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ToLua.Push(L, ((UnityEngine.AudioSource)o).clip); return 1; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index clip on a nil value"); } }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_clip(IntPtr L) { object o = null; try { o = ToLua.ToObject(L, 1); ((UnityEngine.AudioSource)o).clip = (UnityEngine.AudioClip)ToLua.CheckObject<UnityEngine.AudioClip>(L, 2); return 0; } catch (Exception e) { return LuaDLL.toluaL_exception(L, e, o, "attempt to index clip on a nil value"); } }
}
