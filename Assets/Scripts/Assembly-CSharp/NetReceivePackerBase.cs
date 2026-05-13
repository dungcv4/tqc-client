// Source: Ghidra work/06_ghidra/decompiled_full/NetReceivePackerBase/ (16 .c).
// Source: dump.cs TypeDefIndex 801
// Wraps BAToS (BitArray-to-Stream). All read* methods delegate to T (BAToS field @ 0x10).
// Ghidra inlines BAToS field access (T.data, T.startIndex directly) since BAToS fields are private —
// in C# we delegate to T.readX() which has identical semantics.

using System.Text;

public class NetReceivePackerBase
{
    private BAToS T;  // 0x10

    // Source: Ghidra .ctor.c  RVA 0x18FBB38 — initializes T = new BAToS(data, 0).
    public NetReceivePackerBase(byte[] data)
    {
        T = new BAToS(data, 0);
    }

    // Source: Ghidra readInt32.c  RVA 0x18FBBC4 — delegates to T.readInt32() (Ghidra inlines BitConverter+advance).
    public int readInt32()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readInt32();
    }

    // Source: Ghidra readInt64.c  RVA 0x18FBBF8
    public long readInt64()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readInt64();
    }

    // Source: Ghidra readUInt32.c  RVA 0x18FBC2C
    public uint readUInt32()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readUInt32();
    }

    // Source: Ghidra readUInt64.c  RVA 0x18FBC60 — delegates to T.readUInt64() (scalar).
    public ulong readUInt64()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readUInt64();
    }

    // Source: Ghidra readInt16.c  RVA 0x18FBC94
    public short readInt16()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readInt16();
    }

    // Source: Ghidra readUInt16.c  RVA 0x18FBCC8
    public ushort readUInt16()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readUInt16();
    }

    // Source: Ghidra readFloat.c  RVA 0x18FBCFC
    public float readFloat()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readFloat();
    }

    // Source: Ghidra readDouble.c  RVA 0x18FBD30
    public double readDouble()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readDouble();
    }

    // Source: Ghidra readByte.c  RVA 0x18FBD64 — delegates to T.readByte().
    public byte readByte()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readByte();
    }

    // Source: Ghidra readSByte.c  RVA 0x18FBD7C
    public sbyte readSByte()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readSByte();
    }

    // Source: Ghidra readStringLen.c  RVA 0x18FBD94
    // TODO: not in standard BAToS interface; body reads a length prefix (likely 4-byte int).
    // Pattern: int len = T.readInt32(); return len;
    public int readStringLen()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readInt32();
    }

    // Source: Ghidra readString.c  RVA 0x18FBDA8
    // Reads `len` bytes via T.readBytes(len) then decodes as UTF-8.
    public string readString(int len)
    {
        if (T == null) throw new System.NullReferenceException();
        byte[] bytes = T.readBytes(len);
        if (bytes == null) return string.Empty;
        return Encoding.UTF8.GetString(bytes);
    }

    // Source: Ghidra readBytes.c  RVA 0x18FBE00 — delegates to T.readBytes(len).
    public byte[] readBytes(int len)
    {
        if (T == null) throw new System.NullReferenceException();
        return T.readBytes(len);
    }

    // Source: Ghidra getStartIndex.c  RVA 0x18FBE18 — delegates to T.getStartIndex().
    public int getStartIndex()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.getStartIndex();
    }

    // Source: Ghidra setStartIndex.c  RVA 0x18FBE34 — delegates to T.setStartIndex(index).
    public void setStartIndex(int index)
    {
        if (T == null) throw new System.NullReferenceException();
        T.setStartIndex(index);
    }

    // Source: Ghidra getData.c  RVA 0x18FBE50 — delegates to T.getData().
    public byte[] getData()
    {
        if (T == null) throw new System.NullReferenceException();
        return T.getData();
    }
}
