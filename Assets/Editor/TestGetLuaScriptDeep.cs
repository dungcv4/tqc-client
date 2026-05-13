using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class TestGetLuaScriptDeep
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType?.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);

        string[] keys = new[] {
            "Common.GameDef.lua",
            "Logic.SGCLuaMgr.lua",
            "ToLua.tolua.lua",
        };

        sb.AppendLine($"lfl.Count = {lfl?.Count}");
        sb.AppendLine($"LuaBundleOP = {(luaBundleOP != null ? luaBundleOP.GetType().FullName : "null")}");
        sb.AppendLine();

        foreach (var k in keys)
        {
            sb.AppendLine($"=== {k} ===");
            sb.AppendLine($"  Contains: {lfl?.Contains(k)}");
            if (lfl == null || !lfl.Contains(k)) continue;
            var lfd = lfl[k];
            var hashName = lfd.GetType().GetField("sHashName").GetValue(lfd) as string;
            var bLoad = (bool)lfd.GetType().GetField("bLoad").GetValue(lfd);
            sb.AppendLine($"  sHashName: {hashName}");
            sb.AppendLine($"  bLoad: {bLoad}");

            if (luaBundleOP == null) continue;
            var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
            var loadMethod = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) });
            sb.AppendLine($"  holderType: {holderType}");
            sb.AppendLine($"  loadMethod: {loadMethod}");
            try
            {
                var script = loadMethod.Invoke(luaBundleOP, new object[] { hashName, holderType });
                sb.AppendLine($"  Load result: {(script == null ? "null" : script.GetType().FullName)}");
                if (script is ScriptableObject so)
                {
                    var d1 = so.GetType().GetField("data1").GetValue(so) as byte[];
                    var d2 = so.GetType().GetField("data2").GetValue(so) as byte[];
                    sb.AppendLine($"  data1.Length: {d1?.Length}");
                    sb.AppendLine($"  data2.Length: {d2?.Length}");
                    if (d1 != null && d1.Length > 0)
                    {
                        sb.Append($"  data1[0..min(10,len)]: ");
                        for (int i = 0; i < System.Math.Min(d1.Length, 10); i++) sb.Append(d1[i].ToString("X2") + " ");
                        sb.AppendLine();
                    }
                }
            }
            catch (System.Exception e)
            {
                sb.AppendLine($"  Load EXCEPTION: " + (e.InnerException?.Message ?? e.Message));
            }
            sb.AppendLine();
        }

        File.WriteAllText("/tmp/test_deep.txt", sb.ToString());
        Debug.Log("[TestGetLuaScriptDeep]\n" + sb);
    }
}
