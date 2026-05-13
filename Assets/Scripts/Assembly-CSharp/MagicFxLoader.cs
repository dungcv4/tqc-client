// Source: work/03_il2cpp_dump/dump.cs class 'MagicFxLoader' (TypeDefIndex 765)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/MagicFxLoader{,.FxLoader,.FxLoaders}/.ctor.c

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

// Source: Il2CppDumper-stub  TypeDefIndex: 765
[Serializable]
public class MagicFxLoader
{
    public MagicFxLoader.FxLoaders[] fx;

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxLoader/.ctor.c RVA 0x18F0CEC
    // Body: fx = new FxLoaders[3]; loop populates fx[0..2] with new FxLoaders().
    public MagicFxLoader()
    {
        fx = new FxLoaders[3];
        for (int i = 0; i < 3; i++)
        {
            fx[i] = new FxLoaders();
        }
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 763
    [Serializable]
    public class FxLoader
    {
        public FxResource resObj;

        // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxLoader.FxLoader/.ctor.c RVA 0x18F0E7C
        // Body: empty (only base System_Object___ctor).
        public FxLoader()
        {
        }
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 764
    [Serializable]
    public class FxLoaders
    {
        public List<MagicFxLoader.FxLoader> data;

        // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxLoader.FxLoaders/.ctor.c RVA 0x18F0DF4
        // Body: data = new List<FxLoader>();
        public FxLoaders()
        {
            data = new List<FxLoader>();
        }
    }
}
