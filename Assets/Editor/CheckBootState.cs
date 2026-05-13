using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class CheckBootState
{
    public static void Execute()
    {
        var sb = new StringBuilder();

        // ResMgr state
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        sb.AppendLine("=== ResMgr ===");
        sb.AppendLine("rmInst: " + (rmInst != null));
        if (rmInst != null)
        {
            foreach (var f in rmType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (f.Name.Contains("Bundle") || f.Name.Contains("File") || f.Name.Contains("List") || f.Name.Contains("Scene") || f.Name.Contains("FX"))
                {
                    var val = f.GetValue(rmInst);
                    string desc;
                    if (val == null) desc = "null";
                    else if (val is System.Collections.IDictionary d) desc = "Dict[" + d.Count + "]";
                    else if (val is System.Collections.ICollection c) desc = val.GetType().Name + "[" + c.Count + "]";
                    else desc = val.ToString();
                    sb.AppendLine($"  {f.Name} = {desc}");
                }
            }
        }

        // Main state
        sb.AppendLine("\n=== Main ===");
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        var mainInst = mainType?.GetProperty("Instance")?.GetValue(null);
        sb.AppendLine("mainInst: " + (mainInst != null));
        if (mainInst != null)
        {
            foreach (var f in mainType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (f.FieldType == typeof(bool) || f.FieldType == typeof(int) || f.FieldType.Name.Contains("State"))
                {
                    sb.AppendLine($"  Main.{f.Name} = {f.GetValue(mainInst)}");
                }
            }
            // also static
            foreach (var f in mainType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (f.FieldType == typeof(bool) || f.FieldType == typeof(int))
                {
                    sb.AppendLine($"  Main.{f.Name} (static) = {f.GetValue(null)}");
                }
            }
        }

        // ProcessLunchGame state
        sb.AppendLine("\n=== ProcessLunchGame ===");
        var plgType = System.Type.GetType("ProcessLunchGame, Assembly-CSharp");
        if (plgType != null)
        {
            var plgInst = plgType.GetProperty("Instance")?.GetValue(null);
            sb.AppendLine("plgInst: " + (plgInst != null));
            if (plgInst != null)
            {
                foreach (var f in plgType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (f.FieldType == typeof(bool) || f.FieldType == typeof(int) || f.FieldType.Name == "DownloadStep")
                    {
                        sb.AppendLine($"  {f.Name} = {f.GetValue(plgInst)}");
                    }
                }
            }
        }

        File.WriteAllText("/tmp/boot_state.txt", sb.ToString());
        Debug.Log("[CheckBootState]\n" + sb);
    }
}
