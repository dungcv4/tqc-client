// Source: Manual diag — verify icon bundle loaded by triggering ProcessLunchGame state.
using System.Reflection;
using UnityEngine;

public static class CheckBootState
{
    public static void Execute()
    {
        // Try finding all ProcessLunchGame instances via FindObjectsOfType<MonoBehaviour>
        var all = Object.FindObjectsOfType<MonoBehaviour>();
        foreach (var mb in all)
        {
            if (mb.GetType().Name == "ProcessLunchGame")
            {
                Debug.Log("[CheckBootState] found ProcessLunchGame via Object.Find");
                DumpStep(mb);
                return;
            }
        }
        Debug.LogWarning("[CheckBootState] ProcessLunchGame not found");
    }

    private static void DumpStep(object proc)
    {
        var t = proc.GetType();
        string[] interesting = { "_stepUpdate", "_stepDownload", "_state", "_stepCurrent", "_lunched", "bWaitConfirm" };
        foreach (var name in interesting)
        {
            var f = t.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f != null) Debug.Log($"  {name} = {f.GetValue(proc)}");
            else
            {
                var p = t.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (p != null) Debug.Log($"  {name} (prop) = {p.GetValue(proc)}");
            }
        }
    }
}
