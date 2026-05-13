// Source: Ghidra work/06_ghidra/decompiled_full/SoundResource/OnLoaded.c RVA 0x18F0B8C — ported 1-1.
// Source: dump.cs TypeDefIndex 762 — SoundResource : TResource<AudioClip>
// Field layout (inherited): name@0x10 (ResourceBase), callback@0x20, data@0x28 (AudioClip), assetType@0x30.

using System;
using UnityEngine;

[Serializable]
public class SoundResource : TResource<AudioClip>
{
    // Source: Ghidra OnLoaded.c RVA 0x18F0B8C
    // 1. base.OnLoaded(objs) (Ghidra: TResource<object>__OnLoaded(this, objs, AudioClip-type))
    // 2. If this.data != null:
    //    var inst = MagicLoader.Instance;
    //    if (inst != null && inst.fxPool != null && inst.soundPool != null)
    //        inst.soundPool.Add(this.name, this, inst.fxPool.timeDefault);
    // Note: Ghidra uses fxPool.timeDefault (offset 0x38+0x1c) as the time arg when adding to soundPool
    // (offset 0x50). Faithful to binary — appears to be intentional cross-pool timing constant.
    protected override void OnLoaded(UnityEngine.Object[] objs)
    {
        base.OnLoaded(objs);
        if (this.data != null)
        {
            MagicLoader inst = MagicLoader.Instance;
            if (inst != null && inst.fxPool != null && inst.soundPool != null)
            {
                inst.soundPool.Add(this.name, this, inst.fxPool.timeDefault);
            }
        }
    }

    // RVA: 0x18F0CA4 — default ctor (no .ctor.c in decompiled_full/).
    public SoundResource() { }
}
