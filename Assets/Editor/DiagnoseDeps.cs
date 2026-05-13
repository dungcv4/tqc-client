using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class DiagnoseDeps
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        sb.AppendLine("rmType: " + rmType);
        if (rmType == null) { File.WriteAllText("/tmp/diag.txt", sb.ToString()); return; }

        var instProp = rmType.GetProperty("Instance");
        sb.AppendLine("instProp: " + instProp);
        var rmInst = instProp?.GetValue(null);
        sb.AppendLine("rmInst: " + (rmInst != null ? rmInst.GetType().FullName : "null"));
        if (rmInst == null) {
            // try field
            var instField = rmType.GetField("_Instance", BindingFlags.NonPublic | BindingFlags.Static);
            sb.AppendLine("_Instance field: " + instField);
            if (instField != null) {
                rmInst = instField.GetValue(null);
                sb.AppendLine("rmInst via field: " + (rmInst != null ? rmInst.GetType().FullName : "null"));
            }
            // list all static fields/props
            sb.AppendLine("=== Static fields ===");
            foreach (var f in rmType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)) {
                sb.AppendLine("  " + f.FieldType.Name + " " + f.Name);
            }
            sb.AppendLine("=== Static props ===");
            foreach (var p in rmType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)) {
                sb.AppendLine("  " + p.PropertyType.Name + " " + p.Name);
            }
        }
        if (rmInst == null) { File.WriteAllText("/tmp/diag.txt", sb.ToString()); return; }

        var lflField = rmType.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance);
        sb.AppendLine("lflField: " + lflField);
        var lflRaw = lflField?.GetValue(rmInst);
        sb.AppendLine("lflRaw: " + (lflRaw != null ? lflRaw.GetType().FullName : "null"));
        var lfl = lflRaw as System.Collections.IDictionary;
        sb.AppendLine("lfl IDictionary: " + lfl);
        if (lfl != null) sb.AppendLine("lfl.Count: " + lfl.Count);

        var bundleField = rmType.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance);
        sb.AppendLine("bundleField: " + bundleField);
        var bundleRaw = bundleField?.GetValue(rmInst);
        sb.AppendLine("bundleRaw: " + (bundleRaw != null ? bundleRaw.GetType().FullName : "null"));

        File.WriteAllText("/tmp/diag.txt", sb.ToString());
        Debug.Log("[DiagnoseDeps]\n" + sb);
    }
}
