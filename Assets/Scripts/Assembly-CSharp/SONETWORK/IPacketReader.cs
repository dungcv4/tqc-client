// Source: Ghidra-decompiled libil2cpp.so
// RVAs: -1 (interface declaration only — no method bodies)
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.IPacketReader/

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

namespace SONETWORK
{
    // Source: Il2CppDumper-stub  TypeDefIndex: 934
    public interface IPacketReader
    {
        byte[] vBuffer(out int offset, out int size);
        int vRecvLength(int size, ConnectProxy proxy);
    }
}
