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

    // RVA: 0x1573908  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/PixelSpaceToUVSpace.c
    public Vector2 PixelSpaceToUVSpace(Vector2 xy) { throw new System.NotImplementedException(); }

    // RVA: 0x15739FC  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/PixelSpaceToUVSpace.c
    public Vector2 PixelSpaceToUVSpace(int x, int y) { throw new System.NotImplementedException(); }

    // RVA: 0x1573A08  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/PixelCoordToUVCoord.c
    public Vector2 PixelCoordToUVCoord(Vector2 xy) { throw new System.NotImplementedException(); }

    // RVA: 0x1573B10  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/PixelCoordToUVCoord.c
    public Vector2 PixelCoordToUVCoord(int x, int y) { throw new System.NotImplementedException(); }

    // RVA: 0x1573B1C  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/SetupBoneWeights.c
    protected void SetupBoneWeights(SpriteMesh_Managed s) { throw new System.NotImplementedException(); }

    // RVA: 0x1573CA0  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/Awake.c
    private void Awake() { throw new System.NotImplementedException(); }

    // RVA: 0x1574C48  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/InitArrays.c
    protected void InitArrays() { throw new System.NotImplementedException(); }

    // RVA: 0x1574078  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/EnlargeArrays.c
    protected int EnlargeArrays(int count) { throw new System.NotImplementedException(); }

    // RVA: 0x1574F00  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/AlreadyAdded.c
    public bool AlreadyAdded(SpriteRoot sprite) { throw new System.NotImplementedException(); }

    // RVA: 0x1575008  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/AddSprite.c
    public SpriteMesh_Managed AddSprite(GameObject go) { throw new System.NotImplementedException(); }

    // RVA: 0x15747DC  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/AddSprite.c
    public SpriteMesh_Managed AddSprite(SpriteRoot sprite) { throw new System.NotImplementedException(); }

    // RVA: 0x1575380  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/CreateSprite.c
    public SpriteRoot CreateSprite(GameObject prefab) { throw new System.NotImplementedException(); }

    // RVA: 0x1575430  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/CreateSprite.c
    public SpriteRoot CreateSprite(GameObject prefab, Vector3 position, Quaternion rotation) { throw new System.NotImplementedException(); }

    // RVA: 0x15755CC  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/RemoveSprite.c
    public void RemoveSprite(SpriteRoot sprite) { throw new System.NotImplementedException(); }

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

    // RVA: 0x1576A18  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/GetSprite.c
    public SpriteMesh_Managed GetSprite(int i) { throw new System.NotImplementedException(); }

    // RVA: 0x1576A54  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/UpdatePositions.c
    public void UpdatePositions() { throw new System.NotImplementedException(); }

    // RVA: 0x1576A60  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/UpdateUVs.c
    public void UpdateUVs() { throw new System.NotImplementedException(); }

    // RVA: 0x1576A6C  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/UpdateColors.c
    public void UpdateColors() { throw new System.NotImplementedException(); }

    // RVA: 0x1576A78  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/UpdateBounds.c
    public void UpdateBounds() { throw new System.NotImplementedException(); }

    // RVA: 0x1576A84  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/ScheduleBoundsUpdate.c
    public void ScheduleBoundsUpdate(float seconds) { throw new System.NotImplementedException(); }

    // RVA: 0x1576AE8  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/CancelBoundsUpdate.c
    public void CancelBoundsUpdate() { throw new System.NotImplementedException(); }

    // RVA: 0x1576B34  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/get_IsInitialized.c
    public bool get_IsInitialized() { throw new System.NotImplementedException(); }

    // RVA: 0x1576B3C  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/get_ManagedRenderer.c
    public Renderer get_ManagedRenderer() { throw new System.NotImplementedException(); }

    // RVA: 0x1576BE4  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/LateUpdate.c
    public virtual void LateUpdate() { throw new System.NotImplementedException(); }

    // RVA: 0x1576D64  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/DoMirror.c
    public virtual void DoMirror() { throw new System.NotImplementedException(); }

    // RVA: 0x1576F08  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/Update.c
    private void Update() { throw new System.NotImplementedException(); }

    // RVA: 0x1576F14  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/OnDrawGizmos.c
    public virtual void OnDrawGizmos() { throw new System.NotImplementedException(); }

    // RVA: 0x15772FC  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/OnDrawGizmosSelected.c
    public void OnDrawGizmosSelected() { throw new System.NotImplementedException(); }

    // RVA: 0x1576FC8  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/DrawCenter.c
    protected void DrawCenter() { throw new System.NotImplementedException(); }

    // RVA: 0x15773A8  Ghidra: work/06_ghidra/decompiled_full/SpriteManager/.ctor.c
    public SpriteManager() { throw new System.NotImplementedException(); }

}
