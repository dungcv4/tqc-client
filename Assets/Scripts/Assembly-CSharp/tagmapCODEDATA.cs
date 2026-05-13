// Source: work/03_il2cpp_dump/dump.cs class 'tagmapCODEDATA' (TypeDefIndex 734, line 48022)
// Field layout (offsets):
//   mapCode uint @ 0x10
// Bodies ported 1-1 from work/06_ghidra/decompiled_rva/tagmapCODEDATA__*.c

public class tagmapCODEDATA
{
    public uint mapCode;

    // Source: Ghidra work/06_ghidra/decompiled_rva/tagmapCODEDATA___ctor.c RVA 0x18E1A54
    // Body: base..ctor(); mapCode = 0;
    public tagmapCODEDATA()
    {
        this.mapCode = 0u;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tagmapCODEDATA__ByteArrayToStructure.c RVA 0x18E1A70
    // Body: BAToS reader = new BAToS(data, startIndex);
    //       mapCode = reader.readUInt32();
    //       return reader.startIndex;
    public int ByteArrayToStructure(byte[] data, int startIndex = 0)
    {
        BAToS reader = new BAToS(data, startIndex);
        this.mapCode = reader.readUInt32();
        return reader.getStartIndex();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tagmapCODEDATA__StructureToByteArray.c RVA 0x18E1AF8
    // Body: SToBA writer = new SToBA();
    //       writer.writeUInt32(mapCode);
    //       return writer.getBytes();
    public byte[] StructureToByteArray()
    {
        SToBA writer = new SToBA();
        writer.writeUInt32(this.mapCode);
        return writer.getBytes();
    }
}
