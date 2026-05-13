using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class BruteForceDecrypt
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/brute.txt", "ResMgr null"); return; }
        var lfl = rmType.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        if (luaBundleOP == null || lfl == null) { File.WriteAllText("/tmp/brute.txt", "deps null"); return; }

        var lfd = lfl["ToLua.tolua.lua"];
        var hashName = lfd.GetType().GetField("sHashName").GetValue(lfd) as string;
        var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
        var script = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) })
            .Invoke(luaBundleOP, new object[] { hashName, holderType });
        var d1 = script.GetType().GetField("data1").GetValue(script) as byte[];
        var d2 = script.GetType().GetField("data2").GetValue(script) as byte[];

        sb.AppendLine("data1 (10): " + System.BitConverter.ToString(d1));
        sb.AppendLine("data2[0..31]: " + System.BitConverter.ToString(d2, 0, System.Math.Min(32, d2.Length)));
        sb.AppendLine();
        sb.AppendLine("=== Brute force all 256 XOR keys ===");

        // Find keys that produce recognizable text
        for (int k = 0; k < 256; k++)
        {
            byte[] decoded = new byte[32];
            for (int i = 0; i < decoded.Length; i++) decoded[i] = (byte)(d2[i] ^ k);

            // Check for Lua signatures / common text
            string s = "";
            int printable = 0;
            foreach (var b in decoded)
            {
                if (b >= 0x20 && b < 0x7F) { s += (char)b; printable++; }
                else if (b == 0x0A || b == 0x0D) { s += '\\'; printable++; }
                else s += '.';
            }

            bool isLuaMagic = decoded[0] == 0x1B && decoded[1] == 'L' && (decoded[2] == 'u' || decoded[2] == 'J');
            bool likelyText = printable >= 16;  // >50% printable

            if (isLuaMagic || likelyText)
            {
                sb.AppendLine("key=0x" + k.ToString("X2") + " (" + printable + "/" + 32 + " printable): " + s + (isLuaMagic ? " ★ LUA!" : ""));
            }
        }

        File.WriteAllText("/tmp/brute.txt", sb.ToString());
        Debug.Log("[BruteForceDecrypt]\n" + sb);
    }
}
