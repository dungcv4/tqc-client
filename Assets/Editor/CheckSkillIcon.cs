// Source: Manual diag — inspect SkillIcon GameObjects (full component list + descendants).
using UnityEngine;
using UnityEngine.UI;

public static class CheckSkillIcon
{
    public static void Execute()
    {
        var wnd = GameObject.Find("GUI_Root/WndFormProxy/WndForm_LoginCreateChar(Clone)");
        if (wnd == null) { Debug.LogWarning("[CheckSkillIcon] wnd not found"); return; }
        var group = wnd.transform.Find("SelectJOB_LeftArea/JobExplanationGroup/SkillIconGroup");
        if (group == null) { Debug.LogWarning("[CheckSkillIcon] SkillIconGroup not found"); return; }
        Debug.Log($"[CheckSkillIcon] SkillIconGroup childCount={group.childCount}");
        for (int i = 0; i < group.childCount; i++)
        {
            var c = group.GetChild(i);
            Debug.Log($"  [{i}] '{c.name}' active={c.gameObject.activeInHierarchy} childCount={c.childCount}");
            DumpComponents(c, "    ");
            for (int j = 0; j < c.childCount; j++)
            {
                var cc = c.GetChild(j);
                Debug.Log($"    Child[{j}] '{cc.name}' active={cc.gameObject.activeInHierarchy}");
                DumpComponents(cc, "      ");
            }
        }
    }

    private static void DumpComponents(Transform t, string indent)
    {
        var comps = t.GetComponents<Component>();
        foreach (var c in comps)
        {
            if (c == null) { Debug.Log($"{indent}<MissingScript>"); continue; }
            string extra = "";
            if (c is Image img)
                extra = $" sprite={(img.sprite==null?"NULL":img.sprite.name)} mat={(img.material==null?"NULL":img.material.name)} shader={(img.material!=null&&img.material.shader!=null?img.material.shader.name:"NULL")} color={img.color}";
            else if (c is RawImage ri)
                extra = $" tex={(ri.texture==null?"NULL":ri.texture.name)}";
            else if (c is MeshRenderer mr)
                extra = $" mat={(mr.sharedMaterial==null?"NULL":mr.sharedMaterial.name)} shader={(mr.sharedMaterial!=null&&mr.sharedMaterial.shader!=null?mr.sharedMaterial.shader.name:"NULL")}";
            Debug.Log($"{indent}+ {c.GetType().Name}{extra}");
        }
    }
}
