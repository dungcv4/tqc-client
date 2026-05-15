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
    // 1-1 port. Pseudocode reconstructed from C decompile:
    //   base.OnLoaded(objs);                     // TResource<TextAsset>__OnLoaded sets _isDone=true, data
    //   if (UnityEngine.Object.op_Equality(this.data, null)) return;     // Unity-null short-circuit
    //   if (this.data == null) return;                                    // raw null guard (redundant)
    //   this.magicData = JsonUtility.FromJson<MagicFxData>(this.data.text);   // offset 0x40
    //   if (magicData == null || magicData.fx == null) goto throw_path;       // bail
    //   for (int i = 0; i < magicData.fx.Length; i++) {
    //       int j = 0;
    //       while (true) {
    //           if (magicData == null || magicData.fx == null) goto throw_path;
    //           FxDatas fxDatas = magicData.fx[i];                                  // re-read
    //           if (fxDatas == null || fxDatas.data == null) goto throw_path;
    //           if (j >= fxDatas.data.Count) break;                                  // inner loop exit
    //           var fxLoader = new MagicFxLoader.FxLoader();
    //           if (magicData == null || magicData.fx == null) goto throw_path;
    //           fxDatas = magicData.fx[i]; (re-read)
    //           if (fxDatas == null || fxDatas.data == null) goto throw_path;
    //           FxData fxData = fxDatas.data[j];
    //           if (fxData == null) goto throw_path;
    //           if (!string.IsNullOrEmpty(fxData.fxName)) {
    //               // re-read magicData/fx/fxDatas/data, refetch fxData (defensive)
    //               if (magicData == null || magicData.fx == null) goto throw_path;
    //               fxDatas = magicData.fx[i]; if (fxDatas == null || fxDatas.data == null) goto throw_path;
    //               MagicLoader ml = MagicLoader.Instance;
    //               FxData fxData2 = fxDatas.data[j];
    //               if (fxData2 == null || ml == null) goto throw_path;
    //               fxLoader.resObj = ml.LoadFx(fxData2.fxName);
    //           }
    //           if (this.magicLoader == null || this.magicLoader.fx == null) goto throw_path;
    //           List<MagicFxLoader.FxLoader> list = this.magicLoader.fx[i].data;
    //           if (list == null) goto throw_path;
    //           list.Add(fxLoader);    // inlined as either direct write or AddWithResize
    //           if (magicData == null || magicData.fx == null) goto throw_path;
    //           j++;
    //       }
    //   }
    //   // post-outer-loop:
    //   MagicLoader ml2 = MagicLoader.Instance;
    //   if (ml2 != null && ml2.magicFxPool != null) {
    //       ml2.magicFxPool.Add(this.magicID, this, ml2.magicFxPool.timeDefault);
    //       return;
    //   }
    //   throw_path: throw new NullReferenceException();   // FUN_015cb8fc tail
    protected override void OnLoaded(UnityEngine.Object[] objs)
    {
        base.OnLoaded(objs);
        if (this.data == null)   // Unity Object null check (op_Equality with null)
        {
            return;
        }
        if (this.data == null)   // raw null guard from Ghidra
        {
            return;
        }
        string text = this.data.text;
        this.magicData = UnityEngine.JsonUtility.FromJson<MagicFxData>(text);
        if (this.magicData == null || this.magicData.fx == null)
        {
            throw new System.NullReferenceException();
        }
        for (int i = 0; i < this.magicData.fx.Length; i++)
        {
            int j = 0;
            while (true)
            {
                if (this.magicData == null || this.magicData.fx == null)
                    throw new System.NullReferenceException();
                MagicFxData.FxDatas fxDatas = this.magicData.fx[i];
                if (fxDatas == null || fxDatas.data == null)
                    throw new System.NullReferenceException();
                if (j >= fxDatas.data.Count) break;
                MagicFxLoader.FxLoader fxLoader = new MagicFxLoader.FxLoader();
                if (this.magicData == null || this.magicData.fx == null)
                    throw new System.NullReferenceException();
                fxDatas = this.magicData.fx[i];
                if (fxDatas == null || fxDatas.data == null)
                    throw new System.NullReferenceException();
                MagicFxData.FxData fxData = fxDatas.data[j];
                if (fxData == null) throw new System.NullReferenceException();
                if (!string.IsNullOrEmpty(fxData.fxName))
                {
                    if (this.magicData == null || this.magicData.fx == null)
                        throw new System.NullReferenceException();
                    fxDatas = this.magicData.fx[i];
                    if (fxDatas == null || fxDatas.data == null)
                        throw new System.NullReferenceException();
                    MagicLoader ml = MagicLoader.Instance;
                    MagicFxData.FxData fxData2 = fxDatas.data[j];
                    if (fxData2 == null || ml == null)
                        throw new System.NullReferenceException();
                    fxLoader.resObj = ml.LoadFx(fxData2.fxName);
                }
                if (this.magicLoader == null || this.magicLoader.fx == null)
                    throw new System.NullReferenceException();
                MagicFxLoader.FxLoaders fxLoadersAtI = this.magicLoader.fx[i];
                if (fxLoadersAtI == null || fxLoadersAtI.data == null)
                    throw new System.NullReferenceException();
                fxLoadersAtI.data.Add(fxLoader);
                if (this.magicData == null || this.magicData.fx == null)
                    throw new System.NullReferenceException();
                j++;
            }
        }
        // Post-outer-loop: add to MagicLoader.Instance.magicFxPool
        MagicLoader instance = MagicLoader.Instance;
        if (instance != null && instance.magicFxPool != null)
        {
            instance.magicFxPool.Add(this.magicID, this, instance.magicFxPool.timeDefault);
            return;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxResource/IsLoadFinish.c RVA 0x18F1358
    // 1-1 port. Pseudocode:
    //   var ml = this.magicLoader;                                  // offset 0x48
    //   if (ml == null || ml.fx == null) goto throw_path;
    //   uint i = 0;
    //   bool result = this._isDone;                                  // offset 0x19
    //   while (true) {
    //       if (i >= ml.fx.Length) return result;
    //       int j = 0;
    //       while (true) {
    //           if (i >= ml.fx.Length) bounds_throw;
    //           var fxLoaders = ml.fx[i];                            // array element
    //           if (fxLoaders == null || fxLoaders.data == null) goto throw_path;
    //           if (j >= fxLoaders.data.Count) break;                 // exit inner loop
    //           var fxLoader = fxLoaders.data[j];
    //           if (fxLoader == null) goto throw_path;
    //           var resObj = fxLoader.resObj;                         // offset 0x10
    //           if (resObj != null) {
    //               result &= resObj.IsLoadFinish();                  // virtual slot 0x198 -> override IsLoadFinish
    //           }
    //           ml = this.magicLoader; if (ml == null) goto throw_path;
    //           if (ml.fx == null) goto throw_path;
    //           j++;
    //       }
    //       // re-read ml.fx after inner break
    //       if (ml.fx == null) break;     // outer break -> throw_path
    //       i++;
    //   }
    //   throw_path: throw new NullReferenceException();   // FUN_015cb8fc
    public override bool IsLoadFinish()
    {
        MagicFxLoader ml = this.magicLoader;
        if (ml == null || ml.fx == null)
        {
            throw new System.NullReferenceException();
        }
        bool result = this._isDone;
        int i = 0;
        while (true)
        {
            if (i >= ml.fx.Length) return result;
            int j = 0;
            while (true)
            {
                MagicFxLoader.FxLoaders fxLoaders = ml.fx[i];
                if (fxLoaders == null || fxLoaders.data == null)
                    throw new System.NullReferenceException();
                if (j >= fxLoaders.data.Count) break;
                MagicFxLoader.FxLoader fxLoader = fxLoaders.data[j];
                if (fxLoader == null) throw new System.NullReferenceException();
                FxResource resObj = fxLoader.resObj;
                if (resObj != null)
                {
                    result = result & resObj.IsLoadFinish();
                }
                ml = this.magicLoader;
                if (ml == null) throw new System.NullReferenceException();
                if (ml.fx == null) throw new System.NullReferenceException();
                j++;
            }
            if (ml.fx == null) break;
            i++;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxResource/IsDataReady.c RVA 0x18F151C
    // 1-1 port. Similar to IsLoadFinish but checks resObj.data (offset 0x28) via UnityEngine.Object.op_Inequality(null).
    // Final return: (magicData != null) & bVar9.   // bVar9 is AND of all child checks
    //   var ml = this.magicLoader;
    //   if (ml == null || ml.fx == null) goto throw;
    //   var md = this.magicData;
    //   uint i = 0;
    //   byte allReady = 1;
    //   while (true) {
    //       if (i >= ml.fx.Length) return (md != null) & allReady;
    //       int j = 0;
    //       while (true) {
    //           var fxLoaders = ml.fx[i];
    //           if (fxLoaders == null || fxLoaders.data == null) goto throw;
    //           if (j >= fxLoaders.data.Count) break;
    //           var fxLoader = fxLoaders.data[j];
    //           if (fxLoader == null) goto throw;
    //           if (fxLoader.resObj != null) {
    //               var d = fxLoader.resObj.data;          // FxResource.data (GameObject) at offset 0x28
    //               allReady &= (byte)(UnityEngine.Object.op_Inequality(d, null) ? 1 : 0);
    //           }
    //           ml = this.magicLoader; if (ml == null) goto throw;
    //           if (ml.fx == null) goto throw;
    //           j++;
    //       }
    //       if (ml.fx == null) break;
    //       i++;
    //   }
    //   throw: throw new NullReferenceException();
    public bool IsDataReady()
    {
        MagicFxLoader ml = this.magicLoader;
        if (ml == null || ml.fx == null)
        {
            throw new System.NullReferenceException();
        }
        MagicFxData md = this.magicData;
        bool allReady = true;
        int i = 0;
        while (true)
        {
            if (i >= ml.fx.Length) return (md != null) & allReady;
            int j = 0;
            while (true)
            {
                MagicFxLoader.FxLoaders fxLoaders = ml.fx[i];
                if (fxLoaders == null || fxLoaders.data == null)
                    throw new System.NullReferenceException();
                if (j >= fxLoaders.data.Count) break;
                MagicFxLoader.FxLoader fxLoader = fxLoaders.data[j];
                if (fxLoader == null) throw new System.NullReferenceException();
                if (fxLoader.resObj != null)
                {
                    UnityEngine.GameObject d = fxLoader.resObj.data;
                    allReady = allReady & (d != null);   // Unity Object.op_Inequality with null
                }
                ml = this.magicLoader;
                if (ml == null) throw new System.NullReferenceException();
                if (ml.fx == null) throw new System.NullReferenceException();
                j++;
            }
            if (ml.fx == null) break;
            i++;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxResource/.ctor.c RVA 0x18F166C
    // 1-1 port. Body:
    //   this.magicID = "";                       // offset 0x38 (StringLiteral_0)
    //   this.magicLoader = new MagicFxLoader();  // offset 0x48
    //   base..ctor();                            // TResource<TextAsset>.<ctor>
    public MagicFxResource()
    {
        magicID = "";
        magicLoader = new MagicFxLoader();
        // base()  // implicit in C#
    }
}
