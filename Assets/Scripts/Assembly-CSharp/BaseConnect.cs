// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18F9A68, 0x18F9D64, 0x18F9E30, 0x18F9E5C, 0x18F9E88, 0x18FA1A4, 0x18FA218,
//       0x18FA240, 0x18FA274, 0x18FA278, 0x18FA2A4, 0x18FA2D0, 0x18FA470, 0x18FA5B0,
//       0x18FA684, 0x18FAA44, 0x18FAB90
// Ghidra dir: work/06_ghidra/decompiled_full/BaseConnect/

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
using SONETWORK;

// Source: Il2CppDumper-stub  TypeDefIndex: 798
public class BaseConnect : MonoBehaviour, ConnectProxy.IConnectProxyCB
{
    private static BaseConnect instance;        // 0x18 (static-field area)
    private ConnectProxy proxyLogin;            // 0x20
    private ConnectProxy proxyMap;              // 0x28
    private ConnectProxy activeProxy;           // 0x30
    public const int ID_LOGIN = 0;
    public const int ID_MAP = 1;
    private bool isConnect;                     // 0x38
    private Callback onConnectCBFunc;           // 0x40
    public static bool isShuttingDown;
    public bool ignoreOneMessage;               // 0x48

    // RVA: 0x18F9A68  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/get_Instance.c
    public static BaseConnect Instance { get { if (instance == null)
        {
            instance = (BaseConnect)UnityEngine.Object.FindObjectOfType(typeof(BaseConnect));
            if (instance == null)
            {
                GameObject go = new GameObject("BaseConnect");
                instance = (BaseConnect)go.AddComponent(typeof(BaseConnect));
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
        }
        return instance; } }

    // RVA: 0x18F9D64  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/Start.c
    private void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
        GameObject go = this.gameObject;
        if (go == null)
        {
            // FUN_015cb8fc — il2cpp null-check trap
            throw new NullReferenceException();
        }
        instance = go.GetComponent<BaseConnect>();
    }

    // RVA: 0x18F9E30  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/isMapConnected.c
    public bool isMapConnected()
    {
        if (this.activeProxy != null && this.activeProxy == this.proxyMap)
        {
            return this.isConnect;
        }
        return false;
    }

    // RVA: 0x18F9E5C  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/isLoginConnected.c
    public bool isLoginConnected()
    {
        if (this.activeProxy != null && this.activeProxy == this.proxyLogin)
        {
            return this.isConnect;
        }
        return false;
    }

    // RVA: 0x18F9E88  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/startConnect.c
    public bool startConnect(int connectTypeID, string IPAddress, int Port, Callback cbFunc)
    {
        uint local_48 = 0;
        if (connectTypeID == 0)
        {
            UJDebug.Log("Login Server: " + IPAddress + ":" + Port.ToString());
        }
        else if (connectTypeID == 1)
        {
            UJDebug.Log("Map Server: " + IPAddress + ":" + Port.ToString());
        }
        if (this.proxyLogin != null)
        {
            this.proxyLogin.close();
        }
        if (this.proxyMap != null)
        {
            this.proxyMap.close();
        }
        if (connectTypeID == 0)
        {
            this.proxyLogin = TcpNetWork.createConnection(IPAddress, Port, true, 3.0f);
            if (this.proxyLogin == null)
            {
                UJDebug.Log("Login server connect failed");
                this.isConnect = false;
                return false;
            }
            this.proxyLogin.callback(this);
            this.activeProxy = this.proxyLogin;
            this.isConnect = true;
        }
        else if (connectTypeID == 1)
        {
            this.proxyMap = TcpNetWork.createConnection(IPAddress, Port, true, 3.0f);
            if (this.proxyMap == null)
            {
                UJDebug.Log("Map server connect failed");
                this.isConnect = false;
                return false;
            }
            this.proxyMap.callback(this);
            this.activeProxy = this.proxyMap;
            this.isConnect = true;
        }
        if (this.activeProxy != null)
        {
            local_48 = this.activeProxy.proxyID();
            UJDebug.Log("Connect proxyID: " + local_48.ToString());
            this.onConnectCBFunc = cbFunc;
            return true;
        }
        // FUN_015cb8fc — fall-through trap when connectTypeID is neither 0 nor 1
        throw new NullReferenceException();
    }

    // RVA: 0x18FA1A4  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/Update.c
    private void Update()
    {
        float dt = Time.deltaTime;
        if (0.0f < dt)
        {
            TcpNetWork.run(dt);
        }
    }

    // RVA: 0x18FA218  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/closeConnect.c
    public void closeConnect(int connectTypeID)
    {
        ConnectProxy proxy;
        if (connectTypeID == 1)
        {
            proxy = this.proxyMap;
        }
        else if (connectTypeID == 0)
        {
            proxy = this.proxyLogin;
        }
        else
        {
            return;
        }
        if (proxy == null)
        {
            return;
        }
        proxy.close();
    }

    // RVA: 0x18FA240  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/closeConnectAll.c
    public void closeConnectAll()
    {
        if (this.proxyLogin != null)
        {
            this.proxyLogin.close();
        }
        if (this.proxyMap != null)
        {
            this.proxyMap.close();
        }
    }

    // RVA: 0x18FA274  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/SendMessageToServer.c
    public bool SendMessageToServer(int eventID, object obj)
    {
        return SendMessageToServer(eventID, obj, 0u);
    }

    // RVA: 0x18FA278  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/SendMessageToServer.c
    public bool SendMessageToServer(int eventID, object obj, uint UID)
    {
        // TODO: confidence:medium — overloaded methods share .c file (object overloads)
        // Underlying call funnels to ConnectProxy.write(eventID, obj, UID).
        if (this.activeProxy == null)
        {
            throw new NullReferenceException();
        }
        int rc = this.activeProxy.write((ushort)eventID, obj, UID);
        return rc == 0;
    }

    // RVA: 0x18FA2A4  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/SendMessageToServer.c
    public bool SendMessageToServer(int eventID, byte[] bArray, uint UID = 0)
    {
        if (this.activeProxy == null)
        {
            throw new NullReferenceException();
        }
        int rc = this.activeProxy.write((ushort)eventID, bArray, UID);
        return rc == 0;
    }

    // RVA: 0x18FA2D0  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/SONETWORK.ConnectProxy.IConnectProxyCB.vOnConnected.c
    bool ConnectProxy.IConnectProxyCB.vOnConnected(ConnectProxy proxy)
    {
        if (proxy == null)
        {
            throw new NullReferenceException();
        }
        UJDebug.Log("OnConnected proxyID: " + proxy.proxyID().ToString());
        Callback cb = this.onConnectCBFunc;
        if (cb == null)
        {
            if (BaseProcLua.Instance != null)
            {
                BaseProcLua.Instance.onServerConnected();
            }
        }
        else
        {
            cb();
            this.onConnectCBFunc = null;
        }
        return true;
    }

    // RVA: 0x18FA470  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/SONETWORK.ConnectProxy.IConnectProxyCB.vOnStream.c
    int ConnectProxy.IConnectProxyCB.vOnStream(ConnectProxy proxy, ref proto_COMM header, byte[] data)
    {
        // Ghidra: Messenger<int,object>.Broadcast(protocol, protocol, data, MessengerMode)
        // The static MessengerMode at PTR_DAT_034625e8 is the default — not directly resolvable.
        // Source: Ghidra work/06_ghidra/decompiled_full/BaseConnect/SONETWORK.ConnectProxy.IConnectProxyCB.vOnStream.c
        // IL calls Messenger<int,object>.Broadcast(uVar1, uVar1, param_4, *PTR_DAT_034625e8)
        // TODO: verify MessengerMode static value at libil2cpp.so PTR_DAT_034625e8 — using enum default (0=DONT_REQUIRE_LISTENER)
        Messenger<int, object>.Broadcast((int)header.m_pcProtoco, (int)header.m_pcProtoco, data, MessengerMode.DONT_REQUIRE_LISTENER);
        return 0;
    }

    // RVA: 0x18FA5B0  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/showErrorMessage.c
    private void showErrorMessage(BaseConnect.disconnectErrorCode code)
    {
        UJDebug.LogError("Disconnect error: " + code.ToString());
    }

    // RVA: 0x18FA684  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/SONETWORK.ConnectProxy.IConnectProxyCB.vOnClosed.c
    void ConnectProxy.IConnectProxyCB.vOnClosed(ConnectProxy proxy, int nReason)
    {
        if (proxy == null)
        {
            throw new NullReferenceException();
        }
        UJDebug.LogWarning("OnClosed proxyID: " + proxy.proxyID().ToString() + " reason: " + nReason.ToString());
        ConnectProxy login = this.proxyLogin;
        if (login != null && proxy.proxyID() == login.proxyID())
        {
            UJDebug.LogWarning("Login disconnect");
            this.proxyLogin = null;
            bool showErr;
            if (this.proxyMap == null)
            {
                if (this.ignoreOneMessage)
                {
                    this.ignoreOneMessage = false;
                    showErr = true;
                }
                else
                {
                    UJDebug.Log("Disconnect: clean shutdown");
                    showErr = false;
                }
            }
            else
            {
                UJDebug.Log("Disconnect: clean shutdown");
                showErr = true;
            }
            if (BaseProcLua.Instance != null)
            {
                BaseProcLua.Instance.OnServerDisconnect(0, nReason);
            }
            if (showErr)
            {
                return;
            }
            this.showErrorMessage(BaseConnect.disconnectErrorCode.LOGIN_DISCONNECT);
            return;
        }
        if (this.proxyMap == null)
        {
            return;
        }
        if (proxy.proxyID() != this.proxyMap.proxyID())
        {
            return;
        }
        UJDebug.LogWarning("Map disconnect");
        this.proxyMap = null;
        if (BaseProcLua.Instance != null)
        {
            BaseProcLua.Instance.OnServerDisconnect(1, nReason);
        }
        this.showErrorMessage(BaseConnect.disconnectErrorCode.MAP_DISCONNECT);
    }

    // RVA: 0x18FAA44  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/OnApplicationQuit.c
    private void OnApplicationQuit()
    {
        UJDebug.Log("OnApplicationQuit");
        BaseConnect.isShuttingDown = true;
        ConnectProxy proxy;
        if (this.proxyLogin == null)
        {
            if (this.proxyMap == null)
            {
                return;
            }
            UJDebug.Log("Quit: closing map proxy");
            proxy = this.proxyMap;
        }
        else
        {
            UJDebug.Log("Quit: closing login proxy");
            proxy = this.proxyLogin;
        }
        if (proxy != null)
        {
            proxy.close();
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x18FAB90  Ghidra: work/06_ghidra/decompiled_full/BaseConnect/.ctor.c
    public BaseConnect()
    {
        // TODO: confidence:low — Ghidra .c not generated for default ctor; defaults inferred from field types.
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 797
    public enum disconnectErrorCode
    {
        NONE = 0,
        LOGIN_DISCONNECT = 1,
        MAP_DISCONNECT = 2,
        OTHER = 3,
    }
}
