using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class TestGetLuaScript
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/test_getluascript.txt", "ResMgr null"); return; }

        string[] keys = new[] {
            "ToLua.tolua.lua",
            "Common.GameDef.lua",
            "Common.SgzFunctions.lua",
            "Logic.SGCLuaMgr.lua",
            "Main.lua",
        };

        var getLua = rmType.GetMethod("GetLuaScript");
        foreach (var k in keys)
        {
            try
            {
                var result = getLua.Invoke(rmInst, new object[] { k });
                if (result is byte[] bytes)
                {
                    sb.Append($"  {k} → byte[{bytes.Length}]: ");
                    int n = System.Math.Min(bytes.Length, 32);
                    for (int i = 0; i < n; i++) sb.Append(bytes[i].ToString("X2") + " ");
                    string ascii = System.Text.Encoding.ASCII.GetString(bytes, 0, System.Math.Min(80, bytes.Length));
                    var clean = new StringBuilder();
                    foreach (var c in ascii) clean.Append((c >= ' ' && c <= '~') ? c : '.');
                    sb.AppendLine();
                    sb.AppendLine("    ascii: " + clean);
                }
                else if (result == null) sb.AppendLine($"  {k} → null");
                else sb.AppendLine($"  {k} → {result.GetType().Name}");
            }
            catch (System.Exception e)
            {
                sb.AppendLine($"  {k} → EXCEPTION: " + (e.InnerException?.Message ?? e.Message));
            }
        }

        File.WriteAllText("/tmp/test_getluascript.txt", sb.ToString());
        Debug.Log("[TestGetLuaScript]\n" + sb);
    }
}
