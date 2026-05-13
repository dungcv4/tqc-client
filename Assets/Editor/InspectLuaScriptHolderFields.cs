using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class InspectLuaScriptHolderFields
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType?.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        if (luaBundleOP == null || lfl == null) { File.WriteAllText("/tmp/holder_fields.txt", "deps null"); return; }

        var lfd = lfl["ToLua.tolua.lua"];
        var hashName = lfd.GetType().GetField("sHashName").GetValue(lfd) as string;
        var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
        var script = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) })
            .Invoke(luaBundleOP, new object[] { hashName, holderType }) as ScriptableObject;
        if (script == null) { File.WriteAllText("/tmp/holder_fields.txt", "script null"); return; }

        sb.AppendLine("LuaScriptHolder asset name: " + script.name);
        sb.AppendLine("Type: " + script.GetType().FullName);
        sb.AppendLine();

        // List ALL fields (public + private)
        sb.AppendLine("=== Public+Private fields ===");
        foreach (var f in script.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var val = f.GetValue(script);
            string desc;
            if (val is byte[] arr) desc = "byte[" + arr.Length + "]";
            else desc = val?.ToString() ?? "null";
            sb.AppendLine($"  {f.FieldType.Name} {f.Name} = {desc}");
        }
        sb.AppendLine();

        // Use Unity's SerializedObject to inspect serialized fields
        var so = new UnityEditor.SerializedObject(script);
        sb.AppendLine("=== UnityEditor.SerializedObject iteration ===");
        var prop = so.GetIterator();
        bool enterChildren = true;
        while (prop.NextVisible(enterChildren))
        {
            enterChildren = false;
            sb.AppendLine($"  {prop.propertyType} {prop.name} (path={prop.propertyPath})");
            if (prop.propertyType == UnityEditor.SerializedPropertyType.Generic && prop.isArray && prop.arrayElementType == "char")
            {
                sb.AppendLine("    arraySize: " + prop.arraySize);
            }
        }

        File.WriteAllText("/tmp/holder_fields.txt", sb.ToString());
        Debug.Log("[InspectLuaScriptHolderFields]\n" + sb);
    }
}
