using UnityEngine;

public static class DumpSGCLang
{
    public static string Execute()
    {
        var sb = new System.Text.StringBuilder();
        var sls = Resources.FindObjectsOfTypeAll<SGCLanguageSelect>();
        foreach (var s in sls)
        {
            sb.AppendLine($"SGCLanguageSelect on {s.gameObject.name}");
            // Note: titleTextList/contentTextList/confirmTextList are private — use reflection
            var t = typeof(SGCLanguageSelect);
            foreach (var f in new[] { "titleTextList", "contentTextList", "confirmTextList" })
            {
                var fi = t.GetField(f, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (fi != null)
                {
                    var arr = fi.GetValue(s) as string[];
                    sb.AppendLine($"  {f}: {(arr == null ? "NULL" : "len=" + arr.Length)}");
                    if (arr != null) for (int i = 0; i < arr.Length; i++) sb.AppendLine($"    [{i}] = '{arr[i]}'");
                }
                else
                {
                    sb.AppendLine($"  {f}: field-not-found");
                }
            }
        }
        return sb.ToString();
    }
}
