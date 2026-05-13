// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x19749C8, 0x19749E8, 0x1974B28, 0x1974D78
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketReader/

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
    // Source: Il2CppDumper-stub  TypeDefIndex: 938
    public sealed class SangoPacketReader : IPacketReader
    {
        private byte[] m_buf;        // 0x10
        private int m_pos;           // 0x18
        private ushort m_seqno;      // 0x1c
        private byte[] m_protocBuf;  // 0x20

        // RVA: 0x19749C8  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketReader/SONETWORK.IPacketReader.vBuffer.c
        byte[] IPacketReader.vBuffer(out int offset, out int size)
        {
            offset = this.m_pos;
            size = 0xfdee - this.m_pos;
            return this.m_buf;
        }

        // RVA: 0x19749E8  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketReader/SONETWORK.IPacketReader.vRecvLength.c
        int IPacketReader.vRecvLength(int size, ConnectProxy proxy)
        {
            // Ghidra reads napiPACKET fields directly: local_48 covers (m_npSize:2,
            // m_npMsgCount:2), local_44 is m_npCheckSum:2.  Translated as napiPACKET struct.
            napiPACKET local_48 = default(napiPACKET);

            size = this.m_pos + size;
            this.m_pos = size;
            int iVar4 = 0;
            int iVar1 = 0;
            if (6 < size)
            {
                do
                {
                    local_48.readFromByteArray(this.m_buf, iVar4);
                    if (65000 < (ushort)local_48.m_npSize)
                    {
                        return unchecked((int)0xffffff96);
                    }
                    uint uVar5 = (uint)local_48.m_npSize & 0xffff;
                    iVar1 = size - (int)(uVar5 + 6);
                    if (size < (int)(uVar5 + 6)) break;
                    if ((short)local_48.m_npMsgCount != (short)this.m_seqno)
                    {
                        return unchecked((int)0xffffff95);
                    }
                    iVar4 = iVar4 + 6;
                    this.m_seqno = (ushort)((short)local_48.m_npMsgCount + 1);
                    short sVar2 = (short)napiPACKET.getBlockCheckSum(this.m_buf, iVar4, (int)uVar5);
                    if ((short)local_48.m_npCheckSum != sVar2)
                    {
                        return unchecked((int)0xffffff94);
                    }
                    int rc = this.processPackage(proxy, this.m_buf, iVar4, (int)uVar5);
                    if (rc != 0)
                    {
                        return rc;
                    }
                    iVar4 = iVar4 + (int)uVar5;
                    size = iVar1;
                } while (6 < iVar1);
                if (size != this.m_pos)
                {
                    this.m_pos = size;
                    if (0 < size)
                    {
                        System.Array.Copy(this.m_buf, iVar4, this.m_buf, 0, size);
                    }
                }
            }
            return 0;
        }

        // RVA: 0x1974B28  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketReader/processPackage.c
        // 1-1 (BUGFIX 2026-05-13): Ghidra reads `local_58._2_2_` (offset 2 = m_pcSize), NOT
        // `local_58._0_2_` (offset 0 = m_pcProtoco) as the previous port did. The size mismatch
        // check `m_pcSize + 6 != packet_size` was always failing (e.g. proto=2003 → 2009 != 104)
        // so inbound packets like CHECKACCRESULT got rejected with 0xffffff9a, breaking login.
        private int processPackage(ConnectProxy proxy, byte[] data, int offset, int size)
        {
            // Ghidra layout: local_58 holds proto_COMM struct read via readFromByteArray.
            // local_58._2_2_ (offset 2, 2 bytes) = m_pcSize (uncompressed payload bytes)
            // local_54 = m_pcCompressSize (offset 4)
            // local_44 = out crc (separate stack var passed as `out`)
            proto_COMM local_58 = default(proto_COMM);
            ushort local_44 = 0;

            if (size < 6)
            {
                return unchecked((int)0xffffff99);
            }
            local_58.readFromByteArray(out local_44, data, offset, false);
            ushort uVar2 = (ushort)local_58.m_pcSize;           // local_58._2_2_ (offset 2)
            ushort uVar3 = (ushort)local_58.m_pcCompressSize;   // local_54 (offset 4)
            int uVar6;
            if (uVar2 < 0xfde3)
            {
                uint uVar8 = (uint)uVar2;
                if (uVar3 == 0)
                {
                    if (uVar2 + 6 != size)
                    {
                        UnityEngine.Debug.Log("packet size mismatch (uncompressed)");
                        return unchecked((int)0xffffff9a);
                    }
                    if (this.m_protocBuf == null)
                    {
                        throw new NullReferenceException();
                    }
                    System.Array.Clear(this.m_protocBuf, 0, this.m_protocBuf.Length);
                    System.Array.Copy(data, offset + 6, this.m_protocBuf, 0, uVar2);
                    proto_COMM.blockEncrypt(this.m_protocBuf, 0, uVar2);
                }
                else
                {
                    if (uVar3 + 6 != size)
                    {
                        UnityEngine.Debug.Log("packet size mismatch (compressed)");
                        return unchecked((int)0xffffff9a);
                    }
                    if (this.m_protocBuf == null)
                    {
                        throw new NullReferenceException();
                    }
                    System.Array.Clear(this.m_protocBuf, 0, this.m_protocBuf.Length);
                    if (this.m_protocBuf == null)
                    {
                        throw new NullReferenceException();
                    }
                    uint uVar4 = (uint)Zip.UncompressMemoryToMemoryZIP(this.m_protocBuf, 0, this.m_protocBuf.Length, data, offset + 6, uVar3);
                    if (uVar4 != uVar8)
                    {
                        return unchecked((int)0xffffff92);
                    }
                }
                if (proxy == null)
                {
                    throw new NullReferenceException();
                }
                uVar6 = proxy.onStream(ref local_58, this.m_protocBuf);
            }
            else
            {
                UnityEngine.Debug.Log("packet protoco out of range");
                uVar6 = unchecked((int)0xffffff9a);
            }
            return uVar6;
        }

        // RVA: 0x1974D78  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketReader/.ctor.c
        public SangoPacketReader()
        {
            this.m_buf = new byte[0xfdee];
            this.m_seqno = 1;
            this.m_protocBuf = new byte[65000];
        }
    }
}
