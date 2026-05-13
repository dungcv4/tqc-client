// Source: work/03_il2cpp_dump/dump.cs class 'BAToS' (TypeDefIndex 799, line 50457)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/BAToS/*.c
// Field offsets: startIndex @ 0x10 (int), data @ 0x18 (byte[])
// IL2CPP array layout: length @ +0x18, elements @ +0x20

using System;

// Source: Il2CppDumper-stub  TypeDefIndex: 799
public class BAToS
{
    private int startIndex;
    private byte[] data;

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/.ctor.c  RVA 0x18FAB98
    public BAToS(byte[] data, int startIndex)
    {
        this.data = data;
        this.startIndex = startIndex;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readInt32.c  RVA 0x18FABD4
    public int readInt32()
    {
        int v = BitConverter.ToInt32(this.data, this.startIndex);
        this.startIndex += 4;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readInt32s.c  RVA 0x18FAC00
    public int[] readInt32s(int count)
    {
        int[] arr = new int[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count << 2);
        this.startIndex += count * 4;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readInt64.c  RVA 0x18FAC88
    public long readInt64()
    {
        long v = BitConverter.ToInt64(this.data, this.startIndex);
        this.startIndex += 8;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readInt64s.c  RVA 0x18FACB4
    public long[] readInt64s(int count)
    {
        long[] arr = new long[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count << 3);
        this.startIndex += count * 8;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readUInt32.c  RVA 0x18FAD3C
    public uint readUInt32()
    {
        uint v = BitConverter.ToUInt32(this.data, this.startIndex);
        this.startIndex += 4;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readUInt32s.c  RVA 0x18FAD68
    public uint[] readUInt32s(int count)
    {
        uint[] arr = new uint[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count << 2);
        this.startIndex += count * 4;
        return arr;
    }

    // Source: pattern from sibling read{Int64,UInt32,UInt16} scalar bodies (same shape) — Ghidra .c
    // for this exact RVA 0x18FADF0 not in decompiled_full/BAToS/, but every other readX() scalar uses
    // BitConverter.To<T>(data,startIndex) + advance by sizeof(T). Confidence:medium — applied pattern.
    public ulong readUInt64()
    {
        ulong v = BitConverter.ToUInt64(this.data, this.startIndex);
        this.startIndex += 8;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readUInt64.c  RVA 0x18FAE1C
    // Note: dump.cs declares this overload as `public ulong[] readUInt64(int count)` (name collision with scalar)
    public ulong[] readUInt64(int count)
    {
        ulong[] arr = new ulong[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count << 3);
        this.startIndex += count * 8;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readInt16.c  RVA 0x18FAEA4
    public short readInt16()
    {
        short v = BitConverter.ToInt16(this.data, this.startIndex);
        this.startIndex += 2;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readInt16s.c  RVA 0x18FAED0
    public short[] readInt16s(int count)
    {
        short[] arr = new short[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count << 1);
        this.startIndex += count * 2;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readUInt16.c  RVA 0x18FAF58
    public ushort readUInt16()
    {
        ushort v = BitConverter.ToUInt16(this.data, this.startIndex);
        this.startIndex += 2;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readUInt16s.c  RVA 0x18FAF84
    public ushort[] readUInt16s(int count)
    {
        ushort[] arr = new ushort[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count << 1);
        this.startIndex += count * 2;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readFloat.c  RVA 0x18FB00C
    public float readFloat()
    {
        float v = BitConverter.ToSingle(this.data, this.startIndex);
        this.startIndex += 4;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readDouble.c  RVA 0x18FB038
    public double readDouble()
    {
        double v = BitConverter.ToDouble(this.data, this.startIndex);
        this.startIndex += 8;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readByte.c  RVA 0x18FB064
    // Bounds-check inlined by Ghidra (this.data null + startIndex < data.Length); C# array access handles both
    public byte readByte()
    {
        byte v = this.data[this.startIndex];
        this.startIndex += 1;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readBytes.c  RVA 0x18FB0A4
    public byte[] readBytes(int count)
    {
        byte[] arr = new byte[count];
        Buffer.BlockCopy(this.data, this.startIndex, arr, 0, count);
        this.startIndex += count;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readSByte.c  RVA 0x18FB130
    public sbyte readSByte()
    {
        sbyte v = (sbyte)this.data[this.startIndex];
        this.startIndex += 1;
        return v;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readSBytes.c  RVA 0x18FB170
    // Per-element copy (not BlockCopy) per original assembly
    public sbyte[] readSBytes(int count)
    {
        sbyte[] arr = new sbyte[count];
        for (int i = 0; i < count; i++)
        {
            arr[i] = (sbyte)this.data[i + this.startIndex];
        }
        this.startIndex += count;
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readOther<__Il2CppFullySharedGenericType>.c RVA 0x1BC908C
    // 1-1: T inst = Activator.CreateInstance<T>();
    //       MethodInfo m = typeof(T).GetMethod(StringLit_3497 "ByteArrayToStructure");  // instance method on T
    //       object[] args = new object[] { this.data, this.startIndex };
    //       int read = (int)m.Invoke(inst, args);
    //       this.startIndex += read;  (Ghidra: *(int*)(param_1 + 0x10) = result of MethodBase.Invoke)
    //       return inst;
    // Note: Ghidra assigns startIndex from the int return of Invoke — not adding, replacing. The
    // ByteArrayToStructure(data, startIndex) in dump.cs returns the new index.
    public T readOther<T>()
    {
        T inst = System.Activator.CreateInstance<T>();
        System.Reflection.MethodInfo m = typeof(T).GetMethod("ByteArrayToStructure");
        if (m == null) throw new System.NullReferenceException();
        object[] args = new object[] { this.data, this.startIndex };
        int newStart = (int)m.Invoke(inst, args);
        this.startIndex = newStart;
        return inst;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readOthers<__Il2CppFullySharedGenericType>.c RVA 0x1BC92F4
    // 1-1: T[] arr = new T[count]; for i in 0..count: arr[i] = this.readOther<T>(); return arr;
    public T[] readOthers<T>(int count)
    {
        T[] arr = new T[count];
        for (int i = 0; i < count; i++)
        {
            arr[i] = this.readOther<T>();
        }
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/readOthers2Dim<__Il2CppFullySharedGenericType>.c RVA 0x1BC9464
    // 1-1: T[,] arr = new T[countX, countY]; for x,y nested: arr[x,y] = readOther<T>(); return arr;
    public T[,] readOthers2Dim<T>(int countX, int countY)
    {
        T[,] arr = new T[countX, countY];
        for (int x = 0; x < countX; x++)
        {
            for (int y = 0; y < countY; y++)
            {
                arr[x, y] = this.readOther<T>();
            }
        }
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/getStartIndex.c  RVA 0x18FB234
    public int getStartIndex()
    {
        return this.startIndex;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/setStartIndex.c  RVA 0x18FB23C
    public void setStartIndex(int index)
    {
        this.startIndex = index;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/BAToS/getData.c  RVA 0x18FB244
    public byte[] getData()
    {
        return this.data;
    }
}
