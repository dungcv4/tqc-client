// Source: work/03_il2cpp_dump/dump.cs class 'tageventDATA' (TypeDefIndex 709, line 47252)
// Field layout (offsets):
//   eveHardLevel int   @ 0x10
//   eveCode      int   @ 0x14
//   eveX         int   @ 0x18
//   eveY         int   @ 0x1C
//   eveItem      int[] @ 0x20  (size 0x10 = 16)
//   eveData      int[] @ 0x28  (size 0x20 = 32)
// Bodies ported 1-1 from work/06_ghidra/decompiled_rva/tageventDATA__*.c

public class tageventDATA
{
    public int eveHardLevel;
    public int eveCode;
    public int eveX;
    public int eveY;
    public int[] eveItem;
    public int[] eveData;

    // Source: Ghidra work/06_ghidra/decompiled_rva/tageventDATA___ctor.c RVA 0x18CFCD8
    // Body: eveItem = new int[0x10]; eveData = new int[0x20]; base..ctor();
    public tageventDATA()
    {
        this.eveItem = new int[0x10];
        this.eveData = new int[0x20];
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tageventDATA__ByteArrayToStructure.c RVA 0x18CFA94
    // Body: BAToS reader = new BAToS(data, startIndex);
    //       eveHardLevel = reader.readInt32();
    //       eveCode      = reader.readInt32();
    //       eveX         = reader.readInt32();
    //       eveY         = reader.readInt32();
    //       for i in 0..0x10:  eveItem[i] = reader.readInt32();
    //       for i in 0..0x20:  eveData[i] = reader.readInt32();
    //       return reader.startIndex;
    // NOTE: Ghidra unrolled loop with element-by-element bounds check inline; per-index readInt32() then
    //       eveItem[i] = val. Could also be expressed as readInt32s(0x10), but Ghidra emits individual reads.
    public int ByteArrayToStructure(byte[] data, int startIndex = 0)
    {
        BAToS reader = new BAToS(data, startIndex);
        this.eveHardLevel = reader.readInt32();
        this.eveCode = reader.readInt32();
        this.eveX = reader.readInt32();
        this.eveY = reader.readInt32();
        for (int i = 0; i < 0x10; i++)
        {
            this.eveItem[i] = reader.readInt32();
        }
        for (int i = 0; i < 0x20; i++)
        {
            this.eveData[i] = reader.readInt32();
        }
        return reader.getStartIndex();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tageventDATA__StructureToByteArray.c RVA 0x18CFBC0
    // Body: SToBA writer = new SToBA();
    //       writer.writeInt32(eveHardLevel);
    //       writer.writeInt32(eveCode);
    //       writer.writeInt32(eveX);
    //       writer.writeInt32(eveY);
    //       for i in 0..0x10: writer.writeInt32(eveItem[i]);
    //       for i in 0..0x20: writer.writeInt32(eveData[i]);
    //       return writer.getBytes();
    public byte[] StructureToByteArray()
    {
        SToBA writer = new SToBA();
        writer.writeInt32(this.eveHardLevel);
        writer.writeInt32(this.eveCode);
        writer.writeInt32(this.eveX);
        writer.writeInt32(this.eveY);
        for (int i = 0; i < 0x10; i++)
        {
            writer.writeInt32(this.eveItem[i]);
        }
        for (int i = 0; i < 0x20; i++)
        {
            writer.writeInt32(this.eveData[i]);
        }
        return writer.getBytes();
    }
}
