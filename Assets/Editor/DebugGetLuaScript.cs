using System.IO;
using UnityEngine;
using System.Reflection;

public class DebugGetLuaScript
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/get_lua_script.txt", "ResMgr null"); return; }

        var luaFileLists = rmType.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance);
        var lfl = luaFileLists.GetValue(rmInst) as System.Collections.IDictionary;
        if (lfl == null) { File.WriteAllText("/tmp/get_lua_script.txt", "_LuaFileLists null"); return; }

        // Get LuaFileData for Common.GameDef.lua
        string key = "Common.GameDef.lua";
        if (!lfl.Contains(key)) { File.WriteAllText("/tmp/get_lua_script.txt", "key not found: " + key); return; }
        var lfd = lfl[key];
        sb.AppendLine("lfd type: " + lfd.GetType().FullName);
        var lfdType = lfd.GetType();
        foreach (var f in lfdType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            sb.AppendLine("  " + f.Name + " = " + f.GetValue(lfd));
        }

        // Try LuaBundleOP.Load
        var luaBundleOP = rmType.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance);
        var op = luaBundleOP?.GetValue(rmInst);
        sb.AppendLine();
        sb.AppendLine("LuaBundleOP: " + (op != null ? op.GetType().FullName : "null"));
        if (op != null)
        {
            var loadMethod = op.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) });
            if (loadMethod != null)
            {
                var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
                sb.AppendLine("LuaScriptHolder type: " + holderType);
                var hashName = lfdType.GetField("sHashName").GetValue(lfd);
                sb.AppendLine("Loading hashName='" + hashName + "' as " + holderType);
                try
                {
                    var script = loadMethod.Invoke(op, new object[] { hashName, holderType });
                    sb.AppendLine("Loaded script: " + (script != null ? script.GetType().FullName : "null"));
                    if (script != null)
                    {
                        var scriptType = script.GetType();
                        foreach (var f in scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            var val = f.GetValue(script);
                            if (val is byte[] arr) sb.AppendLine("  " + f.Name + " = byte[" + arr.Length + "]");
                            else sb.AppendLine("  " + f.Name + " = " + val);
                        }
                    }
                }
                catch (System.Exception e) { sb.AppendLine("Load EX: " + (e.InnerException?.Message ?? e.Message)); }
            }
        }
        File.WriteAllText("/tmp/get_lua_script.txt", sb.ToString());
        Debug.Log("[DebugGetLuaScript]\n" + sb);
    }
}
