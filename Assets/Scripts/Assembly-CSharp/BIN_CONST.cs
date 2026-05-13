// Source: work/03_il2cpp_dump/dump.cs class 'BIN_CONST' (TypeDefIndex 710, line 47275)
// .cctor RVA 0x18CFD58 — IL2CPP-resolves sizeof at compile time.
// Byte stream layouts:
//   tageventHEADER: 4 (mark) + 3*int = 16 bytes
//   tageventDATA: 4*int + 16*int + 32*int = 16 + 64 + 128 = 208 bytes
// Matches BinFileTool.readFile which reads:
//   br.ReadBytes(BIN_HEADER_SIZE) then loop  br.ReadBytes(BIN_DATA_SIZE).

public static class BIN_CONST
{
    // Source: cctor RVA 0x18CFD58 — IL2CPP compile-time sizeof.
    public static int BIN_HEADER_SIZE = 16;
    public static int BIN_DATA_SIZE = 208;
}
