using System.IO;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

public class TestDecryptHypothesis
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        if (rmInst == null) { File.WriteAllText("/tmp/decrypt.txt", "ResMgr null"); return; }

        var lfl = rmType.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        if (lfl == null || lfl.Count == 0) { File.WriteAllText("/tmp/decrypt.txt", "_LuaFileLists empty"); return; }

        var luaBundleOP = rmType.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        if (luaBundleOP == null) { File.WriteAllText("/tmp/decrypt.txt", "LuaBundleOP null"); return; }

        var key = "ToLua.tolua.lua";
        var lfd = lfl[key];
        var hashName = lfd.GetType().GetField("sHashName")?.GetValue(lfd) as string;
        var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
        var script = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) })
            .Invoke(luaBundleOP, new object[] { hashName, holderType });
        if (script == null) { File.WriteAllText("/tmp/decrypt.txt", "script null for " + key); return; }
        var d1 = script.GetType().GetField("data1").GetValue(script) as byte[];
        var d2 = script.GetType().GetField("data2").GetValue(script) as byte[];
        sb.AppendLine("File: " + key);
        sb.AppendLine("data1[" + d1.Length + "]: " + string.Join(" ", System.Linq.Enumerable.Select(d1, b => b.ToString("X2"))));
        sb.AppendLine("data2.Length = " + d2.Length);
        sb.AppendLine("data2[0..15]: " + string.Join(" ", System.Linq.Enumerable.Select(System.Linq.Enumerable.Take(d2, 16), (byte b) => b.ToString("X2"))));

        // Test 3 decryption hypotheses
        // H1: Ghidra direct: key = data1[data1[0]] — fails for data1[0] >= data1.Length
        // H2: Modulo: key = data1[data1[0] % data1.Length]
        // H3: data1[0] is key directly: key = data1[0]
        // H4: like ProcessFileList — chain mod: key = data1[ data1[ data1[0] % len ] % len ]
        // H5: key = data1[1] (always second byte)

        System.Func<byte[], byte, byte[]> xor = (data, k) => {
            byte[] r = new byte[System.Math.Min(32, data.Length)];
            for (int i = 0; i < r.Length; i++) r[i] = (byte)(data[i] ^ k);
            return r;
        };

        for (int testKeyIdx = 0; testKeyIdx < d1.Length; testKeyIdx++)
        {
            byte k = d1[testKeyIdx];
            byte[] head = xor(d2, k);
            string hex = string.Join(" ", System.Linq.Enumerable.Select(head, (byte b) => b.ToString("X2")));
            string ascii = "";
            foreach (var b in head) ascii += (b >= 0x20 && b < 0x7F) ? (char)b : '.';
            string note = "";
            if (head.Length >= 4 && head[0] == 0x1B && head[1] == 'L' && head[2] == 'u' && head[3] == 'a') note = " ★ LUA MAGIC!";
            if (head.Length >= 4 && head[0] == 'F' && head[1] == 'a' && head[2] == 'k' && head[3] == 'e') note = " ★ FakeEnum source!";
            sb.AppendLine("  key=data1[" + testKeyIdx + "]=0x" + k.ToString("X2") + ": " + ascii + note);
        }

        File.WriteAllText("/tmp/decrypt.txt", sb.ToString());
        Debug.Log("[TestDecryptHypothesis]\n" + sb);
    }
}
