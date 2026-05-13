using System.IO;
using UnityEngine;
using System.Reflection;

public class DumpNpcTalk
{
    public static void Execute()
    {
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/dump_npctalk.txt", "ResMgr null"); return; }

        // Call GetLuaScript to get decrypted bytes (already BOM-stripped)
        var getLua = rmType.GetMethod("GetLuaScript");
        try
        {
            var result = getLua.Invoke(rmInst, new object[] { "GameDataLua.npcTalk.lua" }) as byte[];
            if (result == null) { File.WriteAllText("/tmp/dump_npctalk.txt", "GetLuaScript returned null"); return; }
            File.WriteAllBytes("/tmp/npctalk_decrypted.lua", result);
            File.WriteAllText("/tmp/dump_npctalk.txt", "OK, " + result.Length + " bytes, first 200 bytes:\n" +
                System.Text.Encoding.ASCII.GetString(result, 0, System.Math.Min(200, result.Length)).Replace("\n", "\\n").Replace("\r", "\\r"));
            Debug.Log("[DumpNpcTalk] wrote /tmp/npctalk_decrypted.lua " + result.Length);
        }
        catch (System.Exception e)
        {
            File.WriteAllText("/tmp/dump_npctalk.txt", "EX: " + (e.InnerException?.Message ?? e.Message));
        }
    }
}
