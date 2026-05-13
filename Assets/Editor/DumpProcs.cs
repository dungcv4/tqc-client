using UnityEngine;
using System;
using System.Reflection;

public static class DumpProcs
{
    public static string Execute()
    {
        var sb = new System.Text.StringBuilder();
        // Access ProcFactory._mapCreator via reflection
        var t = typeof(ProcFactory);
        var f = t.GetField("_mapCreator", BindingFlags.Static | BindingFlags.NonPublic);
        var dict = f.GetValue(null) as System.Collections.IDictionary;
        sb.AppendLine($"ProcFactory._mapCreator count={dict.Count}");
        foreach (System.Collections.DictionaryEntry kv in dict)
        {
            sb.AppendLine($"  key={kv.Key} ({(EProcID)(int)kv.Key}) → type={((Type)kv.Value).FullName}");
        }
        // Check Main.lunchProcID
        var main = Main.Instance;
        if (main != null)
        {
            sb.AppendLine($"Main.lunchProcID = {main.lunchProcID} ({(int)main.lunchProcID})");
        }
        // Check GameProcMgr state
        var cpm = GameProcMgr.Instance;
        if (cpm != null)
        {
            var cpmType = typeof(CProcManager);
            var curField = cpmType.GetField("_curProc", BindingFlags.Instance | BindingFlags.NonPublic);
            var styleField = cpmType.GetField("_eChangeStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            var idField = cpmType.GetField("_eChangeProcID", BindingFlags.Instance | BindingFlags.NonPublic);
            sb.AppendLine($"CProcManager._curProc = {curField.GetValue(cpm)?.GetType().FullName ?? "null"}");
            sb.AppendLine($"CProcManager._eChangeStyle = {styleField.GetValue(cpm)}");
            sb.AppendLine($"CProcManager._eChangeProcID = {idField.GetValue(cpm)}");
        }
        return sb.ToString();
    }
}
