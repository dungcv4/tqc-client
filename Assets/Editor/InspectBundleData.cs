using System.IO;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class InspectBundleData
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/bundle.txt", "ResMgr null"); return; }

        var lfl = rmType.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        sb.AppendLine("_LuaFileLists count: " + (lfl?.Count ?? -1));
        if (lfl == null || lfl.Count == 0) { File.WriteAllText("/tmp/bundle.txt", sb.ToString()); return; }

        var luaBundleOP = rmType.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)
            ?.GetValue(rmInst);
        sb.AppendLine("LuaBundleOP: " + (luaBundleOP != null ? luaBundleOP.GetType().FullName : "null"));

        // Pick 3 sample keys: one ToLua (bLoad=true), one non-ToLua, one with weird name
        var testKeys = new[] { "ToLua.tolua.lua", "Common.GameDef.lua", "Common.SgzFunctions.lua" };
        foreach (var key in testKeys)
        {
            sb.AppendLine();
            sb.AppendLine("=== " + key + " ===");
            if (!lfl.Contains(key)) { sb.AppendLine("  KEY NOT FOUND"); continue; }
            var lfd = lfl[key];
            var lfdType = lfd.GetType();
            var realName = lfdType.GetField("sRealName")?.GetValue(lfd);
            var hashName = lfdType.GetField("sHashName")?.GetValue(lfd);
            var bLoad = lfdType.GetField("bLoad")?.GetValue(lfd);
            sb.AppendLine("  sRealName: '" + realName + "'");
            sb.AppendLine("  sHashName: '" + hashName + "'  (len=" + (hashName as string)?.Length + ")");
            sb.AppendLine("  bLoad: " + bLoad);

            // Try load
            if (luaBundleOP != null && hashName != null)
            {
                var loadMethod = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) });
                var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
                if (loadMethod == null || holderType == null) { sb.AppendLine("  Load method/type missing"); continue; }
                try
                {
                    var script = loadMethod.Invoke(luaBundleOP, new object[] { hashName, holderType });
                    sb.AppendLine("  Loaded script: " + (script != null ? script.GetType().FullName : "null"));
                    if (script != null)
                    {
                        var d1f = script.GetType().GetField("data1");
                        var d2f = script.GetType().GetField("data2");
                        var d1 = d1f?.GetValue(script) as byte[];
                        var d2 = d2f?.GetValue(script) as byte[];
                        sb.AppendLine("  data1: " + (d1 != null ? "byte[" + d1.Length + "]" : "null"));
                        if (d1 != null && d1.Length > 0)
                        {
                            sb.AppendLine("    bytes[0..min(16,len)]: " + string.Join(" ", d1.Take(System.Math.Min(16, d1.Length)).Select(b => b.ToString("X2"))));
                            sb.AppendLine("    data1[0] = " + d1[0] + "  (length=" + d1.Length + " ⇒ " + (d1[0] < d1.Length ? "OK" : "OOB!") + ")");
                        }
                        sb.AppendLine("  data2: " + (d2 != null ? "byte[" + d2.Length + "]" : "null"));
                    }
                }
                catch (System.Exception e)
                {
                    sb.AppendLine("  Load EX: " + (e.InnerException?.Message ?? e.Message));
                }
            }
        }

        File.WriteAllText("/tmp/bundle.txt", sb.ToString());
        Debug.Log("[InspectBundleData]\n" + sb);
    }
}
