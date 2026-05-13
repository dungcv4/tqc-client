// Source: Manual diag — check if ResMgr icon bundles are loaded.
using UnityEngine;

public static class CheckIconBundle
{
    public static void Execute()
    {
        var rm = ResMgr.Instance;
        if (rm == null) { Debug.LogWarning("[CheckIconBundle] ResMgr.Instance == null"); return; }
        Debug.Log($"[CheckIconBundle] ItemIconBundleOP    = {(rm.ItemIconBundleOP==null?"NULL":"OK")}");
        Debug.Log($"[CheckIconBundle] HeadIconBundleOP    = {(rm.HeadIconBundleOP==null?"NULL":"OK")}");
        Debug.Log($"[CheckIconBundle] SkillIconBundleOP   = {(rm.SkillIconBundleOP==null?"NULL":"OK")}");
        Debug.Log($"[CheckIconBundle] CardIconBundleOP    = {(rm.CardIconBundleOP==null?"NULL":"OK")}");
        Debug.Log($"[CheckIconBundle] EmojiBundleOP       = {(rm.EmojiBundleOP==null?"NULL":"OK")}");
    }
}
