// Source: work/03_il2cpp_dump/dump.cs class 'MagicFxData' (TypeDefIndex 760)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/MagicFxData{,.FxData,.FxDatas}/*.c

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

// Source: Il2CppDumper-stub  TypeDefIndex: 760
[Serializable]
public class MagicFxData
{
    public string id;
    public int[] doAction;
    public MagicFxData.FxDatas[] fx;
    public float[] editorDelay;
    public bool NotRepeating;
    public float hitDelay;
    public float segmentDelay;

    // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxData/.ctor.c RVA 0x18F0324
    // Body: id="", doAction = new int[3]{-1,-1,-1}, fx = new FxDatas[3]{new(),new(),new()},
    //       editorDelay = new float[3]{0,0,0}. Loop iterates 8..0xb (3 entries).
    public MagicFxData()
    {
        id = string.Empty;
        doAction = new int[3] { -1, -1, -1 };
        fx = new FxDatas[3];
        editorDelay = new float[3];
        for (int i = 0; i < 3; i++)
        {
            fx[i] = new FxDatas();
        }
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 757
    [Serializable]
    public class FxData
    {
        public string fxName;
        public string soundName;
        public string soundName2;
        public int mode;
        public int part;
        public Vector3 offset;
        public int part2;
        public Vector3 offset2;
        public int faceMode;
        public float rotationY;
        public float timeDelay;
        public float speedDelay;
        public int timeMode;
        public float timeTotal;
        public int doAction;
        public float doActionTime;
        public bool enableCameraShake;
        public int cameraShakeNum;
        public Vector3 cameraShakePos;
        public float cameraShakeFrequency;
        public float cameraShakeDecrease;
        public bool cameraShakeEveryOne;
        public float cameraShakeDistanceX;
        public bool enableCameraFOV;
        public float cameraFOVTime;
        public float cameraFOVValue;
        public float cameraFOVFadeInTime;
        public float cameraFOVFadeOutTime;

        // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxData.FxData/.ctor.c RVA 0x18F0564
        // Body initializes:
        //   fxName = soundName = soundName2 = ""    (offsets 0x10, 0x18, 0x20)
        //   offset = Vector3.zero                    (0x30..0x3C)
        //   offset2 = Vector3.zero                   (0x40..0x4C)
        //   timeMode = -1                            (0x64)
        //   cameraShakePos = Vector3.zero            (0x74..0x7C)
        //   cameraShakeDistanceX = 60.0f             (0x80, IEEE 0x42700000)
        public FxData()
        {
            fxName = string.Empty;
            soundName = string.Empty;
            soundName2 = string.Empty;
            offset = Vector3.zero;
            offset2 = Vector3.zero;
            timeMode = -1;
            cameraShakePos = Vector3.zero;
            cameraShakeDistanceX = 60f;
        }
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 758
    [Serializable]
    public class FxDatas
    {
        public List<MagicFxData.FxData> data;
        public float timeTotal;

        // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxData.FxDatas/ResetTimeTotal.c RVA 0x18F0654
        // 1-1:
        //   timeTotal = 0;
        //   for i in 0..data.Count:
        //     var fx = data[i];
        //     int quot = floor(fx.timeDelay / 100.0);
        //     float baseT = fx.timeDelay + (quot * -100.0) + (quot / 10.0);
        //     if (fx.timeMode == 0) baseT += fx.timeTotal;
        //     else if (fx.timeMode == 2) baseT += fmodf(fx.timeTotal, DAT_0091c174);
        //     if (timeTotal < baseT) timeTotal = baseT;
        public void ResetTimeTotal()
        {
            timeTotal = 0f;
            if (data == null) return;
            const float CYCLE = 6.2831855f;  // DAT_0091c174 — value verified via libil2cpp.so RDATA
            for (int i = 0; i < data.Count; i++)
            {
                FxData fx = data[i];
                if (fx == null) throw new NullReferenceException();
                float quot = fx.timeDelay / 100f;
                int iQuot = float.IsInfinity(quot) ? int.MinValue : (int)quot;
                float baseT = fx.timeDelay + ((float)iQuot * -100f) + ((float)iQuot / 10f);
                if (fx.timeMode == 0)
                {
                    baseT += fx.timeTotal;
                }
                else if (fx.timeMode == 2)
                {
                    baseT += (float)Math.IEEERemainder(fx.timeTotal, CYCLE);
                    // Ghidra uses fmodf — see C# Math.IEEERemainder for periodic remainder.
                }
                if (timeTotal < baseT)
                {
                    timeTotal = baseT;
                }
            }
        }

        // Source: Ghidra work/06_ghidra/decompiled_full/MagicFxData.FxDatas/.ctor.c RVA 0x18F04DC
        // Body: data = new List<FxData>();
        public FxDatas()
        {
            data = new List<FxData>();
        }
    }
}
