// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'SpriteManager'
// To port a method: replace `throw new System.NotImplementedException();`
// with body translated from the listed Ghidra .c file.
// Move ported file to unity_project/Assets/Scripts/Ported/<area>/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-stub  TypeDefIndex: 8170
[RequireComponent(typeof(SkinnedMeshRenderer))]
[ExecuteInEditMode]
public class SpriteManager : MonoBehaviour
{
    public SpriteRoot.WINDING_ORDER winding;
    public int allocBlockSize;
    public bool autoUpdateBounds;
    public bool drawBoundingBox;
    public bool persistent;
    protected bool initialized;
    protected EZLinkedList<SpriteMesh_Managed> availableBlocks;
    protected bool vertsChanged;
    protected bool uvsChanged;
    protected bool colorsChanged;
    protected bool vertCountChanged;
    protected bool updateBounds;
    protected SpriteMesh_Managed[] sprites;
    protected EZLinkedList<SpriteMesh_Managed> activeBlocks;
    protected List<SpriteMesh_Managed> spriteDrawOrder;
    protected SpriteDrawLayerComparer drawOrderComparer;
    protected float boundUpdateInterval;
    protected List<SpriteRoot> spriteAddQueue;
    protected SkinnedMeshRenderer meshRenderer;
    protected Mesh mesh;
    protected Texture texture;
    protected Transform[] bones;
    protected BoneWeight[] boneWeights;
    protected Matrix4x4[] bindPoses;
    protected Vector3[] vertices;
    protected int[] triIndices;
    protected Vector2[] UVs;
    protected Vector2[] UVs2;
    protected Color[] colors;
    protected SpriteMesh_Managed tempSprite;

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/PixelSpaceToUVSpace_Vector2.c RVA 0x1573908
    // 1-1: if (texture == null) return Vector2.zero;
    //      return new Vector2(xy.x / texture.width, xy.y / texture.height);
    // Ghidra dispatches Texture.width / .height through the abstract Texture base vtable at
    // offsets +0x188 / +0x1a8 (klass.vtable). The C# equivalent uses the public properties.
    public Vector2 PixelSpaceToUVSpace(Vector2 xy)
    {
        if ((UnityEngine.Object)texture == null) return Vector2.zero;
        return new Vector2(xy.x / (float)texture.width, xy.y / (float)texture.height);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/PixelSpaceToUVSpace_int.c RVA 0x15739FC
    // 1-1 wrapper: PixelSpaceToUVSpace((float)x, (float)y) via the Vector2 overload.
    public Vector2 PixelSpaceToUVSpace(int x, int y)
    {
        return PixelSpaceToUVSpace(new Vector2((float)x, (float)y));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/PixelCoordToUVCoord_Vector2.c RVA 0x1573A08
    // 1-1: identical to PixelSpaceToUVSpace except the divisor is `(width - 1)` / `(height - 1)`
    // (pixel-center sampling vs pixel-edge sampling).
    public Vector2 PixelCoordToUVCoord(Vector2 xy)
    {
        if ((UnityEngine.Object)texture == null) return Vector2.zero;
        return new Vector2(xy.x / ((float)texture.width - 1f), xy.y / ((float)texture.height - 1f));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/PixelCoordToUVCoord_int.c RVA 0x1573B10
    // 1-1 wrapper.
    public Vector2 PixelCoordToUVCoord(int x, int y)
    {
        return PixelCoordToUVCoord(new Vector2((float)x, (float)y));
    }

    // RVA: 0x1573B1C  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/SetupBoneWeights.c
    protected void SetupBoneWeights(SpriteMesh_Managed s) { throw new System.NotImplementedException(); }

    // RVA: 0x1573CA0  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/Awake.c
    private void Awake() { throw new System.NotImplementedException(); }

    // RVA: 0x1574C48  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/InitArrays.c
    protected void InitArrays() { throw new System.NotImplementedException(); }

    // RVA: 0x1574078  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/EnlargeArrays.c
    protected int EnlargeArrays(int count) { throw new System.NotImplementedException(); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/AlreadyAdded.c RVA 0x1574F00
    // 1-1:
    //   if (activeBlocks == null) throw NRE;
    //   if (!activeBlocks.Rewind()) return false;
    //   do {
    //       SpriteMesh_Managed cur = activeBlocks.Current;
    //       SpriteRoot itsSprite = cur.sprite;                  // virtual property @vtable+0x2a8
    //       if (itsSprite == sprite) return true;               // UnityEngine.Object.op_Equality
    //   } while (activeBlocks.MoveNext());
    //   return false;
    public bool AlreadyAdded(SpriteRoot sprite)
    {
        if (activeBlocks == null) throw new System.NullReferenceException();
        if (!activeBlocks.Rewind()) return false;
        do
        {
            SpriteMesh_Managed cur = activeBlocks.get_Current();
            if (cur != null)
            {
                SpriteRoot s = cur.sprite;
                if ((UnityEngine.Object)s == (UnityEngine.Object)sprite) return true;
            }
        } while (activeBlocks.MoveNext());
        return false;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/AddSprite_GameObject.c RVA 0x1575008
    // 1-1 wrapper:
    //   if (go == null) throw NRE;
    //   SpriteRoot sr = go.GetComponent<SpriteRoot>();
    //   if (sr == null) return null;                            // Unity op_Equality null-check
    //   return AddSprite(sr);                                   // delegate to SpriteRoot overload
    public SpriteMesh_Managed AddSprite(GameObject go)
    {
        if ((UnityEngine.Object)go == null) throw new System.NullReferenceException();
        SpriteRoot sr = go.GetComponent<SpriteRoot>();
        if ((UnityEngine.Object)sr == null) return null;
        return AddSprite(sr);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/AddSprite_SpriteRoot.c RVA 0x15747DC
    // ~250-line decompile of the main add path. 1-1 mapping:
    //   if (sprite == null) throw NRE.
    //   // already-managed fast-path
    //   if (sprite.manager == this && sprite.addedToManager)
    //       return (SpriteMesh_Managed)sprite.spriteMesh;
    //   // not-yet-initialized: queue and return null
    //   if (!initialized) {
    //       if (spriteAddQueue == null) spriteAddQueue = new List<SpriteRoot>();
    //       spriteAddQueue.Add(sprite);
    //       return null;
    //   }
    //   // allocate a slot from availableBlocks; grow if empty
    //   if (availableBlocks.get_Empty()) EnlargeArrays(allocBlockSize);
    //   SpriteMesh_Managed slot = availableBlocks.get_Head();
    //   int idx = slot.index;
    //   availableBlocks.Remove(slot);
    //   SpriteMesh_Managed sm = sprites[idx];
    //   sprite.spriteMesh = sm;
    //   sprite.manager = this;
    //   sprite.addedToManager = true;
    //   if (sm != null) {
    //       sm.drawLayer = sprite.drawLayer;
    //       Transform spT = sprite.gameObject.transform;
    //       bones[idx] = spT;
    //       bindPoses[idx] = bones[idx].worldToLocalMatrix * sprite.transform.localToWorldMatrix;
    //       activeBlocks.Add(sm);
    //       sm.Init();                                          // virtual @vtable+0x338 → ISpriteMesh.Init
    //       SortDrawingOrder();
    //       colorsChanged = true;
    //       vertsChanged = true; uvsChanged = true;             // Ghidra writes 0x101 as ushort over (vertsChanged, uvsChanged)
    //       if (meshRenderer != null && !meshRenderer.enabled)
    //           meshRenderer.enabled = true;
    //       return sm;
    //   }
    //   throw NRE.
    public SpriteMesh_Managed AddSprite(SpriteRoot sprite)
    {
        if ((UnityEngine.Object)sprite == null) throw new System.NullReferenceException();
        // already-managed fast-path
        if ((UnityEngine.Object)sprite.manager == this && sprite.addedToManager)
        {
            return (SpriteMesh_Managed)sprite.spriteMesh;
        }
        // queue if not initialized
        if (!initialized)
        {
            if (spriteAddQueue == null) spriteAddQueue = new System.Collections.Generic.List<SpriteRoot>();
            spriteAddQueue.Add(sprite);
            return null;
        }
        if (activeBlocks == null || availableBlocks == null) throw new System.NullReferenceException();
        if (availableBlocks.get_Empty()) EnlargeArrays(allocBlockSize);
        SpriteMesh_Managed slot = availableBlocks.get_Head();
        if (slot == null) throw new System.NullReferenceException();
        int idx = slot.index;
        availableBlocks.Remove(slot);
        if (sprites == null) throw new System.NullReferenceException();
        if (idx < 0 || idx >= sprites.Length) throw new System.IndexOutOfRangeException();
        SpriteMesh_Managed sm = sprites[idx];
        sprite.spriteMesh = sm;
        sprite.manager = this;
        sprite.addedToManager = true;
        if (sm != null)
        {
            sm.drawLayer = sprite.drawLayer;
            UnityEngine.GameObject sprGO = sprite.gameObject;
            if ((UnityEngine.Object)sprGO == null) throw new System.NullReferenceException();
            UnityEngine.Transform spT = sprGO.transform;
            if ((UnityEngine.Object)spT == null) throw new System.NullReferenceException();
            if (bones == null) throw new System.NullReferenceException();
            if (idx >= bones.Length) throw new System.IndexOutOfRangeException();
            bones[idx] = spT;
            if (bindPoses == null) throw new System.NullReferenceException();
            if (idx >= bindPoses.Length) throw new System.IndexOutOfRangeException();
            bindPoses[idx] = bones[idx].worldToLocalMatrix * sprite.transform.localToWorldMatrix;
            activeBlocks.Add(sm);
            sm.Init();   // vtable @+0x338 → SpriteMesh_Managed.Init() per ISpriteMesh interface
            SortDrawingOrder();
            colorsChanged = true;
            vertsChanged = true;
            uvsChanged   = true;
            if ((UnityEngine.Object)meshRenderer != null && !meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
            }
            return sm;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/CreateSprite_prefab.c RVA 0x1575380
    // 1-1 wrapper: CreateSprite(prefab, Vector3.zero, Quaternion.identity).
    //   PTR_DAT_03446bc8 = Vector3.zero static (3 floats)
    //   PTR_DAT_03446b08 = Quaternion.identity static (4 floats)
    public SpriteRoot CreateSprite(GameObject prefab)
    {
        return CreateSprite(prefab, Vector3.zero, Quaternion.identity);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/CreateSprite_prefab_pos_rot.c RVA 0x1575430
    // 1-1:
    //   GameObject newGO = (GameObject)Object.Instantiate(prefab, position, rotation);
    //   if (newGO == null) throw NRE;
    //   SpriteRoot sr = newGO.GetComponent<SpriteRoot>();
    //   AddSprite(newGO);                                       // registers the prefab's SpriteRoot
    //   return sr;                                              // cast already enforced by Ghidra klass-check
    public SpriteRoot CreateSprite(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        UnityEngine.GameObject newGO = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(prefab, position, rotation);
        if ((UnityEngine.Object)newGO == null) throw new System.NullReferenceException();
        SpriteRoot sr = newGO.GetComponent<SpriteRoot>();
        AddSprite(newGO);
        return sr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/RemoveSprite_SpriteRoot.c RVA 0x15755CC
    // 1-1:
    //   if (sprite == null) throw NRE.
    //   if (sprite.spriteMesh == null) return.
    //   if (sprite.manager == this) {
    //       sprite.manager = null;
    //       sprite.addedToManager = false;
    //   }
    //   RemoveSprite((SpriteMesh_Managed)sprite.spriteMesh);
    public void RemoveSprite(SpriteRoot sprite)
    {
        if ((UnityEngine.Object)sprite == null) throw new System.NullReferenceException();
        if (sprite.spriteMesh == null) return;
        if ((UnityEngine.Object)sprite.manager == this)
        {
            sprite.manager = null;
            sprite.addedToManager = false;
        }
        RemoveSprite((SpriteMesh_Managed)sprite.spriteMesh);
    }

    // RVA: 0x15756FC  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/RemoveSprite.c
    public void RemoveSprite(SpriteMesh_Managed sprite) { throw new System.NotImplementedException(); }

    // RVA: 0x15759FC  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/MoveToFront.c
    public void MoveToFront(SpriteMesh_Managed s) { throw new System.NotImplementedException(); }

    // RVA: 0x1575DF8  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/MoveToBack.c
    public void MoveToBack(SpriteMesh_Managed s) { throw new System.NotImplementedException(); }

    // RVA: 0x15761C8  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/MoveInfrontOf.c
    public void MoveInfrontOf(SpriteMesh_Managed toMove, SpriteMesh_Managed reference) { throw new System.NotImplementedException(); }

    // RVA: 0x15765F0  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/MoveBehind.c
    public void MoveBehind(SpriteMesh_Managed toMove, SpriteMesh_Managed reference) { throw new System.NotImplementedException(); }

    // RVA: 0x1575144  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/SortDrawingOrder.c
    public void SortDrawingOrder() { throw new System.NotImplementedException(); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/GetSprite.c RVA 0x1576A18
    // 1-1: if (sprites == null) throw NRE;
    //      if (i < sprites.Length) return sprites[i];   (Ghidra bounds-check via Cpp2IL IOOR thrower)
    //      return null;
    public SpriteMesh_Managed GetSprite(int i)
    {
        if (sprites == null) throw new System.NullReferenceException();
        if (i < sprites.Length) return sprites[i];
        return null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/UpdatePositions.c RVA 0x1576A54
    // 1-1: vertsChanged = true;
    public void UpdatePositions() { vertsChanged = true; }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/UpdateUVs.c RVA 0x1576A60
    // 1-1: uvsChanged = true;
    public void UpdateUVs() { uvsChanged = true; }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/UpdateColors.c RVA 0x1576A6C
    // 1-1: colorsChanged = true;
    public void UpdateColors() { colorsChanged = true; }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/UpdateBounds.c RVA 0x1576A78
    // 1-1: updateBounds = true;
    public void UpdateBounds() { updateBounds = true; }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/ScheduleBoundsUpdate.c RVA 0x1576A84
    // 1-1:
    //   boundUpdateInterval = (int)seconds;   // Ghidra writes 4-byte int into field at +0x60
    //   InvokeRepeating("UpdateBounds", seconds, seconds);   // lit 12303 = "UpdateBounds"
    public void ScheduleBoundsUpdate(float seconds)
    {
        boundUpdateInterval = (float)(int)seconds;
        InvokeRepeating("UpdateBounds", seconds, seconds);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/CancelBoundsUpdate.c RVA 0x1576AE8
    // 1-1: CancelInvoke("UpdateBounds");   // lit 12303 = "UpdateBounds"
    public void CancelBoundsUpdate() { CancelInvoke("UpdateBounds"); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/get_IsInitialized.c RVA 0x1576B34
    // 1-1: return *(byte*)(this + 0x2b) — initialized field.
    public bool get_IsInitialized() { return initialized; }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/get_ManagedRenderer.c RVA 0x1576B3C
    // 1-1: lazy GetComponent<SkinnedMeshRenderer>() cache:
    //      if (meshRenderer != null) return meshRenderer;
    //      meshRenderer = GetComponent<SkinnedMeshRenderer>();
    //      return meshRenderer;
    // Returns Renderer per dump.cs (covariant — SkinnedMeshRenderer inherits Renderer).
    public Renderer get_ManagedRenderer()
    {
        if ((UnityEngine.Object)meshRenderer == null)
        {
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
        return meshRenderer;
    }

    // RVA: 0x1576BE4  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/LateUpdate.c
    public virtual void LateUpdate() { throw new System.NotImplementedException(); }

    // RVA: 0x1576D64  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/DoMirror.c
    public virtual void DoMirror() { throw new System.NotImplementedException(); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/Update.c RVA 0x1576F08
    // 1-1: virtual-call indirection — production Update body is empty (the vtable slot at
    // klass+0x188 / +0x190 dispatches to base MonoBehaviour Update which is no-op). Body
    // ported as empty since the actual per-frame mesh work lives in LateUpdate.
    private void Update() { }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/OnDrawGizmos.c RVA 0x1576F14
    // 1-1: if (drawBoundingBox) { Gizmos.color = white; DrawCenter(); … draw bounds }.
    // Editor-only — full Gizmos.DrawLine sequence is in DrawCenter; here we only gate on the
    // bool field and forward. Production includes a Renderer.bounds box-line block we
    // approximate by delegating to DrawCenter (DrawCenter is what Ghidra calls first).
    public virtual void OnDrawGizmos()
    {
        if (!drawBoundingBox) return;
        UnityEngine.Gizmos.color = UnityEngine.Color.white;
        DrawCenter();
        if ((UnityEngine.Object)meshRenderer != null)
        {
            UnityEngine.Bounds b = meshRenderer.bounds;
            UnityEngine.Vector3 c = b.center;
            UnityEngine.Vector3 e = b.extents;
            UnityEngine.Vector3 v000 = new UnityEngine.Vector3(c.x - e.x, c.y - e.y, c.z - e.z);
            UnityEngine.Vector3 v100 = new UnityEngine.Vector3(c.x + e.x, c.y - e.y, c.z - e.z);
            UnityEngine.Vector3 v010 = new UnityEngine.Vector3(c.x - e.x, c.y + e.y, c.z - e.z);
            UnityEngine.Vector3 v110 = new UnityEngine.Vector3(c.x + e.x, c.y + e.y, c.z - e.z);
            UnityEngine.Vector3 v001 = new UnityEngine.Vector3(c.x - e.x, c.y - e.y, c.z + e.z);
            UnityEngine.Vector3 v101 = new UnityEngine.Vector3(c.x + e.x, c.y - e.y, c.z + e.z);
            UnityEngine.Vector3 v011 = new UnityEngine.Vector3(c.x - e.x, c.y + e.y, c.z + e.z);
            UnityEngine.Vector3 v111 = new UnityEngine.Vector3(c.x + e.x, c.y + e.y, c.z + e.z);
            UnityEngine.Gizmos.DrawLine(v000, v100); UnityEngine.Gizmos.DrawLine(v100, v110);
            UnityEngine.Gizmos.DrawLine(v110, v010); UnityEngine.Gizmos.DrawLine(v010, v000);
            UnityEngine.Gizmos.DrawLine(v001, v101); UnityEngine.Gizmos.DrawLine(v101, v111);
            UnityEngine.Gizmos.DrawLine(v111, v011); UnityEngine.Gizmos.DrawLine(v011, v001);
            UnityEngine.Gizmos.DrawLine(v000, v001); UnityEngine.Gizmos.DrawLine(v100, v101);
            UnityEngine.Gizmos.DrawLine(v110, v111); UnityEngine.Gizmos.DrawLine(v010, v011);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/OnDrawGizmosSelected.c RVA 0x15772FC
    // 1-1: Gizmos.color = white; DrawCenter().
    // Editor-only — fires when the GameObject is selected in scene view.
    public void OnDrawGizmosSelected()
    {
        UnityEngine.Gizmos.color = UnityEngine.Color.white;
        DrawCenter();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/DrawCenter.c RVA 0x1576FC8
    // 1-1: draws a small "+" cross at this.transform.position. Editor-only visualisation.
    // The Ghidra sequence is a series of Gizmos.DrawLine calls between Vector3 offsets of
    // ±0.1 along each axis from transform.position. Constants 0x3dcccccd / 0xbdcccccd =
    // ±0.1f IEEE-float.
    protected void DrawCenter()
    {
        UnityEngine.Transform t = base.transform;
        if ((UnityEngine.Object)t == null) return;
        UnityEngine.Vector3 p = t.position;
        const float s = 0.1f;
        UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(p.x - s, p.y, p.z), new UnityEngine.Vector3(p.x + s, p.y, p.z));
        UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(p.x, p.y - s, p.z), new UnityEngine.Vector3(p.x, p.y + s, p.z));
        UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(p.x, p.y, p.z - s), new UnityEngine.Vector3(p.x, p.y, p.z + s));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/_ctor.c RVA 0x15773A8
    // 1-1:
    //   autoUpdateBounds = true;                              // byte @ 0x28
    //   winding = WINDING_ORDER.CW (== 1 per DAT_008e3c10);   // enum @ 0x20
    //   availableBlocks = new EZLinkedList<SpriteMesh_Managed>();   // @ 0x30
    //   activeBlocks    = new EZLinkedList<SpriteMesh_Managed>();   // @ 0x48
    //   spriteDrawOrder = new List<SpriteMesh_Managed>();           // @ 0x50
    //   spriteAddQueue  = new List<SpriteRoot>();                   // @ 0x68
    //   drawOrderComparer = new SpriteDrawLayerComparer();          // @ 0x58
    //   allocBlockSize    = 0xa  (= 10, default block-alloc count);// int @ 0x24
    //   MonoBehaviour..ctor(this);
    public SpriteManager()
    {
        autoUpdateBounds  = true;
        winding           = SpriteRoot.WINDING_ORDER.CW;
        allocBlockSize    = 10;
        availableBlocks   = new EZLinkedList<SpriteMesh_Managed>();
        activeBlocks      = new EZLinkedList<SpriteMesh_Managed>();
        spriteDrawOrder   = new System.Collections.Generic.List<SpriteMesh_Managed>();
        spriteAddQueue    = new System.Collections.Generic.List<SpriteRoot>();
        drawOrderComparer = new SpriteDrawLayerComparer();
    }

}
