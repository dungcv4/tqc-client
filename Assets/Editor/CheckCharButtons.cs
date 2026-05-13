// Source: Manual diag — inspect YesBtn/NoBtn state + mask + lockFlag.
using UnityEngine;
using System.Reflection;

public static class CheckCharButtons
{
    public static void Execute()
    {
        var wnd = GameObject.Find("GUI_Root/WndFormProxy/WndForm_LoginCreateChar(Clone)");
        if (wnd == null) { Debug.LogWarning("[CheckCharButtons] wnd not found"); return; }
        // Find YesBtn / NoBtn / YesBtnMask
        var yes = FindRecursive(wnd.transform, "YesBtn");
        var no = FindRecursive(wnd.transform, "NoBtn");
        var mask = FindRecursive(wnd.transform, "YesBtnMask");
        DumpGo("YesBtn", yes);
        DumpGo("NoBtn", no);
        DumpGo("YesBtnMask", mask);
        // Walk YesBtn children
        if (yes != null)
        {
            Debug.Log($"  YesBtn childCount={yes.childCount}");
            for (int i = 0; i < yes.childCount; i++)
            {
                var c = yes.GetChild(i);
                Debug.Log($"    Child[{i}] '{c.name}' active={c.gameObject.activeInHierarchy}");
            }
        }
    }

    private static void DumpGo(string label, Transform t)
    {
        if (t == null) { Debug.Log($"[{label}] NOT FOUND"); return; }
        var g = t.gameObject;
        Debug.Log($"[{label}] path='{GetPath(t)}' activeSelf={g.activeSelf} activeInHierarchy={g.activeInHierarchy}");
        foreach (var c in g.GetComponents<Component>())
        {
            if (c == null) continue;
            string extra = "";
            if (c is UnityEngine.UI.Image img) extra = $" raycastTarget={img.raycastTarget} sprite={(img.sprite==null?"NULL":img.sprite.name)} color={img.color}";
            if (c is UnityEngine.UI.Graphic g2) extra += $" raycastTarget={g2.raycastTarget}";
            if (c is CanvasGroup cg) extra = $" interactable={cg.interactable} blocksRaycasts={cg.blocksRaycasts} alpha={cg.alpha}";
            Debug.Log($"  + {c.GetType().Name}{extra}");
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

    private static string GetPath(Transform t)
    {
        var sb = new System.Text.StringBuilder();
        var cur = t;
        while (cur != null) { if (sb.Length > 0) sb.Insert(0, '/'); sb.Insert(0, cur.name); cur = cur.parent; }
        return sb.ToString();
    }
}
