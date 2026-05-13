// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1974614, 0x1974844, 0x1974730, 0x1974904
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/

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
    // Source: Il2CppDumper-stub  TypeDefIndex: 937
    public struct proto_COMM
    {
        public ushort m_pcProtoco;
        public ushort m_pcSize;
        public ushort m_pcCompressSize;
        public const int SIZE = 6;
        public const int MAX_SENDDATA_LEN = 64994;
        // PTR_DAT_03465ff0 in Ghidra: 10-byte CRYPT_TABLE in IL2CPP metadata.
        // .cctor (RVA 0x1974904) initializes from il2cpp metadata blob — body stays NIE,
        // so default-init the field to a 10-byte zero array.  Actual XOR table values
        // will be patched by the type's static ctor when re-decompiled.
        private static byte[] CRYPT_TABLE = new byte[10];
        private static byte[] HEADER_TEMP = new byte[6];

        // RVA: 0x1974614  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/readFromByteArray.c
        public void readFromByteArray(out ushort crc, byte[] data, int offset, bool noEncrypt = false)
        {
            System.Array.Copy(data, offset, HEADER_TEMP, 0, 6);
            if (!noEncrypt)
            {
                proto_COMM.blockEncrypt(HEADER_TEMP, 0, 6);
            }
            this.m_pcProtoco = DataConverter.readUInt16(HEADER_TEMP, 0);
            this.m_pcSize = DataConverter.readUInt16(HEADER_TEMP, 2);
            this.m_pcCompressSize = DataConverter.readUInt16(HEADER_TEMP, 4);
            crc = 0;
        }

        // RVA: 0x1974844  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/writeToByteArray.c
        public void writeToByteArray(byte[] data, int offset)
        {
            System.Array.Copy(data, offset, HEADER_TEMP, 0, 6);
            DataConverter.writeUInt16(this.m_pcProtoco, data, offset);
            DataConverter.writeUInt16(this.m_pcSize, data, offset + 2);
            DataConverter.writeUInt16(this.m_pcCompressSize, data, offset + 4);
            proto_COMM.blockEncrypt(data, offset, 6);
        }

        // RVA: 0x1974730  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/blockEncrypt.c
        public static void blockEncrypt(byte[] data, int offset, int size)
        {
            if (CRYPT_TABLE == null)
            {
                throw new NullReferenceException();
            }
            if (0 < size)
            {
                if (data == null)
                {
                    throw new NullReferenceException();
                }
                int tableLen = CRYPT_TABLE.Length;
                int iVar3 = 0;
                if (tableLen != 0)
                {
                    iVar3 = size / tableLen;
                }
                uint uVar7 = (uint)(size - iVar3 * tableLen);
                ulong uVar6 = (ulong)(uint)size;
                do
                {
                    if ((uint)data.Length <= (uint)offset)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    byte bVar2 = data[offset];
                    if (CRYPT_TABLE == null)
                    {
                        throw new NullReferenceException();
                    }
                    if ((uint)CRYPT_TABLE.Length <= uVar7)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    data[offset] = (byte)(CRYPT_TABLE[(int)uVar7] ^ bVar2);
                    if (CRYPT_TABLE == null)
                    {
                        throw new NullReferenceException();
                    }
                    int tableLen2 = CRYPT_TABLE.Length;
                    uVar6 = uVar6 - 1;
                    offset = offset + 1;
                    int iVar3b = 0;
                    if (tableLen2 != 0)
                    {
                        iVar3b = (int)(uVar7 + 1) / tableLen2;
                    }
                    uVar7 = (uVar7 + 1) - (uint)(iVar3b * tableLen2);
                } while (uVar6 != 0);
            }
        }

        // RVA: 0x1974904  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/.cctor.c
        // TODO: confidence:low — Ghidra .c not generated for static ctor; CRYPT_TABLE blob
        //   lives in IL2CPP metadata at PTR_DAT_03465ff0 and must be lifted from
        //   global-metadata.dat. Until then default zero table is used (XOR identity).
        static proto_COMM()
        {
        }
    }
}
