// Source: work/03_il2cpp_dump/dump.cs class 'Game.Conf.ConfigGeneral' (TypeDefIndex 929)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/Game.Conf.ConfigGeneral/*.c

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

namespace Game.Conf
{
    // Source: Il2CppDumper-stub  TypeDefIndex: 929
    public sealed class ConfigGeneral : ScriptableObject
    {
        private int _targetFrameRate;
        private int _maximumAvailableDiskSpace;
        public string AppName;
        public string BundleVersion;
        public string UnityVersion;
        public string BuildVersionNumber;
        public string BuildTime;
        public string BuildNumber;

        // Source: Ghidra work/06_ghidra/decompiled_full/Game.Conf.ConfigGeneral/get_TargetFrameRate.c RVA 0x1972E8C
        // Body: return _targetFrameRate; (field at +0x18)
        public int TargetFrameRate
        {
            get { return _targetFrameRate; }
            // Source: Ghidra work/06_ghidra/decompiled_full/Game.Conf.ConfigGeneral/set_TargetFrameRate.c RVA 0x1972E94
            set { _targetFrameRate = value; }
        }

        // Source: Ghidra work/06_ghidra/decompiled_full/Game.Conf.ConfigGeneral/get_MaximumAvailableDiskSpace.c RVA 0x1972E9C
        // Body: return _maximumAvailableDiskSpace; (field at +0x1C)
        public int MaximumAvailableDiskSpace
        {
            get { return _maximumAvailableDiskSpace; }
            // Source: Ghidra work/06_ghidra/decompiled_full/Game.Conf.ConfigGeneral/set_MaximumAvailableDiskSpace.c RVA 0x1972EA4
            set { _maximumAvailableDiskSpace = value; }
        }

        // Source: Ghidra work/06_ghidra/decompiled_full/Game.Conf.ConfigGeneral/.ctor.c RVA 0x01972eac
        // Body: empty (only base ScriptableObject.ctor — implicit in C#).
        public ConfigGeneral()
        {
        }
    }
}
