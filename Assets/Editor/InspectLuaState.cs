using System.IO;
using UnityEngine;

public class InspectLuaState
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();

        // 1) Main statics
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        if (mainType != null)
        {
            var instProp = mainType.GetProperty("Instance");
            var inst = instProp?.GetValue(null);
            sb.AppendLine("Main.Instance: " + (inst == null ? "null" : "exists"));
            var luaMgrField = mainType.GetField("_LuaMgr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var lm = luaMgrField?.GetValue(null);
            sb.AppendLine("Main._LuaMgr: " + (lm == null ? "null" : lm.GetType().FullName));
            var bLuaBundle = mainType.GetField("bLoadLuaBundle", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            sb.AppendLine("Main.bLoadLuaBundle: " + bLuaBundle?.GetValue(null));
        }

        // 2) ProcessLunchGame state
        var plgType = System.Type.GetType("ProcessLunchGame, Assembly-CSharp");
        if (plgType != null)
        {
            // find instance via GameProcMgr.Instance._curProc
            var gpmType = System.Type.GetType("GameProcMgr, Assembly-CSharp");
            var gpm = gpmType?.GetProperty("Instance")?.GetValue(null);
            sb.AppendLine("GameProcMgr.Instance: " + (gpm == null ? "null" : "exists"));
            if (gpm != null)
            {
                var curProcField = gpm.GetType().BaseType.GetField("_curProc", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var curProc = curProcField?.GetValue(gpm);
                sb.AppendLine("GameProcMgr._curProc: " + (curProc == null ? "null" : curProc.GetType().Name));
                if (curProc != null && curProc.GetType().Name == "ProcessLunchGame")
                {
                    var stepUpd = plgType.GetField("_stepUpdate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var stepLoad = plgType.GetField("_stepLoading", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var bLuaReady = plgType.GetField("bLuaReady", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var bBaseText = plgType.GetField("bBaseTextLoaded", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    sb.AppendLine("PLG._stepUpdate: " + stepUpd?.GetValue(curProc));
                    sb.AppendLine("PLG._stepLoading: " + stepLoad?.GetValue(curProc));
                    sb.AppendLine("PLG.bLuaReady: " + bLuaReady?.GetValue(curProc));
                    sb.AppendLine("PLG.bBaseTextLoaded: " + bBaseText?.GetValue(curProc));
                }
            }
        }

        // 3) ResMgr state
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        if (rmType != null)
        {
            var rmInst = rmType.GetProperty("Instance")?.GetValue(null);
            sb.AppendLine("ResMgr.Instance: " + (rmInst == null ? "null" : "exists"));
            if (rmInst != null)
            {
                var bFL = rmType.GetField("bFileListReady", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                sb.AppendLine("ResMgr.bFileListReady: " + bFL?.GetValue(rmInst));
                var luaFileLists = rmType.GetField("_LuaFileLists", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var lfl = luaFileLists?.GetValue(rmInst);
                if (lfl is System.Collections.IDictionary dict)
                {
                    sb.AppendLine("ResMgr._LuaFileLists.Count: " + dict.Count);
                    int loaded = 0, total = 0;
                    foreach (var v in dict.Values)
                    {
                        total++;
                        var bLoad = v.GetType().GetField("bLoad")?.GetValue(v);
                        if (bLoad is bool b && b) loaded++;
                    }
                    sb.AppendLine("_LuaFileLists: " + loaded + "/" + total + " loaded");
                }
            }
        }

        // 4) Check if LuaManager actually exists on a GameObject
        var luaMgrType = System.Type.GetType("LuaFramework.LuaManager, Assembly-CSharp");
        sb.AppendLine("\nLuaFramework.LuaManager type: " + (luaMgrType == null ? "NULL" : "found"));
        if (luaMgrType != null)
        {
            var all = Resources.FindObjectsOfTypeAll(luaMgrType);
            sb.AppendLine("All LuaManager instances: " + all.Length);
            foreach (var lm in all)
            {
                var lmComp = lm as MonoBehaviour;
                sb.AppendLine("  on GO=" + lmComp?.gameObject?.name + " enabled=" + lmComp?.enabled);
                // LuaState
                var luaField = luaMgrType.GetField("lua", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?? luaMgrType.GetField("_lua", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (luaField != null) sb.AppendLine("    lua state: " + (luaField.GetValue(lm) == null ? "null" : "alive"));
                else sb.AppendLine("    (no 'lua' / '_lua' field found)");
            }
        }

        // 5) tolua native plugin check
        try
        {
            System.Type toluaType = System.Type.GetType("LuaInterface.LuaDLL, Assembly-CSharp")
                                  ?? System.Type.GetType("LuaInterface.LuaDLL, ToLua")
                                  ?? System.Type.GetType("LuaInterface.LuaDLL");
            sb.AppendLine("\nLuaInterface.LuaDLL: " + (toluaType == null ? "NULL" : toluaType.AssemblyQualifiedName));
        }
        catch (System.Exception e) { sb.AppendLine("LuaDLL probe ex: " + e.Message); }

        File.WriteAllText("/tmp/lua_state.txt", sb.ToString());
        Debug.Log("[InspectLuaState] done -> /tmp/lua_state.txt");
    }
}
