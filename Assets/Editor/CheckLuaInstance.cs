using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class CheckLuaInstance
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var lfuType = System.Type.GetType("LuaInterface.LuaFileUtils, Assembly-CSharp")
                    ?? System.Type.GetType("LuaInterface.LuaFileUtils, ToLua")
                    ?? System.Type.GetType("LuaInterface.LuaFileUtils");
        sb.AppendLine("LuaFileUtils type: " + lfuType);
        if (lfuType == null)
        {
            // Search in all loaded assemblies
            foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                var t = asm.GetType("LuaInterface.LuaFileUtils");
                if (t != null) { lfuType = t; sb.AppendLine("Found in: " + asm.GetName().Name); break; }
            }
        }
        if (lfuType != null)
        {
            var instField = lfuType.GetField("instance", BindingFlags.NonPublic | BindingFlags.Static);
            sb.AppendLine("instance field: " + instField);
            if (instField != null)
            {
                var inst = instField.GetValue(null);
                sb.AppendLine("instance value: " + (inst != null ? inst.GetType().FullName : "null"));
            }
            var instProp = lfuType.GetProperty("Instance");
            sb.AppendLine("Instance prop: " + instProp);
            if (instProp != null)
            {
                var inst = instProp.GetValue(null);
                sb.AppendLine("Instance value: " + (inst != null ? inst.GetType().FullName : "null"));
            }
        }

        var lrlType = System.Type.GetType("LuaResLoader, Assembly-CSharp");
        sb.AppendLine("LuaResLoader type: " + lrlType);

        // Check Main.bLoadLuaBundle
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        var blbField = mainType?.GetField("bLoadLuaBundle", BindingFlags.Public | BindingFlags.Static);
        sb.AppendLine("Main.bLoadLuaBundle: " + (blbField?.GetValue(null)));

        File.WriteAllText("/tmp/lua_instance.txt", sb.ToString());
        Debug.Log("[CheckLuaInstance]\n" + sb);
    }
}
