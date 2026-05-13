// Source: Manual diag — inspect job buttons' WndClickMethod (which action_type they pass).
using UnityEngine;
using UnityEngine.UI;

public static class CheckJobButton
{
    public static void Execute()
    {
        var wnd = GameObject.Find("GUI_Root/WndFormProxy/WndForm_LoginCreateChar(Clone)");
        if (wnd == null) { Debug.LogWarning("[CheckJobButton] wnd not found"); return; }
        // jobBtnGroup is _jobBtnGroup field bound in V_InitTables
        var group = FindRecursive(wnd.transform, "JobGroup");
        if (group == null) { Debug.LogWarning("[CheckJobButton] JobGroup not found, listing all transform children of wnd"); DumpTree(wnd.transform, "", 3); return; }
        Debug.Log($"[CheckJobButton] JobGroup found, childCount={group.childCount}");
        for (int i = 0; i < group.childCount; i++)
        {
            var c = group.GetChild(i);
            Debug.Log($"[{i}] '{c.name}' active={c.gameObject.activeInHierarchy}");
            var clickMethod = c.GetComponent<WndClickMethod>();
            if (clickMethod != null)
            {
                // WndClickMethod fields
                var t = clickMethod.GetType();
                foreach (var f in t.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    var v = f.GetValue(clickMethod);
                    Debug.Log($"  WndClickMethod.{f.Name} = {v}");
                }
            }
            else Debug.Log("  (no WndClickMethod)");
            var btn = c.GetComponent<Button>();
            if (btn != null) Debug.Log($"  Button: interactable={btn.interactable}");
        }
    }

    private static Transform FindRecursive(Transform t, string name)
    {
        if (t.name == name) return t;
        for (int i = 0; i < t.childCount; i++)
        {
            var r = FindRecursive(t.GetChild(i), name);
            if (r != null) return r;
        }
        return null;
    }

    private static void DumpTree(Transform t, string indent, int depth)
    {
        if (depth < 0) return;
        Debug.Log($"{indent}{t.name}");
        for (int i = 0; i < t.childCount && i < 20; i++) DumpTree(t.GetChild(i), indent + "  ", depth - 1);
    }
}
