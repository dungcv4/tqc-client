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
            // Trace parent chain so we can see which root-scene + parent path each camera belongs to.
            string parentPath = c.name;
            Transform pt = c.transform.parent;
            while (pt != null) { parentPath = pt.name + "/" + parentPath; pt = pt.parent; }
            sb.Append("  - ").Append(parentPath)
              .Append(" tag='").Append(c.tag).Append("'")
              .Append(" hasPlayerCamControl=").Append(c.GetComponent<PlayerCamControl>() != null)
              .Append(" scene='").Append(c.gameObject.scene.name).Append("'")
              .Append(" enabled=").Append(c.enabled)
              .Append(" depth=").Append(c.depth)
              .Append(" clearFlags=").Append(c.clearFlags)
              .Append(" cullingMask=0x").Append(c.cullingMask.ToString("X"))
              .Append(" pos=").Append(c.transform.position.ToString("F1"))
              .Append(" rot=").Append(c.transform.eulerAngles.ToString("F0"))
              .Append(" fov=").Append(c.fieldOfView.ToString("F0"))
              .Append(" ortho=").Append(c.orthographic).Append(c.orthographic ? ":" + c.orthographicSize : "")
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

        // 5. Renderers actually inside the stage scene — group by layer to see what culls them.
        Scene stage = SceneManager.GetSceneByName(_watchedScene);
        if (stage.isLoaded)
        {
            int totalRenderers = 0;
            int activeRenderers = 0;
            Dictionary<int, int> rendererCountByLayer = new Dictionary<int, int>();
            foreach (GameObject root in stage.GetRootGameObjects())
            {
                Renderer[] rs = root.GetComponentsInChildren<Renderer>(true);
                totalRenderers += rs.Length;
                foreach (Renderer r in rs)
                {
                    if (r.enabled && r.gameObject.activeInHierarchy)
                    {
                        activeRenderers++;
                        int lyr = r.gameObject.layer;
                        rendererCountByLayer.TryGetValue(lyr, out int cnt);
                        rendererCountByLayer[lyr] = cnt + 1;
                    }
                }
            }
            sb.Append("Stage '").Append(_watchedScene).Append("' renderers: ")
              .Append(activeRenderers).Append("/").Append(totalRenderers).AppendLine(" active/total");
            sb.Append("  active renderers by layer: ");
            foreach (var kv in rendererCountByLayer)
                sb.Append(LayerMask.LayerToName(kv.Key)).Append("(").Append(kv.Key).Append(")=").Append(kv.Value).Append(" ");
            sb.AppendLine();
        }

        // 6. Player entity layer + which cameras can see it.
        GameObject playerGo = GameObject.Find("PLAYER-100001");
        if (playerGo != null)
        {
            int playerLayer = playerGo.layer;
            sb.Append("PLAYER-100001 layer=").Append(playerLayer).Append(" (")
              .Append(LayerMask.LayerToName(playerLayer)).AppendLine(")");
            foreach (Camera c in cams)
            {
                bool sees = (c.cullingMask & (1 << playerLayer)) != 0;
                sb.Append("    visible to ").Append(c.name).Append("? ").Append(sees).AppendLine();
            }
        }
        else
        {
            sb.AppendLine("PLAYER-100001 NOT FOUND");
        }

        // 7. Cameras' tracked targets via PlayerCamControl reflection (single shot, defensive).
        var pcc = PlayerCamControl.Instance;
        if (pcc != null)
        {
            sb.Append("PlayerCamControl.Instance found, cameraTarget field via reflection: ");
            var f = typeof(PlayerCamControl).GetField("cameraTarget", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (f != null)
            {
                var tgt = f.GetValue(pcc) as Component;
                sb.AppendLine(tgt == null ? "null" : (tgt.name + " @ " + tgt.transform.position.ToString("F1")));
            }
            else sb.AppendLine("field missing");
            sb.Append("  CAM_HEIGHT=").Append(PlayerCamControl.CAM_HEIGHT)
              .Append("  CAMZ_DIS=").Append(pcc.CAMZ_DIS).AppendLine();
        }
        else sb.AppendLine("PlayerCamControl.Instance NULL");

        // 8. What does FindGameObjectWithTag("MainCamera") return? (Production SetupScreenSize
        //    finds it and AttachComponent<PlayerCamControl> to it. If the wrong GO has this tag,
        //    PlayerCamControl gets attached to the wrong camera GO and the move never happens
        //    on the actual rendering camera.) Also dump Main._MainCamera if available.
        GameObject taggedMain = GameObject.FindGameObjectWithTag("MainCamera");
        if (taggedMain != null)
        {
            string p = taggedMain.name;
            Transform pt2 = taggedMain.transform.parent;
            while (pt2 != null) { p = pt2.name + "/" + p; pt2 = pt2.parent; }
            sb.Append("FindGameObjectWithTag(\"MainCamera\") → ").Append(p)
              .Append("  pos=").Append(taggedMain.transform.position.ToString("F1"))
              .AppendLine();
        }
        else sb.AppendLine("FindGameObjectWithTag(\"MainCamera\") → null");

        if (Main.Instance != null && Main.Instance.mainCamera != null)
        {
            var mc = Main.Instance.mainCamera;
            sb.Append("Main.Instance.mainCamera → ").Append(mc.name)
              .Append(" tag='").Append(mc.tag).Append("'")
              .Append(" pos=").Append(mc.transform.position.ToString("F1"))
              .AppendLine();
        }
        else sb.AppendLine("Main.Instance.mainCamera NULL or Main.Instance NULL");

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
