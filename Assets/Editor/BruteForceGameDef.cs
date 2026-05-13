using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;

public class BruteForceGameDef
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType?.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        if (luaBundleOP == null || lfl == null) { File.WriteAllText("/tmp/brute_gamedef.txt", "deps null"); return; }

        string[] targets = new[] {
            "Common.GameDef.lua",
            "Common.SgzFunctions.lua",
            "ToLua.tolua.lua"
        };

        foreach (var key in targets)
        {
            sb.AppendLine("===== " + key + " =====");
            if (!lfl.Contains(key)) { sb.AppendLine("not in list"); continue; }
            var lfd = lfl[key];
            var hashName = lfd.GetType().GetField("sHashName").GetValue(lfd) as string;
            var holderType = System.Type.GetType("LuaScriptHolder, Assembly-CSharp");
            var script = luaBundleOP.GetType().GetMethod("Load", new[] { typeof(string), typeof(System.Type) })
                .Invoke(luaBundleOP, new object[] { hashName, holderType }) as ScriptableObject;
            if (script == null) { sb.AppendLine("script null"); continue; }
            var d1 = script.GetType().GetField("data1").GetValue(script) as byte[];
            var d2 = script.GetType().GetField("data2").GetValue(script) as byte[];
            if (d1 == null || d2 == null) { sb.AppendLine("d1/d2 null"); continue; }

            sb.Append("  data1 (" + d1.Length + "): ");
            for (int i = 0; i < d1.Length; i++) sb.Append(d1[i].ToString("X2") + (i < d1.Length - 1 ? "-" : ""));
            sb.AppendLine();
            sb.AppendLine("  data2.Length=" + d2.Length);

            // Take first 256 bytes for scoring
            int sampleLen = System.Math.Min(d2.Length, 4096);

            int bestKey = -1;
            int bestScore = -1;
            string bestPreview = "";
            for (int k = 0; k < 256; k++)
            {
                int score = 0;
                var preview = new StringBuilder();
                bool foundLocal = false, foundFunction = false, foundDashDash = false, foundEqualsSign = false;
                int printable = 0;
                for (int i = 0; i < sampleLen; i++)
                {
                    byte b = (byte)(d2[i] ^ k);
                    if (b == 0x09 || b == 0x0A || b == 0x0D || (b >= 0x20 && b <= 0x7E)) printable++;
                }
                // Convert to ascii
                string ascii = System.Text.Encoding.ASCII.GetString(System.Array.ConvertAll(d2, b => (byte)(b ^ k)), 0, sampleLen);
                if (ascii.Contains("local ")) { score += 5; foundLocal = true; }
                if (ascii.Contains("function ")) { score += 5; foundFunction = true; }
                if (ascii.Contains("--")) { score += 2; foundDashDash = true; }
                if (ascii.Contains("end\n") || ascii.Contains("end\r")) score += 3;
                if (ascii.Contains("then ") || ascii.Contains("then\n")) score += 3;
                if (ascii.Contains("require")) score += 4;
                if (ascii.Contains("return ")) score += 3;
                score += printable / 100;
                if (score > bestScore)
                {
                    bestScore = score;
                    bestKey = k;
                    bestPreview = ascii.Substring(0, System.Math.Min(120, ascii.Length))
                        .Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
                }
            }
            sb.AppendLine("  BEST KEY: 0x" + bestKey.ToString("X2") + " score=" + bestScore);
            sb.AppendLine("  preview: " + bestPreview);

            // Now figure out which index of d1 contains bestKey
            sb.Append("  bestKey indexes in d1: ");
            for (int i = 0; i < d1.Length; i++) if (d1[i] == bestKey) sb.Append(i + " ");
            sb.AppendLine();
            sb.AppendLine("  data1[0]=" + d1[0] + " (0x" + d1[0].ToString("X2") + ")");
            // Test masks
            byte b0 = d1[0];
            int[] candidates = new[] {
                b0 & 0x03, b0 & 0x07, b0 & 0x0F, b0 & 0x1F,
                b0 >> 3, b0 >> 4, b0 >> 5,
                b0 % d1.Length, b0 % 4, b0 % 8,
                (b0 >> 4) & 0x0F,
                (b0 & 0x0F)
            };
            string[] names = new[] {
                "&0x03", "&0x07", "&0x0F", "&0x1F",
                ">>3", ">>4", ">>5",
                "%len", "%4", "%8",
                ">>4&0x0F",
                "&0x0F"
            };
            for (int c = 0; c < candidates.Length; c++)
            {
                int idx = candidates[c];
                if (idx >= 0 && idx < d1.Length)
                {
                    bool match = d1[idx] == bestKey;
                    sb.AppendLine("    " + names[c] + " -> idx=" + idx + " d1[idx]=0x" + d1[idx].ToString("X2") + (match ? " ✓ MATCH" : ""));
                }
                else
                {
                    sb.AppendLine("    " + names[c] + " -> idx=" + idx + " OOB");
                }
            }
            sb.AppendLine();
        }

        File.WriteAllText("/tmp/brute_gamedef.txt", sb.ToString());
        Debug.Log("[BruteForceGameDef] wrote /tmp/brute_gamedef.txt");
    }
}
