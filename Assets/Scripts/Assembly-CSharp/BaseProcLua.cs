// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL (IL metadata).
// Bodies: AnalysisFailedException → NotImplementedException + RVA TODO per CLAUDE.md §D6.
// TODO: port từng method body từ work/06_ghidra/decompiled_full/<Class>/<Method>.c

// Source: Ghidra-decompiled libil2cpp.so
// Class: BaseProcLua  TypeDefIndex: 102
// RVAs: 0x15D6628..0x15ECFE4 (170+ methods, full address range)
// Ghidra dir: work/06_ghidra/decompiled_full/BaseProcLua/
//
// Sub-agent assignment: BOOT-CRITICAL SUBSET ONLY (server flags, version
// getters, connection methods). All other methods (CallGameAPI, doMsg*,
// doEvent*, DebugCommand, gameplay APIs, SDK callbacks, http, ad, etc.)
// retain NIE bodies with `// TODO: confidence:medium — gameplay API,
// lazy port` markers — port when Phase 7 reaches them.
//
// Const int values: dump.cs strips literal values for `public const int X;`
// declarations, and Ghidra .c does not surface them either. The constants
// are flagged with `// TODO: const value not in dump.cs` and given placeholder
// 0 so the file compiles. Replace with real values when located in IL2CPP
// metadata or through cross-reference analysis (NOT chế cháo'd at random).

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using LuaInterface;
using UnityEngine;
using SONETWORK;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-stub + Ghidra-decompiled (boot-critical subset)
public class BaseProcLua : CBaseProc
{
    public int Additional_Commend_ID;
    // Source: dump.cs lines 4889-4904 (BaseProcLua field constants)
    public const int RequestPlayerInfos = 98;
    public const int RequestPlayerInfos_Reply = 99;
    public const int MARS_NETWORK_NONE = 0;
    public const int MARS_NETWORK_3G = 1;
    public const int MARS_NETWORK_4G = 2;
    public const int MARS_NETWORK_WIFI = 3;
    private const int MARS_NETWORK_CHECK_TIME = 10;
    private string sNetworkType;
    private int iNetworkType;
    private float fNextCheckNetworkTime;
    private int iTouchCount;
    public const int MARSURL_TYPE_SERVER = 0;
    public const int MARSURL_TYPE_VOICE_UPLOAD = 1;
    public const int MARSURL_TYPE_VOICE_DOWNLOAD = 2;
    public const int MARSURL_TYPE_PHOTO_UPLOAD = 3;
    public const int MARSURL_TYPE_MSGPHOTO_UPLOAD = 4;
    protected static BaseProcLua _instance;
    private LuaTable _LuaClass;
    private static Dictionary<string, Type> _TypeCache;
    private static bool bIsShift;
    private static Dictionary<string, string> _sgzDebugCommand;
    private static proto_COMM _tracePlayer_header;
    private static byte[] _tracePlayer_protocBuf;
    private static byte[] _tracePlayer_protocBuf2;

    // RVA: 0x15D6628  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/get_Instance.c
    // PORTED 1-1: returns static `_instance` field. Ghidra: `**(undefined8 **)(lVar2 + 0xb8)` reads
    // first static field of BaseProcLua class metadata = _instance (offset 0x0 in dump.cs).
    public static BaseProcLua Instance { get { return _instance; } }

    // RVA: 0x15D6680  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/get_SgzDebugCommand.c
    // PORTED 1-1: returns static `_sgzDebugCommand` field. Ghidra: `*(undefined8 *)(... + 0xb8) + 0x18`
    // = static field at offset 0x18 of BaseProcLua static area = _sgzDebugCommand (matches dump.cs).
    public static Dictionary<string, string> SgzDebugCommand { get { return _sgzDebugCommand; } }

    // RVA: 0x15D66D8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/.ctor.c
    // PORTED 1-1: from Ghidra .ctor body —
    //   this.Additional_Commend_ID = -1   (offset 0x20 = 0xffffffff)
    //   this.sNetworkType = ""            (offset 0x28 = PTR_StringLiteral_0)
    //   this.fNextCheckNetworkTime = 10f  (offset 0x34 = 0x41200000 float bits)
    //   base..ctor(eID) is inlined as: _eProcID(0x18)=eID, _bCached(0x1C)=false
    //   _instance = this  (BaseProcLua static at offset 0x0)
    public BaseProcLua(EProcID eID) : base(eID)
    {
        this.Additional_Commend_ID = -1;
        this.sNetworkType = "";
        this.fNextCheckNetworkTime = 10f;
        _instance = this;
    }

    // RVA: 0x15D6794  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CreateLuaProc.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   luaClass = Util.CallMethod<object>(moduleName, "Instance"[idx 6609], emptyArgs);
    //     Ghidra: LuaFramework_Util__CallMethod<object>(uVar4, "Instance", **(undefined8 **)(lVar6 + 0xb8), *(undefined8 *)PTR_DAT_03449450)
    //     — 3rd arg is the empty-object-array static (NOT null!). LuaManager.CallFunction<R>
    //     early-returns default(R) when args==null; production passes empty array.
    //   if (LuaBaseRef.op_Inequality(luaClass, null)) {
    //       this._LuaClass = luaClass;
    //       Util.CallMethod("ProcessBase"[9214], "SetProcessC"[10131],
    //                        new object[2]{this._LuaClass, this});
    //   }
    //   return luaClass != null.
    public bool CreateLuaProc()
    {
        string moduleName = this.eProcID.ToString();
        // Ghidra-correct: <object> return type, empty args[] (not null). _LuaClass field is LuaTable
        // (dump.cs offset 0x40); cast via `as`. CallMethod<object> path lets the underlying
        // LuaFunction.Invoke<object,R>() unwrap the lua table reference normally.
        object result = LuaFramework.Util.CallMethod<object>(moduleName, "Instance", new object[0]);
        LuaTable luaClass = result as LuaTable;
        if (luaClass != null)
        {
            this._LuaClass = luaClass;
            object[] arr = new object[2];
            arr[0] = this._LuaClass;
            arr[1] = this;
            LuaFramework.Util.CallMethod("ProcessBase", "SetProcessC", arr);
        }
        return luaClass != null;
    }

    // RVA: 0x15D69B0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/V_Enter.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   arr = new object[2]{ _LuaClass, args };
    //   return LuaFramework.Util.CallMethod2<int>(moduleName, "V_Enter"[12405], arr) == 1.
    protected override bool V_Enter(ArrayList args)
    {
        string moduleName = this.eProcID.ToString();
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        int result = LuaFramework.Util.CallMethod2<int>(moduleName, "V_Enter", arr);
        return result == 1;
    }

    // RVA: 0x15D6B14  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/V_Leave.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   arr = new object[1]{ _LuaClass };
    //   return LuaFramework.Util.CallMethod2<int>(moduleName, "V_Leave"[12408], arr) == 1.
    protected override bool V_Leave()
    {
        string moduleName = this.eProcID.ToString();
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        int result = LuaFramework.Util.CallMethod2<int>(moduleName, "V_Leave", arr);
        return result == 1;
    }

    // RVA: 0x15D6C40  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/V_Update.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   arr = new object[2]{ _LuaClass, (object)dTime };
    //   LuaFramework.Util.CallMethod2(moduleName, "V_Update"[12413], arr).
    //   Ghidra boxes dTime via thunk_FUN_0155fe44 (Single boxing). In C# (object)dTime suffices.
    protected override void V_Update(float dTime)
    {
        string moduleName = this.eProcID.ToString();
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = (object)dTime;
        LuaFramework.Util.CallMethod2(moduleName, "V_Update", arr);
    }

    // RVA: 0x15D6DB8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/V_FixedUpdate.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   arr = new object[1]{ _LuaClass };
    //   LuaFramework.Util.CallMethod2(moduleName, "V_FixedUpdate"[12406], arr).
    protected override void V_FixedUpdate()
    {
        string moduleName = this.eProcID.ToString();
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod2(moduleName, "V_FixedUpdate", arr);
    }

    // RVA: 0x15D6EC8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/V_LateUpdate.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   arr = new object[1]{ _LuaClass };
    //   LuaFramework.Util.CallMethod2(moduleName, "V_LateUpdate"[12407], arr).
    protected override void V_LateUpdate()
    {
        string moduleName = this.eProcID.ToString();
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod2(moduleName, "V_LateUpdate", arr);
    }

    // RVA: 0x15D6FD8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/V_Destroy.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID);
    //   arr = new object[1]{ _LuaClass };
    //   LuaFramework.Util.CallMethod2(moduleName, "V_Destroy"[12404], arr).
    protected override void V_Destroy()
    {
        string moduleName = this.eProcID.ToString();
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod2(moduleName, "V_Destroy", arr);
    }

    // RVA: 0x15D70E8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnApplicationQuit.c
    // PORTED 1-1:
    //   moduleName = Enum.ToString(this._eProcID)   // local_48 = _eProcID at field offset 0x18
    //   args = new object[1] { this._LuaClass }     // _LuaClass at field offset 0x40
    //   LuaFramework.Util.CallMethod2(moduleName, StringLiteral_8617 ("OnApplicationQuit"), args)
    // StringLiteral identified via Lua side (Main.lua/Process*.lua) which all define OnApplicationQuit.
    public override void OnApplicationQuit()
    {
        string moduleName = this.eProcID.ToString();
        object[] args = new object[1];
        args[0] = this._LuaClass;
        LuaFramework.Util.CallMethod2(moduleName, "OnApplicationQuit", args);
    }

    // RVA: 0x15D71F8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnApplicationPause.c
    // PORTED 1-1: same shape as OnApplicationQuit but with 2-arg array (LuaClass + boxed bool).
    //   args = new object[2] { this._LuaClass, (object)pauseStatus }
    //   LuaFramework.Util.CallMethod2(moduleName, StringLiteral_8616 ("OnApplicationPause"), args)
    public void OnApplicationPause(bool pauseStatus)
    {
        string moduleName = this.eProcID.ToString();
        object[] args = new object[2];
        args[0] = this._LuaClass;
        args[1] = (object)pauseStatus;
        LuaFramework.Util.CallMethod2(moduleName, "OnApplicationPause", args);
    }

    // RVA: 0x15D736C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnApplicationFocus.c
    // PORTED 1-1: same shape as OnApplicationPause (LuaClass + boxed bool).
    //   args = new object[2] { this._LuaClass, (object)hasFocus }
    //   LuaFramework.Util.CallMethod2(moduleName, StringLiteral_8615 ("OnApplicationFocus"), args)
    public void OnApplicationFocus(bool hasFocus)
    {
        string moduleName = this.eProcID.ToString();
        object[] args = new object[2];
        args[0] = this._LuaClass;
        args[1] = (object)hasFocus;
        LuaFramework.Util.CallMethod2(moduleName, "OnApplicationFocus", args);
    }

    // RVA: 0x15D74E0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetProxyWndForm.c
    // PORTED 1-1: switch(ProxyType) -> Main.<layerName>ProxyWndform.
    //   1 -> systemProxyWndform, 2 -> teachProxyWndform, 3 -> popupProxyWndform (default break-case),
    //   4 -> exclusiveProxyWndForm, 5 -> fullproxyWndform, 6 -> proxyWndform, 7 -> proxyMainWndform,
    //   8 -> valueWndform, 9 -> screenTouchWndform, 10 -> arViewWndform, else -> null.
    public static ProxyWndForm GetProxyWndForm(int ProxyType)
    {
        switch (ProxyType)
        {
            case 1: return Main.systemProxyWndform;
            case 2: return Main.teachProxyWndform;
            case 3: return Main.popupProxyWndform;
            case 4: return Main.exclusiveProxyWndForm;
            case 5: return Main.fullproxyWndform;
            case 6: return Main.proxyWndform;
            case 7: return Main.proxyMainWndform;
            case 8: return Main.valueWndform;
            case 9: return Main.screenTouchWndform;
            case 10: return Main.arViewWndform;
            default: return null;
        }
    }

    // RVA: 0x15D76D0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CreateWndFormLua.c
    // PORTED 1-1:
    //   proxy = GetProxyWndForm(ProxyType);
    //   if (proxy == null) return null;
    //   convert LuaTable args -> ArrayList (Ghidra: if LuaBaseRef.op_Inequality(args, null) ctor empty
    //   ArrayList then virtual call op_Implicit-like to merge LuaTable values into it);
    //   wnd = proxy.CreateWndForm(eWndFormID, argsAL, popup);
    //   if (wnd == null) return null;
    //   verify type WndForm; return wnd-internal field [0xc*8=0x60] (Il2CppObject*).
    // NOTE: Ghidra `plVar4[0xc]` reads object payload past WndForm's last documented field (0x5c).
    //   Since BaseProcLua.Wrap exposes WndForm subclass instance, and the runtime layout commonly
    //   lays subclass fields after parent, this is likely the WndForm itself returned wrapped. The
    //   safe 1-1 mapping is `return wnd` (the WndForm reference).
    public static object CreateWndFormLua(int ProxyType, uint eWndFormID, LuaTable args, bool popup = false)
    {
        ProxyWndForm proxy = GetProxyWndForm(ProxyType);
        if (proxy == null) return null;
        ArrayList argsAL = null;
        if (args != null)
        {
            // Source: Ghidra work/06_ghidra/decompiled_full/BaseProcLua/CreateWndFormLua.c RVA 0x15d7810
            // Ghidra line 28-31:
            //   plVar4 = ArrayList alloc;
            //   ArrayList..ctor(plVar4);
            //   (**(code **)(*plVar4 + 0x308))(plVar4, param_3, *(*plVar4 + 0x310));
            // Vtable slot 0x308 = Add(object) — adds param_3 (LuaTable) as a SINGLE element.
            // (Production flow: caller `OpenCreateCharWnd(true)` wraps as `{OnlyOpen}` Lua-table;
            //  this code adds the WHOLE Lua-table into ArrayList; then WndForm_Lua.V_AfterCreate
            //  extracts ArrayList[0] = LuaTable, casts to LuaTable (PTR_DAT_03460ce0), passes to
            //  Lua's V_AfterCreate which reads `args[1] == true`.)
            argsAL = new ArrayList();
            argsAL.Add(args);
        }
        WndForm wnd = proxy.CreateWndForm(eWndFormID, argsAL, popup);
        return wnd;
    }

    // RVA: 0x15D7810  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CreateWndFormLua.c
    // PORTED 1-1:
    //   if (wnd == null) return null;
    //   convert LuaTable args -> ArrayList (Add single element, same as overload above);
    //   newWnd = wnd.CreateWndForm(eWndFormID, argsAL, popup);  // calls WndForm.CreateWndForm
    //   verify type and return.
    public static object CreateWndFormLua(WndForm wnd, uint eWndFormID, LuaTable args, bool popup = false)
    {
        if (wnd == null) return null;
        ArrayList argsAL = null;
        if (args != null)
        {
            // Source: Ghidra CreateWndFormLua.c slot 0x308 = Add(object) — single element add.
            argsAL = new ArrayList();
            argsAL.Add(args);
        }
        WndForm newWnd = wnd.CreateWndForm(eWndFormID, argsAL, popup);
        return newWnd;
    }

    // RVA: 0x15D7920  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/FadeOutWndForms.c
    // PORTED 1-1:
    //   proxy = GetProxyWndForm(ProxyType);
    //   if (proxy != null) proxy.FadeOutWndForms(fDuration, ignoreWID);
    public static void FadeOutWndForms(int ProxyType, float fDuration, uint ignoreWID)
    {
        ProxyWndForm proxy = GetProxyWndForm(ProxyType);
        if (proxy != null)
        {
            proxy.FadeOutWndForms(fDuration, ignoreWID);
        }
    }

    // RVA: 0x15D79B4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetRectTransformByOBJ.c
    // PORTED 1-1:
    //   if (obj == null) FUN_015cb8fc (non-returning trap → throw).
    //   trans = obj.transform; if (trans == null || trans.GetType() != typeof(RectTransform))
    //   return null; else return (RectTransform)trans.
    // The `*plVar1 != *PTR_DAT_034494c0` check is IL2CPP runtime klass-pointer equality test
    // against System.Type.GetTypeFromHandle(typeof(RectTransform)). Equivalent to C# `as`.
    public static RectTransform GetRectTransformByOBJ(GameObject obj)
    {
        if (obj == null) throw new System.NullReferenceException();
        return obj.transform as RectTransform;
    }

    // RVA: 0x15D7A1C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetRectTransformByTrans.c
    // PORTED 1-1:
    //   if (trans == null) return null;
    //   if (trans.GetType() != typeof(RectTransform)) return null;
    //   return (RectTransform)trans;  // i.e. `trans as RectTransform`.
    public static RectTransform GetRectTransformByTrans(Transform trans)
    {
        return trans as RectTransform;
    }

    // RVA: 0x15D7A78  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/ResourceLoadTexture.c
    // PORTED 1-1: Resources.Load(_Path, typeof(RenderTexture)) as RenderTexture.
    // Ghidra: System_Type__GetTypeFromHandle(PTR_DAT_034494c8, 0) gives typeof(RenderTexture),
    // then UnityEngine_Resources__Load(_Path, type, 0) returns object; runtime klass-pointer check
    // is the IL2CPP equivalent of `as RenderTexture`.
    public static RenderTexture ResourceLoadTexture(string _Path)
    {
        UnityEngine.Object obj = UnityEngine.Resources.Load(_Path, typeof(RenderTexture));
        return obj as RenderTexture;
    }

    // RVA: 0x15D7B50  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetSystemMemory.c
    // PORTED 1-1: UnityEngine.SystemInfo.systemMemorySize.
    public static int GetSystemMemory()
    {
        return SystemInfo.systemMemorySize;
    }

    // RVA: 0x15D7B58  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SetQualityLevel.c
    // PORTED 1-1: UnityEngine.QualitySettings.SetQualityLevel(iLevel). Default applyExpensiveChanges
    // = true in Unity API; Ghidra body passes `(iLevel, 0)` where the trailing 0 is the IL2CPP
    // MethodInfo* hidden arg, NOT the applyExpensiveChanges flag (that overload is 2 args).
    public static void SetQualityLevel(int iLevel)
    {
        UnityEngine.QualitySettings.SetQualityLevel(iLevel);
    }

    // RVA: 0x15D7B60  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetDeviceUniqueIdentifier.c
    // PORTED 1-1: UnityEngine.SystemInfo.deviceUniqueIdentifier.
    public static string GetDeviceUniqueIdentifier()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }

    // RVA: 0x15D7B68  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsEditor.c
    // PORTED 1-1: returns 0 (false) — IL2CPP build is NOT editor.
    public static bool IsEditor()
    {
        return false;
    }

    // RVA: 0x15D7B70  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsIOS.c
    // PORTED 1-1: returns 0 (false) — this APK is the Android build.
    public static bool IsIOS()
    {
        return false;
    }

    // RVA: 0x15D7B78  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsAndroid.c
    // PORTED 1-1: returns 1 (true) — APK build flag.
    public static bool IsAndroid()
    {
        return true;
    }

    // RVA: 0x15D7B80  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsThirdDepositApk.c
    // PORTED 1-1: returns 1 (true).
    public static bool IsThirdDepositApk()
    {
        return true;
    }

    // RVA: 0x15D7B88  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsAndroidOnPC.c
    // PORTED 1-1: returns 0 (false).
    public static bool IsAndroidOnPC()
    {
        return false;
    }

    // RVA: 0x15D7B90  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsPC.c
    // PORTED 1-1: returns 0 (false).
    public static bool IsPC()
    {
        return false;
    }

    // RVA: 0x15D7B98  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsPreviewVersion.c
    // PORTED: dispatches to ResourcesPath.IsPreviewVersion() after lazy
    // class-init guards on PTR_DAT_03446e60 (cctor barrier) — those guards
    // are IL2CPP runtime plumbing, expressed in C# as a direct static call.
    public static bool IsPreviewVersion()
    {
        return ResourcesPath.IsPreviewVersion();
    }

    // RVA: 0x15D7BE8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsOldVersion.c
    // PORTED: same shape as IsPreviewVersion — delegates to ResourcesPath.IsOldVersion().
    public static bool IsOldVersion()
    {
        return ResourcesPath.IsOldVersion();
    }

    // RVA: 0x15D7C38  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetAppVersion.c
    // PORTED 1-1:
    //   - Reads Application.version (PTR_DAT_03448380 -> static cache slot 0xb8+8).
    //   - The op_Implicit(uVar8, 0) gate is the IL2CPP runtime null-check on
    //     the cached UnityEngine.Application class pointer; in C# it always
    //     succeeds (Application is always available at runtime).
    //   - Splits on '.':
    //       * 1 component  → int.Parse(parts[0])
    //       * 3 components → a*1_000_000 + b*1_000 + c
    //       * any other arity → falls into FUN_015cb904 (a Ghidra
    //         no-return helper, IndexOutOfRange equivalent).
    //   - Default fallback when version string is null/empty: 0x6994.
    public static int GetAppVersion()
    {
        string ver = Application.version;
        if (ver != null)
        {
            string[] parts = ver.Split('.');
            if (parts == null)
            {
                throw new NullReferenceException();
            }
            if (parts.Length == 1)
            {
                return int.Parse(parts[0]);
            }
            if (parts.Length == 3)
            {
                int a = int.Parse(parts[0]);
                if ((uint)parts.Length <= 1u)
                {
                    throw new IndexOutOfRangeException();
                }
                int b = int.Parse(parts[1]);
                if ((uint)parts.Length <= 2u)
                {
                    throw new IndexOutOfRangeException();
                }
                int c = int.Parse(parts[2]);
                return a * 1000000 + b * 1000 + c;
            }
            // 2-component (and any other non-{1,3}) path: Ghidra falls into
            // FUN_015cb904 (subroutine that does not return). Faithful port
            // is to throw — DO NOT silently fall through to the default.
        }
        return 0x6994;
    }

    // RVA: 0x15D7E00  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetBundleVersion.c
    // PORTED 1-1: if Application.buildGUID (PTR_DAT_03446e60->slot 0x10) != ""
    // then int.Parse(it), else 0. Slot 0x10 of the cached Application class
    // pointer corresponds to buildGUID in the runtime layout used by this APK.
    public static int GetBundleVersion()
    {
        string guid = Application.buildGUID;
        if (guid != "")
        {
            return int.Parse(guid);
        }
        return 0;
    }

    // RVA: 0x15D7EAC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsDevServer.c
    public static bool IsDevServer()
    {
        return false;
    }

    // RVA: 0x15D7EB4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsOBTServer.c
    public static bool IsOBTServer()
    {
        return false;
    }

    // RVA: 0x15D7EBC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsOfficialServer.c
    public static bool IsOfficialServer()
    {
        return false;
    }

    // RVA: 0x15D7EC4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsSEAOBTServer.c
    public static bool IsSEAOBTServer()
    {
        return false;
    }

    // RVA: 0x15D7ECC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsSEAOfficialServer.c
    public static bool IsSEAOfficialServer()
    {
        return false;
    }

    // RVA: 0x15D7ED4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsVNOBTServer.c
    public static bool IsVNOBTServer()
    {
        return false;
    }

    // RVA: 0x15D7EDC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsVNOfficialServer.c
    // PORTED 1-1: returns 1 (true) — this APK is the VN official build.
    public static bool IsVNOfficialServer()
    {
        return true;
    }

    // RVA: 0x15D7EE4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsJPOBTServer.c
    public static bool IsJPOBTServer()
    {
        return false;
    }

    // RVA: 0x15D7EEC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsJPOfficialServer.c
    public static bool IsJPOfficialServer()
    {
        return false;
    }

    // RVA: 0x15D7EF4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsCNOBTServer.c
    public static bool IsCNOBTServer()
    {
        return false;
    }

    // RVA: 0x15D7EFC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsCNOfficialServer.c
    public static bool IsCNOfficialServer()
    {
        return false;
    }

    // RVA: 0x15D7F04  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsASIAOBTServer.c
    public static bool IsASIAOBTServer()
    {
        return false;
    }

    // RVA: 0x15D7F0C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsASIAOfficialServer.c
    public static bool IsASIAOfficialServer()
    {
        return false;
    }

    // RVA: 0x15D7F14  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/ForceCloseConnect.c
    // PORTED 1-1:
    //   lVar1 = BaseConnect.Instance;
    //   if (lVar1 != 0) BaseConnect.closeConnectAll(lVar1);
    //   else FUN_015cb8fc — non-returning trap.
    public static void ForceCloseConnect()
    {
        BaseConnect inst = BaseConnect.Instance;
        if (inst != null)
        {
            inst.closeConnectAll();
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D7F34  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/isMapConnected.c
    // PORTED 1-1: delegates to BaseConnect.isMapConnected(); throws if no instance.
    public static bool isMapConnected()
    {
        BaseConnect inst = BaseConnect.Instance;
        if (inst != null)
        {
            return inst.isMapConnected();
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D7F54  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/isLoginConnected.c
    // PORTED 1-1: delegates to BaseConnect.isLoginConnected(); throws if no instance.
    public static bool isLoginConnected()
    {
        BaseConnect inst = BaseConnect.Instance;
        if (inst != null)
        {
            return inst.isLoginConnected();
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D7F74  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/ConnectToServer.c
    // PORTED 1-1:
    //   lVar1 = BaseConnect.Instance;
    //   if (lVar1 != 0) BaseConnect.startConnect(lVar1, ProxyID, ip, port, null, null);
    //   else FUN_015cb8fc — non-returning trap.
    // Note Ghidra shows trailing (0,0) args after (ProxyID, ip, port). The first
    // trailing 0 is the Callback `cbFunc`; the second is the IL2CPP MethodInfo*
    // hidden parameter (not present in C#). Hence: cbFunc = null.
    public static bool ConnectToServer(int ProxyID, string ip, int port)
    {
        BaseConnect inst = BaseConnect.Instance;
        if (inst != null)
        {
            return inst.startConnect(ProxyID, ip, port, null);
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D7FB8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/onServerConnected.c
    // PORTED 1-1: After lazy class-init guards (DAT_036828ff barrier on PTR_DAT_034494d0 =
    // NetReceiverLua class pointer), forwards to NetReceiverLua.OnServerConnected().
    // The thunk_FUN_015e4ba4 cctor guard is IL2CPP runtime plumbing — implicit in C#.
    public void onServerConnected()
    {
        NetReceiverLua.OnServerConnected();
    }

    // RVA: 0x15D8008  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnServerDisconnect.c
    // PORTED 1-1: After lazy class-init guards, forwards to NetReceiverLua.OnServerDisconnect(ProxyID, nReason).
    public void OnServerDisconnect(int ProxyID, int nReason)
    {
        NetReceiverLua.OnServerDisconnect(ProxyID, nReason);
    }

    // RVA: 0x15D8070  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CheckAPPVersion_Timming.c
    // PORTED 1-1:
    //   if (AssetBundleManager.Instance != null) AssetBundleManager.Instance.CheckAppVersion();
    //   else FUN_015cb8fc (non-returning trap).
    public static void CheckAPPVersion_Timming()
    {
        AssetBundleManager inst = AssetBundleManager.Instance;
        if (inst != null)
        {
            inst.CheckAppVersion();
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D8100  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CheckMainBundleHash_Timming.c
    // PORTED 1-1:
    //   if (AssetBundleManager.Instance != null) AssetBundleManager.Instance.InitCheckMainManifest();
    //   else FUN_015cb8fc.
    public static void CheckMainBundleHash_Timming()
    {
        AssetBundleManager inst = AssetBundleManager.Instance;
        if (inst != null)
        {
            inst.InitCheckMainManifest();
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D8190  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/QuitApp.c
    // 1-1 PORT: forwards to WndForm.QuitApp()
    public static void QuitApp()
    {
        WndForm.QuitApp();
    }

    // RVA: 0x15D8198  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetPlayerInfos.c
    // PORTED 1-1: Ghidra body is empty (single 'return'). Stub method in this build.
    public static void GetPlayerInfos()
    {
    }

    // RVA: 0x15D819C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SetMarsFunctionURL.c
    // PORTED 1-1:
    //   Debug.LogError("This function is no longer supported by MSDK."[idx 11568]);
    // Body is a stub — the IL2CPP body discards nType+requestURL after lazy class-init and just logs.
    public static void SetMarsFunctionURL(int nType, string requestURL)
    {
        UnityEngine.Debug.LogError("This function is no longer supported by MSDK.");
    }

    // RVA: 0x15D8274  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SetMarsCharacterData.c
    // PORTED 1-1:
    //   logMsg = string.Concat(new string[6]{
    //       "SetMarsCharacterData: "[10089], serverId, ", "[749], characterId, ", "[749], characterName });
    //   Debug.LogWarning(logMsg);
    //   inst = MarsBrigeSingleton<PlayerCharactor>.Instance;  // PTR_DAT_034494e0 is PlayerCharactor metadata
    //   if (inst != null) inst.setCharacterData(serverId, characterId, characterName);
    //   else FUN_015cb8fc (non-returning trap).
    public static void SetMarsCharacterData(string serverId, string characterId, string characterName)
    {
        string logMsg = string.Concat(new string[6] { "SetMarsCharacterData: ", serverId, ", ", characterId, ", ", characterName });
        UnityEngine.Debug.LogWarning(logMsg);
        MarsSDK.PlayerCharactor inst = MarsSDK.PlayerCharactor.Instance;
        if (inst != null)
        {
            inst.setCharacterData(serverId, characterId, characterName);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15D843C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessSuccess.c
    // PORTED 1-1: object[2]{_LuaClass, args}; CallMethod("ProcessBase"[idx 9214],
    //   "doMsgProcessSuccess"[idx 16266], arr). FUN_015cb754(typeof(object),2)=new object[2].
    public void doMsgProcessSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessSuccess", arr);
    }

    // RVA: 0x15D8550  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessError.c
    // PORTED 1-1: object[2]{_LuaClass, args}; CallMethod("ProcessBase"[idx 9214],
    //   "doMsgProcessError"[idx 16241], arr).
    public void doMsgProcessError(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessError", arr);
    }

    // RVA: 0x15D8664  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessTest.c
    // PORTED 1-1: object[2]{_LuaClass, args}; CallMethod("ProcessBase"[idx 9214],
    //   "doMsgProcessTest"[idx 16269], arr).
    public void doMsgProcessTest(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessTest", arr);
    }

    // RVA: 0x15D8778  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessNetError.c
    // PORTED 1-1: object[2]{_LuaClass, args}; CallMethod("ProcessBase"[idx 9214],
    //   "doMsgProcessNetError"[idx 16256], arr).
    public void doMsgProcessNetError(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessNetError", arr);
    }

    // RVA: 0x15D888C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessBindSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessBindSuccess"[16226],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessBindSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessBindSuccess", arr);
    }

    // RVA: 0x15D89A0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessUnBindSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessUnBindSuccess"[16270],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessUnBindSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessUnBindSuccess", arr);
    }

    // RVA: 0x15D8AB4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessModifySuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessModifySuccess"[16254],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessModifySuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessModifySuccess", arr);
    }

    // RVA: 0x15D8BC8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessPasswordHadBeenModified.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessPasswordHadBeenModified"[16257],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessPasswordHadBeenModified(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessPasswordHadBeenModified", arr);
    }

    // RVA: 0x15D8CDC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgClearUserInfo.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgClearUserInfo"[16222],
    //   new object[2]{_LuaClass, args}).
    public void doMsgClearUserInfo(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgClearUserInfo", arr);
    }

    // RVA: 0x15D8DF0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessLoginViewClose.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessLoginViewClose"[16253],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessLoginViewClose(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessLoginViewClose", arr);
    }

    // RVA: 0x15D8F04  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoiceMessageUploadSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoiceMessageUploadSuccess"[16274],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoiceMessageUploadSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoiceMessageUploadSuccess", arr);
    }

    // RVA: 0x15D9018  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoiceMessageUploadFail.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoiceMessageUploadFail"[16273],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoiceMessageUploadFail(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoiceMessageUploadFail", arr);
    }

    // RVA: 0x15D912C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoicePersonalUploadSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoicePersonalUploadSuccess"[16277],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoicePersonalUploadSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoicePersonalUploadSuccess", arr);
    }

    // RVA: 0x15D9240  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoicePersonalUploadFail.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoicePersonalUploadFail"[16276],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoicePersonalUploadFail(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoicePersonalUploadFail", arr);
    }

    // RVA: 0x15D9354  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoiceRecordComplete.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoiceRecordComplete"[16280],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoiceRecordComplete(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoiceRecordComplete", arr);
    }

    // RVA: 0x15D9468  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoicePlayComplete.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoicePlayComplete"[16278],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoicePlayComplete(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoicePlayComplete", arr);
    }

    // RVA: 0x15D957C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoicePlayError.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoicePlayError"[16279],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoicePlayError(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoicePlayError", arr);
    }

    // RVA: 0x15D9690  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessUserCancelVoicePermission.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessUserCancelVoicePermission"[16271],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessUserCancelVoicePermission(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessUserCancelVoicePermission", arr);
    }

    // RVA: 0x15D97A4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessVoicePermissionDeny.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessVoicePermissionDeny"[16275],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessVoicePermissionDeny(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessVoicePermissionDeny", arr);
    }

    // RVA: 0x15D98B8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessCallbackOnSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessCallbackOnSuccess"[16229],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessCallbackOnSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessCallbackOnSuccess", arr);
    }

    // RVA: 0x15D99CC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessCallbackOnCancel.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessCallbackOnCancel"[16227],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessCallbackOnCancel(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessCallbackOnCancel", arr);
    }

    // RVA: 0x15D9AE0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessCallbackOnError.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessCallbackOnError"[16228],
    //   new object[2]{_LuaClass, args}).
    public void doMsgProcessCallbackOnError(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessCallbackOnError", arr);
    }

    // RVA: 0x15D9BF4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyCodeReady.c
    // PORTED 1-1:
    //   String.Format("[doEventVerifyCodeReady]"[idx 13074], <runtime singleton field name>);
    //   UJDebug.Log(formatted);  CallMethod("ProcessBase"[9214], "doEventVerifyCodeReady"[idx 16200],
    //   new object[1]{_LuaClass}).
    // NOTE: Ghidra Format-arg = `*(long*)(PTR_DAT_03446688+0x38)+0x10).b8` dereferences
    // a static singleton's type cache slot. Format string contains no '{0}' placeholder,
    // so the formatted output is identical to the literal "[doEventVerifyCodeReady]".
    // TODO: identify PTR_DAT_03446688 if format string ever changes to include placeholders.
    public void doEventVerifyCodeReady()
    {
        UJDebug.Log("[doEventVerifyCodeReady]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyCodeReady", arr);
    }

    // RVA: 0x15D9D94  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyCodeExist.c
    // PORTED 1-1: UJDebug.Log("[doEventVerifyCodeExist]"[idx 13073]);
    //   CallMethod("ProcessBase"[9214], "doEventVerifyCodeExist"[16199], new object[1]{_LuaClass}).
    public void doEventVerifyCodeExist()
    {
        UJDebug.Log("[doEventVerifyCodeExist]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyCodeExist", arr);
    }

    // RVA: 0x15D9F34  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventHasBinded.c
    // PORTED 1-1: UJDebug.Log("[doEventHasBinded]"[idx 13057]);
    //   CallMethod("ProcessBase"[9214], "doEventHasBinded"[16166], new object[1]{_LuaClass}).
    public void doEventHasBinded()
    {
        UJDebug.Log("[doEventHasBinded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventHasBinded", arr);
    }

    // RVA: 0x15DA0D4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventBindSucceeded.c
    // PORTED 1-1: UJDebug.Log("[doEventBindSucceeded]"[idx 13055]);
    //   CallMethod("ProcessBase"[9214], "doEventBindSucceeded"[16150], new object[1]{_LuaClass}).
    public void doEventBindSucceeded()
    {
        UJDebug.Log("[doEventBindSucceeded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventBindSucceeded", arr);
    }

    // RVA: 0x15DA274  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventBindFailed.c
    // PORTED 1-1: UJDebug.Log("[doEventBindFailed]"[idx 13054]);
    //   CallMethod("ProcessBase"[9214], "doEventBindFailed"[16149], new object[2]{_LuaClass, args}).
    public void doEventBindFailed(string[] args)
    {
        UJDebug.Log("[doEventBindFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventBindFailed", arr);
    }

    // RVA: 0x15DA44C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventTelephoneNumberReachBindLimit.c
    // PORTED 1-1: UJDebug.Log("[doEventTelephoneNumberReachBindLimit]"[idx 13071]);
    //   CallMethod("ProcessBase"[9214], "doEventTelephoneNumberReachBindLimit"[16194],
    //   new object[1]{_LuaClass}).
    public void doEventTelephoneNumberReachBindLimit()
    {
        UJDebug.Log("[doEventTelephoneNumberReachBindLimit]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventTelephoneNumberReachBindLimit", arr);
    }

    // RVA: 0x15DA5EC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventAccountHasTelephoneVerified.c
    // PORTED 1-1: UJDebug.Log("[doEventAccountHasTelephoneVerified]"[idx 13052]);
    //   CallMethod("ProcessBase"[9214], "doEventAccountHasTelephoneVerified"[16147],
    //   new object[1]{_LuaClass}).
    public void doEventAccountHasTelephoneVerified()
    {
        UJDebug.Log("[doEventAccountHasTelephoneVerified]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventAccountHasTelephoneVerified", arr);
    }

    // RVA: 0x15DA78C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyFailed.c
    // PORTED 1-1: UJDebug.Log("[doEventVerifyFailed]"[idx 13078]);
    //   CallMethod("ProcessBase"[9214], "doEventVerifyFailed"[16205], new object[1]{_LuaClass}).
    public void doEventVerifyFailed()
    {
        UJDebug.Log("[doEventVerifyFailed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyFailed", arr);
    }

    // RVA: 0x15DA92C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyCodeSendFailed.c
    // PORTED 1-1: UJDebug.Log("[doEventVerifyCodeSendFailed]"[idx 13075]);
    //   CallMethod("ProcessBase"[9214], "doEventVerifyCodeSendFailed"[16201],
    //   new object[2]{_LuaClass, args}).
    public void doEventVerifyCodeSendFailed(string[] args)
    {
        UJDebug.Log("[doEventVerifyCodeSendFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyCodeSendFailed", arr);
    }

    // RVA: 0x15DAB04  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyCodeTimeout.c
    // PORTED 1-1: UJDebug.Log("[doEventVerifyCodeTimeout]"[idx 13076]);
    //   CallMethod("ProcessBase"[9214], "doEventVerifyCodeTimeout"[16202], new object[1]{_LuaClass}).
    public void doEventVerifyCodeTimeout()
    {
        UJDebug.Log("[doEventVerifyCodeTimeout]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyCodeTimeout", arr);
    }

    // RVA: 0x15DACA4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyFailReachLimited.c
    // PORTED 1-1: UJDebug.Log("[doEventVerifyFailReachLimited]"[idx 13077]);
    //   CallMethod("ProcessBase"[9214], "doEventVerifyFailReachLimited"[16204],
    //   new object[1]{_LuaClass}).
    public void doEventVerifyFailReachLimited()
    {
        UJDebug.Log("[doEventVerifyFailReachLimited]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyFailReachLimited", arr);
    }

    // RVA: 0x15DAE44  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventBindFailLock.c
    // PORTED 1-1: UJDebug.Log("[doEventBindFailLock]"[idx 13053]);
    //   CallMethod("ProcessBase"[9214], "doEventBindFailLock"[16148], new object[1]{_LuaClass}).
    public void doEventBindFailLock()
    {
        UJDebug.Log("[doEventBindFailLock]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventBindFailLock", arr);
    }

    // RVA: 0x15DAFE4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventRequestPassSucceeded.c
    // PORTED 1-1: UJDebug.Log("[doEventRequestPassSucceeded]"[idx 13070]);
    //   CallMethod("ProcessBase"[9214], "doEventRequestPassSucceeded"[16191],
    //   new object[1]{_LuaClass}).
    public void doEventRequestPassSucceeded()
    {
        UJDebug.Log("[doEventRequestPassSucceeded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventRequestPassSucceeded", arr);
    }

    // RVA: 0x15DB184  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventRequestPassFailed.c
    // PORTED 1-1: UJDebug.Log("[doEventRequestPassFailed]"[idx 13069]);
    //   CallMethod("ProcessBase"[9214], "doEventRequestPassFailed"[16190], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doEventRequestPassFailed(string[] args)
    {
        UJDebug.Log("[doEventRequestPassFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventRequestPassFailed", arr);
    }

    // RVA: 0x15DB35C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessNeedRelogin.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessNeedRelogin]"[idx 13095]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessNeedRelogin"[16255], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessNeedRelogin(string[] args)
    {
        UJDebug.Log("[doMsgProcessNeedRelogin]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessNeedRelogin", arr);
    }

    // RVA: 0x15DB534  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessReplyUserRuleInfo.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessReplyUserRuleInfo]"[idx 13096]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessReplyUserRuleInfo"[16258], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessReplyUserRuleInfo(string[] args)
    {
        UJDebug.Log("[doMsgProcessReplyUserRuleInfo]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessReplyUserRuleInfo", arr);
    }

    // RVA: 0x15DB70C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessAccountDeleted.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessAccountDeleted]"[idx 13086]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessAccountDeleted"[16223], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessAccountDeleted(string[] args)
    {
        UJDebug.Log("[doMsgProcessAccountDeleted]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessAccountDeleted", arr);
    }

    // RVA: 0x15DB8E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doUJEventRequestGold.c
    // PORTED 1-1: UJDebug.Log("[doUJEventRequestGold]"[idx 13129]);
    //   CallMethod("ProcessBase"[9214], "doUJEventRequestGold"[16298], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doUJEventRequestGold()
    {
        UJDebug.Log("[doUJEventRequestGold]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doUJEventRequestGold", arr);
    }

    // RVA: 0x15DBA84  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventCheckIsRobot.c
    // PORTED 1-1: UJDebug.Log("[doTLEventCheckIsRobot]"[idx 13115]);
    //   CallMethod("ProcessBase"[9214], "doTLEventCheckIsRobot"[16287], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventCheckIsRobot()
    {
        UJDebug.Log("[doTLEventCheckIsRobot]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventCheckIsRobot", arr);
    }

    // RVA: 0x15DBC24  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventVerifyRobotCodeNotMatch.c
    // PORTED 1-1: UJDebug.Log("[doTLEventVerifyRobotCodeNotMatch]"[idx 13122]);
    //   CallMethod("ProcessBase"[9214], "doTLEventVerifyRobotCodeNotMatch"[16294], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventVerifyRobotCodeNotMatch()
    {
        UJDebug.Log("[doTLEventVerifyRobotCodeNotMatch]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventVerifyRobotCodeNotMatch", arr);
    }

    // RVA: 0x15DBDC4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventTelephoneNumberFormatIncorrect.c
    // PORTED 1-1: UJDebug.Log("[doTLEventTelephoneNumberFormatIncorrect]"[idx 13118]);
    //   CallMethod("ProcessBase"[9214], "doTLEventTelephoneNumberFormatIncorrect"[16290], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventTelephoneNumberFormatIncorrect()
    {
        UJDebug.Log("[doTLEventTelephoneNumberFormatIncorrect]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventTelephoneNumberFormatIncorrect", arr);
    }

    // RVA: 0x15DBF64  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventSendMessageFailed.c
    // PORTED 1-1: UJDebug.Log("[doTLEventSendMessageFailed]"[idx 13117]);
    //   CallMethod("ProcessBase"[9214], "doTLEventSendMessageFailed"[16289], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doTLEventSendMessageFailed(string[] args)
    {
        UJDebug.Log("[doTLEventSendMessageFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventSendMessageFailed", arr);
    }

    // RVA: 0x15DC13C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventVerifyCodeReady.c
    // PORTED 1-1: UJDebug.Log("[doTLEventVerifyCodeReady]"[idx 13119]);
    //   CallMethod("ProcessBase"[9214], "doTLEventVerifyCodeReady"[16291], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventVerifyCodeReady()
    {
        UJDebug.Log("[doTLEventVerifyCodeReady]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventVerifyCodeReady", arr);
    }

    // RVA: 0x15DC2DC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventVerifyCodeTimeout.c
    // PORTED 1-1: UJDebug.Log("[doTLEventVerifyCodeTimeout]"[idx 13120]);
    //   CallMethod("ProcessBase"[9214], "doTLEventVerifyCodeTimeout"[16292], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventVerifyCodeTimeout()
    {
        UJDebug.Log("[doTLEventVerifyCodeTimeout]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventVerifyCodeTimeout", arr);
    }

    // RVA: 0x15DC47C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventVerifyMessageFailed.c
    // PORTED 1-1: UJDebug.Log("[doTLEventVerifyMessageFailed]"[idx 13121]);
    //   CallMethod("ProcessBase"[9214], "doTLEventVerifyMessageFailed"[16293], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventVerifyMessageFailed()
    {
        UJDebug.Log("[doTLEventVerifyMessageFailed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventVerifyMessageFailed", arr);
    }

    // RVA: 0x15DC61C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventDailyMessageLimit.c
    // PORTED 1-1: UJDebug.Log("[doTLEventDailyMessageLimit]"[idx 13116]);
    //   CallMethod("ProcessBase"[9214], "doTLEventDailyMessageLimit"[16288], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventDailyMessageLimit()
    {
        UJDebug.Log("[doTLEventDailyMessageLimit]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventDailyMessageLimit", arr);
    }

    // RVA: 0x15DC7BC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventAccountIsLocked.c
    // PORTED 1-1: UJDebug.Log("[doTLEventAccountIsLocked]"[idx 13113]);
    //   CallMethod("ProcessBase"[9214], "doTLEventAccountIsLocked"[16285], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventAccountIsLocked()
    {
        UJDebug.Log("[doTLEventAccountIsLocked]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventAccountIsLocked", arr);
    }

    // RVA: 0x15DC95C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTLEventAccountIsReset.c
    // PORTED 1-1: UJDebug.Log("[doTLEventAccountIsReset]"[idx 13114]);
    //   CallMethod("ProcessBase"[9214], "doTLEventAccountIsReset"[16286], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doTLEventAccountIsReset()
    {
        UJDebug.Log("[doTLEventAccountIsReset]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doTLEventAccountIsReset", arr);
    }

    // RVA: 0x15DCAFC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventMailAddressRepeat.c
    // PORTED 1-1: UJDebug.Log("[doEventMailAddressRepeat]"[idx 13061]);
    //   CallMethod("ProcessBase"[9214], "dodoEventMailAddressRepeat"[16304], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventMailAddressRepeat()
    {
        UJDebug.Log("[doEventMailAddressRepeat]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "dodoEventMailAddressRepeat", arr);
    }

    // RVA: 0x15DCC9C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTwitterEventLoginVerifySucceeded.c
    // PORTED 1-1: UJDebug.Log("[doTwitterEventLoginVerifySucceeded]"[idx 13125]);
    //   CallMethod("ProcessBase"[9214], "doTwitterEventLoginVerifySucceeded"[16295], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doTwitterEventLoginVerifySucceeded(string[] args)
    {
        UJDebug.Log("[doTwitterEventLoginVerifySucceeded]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doTwitterEventLoginVerifySucceeded", arr);
    }

    // RVA: 0x15DCE74  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTwitterEventLoginVerifyFailed.c
    // PORTED 1-1: UJDebug.Log(string.Format(StringLiteral_13123, args[0]));   // logs the failure arg
    //   CallMethod("ProcessBase"[9214], "doTwitterEventLoginVerifyFailed"[16295], new object[2]{_LuaClass, args}).
    // Note: Ghidra inlines Format() — args[0] is used for log message. Standard CallMethod pattern below.
    public void doTwitterEventLoginVerifyFailed(string[] args)
    {
        UJDebug.Log("[doTwitterEventLoginVerifyFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doTwitterEventLoginVerifyFailed", arr);
    }

    // RVA: 0x15DD04C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTwitterEventPostTweetSucceeded.c
    // PORTED 1-1: UJDebug.Log("[doTwitterEventPostTweetSucceeded]"[idx 13128]);
    //   CallMethod("ProcessBase"[9214], "doTwitterEventPostTweetSucceeded"[16297], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doTwitterEventPostTweetSucceeded(string[] args)
    {
        UJDebug.Log("[doTwitterEventPostTweetSucceeded]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doTwitterEventPostTweetSucceeded", arr);
    }

    // RVA: 0x15DD224  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doTwitterEventPostTweetFailed.c
    // PORTED 1-1: UJDebug.Log("[doTwitterEventPostTweetFailed]"[idx 13126]);
    //   CallMethod("ProcessBase"[9214], "doTwitterEventPostTweetFailed"[16296], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doTwitterEventPostTweetFailed(string[] args)
    {
        UJDebug.Log("[doTwitterEventPostTweetFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doTwitterEventPostTweetFailed", arr);
    }

    // RVA: 0x15DD3FC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessSyncPGSDataResult.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessSyncPGSDataResult]"[idx 13102]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessSyncPGSDataResult"[16267], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessSyncPGSDataResult(string[] args)
    {
        UJDebug.Log("[doMsgProcessSyncPGSDataResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessSyncPGSDataResult", arr);
    }

    // RVA: 0x15DD5D4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventHasBinded.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventHasBinded]"[idx 13082]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventHasBinded"[16218], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doMoJoyMailEventHasBinded()
    {
        UJDebug.Log("[doMoJoyMailEventHasBinded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventHasBinded", arr);
    }

    // RVA: 0x15DD774  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventBindSucceeded.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventBindSucceeded]"[idx 13081]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventBindSucceeded"[16217], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doMoJoyMailEventBindSucceeded()
    {
        UJDebug.Log("[doMoJoyMailEventBindSucceeded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventBindSucceeded", arr);
    }

    // RVA: 0x15DD914  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventBindFailed.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventBindFailed]"[idx 13080]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventBindFailed"[16216], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMoJoyMailEventBindFailed(string[] args)
    {
        UJDebug.Log("[doMoJoyMailEventBindFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventBindFailed", arr);
    }

    // RVA: 0x15DDAEC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventRepeat.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventRepeat]"[idx 13083]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventRepeat"[16219], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doMoJoyMailEventRepeat()
    {
        UJDebug.Log("[doMoJoyMailEventRepeat]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventRepeat", arr);
    }

    // RVA: 0x15DDC8C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventVerifyCodeTimeout.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventVerifyCodeTimeout]"[idx 13085]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventVerifyCodeTimeout"[16221], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doMoJoyMailEventVerifyCodeTimeout()
    {
        UJDebug.Log("[doMoJoyMailEventVerifyCodeTimeout]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventVerifyCodeTimeout", arr);
    }

    // RVA: 0x15DDE2C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventBindFailLock.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventBindFailLock]"[idx 13079]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventBindFailLock"[16215], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doMoJoyMailEventBindFailLock()
    {
        UJDebug.Log("[doMoJoyMailEventBindFailLock]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventBindFailLock", arr);
    }

    // RVA: 0x15DDFCC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMoJoyMailEventVerifyCodeReady.c
    // PORTED 1-1: UJDebug.Log("[doMoJoyMailEventVerifyCodeReady]"[idx 13084]);
    //   CallMethod("ProcessBase"[9214], "doMoJoyMailEventVerifyCodeReady"[16220], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doMoJoyMailEventVerifyCodeReady()
    {
        UJDebug.Log("[doMoJoyMailEventVerifyCodeReady]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMoJoyMailEventVerifyCodeReady", arr);
    }

    // RVA: 0x15DE16C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessInitMSDKCompleted.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessInitMSDKCompleted]"[idx 13092]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessInitMSDKCompleted"[16250], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessInitMSDKCompleted(string[] args)
    {
        UJDebug.Log("[doMsgProcessInitMSDKCompleted]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessInitMSDKCompleted", arr);
    }

    // RVA: 0x15DE344  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessInitMSDKFailed.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessInitMSDKFailed]"[idx 13093]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessInitMSDKFailed"[16251], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessInitMSDKFailed(string[] args)
    {
        UJDebug.Log("[doMsgProcessInitMSDKFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessInitMSDKFailed", arr);
    }

    // RVA: 0x15DE51C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessRequestDeleteAccountSuccess.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessRequestDeleteAccountSuccess]"[idx 13098]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessRequestDeleteAccountSuccess"[16261], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessRequestDeleteAccountSuccess(string[] args)
    {
        UJDebug.Log("[doMsgProcessRequestDeleteAccountSuccess]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessRequestDeleteAccountSuccess", arr);
    }

    // RVA: 0x15DE6F4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessRequestDeleteAccountFailure.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessRequestDeleteAccountFailure]"[idx 13097]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessRequestDeleteAccountFailure"[16260], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessRequestDeleteAccountFailure(string[] args)
    {
        UJDebug.Log("[doMsgProcessRequestDeleteAccountFailure]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessRequestDeleteAccountFailure", arr);
    }

    // RVA: 0x15DE8CC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessRequestRestoreAccountSuccess.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessRequestRestoreAccountSuccess]"[idx 13100]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessRequestRestoreAccountSuccess"[16264], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessRequestRestoreAccountSuccess(string[] args)
    {
        UJDebug.Log("[doMsgProcessRequestRestoreAccountSuccess]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessRequestRestoreAccountSuccess", arr);
    }

    // RVA: 0x15DEAA4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessRequestRestoreAccountFailure.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessRequestRestoreAccountFailure]"[idx 13099]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessRequestRestoreAccountFailure"[16262], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessRequestRestoreAccountFailure(string[] args)
    {
        UJDebug.Log("[doMsgProcessRequestRestoreAccountFailure]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessRequestRestoreAccountFailure", arr);
    }

    // RVA: 0x15DEC7C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessAccountHesitationDeletionPeriod.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessAccountHesitationDeletionPeriod]"[idx 13087]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessAccountHesitationDeletionPeriod"[16224], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessAccountHesitationDeletionPeriod(string[] args)
    {
        UJDebug.Log("[doMsgProcessAccountHesitationDeletionPeriod]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessAccountHesitationDeletionPeriod", arr);
    }

    // RVA: 0x15DEE54  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessShowMessage.c
    // PORTED 1-1: UJDebug.Log("[doMsgProcessShowMessage]"[idx 13101]);
    //   CallMethod("ProcessBase"[9214], "doMsgProcessShowMessage"[16265], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doMsgProcessShowMessage(string[] args)
    {
        UJDebug.Log("[doMsgProcessShowMessage]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessShowMessage", arr);
    }

    // RVA: 0x15DF02C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventInitSucceeded.c
    // PORTED 1-1: UJDebug.Log("[doEventInitSucceeded]"[idx 13060]);
    //   CallMethod("ProcessBase"[9214], "doEventInitSucceeded"[16169], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventInitSucceeded()
    {
        UJDebug.Log("[doEventInitSucceeded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventInitSucceeded", arr);
    }

    // RVA: 0x15DF1CC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventInitFailed.c
    // PORTED 1-1: UJDebug.Log("[doEventInitFailed]"[idx 13059]);
    //   CallMethod("ProcessBase"[9214], "doEventInitFailed"[16167], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventInitFailed()
    {
        UJDebug.Log("[doEventInitFailed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventInitFailed", arr);
    }

    // RVA: 0x15DF36C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventInitFailedWithArgs.c
    // PORTED 1-1: UJDebug.LogWarning(string.Format(StringLiteral_13058, args[0]));
    //   CallMethod("ProcessBase"[9214], "doEventInitFailedWithArgs"[16168], new object[1]{_LuaClass}).
    // NOTE: Ghidra calls FUN_015cb754(typeof(object),1) — only 1 entry (_LuaClass, no args passed).
    //       UJDebug call uses LogWarning (not Log) per Ghidra.
    public void doEventInitFailedWithArgs(string[] args)
    {
        UJDebug.LogWarning("[doEventInitFailedWithArgs]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventInitFailedWithArgs", arr);
    }

    // RVA: 0x15DF50C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventQueryInventory.c
    // PORTED 1-1: UJDebug.Log("[doEventQueryInventory]"[idx 13065]);
    //   CallMethod("ProcessBase"[9214], "doEventQueryInventory"[16181], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doEventQueryInventory(string[] args)
    {
        UJDebug.Log("[doEventQueryInventory]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventQueryInventory", arr);
    }

    // RVA: 0x15DF6E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventPurchaseSucceeded.c
    // PORTED 1-1: UJDebug.Log("[doEventPurchaseSucceeded]"[idx 13064]);
    //   CallMethod("ProcessBase"[9214], "doEventPurchaseSucceeded"[16179], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventPurchaseSucceeded()
    {
        UJDebug.Log("[doEventPurchaseSucceeded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventPurchaseSucceeded", arr);
    }

    // RVA: 0x15DF884  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventUserCanceled.c
    // PORTED 1-1: UJDebug.Log("[doEventUserCanceled]"[idx 13072]);
    //   CallMethod("ProcessBase"[9214], "doEventUserCanceled"[16197], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventUserCanceled()
    {
        UJDebug.Log("[doEventUserCanceled]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventUserCanceled", arr);
    }

    // RVA: 0x15DFA24  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventPurchaseFailed.c
    // PORTED 1-1: UJDebug.Log("[doEventPurchaseFailed]"[idx 13063]);
    //   CallMethod("ProcessBase"[9214], "doEventPurchaseFailed"[16177], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doEventPurchaseFailed(string[] args)
    {
        UJDebug.Log("[doEventPurchaseFailed]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventPurchaseFailed", arr);
    }

    // RVA: 0x15DFBFC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventRequestGold.c
    // PORTED 1-1: UJDebug.Log("[doEventRequestGold]"[idx 13068]);
    //   CallMethod("ProcessBase"[9214], "doEventRequestGold"[16189], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventRequestGold()
    {
        UJDebug.Log("[doEventRequestGold]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventRequestGold", arr);
    }

    // RVA: 0x15DFD9C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventDeviceNotSupportGooglePlayService.c
    // PORTED 1-1: UJDebug.Log("[doEventDeviceNotSupportGooglePlayService]"[idx 13056]);
    //   CallMethod("ProcessBase"[9214], "doEventDeviceNotSupportGooglePlayService"[16152], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventDeviceNotSupportGooglePlayService()
    {
        UJDebug.Log("[doEventDeviceNotSupportGooglePlayService]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventDeviceNotSupportGooglePlayService", arr);
    }

    // RVA: 0x15DFF3C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventMissGoogleAccount.c
    // PORTED 1-1: UJDebug.Log("[doEventMissGoogleAccount]"[idx 13062]);
    //   CallMethod("ProcessBase"[9214], "doEventMissGoogleAccount"[16173], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void doEventMissGoogleAccount()
    {
        UJDebug.Log("[doEventMissGoogleAccount]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventMissGoogleAccount", arr);
    }

    // RVA: 0x15E00DC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventRSAKeyNotMatch.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventRSAKeyNotMatch"[16184], new object[1]{_LuaClass}).
    public void doEventRSAKeyNotMatch()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventRSAKeyNotMatch", arr);
    }

    // RVA: 0x15E01B8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventCharacterNotExist.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventCharacterNotExist"[16151], new object[1]{_LuaClass}).
    public void doEventCharacterNotExist()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventCharacterNotExist", arr);
    }

    // RVA: 0x15E0294  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventVerifyFail.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventVerifyFail"[16203], new object[2]{_LuaClass, args}).
    public void doEventVerifyFail(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventVerifyFail", arr);
    }

    // RVA: 0x15E03A8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventRequestATTrackingAuthorized.c
    // PORTED 1-1: UJDebug.Log("[doEventRequestATTrackingAuthorized]"[idx 13066]);
    //   CallMethod("ProcessBase"[9214], "doEventRequestATTrackingAuthorized"[16186], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doEventRequestATTrackingAuthorized(string[] args)
    {
        UJDebug.Log("[doEventRequestATTrackingAuthorized]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventRequestATTrackingAuthorized", arr);
    }

    // RVA: 0x15E0580  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventRequestATTrackingDenied.c
    // PORTED 1-1: UJDebug.Log("[doEventRequestATTrackingDenied]"[idx 13067]);
    //   CallMethod("ProcessBase"[9214], "doEventRequestATTrackingDenied"[16188], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void doEventRequestATTrackingDenied(string[] args)
    {
        UJDebug.Log("[doEventRequestATTrackingDenied]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventRequestATTrackingDenied", arr);
    }

    // RVA: 0x15E0758  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGooglePlayApiConnected.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGooglePlayApiConnected"[16158], new object[1]{_LuaClass}).
    public void doEventGooglePlayApiConnected()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGooglePlayApiConnected", arr);
    }

    // RVA: 0x15E0834  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGooglePlayApiConnectionSuspended.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGooglePlayApiConnectionSuspended"[16159], new object[1]{_LuaClass}).
    public void doEventGooglePlayApiConnectionSuspended()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGooglePlayApiConnectionSuspended", arr);
    }

    // RVA: 0x15E0910  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGooglePlayLoginSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGooglePlayLoginSuccess"[16162], new object[2]{_LuaClass, args}).
    public void doEventGooglePlayLoginSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGooglePlayLoginSuccess", arr);
    }

    // RVA: 0x15E0A24  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGooglePlayLoginFail.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGooglePlayLoginFail"[16161], new object[2]{_LuaClass, args}).
    public void doEventGooglePlayLoginFail(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGooglePlayLoginFail", arr);
    }

    // RVA: 0x15E0B38  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGooglePlayLoginCancel.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGooglePlayLoginCancel"[16160], new object[2]{_LuaClass, args}).
    public void doEventGooglePlayLoginCancel(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGooglePlayLoginCancel", arr);
    }

    // RVA: 0x15E0C4C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGooglePlayRevokeAccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGooglePlayRevokeAccess"[16163],
    //   new object[2]{_LuaClass, args}).
    // SIGNATURE FIX: Ghidra signature is `(BaseProcLua*, String[], MethodInfo*)` — prior stub
    // declared `()` no-args which mismatched Main.cs call site `bpl.doEventGooglePlayRevokeAccess(args)`.
    public void doEventGooglePlayRevokeAccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGooglePlayRevokeAccess", arr);
    }

    // RVA: 0x15E0D60  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGameCenterAuthenticateSucceeded.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGameCenterAuthenticateSucceeded"[16154], new object[1]{_LuaClass}).
    public void doEventGameCenterAuthenticateSucceeded()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGameCenterAuthenticateSucceeded", arr);
    }

    // RVA: 0x15E0E3C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGameCenterAuthenticateFailed.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGameCenterAuthenticateFailed"[16153], new object[1]{_LuaClass}).
    public void doEventGameCenterAuthenticateFailed()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGameCenterAuthenticateFailed", arr);
    }

    // RVA: 0x15E0F18  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventLoadAchievementsDone.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventLoadAchievementsDone"[16170], new object[1]{_LuaClass}).
    public void doEventLoadAchievementsDone()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventLoadAchievementsDone", arr);
    }

    // RVA: 0x15E0FF4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doAppleEventSIWASucceeded.c
    // PORTED 1-1: canonical pattern matching all doAppleEvent* / doEvent* siblings.
    // CallMethod("ProcessBase", "doAppleEventSIWASucceeded", new object[2]{_LuaClass, args}).
    // FUN_032a5a98 is the IL2CPP tail-call helper for Util.CallMethod dispatch — confirmed by
    // identical structure to doAppleEventpasswordSucceeded (RVA 0x15E1108), doAppleEventSIWAFailed
    // (RVA 0x15E121C), and doAppleEventSIWACanceled (RVA 0x15E1330) which all decompile to the
    // 2-element object[] CallMethod pattern.
    public void doAppleEventSIWASucceeded(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doAppleEventSIWASucceeded", arr);
    }

    // RVA: 0x15E1108  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doAppleEventpasswordSucceeded.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doAppleEventpasswordSucceeded"[16146], new object[2]{_LuaClass, args}).
    public void doAppleEventpasswordSucceeded(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doAppleEventpasswordSucceeded", arr);
    }

    // RVA: 0x15E121C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doAppleEventSIWAFailed.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doAppleEventSIWAFailed"[16144], new object[2]{_LuaClass, args}).
    public void doAppleEventSIWAFailed(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doAppleEventSIWAFailed", arr);
    }

    // RVA: 0x15E1330  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doAppleEventSIWACanceled.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doAppleEventSIWACanceled"[16143], new object[2]{_LuaClass, args}).
    public void doAppleEventSIWACanceled(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doAppleEventSIWACanceled", arr);
    }

    // RVA: 0x15E1444  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventHandleLostGold.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventHandleLostGold"[16165], new object[2]{_LuaClass, args}).
    public void doEventHandleLostGold(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventHandleLostGold", arr);
    }

    // RVA: 0x15E1558  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventPurchaseUpdated.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventPurchaseUpdated"[16180], new object[2]{_LuaClass, args}).
    public void doEventPurchaseUpdated(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventPurchaseUpdated", arr);
    }

    // RVA: 0x15E166C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventPGSSupported.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventPGSSupported"[16175], new object[1]{_LuaClass}).
    public void doEventPGSSupported()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventPGSSupported", arr);
    }

    // RVA: 0x15E1748  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doEventGamePromotionSupported.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doEventGamePromotionSupported"[16155], new object[1]{_LuaClass}).
    public void doEventGamePromotionSupported()
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doEventGamePromotionSupported", arr);
    }

    // RVA: 0x15E1824  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/Notify.c
    // PORTED 1-1: Ghidra .c for this RVA forwards to the (string, int, string, string, long time)
    // overload after `time = DateTime.Now.AddSeconds(nextSecond)` -> DateTimeToMillis. Since the
    // dedicated .c file only contains the `long time` variant (0x15E18E4), the other RVAs are
    // tail-call shims:
    //   time = MarsTools.DateTimeToMillis(DateTime.Now.AddSeconds(nextSecond));
    //   Notify(iconName, notifyID, title, msg, time);
    public static void Notify(string iconName, int notifyID, string title, string msg, int nextSecond)
    {
        DateTime when = DateTime.Now.AddSeconds(nextSecond);
        long time = MarsSDK.MarsTools.DateTimeToMillis(when);
        Notify(iconName, notifyID, title, msg, time);
    }

    // RVA: 0x15E1978  Ghidra: same Notify.c — date-component overload tails into long-time overload.
    // PORTED 1-1:
    //   DateTime when = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
    //   time = MarsTools.DateTimeToMillis(when);
    //   Notify(iconName, notifyID, title, msg, time);
    // Note: Ghidra's NotifyWithImage uses DateTimeKind=2 (Utc) for ctor; consistent here.
    public static void Notify(string iconName, int notifyID, string title, string msg, int year, int month, int day, int hour, int minute, int second)
    {
        DateTime when = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        long time = MarsSDK.MarsTools.DateTimeToMillis(when);
        Notify(iconName, notifyID, title, msg, time);
    }

    // RVA: 0x15E18E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/Notify.c (canonical body)
    // PORTED 1-1:
    //   inst = NotificationManager.Instance();
    //   if (inst != null) inst.RegisterNotifyOnAndroid(channelID, notifyID, title, msg, iconName, time);
    //   else FUN_015cb8fc (trap).
    // Ghidra reads channelID via PTR_DAT_034464d0 (unidentified static class, first static field at 0xb8+0).
    // TODO: identify PTR_DAT_034464d0 (used here + NotifyWithImage + NotifyWithBanner + GetTreasureWebViewCK).
    //        For now we pass empty string — Android NotificationChannel ID is optional pre-API-26.
    public static void Notify(string iconName, int notifyID, string title, string msg, long time)
    {
        MarsSDK.Notification.NotificationManager inst = MarsSDK.Notification.NotificationManager.Instance();
        if (inst != null)
        {
            inst.RegisterNotifyOnAndroid("", notifyID, title, msg, iconName, time);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E1A8C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/NotifyWithImage.c
    // PORTED 1-1:
    //   DateTime when = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
    //   long time = MarsTools.DateTimeToMillis(when);
    //   inst = NotificationManager.Instance();
    //   if (inst != null) inst.RegisterNotifyWithImageOnAndroid(channelID, notifyID, title, msg, iconName, time, imageData);
    //   else FUN_015cb8fc.
    public static void NotifyWithImage(string iconName, int notifyID, string title, string msg, int year, int month, int day, int hour, int minute, int second, byte[] imageData)
    {
        DateTime when = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        long time = MarsSDK.MarsTools.DateTimeToMillis(when);
        MarsSDK.Notification.NotificationManager inst = MarsSDK.Notification.NotificationManager.Instance();
        if (inst != null)
        {
            inst.RegisterNotifyWithImageOnAndroid("", notifyID, title, msg, iconName, time, imageData);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E1BB0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/NotifyWithBanner.c
    // PORTED 1-1:
    //   DateTime when = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
    //   long time = MarsTools.DateTimeToMillis(when);
    //   inst = NotificationManager.Instance();
    //   if (inst != null) inst.RegisterNotifyWithBannerOnAndroid(channelID, notifyID, iconName, time, imageData, bigImageData, headsUpImageData);
    //   else FUN_015cb8fc.
    public static void NotifyWithBanner(string iconName, int notifyID, int year, int month, int day, int hour, int minute, int second, byte[] imageData, byte[] bigImageData, byte[] headsUpImageData)
    {
        DateTime when = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        long time = MarsSDK.MarsTools.DateTimeToMillis(when);
        MarsSDK.Notification.NotificationManager inst = MarsSDK.Notification.NotificationManager.Instance();
        if (inst != null)
        {
            inst.RegisterNotifyWithBannerOnAndroid("", notifyID, iconName, time, imageData, bigImageData, headsUpImageData);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E1CCC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CancelNotify.c
    // PORTED 1-1:
    //   inst = NotificationManager.Instance();
    //   if (inst != null) inst.CancelNotify(notifyID, "")  // Ghidra hardcodes empty literal — title arg dropped
    //   else FUN_015cb8fc (non-returning trap).
    // NOTE: Ghidra body passes PTR_StringLiteral_0 ("") not the user's `title` param — faithful port.
    public static void CancelNotify(int notifyID = -1, string title = "")
    {
        MarsSDK.Notification.NotificationManager inst = MarsSDK.Notification.NotificationManager.Instance();
        if (inst != null)
        {
            inst.CancelNotify(notifyID, "");
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E1D28  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CancelAllNotify.c
    // PORTED 1-1:
    //   inst = NotificationManager.Instance();
    //   if (inst != null) inst.CancelAllNotify();
    //   else FUN_015cb8fc (non-returning trap).
    public static void CancelAllNotify()
    {
        MarsSDK.Notification.NotificationManager inst = MarsSDK.Notification.NotificationManager.Instance();
        if (inst != null)
        {
            inst.CancelAllNotify();
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E1D48  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetNotifyDateTime.c
    // PORTED 1-1:
    //   result = new int[6] (FUN_015cb754 with size 6, indexed via lVar12 + 0x20..0x34)
    //   now = DateTime.Now
    //   switch (iMode):
    //     2: weekly — if today's DayOfWeek matches iNextTime AND hour > iNextTime2 or hour==iNextTime2 && minute>=iNextTime3 → add 7 days
    //                  else add (iNextTime - today.DayOfWeek + (today<iNextTime ? 0 : 7)) days
    //                  fill result[0..4] = year,month,day,hour=iNextTime2,minute=iNextTime3 (no second)
    //     3: daily — if hour matches iNextTime AND minute >= iNextTime2 → add 1 day
    //                  else if hour > iNextTime: add 1 day
    //                  fill result[0..4] = year,month,day,hour=iNextTime,minute=iNextTime2
    //     4/5/6: AddSeconds(iNextTime) — fill all 6 components
    //     7/8/9: AddDays(2/3/7) — fill year,month,day,hour=iNextTime (5 components)
    //     default: no fill (result returned as zero-init)
    //   if (iTimeOffset != 0 AND iMode in {2,3,7,8,9}): construct DateTime from result, AddSeconds(iTimeOffset), refill.
    // Ghidra DayOfWeek mapping: Sunday=0 becomes 7 internally.
    public static int[] GetNotifyDateTime(int iMode, int iNextTime, int iNextTime2 = 0, int iNextTime3 = 0, int iTimeOffset = 0)
    {
        int[] result = new int[6];
        DateTime now = DateTime.Now;
        int dowRaw = (int)now.DayOfWeek;
        int dow = dowRaw == 0 ? 7 : dowRaw;
        DateTime target = now;
        switch (iMode)
        {
            case 2: // weekly
                if (dow == iNextTime)
                {
                    int hour = now.Hour;
                    if (hour == iNextTime2)
                    {
                        int minute = now.Minute;
                        if (iNextTime3 <= minute)
                        {
                            target = now.AddDays(7.0);
                        }
                    }
                    else if (iNextTime2 < hour)
                    {
                        target = now.AddDays(7.0);
                    }
                }
                else
                {
                    int diff = iNextTime - dow;
                    if (iNextTime <= dow) diff += 7;
                    target = now.AddDays((double)diff);
                }
                result[0] = target.Year;
                result[1] = target.Month;
                result[2] = target.Day;
                result[3] = iNextTime2;
                result[4] = iNextTime3;
                break;
            case 3: // daily
                {
                    int hour = now.Hour;
                    if (hour == iNextTime)
                    {
                        int minute = now.Minute;
                        if (iNextTime2 <= minute)
                        {
                            target = now.AddDays(1.0);
                        }
                    }
                    else if (iNextTime < hour)
                    {
                        target = now.AddDays(1.0);
                    }
                    result[0] = target.Year;
                    result[1] = target.Month;
                    result[2] = target.Day;
                    result[3] = iNextTime;
                    result[4] = iNextTime2;
                }
                break;
            case 4:
            case 5:
            case 6: // seconds offset
                target = now.AddSeconds((double)iNextTime);
                result[0] = target.Year;
                result[1] = target.Month;
                result[2] = target.Day;
                result[3] = target.Hour;
                result[4] = target.Minute;
                result[5] = target.Second;
                break;
            case 7: // +2 days
                target = now.AddDays(2.0);
                result[0] = target.Year;
                result[1] = target.Month;
                result[2] = target.Day;
                result[3] = iNextTime;
                break;
            case 8: // +3 days
                target = now.AddDays(3.0);
                result[0] = target.Year;
                result[1] = target.Month;
                result[2] = target.Day;
                result[3] = iNextTime;
                break;
            case 9: // +7 days
                target = now.AddDays(7.0);
                result[0] = target.Year;
                result[1] = target.Month;
                result[2] = target.Day;
                result[3] = iNextTime;
                break;
            default:
                break;
        }
        if (iTimeOffset != 0)
        {
            // Apply offset to modes 2,3,7,8,9 (Ghidra: !=0 && !=1 && >2 && !=3 && >4 && !=5).
            if (iMode != 0 && iMode != 1 && iMode > 2 && iMode != 3 && iMode > 4 && iMode != 5)
            {
                DateTime adj = new DateTime(result[0], result[1], result[2], result[3], result[4], result[5], DateTimeKind.Utc);
                adj = adj.AddSeconds((double)iTimeOffset);
                result[0] = adj.Year;
                result[1] = adj.Month;
                result[2] = adj.Day;
                result[3] = adj.Hour;
                result[4] = adj.Minute;
                result[5] = adj.Second;
            }
        }
        return result;
    }

    // RVA: 0x15E23A8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessImagePersonalUploadSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessImagePersonalUploadSuccess"[16249], new object[2]{_LuaClass, args}).
    public void doMsgProcessImagePersonalUploadSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessImagePersonalUploadSuccess", arr);
    }

    // RVA: 0x15E24BC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessImagePersonalUploadFail.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessImagePersonalUploadFail"[16248], new object[2]{_LuaClass, args}).
    public void doMsgProcessImagePersonalUploadFail(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessImagePersonalUploadFail", arr);
    }

    // RVA: 0x15E25D0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessImageMessageUploadSuccess.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessImageMessageUploadSuccess"[16246], new object[2]{_LuaClass, args}).
    public void doMsgProcessImageMessageUploadSuccess(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessImageMessageUploadSuccess", arr);
    }

    // RVA: 0x15E26E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessImageMessageUploadFail.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessImageMessageUploadFail"[16245], new object[1]{_LuaClass}).
    // NOTE: Ghidra uses object[1] (1-entry) — does NOT pass `args` (arg list of size 1 not 2).
    public void doMsgProcessImageMessageUploadFail(string[] args)
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessImageMessageUploadFail", arr);
    }

    // RVA: 0x15E27C0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessUserRuleNeedsUpdate.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessUserRuleNeedsUpdate"[16272], new object[1]{_LuaClass}).
    // NOTE: Ghidra uses object[1] (1-entry).
    public void doMsgProcessUserRuleNeedsUpdate(string[] args)
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessUserRuleNeedsUpdate", arr);
    }

    // RVA: 0x15E289C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessGetUJWebURL.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessGetUJWebURL"[16244], new object[1]{_LuaClass}).
    // NOTE: Ghidra uses object[1] (1-entry).
    public void doMsgProcessGetUJWebURL(string[] args)
    {
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessGetUJWebURL", arr);
    }

    // RVA: 0x15E2978  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/doMsgProcessGetSettingComplete.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "doMsgProcessGetSettingComplete"[16243], new object[2]{_LuaClass, args}).
    public void doMsgProcessGetSettingComplete(string[] args)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "doMsgProcessGetSettingComplete", arr);
    }

    // RVA: 0x15E2A8C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/StartWebView.c
    // PORTED 1-1:
    //   inst = MarsSDK.Platform.UserjoyPlatform.Instance();
    //   setIDStr = iSetID.ToString();
    //   webTypeStr = iUJWebType.ToString();
    //   if (inst != null) inst.StartUJWeb(sPlayerName, setIDStr, webTypeStr, lan, ctype, characterid);
    //   else FUN_015cb8fc (trap).
    public static void StartWebView(string sPlayerName, int iSetID, int iUJWebType, string lan = "", string ctype = "", string characterid = "")
    {
        MarsSDK.Platform.UserjoyPlatform inst = MarsSDK.Platform.UserjoyPlatform.Instance();
        string setIDStr = iSetID.ToString();
        string webTypeStr = iUJWebType.ToString();
        if (inst != null)
        {
            inst.StartUJWeb(sPlayerName, setIDStr, webTypeStr, lan, ctype, characterid);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E2B18  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnImagePicked.c
    // PORTED 1-1:
    //   if (string.IsNullOrEmpty(path)) failMsg = "Cannot find path of picked image."[3828]; else
    //     tex = NativeGallery.LoadImageAtPath(path, -1, false, true, false);
    //     if (tex == null) failMsg = "Load image error."[7633];
    //     else { crop-to-square if w != h, create Sprite, CallMethod("ProcessBase", "OnImagePicked"[8701], new object[]{_LuaClass, sprite}); return; }
    //   on failure: Debug.LogError(failMsg); CallMethod("ProcessBase", "OnImagePicked"[8701], new object[1]{_LuaClass});
    // Note: Ghidra crops to square taking the smaller dimension as size, centered. The complex
    //   pixel-rebuild loop replicates GetPixels + SetPixels via Color array indexing on the cropped region.
    public void OnImagePicked(string path)
    {
        UnityEngine.Sprite sprite = null;
        bool ok = false;
        string failMsg = null;
        if (string.IsNullOrEmpty(path))
        {
            failMsg = "Cannot find path of picked image.";
        }
        else
        {
            UnityEngine.Texture2D tex = NativeGallery.LoadImageAtPath(path, -1, false, true, false);
            if (tex == null)
            {
                failMsg = "Load image error.";
            }
            else
            {
                UnityEngine.Texture2D outTex = tex;
                int w = tex.width;
                int h = tex.height;
                if (w != h)
                {
                    int side = h < w ? h : w;
                    outTex = new UnityEngine.Texture2D(side, side, UnityEngine.TextureFormat.RGB24, false);
                    int diff = w - h;
                    int absDiff = diff < 0 ? -diff : diff;
                    UnityEngine.Color[] srcPixels = tex.GetPixels();
                    UnityEngine.Color[] dstPixels = new UnityEngine.Color[side * side];
                    int half = absDiff / 2;
                    bool wIsWider = w >= h;
                    int srcIdx = 0;
                    for (int i = 0; i < side * side; i++)
                    {
                        // Compute (col,row) in cropped square; pull from offset source pixel.
                        int col = wIsWider ? (i % side) : (i % side);
                        int row = wIsWider ? (i / side) : (i / side);
                        int srcCol = wIsWider ? (col + half) : col;
                        int srcRow = wIsWider ? row : (row + half);
                        if (srcCol < 0 || srcCol >= w || srcRow < 0 || srcRow >= h) continue;
                        if (srcIdx >= srcPixels.Length) break;
                        dstPixels[i] = srcPixels[srcRow * w + srcCol];
                        srcIdx++;
                    }
                    outTex.SetPixels(dstPixels);
                    outTex.Apply();
                    UnityEngine.Object.Destroy(tex);
                }
                int outW = outTex.width;
                int outH = outTex.height;
                sprite = UnityEngine.Sprite.Create(outTex, new UnityEngine.Rect(0f, 0f, (float)outW, (float)outH), new UnityEngine.Vector2(0.5f, 0.5f));
                ok = true;
            }
        }
        if (!ok && failMsg != null)
        {
            UnityEngine.Debug.LogError(failMsg);
            object[] arr1 = new object[1];
            arr1[0] = this._LuaClass;
            LuaFramework.Util.CallMethod("ProcessBase", "OnImagePicked", arr1);
            return;
        }
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = sprite;
        LuaFramework.Util.CallMethod("ProcessBase", "OnImagePicked", arr);
    }

    // RVA: 0x15E30A0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetImageFromGallery.c
    // PORTED 1-1:
    //   if (NativeGallery.CheckPermission(Read, Image) == Denied) NativeGallery.RequestPermission(Read, Image);
    //   if (CheckPermission == Granted) {
    //     cached static callback PTR_DAT_03449a88 (a NativeGallery.MediaPickCallback);
    //     if (callback == null) callback = new MediaPickCallback(this, OnImagePicked);
    //     NativeGallery.GetImageFromGallery(callback, ""[idx 0], "image/*"[idx 17259]);
    //   } else Debug.LogError("Read permission request denined."[idx 9349]);
    // PTR_DAT_03449a88 holds a static MediaPickCallback delegate slot. We model it as a static field.
    private static NativeGallery.MediaPickCallback _galleryPickCallback;
    public static void GetImageFromGallery()
    {
        NativeGallery.Permission p = NativeGallery.CheckPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        if (p == NativeGallery.Permission.ShouldAsk)
        {
            NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        }
        p = NativeGallery.CheckPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        if (p == NativeGallery.Permission.Granted)
        {
            if (_galleryPickCallback == null && _instance != null)
            {
                _galleryPickCallback = new NativeGallery.MediaPickCallback(_instance.OnImagePicked);
            }
            NativeGallery.GetImageFromGallery(_galleryPickCallback, "", "image/*");
        }
        else
        {
            UnityEngine.Debug.LogError("Read permission request denined.");
        }
    }

    // RVA: 0x15E3230  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SaveScreenshotToGallery.c
    // PORTED 1-1:
    //   tex = ScreenCapture.CaptureScreenshotAsTexture();
    //   if (tex == null) { Debug.LogError("Screenshot capture failed."[9846]); return; }
    //   perm = NativeGallery.RequestPermission(Write, Image);
    //   if (perm == Granted) {
    //     name = "ScreenShot"[9842] + GRandom.GetRandom().ToString() + ".png"[973];
    //     NativeGallery.SaveImageToGallery(tex, "SGC"[9714], name);
    //   } else Debug.LogError("Write file permission request denied."[12632]);
    public static void SaveScreenshotToGallery()
    {
        UnityEngine.Texture2D tex = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
        if (tex == null)
        {
            UnityEngine.Debug.LogError("Screenshot capture failed.");
            return;
        }
        NativeGallery.Permission perm = NativeGallery.RequestPermission(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);
        if (perm == NativeGallery.Permission.Granted)
        {
            int rnd = GRandom.GetRandom();
            string name = "ScreenShot" + rnd.ToString() + ".png";
            NativeGallery.SaveImageToGallery(tex, "SGC", name, null);
        }
        else
        {
            UnityEngine.Debug.LogError("Write file permission request denied.");
        }
    }

    // RVA: 0x15E33B4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CheckStringLenInUTF8.c
    // PORTED 1-1:
    //   enc = Encoding.GetEncoding("utf-8"[20794]);
    //   bytes = enc.GetBytes(str);
    //   bOk = true; len1 = 0; len2 = 0; uVar9 = 0; isMb = false;
    //   for each byte b at index i:
    //     if (b & 0xc0) != 0x80:  // start of UTF-8 codepoint (not continuation)
    //        isMb = (sbyte)b < 0;  // top bit set -> multibyte
    //        len1 += (b >> 7) ^ 1;  // count ASCII
    //        len2 += (b >> 7);     // count multibyte
    //        widthApprox = (int)((double)len1 * DAT_008e3c20)  // weight constant ~0.5 likely
    //        if maxLen < widthApprox + len2*2: bOk = false; if b < 0x80 break to LAB_016e3568 else 016e3584
    //   final: if !bOk: if isMb { len1=count_after_loop; len2=mb_count-1; } else { len1=count-1; }
    //          else: len1=count;
    //   *len2 = mb;
    //   return enc.GetString(bytes, 0, uVar9);  // truncated string up to byte position uVar9
    // NOTE: Ghidra DAT_008e3c20 is a double constant ~0.5 (ASCII chars weigh ~0.5 in CJK width terms).
    //       Without exact value lookup we use 0.5; the IL2CPP body computes truncated width-fit substring.
    public static string CheckStringLenInUTF8(string str, int maxLen, out bool bOk, out int len1, out int len2)
    {
        bOk = true;
        len1 = 0;
        len2 = 0;
        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
        byte[] bytes = enc.GetBytes(str);
        int asciiCount = 0;
        int mbCount = 0;
        int byteIdx = 0;
        bool isMb = false;
        const double weight = 0.5;
        for (int i = 0; i < bytes.Length; i++)
        {
            byte b = bytes[i];
            if ((b & 0xC0) != 0x80)
            {
                isMb = ((sbyte)b) < 0;
                asciiCount += ((b >> 7) ^ 1);
                mbCount += (b >> 7);
                int widthApprox = (int)((double)asciiCount * weight);
                if (maxLen < widthApprox + mbCount * 2)
                {
                    bOk = false;
                    byteIdx = i;
                    if ((sbyte)b >= 0)
                    {
                        len1 = asciiCount - 1;
                    }
                    else
                    {
                        len1 = asciiCount;
                        mbCount--;
                    }
                    len2 = mbCount;
                    return enc.GetString(bytes, 0, byteIdx);
                }
            }
            byteIdx = i + 1;
        }
        // No truncation needed
        len1 = asciiCount;
        len2 = mbCount;
        return enc.GetString(bytes, 0, byteIdx);
    }

    // RVA: 0x15E35E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendAppsFlyerMessage.c
    // PORTED 1-1:
    //   UJDebug.Log("SendAppsFlyerMessage:"[9892] + mode);
    //   if (keys != null && Values != null && keys.Length == Values.Length) {
    //     dict = new Dictionary<string, object>();
    //     for i: dict.Add(keys[i], Values[i]);
    //     AppsFlyerSDK.AppsFlyer.sendEvent(mode, dict);
    //   } else if mismatch length: just return (Ghidra: early return when lengths differ).
    public static void SendAppsFlyerMessage(string mode, string[] keys, string[] Values)
    {
        UJDebug.Log("SendAppsFlyerMessage:" + mode);
        if (keys == null || Values == null)
        {
            throw new NullReferenceException();
        }
        if (keys.Length != Values.Length)
        {
            return;
        }
        Dictionary<string, string> dict = new Dictionary<string, string>();
        for (int i = 0; i < keys.Length; i++)
        {
            dict.Add(keys[i], Values[i]);
        }
        AppsFlyerSDK.AppsFlyer.sendEvent(mode, dict);
    }

    // RVA: 0x15E37A4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/PointIsOverUI.c
    // PORTED 1-1:
    //   results = new List<RaycastResult>();
    //   pointerData = new PointerEventData(EventSystem.current);
    //   pointerData.position = ScreenPos;
    //   results.Clear();
    //   EventSystem.current.RaycastAll(pointerData, results);
    //   return results.Count > 0.
    public static bool PointIsOverUI(Vector2 ScreenPos)
    {
        System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> results = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
        UnityEngine.EventSystems.PointerEventData ped = new UnityEngine.EventSystems.PointerEventData(es);
        ped.position = ScreenPos;
        results.Clear();
        UnityEngine.EventSystems.EventSystem cur = UnityEngine.EventSystems.EventSystem.current;
        if (cur == null) throw new NullReferenceException();
        cur.RaycastAll(ped, results);
        return results.Count > 0;
    }

    // RVA: 0x15E38F8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/IsDebugConsoleVisible.c
    // PORTED 1-1: forwards to Opencoding.Console.DebugConsole.IsVisible (static property).
    // Ghidra signature returns bool but body is `Opencoding_Console_DebugConsole__get_IsVisible(0); return;`
    // — the return value is implicit (Ghidra type propagation gave up; the function does return the call's value).
    //
    // ⚠️ TODO Phase H — Replace paid dev-tool DLLs:
    //   Asset:  TouchConsole Pro (Opencoding) — Unity Asset Store ID 25559 (~$30 USD)
    //   URL:    https://assetstore.unity.com/packages/tools/gui/touchconsole-pro-25559
    //   Why:    Production has these 4 DLLs (Opencoding.Console/CommandHandlers/LogHistory/Shared)
    //           but Cpp2IL stripped the bodies → .cctor throws AnalysisFailedException.
    //   Impact: Developer-only console (GM command terminal). Production user-facing logic
    //           expects `IsVisible == false` 99% of the time. Returning false from this stub
    //           matches production steady-state behavior; only `~`-key admin command path
    //           lost (not used in login → char select flow).
    //   Catch is type-specific (TypeInitializationException) — NOT generic catch-all.
    public static bool IsDebugConsoleVisible()
    {
        try
        {
            return Opencoding.Console.DebugConsole.IsVisible;
        }
        catch (System.TypeInitializationException)
        {
            // Cpp2IL-stubbed .cctor in Opencoding.Console.dll throws. Replace DLL via Phase H
            // (purchase TouchConsole Pro). For now: console-hidden default matches production.
            return false;
        }
    }

    // RVA: 0x15E3948  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SetDebugConsoleVisible.c
    // PORTED 1-1:
    //   inst = WndForm_DebugConsole.s_instance  // PTR_DAT_03449af8 = WndForm_DebugConsole metadata
    //   if (inst != null) inst.SetDebugConsoleVisible(enable, false);
    //   else throw (FUN_015cb8fc non-returning).
    public static void SetDebugConsoleVisible(bool enable)
    {
        WndForm_DebugConsole inst = WndForm_DebugConsole.Instance;
        if (inst == null) throw new NullReferenceException();
        inst.SetDebugConsoleVisible(enable, false);
    }

    // RVA: 0x15E39A8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/Event_NameTagClickListener.c
    // PORTED 1-1:
    //   if (eventObj == null) FUN_015cb8fc;
    //   trigger = eventObj.GetComponent<EventTrigger>();
    //   if (trigger == null) trigger = eventObj.AddComponent<EventTrigger>();
    //   trigger.triggers.Clear();
    //   entry = new EventTrigger.Entry();
    //   entry.eventID = (EventTriggerType)4;  // PointerClick
    //   entry.callback = new TriggerEvent();
    //   cb = new NameTagClickEventCallBack(UID);
    //   action = new UnityAction<BaseEventData>(cb, EventCallback);
    //   entry.callback.AddListener(action);
    //   trigger.triggers.Add(entry);
    public static void Event_NameTagClickListener(GameObject eventObj, int UID)
    {
        if (eventObj == null) throw new NullReferenceException();
        UnityEngine.EventSystems.EventTrigger trigger = eventObj.GetComponent<UnityEngine.EventSystems.EventTrigger>();
        if (trigger == null)
        {
            trigger = eventObj.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        }
        System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry> triggers = trigger.triggers;
        triggers.Clear();
        UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry();
        entry.eventID = (UnityEngine.EventSystems.EventTriggerType)4;
        entry.callback = new UnityEngine.EventSystems.EventTrigger.TriggerEvent();
        NameTagClickEventCallBack cb = new NameTagClickEventCallBack(UID);
        UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData> action =
            new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>(cb.OnEvent);
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    // RVA: 0x15E3C58  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetMacAddress.c
    // PORTED 1-1:
    //   result = "";
    //   nics = NetworkInterface.GetAllNetworkInterfaces();
    //   foreach (nic in nics):
    //     if (nic == null) trap;
    //     if (nic.NetworkInterfaceType == 6 /*Ethernet*/):
    //         if (nic.OperationalStatus == 1 /*Up*/):
    //             addr = nic.GetPhysicalAddress();
    //             return "" + addr.ToString();
    //         else: Debug.LogWarning("GetAllNetworkInterfaces Status: "[5708] + nic.OperationalStatus.ToString());
    //     else: Debug.LogWarning("GetAllNetworkInterfaces Type: "[5709] + nic.NetworkInterfaceType.ToString());
    //   if iterated all without Ethernet+Up: return "" (Ghidra falls through to return uVar12 = "")
    public static string GetMacAddress()
    {
        string result = "";
        System.Net.NetworkInformation.NetworkInterface[] nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        if (nics == null) throw new NullReferenceException();
        for (int i = 0; i < nics.Length; i++)
        {
            System.Net.NetworkInformation.NetworkInterface nic = nics[i];
            if (nic == null) throw new NullReferenceException();
            if (nic.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Ethernet)
            {
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    System.Net.NetworkInformation.PhysicalAddress addr = nic.GetPhysicalAddress();
                    if (addr == null) throw new NullReferenceException();
                    return string.Concat(result, addr.ToString());
                }
                UnityEngine.Debug.LogWarning("GetAllNetworkInterfaces Status: " + nic.OperationalStatus.ToString());
            }
            else
            {
                UnityEngine.Debug.LogWarning("GetAllNetworkInterfaces Type: " + nic.NetworkInterfaceType.ToString());
            }
        }
        return result;
    }

    // RVA: 0x15E3F34  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetCurrentUnixTimeString.c
    // PORTED 1-1:
    //   epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);   // 0x7b2 = 1970, kind=1=Utc
    //   span = DateTime.UtcNow - epoch;
    //   secs = (long)span.TotalSeconds;  // double cast to long; INFINITY → long.MinValue (Ghidra branch)
    //   return secs.ToString();
    public static string GetCurrentUnixTimeString()
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan span = DateTime.UtcNow - epoch;
        double total = span.TotalSeconds;
        long secs;
        if (total == double.PositiveInfinity)
        {
            secs = long.MinValue;
        }
        else
        {
            secs = (long)total;
        }
        return secs.ToString();
    }

    // RVA: 0x15E402C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetTreasureWebViewCK.c
    // PORTED 1-1: returns **(undefined8 **)(*(long *)PTR_DAT_034464d0 + 0xb8) — the first static
    // field of the (unidentified) class metadata at 0x034464d0. The `param` argument is unused
    // in the IL2CPP body — it's purely a static-field reader.
    // TODO: identify PTR_DAT_034464d0 (shared with Notify family; likely a static config string).
    //       Returning empty string preserves behavior for stub-init builds.
    public static string GetTreasureWebViewCK(string param)
    {
        return "";
    }

    // RVA: 0x15E4074  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/Int32ToDWORD.c
    // PORTED 1-1: trivial bit-cast int → uint (IL2CPP method body is essentially the conv.u4 op).
    // Ghidra emits "void return" because the type propagation gave up on the param/return registers,
    // but Signature comment in .c shows uint32_t Int32ToDWORD(int32_t nNum) — passthrough.
    public static uint Int32ToDWORD(int nNum)
    {
        return (uint)nNum;
    }

    // RVA: 0x15E4078  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/DWORDToInt32.c
    // PORTED 1-1: trivial bit-cast uint → int.
    public static int DWORDToInt32(uint dwNum)
    {
        return (int)dwNum;
    }

    // RVA: 0x15E407C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/Int16ToWORD.c
    // PORTED 1-1: trivial bit-cast short → ushort.
    public static ushort Int16ToWORD(short nNum)
    {
        return (ushort)nNum;
    }

    // RVA: 0x15E4080  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/WORDToInt16.c
    // PORTED 1-1: trivial bit-cast ushort → short.
    public static short WORDToInt16(ushort wNum)
    {
        return (short)wNum;
    }

    // RVA: 0x15E4084  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/BYTEToSBYTE.c
    // PORTED 1-1: trivial bit-cast byte → sbyte.
    public static sbyte BYTEToSBYTE(byte cNum)
    {
        return (sbyte)cNum;
    }

    // RVA: 0x15E4088  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SBYTEToBYTE.c
    // PORTED 1-1: trivial bit-cast sbyte → byte.
    public static byte SBYTEToBYTE(sbyte cNum)
    {
        return (byte)cNum;
    }

    // RVA: 0x15E408C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/DWORDLONG_LEFT.c
    // PORTED 1-1: high 32 bits of ulong — Ghidra body: "return param_1 >> 0x20;"
    public static uint DWORDLONG_LEFT(ulong llNum)
    {
        return (uint)(llNum >> 0x20);
    }

    // RVA: 0x15E4094  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/DWORDLONG_RIGHT.c
    // PORTED 1-1: low 32 bits of ulong (Ghidra emits void-return because of register reuse, but
    // by naming convention symmetric to DWORDLONG_LEFT this returns the lower 32 bits).
    public static uint DWORDLONG_RIGHT(ulong llNum)
    {
        return (uint)llNum;
    }

    // RVA: 0x15E4098  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetTraceDataPrt.c
    // PORTED 1-1: Ghidra body translated to C#. The static fields _tracePlayer_header (proto_COMM at
    // field offset 0x20), _tracePlayer_protocBuf (byte[] at 0x28), _tracePlayer_protocBuf2 (byte[] at 0x30)
    // are accessed via *(long *)(lVar8 + 0xb8) + offset in the Ghidra code; mapped directly to the
    // C# static fields. proto_COMM.m_pcProtoco (offset +0x20 in struct view = 0 in struct) maps to
    // m_pcProtoco; +0x22 = m_pcSize; +0x24 = m_pcCompressSize. Buffer size 65000 from FUN_015cb754 calls.
    public static NetReceivePackerBase GetTraceDataPrt(NetReceivePackerBase srcPrt, int msgCompressSize, int msgSize, bool noEncrypt, bool noCompress, bool keep, out int protocol)
    {
        protocol = 0;

        // Ensure _tracePlayer_protocBuf is allocated.
        if (_tracePlayer_protocBuf == null)
        {
            _tracePlayer_protocBuf = new byte[65000];
        }

        if (_tracePlayer_protocBuf == null)
        {
            throw new System.NullReferenceException();
        }
        System.Array.Clear(_tracePlayer_protocBuf, 0, _tracePlayer_protocBuf.Length);

        if (msgCompressSize < 1)
        {
            // No compression: copy directly from srcPrt data.
            if (srcPrt == null)
            {
                throw new System.NullReferenceException();
            }
            byte[] srcData = srcPrt.getData();
            int srcStart = srcPrt.getStartIndex();
            System.Array.Copy(srcData, srcStart, _tracePlayer_protocBuf, 0, msgSize);
        }
        else
        {
            if (srcPrt == null)
            {
                throw new System.NullReferenceException();
            }
            byte[] srcData = srcPrt.getData();
            int srcStart = srcPrt.getStartIndex();
            int uncompLen = Zip.UncompressMemoryToMemoryZIP(
                _tracePlayer_protocBuf, 0, _tracePlayer_protocBuf.Length,
                srcData, srcStart, msgCompressSize);
            if (uncompLen != msgSize)
            {
                return null;
            }
        }

        // Ensure _tracePlayer_protocBuf2 allocated.
        if (_tracePlayer_protocBuf2 == null)
        {
            _tracePlayer_protocBuf2 = new byte[65000];
        }

        if (msgSize <= 5)
        {
            return null;
        }

        // Read header from _tracePlayer_protocBuf into _tracePlayer_header.
        // noEncrypt flag: if false → blockEncrypt happens in readFromByteArray; if true → skip encryption.
        if (!noEncrypt)
        {
            ushort crcUnused;
            _tracePlayer_header.readFromByteArray(out crcUnused, _tracePlayer_protocBuf, 0, false);
            protocol = (int)_tracePlayer_header.m_pcProtoco;
            // Cap on size (0xfde2 = 65000-6 = MAX_SENDDATA_LEN-6 check).
            if (_tracePlayer_header.m_pcSize > 0xfde2)
            {
                return null;
            }
        }
        else
        {
            ushort crcUnused;
            _tracePlayer_header.readFromByteArray(out crcUnused, _tracePlayer_protocBuf, 0, true);
            protocol = (int)_tracePlayer_header.m_pcProtoco;
        }

        if (noCompress)
        {
            if (_tracePlayer_header.m_pcCompressSize != 0)
            {
                return null;
            }
        }

        ushort hdrSize = _tracePlayer_header.m_pcSize;
        ushort hdrCompressSize = _tracePlayer_header.m_pcCompressSize;

        // Two branches based on whether body is compressed (hdrCompressSize != 0):
        if (hdrCompressSize == 0)
        {
            // Uncompressed body — copy 6..msgSize-6 bytes verbatim.
            if (hdrSize + 6 != msgSize)
            {
                return null;  // size mismatch
            }
            if (_tracePlayer_protocBuf2 == null)
            {
                throw new System.NullReferenceException();
            }
            System.Array.Clear(_tracePlayer_protocBuf2, 0, _tracePlayer_protocBuf2.Length);
            System.Array.Copy(_tracePlayer_protocBuf, 6, _tracePlayer_protocBuf2, 0, (int)hdrSize);
            if (!noEncrypt)
            {
                proto_COMM.blockEncrypt(_tracePlayer_protocBuf2, 0, (int)hdrSize);
            }
        }
        else
        {
            // Compressed body — decompress into _tracePlayer_protocBuf2.
            if (hdrCompressSize + 6 != msgSize)
            {
                return null;
            }
            if (_tracePlayer_protocBuf2 == null)
            {
                throw new System.NullReferenceException();
            }
            System.Array.Clear(_tracePlayer_protocBuf2, 0, _tracePlayer_protocBuf2.Length);
            int decompLen = Zip.UncompressMemoryToMemoryZIP(
                _tracePlayer_protocBuf2, 0, _tracePlayer_protocBuf2.Length,
                _tracePlayer_protocBuf, 6, (int)hdrCompressSize);
            if (decompLen != hdrSize)
            {
                return null;
            }
        }

        // keep flag: when true, allocate a fresh 65000-byte buffer and copy buf2 into it,
        //   so caller can retain the data after subsequent calls overwrite buf2.
        // When false, hand out buf2 directly (reused on next call).
        byte[] outBuf;
        if (!keep)
        {
            outBuf = _tracePlayer_protocBuf2;
        }
        else
        {
            outBuf = new byte[65000];
            System.Array.Copy(_tracePlayer_protocBuf2, 0, outBuf, 0, 65000);
        }
        return new NetReceivePackerBase(outBuf);
    }

    // RVA: 0x15E45BC + 0x15E4614  Ghidra: get_ScreenHalfPixel.c + set_ScreenHalfPixel.c
    // PORTED 1-1: Both getter/setter access `Main.bScreenHalfPixel` (Main static, offset 0x82).
    // Setter additionally calls `Main.Instance.SetupScreenSize()` to re-apply screen size.
    // PTR_DAT_03448380 = Main class metadata; static area offset 0x82 = bScreenHalfPixel; offset 0x8 = s_instance.
    public static bool ScreenHalfPixel
    {
        get { return Main.bScreenHalfPixel; }
        set
        {
            Main.bScreenHalfPixel = value;
            Main main = Main.Instance;
            if (main == null) throw new NullReferenceException();
            main.SetupScreenSize();
        }
    }

    // RVA: 0x15E46B8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/get_ScreenOriginWidth.c
    // PORTED 1-1: returns Main.iScreenOriginWidth (Main static, offset 0x44).
    public static int ScreenOriginWidth { get { return Main.iScreenOriginWidth; } }

    // RVA: 0x15E4710  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/get_ScreenOriginHeight.c
    // PORTED 1-1: returns Main.iScreenOriginHeight (Main static, offset 0x48).
    public static int ScreenOriginHeight { get { return Main.iScreenOriginHeight; } }

    // RVA: 0x15E4768  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/ConvertSpriteToTexture.c
    // PORTED 1-1:
    //   if (sprite == null) FUN_015cb8fc;
    //   rect = sprite.rect;
    //   srcTex = sprite.texture;
    //   if (srcTex == null) FUN_015cb8fc;
    //   if (rect.width == srcTex.width) return srcTex;
    //   else:
    //     dstTex = new Texture2D((int)rect.width, (int)rect.height);
    //     pixels = srcTex.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
    //     dstTex.SetPixels(pixels);
    //     dstTex.Apply();
    //     return dstTex;
    public static Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        if (sprite == null) throw new NullReferenceException();
        UnityEngine.Rect rect = sprite.rect;
        UnityEngine.Texture2D srcTex = sprite.texture;
        if (srcTex == null) throw new NullReferenceException();
        if ((float)srcTex.width == rect.width)
        {
            return srcTex;
        }
        UnityEngine.Texture2D dstTex = new UnityEngine.Texture2D((int)rect.width, (int)rect.height);
        UnityEngine.Color[] pixels = srcTex.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        dstTex.SetPixels(pixels);
        dstTex.Apply();
        return dstTex;
    }

    // RVA: 0x15E49FC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/EncodeToPNG.c
    // PORTED 1-1: forwards to UnityEngine.ImageConversion.EncodeToPNG(tex).
    public static byte[] EncodeToPNG(Texture2D tex)
    {
        return UnityEngine.ImageConversion.EncodeToPNG(tex);
    }

    // RVA: 0x15E4A04  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/EncodeToJPG.c
    // PORTED 1-1: forwards to UnityEngine.ImageConversion.EncodeToJPG(tex).
    public static byte[] EncodeToJPG(Texture2D tex)
    {
        return UnityEngine.ImageConversion.EncodeToJPG(tex);
    }

    // RVA: 0x15E4A0C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/ResizeTexture.c
    // PORTED 1-1:
    //   rt = new RenderTexture(targetWidth, targetHeight, 16);
    //   RenderTexture.active = rt;
    //   Graphics.Blit(tex, rt);
    //   if (!inplace) { result = new Texture2D(targetWidth, targetHeight); }
    //   else { tex.Reinitialize(targetWidth, targetHeight); result = tex; }
    //   result.ReadPixels(new Rect(0, 0, targetWidth, targetHeight), 0, 0);
    //   result.Apply();
    //   RenderTexture.active = null;
    //   rt.Release();
    //   return result;
    public static Texture2D ResizeTexture(Texture2D tex, int targetWidth, int targetHeight, bool inplace = true)
    {
        UnityEngine.RenderTexture rt = new UnityEngine.RenderTexture(targetWidth, targetHeight, 16);
        UnityEngine.RenderTexture.active = rt;
        UnityEngine.Graphics.Blit(tex, rt);
        UnityEngine.Texture2D result;
        if (!inplace)
        {
            result = new UnityEngine.Texture2D(targetWidth, targetHeight);
        }
        else
        {
            if (tex == null) throw new NullReferenceException();
            tex.Reinitialize(targetWidth, targetHeight);
            result = tex;
        }
        result.ReadPixels(new UnityEngine.Rect(0, 0, (float)targetWidth, (float)targetHeight), 0, 0);
        result.Apply();
        UnityEngine.RenderTexture.active = null;
        rt.Release();
        return result;
    }

    // RVA: 0x15E4B70  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/LoadHeadIcon.c
    // PORTED 1-1:
    //   if (BaseConnect.Instance == null) FUN_015cb8fc;
    //   url = MarsFunction.GetDSSDownloadURL() + "/"[idx 986] + downloadKey;
    //   bplua = BaseProcLua.Instance;
    //   monoLuaMgr = Main.Instance.LuaMgr;  (Ghidra reads Main static instance.LuaMgr at offset 0x8)
    //   if (bplua != null && monoLuaMgr != null) {
    //       coroutine = bplua.LoadHeadIconCoroutine(url, downloadKey);
    //       monoLuaMgr.StartCoroutine(coroutine);
    //   }
    //   return BaseConnect.Instance != null  (Ghidra: `return lVar4 != 0;` where lVar4 = BaseConnect.Instance)
    public static bool LoadHeadIcon(string downloadKey)
    {
        BaseConnect connInst = BaseConnect.Instance;
        if (connInst == null) throw new NullReferenceException();
        string url = MarsSDK.MarsFunction.GetDSSDownloadURL() + "/" + downloadKey;
        BaseProcLua bp = BaseProcLua.Instance;
        LuaFramework.LuaManager luaMgr = LuaFramework.LuaManager.Instance;
        if (bp != null && luaMgr != null)
        {
            System.Collections.IEnumerator co = bp.LoadHeadIconCoroutine(url, downloadKey);
            luaMgr.StartCoroutine(co);
        }
        return connInst != null;
    }

    // RVA: 0x15E4D8C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/LoadFacebookHeadIcon.c
    // PORTED 1-1:
    //   if (!MarsFunction.IsBindFacebook()) return false;
    //   fbInst = FacebookPlatform.Instance();
    //   if (fbInst == null) FUN_015cb8fc;
    //   url = fbInst.GetFacebookPhotoUri();
    //   Debug.Log("[Facebook Get Photo URL] url => "[12954] + url);
    //   fbInst = FacebookPlatform.Instance();
    //   if (fbInst == null) FUN_015cb8fc;
    //   name = fbInst.GetFacebookDisplayName();
    //   Debug.Log("[Facebook Debug] display name => "[12952] + name);
    //   fbInst = FacebookPlatform.Instance();
    //   if (fbInst == null) FUN_015cb8fc;
    //   uid = fbInst.GetFacebookUID();
    //   Debug.Log("[Facebook Debug] uid => "[12953] + uid);
    //   if (string.IsNullOrEmpty(url)) return false;
    //   bplua = BaseProcLua.Instance;
    //   luaMgr = Main.LuaMgr;
    //   if (bplua != null && luaMgr != null) {
    //       co = bplua.LoadHeadIconCoroutine(url, "fb"[16623]);
    //       luaMgr.StartCoroutine(co);
    //       return true;
    //   }
    //   FUN_015cb8fc (trap).
    public static bool LoadFacebookHeadIcon()
    {
        if (!MarsSDK.MarsFunction.IsBindFacebook())
        {
            return false;
        }
        MarsSDK.Platform.FacebookPlatform fbInst = MarsSDK.Platform.FacebookPlatform.Instance();
        if (fbInst == null) throw new NullReferenceException();
        string url = fbInst.GetFacebookPhotoUri();
        UnityEngine.Debug.Log("[Facebook Get Photo URL] url => " + url);
        fbInst = MarsSDK.Platform.FacebookPlatform.Instance();
        if (fbInst == null) throw new NullReferenceException();
        string name = fbInst.GetFacebookDisplayName();
        UnityEngine.Debug.Log("[Facebook Debug] display name => " + name);
        fbInst = MarsSDK.Platform.FacebookPlatform.Instance();
        if (fbInst == null) throw new NullReferenceException();
        string uid = fbInst.GetFacebookUID();
        UnityEngine.Debug.Log("[Facebook Debug] uid => " + uid);
        if (string.IsNullOrEmpty(url)) return false;
        BaseProcLua bp = BaseProcLua.Instance;
        LuaFramework.LuaManager luaMgr = LuaFramework.LuaManager.Instance;
        if (bp != null && luaMgr != null)
        {
            System.Collections.IEnumerator co = bp.LoadHeadIconCoroutine(url, "fb");
            luaMgr.StartCoroutine(co);
            return true;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E4CE8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/LoadHeadIconCoroutine.c
    // PORTED 1-1 from state machine MoveNext in
    //   work/06_ghidra/decompiled_full/BaseProcLua.<LoadHeadIconCoroutine>d__238/MoveNext.c
    // Strings: 12956 "[LoadHeadIconCoroutine] downloadURL => ", 779 ", info => ",
    //   7666 "LoadHeadIconCoroutine Path-> {0}", 2641 "==== LoadHeadIcon success ====",
    //   2640 "==== LoadHeadIcon Error ====", 7664 "LoadHeadIcon Fail: ",
    //   764 ", URL: ", 8700 "OnHeadIconLoadSuccess", 8699 "OnHeadIconLoadFail",
    //   9214 "ProcessBase". <>4__this field at 0x40 = _LuaClass (LuaTable).
    public IEnumerator LoadHeadIconCoroutine(string downloadURL, string info)
    {
        // ── State 0: initial entry ──
        UnityEngine.Debug.Log("[LoadHeadIconCoroutine] downloadURL => " + downloadURL + ", info => " + info);
        UJDebug.LogFormat("LoadHeadIconCoroutine Path-> {0}", false, 0, new object[] { downloadURL });
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(downloadURL);
        yield return www.SendWebRequest();

        // ── State 1: after yield ──
        if (www == null)
            yield break;
        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHandler handler = www.downloadHandler;
            UnityEngine.Sprite sprite = null;
            if (handler != null)
            {
                UJDebug.Log("==== LoadHeadIcon success ====");
                DownloadHandlerTexture dht = (DownloadHandlerTexture)www.downloadHandler;
                if (dht == null)
                    yield break;
                UnityEngine.Texture2D tex = dht.texture;
                if (tex == null)
                    yield break;
                int w = tex.width;
                int h = tex.height;
                // Ghidra invokes the 3-arg Sprite.Create(Texture2D, Rect, Vector2) overload.
                // Argument ordering in the IL2CPP-generated call follows ARM64 ABI: rect.xyzw
                // map to s0-s3, pivot.xy to s4-s5, then texture (x0) and methodInfo (x1).
                // Pivot value (0.5,0.5) is from Vector2.center static field
                // (PTR_DAT_03446a48 -> Vector2 static + 0xb8).
                sprite = UnityEngine.Sprite.Create(
                    tex,
                    new UnityEngine.Rect(0, 0, (float)w, (float)h),
                    new UnityEngine.Vector2(0.5f, 0.5f));
            }
            LuaFramework.Util.CallMethod("ProcessBase", "OnHeadIconLoadSuccess",
                new object[] { this._LuaClass, sprite, info });
        }
        else
        {
            UJDebug.Log("==== LoadHeadIcon Error ====" + www.error);
            string failMsg = "LoadHeadIcon Fail: " + www.error + ", URL: " + downloadURL;
            LuaFramework.Util.CallMethod("ProcessBase", "OnHeadIconLoadFail",
                new object[] { this._LuaClass, info, failMsg });
        }
    }

    // RVA: 0x15E4FF4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/CheckServerListVersion.c
    // PORTED: Ghidra body is a single call to FUN_032a5aa0(DAT_0368298f) — a raw IL2CPP runtime
    // helper that wraps the LoadServerListVersion coroutine launch. The actual semantic is to
    // start the LoadServerListVersion coroutine on the BaseProcLua.Instance MonoBehaviour (which
    // owns the LuaManager state). Verified by checking that LoadServerListVersion (already ported)
    // is the only coroutine in this class that would be invoked from such a static entrypoint.
    public static void CheckServerListVersion()
    {
        BaseProcLua bp = BaseProcLua.Instance;
        LuaFramework.LuaManager luaMgr = LuaFramework.LuaManager.Instance;
        if (bp == null || luaMgr == null) throw new NullReferenceException();
        System.Collections.IEnumerator co = bp.LoadServerListVersion();
        luaMgr.StartCoroutine(co);
    }

    // RVA: 0x15E50CC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/LoadServerListVersion.c
    // PORTED 1-1 from state machine MoveNext in
    //   work/06_ghidra/decompiled_full/BaseProcLua.<LoadServerListVersion>d__240/MoveNext.c
    // Strings: 9029 "PatchHostList.xml", 21255 "{0}?v={1}", 7684 "LoadPatchList Path-> {0}",
    //   2648 "==== LoadServerListVersion Error ====", 9997 "SetConnectCheckState",
    //   9214 "ProcessBase". PTR_DAT_03446548 = typeof(int) for boxing.
    // States 4/5/11 are eConnectCheckState enum values passed to Lua callback.
    private IEnumerator LoadServerListVersion()
    {
        // ── State 0: initial entry ──
        int ts = Main.GetNowTimeStamp();
        string url = string.Format("{0}?v={1}", ResourcesPath.PatchHost + "PatchHostList.xml", ts);
        UJDebug.LogFormat("LoadPatchList Path-> {0}", false, 0, new object[] { url });
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // ── State 1: after yield ──
        if (www == null)
            yield break;
        if (!string.IsNullOrEmpty(www.error))
        {
            UJDebug.Log("==== LoadServerListVersion Error ====" + www.error);
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState",
                new object[] { this._LuaClass, 5 });
        }
        else
        {
            DownloadHandler handler = www.downloadHandler;
            if (handler == null)
                yield break;
            string text = handler.text;
            if (text == null)
                yield break;
            string trimmed = text.Trim();
            bool ok = this.ParsingServerListInfo(trimmed);
            int state = ok ? 4 : 11;
            LuaFramework.Util.CallMethod("ProcessBase", "SetConnectCheckState",
                new object[] { this._LuaClass, state });
        }
    }

    // RVA: 0x15E5140  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/ParsingServerListInfo.c
    // PORTED 1-1:
    //   doc = new XmlDocument();
    //   doc.Load(new StringReader(text));
    //   for each node in doc.SelectNodes("//Patch"[idx 991]):
    //       attr = node.Attributes["serverlistVersion"[19729]];  // XmlAttribute
    //       — Ghidra walks Attributes via uintsmask: actually iterates ItemOf with name lookup.
    //       — Reads "serverlistVersion" value into latestNormalVersion variable (uVar9).
    //   for each node in doc.SelectNodes("//PatchPreview"[idx 993]):
    //       attr = node.Attributes["serverlistVersion"];
    //       — Reads value into latestPreviewVersion (uVar17).
    //   isPreview = BaseProcLua.IsPreviewVersion();
    //   if isPreview: localVersion = ResourcesPath._patchDataPreview.PatchServerListVersion  (offset 0x78 of PatchHostData)
    //   else:         localVersion = ResourcesPath._patchData.PatchServerListVersion
    //   return string.Equals(localVersion, latestParsedVersion);
    private bool ParsingServerListInfo(string text)
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.Load(new System.IO.StringReader(text));
        string latestNormal = "";
        string latestPreview = "";
        System.Xml.XmlNodeList normalNodes = doc.SelectNodes("//Patch");
        if (normalNodes != null)
        {
            for (int i = 0; i < normalNodes.Count; i++)
            {
                System.Xml.XmlNode node = normalNodes[i];
                if (node == null || node.Attributes == null) continue;
                System.Xml.XmlAttribute attr = node.Attributes["serverlistVersion"];
                if (attr != null) latestNormal = attr.Value;
            }
        }
        System.Xml.XmlNodeList previewNodes = doc.SelectNodes("//PatchPreview");
        if (previewNodes != null)
        {
            for (int i = 0; i < previewNodes.Count; i++)
            {
                System.Xml.XmlNode node = previewNodes[i];
                if (node == null || node.Attributes == null) continue;
                System.Xml.XmlAttribute attr = node.Attributes["serverlistVersion"];
                if (attr != null) latestPreview = attr.Value;
            }
        }
        bool isPreview = BaseProcLua.IsPreviewVersion();
        string localVersion;
        if (isPreview)
        {
            PatchHostData pd = ResourcesPath.PatchDataPreview;
            if (pd == null) throw new NullReferenceException();
            localVersion = pd.PatchServerListVersion;
            return localVersion == latestPreview;
        }
        else
        {
            PatchHostData pd = ResourcesPath.PatchData;
            if (pd == null) throw new NullReferenceException();
            localVersion = pd.PatchServerListVersion;
            return localVersion == latestNormal;
        }
    }

    // RVA: 0x15E593C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendHttpPostRequestWithFormData.c
    // PORTED 1-1:
    //   if (keys == null || values == null) FUN_015cb8fc;
    //   keysJson = "{"[21155] + keys.Aggregate(", "[749]) + "}"[21287];
    //   valuesJson = "{" + values.Aggregate(", ") + "}";
    //   msg = string.Format(
    //       "SendHttpPostRequestWithFormData url = "[9905] + url +
    //       ", keys = "[780] + keysJson +
    //       ", values = "[786] + valuesJson);
    //   Debug.Log(msg);
    //   bplua = BaseProcLua.Instance;
    //   luaMgr = Main.LuaMgr;
    //   if (bplua != null && luaMgr != null) {
    //       co = bplua.SendHttpPostRequestWithFormDataCoroutine(requestId, url, keys, values);
    //       luaMgr.StartCoroutine(co);
    //   }
    public static void SendHttpPostRequestWithFormData(string requestId, string url, string[] keys, string[] values)
    {
        if (keys == null || values == null) throw new NullReferenceException();
        string keysJoined = "{";
        for (int i = 0; i < keys.Length; i++)
        {
            keysJoined += keys[i];
            if (i < keys.Length - 1) keysJoined += ", ";
        }
        keysJoined += "}";
        string valsJoined = "{";
        for (int i = 0; i < values.Length; i++)
        {
            valsJoined += values[i];
            if (i < values.Length - 1) valsJoined += ", ";
        }
        valsJoined += "}";
        string msg = "SendHttpPostRequestWithFormData url = " + url + ", keys = " + keysJoined + ", values = " + valsJoined;
        UnityEngine.Debug.Log(msg);
        BaseProcLua bp = BaseProcLua.Instance;
        LuaFramework.LuaManager luaMgr = LuaFramework.LuaManager.Instance;
        if (bp != null && luaMgr != null)
        {
            System.Collections.IEnumerator co = bp.SendHttpPostRequestWithFormDataCoroutine(requestId, url, keys, values);
            luaMgr.StartCoroutine(co);
        }
    }

    // RVA: 0x15E5CD0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendHttpPostRequestWithFormDataCoroutine.c
    // PORTED 1-1 from state machine MoveNext in
    //   work/06_ghidra/decompiled_full/BaseProcLua.<SendHttpPostRequestWithFormDataCoroutine>d__243/MoveNext.c
    // Strings: 9904 "SendHttpPostRequestWithFormData Success: ",
    //   9902 "SendHttpPostRequestWithFormData Error: ",
    //   9903 "SendHttpPostRequestWithFormData Error: handler is null",
    //   8761 "OnSendHttpPostRequestWithFormDataSuccess",
    //   8760 "OnSendHttpPostRequestWithFormDataFail", 9214 "ProcessBase".
    private IEnumerator SendHttpPostRequestWithFormDataCoroutine(string requestId, string url, string[] keys, string[] values)
    {
        // ── State 0: initial entry ──
        WWWForm form = new WWWForm();
        if (keys != null)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (values == null || i >= values.Length)
                    break;
                form.AddField(keys[i], values[i]);
            }
        }
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        // ── State 1: after yield ──
        if (www == null)
            yield break;
        string failMsg;
        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHandler handler = www.downloadHandler;
            if (handler != null)
            {
                string text = handler.text;
                UJDebug.Log("SendHttpPostRequestWithFormData Success: " + text);
                LuaFramework.Util.CallMethod("ProcessBase", "OnSendHttpPostRequestWithFormDataSuccess",
                    new object[] { this._LuaClass, requestId, text });
                yield break;
            }
            failMsg = "SendHttpPostRequestWithFormData Error: handler is null";
        }
        else
        {
            failMsg = "SendHttpPostRequestWithFormData Error: " + www.error;
        }
        UJDebug.LogError(failMsg);
        LuaFramework.Util.CallMethod("ProcessBase", "OnSendHttpPostRequestWithFormDataFail",
            new object[] { this._LuaClass, requestId });
    }

    // RVA: 0x15E5DA4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendHttpPostRequestWithJsonData.c
    // PORTED 1-1:
    //   bplua = BaseProcLua.Instance;
    //   luaMgr = Main.LuaMgr;
    //   if (bplua != null && luaMgr != null) {
    //       co = bplua.SendHttpPostRequestWithJsonDataCoroutine(requestId, url, jsonData);
    //       luaMgr.StartCoroutine(co);
    //   } else FUN_015cb8fc.
    public static void SendHttpPostRequestWithJsonData(string requestId, string url, string jsonData)
    {
        BaseProcLua bp = BaseProcLua.Instance;
        LuaFramework.LuaManager luaMgr = LuaFramework.LuaManager.Instance;
        if (bp != null && luaMgr != null)
        {
            System.Collections.IEnumerator co = bp.SendHttpPostRequestWithJsonDataCoroutine(requestId, url, jsonData);
            luaMgr.StartCoroutine(co);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E5E9C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendHttpPostRequestWithJsonDataCoroutine.c
    // PORTED 1-1 from state machine MoveNext in
    //   work/06_ghidra/decompiled_full/BaseProcLua.<SendHttpPostRequestWithJsonDataCoroutine>d__245/MoveNext.c
    // Strings: 8937 "POST", 4265 "Content-Type", 13873 "application/json",
    //   9909 "SendHttpPostRequestWithJsonData Success: ",
    //   9907 "SendHttpPostRequestWithJsonData Error: ",
    //   9908 "SendHttpPostRequestWithJsonData Error: handler is null",
    //   8763 "OnSendHttpPostRequestWithJsonDataSuccess",
    //   8762 "OnSendHttpPostRequestWithJsonDataFail", 9214 "ProcessBase".
    private IEnumerator SendHttpPostRequestWithJsonDataCoroutine(string requestId, string url, string jsonData)
    {
        // ── State 0: initial entry ──
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        // ── State 1: after yield ──
        if (www == null)
            yield break;
        string failMsg;
        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHandler handler = www.downloadHandler;
            if (handler != null)
            {
                string text = handler.text;
                UJDebug.Log("SendHttpPostRequestWithJsonData Success: " + text);
                LuaFramework.Util.CallMethod("ProcessBase", "OnSendHttpPostRequestWithJsonDataSuccess",
                    new object[] { this._LuaClass, requestId, text });
                yield break;
            }
            failMsg = "SendHttpPostRequestWithJsonData Error: handler is null";
        }
        else
        {
            failMsg = "SendHttpPostRequestWithJsonData Error: " + www.error;
        }
        UJDebug.LogError(failMsg);
        LuaFramework.Util.CallMethod("ProcessBase", "OnSendHttpPostRequestWithJsonDataFail",
            new object[] { this._LuaClass, requestId });
    }

    // RVA: 0x15E5F5C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/LoadNotificationImage.c
    // PORTED 1-1:
    //   bplua = BaseProcLua.Instance;
    //   luaMgr = Main.LuaMgr;
    //   if (bplua != null && luaMgr != null) {
    //       co = bplua.LoadNotificationImageCoroutine(imageName);
    //       luaMgr.StartCoroutine(co);
    //   } else FUN_015cb8fc.
    public static void LoadNotificationImage(string imageName)
    {
        BaseProcLua bp = BaseProcLua.Instance;
        LuaFramework.LuaManager luaMgr = LuaFramework.LuaManager.Instance;
        if (bp != null && luaMgr != null)
        {
            System.Collections.IEnumerator co = bp.LoadNotificationImageCoroutine(imageName);
            luaMgr.StartCoroutine(co);
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E603C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/LoadNotificationImageCoroutine.c
    // PORTED 1-1 from state machine MoveNext in
    //   work/06_ghidra/decompiled_full/BaseProcLua.<LoadNotificationImageCoroutine>d__247/MoveNext.c
    // Strings: 8491 "Notifications", 21265 "{0}{1}/{2}.png?v={3}",
    //   7681 "LoadNotificationImageCoroutine url = ", 7680 "LoadNotificationImage Success",
    //   7678 "LoadNotificationImage Error: ", 7679 "LoadNotificationImage Error: handler is null",
    //   8708 "OnLoadNotificationImageSuccess", 8707 "OnLoadNotificationImageFail",
    //   9214 "ProcessBase".
    private IEnumerator LoadNotificationImageCoroutine(string imageName)
    {
        // ── State 0: initial entry ──
        int ts = Main.GetNowTimeStamp();
        string url = string.Format("{0}{1}/{2}.png?v={3}", ResourcesPath.PatchHost, "Notifications", imageName, ts);
        UJDebug.Log("LoadNotificationImageCoroutine url = " + url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        // ── State 1: after yield ──
        if (www == null)
            yield break;
        string failMsg;
        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHandler handler = www.downloadHandler;
            if (handler != null)
            {
                UJDebug.Log("LoadNotificationImage Success");
                DownloadHandlerTexture dht = (DownloadHandlerTexture)www.downloadHandler;
                if (dht == null)
                    yield break;
                UnityEngine.Texture2D tex = dht.texture;
                LuaFramework.Util.CallMethod("ProcessBase", "OnLoadNotificationImageSuccess",
                    new object[] { this._LuaClass, tex, imageName });
                yield break;
            }
            failMsg = "LoadNotificationImage Error: handler is null";
        }
        else
        {
            failMsg = "LoadNotificationImage Error: " + www.error;
        }
        UJDebug.LogError(failMsg);
        LuaFramework.Util.CallMethod("ProcessBase", "OnLoadNotificationImageFail",
            new object[] { this._LuaClass, imageName });
    }

    // RVA: 0x15E60CC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/MTPLogin.c
    // PORTED 1-1: Ghidra body is empty (single 'return'). This is the Tencent MTP/TerSafe
    // stub for the non-CN build — APK uses libtersafe2.so guarded by CN compile-flag.
    public static void MTPLogin()
    {
    }

    // RVA: 0x15E60D0  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/MTPLuaCheck.c
    // PORTED 1-1: Ghidra body is empty (single 'return'). MTP stub for non-CN build.
    public static void MTPLuaCheck()
    {
    }

    // RVA: 0x15E60D4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/MTPGetAntiData.c
    // PORTED 1-1: Ghidra body is empty (single 'return'). MTP stub for non-CN build.
    public static void MTPGetAntiData()
    {
    }

    // RVA: 0x15E60D8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/MTPInfo.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "MTPInfo"[7883], new object[2]{_LuaClass, MTPInfo}).
    public void MTPInfo(string MTPInfo)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = MTPInfo;
        LuaFramework.Util.CallMethod("ProcessBase", "MTPInfo", arr);
    }

    // RVA: 0x15E61EC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/MTPAntiData.c
    // PORTED 1-1: CallMethod("ProcessBase"[9214], "MTPAntiData"[7881], new object[2]{_LuaClass, MTPAntiData}).
    public void MTPAntiData(string MTPAntiData)
    {
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = MTPAntiData;
        LuaFramework.Util.CallMethod("ProcessBase", "MTPAntiData", arr);
    }

    // RVA: 0x15E6300  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SetDebugInfoDisplay.c
    // PORTED 1-1:
    //   debugInst = WndForm_DebugConsole.s_instance;
    //   if (debugInst != null) {
    //     debugInst.SetDebugConsoleVisible(enable, true);  // onlyHide=true
    //     fpsInst = FPSText.m_instance;
    //     if (fpsInst != null) fpsInst.SetDisplay(enable);
    //   } else throw.
    // PTR_DAT_03449af8 = WndForm_DebugConsole; PTR_DAT_03449c18 = FPSText.
    public static void SetDebugInfoDisplay(bool enable)
    {
        WndForm_DebugConsole debugInst = WndForm_DebugConsole.Instance;
        if (debugInst == null) throw new NullReferenceException();
        debugInst.SetDebugConsoleVisible(enable, true);
        FPSText fpsInst = FPSText.get_Instance();
        if (fpsInst == null) throw new NullReferenceException();
        fpsInst.SetDisplay(enable);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BaseProcLua/isUserFireBase.c RVA 0x015e63a4
    // 1-1: return true; (constant — Ghidra body: `return 1;`)
    public static bool isUserFireBase()
    {
        return true;
    }

    // RVA: 0x15E63AC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendFirebaseEvent.c
    // PORTED 1-1:
    //   Debug.Log("SendFirebaseEvent:"[9896] + eventCode);
    //   Firebase.Analytics.FirebaseAnalytics.LogEvent(eventCode);
    public static void SendFirebaseEvent(string eventCode)
    {
        UnityEngine.Debug.Log("SendFirebaseEvent:" + eventCode);
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventCode);
    }

    // RVA: 0x15E655C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendFirebaseEventFloat.c
    // PORTED 1-1: FirebaseAnalytics.LogEvent(eventCode, eventParamName, (double)value).
    // Ghidra: `Firebase_Analytics_FirebaseAnalytics__LogEvent((double)param_1,param_2,param_3,0)` — ARM64
    // ABI shuffles param_1 (float value) to xmm0 as double, then (eventCode,paramName) as x0/x1.
    public static void SendFirebaseEventFloat(string eventCode, string eventParamName, float value)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventCode, eventParamName, (double)value);
    }

    // RVA: 0x15E66D8  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SendFirebaseEventInt.c
    // PORTED 1-1: FirebaseAnalytics.LogEvent(eventCode, eventParamName, value).
    public static void SendFirebaseEventInt(string eventCode, string eventParamName, int value)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventCode, eventParamName, value);
    }

    // RVA: 0x15E6848  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/SetDebugInfo.c
    // PORTED 1-1:
    //   WriteFile("DebugInfo.txt"[4679], infoData);   // calls string overload
    //   inst = ConfigMgr.Instance;
    //   if (inst != null) inst.SetConfigVarStrbyStr("EN_DEBUG"[4900], "1"[1071]);
    //   else FUN_015cb8fc.
    public static void SetDebugInfo(string infoData)
    {
        BaseProcLua.WriteFile("DebugInfo.txt", infoData);
        ConfigMgr inst = ConfigMgr.Instance;
        if (inst != null)
        {
            inst.SetConfigVarStrbyStr("EN_DEBUG", "1");
            return;
        }
        throw new NullReferenceException();
    }

    // RVA: 0x15E69B4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/WriteFile.c
    // PORTED 1-1: byte[] overload follows the same shape as the string overload — only Ghidra .c
    // we have is the string overload at 0x15E68FC. The byte[] body in IL2CPP simply replaces
    // File.WriteAllText with File.WriteAllBytes.
    //   if (!IsNullOrEmpty(fileNameWithExt)) {
    //       path = string.Format("{0}/{1}", Application.persistentDataPath, fileNameWithExt);
    //       File.WriteAllBytes(path, byteData);
    //   }
    //   return !IsNullOrEmpty.
    public static bool WriteFile(string fileNameWithExt, byte[] byteData)
    {
        bool isEmpty = string.IsNullOrEmpty(fileNameWithExt);
        if (!isEmpty)
        {
            string path = string.Format("{0}/{1}", Application.persistentDataPath, fileNameWithExt);
            System.IO.File.WriteAllBytes(path, byteData);
        }
        return !isEmpty;
    }

    // RVA: 0x15E68FC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/WriteFile.c
    // PORTED 1-1:
    //   if (!string.IsNullOrEmpty(fileNameWithExt)) {
    //     path = string.Format(StringLiteral_21237, Application.persistentDataPath, fileNameWithExt);
    //     File.WriteAllText(path, strData);
    //   }
    //   return !IsNullOrEmpty  // Ghidra: `return ~uVar2 & 1;` where uVar2 = IsNullOrEmpty
    // NOTE: StringLiteral_21237 format string is unverified — assumed "{0}/{1}" since
    //       there are exactly 2 args. Replace with literal table lookup when located.
    public static bool WriteFile(string fileNameWithExt, string strData)
    {
        bool isEmpty = string.IsNullOrEmpty(fileNameWithExt);
        if (!isEmpty)
        {
            string path = string.Format("{0}/{1}", Application.persistentDataPath, fileNameWithExt);
            System.IO.File.WriteAllText(path, strData);
        }
        return !isEmpty;
    }

    // RVA: 0x15E6A6C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameLoginSuccess.c
    // PORTED 1-1: UJDebug.Log("[OnESGameLoginSuccess]"[idx 12971]);
    //   CallMethod("ProcessBase"[9214], "OnESGameLoginSuccess"[8646], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnESGameLoginSuccess(string[] args)
    {
        UJDebug.Log("[OnESGameLoginSuccess]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameLoginSuccess", arr);
    }

    // RVA: 0x15E6C3C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameLoginFail.c
    // PORTED 1-1: UJDebug.Log("[OnESGameLoginFail]"[idx 12970]);
    //   CallMethod("ProcessBase"[9214], "OnESGameLoginFail"[8645], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnESGameLoginFail(string[] args)
    {
        UJDebug.Log("[OnESGameLoginFail]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameLoginFail", arr);
    }

    // RVA: 0x15E6E0C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameLogout.c
    // PORTED 1-1: UJDebug.Log("[OnESGameLogout]"[idx 12972]);
    //   CallMethod("ProcessBase"[9214], "OnESGameLogout"[8647], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnESGameLogout()
    {
        UJDebug.Log("[OnESGameLogout]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameLogout", arr);
    }

    // RVA: 0x15E6FA4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameGGBillingResult.c
    // PORTED 1-1: UJDebug.Log("[OnESGameGGBillingResult]"[idx 12969]);
    //   CallMethod("ProcessBase"[9214], "OnESGameGGBillingResult"[8644], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnESGameGGBillingResult(string[] args)
    {
        UJDebug.Log("[OnESGameGGBillingResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameGGBillingResult", arr);
    }

    // RVA: 0x15E7174  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameWebBillingResult.c
    // PORTED 1-1: UJDebug.Log("[OnESGameWebBillingResult]"[idx 12974]);
    //   CallMethod("ProcessBase"[9214], "OnESGameWebBillingResult"[8649], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnESGameWebBillingResult(string[] args)
    {
        UJDebug.Log("[OnESGameWebBillingResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameWebBillingResult", arr);
    }

    // RVA: 0x15E7344  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameAppleBillingResult.c
    // PORTED 1-1: UJDebug.Log("[OnESGameAppleBillingResult]"[idx 12967]);
    //   CallMethod("ProcessBase"[9214], "OnESGameAppleBillingResult"[8641], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnESGameAppleBillingResult(string[] args)
    {
        UJDebug.Log("[OnESGameAppleBillingResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameAppleBillingResult", arr);
    }

    // RVA: 0x15E7514  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameViewClose.c
    // PORTED 1-1: UJDebug.Log("[OnESGameViewClose]"[idx 12973]);
    //   CallMethod("ProcessBase"[9214], "OnESGameViewClose"[8648], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnESGameViewClose()
    {
        UJDebug.Log("[OnESGameViewClose]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameViewClose", arr);
    }

    // RVA: 0x15E76AC  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnESGameError.c
    // PORTED 1-1: UJDebug.Log("[OnESGameError]"[idx 12968]);
    //   CallMethod("ProcessBase"[9214], "OnESGameError"[8643], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnESGameError(string[] args)
    {
        UJDebug.Log("[OnESGameError]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnESGameError", arr);
    }

    // RVA: 0x15E787C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKLoginSuccess.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKLoginSuccess]"[idx 12986]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKLoginSuccess"[8662], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKLoginSuccess(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKLoginSuccess]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKLoginSuccess", arr);
    }

    // RVA: 0x15E7A4C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKLoginFail.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKLoginFail]"[idx 12985]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKLoginFail"[8661], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKLoginFail(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKLoginFail]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKLoginFail", arr);
    }

    // RVA: 0x15E7C1C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKLogout.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKLogout]"[idx 12987]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKLogout"[8663], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnFhyxSDKLogout()
    {
        UJDebug.Log("[OnFhyxSDKLogout]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKLogout", arr);
    }

    // RVA: 0x15E7DB4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKSendRegisterVerificationCodeResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKSendRegisterVerificationCodeResult]"[idx 12998]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKSendRegisterVerificationCodeResult"[8674], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKSendRegisterVerificationCodeResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKSendRegisterVerificationCodeResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKSendRegisterVerificationCodeResult", arr);
    }

    // RVA: 0x15E7F84  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKBindingPersonalIdentityResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKBindingPersonalIdentityResult]"[idx 12976]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKBindingPersonalIdentityResult"[8652], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKBindingPersonalIdentityResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKBindingPersonalIdentityResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKBindingPersonalIdentityResult", arr);
    }

    // RVA: 0x15E8154  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKGetUserBoundInfoSuccess.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKGetUserBoundInfoSuccess]"[idx 12982]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKGetUserBoundInfoSuccess"[8658], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKGetUserBoundInfoSuccess(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKGetUserBoundInfoSuccess]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKGetUserBoundInfoSuccess", arr);
    }

    // RVA: 0x15E8324  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKGetUserBoundInfoFail.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKGetUserBoundInfoFail]"[idx 12981]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKGetUserBoundInfoFail"[8657], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKGetUserBoundInfoFail(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKGetUserBoundInfoFail]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKGetUserBoundInfoFail", arr);
    }

    // RVA: 0x15E84F4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKGetUserInfoSuccess.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKGetUserInfoSuccess]"[idx 12984]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKGetUserInfoSuccess"[8660], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKGetUserInfoSuccess(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKGetUserInfoSuccess]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKGetUserInfoSuccess", arr);
    }

    // RVA: 0x15E86C4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKGetUserInfoFail.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKGetUserInfoFail]"[idx 12983]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKGetUserInfoFail"[8659], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKGetUserInfoFail(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKGetUserInfoFail]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKGetUserInfoFail", arr);
    }

    // RVA: 0x15E8894  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKGetLoginVerificationCodeResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKGetLoginVerificationCodeResult]"[idx 12980]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKGetLoginVerificationCodeResult"[8656], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKGetLoginVerificationCodeResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKGetLoginVerificationCodeResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKGetLoginVerificationCodeResult", arr);
    }

    // RVA: 0x15E8A64  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKChangePasswordResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKChangePasswordResult]"[idx 12977]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKChangePasswordResult"[8653], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKChangePasswordResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKChangePasswordResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKChangePasswordResult", arr);
    }

    // RVA: 0x15E8C34  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKResetPasswordSendVerificationCodeResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKResetPasswordSendVerificationCodeResult]"[idx 12996]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKResetPasswordSendVerificationCodeResult"[8672], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKResetPasswordSendVerificationCodeResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKResetPasswordSendVerificationCodeResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKResetPasswordSendVerificationCodeResult", arr);
    }

    // RVA: 0x15E8E04  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKResetPasswordCheckVerificationCodeResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKResetPasswordCheckVerificationCodeResult]"[idx 12994]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKResetPasswordCheckVerificationCodeResult"[8670], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKResetPasswordCheckVerificationCodeResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKResetPasswordCheckVerificationCodeResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKResetPasswordCheckVerificationCodeResult", arr);
    }

    // RVA: 0x15E8FD4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKResetPasswordResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKResetPasswordResult]"[idx 12995]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKResetPasswordResult"[8671], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKResetPasswordResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKResetPasswordResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKResetPasswordResult", arr);
    }

    // RVA: 0x15E91A4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult]"[idx 12992]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult"[8668], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult", arr);
    }

    // RVA: 0x15E9374  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKRebindPhoneCheckVerificationCodeResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKRebindPhoneCheckVerificationCodeResult]"[idx 12989]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKRebindPhoneCheckVerificationCodeResult"[8665], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKRebindPhoneCheckVerificationCodeResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKRebindPhoneCheckVerificationCodeResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKRebindPhoneCheckVerificationCodeResult", arr);
    }

    // RVA: 0x15E9544  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult]"[idx 12991]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult"[8667], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult", arr);
    }

    // RVA: 0x15E9714  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKRebindPhoneResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKRebindPhoneResult]"[idx 12990]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKRebindPhoneResult"[8666], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKRebindPhoneResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKRebindPhoneResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKRebindPhoneResult", arr);
    }

    // RVA: 0x15E98E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKRegisterResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKRegisterResult]"[idx 12993]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKRegisterResult"[8669], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKRegisterResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKRegisterResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKRegisterResult", arr);
    }

    // RVA: 0x15E9AB4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKWebViewClose.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKWebViewClose]"[idx 12999]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKWebViewClose"[8675], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKWebViewClose(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKWebViewClose]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKWebViewClose", arr);
    }

    // RVA: 0x15E9C84  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKBindActivationCodeResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKBindActivationCodeResult]"[idx 12975]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKBindActivationCodeResult"[8651], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKBindActivationCodeResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKBindActivationCodeResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKBindActivationCodeResult", arr);
    }

    // RVA: 0x15E9E54  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKCheckAccountActivationResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKCheckAccountActivationResult]"[idx 12978]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKCheckAccountActivationResult"[8654], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKCheckAccountActivationResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKCheckAccountActivationResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKCheckAccountActivationResult", arr);
    }

    // RVA: 0x15EA024  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKSendLogResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKSendLogResult]"[idx 12997]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKSendLogResult"[8673], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKSendLogResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKSendLogResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKSendLogResult", arr);
    }

    // RVA: 0x15EA1F4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKPayResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKPayResult]"[idx 12988]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKPayResult"[8664], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKPayResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKPayResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKPayResult", arr);
    }

    // RVA: 0x15EA3C4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFhyxSDKCheckWordResult.c
    // PORTED 1-1: UJDebug.Log("[OnFhyxSDKCheckWordResult]"[idx 12979]);
    //   CallMethod("ProcessBase"[9214], "OnFhyxSDKCheckWordResult"[8655], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFhyxSDKCheckWordResult(string[] args)
    {
        UJDebug.Log("[OnFhyxSDKCheckWordResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFhyxSDKCheckWordResult", arr);
    }

    // RVA: 0x15EA594  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowPersonalInfoGuideDialogResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowPersonalInfoGuideDialogResult]"[idx 13006]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowPersonalInfoGuideDialogResult"[8683], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowPersonalInfoGuideDialogResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowPersonalInfoGuideDialogResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowPersonalInfoGuideDialogResult", arr);
    }

    // RVA: 0x15EA764  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKInitResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKInitResult]"[idx 13002]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKInitResult"[8679], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKInitResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKInitResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKInitResult", arr);
    }

    // RVA: 0x15EA934  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKCheckLoginResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKCheckLoginResult]"[idx 13000]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKCheckLoginResult"[8677], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKCheckLoginResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKCheckLoginResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKCheckLoginResult", arr);
    }

    // RVA: 0x15EAB04  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKStartLoginResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKStartLoginResult]"[idx 13012]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKStartLoginResult"[8689], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKStartLoginResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKStartLoginResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKStartLoginResult", arr);
    }

    // RVA: 0x15EACD4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKStartLogoutResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKStartLogoutResult]"[idx 13013]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKStartLogoutResult"[8690], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKStartLogoutResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKStartLogoutResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKStartLogoutResult", arr);
    }

    // RVA: 0x15EAEA4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKStartUserCenterResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKStartUserCenterResult]"[idx 13014]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKStartUserCenterResult"[8691], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKStartUserCenterResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKStartUserCenterResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKStartUserCenterResult", arr);
    }

    // RVA: 0x15EB074  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowCloseAccountResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowCloseAccountResult]"[idx 13005]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowCloseAccountResult"[8682], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowCloseAccountResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowCloseAccountResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowCloseAccountResult", arr);
    }

    // RVA: 0x15EB244  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowRealNameAuthenticationResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowRealNameAuthenticationResult]"[idx 13007]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowRealNameAuthenticationResult"[8684], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowRealNameAuthenticationResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowRealNameAuthenticationResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowRealNameAuthenticationResult", arr);
    }

    // RVA: 0x15EB414  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowRoundIdentityInfoResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowRoundIdentityInfoResult]"[idx 13008]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowRoundIdentityInfoResult"[8685], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowRoundIdentityInfoResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowRoundIdentityInfoResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowRoundIdentityInfoResult", arr);
    }

    // RVA: 0x15EB5E4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowWebDialogResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowWebDialogResult]"[idx 13009]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowWebDialogResult"[8686], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowWebDialogResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowWebDialogResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowWebDialogResult", arr);
    }

    // RVA: 0x15EB7B4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowWebUIDialogResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowWebUIDialogResult]"[idx 13011]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowWebUIDialogResult"[8688], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowWebUIDialogResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowWebUIDialogResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowWebUIDialogResult", arr);
    }

    // RVA: 0x15EB984  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKShowWebSystemResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKShowWebSystemResult]"[idx 13010]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKShowWebSystemResult"[8687], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKShowWebSystemResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKShowWebSystemResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKShowWebSystemResult", arr);
    }

    // RVA: 0x15EBB54  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKUploadGameUserInfoResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKUploadGameUserInfoResult]"[idx 13015]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKUploadGameUserInfoResult"[8692], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKUploadGameUserInfoResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKUploadGameUserInfoResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKUploadGameUserInfoResult", arr);
    }

    // RVA: 0x15EBD24  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKPayResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKPayResult]"[idx 13004]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKPayResult"[8681], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKPayResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKPayResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKPayResult", arr);
    }

    // RVA: 0x15EBEF4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKNotImplementEventResult.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKNotImplementEventResult]"[idx 13003]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKNotImplementEventResult"[8680], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKNotImplementEventResult(string[] args)
    {
        UJDebug.Log("[OnFxhySDKNotImplementEventResult]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKNotImplementEventResult", arr);
    }

    // RVA: 0x15EC0C4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnFxhySDKClose.c
    // PORTED 1-1: UJDebug.Log("[OnFxhySDKClose]"[idx 13001]);
    //   CallMethod("ProcessBase"[9214], "OnFxhySDKClose"[8678], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnFxhySDKClose(string[] args)
    {
        UJDebug.Log("[OnFxhySDKClose]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnFxhySDKClose", arr);
    }

    // RVA: 0x15EC294  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdLoadSucceed.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdLoadSucceed]"[idx 13022]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdLoadSucceed"[8756], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdLoadSucceed()
    {
        UJDebug.Log("[OnRewardAdLoadSucceed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdLoadSucceed", arr);
    }

    // RVA: 0x15EC42C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdLoadFailed.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdLoadFailed]"[idx 13021]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdLoadFailed"[8755], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdLoadFailed()
    {
        UJDebug.Log("[OnRewardAdLoadFailed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdLoadFailed", arr);
    }

    // RVA: 0x15EC5C4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdPaid.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdPaid]"[idx 13023]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdPaid"[8757], new object[2]{_LuaClass, args}).
    //   FUN_015cb754(typeof(object),2) = new object[2]; arr[0]=this._LuaClass; arr[1]=args.
    public void OnRewardAdPaid(string[] args)
    {
        UJDebug.Log("[OnRewardAdPaid]");
        object[] arr = new object[2];
        arr[0] = this._LuaClass;
        arr[1] = args;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdPaid", arr);
    }

    // RVA: 0x15EC794  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdImpressionRecorded.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdImpressionRecorded]"[idx 13020]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdImpressionRecorded"[8754], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdImpressionRecorded()
    {
        UJDebug.Log("[OnRewardAdImpressionRecorded]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdImpressionRecorded", arr);
    }

    // RVA: 0x15EC92C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdClicked.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdClicked]"[idx 13016]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdClicked"[8750], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdClicked()
    {
        UJDebug.Log("[OnRewardAdClicked]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdClicked", arr);
    }

    // RVA: 0x15ECAC4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdFullScreenContentOpened.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdFullScreenContentOpened]"[idx 13019]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdFullScreenContentOpened"[8753], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdFullScreenContentOpened()
    {
        UJDebug.Log("[OnRewardAdFullScreenContentOpened]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdFullScreenContentOpened", arr);
    }

    // RVA: 0x15ECC5C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdFullScreenContentClosed.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdFullScreenContentClosed]"[idx 13017]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdFullScreenContentClosed"[8751], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdFullScreenContentClosed()
    {
        UJDebug.Log("[OnRewardAdFullScreenContentClosed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdFullScreenContentClosed", arr);
    }

    // RVA: 0x15ECDF4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/OnRewardAdFullScreenContentFailed.c
    // PORTED 1-1: UJDebug.Log("[OnRewardAdFullScreenContentFailed]"[idx 13018]);
    //   CallMethod("ProcessBase"[9214], "OnRewardAdFullScreenContentFailed"[8752], new object[1]{_LuaClass}).
    //   FUN_015cb754(typeof(object),1) = new object[1]; arr[0]=this._LuaClass.
    public void OnRewardAdFullScreenContentFailed()
    {
        UJDebug.Log("[OnRewardAdFullScreenContentFailed]");
        object[] arr = new object[1];
        arr[0] = this._LuaClass;
        LuaFramework.Util.CallMethod("ProcessBase", "OnRewardAdFullScreenContentFailed", arr);
    }

    // RVA: 0x15ECF8C  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/GetAndroidSDKVersion.c
    // PORTED 1-1: returns Main.nAndroidAPILevel (Main static, offset 0x1C).
    // Ghidra: `*(undefined4 *)(*(long *)(... + 0xb8) + 0x1c)` = Main static field offset 0x1C = nAndroidAPILevel.
    public static int GetAndroidSDKVersion()
    {
        return Main.nAndroidAPILevel;
    }

    // RVA: 0x15ECFE4  Ghidra: work/06_ghidra/decompiled_full/BaseProcLua/.cctor.c
    // Static ctor — no-op stub. Original Ghidra cctor likely inits static caches
    // (_TypeCache, _sgzDebugCommand, etc). Throwing here breaks ALL static method calls
    // because cctor runs lazily on first member access.
    // TODO: port body 1-1 from Ghidra .cctor.c (likely empty caches + flag init).
    static BaseProcLua()
    {
        // No-op: static fields default to null/false, lazy init in callers.
    }
}
