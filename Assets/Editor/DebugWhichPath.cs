using System.IO;
using UnityEngine;

public class DebugWhichPath
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var inst = LuaInterface.LuaFileUtils.Instance as LuaResLoader;
        if (inst == null) { File.WriteAllText("/tmp/which_path.txt", "no LuaResLoader"); return; }

        // Test each path directly via reflection
        var methods = typeof(LuaResLoader).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        sb.AppendLine("Methods: " + string.Join(",", System.Linq.Enumerable.Select(methods, m => m.Name)));

        var readDL = typeof(LuaResLoader).GetMethod("ReadDownLoadFile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var readRes = typeof(LuaResLoader).GetMethod("ReadResourceFile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (readDL != null) {
            try {
                byte[] data = (byte[])readDL.Invoke(inst, new object[] { "Common/GameDef" });
                sb.AppendLine("ReadDownLoadFile('Common/GameDef'): " + (data != null ? data.Length + " bytes" : "NULL"));
            } catch (System.Exception e) { sb.AppendLine("ReadDownLoadFile EX: " + (e.InnerException?.Message ?? e.Message)); }
        }
        if (readRes != null) {
            try {
                byte[] data = (byte[])readRes.Invoke(inst, new object[] { "Common/GameDef" });
                sb.AppendLine("ReadResourceFile('Common/GameDef'): " + (data != null ? data.Length + " bytes" : "NULL"));
            } catch (System.Exception e) { sb.AppendLine("ReadResourceFile EX: " + (e.InnerException?.Message ?? e.Message)); }
        }

        // ResourcesPath.OutputPath
        var rpType = System.Type.GetType("ResourcesPath, Assembly-CSharp");
        if (rpType != null) {
            var op = rpType.GetField("_outputPath", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            sb.AppendLine("ResourcesPath._outputPath: " + op?.GetValue(null));
        }

        File.WriteAllText("/tmp/which_path.txt", sb.ToString());
        Debug.Log("[DebugWhichPath]\n" + sb);
    }
}
