// Source: work/03_il2cpp_dump/dump.cs class 'tagmapHEADER' (TypeDefIndex 733, line 47999)
// Field layout (offsets):
//   mapMark       byte[] @ 0x10
//   mapType       int    @ 0x18
//   mapVersion    int    @ 0x1C
//   mapWidth      int    @ 0x20
//   mapHeight     int    @ 0x24
//   mapStructSize int    @ 0x28
// Bodies ported 1-1 from work/06_ghidra/decompiled_rva/tagmapHEADER__*.c

using System.Text;

public class tagmapHEADER
{
    public byte[] mapMark;
    public int mapType;
    public int mapVersion;
    public int mapWidth;
    public int mapHeight;
    public int mapStructSize;

    // Source: Ghidra work/06_ghidra/decompiled_rva/tagmapHEADER___ctor.c RVA 0x18E17C0
    // Body: allocates 4-byte mapMark, then ASCII.GetBytes(StringLiteral_12551 "WORL") into mapMark,
    //       then sets mapStructSize = sizeof(tagmapHEADER struct fields after mark) (offset 0x28).
    //       Ghidra: *(int*)(param_1 + 0x28) = *(int*)((PTR_DAT_03461400)->ClassMetadata + 4)
    //         which is the metadata for an int — sizeof(int) = 4? No, it's pulling the class size of a runtime type.
    //       For tagmapHEADER body packed: mapMark(4) + mapType(4) + mapVersion(4) + mapWidth(4) + mapHeight(4) + mapStructSize(4) = 24 bytes
    //       The byte stream layout matches BAToS reads (4-byte mark + 5 ints) = 24 bytes
    //       Ghidra assigns this from the C# Type runtime metadata; the value at byte-stream-pack level is 24.
    //       Note: dump.cs declares mapStructSize int @ 0x28 but Ghidra-init source is from Type.SizeOf metadata
    //       which IL2CPP fills from il2cpp_class_layout. Hardcoding "24" would be invent; we follow Ghidra exactly:
    //       initialize from typeof(int).<some sizeof>. Closest faithful C# port: leave the field default (0)
    //       since we cannot replicate IL2CPP class-metadata read without IL2CPP runtime.
    //       INTERPRETATION CHOICE: set mapStructSize = 0x18 (24 decimal — sum of packed body sizes after mark+5 ints)
    //       — but this is unverified; the original IL2CPP value depends on runtime metadata.
    //       Conservative: initialize to 0 (the field starts as 0 anyway from object alloc).
    //       The actual value gets overwritten by ByteArrayToStructure when reading from disk anyway.
    public tagmapHEADER()
    {
        this.mapMark = new byte[4];
        // Ghidra: ASCII.GetBytes("WORL", mapMark)
        Encoding.ASCII.GetBytes("WORL", 0, 4, this.mapMark, 0);
        // Source: Ghidra writes mapStructSize from runtime Type metadata; in 1-1 IL2CPP terms this is
        // typeof(int).<MonoType.cached_size + 4 offset>. We cannot replicate without IL2CPP runtime —
        // value gets overwritten on disk read; left as default(int) = 0.
        // TODO: verify if any caller reads mapStructSize before ByteArrayToStructure is called.
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tagmapHEADER__ByteArrayToStructure.c RVA 0x18E18A8
    // Body: BAToS reader = new BAToS(data, startIndex);
    //       mapMark       = reader.readBytes(4);
    //       mapType       = reader.readInt32();
    //       mapVersion    = reader.readInt32();
    //       mapWidth      = reader.readInt32();
    //       mapHeight     = reader.readInt32();
    //       mapStructSize = reader.readInt32();
    //       return reader.startIndex;  (Ghidra: return *(int*)(lVar3 + 0x10))
    public int ByteArrayToStructure(byte[] data, int startIndex = 0)
    {
        BAToS reader = new BAToS(data, startIndex);
        this.mapMark = reader.readBytes(4);
        this.mapType = reader.readInt32();
        this.mapVersion = reader.readInt32();
        this.mapWidth = reader.readInt32();
        this.mapHeight = reader.readInt32();
        this.mapStructSize = reader.readInt32();
        return reader.getStartIndex();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/tagmapHEADER__StructureToByteArray.c RVA 0x18E1990
    // Body: SToBA writer = new SToBA();
    //       writer.writeBytes(mapMark);
    //       writer.writeInt32(mapType);
    //       writer.writeInt32(mapVersion);
    //       writer.writeInt32(mapWidth);
    //       writer.writeInt32(mapHeight);
    //       writer.writeInt32(mapStructSize);
    //       return writer.getBytes();
    public byte[] StructureToByteArray()
    {
        SToBA writer = new SToBA();
        writer.writeBytes(this.mapMark);
        writer.writeInt32(this.mapType);
        writer.writeInt32(this.mapVersion);
        writer.writeInt32(this.mapWidth);
        writer.writeInt32(this.mapHeight);
        writer.writeInt32(this.mapStructSize);
        return writer.getBytes();
    }
}
