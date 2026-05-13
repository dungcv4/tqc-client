using System.IO;
using UnityEngine;
using LuaInterface;
using System.Reflection;

public class DebugLuaReadFile
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        // 1. Check Main.bLoadLuaBundle right now
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        var bLoadLuaBundle = mainType.GetField("bLoadLuaBundle", BindingFlags.Public | BindingFlags.Static);
        sb.AppendLine("Main.bLoadLuaBundle = " + bLoadLuaBundle?.GetValue(null));

        // 2. Check LuaFileUtils.Instance type (should be LuaResLoader subclass)
        var inst = LuaFileUtils.Instance;
        sb.AppendLine("LuaFileUtils.Instance type: " + (inst != null ? inst.GetType().FullName : "null"));

        // 3. Try ReadFile via base instance for Common/GameDef
        try {
            byte[] data = inst.ReadFile("Common/GameDef");
            sb.AppendLine("ReadFile('Common/GameDef'): " + (data != null ? data.Length + " bytes" : "NULL"));
        } catch (System.Exception e) { sb.AppendLine("ReadFile EX: " + e.Message); }

        // 4. Try GetLuaScript directly
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType.GetProperty("Instance").GetValue(null);
        if (rmInst != null) {
            var getLuaScript = rmType.GetMethod("GetLuaScript");
            try {
                var data = (byte[])getLuaScript.Invoke(rmInst, new object[] { "Common.GameDef.lua" });
                sb.AppendLine("ResMgr.GetLuaScript('Common.GameDef.lua'): " + (data != null ? data.Length + " bytes" : "NULL"));
            } catch (System.Exception e) { sb.AppendLine("GetLuaScript EX: " + (e.InnerException != null ? e.InnerException.Message : e.Message)); }
        }

        // 5. Check LuaBundleOP
        var luaBundleOP = rmType.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance);
        sb.AppendLine("ResMgr.LuaBundleOP: " + (luaBundleOP?.GetValue(rmInst) != null ? "set" : "null"));

        File.WriteAllText("/tmp/lua_readfile.txt", sb.ToString());
        Debug.Log("[DebugLuaReadFile] " + sb);
    }
}
