// SGC paths: game Lua in StreamingAssets/Lua/, tolua# helper in Assets/ToLua/Lua/.
// Both must contain "/Lua" substring (tolua# ToLua.GetToLuaInstanceID uses LastIndexOf("/Lua")).
using UnityEngine;

public static class LuaConst
{
    public static string luaDir = Application.dataPath + "/StreamingAssets/Lua";  // SGC game Lua source
    public static string toluaDir = Application.dataPath + "/ToLua/Lua";          // tolua# helper Lua scripts

#if UNITY_STANDALONE
    public static string osDir = "Win";
#elif UNITY_ANDROID
    public static string osDir = "Android";
#elif UNITY_IPHONE
    public static string osDir = "iOS";
#else
    public static string osDir = "";
#endif

    public static string luaResDir = string.Format("{0}/{1}/Lua", Application.persistentDataPath, osDir);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    public static string zbsDir = "D:/ZeroBraneStudio/lualibs/mobdebug";
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
    public static string zbsDir = "/Applications/ZeroBraneStudio.app/Contents/ZeroBraneStudio/lualibs/mobdebug";
#else
    public static string zbsDir = luaResDir + "/mobdebug/";
#endif

    public static string emmyLuaIdeAddr = "";
    public static int emmyLuaIdePort = 9966;
    public static bool openLuaSocket = false;
    public static bool openLuaDebugger = false;
    public static bool openZbsDebugger = false;
}
