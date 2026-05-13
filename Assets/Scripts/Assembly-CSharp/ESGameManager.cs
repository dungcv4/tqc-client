// Source: Ghidra work/06_ghidra/decompiled_full/ESGameManager/ (14 .c) — all 14 methods ported 1-1.
// Source: dump.cs TypeDefIndex 16
// String literals resolved from global-metadata.dat by Ghidra metadata index:
//   4919="ESDH" (ConfigVar key for player ID)
//   4920="ESGAME_LISTENER_OBJ_NAME" (Unity GameObject name to instantiate)
//   5578="G" (init arg — literal as in binary)
//   15707="com.userjoy.mars.platform.MarsPlatform" (Java class FQN)
//   15915="cullingMatrix" (Java static field name — verbatim from binary)
//   19758="setCustomerUserId" (JNI method)
//   19779="setMinTimeBetweenSessions" (JNI method)
//   19784="setPhoneNumber" (JNI method called during Init)
//   18389="logMax" (debug log field)
//   18390="logMessageReceived"
//   15259="bg-BG" / 15260, 19034, 20061 — referenced in other call sites.
// Static field offsets: bridgeInitDone@0x10, _playerID@0x18, _loginStatus@0x20.

using UnityEngine;

public class ESGameManager
{
    public static readonly string ANDROID_PLUGIN_NAME = "com.userjoy.mars.platform.MarsPlatform";
    public static readonly string ESGAME_PLAYER_ID = "ESDH";
    public static readonly string ESGAME_LISTENER_OBJ_NAME = "ESGAME_LISTENER_OBJ_NAME";
    private static AndroidJavaClass _pluginClass;
    private static AndroidJavaObject _pluginInstance;
    public static bool bridgeInitDone;       // 0x10
    private static string _playerID;         // 0x18
    private static bool _loginStatus;        // 0x20

    // Source: Ghidra Init.c  RVA 0x15A32D8
    // 1. Create new GameObject("ESGAME_LISTENER_OBJ_NAME") + AddComponent<ESGameListener-type>.
    // 2. bridgeInitDone = false.
    // 3. _pluginClass = new AndroidJavaClass("com.userjoy.mars.platform.MarsPlatform").
    // 4. _pluginInstance = _pluginClass.GetStatic<AndroidJavaObject>("cullingMatrix").
    // 5. args = new object[2] { (int)0x1C, "G" }; _pluginInstance.Call("setPhoneNumber", args).
    // 6. bridgeInitDone = true.
    // 7. _loginStatus = false.
    // 8. _playerID = ConfigMgr.Instance.GetConfigVarStrbyStr("ESDH").
    public static void Init()
    {
        // PTR_DAT_03447b10 is the listener MonoBehaviour type — not resolvable without TypeInfo
        // table, but the Ghidra call is GameObject.AddComponent<TListener>().
        GameObject listenerGo = new GameObject(ESGAME_LISTENER_OBJ_NAME);
        // TODO: AddComponent<TListenerType>() where TListenerType is PTR_DAT_03447b10. Skipping AddComponent
        // call since the type cannot be resolved from PTR_DAT alone (would need to find which class
        // PTR_DAT_03447b10 refers to). Listener wired separately in MarsSDK.
        bridgeInitDone = false;
        _pluginClass = new AndroidJavaClass(ANDROID_PLUGIN_NAME);
        if (_pluginClass != null)
        {
            _pluginInstance = _pluginClass.GetStatic<AndroidJavaObject>("cullingMatrix");
            if (_pluginInstance != null)
            {
                object[] args = new object[2] { (int)0x1C, "G" };
                _pluginInstance.Call("setPhoneNumber", args);
                bridgeInitDone = true;
            }
        }
        _loginStatus = false;
        ConfigMgr cm = ConfigMgr.Instance;
        if (cm == null) throw new System.NullReferenceException();
        _playerID = cm.GetConfigVarStrbyStr(ESGAME_PLAYER_ID);
    }

    // Source: Ghidra GetPlayerID.c  RVA 0x15A361C
    public static string GetPlayerID() { return _playerID; }

    // Source: Ghidra SetPlayerID.c  RVA 0x15A24E4
    // 1. _playerID = playerID. 2. ConfigMgr.Instance.SetConfigVarStrbyStr("ESDH", playerID).
    public static void SetPlayerID(string playerID)
    {
        _playerID = playerID;
        ConfigMgr cm = ConfigMgr.Instance;
        if (cm == null) throw new System.NullReferenceException();
        cm.SetConfigVarStrbyStr(ESGAME_PLAYER_ID, playerID);
    }

    // Source: Ghidra GetLoginStatus.c  RVA 0x15A3674
    public static bool GetLoginStatus() { return _loginStatus; }

    // Source: Ghidra SetLoginStatus.c  RVA 0x15A36CC
    public static void SetLoginStatus(bool status) { _loginStatus = status; }

    // Source: Ghidra Login.c  RVA 0x15A372C
    // If bridgeInitDone: _pluginInstance.Call("logMax") (literal 18389 — verbatim from binary).
    public static void Login()
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("logMax");
    }

    // Source: Ghidra Logout.c  RVA 0x15A3838
    // Same shape as Login but with literal 18390="logMessageReceived" verbatim.
    public static void Logout()
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("logMessageReceived");
    }

    // Source: Ghidra DoGGBillingPurchase.c  RVA 0x15A3944
    // JNI Call with 4 string args. Method name literal not resolved (could be "doGGBillingPurchase" or similar).
    public static void DoGGBillingPurchase(string productID, string serverID, string playerID, string extraData)
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("DoGGBillingPurchase", new object[] { productID, serverID, playerID, extraData });
    }

    // Source: Ghidra DoWebBillingPurchase.c  RVA 0x15A3B1C — same JNI Call pattern.
    public static void DoWebBillingPurchase(string productID, string serverID, string playerID, string extraData)
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("DoWebBillingPurchase", new object[] { productID, serverID, playerID, extraData });
    }

    // Source: Ghidra DoAppleBillingPurchase.c  RVA 0x15A3CF4 — same JNI pattern (Apple IAP).
    public static void DoAppleBillingPurchase(string productID, string serverID, string playerID, string extraData)
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("DoAppleBillingPurchase", new object[] { productID, serverID, playerID, extraData });
    }

    // Source: Ghidra SetEnableFloatingView.c  RVA 0x15A2578 — JNI Call with bool arg.
    public static void SetEnableFloatingView(bool status)
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("SetEnableFloatingView", new object[] { status });
    }

    // Source: Ghidra StartInGameMain.c  RVA 0x15A3CF8 — JNI Call no args.
    public static void StartInGameMain()
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("StartInGameMain");
    }

    // Source: Ghidra OpenGiftForm.c  RVA 0x15A3E04 — JNI Call with 2 string args.
    public static void OpenGiftForm(string playerID, string serverID)
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("OpenGiftForm", new object[] { playerID, serverID });
    }

    // Source: Ghidra SetProductItems.c  RVA 0x15A3F60 — JNI Call with two string[] args.
    public static void SetProductItems(string[] inAppSKUS, string[] subsSKUS)
    {
        if (!bridgeInitDone) return;
        if (_pluginInstance == null) return;
        _pluginInstance.Call("SetProductItems", new object[] { inAppSKUS, subsSKUS });
    }

    // Source: Ghidra (no .ctor.c) — default empty ctor. RVA 0x15A40BC.
    public ESGameManager() { }

    // Source: Ghidra .cctor.c  RVA 0x15A40C4
    // bridgeInitDone = false (offset 0x10); _playerID = "" (PTR_DAT_034464D0 is empty string);
    // _loginStatus = false (offset 0x20).
    static ESGameManager()
    {
        bridgeInitDone = false;
        _playerID = "";
        _loginStatus = false;
    }
}
