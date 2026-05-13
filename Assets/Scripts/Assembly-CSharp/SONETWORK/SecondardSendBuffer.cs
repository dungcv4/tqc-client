// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x197410C, 0x197418C, 0x19742C8, 0x1974360, 0x19743A8, 0x1974234
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/

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
    // Source: Il2CppDumper-stub  TypeDefIndex: 933
    public sealed class SecondardSendBuffer
    {
        private byte[] m_buf;       // 0x10
        private int m_bufSize;      // 0x18
        private int m_rpos;         // 0x1C
        private int m_wpos;         // 0x20
        private int m_maxDataSize;  // 0x24

        // RVA: 0x197410C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/.ctor.c
        // TODO: confidence:low — Ghidra .c not generated for ctor; structure inferred from dump.cs.
        public SecondardSendBuffer(int maxDataSize)
        {
            this.m_maxDataSize = maxDataSize;
            this.m_bufSize = maxDataSize + 2;
            this.m_buf = new byte[this.m_bufSize];
            this.m_rpos = 0;
            this.m_wpos = 0;
        }

        // RVA: 0x197418C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/write.c
        public bool write(byte[] data, int offset, int size)
        {
            if (size <= this.m_maxDataSize)
            {
                if (0 < this.m_rpos)
                {
                    this.refresh();
                }
                if (size + 2 <= this.m_bufSize - this.m_wpos)
                {
                    DataConverter.writeUInt16((ushort)size, this.m_buf, this.m_wpos);
                    System.Array.Copy(data, offset, this.m_buf, this.m_wpos + 2, size);
                    this.m_wpos = this.m_wpos + size + 2;
                    return true;
                }
            }
            return false;
        }

        // RVA: 0x19742C8  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/read.c
        public byte[] read(out int offset, out int len)
        {
            offset = this.m_rpos + 2;
            len = 0;
            if (this.m_wpos - this.m_rpos < 2)
            {
                return null;
            }
            ushort uVar1 = DataConverter.readUInt16(this.m_buf, this.m_rpos);
            len = (int)(uVar1 & 0xffff);
            return this.m_buf;
        }

        // RVA: 0x1974360  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/next.c
        public bool next()
        {
            int iVar1 = this.m_wpos - this.m_rpos;
            if (1 < iVar1)
            {
                ushort uVar2 = DataConverter.readUInt16(this.m_buf, this.m_rpos);
                this.m_rpos = this.m_rpos + (int)(uVar2 & 0xffff) + 2;
            }
            return 1 < iVar1;
        }

        // RVA: 0x19743A8  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/isEmpty.c
        public bool isEmpty()
        {
            return this.m_rpos == this.m_wpos;
        }

        // RVA: 0x1974234  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.SecondardSendBuffer/refresh.c
        private void refresh()
        {
            int iVar1 = this.m_wpos - this.m_rpos;
            if (iVar1 < 1)
            {
                iVar1 = 0;
            }
            else
            {
                System.Array.Copy(this.m_buf, this.m_rpos, this.m_buf, 0, iVar1);
            }
            this.m_rpos = 0;
            this.m_wpos = iVar1;
        }
    }
}
