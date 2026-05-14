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

            // 6b. DIAG: dump PLAYER hierarchy + renderers + sprite components so we can see
            //     WHY the sprite doesn't draw. Likely culprits: no Renderer, mesh has 0 verts,
            //     material null, sprite frame uvs all zero, scale zero, etc.
            sb.AppendLine("  PLAYER-100001 hierarchy + components:");
            DumpGoTree(sb, playerGo.transform, "    ", 0, 5);

            // 6c. DIAG: SM2 architecture — PackedSprite has no own renderer; rendering is on the
            //     SpriteManager that owns its slot. Find ALL SpriteManager objects in scene and log
            //     their mesh state + sprite-slot count + sample player part manager refs.
            sb.AppendLine("  All SpriteManager instances in scene:");
            var smType = System.Type.GetType("SpriteManager, Assembly-CSharp-firstpass");
            if (smType == null) smType = System.Type.GetType("SpriteManager");
            if (smType != null)
            {
                UnityEngine.Object[] allSm = UnityEngine.Object.FindObjectsOfType(smType);
                sb.Append("    Found ").Append(allSm.Length).AppendLine(" SpriteManager objects:");
                foreach (UnityEngine.Object o in allSm)
                {
                    var comp = o as Component;
                    if (comp == null) continue;
                    var go = comp.gameObject;
                    sb.Append("    - ").Append(GetPath(go.transform))
                      .Append(" active=").Append(go.activeInHierarchy)
                      .Append(" layer=").Append(go.layer);
                    var rd = go.GetComponent<Renderer>();
                    if (rd != null)
                    {
                        sb.Append(" RENDERER:").Append(rd.GetType().Name)
                          .Append(" enabled=").Append(rd.enabled);
                        if (rd.sharedMaterial != null) sb.Append(" mat=").Append(rd.sharedMaterial.name);
                        else sb.Append(" mat=NULL");
                    }
                    else sb.Append(" NO-RENDERER");
                    // SpriteManager uses SkinnedMeshRenderer.sharedMesh (NOT MeshFilter).
                    var smr = go.GetComponent<SkinnedMeshRenderer>();
                    if (smr != null && smr.sharedMesh != null)
                    {
                        sb.Append(" MESH:").Append(smr.sharedMesh.vertexCount).Append("v ")
                          .Append(smr.sharedMesh.subMeshCount).Append("sub");
                        try { var b = smr.localBounds; sb.Append(" bounds=").Append(b.size.ToString("F1")); } catch { }
                    }
                    else
                    {
                        var mf = go.GetComponent<MeshFilter>();
                        if (mf != null && mf.sharedMesh != null)
                            sb.Append(" MESH(MF):").Append(mf.sharedMesh.vertexCount).Append("v");
                        else
                            sb.Append(" NO-MESH");
                    }
                    // SpriteManager.sprites: 'protected SpriteMesh_Managed[] sprites' (NonPublic).
                    const System.Reflection.BindingFlags BF = System.Reflection.BindingFlags.Public
                        | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
                    var spritesField = smType.GetField("sprites", BF);
                    if (spritesField != null)
                    {
                        var spritesVal = spritesField.GetValue(comp);
                        var arr = spritesVal as System.Array;
                        if (arr != null) sb.Append(" sprites[").Append(arr.Length).Append("]");
                        else if (spritesVal == null) sb.Append(" sprites=NULL");
                        else sb.Append(" sprites=").Append(spritesVal.GetType().Name);
                    }
                    else sb.Append(" no-sprites-field");
                    // SpriteManager.initialized flag — Awake() sets this true after EnlargeArrays.
                    var initField = smType.GetField("initialized", BF);
                    if (initField != null)
                        sb.Append(" init=").Append(initField.GetValue(comp));
                    // SpriteManager.activeBlock — bitmask of used slots (via reflection).
                    var activeBlockField = smType.GetField("activeBlock", BF);
                    if (activeBlockField != null)
                    {
                        var ab = activeBlockField.GetValue(comp);
                        if (ab != null)
                            sb.Append(" activeBlock=").Append(ab.GetType().Name);
                    }
                    sb.AppendLine();

                    // DIAG: dump ALL bone slots + which have non-zero verts. Find the actually-used slot.
                    if (smr != null && smr.sharedMesh != null && smr.sharedMesh.vertexCount >= 4)
                    {
                        var verts = smr.sharedMesh.vertices;
                        var uvs = smr.sharedMesh.uv;
                        var bones = smr.bones;
                        sb.Append("        bones[").Append(bones != null ? bones.Length : 0)
                          .Append("] rootBone=").Append(smr.rootBone != null ? smr.rootBone.name : "<null>").AppendLine();
                        int slotsToScan = verts.Length / 4;
                        for (int s = 0; s < slotsToScan; s++)
                        {
                            int baseV = s * 4;
                            // Skip empty all-zero slots when summarizing — but record at least 1.
                            bool vertsZero = verts[baseV] == Vector3.zero && verts[baseV + 1] == Vector3.zero
                                          && verts[baseV + 2] == Vector3.zero && verts[baseV + 3] == Vector3.zero;
                            bool uvsZero = uvs != null && uvs.Length > baseV + 3
                                          && uvs[baseV] == Vector2.zero && uvs[baseV + 1] == Vector2.zero
                                          && uvs[baseV + 2] == Vector2.zero && uvs[baseV + 3] == Vector2.zero;
                            string bone = (bones != null && s < bones.Length && bones[s] != null)
                                ? (bones[s].name + "@" + bones[s].position.ToString("F1"))
                                : "<null>";
                            if (vertsZero && uvsZero)
                            {
                                sb.Append("        slot").Append(s).Append(": EMPTY  bone=").Append(bone).AppendLine();
                            }
                            else
                            {
                                sb.Append("        slot").Append(s).Append(": verts[0]=").Append(verts[baseV].ToString("F1"))
                                  .Append(" verts[2]=").Append(verts[baseV + 2].ToString("F1"));
                                if (uvs != null && uvs.Length > baseV + 3)
                                    sb.Append(" uv[0]=").Append(uvs[baseV].ToString("F3"))
                                      .Append(" uv[2]=").Append(uvs[baseV + 2].ToString("F3"));
                                sb.Append(" bone=").Append(bone).AppendLine();
                            }
                        }
                    }
                }
            }
            else sb.AppendLine("    SpriteManager type NOT FOUND via reflection");
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

        // 9. What's inside the stage scene root? Maybe stage11 has its own scene Camera that
        //    should be the active 3D renderer (and Main Camera in DontDestroyOnLoad is leftover).
        Scene stageScn = SceneManager.GetSceneByName(_watchedScene);
        if (stageScn.isLoaded)
        {
            sb.Append("Stage '").Append(_watchedScene).AppendLine("' root contents:");
            foreach (GameObject root in stageScn.GetRootGameObjects())
            {
                sb.Append("  - root='").Append(root.name).Append("' active=").Append(root.activeSelf).AppendLine();
                DumpTree(sb, root.transform, 1, 2);
                Camera[] sceneCams = root.GetComponentsInChildren<Camera>(true);
                if (sceneCams != null && sceneCams.Length > 0)
                {
                    sb.Append("    cameras inside stage root: ").Append(sceneCams.Length).AppendLine();
                    foreach (Camera c in sceneCams)
                    {
                        sb.Append("      - ").Append(c.name).Append(" tag='").Append(c.tag).Append("'")
                          .Append(" enabled=").Append(c.enabled).Append(" pos=").Append(c.transform.position.ToString("F1"))
                          .AppendLine();
                    }
                }
            }
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

    // Helper: build a "/Root/A/B/C" path string for a transform (limit to 6 parents).
    static string GetPath(Transform t)
    {
        var sb2 = new System.Text.StringBuilder(128);
        int cap = 6;
        for (Transform cur = t; cur != null && cap > 0; cur = cur.parent, cap--)
            sb2.Insert(0, "/" + cur.name);
        return sb2.ToString();
    }

    // For PLAYER sub-tree: log GameObject + renderers + sprite components so we see why nothing draws.
    static void DumpGoTree(StringBuilder sb, Transform t, string indent, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        sb.Append(indent).Append(t.name)
          .Append(" active=").Append(t.gameObject.activeSelf)
          .Append(" layer=").Append(t.gameObject.layer)
          .Append(" scale=").Append(t.localScale.ToString("F2"))
          .Append(" pos=").Append(t.position.ToString("F1"));
        // Renderer info — bounds, material, enabled
        var renderer = t.GetComponent<Renderer>();
        if (renderer != null)
        {
            sb.Append(" RENDERER:").Append(renderer.GetType().Name)
              .Append(" enabled=").Append(renderer.enabled)
              .Append(" sortingOrder=").Append(renderer.sortingOrder);
            try
            {
                var b = renderer.bounds;
                sb.Append(" bounds=").Append(b.center.ToString("F1")).Append("±").Append(b.size.ToString("F1"));
            }
            catch { }
            if (renderer.sharedMaterial != null)
                sb.Append(" mat=").Append(renderer.sharedMaterial.name);
            else
                sb.Append(" mat=NULL");
        }
        // MeshFilter
        var mf = t.GetComponent<MeshFilter>();
        if (mf != null && mf.sharedMesh != null)
        {
            sb.Append(" MESH:").Append(mf.sharedMesh.vertexCount).Append("v");
        }
        else if (mf != null)
        {
            sb.Append(" MESH:NULL");
        }
        // SpriteBase / PackedSprite / TextureAnim — anything in SpriteManager 2 stack
        var sb_comp = t.GetComponent("SpriteBase");
        if (sb_comp != null) sb.Append(" [has SpriteBase]");
        var ps_comp = t.GetComponent("PackedSprite");
        if (ps_comp != null) sb.Append(" [has PackedSprite]");
        var sm_comp = t.GetComponent("SpriteManager");
        if (sm_comp != null) sb.Append(" [has SpriteManager]");
        var au_comp = t.GetComponent("AutoSpriteBase");
        if (au_comp != null) sb.Append(" [has AutoSpriteBase]");
        sb.AppendLine();

        // Recurse
        for (int i = 0; i < t.childCount && i < 30; i++)
            DumpGoTree(sb, t.GetChild(i), indent + "  ", depth + 1, maxDepth);
        if (t.childCount > 30) sb.Append(indent).Append("  ").Append("… (+").Append(t.childCount - 30).AppendLine(" more)");
    }
}
