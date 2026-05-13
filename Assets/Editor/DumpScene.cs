using UnityEngine;
using UnityEditor;
using System.Text;

public static class DumpScene
{
    public static string Execute()
    {
        var sb = new StringBuilder();
        // Find all PermissionCheck instances
        var pcs = Resources.FindObjectsOfTypeAll<PermissionCheck>();
        sb.AppendLine("PermissionCheck instances: " + pcs.Length);
        foreach (var pc in pcs)
        {
            sb.AppendLine("---");
            sb.AppendLine($"GO={GetPath(pc.gameObject)} active={pc.gameObject.activeInHierarchy} selfActive={pc.gameObject.activeSelf}");
            sb.AppendLine($"  permissionObj: {(pc.permissionObj == null ? "NULL" : GetPath(pc.permissionObj))}");
            sb.AppendLine($"  title: {(pc.title == null ? "NULL" : GetPath(pc.title.gameObject))}");
            sb.AppendLine($"  hint: {(pc.hint == null ? "NULL" : GetPath(pc.hint.gameObject))}");
            sb.AppendLine($"  confirmBtn: {(pc.confirmBtn == null ? "NULL" : GetPath(pc.confirmBtn.gameObject))}");
            sb.AppendLine($"  denyBtn: {(pc.denyBtn == null ? "NULL" : GetPath(pc.denyBtn.gameObject))}");
            sb.AppendLine($"  okBtnText: {(pc.okBtnText == null ? "NULL" : GetPath(pc.okBtnText.gameObject))}");
            sb.AppendLine($"  denyBtnText: {(pc.denyBtnText == null ? "NULL" : GetPath(pc.denyBtnText.gameObject))}");
        }
        // FPSText
        var fps = Resources.FindObjectsOfTypeAll<FPSText>();
        sb.AppendLine();
        sb.AppendLine("FPSText instances: " + fps.Length);
        foreach (var f in fps)
        {
            sb.AppendLine($"  GO={GetPath(f.gameObject)} active={f.gameObject.activeInHierarchy} fpsText:{(f.fpsText == null ? "NULL" : GetPath(f.fpsText.gameObject))}");
        }
        // SGCLanguageSelect
        var sls = Resources.FindObjectsOfTypeAll<SGCLanguageSelect>();
        sb.AppendLine();
        sb.AppendLine("SGCLanguageSelect instances: " + sls.Length);
        foreach (var s in sls)
        {
            sb.AppendLine($"  GO={GetPath(s.gameObject)} active={s.gameObject.activeInHierarchy}");
            sb.AppendLine($"    _lanSelWnd: {(s._lanSelWnd == null ? "NULL" : "ok")}");
            sb.AppendLine($"    _gpLanBoard: {(s._gpLanBoard == null ? "NULL" : "ok")}");
            sb.AppendLine($"    _thaiBoardBtn: {(s._thaiBoardBtn == null ? "NULL" : "ok")}");
            sb.AppendLine($"    _lanPanelTrans: {(s._lanPanelTrans == null ? "NULL" : "ok")}");
            sb.AppendLine($"    _btnChange: {(s._btnChange == null ? "NULL" : "ok")}");
            sb.AppendLine($"    _lanTitleText: {(s._lanTitleText == null ? "NULL" : "ok")}");
            sb.AppendLine($"    _btnChangeText: {(s._btnChangeText == null ? "NULL" : "ok")}");
        }
        return sb.ToString();
    }

    private static string GetPath(GameObject g)
    {
        if (g == null) return "(null)";
        var sb = new StringBuilder(g.name);
        var t = g.transform.parent;
        while (t != null)
        {
            sb.Insert(0, t.name + "/");
            t = t.parent;
        }
        return sb.ToString();
    }
}
