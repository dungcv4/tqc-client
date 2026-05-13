// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18FBE6C, 0x18FBF74, 0x18FBF7C, 0x18FBF84, 0x18FBF8C, 0x18FBF94, 0x18FC024,
//       0x18FC094, 0x18FC19C, 0x18FC2C0, 0x18FC49C, 0x18FC650, 0x18FC730, 0x18FBF1C,
//       0x18FC8E4
// Ghidra dir: work/06_ghidra/decompiled_full/NetReceiverLua/

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
using LuaFramework;

// Source: Il2CppDumper-decompiled  TypeDefIndex: 802
public class NetReceiverLua
{
    protected static NetReceiverLua _instance;
    private string _LuaClass;                            // 0x10
    private object _LuaClassObj;                         // 0x18
    private static Dictionary<int, string> CallBackMapping;

    // RVA: 0x18FBE6C  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/get_Instance.c
    public static NetReceiverLua Instance { get { if (_instance == null)
        {
            _instance = new NetReceiverLua();
        }
        return _instance; } }

    // RVA: 0x18FBF74  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/get_LuaClass.c
    public string LuaClass { get { return this._LuaClass; } set { this._LuaClass = value; } }

    // RVA: 0x18FBF7C  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/set_LuaClass.c
    
    // RVA: 0x18FBF84  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/get_LuaClassObj.c
    public object LuaClassObj { get { return this._LuaClassObj; } set { this._LuaClassObj = value; } }

    // RVA: 0x18FBF8C  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/set_LuaClassObj.c
    
    // RVA: 0x18FBF94  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/SetCbNetReceiverLua.c
    public static void SetCbNetReceiverLua(string sLuaClass, object NetReceiverObj)
    {
        NetReceiverLua inst = NetReceiverLua.Instance;
        if (inst == null)
        {
            throw new NullReferenceException();
        }
        inst._LuaClass = sLuaClass;
        NetReceiverLua inst2 = NetReceiverLua.Instance;
        if (inst2 == null)
        {
            throw new NullReferenceException();
        }
        inst2._LuaClassObj = NetReceiverObj;
        NetReceiverLua inst3 = NetReceiverLua.Instance;
        if (inst3 == null)
        {
            throw new NullReferenceException();
        }
        inst3.CallInit();
    }

    // RVA: 0x18FC024  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/CallInit.c
    public void CallInit()
    {
        Messenger<int, object>.ClearAllCBListener();
        this.OnInit();
    }

    // RVA: 0x18FC094  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/OnInit.c
    protected void OnInit()
    {
        if (CallBackMapping == null)
        {
            throw new NullReferenceException();
        }
        CallBackMapping.Clear();
        LuaFramework.Util.CallMethod(this._LuaClass, "OnInit", new object[0]);
    }

    // RVA: 0x18FC19C  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/SetNetCallbackFuncLua.c
    public static void SetNetCallbackFuncLua(int protocol, string cbFun)
    {
        if (CallBackMapping == null)
        {
            throw new NullReferenceException();
        }
        CallBackMapping[protocol] = cbFun;
        // Source: Ghidra work/06_ghidra/decompiled_full/NetReceiverLua/SetNetCallbackFuncLua.c
        // IL: Messenger<int,object>.AddListener(param_1, Callback<int,object>{OnNetCallbackFunc(byte[])})
        // Pro v1 ported with System.Action (wrong type). Real type = Callback<int, object>.
        // Fixed per dump.cs: AddListener(int eventType, Callback<T,U> handler) + cast lambda explicit.
        Callback<int, object> cb = (p, d) => NetReceiverLua.OnNetCallbackFunc(p, (byte[])d);
        Messenger<int, object>.AddListener(protocol, cb);
    }

    // Source: Ghidra dump_by_rva → decompiled_rva/NetReceiverLua__OnNetCallbackFunc_byteArray.c
    // RVA: 0x18FC2C0
    // 1. If CallBackMapping null or !ContainsKey(protocol): return.
    // 2. packer = new NetReceivePackerBase(data) — wraps byte[] into stream reader.
    // 3. inst = NetReceiverLua.Instance; args = new object[2] { inst._LuaClassObj, packer };
    //    LuaFramework.Util.CallMethod(inst._LuaClass, CallBackMapping[protocol], args).
    public static void OnNetCallbackFunc(int protocol, byte[] data)
    {
        if (CallBackMapping == null) throw new NullReferenceException();
        if (!CallBackMapping.ContainsKey(protocol)) return;
        NetReceivePackerBase packer = new NetReceivePackerBase(data);
        NetReceiverLua inst = NetReceiverLua.Instance;
        if (inst == null || CallBackMapping == null) throw new NullReferenceException();
        string luaClass = inst._LuaClass;
        string funcName = CallBackMapping[protocol];
        object[] args = new object[2];
        NetReceiverLua inst2 = NetReceiverLua.Instance;
        if (inst2 == null) throw new NullReferenceException();
        args[0] = inst2._LuaClassObj;
        args[1] = packer;
        LuaFramework.Util.CallMethod(luaClass, funcName, args);
    }

    // RVA: 0x18FC49C  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/OnNetCallbackFunc.c
    public static void OnNetCallbackFunc(int protocol, NetReceivePackerBase netData)
    {
        if (CallBackMapping == null)
        {
            throw new NullReferenceException();
        }
        if (!CallBackMapping.ContainsKey(protocol))
        {
            return;
        }
        NetReceiverLua inst = NetReceiverLua.Instance;
        if (inst == null || CallBackMapping == null)
        {
            throw new NullReferenceException();
        }
        string luaClass = inst._LuaClass;
        string funcName = CallBackMapping[protocol];
        object[] args = new object[2];
        NetReceiverLua inst2 = NetReceiverLua.Instance;
        if (inst2 == null || args == null)
        {
            throw new NullReferenceException();
        }
        object luaClassObj = inst2._LuaClassObj;
        if (luaClassObj == null)
        {
            throw new InvalidCastException();
        }
        if (args.Length != 0)
        {
            args[0] = luaClassObj;
            if (netData == null)
            {
                throw new InvalidCastException();
            }
            if (1 < args.Length)
            {
                args[1] = netData;
                LuaFramework.Util.CallMethod(luaClass, funcName, args);
                return;
            }
        }
        throw new IndexOutOfRangeException();
    }

    // RVA: 0x18FC650  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/OnServerConnected.c
    public static void OnServerConnected()
    {
        NetReceiverLua inst = NetReceiverLua.Instance;
        if (inst == null)
        {
            throw new NullReferenceException();
        }
        LuaFramework.Util.CallMethod(inst._LuaClass, "OnServerConnected", new object[0]);
    }

    // RVA: 0x18FC730  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/OnServerDisconnect.c
    public static void OnServerDisconnect(int ProxyID, int nReason)
    {
        NetReceiverLua inst = NetReceiverLua.Instance;
        if (inst == null)
        {
            throw new NullReferenceException();
        }
        string luaClass = inst._LuaClass;
        object[] args = new object[3];
        NetReceiverLua inst2 = NetReceiverLua.Instance;
        if (inst2 == null || args == null)
        {
            throw new NullReferenceException();
        }
        object luaClassObj = inst2._LuaClassObj;
        if (luaClassObj == null)
        {
            throw new InvalidCastException();
        }
        if (args.Length != 0)
        {
            args[0] = luaClassObj;
            object boxed1 = (object)ProxyID;
            if (boxed1 == null)
            {
                throw new InvalidCastException();
            }
            if (1 < args.Length)
            {
                args[1] = boxed1;
                object boxed2 = (object)nReason;
                if (boxed2 == null)
                {
                    throw new InvalidCastException();
                }
                if (2 < args.Length)
                {
                    args[2] = boxed2;
                    LuaFramework.Util.CallMethod(luaClass, "OnServerDisconnect", args);
                    return;
                }
            }
        }
        throw new IndexOutOfRangeException();
    }

    // RVA: 0x18FBF1C  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/.ctor.c
    public NetReceiverLua()
    {
        this._LuaClass = string.Empty;
    }

    // RVA: 0x18FC8E4  Ghidra: work/06_ghidra/decompiled_full/NetReceiverLua/.cctor.c
    static NetReceiverLua()
    {
        _instance = null;
        CallBackMapping = new Dictionary<int, string>();
    }

}
