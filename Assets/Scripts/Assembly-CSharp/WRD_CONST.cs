// Source: work/03_il2cpp_dump/dump.cs class 'WRD_CONST' (TypeDefIndex 735, line 48040)
// .cctor RVA 0x18E1B6C — Ghidra body: stores DAT_008e3828 (8 bytes) into class static area @ +0xb8.
// 8-byte slot covers HEADER_SIZE@0x0 and CODE_SIZE@0x4. Bytes at DAT_008e3828:
//   6d 00 00 00 ff ff ff ff  (Ghidra-dumped bytes)
//   → HEADER_SIZE = 109 (0x6d) ... but this is a Ghidra relocation artifact, not the real value
//
// 1-1 INTERPRETATION CHOICE: The byte stream layouts for tagmapHEADER / tagmapCODEDATA are deterministic:
//   tagmapHEADER bytes  = 4 (mark) + 4*int(type,version,width,height) + 4 (structSize) = 24 bytes
//   tagmapCODEDATA bytes = 4 (mapCode uint) = 4 bytes
// These are the values WrdFileTool.readWRDFile uses (br.ReadBytes(HEADER_SIZE) then HEADER_SIZE = 24).
// The IL2CPP cctor resolves these at compile time from sizeof(tagmapHEADER struct body fields).
// We initialize them inline; this matches what the byte stream code expects.

public static class WRD_CONST
{
    // Source: cctor RVA 0x18E1B6C — IL2CPP resolves at compile time from sizeof.
    // HEADER_SIZE = 24 (byte stream size of tagmapHEADER: 4 + 5*4)
    public static int HEADER_SIZE = 24;

    // CODE_SIZE = 4 (byte stream size of tagmapCODEDATA: single uint)
    public static int CODE_SIZE = 4;

    public const uint MAP_CODE_WALL = uint.MaxValue;
    public const uint MAP_CODE_FREE = 4294967294u;
    public const uint MAP_CODE_EMPTY = 0u;
    public const int SERVER_GRID_SIZE = 64;
    public const int CLIENT_GRID_SIZE = 1;
}
