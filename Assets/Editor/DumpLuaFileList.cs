using System.IO;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class DumpLuaFileList
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/lua_files.txt", "ResMgr null"); return; }
        var luaFileLists = rmType.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance);
        var lfl = luaFileLists?.GetValue(rmInst);
        if (lfl is System.Collections.IDictionary dict)
        {
            sb.AppendLine("Total keys: " + dict.Count);
            sb.AppendLine();
            // Search for tolua-like keys
            sb.AppendLine("=== Keys matching 'tolua' (case-insensitive) ===");
            foreach (var key in dict.Keys)
            {
                string s = key.ToString();
                if (s.ToLower().Contains("tolua"))
                {
                    var val = dict[key];
                    var bLoad = val.GetType().GetField("bLoad")?.GetValue(val);
                    sb.AppendLine($"  '{s}' bLoad={bLoad}");
                }
            }
            sb.AppendLine();
            // Sample first 30 keys
            sb.AppendLine("=== First 30 keys ===");
            int n = 0;
            foreach (var key in dict.Keys)
            {
                if (n++ >= 30) break;
                var val = dict[key];
                var bLoad = val.GetType().GetField("bLoad")?.GetValue(val);
                sb.AppendLine($"  '{key}' bLoad={bLoad}");
            }
        }
        File.WriteAllText("/tmp/lua_files.txt", sb.ToString());
        Debug.Log("[DumpLuaFileList] done");
    }
}
