// Source: work/03_il2cpp_dump/dump.cs class 'Zip' (TypeDefIndex 74)
// Bodies ported 1-1 from work/06_ghidra/decompiled_full/Zip/*.c and decompiled_rva/Zip__*.c
// XOR-byte 0x7A: per Ghidra, applied to dest[destStartIdx] after Compress, and to
// source[sourceStartIdx] before+after Uncompress (de-XOR the encrypted first byte for input).

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
using ComponentAce.Compression.Libs.zlib;

// Source: Il2CppDumper-stub  TypeDefIndex: 74
public sealed class Zip
{
    private static readonly byte zip_xor_byte = 0x7A;
    public static readonly int Z_NO_COMPRESSION = 0;
    public static readonly int Z_BEST_SPEED = 1;
    public static readonly int Z_BEST_COMPRESSION = 9;
    public static readonly int Z_DEFAULT_COMPRESSION = -1;

    // Source: Ghidra work/06_ghidra/decompiled_full/Zip/CompressMemoryToMemoryZIP_Level.c RVA 0x15CCF18
    public static int CompressMemoryToMemoryZIP_Level(byte[] dest, int destStartIdx, int destLen, byte[] source, int sourceStartIdx, int sourceLen, int level)
    {
        ZStream zs = new ZStream();
        zs.next_in = source;
        if (destLen < 1)
        {
            destLen = sourceLen << 1;
        }
        zs.next_in_index = sourceStartIdx;
        zs.avail_in = sourceLen;
        zs.next_out = dest;
        zs.next_out_index = destStartIdx;
        zs.avail_out = destLen;
        zs.total_out = 0;
        int rc = zs.deflateInit(level);
        if (rc != 0)
        {
            return 0;
        }
        int dResult = zs.deflate(4);
        int eResult = zs.deflateEnd();
        if (dResult != 1 || eResult != 0)
        {
            return 0;
        }
        if (dest == null) throw new NullReferenceException();
        if ((uint)dest.Length <= (uint)destStartIdx)
        {
            throw new IndexOutOfRangeException();
        }
        dest[destStartIdx] = (byte)(dest[destStartIdx] ^ zip_xor_byte);
        return (int)zs.total_out;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/Zip__UncompressMemoryToMemoryZIP_6arg.c RVA 0x15CD058
    public static int UncompressMemoryToMemoryZIP(byte[] dest, int destStartIdx, int destLen, byte[] source, int sourceStartIdx, int sourceLen)
    {
        ZStream zs = new ZStream();
        zs.next_out = dest;
        zs.next_out_index = destStartIdx;
        zs.avail_out = destLen;
        zs.next_in = source;
        zs.next_in_index = sourceStartIdx;
        zs.avail_in = sourceLen;
        zs.total_out = 0;
        int rc = zs.inflateInit();
        if (rc != 0)
        {
            return 0;
        }
        if (source == null) throw new NullReferenceException();
        if ((uint)source.Length <= (uint)sourceStartIdx)
        {
            throw new IndexOutOfRangeException();
        }
        // XOR the encrypted leading byte before inflate, then XOR back to restore source after.
        source[sourceStartIdx] = (byte)(source[sourceStartIdx] ^ zip_xor_byte);
        int infResult = zs.inflate(4);
        if (infResult == 1)
        {
            source[sourceStartIdx] = (byte)(source[sourceStartIdx] ^ zip_xor_byte);
            int endRc = zs.inflateEnd();
            if (endRc != 0)
            {
                return 0;
            }
            return (int)zs.total_out;
        }
        zs.inflateEnd();
        source[sourceStartIdx] = (byte)(source[sourceStartIdx] ^ zip_xor_byte);
        return 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Zip/CompressMemoryToMemoryZIP.c RVA 0x15CD1C4
    // (7-arg overload with start indices.) Wrapper around CompressMemoryToMemoryZIP_Level with level=Z_DEFAULT_COMPRESSION.
    public static int CompressMemoryToMemoryZIP(byte[] dest, int destStartIdx, int destLen, byte[] source, int sourceStartIdx, int sourceLen)
    {
        return CompressMemoryToMemoryZIP_Level(dest, destStartIdx, destLen, source, sourceStartIdx, sourceLen, Z_DEFAULT_COMPRESSION);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Zip/CompressMemoryToMemoryZIP.c RVA 0x15CD1CC (4-arg wrapper)
    // Per Ghidra: CompressMemoryToMemoryZIP_Level(dest,0,destLen,source,0,sourceLen,Z_DEFAULT_COMPRESSION)
    public static int CompressMemoryToMemoryZIP(byte[] dest, int destLen, byte[] source, int sourceLen)
    {
        return CompressMemoryToMemoryZIP_Level(dest, 0, destLen, source, 0, sourceLen, Z_DEFAULT_COMPRESSION);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/Zip/UncompressMemoryToMemoryZIP.c RVA 0x15CD1E8 (4-arg wrapper)
    public static int UncompressMemoryToMemoryZIP(byte[] dest, int destLen, byte[] source, int sourceLen)
    {
        return UncompressMemoryToMemoryZIP(dest, 0, destLen, source, 0, sourceLen);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/Zip___ctor.c RVA 0x15CD200
    // Body: only `System_Object___ctor(this, 0)`.
    public Zip()
    {
    }
}
