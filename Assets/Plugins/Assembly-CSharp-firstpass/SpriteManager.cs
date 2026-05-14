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

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/SetupBoneWeights.c RVA 0x1573B1C
    // 1-1: for each of the 4 vertex slots (mv1..mv4 — SpriteMesh_Managed offsets 0x90/94/98/9c),
    //      writes boneWeights[mvN].boneIndex0 = s.index, .weight0 = 1.0f. Pins all 4 quad
    //      vertices to the same bone — the bone driving sm's SpriteRoot transform.
    protected void SetupBoneWeights(SpriteMesh_Managed s)
    {
        if (s == null) throw new System.NullReferenceException();
        if (boneWeights == null) throw new System.NullReferenceException();
        int idx = s.index;
        int[] mv = new int[] { s.mv1, s.mv2, s.mv3, s.mv4 };
        for (int k = 0; k < 4; k++)
        {
            int slot = mv[k];
            if ((uint)slot >= (uint)boneWeights.Length) throw new System.IndexOutOfRangeException();
            UnityEngine.BoneWeight bw = boneWeights[slot];
            bw.boneIndex0 = idx;
            bw.weight0    = 1f;
            boneWeights[slot] = bw;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/Awake.c RVA 0x1573CA0
    // 1-1 mapping:
    //   if (spriteAddQueue == null) spriteAddQueue = new List<SpriteRoot>();
    //   meshRenderer = this.GetComponent<SkinnedMeshRenderer>();
    //   if (meshRenderer != null) {
    //       Material sm = meshRenderer.sharedMaterial;
    //       if (sm != null) texture = sm.GetTexture("_MainTex");     // lit 13370
    //   }
    //   if (meshRenderer != null) {
    //       if (meshRenderer.sharedMesh == null) meshRenderer.sharedMesh = new Mesh();
    //       mesh = meshRenderer.sharedMesh;
    //       if (mesh != null) {
    //           mesh.MarkDynamic();
    //           if (persistent) { DontDestroyOnLoad(this); DontDestroyOnLoad(mesh); }
    //           EnlargeArrays(allocBlockSize);
    //           this.transform.rotation = Quaternion.identity;        // PTR_DAT_03446b08 = Quaternion.identity static
    //           initialized = true;
    //           // Drain spriteAddQueue
    //           for (int i = 0; i < spriteAddQueue.Count; i++) AddSprite(spriteAddQueue[i]);
    //       }
    //   }
    private void Awake()
    {
        if (spriteAddQueue == null) spriteAddQueue = new System.Collections.Generic.List<SpriteRoot>();
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        if ((UnityEngine.Object)meshRenderer != null)
        {
            UnityEngine.Material sharedMat = meshRenderer.sharedMaterial;
            if ((UnityEngine.Object)sharedMat != null)
            {
                texture = sharedMat.GetTexture("_MainTex");
            }
        }
        if ((UnityEngine.Object)meshRenderer != null)
        {
            if ((UnityEngine.Object)meshRenderer.sharedMesh == null)
            {
                meshRenderer.sharedMesh = new UnityEngine.Mesh();
            }
            mesh = meshRenderer.sharedMesh;
            if ((UnityEngine.Object)mesh != null)
            {
                mesh.MarkDynamic();
                if (persistent)
                {
                    UnityEngine.Object.DontDestroyOnLoad(this);
                    UnityEngine.Object.DontDestroyOnLoad(mesh);
                }
                EnlargeArrays(allocBlockSize);
                base.transform.rotation = UnityEngine.Quaternion.identity;
                initialized = true;
                if (spriteAddQueue != null)
                {
                    for (int i = 0; i < spriteAddQueue.Count; i++)
                    {
                        AddSprite(spriteAddQueue[i]);
                    }
                }
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/InitArrays.c RVA 0x1574C48
    // 1-1 mapping (initial 1-slot allocation; EnlargeArrays grows from here):
    //   bones        = new Transform[1];
    //   bones[0]     = this.transform;
    //   bindPoses    = new Matrix4x4[1];
    //   sprites      = new SpriteMesh_Managed[1];
    //   sprites[0]   = new SpriteMesh_Managed();
    //   vertices     = new Vector3[4];   // 4 verts per quad
    //   UVs          = new Vector2[4];
    //   UVs2         = new Vector2[4];
    //   triIndices   = new int[6];       // 2 triangles × 3 indices  (Ghidra count=6 → 0xa8)
    //   boneWeights  = new BoneWeight[4]; // 1 per vertex             (Ghidra count=4 → 0x90)
    //   sprites[0].index = 0;
    //   sprites[0].vertCount? / triCount? = some default vec from .rodata (skipped — fields
    //   not yet identified in SpriteMesh_Managed dump, defaults to zero-init is fine).
    //   SetupBoneWeights(sprites[0]);
    protected void InitArrays()
    {
        bones = new UnityEngine.Transform[1];
        bones[0] = base.transform;
        bindPoses = new UnityEngine.Matrix4x4[1];
        sprites = new SpriteMesh_Managed[1];
        sprites[0] = new SpriteMesh_Managed();
        vertices    = new UnityEngine.Vector3[4];
        UVs         = new UnityEngine.Vector2[4];
        // 0xc0 = colors[] (NOT UVs2). LateUpdate writes Mesh.colors = this+0xc0 confirming
        // the field at 0xc0 is the Color buffer. UVs2 is lazily allocated by the UseUV2 path.
        colors      = new UnityEngine.Color[4];
        triIndices  = new int[6];
        boneWeights = new UnityEngine.BoneWeight[4];
        sprites[0].index = 0;
        SetupBoneWeights(sprites[0]);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/EnlargeArrays.c RVA 0x1574078
    // 1-1 condensed (the 356-line decompile expands every Array.Copy inline; logic mirrored
    // here using System.Array.Resize for clarity but semantically identical):
    //   if (sprites == null) InitArrays();
    //   int oldSpriteCnt = sprites.Length;
    //   int newSpriteCnt = oldSpriteCnt + count;
    //   Array.Resize(ref sprites, newSpriteCnt);                  // SpriteMesh_Managed[]
    //   Array.Resize(ref bones, newSpriteCnt);                    // Transform[]
    //   Array.Resize(ref bindPoses, newSpriteCnt);                // Matrix4x4[]
    //   Array.Resize(ref vertices,    newSpriteCnt * 4);          // 4 verts per quad
    //   Array.Resize(ref UVs,         newSpriteCnt * 4);
    //   Array.Resize(ref UVs2,        newSpriteCnt * 4);
    //   Array.Resize(ref triIndices,  newSpriteCnt * 6);          // 6 indices per quad
    //   Array.Resize(ref boneWeights, newSpriteCnt * 4);
    //   for (int i = oldSpriteCnt; i < newSpriteCnt; i++) {
    //       sprites[i] = new SpriteMesh_Managed();
    //       sprites[i].index = i;
    //       sprites[i].mv1 = i*4;   sprites[i].mv2 = i*4 + 1;
    //       sprites[i].mv3 = i*4 + 2; sprites[i].mv4 = i*4 + 3;
    //       sprites[i].uv1 = sprites[i].mv1; ... uv4 = mv4;
    //       sprites[i].cv1 = sprites[i].mv1; ... cv4 = mv4;
    //       // wire 2 triangles into triIndices
    //       triIndices[i*6 + 0] = i*4 + 0;
    //       triIndices[i*6 + 1] = i*4 + 1;
    //       triIndices[i*6 + 2] = i*4 + 2;
    //       triIndices[i*6 + 3] = i*4 + 2;
    //       triIndices[i*6 + 4] = i*4 + 3;
    //       triIndices[i*6 + 5] = i*4 + 0;
    //       // add to availableBlocks pool
    //       availableBlocks.Add(sprites[i]);
    //       SetupBoneWeights(sprites[i]);
    //   }
    //   vertCountChanged = true;
    //   return newSpriteCnt;
    protected int EnlargeArrays(int count)
    {
        if (sprites == null) InitArrays();
        int oldCnt = sprites.Length;
        int newCnt = oldCnt + count;
        System.Array.Resize(ref sprites, newCnt);
        System.Array.Resize(ref bones, newCnt);
        System.Array.Resize(ref bindPoses, newCnt);
        System.Array.Resize(ref vertices,    newCnt * 4);
        System.Array.Resize(ref UVs,         newCnt * 4);
        System.Array.Resize(ref colors,      newCnt * 4);
        System.Array.Resize(ref triIndices,  newCnt * 6);
        System.Array.Resize(ref boneWeights, newCnt * 4);
        if (UVs2 != null) System.Array.Resize(ref UVs2, newCnt * 4);   // optional, only if previously allocated
        for (int i = oldCnt; i < newCnt; i++)
        {
            sprites[i] = new SpriteMesh_Managed();
            sprites[i].index = i;
            sprites[i].mv1 = i * 4 + 0;
            sprites[i].mv2 = i * 4 + 1;
            sprites[i].mv3 = i * 4 + 2;
            sprites[i].mv4 = i * 4 + 3;
            sprites[i].uv1 = sprites[i].mv1;
            sprites[i].uv2 = sprites[i].mv2;
            sprites[i].uv3 = sprites[i].mv3;
            sprites[i].uv4 = sprites[i].mv4;
            sprites[i].cv1 = sprites[i].mv1;
            sprites[i].cv2 = sprites[i].mv2;
            sprites[i].cv3 = sprites[i].mv3;
            sprites[i].cv4 = sprites[i].mv4;
            triIndices[i * 6 + 0] = i * 4 + 0;
            triIndices[i * 6 + 1] = i * 4 + 1;
            triIndices[i * 6 + 2] = i * 4 + 2;
            triIndices[i * 6 + 3] = i * 4 + 2;
            triIndices[i * 6 + 4] = i * 4 + 3;
            triIndices[i * 6 + 5] = i * 4 + 0;
            if (availableBlocks != null) availableBlocks.Add(sprites[i]);
            SetupBoneWeights(sprites[i]);
        }
        vertCountChanged = true;
        return newCnt;
    }

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

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/RemoveSprite.c RVA 0x15756FC
    // 1-1:
    //   if (sprite == null) NRE;
    //   vertices[mv1] = vertices[mv2] = vertices[mv3] = vertices[mv4] = Vector3.zero;   // static prefab
    //   activeBlocks.Remove(sprite);                                   // 0x48
    //   if (this.gameObject != null) { bones[sprite.index] = this.transform; }   // 0x88, sprite+0x1c
    //   sprite.Clear();                                                // SpriteMesh_Managed.Clear
    //   SpriteRoot owner = sprite.sprite;                              // virtual (vtable+0x2a8)
    //   if (owner == null) NRE;                                        // Ghidra falls through to NRE if get_sprite returns null
    //   owner.spriteMesh = null;                                       // SpriteRoot.set_spriteMesh
    //   sprite.sprite = null;                                          // virtual (vtable+0x2b8)
    //   availableBlocks.Add(sprite);                                   // 0x30
    //   vertsChanged = true;                                           // 0x38
    //   if (activeBlocks.Count == 0) meshRenderer.enabled = false;     // 0x70 — disable when no sprites left
    // Ghidra emits IL2CPP-array bounds-check IOOR throws inline (FUN_015cb904) — managed array
    // indexer raises IndexOutOfRangeException automatically; no explicit guard needed.
    public void RemoveSprite(SpriteMesh_Managed sprite)
    {
        if (sprite == null) throw new System.NullReferenceException();
        if (vertices == null) throw new System.NullReferenceException();
        vertices[sprite.mv1] = Vector3.zero;
        vertices[sprite.mv2] = Vector3.zero;
        vertices[sprite.mv3] = Vector3.zero;
        vertices[sprite.mv4] = Vector3.zero;
        if (activeBlocks == null) throw new System.NullReferenceException();
        activeBlocks.Remove(sprite);
        if ((UnityEngine.Object)gameObject != null)
        {
            if (bones == null) throw new System.NullReferenceException();
            bones[sprite.index] = transform;
        }
        sprite.Clear();
        SpriteRoot owner = sprite.sprite;     // virtual get_sprite
        if (owner == null) throw new System.NullReferenceException();
        owner.spriteMesh = null;
        sprite.sprite = null;                 // virtual set_sprite
        if (availableBlocks == null) throw new System.NullReferenceException();
        availableBlocks.Add(sprite);
        vertsChanged = true;
        if (activeBlocks.get_Count() == 0)
        {
            if ((UnityEngine.Object)meshRenderer == null) throw new System.NullReferenceException();
            meshRenderer.enabled = false;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/MoveToFront.c RVA 0x15759FC
    // 1-1: pop sprite at idx in spriteDrawOrder and push to back (= drawn last = on top).
    //   int[] buffer = new int[6];                            // FUN_015cb754(int_klass, 6)
    //   if (spriteDrawOrder == null) NRE;
    //   int idx = spriteDrawOrder.IndexOf(s);
    //   uVar11 = idx * 6;
    //   if (uVar11 < 0) return;                               // not found
    //   SpriteMesh_Managed last = spriteDrawOrder[Count - 1];
    //   if (last == null || s == null) NRE;
    //   s.drawLayer = last.drawLayer + 1;                     // (sprite+0x20)
    //   if (triIndices == null) NRE;
    //   if (idx*6 < triIndices.Length) {
    //       for (k=0..5) buffer[k] = triIndices[idx*6 + k];
    //       while (idx*6 + 1 < count*6 - 6) {                 // shift triangle k right-neighbour into [idx]
    //           for (k=0..5) triIndices[idx*6 + k] = triIndices[(idx+1)*6 + k];
    //           spriteDrawOrder[idx] = spriteDrawOrder[idx + 1];
    //           idx++;
    //       }
    //       // tail: write buffer to [Count-1]*6
    //       for (k=0..5) triIndices[(Count-1)*6 + k] = buffer[k];
    //       spriteDrawOrder[Count - 1] = s;
    //       vertCountChanged = true;
    //   }
    public void MoveToFront(SpriteMesh_Managed s)
    {
        int[] buffer = new int[6];
        if (spriteDrawOrder == null) throw new System.NullReferenceException();
        int idx = spriteDrawOrder.IndexOf(s);
        if (idx * 6 < 0) return;                              // not found (idx == -1 → -6)
        int count = spriteDrawOrder.Count;
        SpriteMesh_Managed last = spriteDrawOrder[count - 1];
        if (last == null) throw new System.NullReferenceException();
        if (s == null) throw new System.NullReferenceException();
        s.drawLayer = last.drawLayer + 1;
        if (triIndices == null) throw new System.NullReferenceException();
        if (idx * 6 >= triIndices.Length) return;
        for (int k = 0; k < 6; k++) buffer[k] = triIndices[idx * 6 + k];
        for (int i = idx; i < count - 1; i++)
        {
            for (int k = 0; k < 6; k++) triIndices[i * 6 + k] = triIndices[(i + 1) * 6 + k];
            spriteDrawOrder[i] = spriteDrawOrder[i + 1];
        }
        for (int k = 0; k < 6; k++) triIndices[(count - 1) * 6 + k] = buffer[k];
        spriteDrawOrder[count - 1] = s;
        vertCountChanged = true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/MoveToBack.c RVA 0x1575DF8
    // 1-1: pop sprite at idx and push to front (= drawn first = behind everything).
    //   buffer = new int[6];
    //   if (spriteDrawOrder == null) NRE;
    //   int idx = spriteDrawOrder.IndexOf(s);
    //   if (idx*6 < 0) return;
    //   SpriteMesh_Managed first = spriteDrawOrder[0];
    //   if (first == null || s == null) NRE;
    //   s.drawLayer = first.drawLayer - 1;
    //   if (triIndices == null) NRE;
    //   if (idx*6 < triIndices.Length) {
    //       save buffer = triIndices[idx*6..+5];
    //       for (i = idx; i > 0; i--) {                       // loop iterates while idx*6 > 5
    //           triIndices[i*6..+5] = triIndices[(i-1)*6..+5];
    //           spriteDrawOrder[i] = spriteDrawOrder[i - 1];
    //       }
    //       triIndices[0..5] = buffer;
    //       spriteDrawOrder[0] = s;
    //       vertCountChanged = true;
    //   }
    public void MoveToBack(SpriteMesh_Managed s)
    {
        int[] buffer = new int[6];
        if (spriteDrawOrder == null) throw new System.NullReferenceException();
        int idx = spriteDrawOrder.IndexOf(s);
        if (idx * 6 < 0) return;
        SpriteMesh_Managed first = spriteDrawOrder[0];
        if (first == null) throw new System.NullReferenceException();
        if (s == null) throw new System.NullReferenceException();
        s.drawLayer = first.drawLayer - 1;
        if (triIndices == null) throw new System.NullReferenceException();
        if (idx * 6 >= triIndices.Length) return;
        for (int k = 0; k < 6; k++) buffer[k] = triIndices[idx * 6 + k];
        for (int i = idx; i > 0; i--)
        {
            for (int k = 0; k < 6; k++) triIndices[i * 6 + k] = triIndices[(i - 1) * 6 + k];
            spriteDrawOrder[i] = spriteDrawOrder[i - 1];
        }
        for (int k = 0; k < 6; k++) triIndices[k] = buffer[k];
        spriteDrawOrder[0] = s;
        vertCountChanged = true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/MoveInfrontOf.c RVA 0x15761C8
    // 1-1: relocate toMove to immediately in front of (after) reference in spriteDrawOrder.
    //   Only proceeds if reference is currently AFTER toMove (idx_ref > idx_to); otherwise no-op.
    //   buffer = new int[6];
    //   if (spriteDrawOrder == null) NRE;
    //   int idx_to  = spriteDrawOrder.IndexOf(toMove);   uVar10 = idx_to * 6;
    //   int idx_ref = spriteDrawOrder.IndexOf(reference); uVar2 = idx_ref * 6;
    //   if (uVar10 < 0 || uVar2 < uVar10) return;        // not found OR ref already behind toMove
    //   if (reference == null || toMove == null) NRE;
    //   toMove.drawLayer = reference.drawLayer + 1;
    //   if (triIndices == null) NRE;
    //   if (idx_to*6 < triIndices.Length) {
    //       save buffer = triIndices[idx_to*6 .. +5];
    //       for (i = idx_to; i < idx_ref; i++) {
    //           triIndices[i*6..+5] = triIndices[(i+1)*6..+5];
    //           spriteDrawOrder[i] = spriteDrawOrder[i + 1];
    //       }
    //       triIndices[idx_ref*6..+5] = buffer;
    //       spriteDrawOrder[idx_ref] = toMove;
    //       vertCountChanged = true;
    //   }
    public void MoveInfrontOf(SpriteMesh_Managed toMove, SpriteMesh_Managed reference)
    {
        int[] buffer = new int[6];
        if (spriteDrawOrder == null) throw new System.NullReferenceException();
        int idx_to  = spriteDrawOrder.IndexOf(toMove);
        int idx_ref = spriteDrawOrder.IndexOf(reference);
        if (idx_to * 6 < 0)          return;             // toMove not found
        if (idx_ref * 6 < idx_to * 6) return;            // reference already behind toMove
        if (reference == null) throw new System.NullReferenceException();
        if (toMove == null)    throw new System.NullReferenceException();
        toMove.drawLayer = reference.drawLayer + 1;
        if (triIndices == null) throw new System.NullReferenceException();
        if (idx_to * 6 >= triIndices.Length) return;
        for (int k = 0; k < 6; k++) buffer[k] = triIndices[idx_to * 6 + k];
        for (int i = idx_to; i < idx_ref; i++)
        {
            for (int k = 0; k < 6; k++) triIndices[i * 6 + k] = triIndices[(i + 1) * 6 + k];
            spriteDrawOrder[i] = spriteDrawOrder[i + 1];
        }
        for (int k = 0; k < 6; k++) triIndices[idx_ref * 6 + k] = buffer[k];
        spriteDrawOrder[idx_ref] = toMove;
        vertCountChanged = true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/MoveBehind.c RVA 0x15765F0
    // 1-1: relocate toMove to immediately behind (before) reference. Only proceeds if reference
    //   is currently BEFORE toMove (idx_ref < idx_to); otherwise no-op.
    //   if (uVar10 < 0 || uVar10 < uVar2) return;        // toMove not found OR toMove already behind ref
    //   toMove.drawLayer = reference.drawLayer - 1;
    //   save buffer = triIndices[idx_to*6 .. +5];
    //   for (i = idx_to; i > idx_ref; i--) {
    //       triIndices[i*6..+5] = triIndices[(i-1)*6..+5];
    //       spriteDrawOrder[i] = spriteDrawOrder[i - 1];
    //   }
    //   triIndices[idx_ref*6..+5] = buffer;
    //   spriteDrawOrder[idx_ref] = toMove;
    //   vertCountChanged = true;
    public void MoveBehind(SpriteMesh_Managed toMove, SpriteMesh_Managed reference)
    {
        int[] buffer = new int[6];
        if (spriteDrawOrder == null) throw new System.NullReferenceException();
        int idx_to  = spriteDrawOrder.IndexOf(toMove);
        int idx_ref = spriteDrawOrder.IndexOf(reference);
        if (idx_to * 6 < 0)          return;
        if (idx_to * 6 < idx_ref * 6) return;            // toMove already behind reference
        if (reference == null) throw new System.NullReferenceException();
        if (toMove == null)    throw new System.NullReferenceException();
        toMove.drawLayer = reference.drawLayer - 1;
        if (triIndices == null) throw new System.NullReferenceException();
        if (idx_to * 6 >= triIndices.Length) return;
        for (int k = 0; k < 6; k++) buffer[k] = triIndices[idx_to * 6 + k];
        for (int i = idx_to; i > idx_ref; i--)
        {
            for (int k = 0; k < 6; k++) triIndices[i * 6 + k] = triIndices[(i - 1) * 6 + k];
            spriteDrawOrder[i] = spriteDrawOrder[i - 1];
        }
        for (int k = 0; k < 6; k++) triIndices[idx_ref * 6 + k] = buffer[k];
        spriteDrawOrder[idx_ref] = toMove;
        vertCountChanged = true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/SortDrawingOrder.c RVA 0x1575144
    // 1-1:
    //   if (spriteDrawOrder == null) return;                       // Ghidra: branch on (param_1+0x50)!=0
    //   spriteDrawOrder.Sort(drawOrderComparer);
    //   if (winding == CCW) {                                       // (param_1+0x20)==0
    //       for (int i = 0, t = 0; i < spriteDrawOrder.Count; i++, t += 6) {
    //           sm = spriteDrawOrder[i]; if (sm == null) break;
    //           triIndices[t+0] = sm.mv1;  triIndices[t+1] = sm.mv2;  triIndices[t+2] = sm.mv4;
    //           triIndices[t+3] = sm.mv4;  triIndices[t+4] = sm.mv2;  triIndices[t+5] = sm.mv3;
    //       }
    //   } else {                                                    // CW
    //       triIndices[t+0]=mv1; triIndices[t+1]=mv4; triIndices[t+2]=mv2;
    //       triIndices[t+3]=mv4; triIndices[t+4]=mv3; triIndices[t+5]=mv2;
    //   }
    //   vertCountChanged = true;     // 0x3b — guarantees LateUpdate flushes triangles next frame
    // Note: Ghidra emits explicit bounds checks `(uVar2 <= uVar7+k) goto FUN_015cb904` (Cpp2IL IOOR
    // thrower). In managed C# the same IndexOutOfRangeException is raised automatically by the
    // array indexer — we don't need explicit checks.
    public void SortDrawingOrder()
    {
        if (spriteDrawOrder == null) throw new System.NullReferenceException();
        spriteDrawOrder.Sort(drawOrderComparer);
        if (triIndices == null) throw new System.NullReferenceException();
        int triPos = 0;
        int count  = spriteDrawOrder.Count;
        if (winding == SpriteRoot.WINDING_ORDER.CCW)
        {
            for (int i = 0; i < count; i++)
            {
                SpriteMesh_Managed sm = spriteDrawOrder[i];
                if (sm == null) break;
                triIndices[triPos + 0] = sm.mv1;
                triIndices[triPos + 1] = sm.mv2;
                triIndices[triPos + 2] = sm.mv4;
                triIndices[triPos + 3] = sm.mv4;
                triIndices[triPos + 4] = sm.mv2;
                triIndices[triPos + 5] = sm.mv3;
                triPos += 6;
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                SpriteMesh_Managed sm = spriteDrawOrder[i];
                if (sm == null) break;
                triIndices[triPos + 0] = sm.mv1;
                triIndices[triPos + 1] = sm.mv4;
                triIndices[triPos + 2] = sm.mv2;
                triIndices[triPos + 3] = sm.mv4;
                triIndices[triPos + 4] = sm.mv3;
                triIndices[triPos + 5] = sm.mv2;
                triPos += 6;
            }
        }
        vertCountChanged = true;
    }

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

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/LateUpdate.c RVA 0x1576BE4
    // 1-1 — two branches:
    //   if (vertCountChanged) {                             // big rebuild — flush every buffer
    //       updateBounds = false;
    //       vertsChanged = uvsChanged = colorsChanged = vertCountChanged = false;
    //       meshRenderer.bones      = bones;                // SkinnedMeshRenderer.set_bones
    //       mesh.Clear();
    //       mesh.vertices    = vertices;
    //       mesh.bindposes   = bindPoses;
    //       mesh.boneWeights = boneWeights;
    //       mesh.uv          = UVs;
    //       mesh.colors      = colors;                       // 0xc0
    //       mesh.triangles   = triIndices;                   // 0xa8
    //       mesh.RecalculateNormals();
    //       if (autoUpdateBounds) mesh.RecalculateBounds();
    //   }
    //   else {                                              // partial updates
    //       if (vertsChanged)   { vertsChanged = false; if (autoUpdateBounds) updateBounds = true; mesh.vertices = vertices; }
    //       if (updateBounds)   { mesh.RecalculateBounds(); updateBounds = false; }
    //       if (colorsChanged)  { colorsChanged = false; mesh.colors = colors; }
    //       if (uvsChanged)     { uvsChanged = false; mesh.uv = UVs; }
    //   }
    public virtual void LateUpdate()
    {
        if ((UnityEngine.Object)mesh == null) return;        // nothing wired yet — Awake hasn't run
        if (vertCountChanged)
        {
            updateBounds     = false;
            vertsChanged     = false;
            uvsChanged       = false;
            colorsChanged    = false;
            vertCountChanged = false;
            if ((UnityEngine.Object)meshRenderer != null) meshRenderer.bones = bones;
            mesh.Clear();
            mesh.vertices    = vertices;
            mesh.bindposes   = bindPoses;
            mesh.boneWeights = boneWeights;
            mesh.uv          = UVs;
            mesh.colors      = colors;
            mesh.triangles   = triIndices;
            mesh.RecalculateNormals();
            if (autoUpdateBounds) mesh.RecalculateBounds();
            return;
        }
        if (vertsChanged)
        {
            vertsChanged = false;
            if (autoUpdateBounds) updateBounds = true;
            mesh.vertices = vertices;
        }
        if (updateBounds)
        {
            mesh.RecalculateBounds();
            updateBounds = false;
        }
        if (colorsChanged)
        {
            colorsChanged = false;
            mesh.colors = colors;
        }
        if (uvsChanged)
        {
            uvsChanged = false;
            mesh.uv = UVs;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SpriteManager/DoMirror.c RVA 0x1576D64
    // 1-1: editor-mode mirror of LateUpdate — runs from Editor inspector to refresh preview mesh.
    //   if (Application.isPlaying) return;     // play-mode handled by LateUpdate
    //   if (!vertCountChanged) {               // partial update branch
    //       if (vertsChanged)  { vertsChanged = false; updateBounds = true; mesh.vertices = vertices; }
    //       if (updateBounds)  { mesh.RecalculateBounds(); updateBounds = false; }
    //       if (colorsChanged) { colorsChanged = false; mesh.colors = colors; }
    //       if (uvsChanged)    { uvsChanged = false; mesh.uv = UVs; }
    //   } else {                                 // big rebuild branch
    //       updateBounds = false;
    //       vertsChanged = uvsChanged = colorsChanged = vertCountChanged = false;
    //       meshRenderer.bones = bones;
    //       mesh.Clear();
    //       mesh.vertices    = vertices;
    //       mesh.bindposes   = bindPoses;
    //       mesh.boneWeights = boneWeights;
    //       mesh.uv          = UVs;
    //       mesh.colors      = colors;
    //       mesh.triangles   = triIndices;
    //   }
    // Differences vs LateUpdate: (a) DoMirror always forces updateBounds=true on vertsChanged
    // (no autoUpdateBounds gate); (b) big-rebuild branch does NOT call RecalculateNormals or
    // RecalculateBounds — editor inspector refresh is light.
    public virtual void DoMirror()
    {
        if (UnityEngine.Application.isPlaying) return;
        if (!vertCountChanged)
        {
            if (vertsChanged)
            {
                vertsChanged = false;
                updateBounds = true;
                if ((UnityEngine.Object)mesh == null) throw new System.NullReferenceException();
                mesh.vertices = vertices;
            }
            if (updateBounds)
            {
                if ((UnityEngine.Object)mesh == null) throw new System.NullReferenceException();
                mesh.RecalculateBounds();
                updateBounds = false;
            }
            if (colorsChanged)
            {
                colorsChanged = false;
                if ((UnityEngine.Object)mesh == null) throw new System.NullReferenceException();
                mesh.colors = colors;
            }
            if (!uvsChanged) return;
            uvsChanged = false;
            if ((UnityEngine.Object)mesh == null) throw new System.NullReferenceException();
            mesh.uv = UVs;
        }
        else
        {
            updateBounds     = false;
            vertsChanged     = false;
            uvsChanged       = false;
            colorsChanged    = false;
            vertCountChanged = false;
            if ((UnityEngine.Object)meshRenderer == null) throw new System.NullReferenceException();
            meshRenderer.bones = bones;
            if ((UnityEngine.Object)mesh == null) throw new System.NullReferenceException();
            mesh.Clear();
            mesh.vertices    = vertices;
            mesh.bindposes   = bindPoses;
            mesh.boneWeights = boneWeights;
            mesh.uv          = UVs;
            mesh.colors      = colors;
            mesh.triangles   = triIndices;
        }
    }

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
