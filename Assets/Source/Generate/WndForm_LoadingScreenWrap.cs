// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LoadingScreenWrap/*.c
// Ported 1-1 from libil2cpp.so RVAs:
//   Register             0x01870638  (2 RegFunction + 5 RegVar — see body)
//   SetBarText           0x0186FD14
//   get_Instance         0x0186FF78
//   get_showLoading      0x018700B0
//   set_PicIndex         0x018701C0
//   set_showLoading      0x018702E8
//   set_fProgress        0x018703FC
//   set_CharacterLevel   0x01870510
//
// Note: previously the file held many extra wraps for [NoToLua] members
// (set_LoadingFlag, set_ShowLoadingPercent, SetAdultImg, _onClickChangeTip,
// GetPrefab, IsPrefabInResource, setBarEnable, DownLoadCheck_Click,
// ShowDownloadCheck, ShowConfirmCheck, VersionCheck_Click, SetBundleCreateTime,
// _CreateWndForm_LoadingScreen, get_ready, get_Completed, get_fProgress).
// None of those are in the binary's Register table — removed to match 1-1.
using System;
using LuaInterface;

public class WndForm_LoadingScreenWrap
{
	// Source: Ghidra Register.c RVA 0x01870638
	// 1-1 sequence (verified via stringliteral.json indices 9973, 13618, 6609, 9091, 19869, 16610, 4023):
	//   BeginClass(typeof(WndForm_LoadingScreen), typeof(WndForm))
	//   RegFunction("SetBarText",       SetBarText)
	//   RegFunction("__tostring",       ToLua.op_ToString)
	//   RegVar     ("Instance",         get_Instance,    null)
	//   RegVar     ("PicIndex",         null,            set_PicIndex)
	//   RegVar     ("showLoading",      get_showLoading, set_showLoading)
	//   RegVar     ("fProgress",        null,            set_fProgress)
	//   RegVar     ("CharacterLevel",   null,            set_CharacterLevel)
	//   EndClass()
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(WndForm_LoadingScreen), typeof(WndForm));
		L.RegFunction("SetBarText", SetBarText);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.RegVar("PicIndex", null, set_PicIndex);
		L.RegVar("showLoading", get_showLoading, set_showLoading);
		L.RegVar("fProgress", null, set_fProgress);
		L.RegVar("CharacterLevel", null, set_CharacterLevel);
		L.EndClass();
	}

	// Source: Ghidra SetBarText.c RVA 0x0186FD14
	// 1-1: lua_gettop ∈ {1,2}; CheckString(L,1) [+ CheckString(L,2)] → WndForm_LoadingScreen.SetBarText(...);
	//      lua_pushboolean(result); else luaL_throw "invalid arguments…"
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetBarText(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				string arg0 = ToLua.CheckString(L, 1);
				bool o = WndForm_LoadingScreen.SetBarText(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2)
			{
				string arg0 = ToLua.CheckString(L, 1);
				string arg1 = ToLua.CheckString(L, 2);
				bool o = WndForm_LoadingScreen.SetBarText(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: WndForm_LoadingScreen.SetBarText");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	// Source: Ghidra get_Instance.c RVA 0x0186FF78
	// 1-1: ToLua.PushObject(L, WndForm_LoadingScreen.Instance); return 1;
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, WndForm_LoadingScreen.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	// Source: Ghidra get_showLoading.c RVA 0x018700B0
	// 1-1: lua_pushboolean(L, WndForm_LoadingScreen.get_showLoading()); return 1;
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showLoading(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushboolean(L, WndForm_LoadingScreen.showLoading);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	// Source: Ghidra set_PicIndex.c RVA 0x018701C0
	// 1-1: dArg = luaL_checknumber(L, 2);
	//      iArg = (dArg != +INFINITY) ? (int)dArg : int.MinValue;   // explicit INFINITY guard
	//      WndForm_LoadingScreen.set_PicIndex(iArg);
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PicIndex(IntPtr L)
	{
		try
		{
			double dArg = LuaDLL.luaL_checknumber(L, 2);
			int arg0 = (dArg != double.PositiveInfinity) ? (int)dArg : int.MinValue;
			WndForm_LoadingScreen.set_PicIndex(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	// Source: Ghidra set_showLoading.c RVA 0x018702E8
	// 1-1: bArg = luaL_checkboolean(L, 2) & 1; WndForm_LoadingScreen.set_showLoading(bArg);
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showLoading(IntPtr L)
	{
		try
		{
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			WndForm_LoadingScreen.showLoading = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	// Source: Ghidra set_fProgress.c RVA 0x018703FC
	// 1-1: dArg = luaL_checknumber(L, 2); WndForm_LoadingScreen.set_fProgress((float)dArg);
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fProgress(IntPtr L)
	{
		try
		{
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			WndForm_LoadingScreen.fProgress = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	// Source: Ghidra set_CharacterLevel.c RVA 0x01870510
	// 1-1: dArg = luaL_checknumber(L, 2);
	//      iArg = (dArg != +INFINITY) ? (int)dArg : int.MinValue;
	//      WndForm_LoadingScreen.set_CharacterLevel(iArg);
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CharacterLevel(IntPtr L)
	{
		try
		{
			double dArg = LuaDLL.luaL_checknumber(L, 2);
			int arg0 = (dArg != double.PositiveInfinity) ? (int)dArg : int.MinValue;
			WndForm_LoadingScreen.set_CharacterLevel(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
