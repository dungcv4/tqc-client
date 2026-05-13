// Source: dump.cs class 'AudioClipCache' (TypeDefIndex: 143)
// Source: Ghidra work/06_ghidra/decompiled_full/AudioClipCache/.ctor.c RVA 0x017BE21C

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

public sealed class AudioClipCache
{
    public AssetBundleOP BundleOP;
    public AudioClip AudioClip;

    // Source: Ghidra work/06_ghidra/decompiled_full/AudioClipCache/.ctor.c RVA 0x017BE21C
    // 1-1: System.Object.ctor(); BundleOP = _BundleOP (+0x10); AudioClip = _AudioClip (+0x18).
    public AudioClipCache(AssetBundleOP _BundleOP, AudioClip _AudioClip)
    {
        BundleOP = _BundleOP;
        AudioClip = _AudioClip;
    }

}
