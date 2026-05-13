using System.IO;
using UnityEngine;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

public class BruteForceTop5
{
    public static void Execute()
    {
        var sb = new StringBuilder();
        var rmType = System.Type.GetType("ResMgr, Assembly-CSharp");
        var rmInst = rmType?.GetProperty("Instance")?.GetValue(null);
        var lfl = rmType?.GetField("_LuaFileLists", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rmInst) as System.Collections.IDictionary;
        var luaBundleOP = rmType?.GetField("LuaBundleOP", BindingFlags.Public | BindingFlags.Instance)?.GetValue(rmInst);
        if (luaBundleOP == null || lfl == null) { File.WriteAllText("/tmp/brute_top5.txt", "deps null"); return; }

        string[] targets = new[] {
            "Common.GameDef.lua",
            "Common.SgzFunctions.lua",
            "ToLua.tolua.lua",
            "Common.RhythmStep.lua",  // arbitrary 4th sample
            "Main.lua"
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
            sb.AppendLine("  data2.Length=" + d2.Length + ", data2[0..7]=" + string.Join("-", System.Array.ConvertAll(new int[8], i => "")) );
            sb.Append("  data2[0..15]: ");
            int previewLen = System.Math.Min(d2.Length, 16);
            for (int i = 0; i < previewLen; i++) sb.Append(d2[i].ToString("X2") + " ");
            sb.AppendLine();

            int sampleLen = System.Math.Min(d2.Length, 4096);

            // Score all 256 keys
            var scores = new List<(int key, int score, string preview)>();
            for (int k = 0; k < 256; k++)
            {
                int score = 0;
                string ascii = System.Text.Encoding.ASCII.GetString(System.Array.ConvertAll(d2, b => (byte)(b ^ k)), 0, sampleLen);
                if (ascii.Contains("local ")) score += 10;
                if (ascii.Contains("function ")) score += 10;
                if (ascii.Contains("-- ")) score += 4;
                if (ascii.Contains("end\n") || ascii.Contains("end\r")) score += 6;
                if (ascii.Contains("then ") || ascii.Contains("then\n") || ascii.Contains("then\r")) score += 6;
                if (ascii.Contains("require")) score += 8;
                if (ascii.Contains("return ")) score += 6;
                if (ascii.Contains("nil")) score += 3;
                if (ascii.Contains("self.")) score += 4;
                if (ascii.Contains("self:")) score += 4;
                int printable = 0;
                for (int i = 0; i < sampleLen; i++)
                {
                    byte b = (byte)(d2[i] ^ k);
                    if (b == 0x09 || b == 0x0A || b == 0x0D || (b >= 0x20 && b <= 0x7E)) printable++;
                }
                score += printable / 50;

                string p = ascii.Substring(0, System.Math.Min(60, ascii.Length))
                    .Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
                // strip non-ascii printable
                var clean = new StringBuilder();
                foreach (var c in p) clean.Append((c >= ' ' && c <= '~') || c=='\\' ? c : '.');
                scores.Add((k, score, clean.ToString()));
            }
            scores.Sort((a, b) => b.score.CompareTo(a.score));
            sb.AppendLine("  Top 5 candidates:");
            for (int i = 0; i < 5; i++)
            {
                var s = scores[i];
                string idxInD1 = "";
                for (int j = 0; j < d1.Length; j++) if (d1[j] == s.key) idxInD1 += j + ",";
                sb.AppendLine($"    #{i+1} key=0x{s.key:X2} score={s.score} idxInD1=[{idxInD1.TrimEnd(',')}] : {s.preview}");
            }
            sb.AppendLine();
        }

        File.WriteAllText("/tmp/brute_top5.txt", sb.ToString());
        Debug.Log("[BruteForceTop5] wrote /tmp/brute_top5.txt");
    }
}
