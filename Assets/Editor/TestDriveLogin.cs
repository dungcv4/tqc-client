// Source: Manual (test driver — clicks the StartGame button inside WndForm_LoginGame to trigger login flow)
using UnityEngine;
using UnityEngine.UI;

public static class TestDriveLogin
{
    public static void Execute()
    {
        var btns = Object.FindObjectsOfType<Button>(true);
        foreach (var b in btns)
        {
            if (b.gameObject.name == "StartGame" && b.gameObject.activeInHierarchy && b.interactable)
            {
                Debug.Log($"[TestDriveLogin] clicking StartGame at path='{GetPath(b.transform)}'");
                b.onClick.Invoke();
                return;
            }
        }
        Debug.LogWarning("[TestDriveLogin] StartGame button NOT found / not active");
    }

    private static string GetPath(Transform t)
    {
        var sb = new System.Text.StringBuilder();
        var cur = t;
        while (cur != null)
        {
            if (sb.Length > 0) sb.Insert(0, '/');
            sb.Insert(0, cur.name);
            cur = cur.parent;
        }
        return sb.ToString();
    }
}
