using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class SearchNpcTalk
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        if (lfl == null) { File.WriteAllText("/tmp/search_npctalk.txt", "null"); return; }
        foreach (System.Collections.DictionaryEntry kv in lfl)
        {
            string k = kv.Key as string;
            if (k != null && k.ToLower().Contains("npctalk"))
            {
                var lfd = kv.Value;
                var bLoad = (bool)lfd.GetType().GetField("bLoad").GetValue(lfd);
                sb.AppendLine($"  '{k}' bLoad={bLoad}");
            }
        }
        File.WriteAllText("/tmp/search_npctalk.txt", sb.ToString());
        Debug.Log("[SearchNpcTalk]\n" + sb);
    }
}
