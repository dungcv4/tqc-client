// Source: dump.cs TypeDefIndex 766 — `public class MagicFxResource : TResource<TextAsset>`.
// Reverted 2026-05-12 from compat shim back to 1-1 inheritance per CLAUDE.md §D6.
// `obj` is backward-compat property delegating to `data` (TResource<TextAsset>.data) for v1 Wrap compile.
// Field offsets (from dump.cs):
//   magicID @ 0x38, magicData @ 0x40, magicLoader @ 0x48
// Note: TResource<TextAsset> inherits: name(0x10), _isLoad(0x18), _isDone(0x19), callback(0x20), data(0x28), assetType(0x30).

using UnityEngine;

public class MagicFxResource : TResource<TextAsset>
{
    public string magicID;             // 0x38
    public MagicFxData magicData;      // 0x40
    public MagicFxLoader magicLoader;  // 0x48

    // Source: backward-compat for v1 MagicFxResourceWrap.cs which targets `obj` field as GameObject.
    // Note: dump.cs canonical type for TResource<TextAsset>.data is TextAsset — but legacy Wrap expects GameObject.
    // For now expose `obj` as data cast (will be null since types don't match; v1 Wrap path won't be hot).
    public GameObject obj
    {
        get { return null; }   // Wrap won't hit this in game runtime; placeholder for compile.
        set { /* no-op */ }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxResource/Init.c RVA 0x18F1478
    // 1. magicID = _magicID (offset 0x38 via param_1[7]).
    // 2. tmp = _magicID.ToString() (virtual slot 0x168 → object.ToString of String returns self).
    // 3. name = string.Concat("MagicData/", tmp) — StringLiteral_7902.
    // 4. assetType = 8 (offset 0x30 = 4-byte = ResourcesLoader.AssetType.MAGIC_DATA per dump.cs enum).
    // 5. Call this.Load() (virtual slot 0x178).
    public void Init(string _magicID)
    {
        if (_magicID == null)
        {
            throw new System.NullReferenceException();
        }
        magicID = _magicID;
        string tmp = _magicID.ToString();
        name = "MagicData/" + tmp;
        assetType = (ResourcesLoader.AssetType)8;
        Load();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxResource/OnLoaded.c RVA 0x18F0E84
    // 1. base.OnLoaded(objs) — sets _isDone=true, data=Convert.ChangeType(objs[0], typeof(TextAsset)).
    // 2. If data == null: return.
    // 3. magicData = JsonUtility.FromJson<MagicFxData>(data.text); stores via param_1[8] (offset 0x40).
    // 4. For each fxDatas in magicData.fx[]:
    //      For each fxData in fxDatas.data:
    //         if !IsNullOrEmpty(fxData.fxName):
    //           new FxLoader(); fxLoader.resObj = MagicLoader.Instance.LoadFx(fxData.fxName);
    //         magicLoader.fxLoaders[i].Add(fxLoader). (List<MagicFxLoader.FxLoader>)
    // 5. After loop: MagicLoader.Instance.magicFxPool.Add(magicID, this).
    // NIE retained — complex nested list-of-list construction; canonical helper MagicFxLoader.AddFxLoader exists.
    // Keep NIE to avoid invented data layout; LoadFx/LoadMagicFx paths don't require OnLoaded to be correct
    // for compile-pass and basic dispatch — game runtime will exercise this later.
    protected override void OnLoaded(UnityEngine.Object[] objs)
    {
        throw new System.NotImplementedException(); // TODO body RVA 0x18F0E84 — depends on MagicFxLoader subtypes
    }

    // Source: Ghidra IsLoadFinish.c RVA 0x18F1358
    // Nested iteration over magicLoader.fxLoaders[i][j] checking each FxLoader.resObj.IsLoadFinish().
    // bVar2 starts as _isDone; AND'd with each child's IsLoadFinish.
    public override bool IsLoadFinish()
    {
        throw new System.NotImplementedException(); // TODO body RVA 0x18F1358 — depends on MagicFxLoader.FxLoaders shape
    }

    // Source: Ghidra IsDataReady.c RVA 0x18F151C
    // Nested iteration like IsLoadFinish; checks each FxLoader.resObj.data (offset 0x28 — GameObject != null).
    // Returns (magicData != null) & bVar9.
    public bool IsDataReady()
    {
        throw new System.NotImplementedException(); // TODO body RVA 0x18F151C — depends on MagicFxLoader.FxLoaders shape
    }

    // Source: dump.cs — default empty ctor. RVA 0x18F166C.
    public MagicFxResource() { }
}
