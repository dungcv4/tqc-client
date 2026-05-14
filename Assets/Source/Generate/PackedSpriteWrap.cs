// Source: work/06_ghidra/decompiled_full/PackedSpriteWrap/*.c + work/03_il2cpp_dump/dump.cs
// String-literal indices verified against work/03_il2cpp_dump/stringliteral.json:
//   5799=GetDefaultPixelSize 10450=Start 4297=Copy 6571=InitUVs 2833=AddAnimation
//   2886=Aggregate 4422=Create 13568=__eq 13618=__tostring
//   20087=staticTexPath 20086=staticTexGUID 13664=_ser_stat_frame_info
//   20084=staticFrameInfo 20396=textureAnimations 10481=States
//   4710=DefaultFrame 4714=DefaultState 10588=SupportsArbitraryAnimations
using System;
using LuaInterface;
using UnityEngine;

public class PackedSpriteWrap
{
	// Source: Ghidra Register.c RVA 0x01AD95BC
	// 1-1: BeginClass(PackedSprite, AutoSpriteBase); register 9 functions + 6 read/write vars +
	//      3 read-only vars; EndClass.
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(PackedSprite), typeof(AutoSpriteBase));
		L.RegFunction("GetDefaultPixelSize", GetDefaultPixelSize);
		L.RegFunction("Start", Start);
		L.RegFunction("Copy", Copy);
		L.RegFunction("InitUVs", InitUVs);
		L.RegFunction("AddAnimation", AddAnimation);
		L.RegFunction("Aggregate", Aggregate);
		L.RegFunction("Create", Create);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("staticTexPath",        get_staticTexPath,        set_staticTexPath);
		L.RegVar("staticTexGUID",        get_staticTexGUID,        set_staticTexGUID);
		L.RegVar("_ser_stat_frame_info", get__ser_stat_frame_info, set__ser_stat_frame_info);
		L.RegVar("staticFrameInfo",      get_staticFrameInfo,      set_staticFrameInfo);
		L.RegVar("textureAnimations",    get_textureAnimations,    set_textureAnimations);
		L.RegVar("States",               get_States,               set_States);
		L.RegVar("DefaultFrame",                 get_DefaultFrame,                 null);
		L.RegVar("DefaultState",                 get_DefaultState,                 null);
		L.RegVar("SupportsArbitraryAnimations",  get_SupportsArbitraryAnimations,  null);
		L.EndClass();
	}

	// Source: Ghidra GetDefaultPixelSize.c RVA 0x01AD6A90
	// 1-1: Vector2 PackedSprite.GetDefaultPixelSize(PathFromGUIDDelegate, AssetLoaderDelegate).
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDefaultPixelSize(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1);
			PathFromGUIDDelegate arg0 = (PathFromGUIDDelegate)ToLua.CheckDelegate<PathFromGUIDDelegate>(L, 2);
			AssetLoaderDelegate  arg1 = (AssetLoaderDelegate)ToLua.CheckDelegate<AssetLoaderDelegate>(L, 3);
			Vector2 o = obj.GetDefaultPixelSize(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra Start.c RVA 0x01AD6CF4
	// 1-1: PackedSprite.Start() — virtual override.
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Start(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1);
			obj.Start();
			return 0;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra Copy.c RVA 0x01AD6E78
	// 1-1: PackedSprite.Copy(SpriteRoot s).
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Copy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1);
			SpriteRoot   arg0 = (SpriteRoot)ToLua.CheckUnityObject(L, 2, typeof(SpriteRoot));
			obj.Copy(arg0);
			return 0;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra InitUVs.c RVA 0x01AD7088
	// 1-1: PackedSprite.InitUVs() — frameInfo = staticFrameInfo; uvRect = staticFrameInfo.uvs.
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitUVs(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1);
			obj.InitUVs();
			return 0;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra AddAnimation.c RVA 0x01AD720C
	// 1-1: PackedSprite.AddAnimation(UVAnimation anim) — grow animations[].
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddAnimation(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1);
			UVAnimation  arg0 = (UVAnimation)ToLua.CheckObject<UVAnimation>(L, 2);
			obj.AddAnimation(arg0);
			return 0;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra Aggregate.c RVA 0x01AD7418
	// 1-1: PackedSprite.Aggregate(PathFromGUIDDelegate, LoadAssetDelegate, GUIDFromPathDelegate).
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Aggregate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1);
			PathFromGUIDDelegate arg0 = (PathFromGUIDDelegate)ToLua.CheckDelegate<PathFromGUIDDelegate>(L, 2);
			LoadAssetDelegate    arg1 = (LoadAssetDelegate)ToLua.CheckDelegate<LoadAssetDelegate>(L, 3);
			GUIDFromPathDelegate arg2 = (GUIDFromPathDelegate)ToLua.CheckDelegate<GUIDFromPathDelegate>(L, 4);
			obj.Aggregate(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra Create.c RVA 0x01AD76D4
	// 1-1: static factory — Create(string name, Vector3 pos) or Create(string, Vector3, Quaternion).
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Create(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			if (count == 2)
			{
				string  arg0 = ToLua.CheckString(L, 1);
				Vector3 arg1 = ToLua.ToVector3(L, 2);
				PackedSprite o = PackedSprite.Create(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3)
			{
				string     arg0 = ToLua.CheckString(L, 1);
				Vector3    arg1 = ToLua.ToVector3(L, 2);
				Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				PackedSprite o = PackedSprite.Create(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: PackedSprite.Create");
			}
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	// Source: Ghidra op_Equality.c RVA 0x01AD7974
	// 1-1: Unity-Object equality (op_Equality on UnityEngine.Object).
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_staticTexPath(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); LuaDLL.lua_pushstring(L, obj.staticTexPath); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_staticTexGUID(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); LuaDLL.lua_pushstring(L, obj.staticTexGUID); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__ser_stat_frame_info(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); ToLua.PushObject(L, obj._ser_stat_frame_info); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_staticFrameInfo(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); ToLua.PushValue(L, obj.staticFrameInfo); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_textureAnimations(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); ToLua.Push(L, obj.textureAnimations); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_States(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); ToLua.Push(L, obj.States); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DefaultFrame(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); ToLua.PushObject(L, obj.DefaultFrame); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DefaultState(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); ToLua.PushObject(L, obj.DefaultState); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SupportsArbitraryAnimations(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); LuaDLL.lua_pushboolean(L, obj.SupportsArbitraryAnimations); return 1; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_staticTexPath(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); obj.staticTexPath = ToLua.CheckString(L, 2); return 0; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_staticTexGUID(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); obj.staticTexGUID = ToLua.CheckString(L, 2); return 0; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__ser_stat_frame_info(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); obj._ser_stat_frame_info = (CSpriteFrame)ToLua.CheckObject<CSpriteFrame>(L, 2); return 0; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_staticFrameInfo(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); obj.staticFrameInfo = (SPRITE_FRAME)ToLua.CheckObject(L, 2, typeof(SPRITE_FRAME)); return 0; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_textureAnimations(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); obj.textureAnimations = ToLua.CheckObjectArray<TextureAnim>(L, 2); return 0; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_States(IntPtr L)
	{
		try { PackedSprite obj = (PackedSprite)ToLua.CheckObject<PackedSprite>(L, 1); obj.States = ToLua.CheckObjectArray<TextureAnim>(L, 2); return 0; }
		catch (Exception e) { return LuaDLL.toluaL_exception(L, e); }
	}
}
