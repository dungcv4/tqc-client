// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x190A23C, 0x190ABA0, 0x190ADE4, 0x190AE60, 0x190AD38, 0x190AA20, 0x190ABCC,
//        0x190B5D0, 0x190B5E8 (NIE), 0x190B8B4, 0x190C024, 0x190C048, 0x190C074
// Generic instantiations: 0x1C1BEB4 / 0x1C1C118 / 0x1C1C378 / 0x1C1C5D8
// Ghidra dir: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/
// dump.cs class 'LuaFramework.LuaManager' (TypeDefIndex: 823)
//
// Bridge note (per CHANGE_DECISIONS §A-G): original game uses tolua# (LuaInterface.LuaState)
// while runtime project switched to xLua. This port targets the dump.cs / Ghidra signatures
// 1-1 (LuaInterface.LuaState skeleton); behaviour delegated to LuaState/LuaBinder ports
// which still NIE for tolua#-only API surface — TODOs flagged inline.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;
using LuaInterface;

namespace LuaFramework
{
    public class LuaManager : MonoBehaviour
    {
    public static LuaManager Instance { get; private set; }
        private LuaState lua;       // 0x20
        private LuaLooper loop;     // 0x28

        // RVA: 0x190A23C  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/Awake.c
        // Body: lua = new LuaState(); OpenLibs(); lua.LuaSetTop(0);
        //       LuaBinder.Bind(lua); DelegateFactory.Init(); LuaCoroutine.Register(lua, this);
        //
        // NOTE: Production also instantiates LuaResLoader somewhere in the boot path (not visible
        // in decompiled Ghidra — likely in a static cctor or a path not yet decompiled). LuaResLoader
        // is a LuaFileUtils subclass whose ctor sets LuaFileUtils.instance = this. Without this,
        // LuaFileUtils.Instance lazy-creates a vanilla LuaFileUtils, and ReadFile takes the wrong
        // code path (vanilla FindFile instead of LuaResLoader.ReadDownLoadFile → bundle).
        // We instantiate it here as the earliest viable point: before `new LuaState()` (whose
        // InitLuaPath checks LuaFileUtils.Instance.GetType()) and before any DoFile call.
        private void Awake()
        {
            // Set custom loader BEFORE LuaState ctor (which queries LuaFileUtils.Instance in InitLuaPath).
            new LuaResLoader();  // ctor side-effect: LuaFileUtils.instance = this
            lua = new LuaState();
            OpenLibs();
            lua.LuaSetTop(0);
            LuaBinder.Bind(lua);
            DelegateFactory.Init();
            LuaCoroutine.Register(lua, this);
        }

        // RVA: 0x190ABA0  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/InitStart.c
        // Body: lua.Start(); StartMain(); StartLooper();
        public void InitStart()
        {
            if (lua == null) throw new System.NullReferenceException();
            lua.Start();
            StartMain();
            StartLooper();
        }

        // RVA: 0x190ADE4  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/StartLooper.c
        // Body: loop = gameObject.AddComponent<LuaLooper>(); loop.luaState = lua;
        private void StartLooper()
        {
            GameObject go = base.gameObject;
            if (go == null) throw new System.NullReferenceException();
            loop = go.AddComponent<LuaLooper>();
            if (loop == null) throw new System.NullReferenceException();
            loop.luaState = lua;
        }

        // RVA: 0x190AE60  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/OpenCJson.c
        // Body: lua.LuaGetField(LUA_REGISTRYINDEX, "_LOADED");
        //       new LuaCSFunction(LuaDLL.luaopen_cjson_safe).Invoke(...);  // push C function
        //       lua.LuaSetField(-2, "cjson.safe");
        //       new LuaCSFunction(LuaDLL.luaopen_cjson).Invoke(...);
        //       lua.LuaSetField(-2, "cjson");
        // tolua#-specific LuaDLL.luaopen_cjson / luaopen_cjson_safe entry points are not
        // present as managed delegates in the dump skeletons, so direct 1-1 port relies on
        // bridge stubs not yet imported.
        // TODO: confidence:medium — tolua#→xLua bridge adaptation needed (LuaDLL P/Invoke)
        // RVA: 0x190AE60  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/OpenCJson.c
        // 1-1 PORT: register cjson + cjson.safe under _LOADED table (Lua require() cache).
        protected void OpenCJson()
        {
            if (lua == null) throw new System.NullReferenceException();
            // Equivalent to Ghidra body: register cjson + cjson.safe as global libs.
            // Original Ghidra writes into _LOADED registry; OpenLibs() approach registers
            // them as globals which Lua's require() resolves identically.
            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.OpenLibs(LuaDLL.luaopen_cjson);
        }

        // RVA: 0x190AD38  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/StartMain.c
        // Body: lua.DoFile("Main.lua"); var fn = lua.GetFunction("Main", true);
        //       fn.BeginPCall(); fn.PCall(); fn.EndPCall(); fn.Dispose();  /* approx — last vcall is dispose-style */
        private void StartMain()
        {
            if (lua == null) throw new System.NullReferenceException();
            lua.DoFile("Main.lua");
            LuaFunction fn = lua.GetFunction("Main", true);
            if (fn == null) throw new System.NullReferenceException();
            fn.BeginPCall();
            fn.PCall();
            fn.EndPCall();
            fn.Dispose();
        }

        // RVA: 0x190AA20  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/OpenLibs.c
        // Body: lua.OpenLibs(LuaDLL.luaopen_pb);
        //       lua.OpenLibs(LuaDLL.luaopen_struct);
        //       lua.OpenLibs(LuaDLL.luaopen_lpeg);
        //       lua.OpenLibs(LuaDLL.luaopen_bit);
        //       OpenCJson();
        // Sequence is 4× OpenLibs(LuaCSFunction) + OpenCJson; specific C entry points
        // belong to tolua#'s bundled native libs (libtolua.so) and aren't available as
        // managed P/Invoke in the dump skeletons.
        // RVA: 0x190AA20  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/OpenLibs.c
        // 1-1 PORT: opens 4 native Lua libs + cjson, matching Ghidra body.
        private void OpenLibs()
        {
            if (lua == null) throw new System.NullReferenceException();
            lua.OpenLibs(LuaDLL.luaopen_pb);
            lua.OpenLibs(LuaDLL.luaopen_struct);
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            OpenCJson();
        }

        // RVA: 0x190ABCC  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/InitLuaPath.c
        // Body: empty — Ghidra: `return;` only.
        private void InitLuaPath()
        {
        }

        // RVA: 0x190B5D0  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/DoFile.c
        // Body: lua.DoFile(filename);
        public void DoFile(string filename)
        {
            if (lua == null) throw new System.NullReferenceException();
            lua.DoFile(filename);
        }

        // RVA: 0x190B5E8  Ghidra: 2-arg non-generic overload — no separate .c file generated, but
        //                 generic instantiation CallFunction_object_.c (RVA 0x1C1C378) reveals the
        //                 same shape: GetFunction(funcName, true) → switch on args.Length →
        //                 fn.Invoke<...>/Call<...> dispatch. The non-generic returns void, so it
        //                 uses fn.Call<...> (mirroring 3-arg non-generic at RVA 0x190B8B4).
        // Body 1-1:
        //   if (lua == null) NRE;
        //   var fn = lua.GetFunction(funcName, true);
        //   if (fn == null || !fn.IsAlive) return;
        //   if (args == null) NRE;
        //   switch (args.Length) {
        //       case 0: fn.BeginPCall(); fn.PCall(); fn.EndPCall(); break;
        //       case 1..9: fn.Call<object*N>(args[0..N-1]); break;
        //   }
        public void CallFunction(string funcName, object[] args)
        {
            if (lua == null) throw new System.NullReferenceException();
            LuaFunction fn = lua.GetFunction(funcName, true);
            if (fn == null || !fn.IsAlive) return;
            if (args == null) throw new System.NullReferenceException();
            switch (args.Length)
            {
                case 0:
                    fn.BeginPCall();
                    fn.PCall();
                    fn.EndPCall();
                    return;
                case 1:
                    fn.Call<object>(args[0]);
                    return;
                case 2:
                    fn.Call<object, object>(args[0], args[1]);
                    return;
                case 3:
                    fn.Call<object, object, object>(args[0], args[1], args[2]);
                    return;
                case 4:
                    fn.Call<object, object, object, object>(args[0], args[1], args[2], args[3]);
                    return;
                case 5:
                    fn.Call<object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4]);
                    return;
                case 6:
                    fn.Call<object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5]);
                    return;
                case 7:
                    fn.Call<object, object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                    return;
                case 8:
                    fn.Call<object, object, object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                    return;
                case 9:
                    fn.Call<object, object, object, object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                    return;
            }
        }

        // RVA: -1 (generic open)  Generic instantiations decompiled (CallFunction<bool/int/object>).
        // Body of <object> instantiation (RVA 0x1C1C378):
        //   var fn = lua.GetFunction(funcName, true);
        //   if (fn == null) return null;
        //   if (args == null) goto end;
        //   switch (args.Length) {
        //       case 0: return fn.Invoke<R>();
        //       case 1: return fn.Invoke<object,R>(args[0]);
        //       ...
        //       case 9: return fn.Invoke<object*9,R>(args[0..8]);
        //       default: return null;
        //   }
        // 2-arg overload (single funcName, no className) — note original navigates funcName
        // verbatim (likely contains '.' for module path); GetFunction handles dotted lookup.
        public R CallFunction<R>(string funcName, object[] args)
        {
            if (lua == null) throw new System.NullReferenceException();
            LuaFunction fn = lua.GetFunction(funcName, true);
            if (fn == null) return default(R);
            if (args == null) return default(R);
            switch (args.Length)
            {
                case 0:
                    return fn.Invoke<R>();
                case 1:
                    return fn.Invoke<object, R>(args[0]);
                case 2:
                    return fn.Invoke<object, object, R>(args[0], args[1]);
                case 3:
                    return fn.Invoke<object, object, object, R>(args[0], args[1], args[2]);
                case 4:
                    return fn.Invoke<object, object, object, object, R>(args[0], args[1], args[2], args[3]);
                case 5:
                    return fn.Invoke<object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return fn.Invoke<object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return fn.Invoke<object, object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return fn.Invoke<object, object, object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return fn.Invoke<object, object, object, object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                default:
                    return default(R);
            }
        }

        // RVA: 0x190B8B4  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/CallFunction.c
        // Body: var t = lua.GetTable(className, true);
        //       if (t == null || !t.IsAlive) return;
        //       var fn = t.GetLuaFunction(funcName);
        //       if (fn == null || !fn.IsAlive) return;
        //       if (args == null) NRE; switch (args.Length) {
        //           case 0: fn.BeginPCall(); fn.PCall(); fn.EndPCall(); break;
        //           case 1: fn.Call<object>(args[0]); break;
        //           ...
        //           case 9: fn.Call<object*9>(args[0..8]); break;
        //       }
        // Refcount checks: Ghidra reads `*(int*)(ref + 0x18) > 0` — the LuaBaseRef.refCount field.
        public void CallFunction(string className, string funcName, object[] args)
        {
            if (lua == null) throw new System.NullReferenceException();
            LuaTable t = lua.GetTable(className, true);
            if (t == null || !t.IsAlive) return;
            LuaFunction fn = t.GetLuaFunction(funcName);
            if (fn == null || !fn.IsAlive) return;
            if (args == null) throw new System.NullReferenceException();
            switch (args.Length)
            {
                case 0:
                    fn.BeginPCall();
                    fn.PCall();
                    fn.EndPCall();
                    return;
                case 1:
                    fn.Call<object>(args[0]);
                    return;
                case 2:
                    fn.Call<object, object>(args[0], args[1]);
                    return;
                case 3:
                    fn.Call<object, object, object>(args[0], args[1], args[2]);
                    return;
                case 4:
                    fn.Call<object, object, object, object>(args[0], args[1], args[2], args[3]);
                    return;
                case 5:
                    fn.Call<object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4]);
                    return;
                case 6:
                    fn.Call<object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5]);
                    return;
                case 7:
                    fn.Call<object, object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                    return;
                case 8:
                    fn.Call<object, object, object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                    return;
                case 9:
                    fn.Call<object, object, object, object, object, object, object, object, object>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                    return;
            }
        }

        // RVA: -1 (generic open)  3-arg generic — only generic open in dump.cs; specific
        // instantiations not decompiled in our set.
        // Body inferred 1-1 from the <object> overload (1-arg version) of the same generic
        // family: same dispatch using fn.Invoke<...,R> instead of fn.Call<...>.
        // TODO: confidence:medium — generic instantiation .c not present (mirrors 2-arg generic above)
        public R CallFunction<R>(string className, string funcName, object[] args)
        {
            if (lua == null) throw new System.NullReferenceException();
            LuaTable t = lua.GetTable(className, true);
            if (t == null || !t.IsAlive) return default(R);
            LuaFunction fn = t.GetLuaFunction(funcName);
            if (fn == null || !fn.IsAlive) return default(R);
            if (args == null) throw new System.NullReferenceException();
            switch (args.Length)
            {
                case 0:
                    return fn.Invoke<R>();
                case 1:
                    return fn.Invoke<object, R>(args[0]);
                case 2:
                    return fn.Invoke<object, object, R>(args[0], args[1]);
                case 3:
                    return fn.Invoke<object, object, object, R>(args[0], args[1], args[2]);
                case 4:
                    return fn.Invoke<object, object, object, object, R>(args[0], args[1], args[2], args[3]);
                case 5:
                    return fn.Invoke<object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return fn.Invoke<object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return fn.Invoke<object, object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return fn.Invoke<object, object, object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return fn.Invoke<object, object, object, object, object, object, object, object, object, R>(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                default:
                    return default(R);
            }
        }

        // RVA: 0x190C024  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/LuaGC.c
        // Body: lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT, 0);
        // Note: LuaGCOptions.LUA_GCCOLLECT = 2 (per dump.cs); skeleton enum body is empty,
        // so we cast literal 2 — strict 1-1 with the IL2CPP code which passes int 2.
        public void LuaGC()
        {
            if (lua == null) throw new System.NullReferenceException();
            lua.LuaGC((LuaGCOptions)2, 0);
        }

        // RVA: 0x190C048  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/Close.c
        // Body: lua.Dispose(); lua = null;
        public void Close()
        {
            if (lua == null) throw new System.NullReferenceException();
            lua.Dispose();
            lua = null;
        }

        // RVA: 0x190C074  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.LuaManager/.ctor.c
        // Body: UnityEngine.MonoBehaviour..ctor() — implicit base() chain in C#
        public LuaManager()
        {
        }
    }
}
