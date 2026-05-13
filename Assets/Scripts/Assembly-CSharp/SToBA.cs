// Source: work/03_il2cpp_dump/dump.cs class 'SToBA' (TypeDefIndex 800, line 50557)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/SToBA/*.c
// Field offset: data @ 0x10 (List<byte>)
// Network serialization: Server-to-byte-array packer (mirror of BAToS reader)

using System;
using System.Collections.Generic;
using System.Reflection;

// Source: Il2CppDumper-stub  TypeDefIndex: 800
public class SToBA
{
    private List<byte> data;

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/.ctor.c  RVA 0x18FBAB0
    // Body: this.data = new List<byte>(); base..ctor();
    public SToBA()
    {
        this.data = new List<byte>();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB24C
    // All 10 toByteArray overloads share the same Ghidra body (single .c file decompiled):
    // simply forwards param to System_BitConverter__GetBytes. Ghidra's "GetBytes(0)" is the
    // truncation artifact of decompiling 10 identical thunk wrappers into one slot.
    public static byte[] toByteArray(char data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB254
    public static byte[] toByteArray(short data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB25C
    public static byte[] toByteArray(int data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB264
    public static byte[] toByteArray(long data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB26C
    // BitConverter.GetBytes does not have a byte overload; use new byte[] { data } per IL2CPP behavior
    // (single-byte case still produces a 1-byte array containing the value).
    public static byte[] toByteArray(byte data) { return new byte[] { data }; }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB278
    public static byte[] toByteArray(ushort data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB280
    public static byte[] toByteArray(uint data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB288
    private static byte[] toByteArray(ulong data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB290
    public static byte[] toByteArray(float data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/toByteArray.c  RVA 0x18FB298
    public static byte[] toByteArray(double data) { return BitConverter.GetBytes(data); }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeChar.c  RVA 0x18FB2A0
    // Body: data.AddRange(BitConverter.GetBytes(param));
    public void writeChar(char param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeChars.c  RVA 0x18FB308
    // Body: for (i=0; i<param.Length; i++) writeChar(param[i]);
    public void writeChars(char[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeChar(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeInt16.c  RVA 0x18FB374
    public void writeInt16(short param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeInt16s.c  RVA 0x18FB3DC
    public void writeInt16s(short[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeInt16(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeUInt16.c  RVA 0x18FB448
    public void writeUInt16(ushort param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeUInt16s.c  RVA 0x18FB4B0
    public void writeUInt16s(ushort[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeUInt16(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeInt32.c  RVA 0x18FB51C
    public void writeInt32(int param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeInt32s.c  RVA 0x18FB584
    public void writeInt32s(int[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeInt32(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeUInt32.c  RVA 0x18FB5F0
    public void writeUInt32(uint param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeUInt32s.c  RVA 0x18FB658
    public void writeUInt32s(uint[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeUInt32(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeInt64.c  RVA 0x18FB6C4
    public void writeInt64(long param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeInt64s.c  RVA 0x18FB72C
    public void writeInt64s(long[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeInt64(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeUInt64.c  RVA 0x18FB798
    public void writeUInt64(ulong param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeUInt64s.c  RVA 0x18FB800
    public void writeUInt64s(ulong[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeUInt64(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeByte.c  RVA 0x18FB86C
    // Body: data.Add(param); (Ghidra shows fast-path inline of List<T>.Add + AddWithResize fallback)
    public void writeByte(byte param)
    {
        if (this.data == null) throw new NullReferenceException();
        this.data.Add(param);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeBytes.c  RVA 0x18FB910
    // Body: data.AddRange(param);
    public void writeBytes(byte[] param)
    {
        if (this.data == null) throw new NullReferenceException();
        this.data.AddRange(param);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeFloat.c  RVA 0x18FB968
    public void writeFloat(float param)
    {
        this.data.AddRange(BitConverter.GetBytes(param));
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeFloats.c  RVA 0x18FB9D8
    public void writeFloats(float[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeFloat(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeOther<__Il2CppFullySharedGenericType>.c  RVA 0x1C43FF0
    // Body uses System.Type.GetMethod("StructureToByteArray") on typeof(T), invokes via reflection;
    // result is byte[] → AddRange. If method not found, logs UJDebug.LogError(typeName + " not implement StructureToByteArray").
    // String literals resolved via global-metadata.dat:
    //   StringLiteral_10549 = "StructureToByteArray"
    //   StringLiteral_269   = " not implement StructureToByteArray"
    public void writeOther<T>(T param)
    {
        if (this.data == null) throw new NullReferenceException();
        System.Type t = typeof(T);
        MethodInfo mi = t.GetMethod("StructureToByteArray");
        if (mi == null)
        {
            UJDebug.LogError(t.ToString() + " not implement StructureToByteArray");
            return;
        }
        object boxed = (object)param;
        object result = mi.Invoke(boxed, null);
        byte[] bytes = (byte[])result;
        this.data.AddRange(bytes);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/writeOthers<__Il2CppFullySharedGenericType>.c  RVA 0x1C44228
    // Body: for (i=0; i<param.Length; i++) writeOther<T>(param[i]); (loop calls writeOther via shared generic dispatch)
    public void writeOthers<T>(T[] param)
    {
        if (param == null) throw new NullReferenceException();
        for (int i = 0; i < param.Length; i++)
        {
            this.writeOther<T>(param[i]);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SToBA/getBytes.c  RVA 0x18FBA44
    // Body: byte[] r = data.ToArray(); data = null; return r;
    public byte[] getBytes()
    {
        if (this.data == null) throw new NullReferenceException();
        byte[] result = this.data.ToArray();
        this.data = null;
        return result;
    }
}
