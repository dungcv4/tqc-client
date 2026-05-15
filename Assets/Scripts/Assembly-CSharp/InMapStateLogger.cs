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
        DumpAllCanvases();
    }

    // [CANVAS-LOG] Dump every Canvas in scene with mode, camera, parent, enabled.
    static void DumpAllCanvases()
    {
        var sb = new StringBuilder(1024);
        sb.AppendLine("==== [CanvasDump] All Canvas components ====");
        Canvas[] all = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (var c in all)
        {
            if (c == null) continue;
            if (c.gameObject.scene.name == null) continue;  // skip prefab assets
            string path = c.gameObject.name;
            Transform pt = c.transform.parent;
            while (pt != null) { path = pt.name + "/" + path; pt = pt.parent; }
            sb.Append("  ").Append(path)
              .Append("  scene=").Append(c.gameObject.scene.name)
              .Append(" enabled=").Append(c.enabled)
              .Append(" mode=").Append(c.renderMode)
              .Append(" camera=").Append(c.worldCamera != null ? c.worldCamera.name : "null")
              .Append(" sortOrder=").Append(c.sortingOrder)
              .Append(" overrideSorting=").Append(c.overrideSorting)
              .AppendLine();
        }
        sb.AppendLine("==== END CanvasDump ====");
        UnityEngine.Debug.Log(sb.ToString());
    }

    // [INPUT-LOG] Called every frame from Main.Update AFTER snapshot done.
    // Logs at most once per 30 frames + only on state CHANGE to keep log clean.
    private static int  _inputLogCooldown = 0;
    private static bool _wasDown = false;
    private static int  _lastTouchCount = -1;
    public static void TickInputProbe()
    {
        if (!INMAP_LOG_ENABLED) return;
        if (!_snapshotDone) return;

        _inputLogCooldown++;

        bool downNow   = UnityEngine.Input.GetMouseButton(0);
        bool downStart = UnityEngine.Input.GetMouseButtonDown(0);
        bool downEnd   = UnityEngine.Input.GetMouseButtonUp(0);
        int  touchCnt  = UnityEngine.Input.touchCount;
        Vector3 mp     = UnityEngine.Input.mousePosition;

        // Only emit when state changes OR every 60 frames if button held.
        bool stateChanged = (downNow != _wasDown) || (touchCnt != _lastTouchCount) || downStart || downEnd;
        bool periodicWhileDown = downNow && _inputLogCooldown >= 60;

        if (stateChanged || periodicWhileDown)
        {
            bool overUI = false;
            string overUIHits = "";
            try { overUI = BaseProcLua.PointIsOverUI(new Vector2(mp.x, mp.y)); } catch { }
            // Diagnostic: when overUI=true, dump WHICH GameObjects are blocking the raycast (top 5).
            if (overUI && downStart)
            {
                try
                {
                    var es = UnityEngine.EventSystems.EventSystem.current;
                    if (es != null)
                    {
                        var ped = new UnityEngine.EventSystems.PointerEventData(es) { position = new Vector2(mp.x, mp.y) };
                        var hits = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
                        es.RaycastAll(ped, hits);
                        var sb = new System.Text.StringBuilder(" overUI_hits=[");
                        int n = System.Math.Min(hits.Count, 5);
                        for (int i = 0; i < n; i++)
                        {
                            if (i > 0) sb.Append(", ");
                            var go = hits[i].gameObject;
                            string path = go.name;
                            Transform t = go.transform.parent;
                            int depth = 0;
                            while (t != null && depth < 4) { path = t.name + "/" + path; t = t.parent; depth++; }
                            sb.Append(path);
                        }
                        sb.Append("]");
                        overUIHits = sb.ToString();
                    }
                }
                catch { }
            }

            // Probe WndForm_Joystick state via GameObject.Find.
            GameObject joyParent = GameObject.Find("WndForm_Joystick(Clone)");
            string joyState = "missing";
            if (joyParent != null)
            {
                Transform jb = joyParent.transform.Find("Joystick_bak");
                if (jb != null)
                {
                    var rt = jb as RectTransform;
                    if (rt == null) rt = jb.GetComponent<RectTransform>();
                    string posInfo = "";
                    if (rt != null)
                    {
                        posInfo = " anch=" + rt.anchoredPosition.ToString("F0")
                              + " size=" + rt.sizeDelta.ToString("F0")
                              + " scale=" + rt.localScale.ToString("F2");
                    }
                    var cg = jb.GetComponent<CanvasGroup>();
                    string cgInfo = (cg != null) ? (" cg.alpha=" + cg.alpha) : "";
                    // Probe first Image child for alpha
                    var img = jb.GetComponentInChildren<UnityEngine.UI.Image>(true);
                    string imgInfo = "";
                    if (img != null)
                    {
                        var c = img.color;
                        string spriteName = (img.sprite != null) ? img.sprite.name : "NULL";
                        string matName = (img.material != null) ? img.material.name : "NULL";
                        imgInfo = " 1stImg=" + img.gameObject.name + " sprite=" + spriteName + " mat=" + matName + " color=(" + c.r.ToString("F1") + "," + c.g.ToString("F1") + "," + c.b.ToString("F1") + ",α=" + c.a.ToString("F2") + ") enabled=" + img.enabled + " geomActive=" + img.canvasRenderer.cull;
                    }
                    // WORLD-SPACE position (in case Canvas is WorldSpace + UICamera frustum mismatch).
                    string worldInfo = "";
                    if (rt != null)
                    {
                        Vector3 wp = rt.position;
                        worldInfo = " WORLDpos=" + wp.ToString("F1");
                        // Find UICamera and test visibility.
                        Camera uiCam = null;
                        foreach (Camera c in Camera.allCameras)
                        {
                            if (c.gameObject.name == "UICamera") { uiCam = c; break; }
                        }
                        if (uiCam != null)
                        {
                            Vector3 vp = uiCam.WorldToViewportPoint(wp);
                            worldInfo += " UICam.viewport=(" + vp.x.ToString("F2") + "," + vp.y.ToString("F2") + "," + vp.z.ToString("F1") + ")"
                                       + " inFrustum=" + (vp.x>=0 && vp.x<=1 && vp.y>=0 && vp.y<=1 && vp.z>0);
                        }
                    }
                    // Parent Canvas info
                    Canvas pCanvas = jb.GetComponentInParent<Canvas>();
                    string canvasInfo = "";
                    if (pCanvas != null)
                    {
                        canvasInfo = " canvas.mode=" + pCanvas.renderMode + " canvas.camera=" + (pCanvas.worldCamera != null ? pCanvas.worldCamera.name : "null") + " canvas.sortOrder=" + pCanvas.sortingOrder;
                    }
                    joyState = "Joystick_bak.active=" + jb.gameObject.activeSelf + posInfo + cgInfo + imgInfo + worldInfo + canvasInfo;
                }
                else            joyState = "Joystick_bak child not found";
            }

            UnityEngine.Debug.Log("[INPUT-LOG] frame=" + Time.frameCount
                + " mouse.down=" + downNow + (downStart ? " (DOWN)" : "") + (downEnd ? " (UP)" : "")
                + " mp=(" + mp.x.ToString("F0") + "," + mp.y.ToString("F0") + ")"
                + " touchCount=" + touchCnt
                + " PointIsOverUI=" + overUI
                + overUIHits
                + " joy=" + joyState);

            _inputLogCooldown = 0;
            _wasDown = downNow;
            _lastTouchCount = touchCnt;
        }
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
            // Proper frustum test (not just layer mask) — checks if any sub-renderer bounds
            // intersects the camera frustum. This is the ACTUAL "is it on screen?" test.
            foreach (Camera c in cams)
            {
                bool layerOK = (c.cullingMask & (1 << playerLayer)) != 0;
                bool frustumHit = false;
                if (layerOK)
                {
                    var planes = GeometryUtility.CalculateFrustumPlanes(c);
                    var renderers = playerGo.GetComponentsInChildren<Renderer>(true);
                    foreach (var r in renderers)
                    {
                        if (r == null || !r.enabled) continue;
                        if (GeometryUtility.TestPlanesAABB(planes, r.bounds))
                        {
                            frustumHit = true;
                            break;
                        }
                    }
                    // Player sprites have no direct Renderer (SM2 stack renders separately);
                    // also test camera.WorldToViewportPoint of player position.
                    Vector3 vp = c.WorldToViewportPoint(playerGo.transform.position);
                    bool vpInRange = vp.z > 0 && vp.x >= 0 && vp.x <= 1 && vp.y >= 0 && vp.y <= 1;
                    sb.Append("    visible to ").Append(c.name)
                      .Append("? layerOK=").Append(layerOK)
                      .Append(" frustumHit=").Append(frustumHit)
                      .Append(" viewport=(").Append(vp.x.ToString("F2")).Append(",")
                      .Append(vp.y.ToString("F2")).Append(",").Append(vp.z.ToString("F1")).Append(")")
                      .Append(" inRange=").Append(vpInRange).AppendLine();
                }
                else sb.Append("    visible to ").Append(c.name).AppendLine("? layer MASKED OUT");
            }

            // 6b. DIAG: dump PLAYER hierarchy + renderers + sprite components so we can see
            //     WHY the sprite doesn't draw. Likely culprits: no Renderer, mesh has 0 verts,
            //     material null, sprite frame uvs all zero, scale zero, etc.
            sb.AppendLine("  PLAYER-100001 hierarchy + components:");
            DumpGoTree(sb, playerGo.transform, "    ", 0, 5);

            // 6d. DIAG: for each child PackedSprite, dump SpriteRoot field values:
            //     width, height, anchor, offset, topLeft, bottomRight, uvRect, scaleFactor,
            //     topLeftOffset, bottomRightOffset, plane.
            //     PLUS: PackedSprite._ser_stat_frame_info (the serialized prefab CSpriteFrame).
            sb.AppendLine("  PackedSprite SpriteRoot fields:");
            var allPackedSprites = playerGo.GetComponentsInChildren<Component>(true);
            var srType = System.Type.GetType("SpriteRoot, Assembly-CSharp-firstpass");
            var psType = System.Type.GetType("PackedSprite, Assembly-CSharp-firstpass");
            var csFrameType = System.Type.GetType("CSpriteFrame, Assembly-CSharp-firstpass");
            const System.Reflection.BindingFlags BFR = System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
            foreach (var c in allPackedSprites)
            {
                if (c == null) continue;
                if (srType == null || !srType.IsInstanceOfType(c)) continue;
                sb.Append("    [").Append(c.name).Append("] ");
                DumpField(sb, srType, c, "width", BFR);
                DumpField(sb, srType, c, "height", BFR);
                DumpField(sb, srType, c, "anchor", BFR);
                DumpField(sb, srType, c, "plane", BFR);
                DumpField(sb, srType, c, "offset", BFR);
                sb.AppendLine();
                sb.Append("        ");
                DumpField(sb, srType, c, "topLeft", BFR);
                DumpField(sb, srType, c, "bottomRight", BFR);
                DumpField(sb, srType, c, "uvRect", BFR);
                sb.AppendLine();
                sb.Append("        ");
                DumpField(sb, srType, c, "scaleFactor", BFR);
                DumpField(sb, srType, c, "topLeftOffset", BFR);
                DumpField(sb, srType, c, "bottomRightOffset", BFR);
                sb.AppendLine();

                // DIAG: also dump animations[] and textureAnimations[] lengths + curAnim
                // (verify AutoSpriteBase.Awake loop built UVAnimation[] from textureAnimations).
                if (psType != null && psType.IsInstanceOfType(c))
                {
                    var anField = psType.GetField("animations", BFR);
                    var taField = psType.GetField("textureAnimations", BFR);
                    var curField = psType.GetField("curAnim", BFR);
                    var defField = psType.GetField("defaultAnim", BFR);
                    sb.Append("        anims: ");
                    if (anField != null)
                    {
                        var arr = anField.GetValue(c) as System.Array;
                        sb.Append("animations[").Append(arr != null ? arr.Length : -1).Append("] ");
                    }
                    if (taField != null)
                    {
                        var arr = taField.GetValue(c) as System.Array;
                        sb.Append("textureAnimations[").Append(arr != null ? arr.Length : -1).Append("] ");
                    }
                    if (defField != null)
                        sb.Append("defaultAnim=").Append(defField.GetValue(c)).Append(" ");
                    if (curField != null)
                    {
                        var cur = curField.GetValue(c);
                        sb.Append("curAnim=").Append(cur == null ? "<null>" : cur.ToString());
                    }
                    sb.AppendLine();
                }
                if (psType != null && psType.IsInstanceOfType(c))
                {
                    var serField = psType.GetField("_ser_stat_frame_info", BFR);
                    if (serField != null)
                    {
                        var serVal = serField.GetValue(c);
                        if (serVal == null)
                        {
                            sb.AppendLine("        _ser_stat_frame_info=NULL");
                        }
                        else if (csFrameType != null)
                        {
                            sb.Append("        _ser_stat_frame_info: ");
                            DumpField(sb, csFrameType, serVal, "uvs", BFR);
                            DumpField(sb, csFrameType, serVal, "scaleFactor", BFR);
                            DumpField(sb, csFrameType, serVal, "topLeftOffset", BFR);
                            DumpField(sb, csFrameType, serVal, "bottomRightOffset", BFR);
                            sb.AppendLine();
                        }
                    }
                    else sb.AppendLine("        _ser_stat_frame_info=<field missing>");

                    // Also dump PackedSprite.staticFrameInfo (the SPRITE_FRAME struct copy from Awake).
                    var staticField = psType.GetField("staticFrameInfo", BFR);
                    if (staticField != null)
                    {
                        var staticVal = staticField.GetValue(c);
                        if (staticVal == null) sb.AppendLine("        staticFrameInfo=NULL");
                        else
                        {
                            var stType = staticVal.GetType();
                            sb.Append("        staticFrameInfo: ");
                            DumpField(sb, stType, staticVal, "uvs", BFR);
                            DumpField(sb, stType, staticVal, "scaleFactor", BFR);
                            DumpField(sb, stType, staticVal, "topLeftOffset", BFR);
                            DumpField(sb, stType, staticVal, "bottomRightOffset", BFR);
                            sb.AppendLine();
                        }
                    }
                }

                // DIAG: dump SpriteRoot.frameInfo (current frame state — should match staticFrameInfo
                // initially, then gets updated by animation pump). If frameInfo.scaleFactor = (1,1)
                // but staticFrameInfo.scaleFactor = (0.5,0.5), then frameInfo was never seeded from
                // staticFrameInfo → that's the missing copy in Awake/Init.
                var fiField = srType.GetField("frameInfo", BFR);
                if (fiField != null)
                {
                    var fiVal = fiField.GetValue(c);
                    if (fiVal == null) sb.AppendLine("        frameInfo=NULL");
                    else
                    {
                        var fiType = fiVal.GetType();
                        sb.Append("        frameInfo (SpriteRoot): ");
                        DumpField(sb, fiType, fiVal, "uvs", BFR);
                        DumpField(sb, fiType, fiVal, "scaleFactor", BFR);
                        DumpField(sb, fiType, fiVal, "topLeftOffset", BFR);
                        DumpField(sb, fiType, fiVal, "bottomRightOffset", BFR);
                        sb.AppendLine();
                    }
                }
            }

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
                                sb.Append("        slot").Append(s).Append(": bone=").Append(bone).AppendLine();
                                sb.Append("          verts: v0=").Append(verts[baseV].ToString("F1"))
                                  .Append(" v1=").Append(verts[baseV + 1].ToString("F1"))
                                  .Append(" v2=").Append(verts[baseV + 2].ToString("F1"))
                                  .Append(" v3=").Append(verts[baseV + 3].ToString("F1")).AppendLine();
                                if (uvs != null && uvs.Length > baseV + 3)
                                    sb.Append("          uvs:   u0=").Append(uvs[baseV].ToString("F3"))
                                      .Append(" u1=").Append(uvs[baseV + 1].ToString("F3"))
                                      .Append(" u2=").Append(uvs[baseV + 2].ToString("F3"))
                                      .Append(" u3=").Append(uvs[baseV + 3].ToString("F3")).AppendLine();
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

    // Helper: reflection-read a field/property by name + append "key=value " to sb.
    static void DumpField(StringBuilder sb, System.Type t, object obj, string name, System.Reflection.BindingFlags bf)
    {
        var f = t.GetField(name, bf);
        if (f != null)
        {
            var v = f.GetValue(obj);
            sb.Append(name).Append("=");
            if (v is Vector3 v3) sb.Append(v3.ToString("F1"));
            else if (v is Vector2 v2) sb.Append(v2.ToString("F2"));
            else if (v is Rect r) sb.Append("(x:").Append(r.x.ToString("F2")).Append(" y:").Append(r.y.ToString("F2"))
                                   .Append(" w:").Append(r.width.ToString("F2")).Append(" h:").Append(r.height.ToString("F2")).Append(")");
            else sb.Append(v == null ? "null" : v.ToString());
            sb.Append(" ");
            return;
        }
        var p = t.GetProperty(name, bf);
        if (p != null)
        {
            try
            {
                var v = p.GetValue(obj);
                sb.Append(name).Append("=").Append(v == null ? "null" : v.ToString()).Append(" ");
            }
            catch { sb.Append(name).Append("=<getter-throw> "); }
            return;
        }
        sb.Append(name).Append("=<missing> ");
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
