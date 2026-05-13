// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18FC990, 0x18FCA10, 0x18FCA28, 0x18FCA40, 0x18FCAEC, 0x18FCBCC, 0x18FCBE4,
//       0x18FCBB4, 0x18FCBFC, 0x18FCC14, 0x18FCC2C, 0x18FCC44, 0x18FCC5C
// Ghidra dir: work/06_ghidra/decompiled_full/NetRequestPackerBase/

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

// Source: Il2CppDumper-decompiled  TypeDefIndex: 803
public class NetRequestPackerBase
{
    private SToBA T;                 // 0x10
    private int _Protocol;           // 0x18

    // RVA: 0x18FC990  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/.ctor.c
    public NetRequestPackerBase(int nProtocol)
    {
        this._Protocol = -1;
        this._Protocol = nProtocol;
        this.T = new SToBA();
    }

    // RVA: 0x18FCA10  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeByte.c
    public void writeByte(byte param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeByte(param);
    }

    // RVA: 0x18FCA28  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeBytes.c
    public void writeBytes(byte[] param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeBytes(param);
    }

    // RVA: 0x18FCA40  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeString.c
    public void writeString(string param, int len)
    {
        byte[] buf = new byte[len];
        Encoding utf8 = Encoding.UTF8;
        if (utf8 == null)
        {
            throw new NullReferenceException();
        }
        byte[] strBytes = utf8.GetBytes(param);
        if (strBytes == null)
        {
            throw new NullReferenceException();
        }
        Buffer.BlockCopy(strBytes, 0, buf, 0, strBytes.Length);
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeBytes(buf);
    }

    // RVA: 0x18FCAEC  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeStringSerial.c
    public void writeStringSerial(string str)
    {
        bool isNullOrEmpty = string.IsNullOrEmpty(str);
        string useStr = str;
        Encoding utf8 = Encoding.UTF8;
        if (utf8 == null)
        {
            throw new NullReferenceException();
        }
        if (isNullOrEmpty)
        {
            useStr = string.Empty;
        }
        byte[] strBytes = utf8.GetBytes(useStr);
        if (strBytes == null || this.T == null)
        {
            throw new NullReferenceException();
        }
        short len = (short)strBytes.Length;
        this.T.writeInt16(len);
        if (len < 1)
        {
            return;
        }
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeBytes(strBytes);
    }

    // RVA: 0x18FCBCC  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeInt32.c
    public void writeInt32(int param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeInt32(param);
    }

    // RVA: 0x18FCBE4  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeUInt32.c
    public void writeUInt32(uint param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeUInt32(param);
    }

    // RVA: 0x18FCBB4  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeShort.c
    public void writeShort(short param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeInt16(param);
    }

    // RVA: 0x18FCBFC  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeUInt16.c
    public void writeUInt16(ushort param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeUInt16(param);
    }

    // RVA: 0x18FCC14  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeInt64.c
    public void writeInt64(long param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeInt64(param);
    }

    // RVA: 0x18FCC2C  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeUInt64.c
    public void writeUInt64(ulong param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeUInt64(param);
    }

    // RVA: 0x18FCC44  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/writeFloat.c
    public void writeFloat(float param)
    {
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        this.T.writeFloat(param);
    }

    // RVA: 0x18FCC5C  Ghidra: work/06_ghidra/decompiled_full/NetRequestPackerBase/SendPackData.c
    public void SendPackData()
    {
        if (this._Protocol == -1)
        {
            return;
        }
        BaseConnect bc = BaseConnect.Instance;
        if (this.T == null)
        {
            throw new NullReferenceException();
        }
        int protocol = this._Protocol;
        byte[] payload = this.T.getBytes();
        if (bc == null)
        {
            throw new NullReferenceException();
        }
        bc.SendMessageToServer(protocol, payload);
    }

}
