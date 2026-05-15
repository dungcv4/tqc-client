// Source: Ghidra work/06_ghidra/decompiled_full/MagicLoader/ (5 .c)
// Source: dump.cs TypeDefIndex 769
// Field offsets: instance@static0, magicFxPool@0x20, magicFxMgr@0x28, magicFxResourceCount@0x30,
//                fxPool@0x38, fxMgr@0x40, fxResourceCount@0x48,
//                soundPool@0x50, soundMgr@0x58, soundResourceCount@0x60.

using UnityEngine;

public class MagicLoader : MonoBehaviour
{
    private static MagicLoader instance;
    public MagicLoader.MagicFxResourcePool<string> magicFxPool;   // 0x20
    public TResourceManager<string, MagicFxResource> magicFxMgr;  // 0x28
    public int magicFxResourceCount;                              // 0x30
    public MagicLoader.FxResourcePool<string> fxPool;             // 0x38
    public TResourceManager<string, FxResource> fxMgr;            // 0x40
    public int fxResourceCount;                                   // 0x48
    public TResObjectPool<string, SoundResource> soundPool;       // 0x50
    public TResourceManager<string, SoundResource> soundMgr;      // 0x58
    public int soundResourceCount;                                // 0x60

    // Source: Ghidra get_Instance.c  RVA 0x18F1714 — returns static instance field.
    public static MagicLoader Instance { get { return instance; } }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicLoader/.ctor.c RVA 0x18F175C
    // 1-1: 3 (pool, mgr) pairs constructed with (size_max=-1, time_default=60f);
    //   magicFxPool/magicFxMgr (+0x20/+0x28), fxPool/fxMgr (+0x38/+0x40), soundPool/soundMgr (+0x50/+0x58).
    //   MonoBehaviour.ctor(); instance = this (static at PTR_DAT_03462168 +0xB8).
    private MagicLoader()
    {
        magicFxPool = new MagicFxResourcePool<string>(-1, 60f);
        magicFxMgr = new TResourceManager<string, MagicFxResource>();
        fxPool = new FxResourcePool<string>(-1, 60f);
        fxMgr = new TResourceManager<string, FxResource>();
        soundPool = new TResObjectPool<string, SoundResource>(-1, 60f);
        soundMgr = new TResourceManager<string, SoundResource>();
        instance = this;
    }

    // Source: Ghidra Update.c  RVA 0x18F19D0
    // Updates 3 (mgr, pool) pairs and aggregates resourceCount = mgr.Update() + pool.Update(deltaTime).
    private void Update()
    {
        if (magicFxMgr == null) throw new System.NullReferenceException();
        int v1 = magicFxMgr.Update();
        float dt = Time.deltaTime;
        if (magicFxPool == null) throw new System.NullReferenceException();
        int v2 = magicFxPool.Update(dt);
        magicFxResourceCount = v1 + v2;

        if (fxMgr == null) throw new System.NullReferenceException();
        int v3 = fxMgr.Update();
        if (fxPool == null) throw new System.NullReferenceException();
        int v4 = fxPool.Update(Time.deltaTime);
        fxResourceCount = v3 + v4;

        if (soundMgr == null) throw new System.NullReferenceException();
        int v5 = soundMgr.Update();
        if (soundPool == null) throw new System.NullReferenceException();
        int v6 = soundPool.Update(Time.deltaTime);
        soundResourceCount = v5 + v6;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicLoader/LoadMagicFx.c RVA 0x18F1AF8
    // 1. If id == "" (StringLiteral_0_034465a0 is empty string): return null.
    // 2. found = magicFxPool.Find(id, 1f, true) — note: pool Find with time=1f.
    //    (Ghidra calls MagicLoader_MagicFxResourcePool<object>__Find with arg "1" as moveTop/time.)
    // 3. If found != null: return found.
    // 4. existing = magicFxMgr.Find(id) — virtual slot 0x188 on TResourceManager.
    // 5. If existing != null: return existing.
    // 6. res = new MagicFxResource(); res.Init(id); magicFxMgr.Add(id, res); return res.
    //    (Virtual slot 0x198 on TResourceManager = Add(K,R).)
    public MagicFxResource LoadMagicFx(string id)
    {
        if (id == "")
        {
            return null;
        }
        if (magicFxPool == null)
        {
            throw new System.NullReferenceException();
        }
        MagicFxResource found = magicFxPool.Find(id, 1f, true);
        if (found != null)
        {
            return found;
        }
        if (magicFxMgr == null)
        {
            throw new System.NullReferenceException();
        }
        MagicFxResource existing = magicFxMgr.Find(id);
        if (existing != null)
        {
            return existing;
        }
        MagicFxResource res = new MagicFxResource();
        res.Init(id);
        if (magicFxMgr != null)
        {
            magicFxMgr.Add(id, res);
            return res;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicLoader/LoadFx.c RVA 0x18F1248
    // 1. If string.IsNullOrEmpty(id): return null.
    // 2. found = fxPool.Find(id, 1f, true) — TResObjectPool<object,object>.Find with time=1.
    // 3. If found != null: return found.
    // 4. existing = fxMgr.Find(id) — virtual slot 0x188.
    // 5. If existing != null: return existing.
    // 6. res = new FxResource(); res.name = id; res.assetType = 9 (MAGIC_FX);
    //    res.Load() (virtual 0x178); fxMgr.Add(id, res) (virtual 0x198); return res.
    public FxResource LoadFx(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }
        if (fxPool == null)
        {
            throw new System.NullReferenceException();
        }
        FxResource found = fxPool.Find(id, 1f, true);
        if (found != null)
        {
            return found;
        }
        if (fxMgr == null)
        {
            throw new System.NullReferenceException();
        }
        FxResource existing = fxMgr.Find(id);
        if (existing != null)
        {
            return existing;
        }
        FxResource res = new FxResource();
        res.name = id;
        res.assetType = (ResourcesLoader.AssetType)9;  // MAGIC_FX
        res.Load();
        if (fxMgr != null)
        {
            fxMgr.Add(id, res);
            return res;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra Clear.c  RVA 0x18F1C0C
    // Clears all 3 (pool, mgr) pairs. Pool.Clear(isdestroy=true). Manager virtual call at vtable +0x1B8
    // (likely TResourceManager.Clear or similar). Set resourceCount = 0.
    public void Clear()
    {
        if (magicFxPool == null) throw new System.NullReferenceException();
        magicFxPool.Clear(true);
        if (magicFxMgr == null) throw new System.NullReferenceException();
        magicFxMgr.Clear();
        magicFxResourceCount = 0;

        if (fxPool == null) throw new System.NullReferenceException();
        fxPool.Clear(true);
        if (fxMgr == null) throw new System.NullReferenceException();
        fxMgr.Clear();
        fxResourceCount = 0;

        if (soundPool == null) throw new System.NullReferenceException();
        soundPool.Clear(true);
        if (soundMgr == null) throw new System.NullReferenceException();
        soundMgr.Clear();
        soundResourceCount = 0;
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 767
    public class FxResourcePool<K> : TResObjectPool<K, FxResource>
    {
        // Source: Ghidra dump_by_rva → MagicLoader_FxResourcePool_o__DestroyObject.c RVA 0x2067FA0
        // 1-1: if resObject != null && resObject.value != null:
        //   UnityEngine.Object.Destroy(resObject.value.data);  // FxResource.data (TResource<GameObject>.data @ 0x28)
        protected override void DestroyObject(TResObject<K, FxResource> resObject)
        {
            if (resObject == null || resObject.value == null) throw new System.NullReferenceException();
            UnityEngine.Object.Destroy(resObject.value.data);
        }

        // Source: dump.cs — passes args to base. ctor body inferred.
        public FxResourcePool(int size_max = -1, float time_default = -1)
        {
            this._sizeMax = size_max;
            this.timeDefault = time_default;
        }
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 768
    public class MagicFxResourcePool<K> : TResObjectPool<K, MagicFxResource>
    {
        // Source: Ghidra work/06_ghidra/decompiled_rva/MagicLoader_MagicFxResourcePool_o__UpdateObject.c RVA 0x229BACC
        // 1-1 port. Iterates resObject.value.magicData.fx[i].data[j] and pings fxPool/soundPool with
        // each fxData's fxName/soundName so the pool LRU clocks refresh.
        // Calling convention note: Ghidra emits TResObjectPool<...>__Find(timeArg_in_float_reg, this, key, moveTop, methodInfo);
        //   * float reg arg = fxPool.timeDefault (raw uint32 at offset 0x1c of pool)
        //   * moveTop arg = 0 (false)
        // So C# call site uses: pool.Find(key, pool.timeDefault, false).
        // Field offsets traced from dump.cs:
        //   resObject @ TResObject<K,MagicFxResource>: value @ 0x18
        //   MagicFxResource: magicData @ 0x40
        //   MagicFxData: fx (FxDatas[]) @ 0x20
        //   FxDatas: data (List<FxData>) @ 0x10
        //   FxData: fxName @ 0x10, soundName @ 0x18
        //   MagicLoader: fxPool @ 0x38, soundPool @ 0x50
        protected override void UpdateObject(TResObject<K, MagicFxResource> resObject, float elapsedTime)
        {
            if (resObject == null) throw new System.NullReferenceException();
            MagicFxResource value = resObject.value;
            if (value == null) throw new System.NullReferenceException();
            int i = 0;
            while (value.magicData != null && value.magicData.fx != null)
            {
                if (i >= value.magicData.fx.Length) return;
                int j = 0;
                while (true)
                {
                    if (value.magicData == null || value.magicData.fx == null)
                        throw new System.NullReferenceException();
                    MagicFxData.FxDatas fxDatas = value.magicData.fx[i];
                    if (fxDatas == null || fxDatas.data == null)
                        throw new System.NullReferenceException();
                    if (j >= fxDatas.data.Count) break;
                    MagicFxData.FxData fxData = fxDatas.data[j];
                    MagicLoader ml = MagicLoader.Instance;
                    if (ml == null || fxData == null || ml.fxPool == null)
                        throw new System.NullReferenceException();
                    ml.fxPool.Find(fxData.fxName, ml.fxPool.timeDefault, false);
                    if (MagicLoader.Instance == null || MagicLoader.Instance.soundPool == null)
                        throw new System.NullReferenceException();
                    MagicLoader.Instance.soundPool.Find(fxData.soundName, MagicLoader.Instance.soundPool.timeDefault, false);
                    value = resObject.value;
                    j++;
                    if (value == null) throw new System.NullReferenceException();
                }
                i++;
                if (value == null) break;
            }
            throw new System.NullReferenceException();
        }

        public MagicFxResourcePool(int size_max = -1, float time_default = -1)
        {
            this._sizeMax = size_max;
            this.timeDefault = time_default;
        }

        // Source: Ghidra dump_by_rva → MagicLoader_MagicFxResourcePool_o__Find.c RVA 0x229BCAC
        // 1-1: result = base.Find(key, time, moveTop). If result == null: return null.
        // Then iterate result.value.magicLoader.fxLoaders to mark child FxLoaders. Iteration is opaque
        // without confirmed MagicFxLoader.FxLoaders shape — keep base.Find as the safe return.
        public new MagicFxResource Find(K key, float time = 0, bool moveTop = true)
        {
            MagicFxResource res = base.Find(key, time, moveTop);
            // TODO body RVA 0x229BCAC — nested fxLoaders iteration not safely portable yet.
            return res;
        }
    }
}
