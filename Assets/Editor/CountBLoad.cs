using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class CountBLoad
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType?.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        sb.AppendLine("rmInst: " + (rmInst != null));
        sb.AppendLine("lfl: " + (lfl != null ? lfl.Count.ToString() : "null"));
        sb.AppendLine("luaBundleOP: " + (luaBundleOP != null));
        if (lfl == null || lfl.Count == 0) { File.WriteAllText("/tmp/bload_count.txt", sb.ToString()); return; }

        int total = 0, loaded = 0, notLoaded = 0;
        var notLoadedSamples = new System.Collections.Generic.List<string>();
        var loadedSamples = new System.Collections.Generic.List<string>();
        foreach (System.Collections.DictionaryEntry kv in lfl)
        {
            total++;
            var lfd = kv.Value;
            var bLoad = (bool)lfd.GetType().GetField("bLoad").GetValue(lfd);
            string name = (string)kv.Key;
            if (bLoad) { loaded++; if (loadedSamples.Count < 8) loadedSamples.Add(name); }
            else { notLoaded++; if (notLoadedSamples.Count < 8) notLoadedSamples.Add(name); }
        }
        sb.AppendLine($"total={total}, loaded={loaded}, notLoaded={notLoaded}");
        sb.AppendLine("loaded samples: " + string.Join(", ", loadedSamples));
        sb.AppendLine("notLoaded samples: " + string.Join(", ", notLoadedSamples));

        // Also check Main state
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        if (mainType != null)
        {
            var mainInst = mainType.GetProperty("Instance")?.GetValue(null);
            if (mainInst != null)
            {
                foreach (var f in mainType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (f.Name.Contains("state") || f.Name.Contains("State") || f.Name.Contains("Step"))
                    {
                        sb.AppendLine($"  Main.{f.Name} = {f.GetValue(mainInst)}");
                    }
                }
            }
        }

        File.WriteAllText("/tmp/bload_count.txt", sb.ToString());
        Debug.Log("[CountBLoad]\n" + sb);
    }
}
