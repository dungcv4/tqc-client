using System.IO;
using UnityEngine;
using System.Reflection;

public class InspectMain
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        var mains = Object.FindObjectsOfType(mainType, true);
        sb.AppendLine("Main instances: " + mains.Length);
        foreach (var m in mains)
        {
            var mb = m as MonoBehaviour;
            sb.AppendLine("==== Main on '" + mb.gameObject.name + "' active=" + mb.gameObject.activeSelf + " enabled=" + mb.enabled);

            // Check fields that Awake would have set
            var siField = mainType.GetField("s_instance", BindingFlags.NonPublic | BindingFlags.Static);
            sb.AppendLine("  s_instance: " + (siField?.GetValue(null) == null ? "null" : "set"));

            var bLoadBundleField = mainType.GetField("bLoadBundle", BindingFlags.Public | BindingFlags.Static);
            var bLoadLuaBundleField = mainType.GetField("bLoadLuaBundle", BindingFlags.Public | BindingFlags.Static);
            sb.AppendLine("  bLoadBundle: " + bLoadBundleField?.GetValue(null));
            sb.AppendLine("  bLoadLuaBundle: " + bLoadLuaBundleField?.GetValue(null));

            var bInitScreen = mainType.GetField("bInitScreenOrigin", BindingFlags.Public | BindingFlags.Static);
            sb.AppendLine("  bInitScreenOrigin: " + bInitScreen?.GetValue(null));

            var astarField = mainType.GetField("_astarmgr", BindingFlags.NonPublic | BindingFlags.Instance);
            sb.AppendLine("  _astarmgr: " + (astarField?.GetValue(m) == null ? "null" : "set"));

            // Iterate components of GO
            foreach (var c in mb.gameObject.GetComponents<Component>())
                sb.AppendLine("    comp: " + (c == null ? "<missing/null>" : c.GetType().FullName));
        }

        // Other key statics
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        sb.AppendLine("\nResMgr.Instance: " + (rmType?.GetProperty("Instance")?.GetValue(null) == null ? "null" : "set"));
        var gpmType = System.Type.GetType("GameProcMgr, Assembly-CSharp");
        sb.AppendLine("GameProcMgr.Instance: " + (gpmType?.GetProperty("Instance")?.GetValue(null) == null ? "null" : "set"));

        // Application time
        sb.AppendLine("\nTime.timeSinceLevelLoad: " + Time.timeSinceLevelLoad);
        sb.AppendLine("Time.realtimeSinceStartup: " + Time.realtimeSinceStartup);
        sb.AppendLine("Application.isPlaying: " + Application.isPlaying);

        File.WriteAllText("/tmp/main_inspect.txt", sb.ToString());
        Debug.Log("[InspectMain] done");
    }
}
