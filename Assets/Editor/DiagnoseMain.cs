using UnityEngine;
using UnityEditor;

public static class DiagnoseMain
{
    public static string Execute()
    {
        // Check Main GameObject state
        var allMains = Resources.FindObjectsOfTypeAll<Main>();
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Main instances found: " + allMains.Length);
        foreach (var m in allMains)
        {
            sb.AppendLine($"  Main: GO={m.gameObject.name} active={m.gameObject.activeInHierarchy} enabled={m.enabled}");
            sb.AppendLine($"    Main.Instance == this? " + (Main.Instance == m));
        }
        sb.AppendLine("Main.Instance != null: " + (Main.Instance != null));
        sb.AppendLine("GameProcMgr.Instance != null: " + (GameProcMgr.Instance != null));
        sb.AppendLine("WndRoot.uiCamera != null: " + (WndRoot.uiCamera != null));
        sb.AppendLine("ProcFactory.get_count: " + (Main.Instance != null ? ProcFactory.get_count().ToString() : "(skip)"));
        return sb.ToString();
    }
}
