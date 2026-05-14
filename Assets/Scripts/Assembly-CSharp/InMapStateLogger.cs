// HAND-ADDED DIAGNOSTIC — not from Ghidra / dump.cs.
// Purpose: trace in-map state ONCE after the stage scene finishes loading so we can see
// what's rendering vs what's missing. Toggled via INMAP_LOG_ENABLED below.
//
// Snapshot fires the first time SceneManager reports an additively-loaded scene whose name
// starts with "stage" AND 60 game-frames have elapsed since (giving the lua-side
// OnMapLoadFinished cascade — TryCreatePlayerEntity / SpawnNodes / CreateClientOnlyEntityOrFX
// — time to populate its children).
//
// Removable in one piece: this file + the one-line `InMapStateLogger.TickFromMainUpdate()`
// call in Main.Update.

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InMapStateLogger
{
    public const bool INMAP_LOG_ENABLED = true;

    private static bool _snapshotDone = false;
    private static int  _frameCounterSinceLoad = -1;
    private static string _watchedScene = null;
    private const int FRAMES_AFTER_LOAD = 60;

    // Called from Main.Update once per frame.
    public static void TickFromMainUpdate()
    {
        if (!INMAP_LOG_ENABLED) return;
        if (_snapshotDone) return;

        // Look for any stage* scene that's loaded + active.
        if (_watchedScene == null)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene s = SceneManager.GetSceneAt(i);
                if (s.isLoaded && s.name != null && s.name.StartsWith("stage"))
                {
                    _watchedScene = s.name;
                    _frameCounterSinceLoad = 0;
                    UJDebug.Log("[InMapStateLogger] watching scene=" + s.name + " — snapshot in " + FRAMES_AFTER_LOAD + " frames");
                    break;
                }
            }
            return;
        }

        _frameCounterSinceLoad++;
        if (_frameCounterSinceLoad < FRAMES_AFTER_LOAD) return;

        _snapshotDone = true;
        DumpState();
    }

    static void DumpState()
    {
        var sb = new StringBuilder(2048);
        sb.AppendLine("==== [InMapStateLogger] In-Map Snapshot ====");

        // 1. Cameras
        Camera[] cams = Camera.allCameras;
        sb.Append("Cameras (").Append(cams.Length).AppendLine(" active):");
        foreach (Camera c in cams)
        {
            sb.Append("  - ").Append(c.name)
              .Append(" enabled=").Append(c.enabled)
              .Append(" depth=").Append(c.depth)
              .Append(" clearFlags=").Append(c.clearFlags)
              .Append(" cullingMask=0x").Append(c.cullingMask.ToString("X"))
              .Append(" pos=").Append(c.transform.position.ToString("F1"))
              .Append(" rot=").Append(c.transform.eulerAngles.ToString("F0"))
              .AppendLine();
        }

        // 2. Active scenes
        sb.Append("Loaded scenes (").Append(SceneManager.sceneCount).AppendLine("):");
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            sb.Append("  - ").Append(s.name).Append(" isLoaded=").Append(s.isLoaded)
              .Append(" rootCount=").Append(s.GetRootGameObjects().Length).AppendLine();
        }

        // 3. Hierarchy snapshot of likely-in-map containers.
        DumpContainer(sb, "MapEntities");
        DumpContainer(sb, "UIEntities");

        // 4. Active WndForms by name.
        GameObject guiRoot = GameObject.Find("GUI_Root");
        if (guiRoot != null)
        {
            sb.AppendLine("GUI_Root tree (depth ≤ 3):");
            DumpTree(sb, guiRoot.transform, 0, 3);
        }
        else
        {
            sb.AppendLine("GUI_Root: NOT FOUND");
        }

        // 5. Renderers actually inside the stage scene.
        Scene stage = SceneManager.GetSceneByName(_watchedScene);
        if (stage.isLoaded)
        {
            int totalRenderers = 0;
            int activeRenderers = 0;
            foreach (GameObject root in stage.GetRootGameObjects())
            {
                Renderer[] rs = root.GetComponentsInChildren<Renderer>(true);
                totalRenderers += rs.Length;
                foreach (Renderer r in rs) if (r.enabled && r.gameObject.activeInHierarchy) activeRenderers++;
            }
            sb.Append("Stage '").Append(_watchedScene).Append("' renderers: ")
              .Append(activeRenderers).Append("/").Append(totalRenderers).AppendLine(" active/total");
        }

        sb.AppendLine("==== [InMapStateLogger] END ====");
        UJDebug.Log(sb.ToString());
    }

    static void DumpContainer(StringBuilder sb, string name)
    {
        GameObject go = GameObject.Find(name);
        if (go == null) { sb.Append(name).AppendLine(": NOT FOUND"); return; }
        Transform t = go.transform;
        sb.Append(name).Append(": active=").Append(go.activeSelf)
          .Append(" childCount=").Append(t.childCount).AppendLine();
        for (int i = 0; i < t.childCount && i < 30; i++)
        {
            Transform c = t.GetChild(i);
            sb.Append("    [").Append(i).Append("] ").Append(c.name)
              .Append(" active=").Append(c.gameObject.activeSelf)
              .Append(" pos=").Append(c.position.ToString("F1")).AppendLine();
        }
        if (t.childCount > 30) sb.Append("    … (+").Append(t.childCount - 30).AppendLine(" more)");
    }

    static void DumpTree(StringBuilder sb, Transform t, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        for (int i = 0; i < depth; i++) sb.Append("  ");
        sb.Append("- ").Append(t.name)
          .Append(" active=").Append(t.gameObject.activeSelf);
        CanvasGroup cg = t.GetComponent<CanvasGroup>();
        if (cg != null) sb.Append(" cg.alpha=").Append(cg.alpha);
        Canvas canvas = t.GetComponent<Canvas>();
        if (canvas != null) sb.Append(" canvas.enabled=").Append(canvas.enabled);
        sb.AppendLine();
        for (int i = 0; i < t.childCount && i < 30; i++)
            DumpTree(sb, t.GetChild(i), depth + 1, maxDepth);
    }
}
