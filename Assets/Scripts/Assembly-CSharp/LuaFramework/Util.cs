// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x190C07C, 0x190C144, 0x190C19C, 0x190C1F4, 0x190C24C, 0x190C320, 0x190C378,
//        0x190C3D4, 0x190C42C, 0x190C484, 0x190C4DC, 0x190C7AC, 0x190C910, 0x190CA10
// Ghidra dir: work/06_ghidra/decompiled_full/LuaFramework.Util/
// dump.cs class 'LuaFramework.Util' (TypeDefIndex: 824)

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

namespace LuaFramework
{
    public class Util
    {
        // RVA: 0x190C07C  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/LuaPathBundle.c
        // Body: if (name.ToLower().CustomEndsWith(".lua")) name = name.Substring(0, name.LastIndexOf('.'));
        //       return string.Format("{0}.lua", name.Replace('/', '.'));
        public static string LuaPathBundle(string name)
        {
            if (name == null) throw new System.NullReferenceException();
            if (UJString.CustomEndsWith(name.ToLower(), ".lua"))
            {
                int dot = name.LastIndexOf('.');
                name = name.Substring(0, dot);
                if (name == null) throw new System.NullReferenceException();
            }
            return string.Format("{0}.lua", name.Replace('/', '.'));
        }

        // RVA: 0x190C144  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/Boolean.c
        public static bool Boolean(object o)
        {
            return System.Convert.ToBoolean(o);
        }

        // RVA: 0x190C19C  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/Byte.c
        public static byte Byte(object o)
        {
            return System.Convert.ToByte(o);
        }

        // RVA: 0x190C1F4  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/Int.c
        public static int Int(object o)
        {
            return System.Convert.ToInt32(o);
        }

        // RVA: 0x190C24C  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/Float.c
        // Body: f = Convert.ToSingle(o); return (float)Math.Round((double)f, 2);
        public static float Float(object o)
        {
            float f = System.Convert.ToSingle(o);
            return (float)System.Math.Round((double)f, 2);
        }

        // RVA: 0x190C320  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/Long.c
        public static long Long(object o)
        {
            return System.Convert.ToInt64(o);
        }

        // RVA: 0x190C378  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/get_NetAvailable.c
        // Body: return Application.internetReachability != NetworkReachability.NotReachable;
        public static bool NetAvailable { get { return UnityEngine.Application.internetReachability != UnityEngine.NetworkReachability.NotReachable; } }

        // RVA: 0x190C3D4  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/Log.c
        public static void Log(string str)
        {
            UnityEngine.Debug.Log(str);
        }

        // RVA: 0x190C42C  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/LogWarning.c
        public static void LogWarning(string str)
        {
            UnityEngine.Debug.LogWarning(str);
        }

        // RVA: 0x190C484  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/LogError.c
        public static void LogError(string str)
        {
            UnityEngine.Debug.LogError(str);
        }

        // Source: Ghidra LogException.c  RVA 0x190C4DC
        // 1-1: Debug.LogException(new LuaInterface.LuaException(str)).
        public static void LogException(string str)
        {
            UnityEngine.Debug.LogException(new LuaInterface.LuaException(str));
        }

        // RVA: 0x190C7AC  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/CallMethod.c
        // Body: var mgr = Main.LuaMgr; if (((object)mgr == (object)null)) return;
        //       var sb = new StringBuilder(); sb.AppendFormat("{0}.{1}", module, func);
        //       mgr.CallFunction(sb.ToString(), args);
        // Note: Main.LuaMgr is read from Main static field at offset 0x38 (matches WndForm/Main port).
        public static void CallMethod(string module, string func, object[] args)
        {
            LuaManager mgr = global::Main.LuaMgr;
            if (((UnityEngine.Object)mgr == (UnityEngine.Object)null)) return;
            var sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0}.{1}", module, func);
            mgr.CallFunction(sb.ToString(), args);
        }

        // RVA: 0x1C7968C (generic instantiation CallMethod<object>)  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/CallMethod<object>.c
        // Body: same as CallMethod but uses generic CallFunction<R> and returns its result.
        public static R CallMethod<R>(string module, string func, object[] args)
        {
            LuaManager mgr = global::Main.LuaMgr;
            if (((UnityEngine.Object)mgr == (UnityEngine.Object)null)) return default(R);
            var sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0}.{1}", module, func);
            return mgr.CallFunction<R>(sb.ToString(), args);
        }

        // RVA: 0x190C910  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/CallMethod2.c
        // Body: var mgr = Main.LuaMgr; if (((object)mgr == (object)null)) return;
        //       mgr.CallFunction(module, func, args);
        public static void CallMethod2(string module, string func, object[] args)
        {
            LuaManager mgr = global::Main.LuaMgr;
            if (((UnityEngine.Object)mgr == (UnityEngine.Object)null)) return;
            mgr.CallFunction(module, func, args);
        }

        // RVA: generic instantiations  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/CallMethod2<*>.c
        // Body: same as CallMethod2 but uses generic CallFunction<R>.
        public static R CallMethod2<R>(string module, string func, object[] args)
        {
            LuaManager mgr = global::Main.LuaMgr;
            if (((UnityEngine.Object)mgr == (UnityEngine.Object)null)) return default(R);
            return mgr.CallFunction<R>(module, func, args);
        }

        // RVA: 0x190CA10  Ghidra: work/06_ghidra/decompiled_full/LuaFramework.Util/.ctor.c
        // Body: System.Object..ctor() — implicit base() chain in C#
        public Util()
        {
        }
    }
}
