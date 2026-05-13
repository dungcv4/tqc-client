using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class DumpRawData
{
    public static void Execute()
    {
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType?.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        if (luaBundleOP == null || lfl == null) return;

        // Dump tolua.lua raw bundled bytes
        var lfd = lfl["ToLua.tolua.lua"];
        var hashName = lfd.GetType().GetField("sHashName").GetValue(lfd) as string;
        var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
        var script = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) })
            .Invoke(luaBundleOP, new object[] { hashName, holderType }) as ScriptableObject;
        var d1 = script.GetType().GetField("data1").GetValue(script) as byte[];
        var d2 = script.GetType().GetField("data2").GetValue(script) as byte[];

        File.WriteAllBytes("/tmp/tolua_data1.bin", d1);
        File.WriteAllBytes("/tmp/tolua_data2.bin", d2);
        Debug.Log("[DumpRawData] dumped data1=" + d1.Length + " data2=" + d2.Length);
    }
}
