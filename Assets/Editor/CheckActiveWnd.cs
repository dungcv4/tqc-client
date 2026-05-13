// Source: Manual diag — lists active GameObjects.
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CheckActiveWnd
{
    public static void Execute()
    {
        for (int s = 0; s < SceneManager.sceneCount; s++)
        {
            var scn = SceneManager.GetSceneAt(s);
            Debug.Log($"[CheckActiveWnd] scene='{scn.name}' loaded={scn.isLoaded} rootCount={scn.rootCount}");
            foreach (var go in scn.GetRootGameObjects())
            {
                Debug.Log($"  ROOT '{go.name}' active={go.activeInHierarchy}");
            }
        }
        // Probe wndform paths
        string[] probes = {
            "GUI_Root", "GUI_Root/WndFormProxy", "GUI_Root/WndFormProxy/WndForm_LoginGame(Clone)",
            "GUI_Root/WndFormProxy/WndForm_LoginCreateChar(Clone)",
            "GUI_Root/WndFormProxy/WndForm_MsgWindow(Clone)",
            "GUI_Root/WndFormProxy/WndForm_Connecting(Clone)"
        };
        foreach (var p in probes)
        {
            var go = GameObject.Find(p);
            Debug.Log($"[CheckActiveWnd] '{p}' = {(go != null ? (go.activeInHierarchy ? "ACTIVE" : "INACTIVE") : "NULL")}");
        }
    }
}
