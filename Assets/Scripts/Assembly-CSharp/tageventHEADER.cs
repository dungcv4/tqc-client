// Source: work/03_il2cpp_dump/dump.cs class 'tageventHEADER' (TypeDefIndex 708, line 47231)
// Field layout (offsets):
//   eveMark          byte[] @ 0x10
//   eveTotalNumber   int    @ 0x18
//   eveCombineTotal  int    @ 0x1C
//   eveCombineOffset int    @ 0x20
// Bodies ported 1-1 from work/06_ghidra/decompiled_rva/tageventHEADER__*.c

using System.Text;

public class tageventHEADER
{
    public byte[] eveMark;
    public int eveTotalNumber;
    public int eveCombineTotal;
    public int eveCombineOffset;

    // Source: Ghidra work/06_ghidra/decompiled_rva/tageventHEADER___ctor.c RVA 0x18CF878
    // Body: eveMark = new byte[4]; base..ctor();
    //       ASCII.GetBytes(StringLiteral_4924 "EVEF") into eveMark
    public tageventHEADER()
    {
        this.eveMark = new byte[4];
        Encoding.ASCII.GetBytes("EVEF", 0, 4, this.eveMark, 0);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tageventHEADER__ByteArrayToStructure.c RVA 0x18CF928
    // Body: BAToS reader = new BAToS(data, startIndex);
    //       eveMark = reader.readBytes(4);
    //       eveTotalNumber = reader.readInt32();
    //       eveCombineTotal = reader.readInt32();
    //       eveCombineOffset = reader.readInt32();
    //       return reader.startIndex;
    public int ByteArrayToStructure(byte[] data, int startIndex = 0)
    {
        BAToS reader = new BAToS(data, startIndex);
        this.eveMark = reader.readBytes(4);
        this.eveTotalNumber = reader.readInt32();
        this.eveCombineTotal = reader.readInt32();
        this.eveCombineOffset = reader.readInt32();
        return reader.getStartIndex();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tageventHEADER__StructureToByteArray.c RVA 0x18CF9F0
    // Body: SToBA writer = new SToBA();
    //       writer.writeBytes(eveMark);
    //       writer.writeInt32(eveTotalNumber);
    //       writer.writeInt32(eveCombineTotal);
    //       writer.writeInt32(eveCombineOffset);
    //       return writer.getBytes();
    public byte[] StructureToByteArray()
    {
        SToBA writer = new SToBA();
        writer.writeBytes(this.eveMark);
        writer.writeInt32(this.eveTotalNumber);
        writer.writeInt32(this.eveCombineTotal);
        writer.writeInt32(this.eveCombineOffset);
        return writer.getBytes();
    }
}
