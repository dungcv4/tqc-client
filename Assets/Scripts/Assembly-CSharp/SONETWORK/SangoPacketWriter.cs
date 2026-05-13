// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1974E00, 0x1974E94, 0x1974EA8, 0x1974F0C, 0x1974FF8, 0x1975090, 0x1975240, 0x19752EC
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/

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
    // Source: Il2CppDumper-stub  TypeDefIndex: 939
    internal sealed class SangoPacketWriter : IPacketWriter
    {
        private byte[] m_buf;                                // 0x10
        private int m_sendLen;                               // 0x18
        private ushort m_seqno;                              // 0x1c
        private LinkedList<SecondardSendBuffer> m_overspend; // 0x20

        // RVA: 0x1974E00  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/writeProtocol.c
        private void writeProtocol(byte[] data, int offset, int size)
        {
            // Ghidra layout: local_28:2 = m_npSize, local_26:2 = m_npMsgCount,
            // local_24:2 = m_npCheckSum.  Calls napiPACKET.writeToByteArray.
            napiPACKET local_28 = default(napiPACKET);
            local_28.m_npSize = (ushort)size;
            local_28.m_npMsgCount = this.m_seqno;
            this.m_seqno = (ushort)(this.m_seqno + 1);
            local_28.m_npCheckSum = (ushort)napiPACKET.getBlockCheckSum(data, offset, size);
            local_28.writeToByteArray(this.m_buf, this.m_sendLen);
            int iVar1 = this.m_sendLen + 6;
            this.m_sendLen = iVar1;
            System.Array.Copy(data, offset, this.m_buf, iVar1, size);
            this.m_sendLen = this.m_sendLen + size;
        }

        // RVA: 0x1974E94  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/SONETWORK.IPacketWriter.vBuffer.c
        byte[] IPacketWriter.vBuffer(out int offset, out int size)
        {
            offset = 0;
            size = this.m_sendLen;
            return this.m_buf;
        }

        // RVA: 0x1974EA8  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/SONETWORK.IPacketWriter.vEmpty.c
        bool IPacketWriter.vEmpty()
        {
            if (this.m_overspend == null)
            {
                throw new NullReferenceException();
            }
            bool bVar1;
            if (this.m_overspend.Count < 1)
            {
                bVar1 = this.m_sendLen < 1;
            }
            else
            {
                bVar1 = false;
            }
            return bVar1;
        }

        // RVA: 0x1974F0C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/SONETWORK.IPacketWriter.vSendLength.c
        void IPacketWriter.vSendLength(int size)
        {
            if (size < 1)
            {
                return;
            }
            int iVar1 = this.m_sendLen - size;
            this.m_sendLen = iVar1;
            System.Array.Copy(this.m_buf, size, this.m_buf, 0, iVar1);
            LinkedList<SecondardSendBuffer> list = this.m_overspend;
            while (list != null)
            {
                if (list.Count < 1)
                {
                    return;
                }
                if (list.First == null) break;
                SecondardSendBuffer sbuf = list.First.Value;
                this.writeToMainBuf(sbuf);
                if (sbuf == null) break;
                if (!sbuf.isEmpty())
                {
                    return;
                }
                if (this.m_overspend == null) break;
                this.m_overspend.RemoveFirst();
                list = this.m_overspend;
            }
            // FUN_015cb8fc — il2cpp null-deref trap
            throw new NullReferenceException();
        }

        // RVA: 0x1975090  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/SONETWORK.IPacketWriter.vWrite.c
        int IPacketWriter.vWrite(byte[] data, int offset, int size)
        {
            if (data == null)
            {
                return 0;
            }
            if (size < 1)
            {
                return 0;
            }
            if (65000 < size)
            {
                UnityEngine.Debug.Log("packet too big");
                return unchecked((int)0xffffff9a);
            }
            LinkedList<SecondardSendBuffer> list = this.m_overspend;
            if (list != null)
            {
                if (list.Count < 1)
                {
                    if (size <= 65000 - this.m_sendLen)
                    {
                        this.writeProtocol(data, offset, size);
                        return 0;
                    }
                }
                else
                {
                    LinkedListNode<SecondardSendBuffer> last = list.Last;
                    if (last == null || last.Value == null) goto LAB_TRAP;
                    bool ok = last.Value.write(data, offset, size);
                    if (ok)
                    {
                        return 0;
                    }
                }
                SecondardSendBuffer newBuf = new SecondardSendBuffer(65000);
                if (this.m_overspend != null)
                {
                    this.m_overspend.AddLast(newBuf);
                    if (newBuf != null)
                    {
                        newBuf.write(data, offset, size);
                        return 0;
                    }
                }
            }
        LAB_TRAP:
            // FUN_015cb8fc — il2cpp null-deref trap
            throw new NullReferenceException();
        }

        // RVA: 0x1975240  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/SONETWORK.IPacketWriter.vWriteSendBuf.c
        void IPacketWriter.vWriteSendBuf(SecondardSendBuffer SBuf)
        {
            if (SBuf == null || SBuf.isEmpty())
            {
                return;
            }
            LinkedList<SecondardSendBuffer> list = this.m_overspend;
            if (list != null)
            {
                if (list.Count < 1)
                {
                    this.writeToMainBuf(SBuf);
                    if (SBuf.isEmpty())
                    {
                        return;
                    }
                    list = this.m_overspend;
                    if (list == null) goto LAB_TRAP;
                }
                list.AddLast(SBuf);
                return;
            }
        LAB_TRAP:
            // FUN_015cb8fc — il2cpp null-deref trap
            throw new NullReferenceException();
        }

        // RVA: 0x1974FF8  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/writeToMainBuf.c
        private void writeToMainBuf(SecondardSendBuffer SBuf)
        {
            // TODO: confidence:medium — Ghidra accesses SBuf private fields m_rpos (0x1c)
            //   and m_wpos (0x20) directly. We translate via the public read()/next() API
            //   which preserves byte-level semantics: read() returns the buffer + offset
            //   (rpos+2) + len (uVar1 read from the 2-byte slot header). Loop continues
            //   while !isEmpty() (i.e. rpos != wpos).
            if (SBuf == null)
            {
                // FUN_015cb8fc — il2cpp null-deref trap
                throw new NullReferenceException();
            }
            while (!SBuf.isEmpty())
            {
                int outOffset;
                int outLen;
                byte[] sbufData = SBuf.read(out outOffset, out outLen);
                if (sbufData == null)
                {
                    return;
                }
                if (outLen < 0xfde9)
                {
                    if (65000 - this.m_sendLen < outLen)
                    {
                        return;
                    }
                    this.writeProtocol(sbufData, outOffset, outLen);
                }
                SBuf.next();
            }
        }

        // RVA: 0x19752EC  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SangoPacketWriter/.ctor.c
        public SangoPacketWriter()
        {
            this.m_buf = new byte[0xfdee];
            this.m_seqno = 1;
            this.m_overspend = new LinkedList<SecondardSendBuffer>();
        }
    }
}
