// Source: work/03_il2cpp_dump/dump.cs class 'WrdFileTool' (TypeDefIndex 738, line 48188)
// Static class — single static field MapInfoMgr_DATA_PATH @ 0x0
// Bodies ported 1-1 from work/06_ghidra/decompiled_rva/WrdFileTool__*.c

using System;
using System.IO;
using UnityEngine;

public static class WrdFileTool
{
    public static string MapInfoMgr_DATA_PATH;

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool___cctor.c RVA 0x18E3DB8
    // Body: MapInfoMgr_DATA_PATH = StringLiteral_4571 "Data/MapData/";
    static WrdFileTool()
    {
        MapInfoMgr_DATA_PATH = "Data/MapData/";
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__backToFile.c RVA 0x18DE054
    // Body: if (checkWrdFileExist(levelID))
    //       {
    //           string p = getPath(levelID, true) + ".wrd";    // StringLiteral_953 = ".bytes"
    //           //  ^ Wait — Ghidra concats with StringLiteral_953 (".bytes"), then with StringLiteral_948 (".bak").
    //           //  So source: p + ".bytes", and copies to p + ".bytes" + ".bak"
    //           File.Copy(p, p + ".bak", overwrite: true);
    //       }
    public static void backToFile(int levelID)
    {
        if (checkWrdFileExist(levelID))
        {
            string src = getPath(levelID, true) + ".bytes";
            string dst = src + ".bak";
            File.Copy(src, dst, true);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__writeToFile.c RVA 0x18E2A24
    // Body: if (wrdData != null) {
    //         int levelID = wrdData.levelID;  // offset 0x10
    //         string path = getPath(levelID, true) + ".bytes";
    //         Debug.Log("write wrd path:" + path);   // StringLiteral_20968
    //         tagmapHEADER header = wrdData.mapHeader;  // 0x18
    //         tagmapCODEDATA[] codeAry = wrdData.mapCodeAry;  // 0x20
    //         byte[] headerBytes = header.StructureToByteArray();
    //         FileStream fs = new FileStream(path, FileMode.Create);
    //         BinaryWriter bw = new BinaryWriter(fs);
    //         bw.Write(headerBytes);
    //         int count = header.mapWidth * header.mapHeight;  // 0x20 * 0x24
    //         for (int i = 0; i < count; i++) {
    //             byte[] cb = codeAry[i].StructureToByteArray();
    //             bw.Write(cb);
    //         }
    //         bw.Flush();
    //         fs.Close();   // dispose
    //       }
    // NOTE: Ghidra calls vtable slots 0x1f8/0x198/0x268 — these are Stream/BinaryWriter virtual methods.
    //       0x1f8 = BinaryWriter.Write(byte[]); 0x198 = BinaryWriter.Flush(); 0x268 = Stream.Close().
    public static void writeToFile(WrdData wrdData)
    {
        if (wrdData == null) throw new NullReferenceException();
        int levelID = wrdData.levelID;
        string path = getPath(levelID, true) + ".bytes";
        UnityEngine.Debug.Log("write wrd path:" + path);
        tagmapHEADER header = wrdData.mapHeader;
        tagmapCODEDATA[] codeAry = wrdData.mapCodeAry;
        if (header == null) throw new NullReferenceException();
        byte[] headerBytes = header.StructureToByteArray();
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(headerBytes);
        int count = header.mapWidth * header.mapHeight;
        if (count > 0)
        {
            if (codeAry == null) throw new NullReferenceException();
            for (int i = 0; i < count; i++)
            {
                if (codeAry[i] == null) throw new NullReferenceException();
                byte[] cb = codeAry[i].StructureToByteArray();
                bw.Write(cb);
            }
        }
        bw.Flush();
        fs.Close();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__readWRDFile.c RVA 0x18E350C
    // Body: string path = getPath(levelID, false);
    //       TextAsset asset = LoadMapDataFromBundle(path);
    //       if (asset == null) {
    //           UJDebug.Log("wrd file not exist:" + path);  // StringLiteral_20967
    //           return null;
    //       }
    //       byte[] sizeHolder = new byte[size of struct];  // Ghidra alloc 4 bytes (header size constant)
    //       byte[] bytes = asset.bytes;
    //       MemoryStream ms = new MemoryStream(bytes);
    //       BinaryReader br = new BinaryReader(ms);
    //       byte[] headerBytes = br.ReadBytes(WRD_CONST.HEADER_SIZE);    // vtable slot 0x2c8 = ReadBytes(int)
    //       headerStruct.ByteArrayToStructure(headerBytes, 0);
    //       UnityEngine.Debug.Log("" + headerStruct.mapWidth);   // logs width
    //       UnityEngine.Debug.Log("" + headerStruct.mapHeight);  // logs height
    //       int count = headerStruct.mapWidth * headerStruct.mapHeight;
    //       tagmapCODEDATA[] arr = new tagmapCODEDATA[count];
    //       DateTime start = DateTime.Now;
    //       bool wallBugFound = false;
    //       for (int i = 0; i < count; i++) {
    //           byte[] cb = br.ReadBytes(headerStruct.mapStructSize);   // 0x28
    //           tagmapCODEDATA d = new tagmapCODEDATA();
    //           arr[i] = d;
    //           d.ByteArrayToStructure(cb, 0);
    //           if (d.mapCode == 0xFFFF) { wallBugFound = true; d.mapCode = 0xFFFFFFFFu; }  // wall fix
    //       }
    //       TimeSpan dt = DateTime.Now - start;
    //       double ms = dt.TotalMilliseconds;
    //       UJDebug.Log("cost:" + ms + "ms");
    //       if (wallBugFound) UJDebug.LogError("==== Wall bug fix，Please save wrd file Again ====");
    //       return arr;
    public static tagmapCODEDATA[] readWRDFile(int levelID, ref tagmapHEADER headerStruct)
    {
        string path = getPath(levelID, false);
        TextAsset asset = LoadMapDataFromBundle(path);
        if (asset == null)
        {
            UJDebug.Log("wrd file not exist:" + path);
            return null;
        }
        // Ghidra: alloc headerBytes buf using WRD_CONST.HEADER_SIZE (the 0xb8 metadata is class static field)
        byte[] bytes = asset.bytes;
        MemoryStream ms = new MemoryStream(bytes);
        BinaryReader br = new BinaryReader(ms);
        byte[] headerBytes = br.ReadBytes(WRD_CONST.HEADER_SIZE);
        if (headerStruct == null) throw new NullReferenceException();
        headerStruct.ByteArrayToStructure(headerBytes, 0);
        // Debug logs of mapWidth + mapHeight (Ghidra: format using boxed ints + Debug.Log)
        UnityEngine.Debug.Log(headerStruct.mapWidth.ToString());
        UnityEngine.Debug.Log(headerStruct.mapHeight.ToString());
        int count = headerStruct.mapWidth * headerStruct.mapHeight;
        tagmapCODEDATA[] arr = new tagmapCODEDATA[count];
        DateTime start = DateTime.Now;
        bool wallBugFound = false;
        if (count >= 1)
        {
            for (int i = 0; i < count; i++)
            {
                byte[] cb = br.ReadBytes(headerStruct.mapStructSize);
                tagmapCODEDATA d = new tagmapCODEDATA();
                arr[i] = d;
                d.ByteArrayToStructure(cb, 0);
                if (d.mapCode == 0xFFFF)
                {
                    wallBugFound = true;
                    d.mapCode = 0xFFFFFFFFu;
                }
            }
        }
        TimeSpan dt = DateTime.Now - start;
        double msTotal = dt.TotalMilliseconds;
        UJDebug.Log("cost:" + msTotal.ToString() + "ms");
        if (wallBugFound)
        {
            UJDebug.LogError("==== Wall bug fix，Please save wrd file Again ====");
        }
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__getServerMapHeight.c RVA 0x18E3124
    // Body: string path = getPath(levelID, false);
    //       TextAsset asset = LoadMapDataFromBundle(path);
    //       byte[] buf = new byte[WRD_CONST.HEADER_SIZE];
    //       Array.Copy(asset.bytes, 0, buf, 0, WRD_CONST.HEADER_SIZE);
    //       tagmapHEADER h = new tagmapHEADER();
    //       h.ByteArrayToStructure(buf, 0);
    //       return (uint)(h.mapHeight << 6);   // multiply by SERVER_GRID_SIZE (64)
    public static uint getServerMapHeight(int levelID)
    {
        string path = getPath(levelID, false);
        TextAsset asset = LoadMapDataFromBundle(path);
        if (asset == null) throw new NullReferenceException();
        byte[] buf = new byte[WRD_CONST.HEADER_SIZE];
        Array.Copy(asset.bytes, 0, buf, 0, WRD_CONST.HEADER_SIZE);
        tagmapHEADER h = new tagmapHEADER();
        h.ByteArrayToStructure(buf, 0);
        return (uint)(h.mapHeight << 6);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool___printHeader.c RVA 0x18E1F34
    // Body: Ghidra emits a single call FUN_032a5b00(header.mapHeight, "Height:{0}").
    // FUN_032a5b00 is an il2cpp inlined helper — likely Debug.LogFormat or string.Format+Log.
    // The init block preloads all 6 format strings (Height/StructSize/Type/Width/Mark/Version)
    // but only Height:{0} is actually used. Following Ghidra 1-1.
    // NOTE: All other format strings in the init block (StringLiteral_10548/11836/12594/7945/12495)
    //       are loaded but NEVER referenced in code. This may be Ghidra losing the unrolled prints
    //       in dead-code elimination — TODO: verify whether the dump has those calls.
    public static void _printHeader(tagmapHEADER header)
    {
        if (header == null) throw new NullReferenceException();
        // Source: only Height:{0} format observed in Ghidra body. Other format strings preloaded but unused.
        UnityEngine.Debug.LogFormat("Height:{0}", header.mapHeight);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool___printCode.c RVA 0x18E217C
    // Body: UJDebug.Log(string.Format("Code Length:{0}", codeAry.Length));   // StringLiteral_4131
    //       for (int i = 0; i < codeAry.Length; i++) {
    //         tagmapCODEDATA d = codeAry[i];
    //         UJDebug.Log(string.Format("Code:{0}", d.mapCode));   // StringLiteral_4133
    //       }
    public static void _printCode(tagmapCODEDATA[] codeAry)
    {
        if (codeAry == null) throw new NullReferenceException();
        UJDebug.Log(string.Format("Code Length:{0}", codeAry.Length));
        for (int i = 0; i < codeAry.Length; i++)
        {
            tagmapCODEDATA d = codeAry[i];
            if (d == null) throw new NullReferenceException();
            UJDebug.Log(string.Format("Code:{0}", d.mapCode));
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__getPath.c RVA 0x18E23F8
    // Body: prefix selector by levelID magnitude:
    //         <10:  "Level00"   (StringLiteral_7582)
    //         <100: "Level0"    (StringLiteral_7581)
    //         <1000:"Level_"    (StringLiteral_7584)
    //         else: "Level"     (StringLiteral_7580)
    //       NOTE: Ghidra branch reads: default Level00; if levelID>=10 then Level0; if >=100 then Level_; if <1000 keep Level_; else Level.
    //       Verifying via inversion: <10 → "Level00", 10..99 → "Level0", 100..999 → "Level_", 1000+ → "Level".
    //       NOTE on Level_ vs Level0: Ghidra branches go from default "Level00", if >=10 swap to "Level0", if >=100 swap to "Level_",
    //         then if param_1 < 1000, swap to "Level_" again (no-op), so the actual logic is:
    //         <10 → "Level00";  10..99 → "Level0";  100..999 → "Level_";  >=1000 → "Level".
    //       Wait, re-reading: "if (9<p) puVar=L0; if (99<p) puVar=Level_; if (p<1000) puVar=Level_;" — the last
    //       assignment branches only on p<1000 (true for all 100..999), reassigning to same Level_ value (already set).
    //       At p>=1000, the chain ends with last "if (p<1000)" being false — so puVar stays at "Level_".
    //       Hmm — but that means 1000+ also gets "Level_"? Let me re-trace... actually the Ghidra condition chain is short-circuit:
    //       Reading literally: puVar = "Level00"; if (9<p && (puVar="Level0", 99<p)) { puVar="Level_"; if (p<1000) puVar="Level"; }
    //       That is the C-AND short-circuit pattern.
    //       So: <=9: "Level00". 10..99: "Level0". >=100: "Level_", then if <1000: "Level".
    //       So 100..999 → "Level". And >=1000 → "Level_".
    //       Final: <10:"Level00",  10..99:"Level0",  100..999:"Level",  >=1000:"Level_"
    //       suffix: ".wrd" (StringLiteral_981)
    //       fullPath: prefix with Application.dataPath + "/Resources_Bundle/" + MapInfoMgr_DATA_PATH
    //       non-fullPath: just prefix + levelID + ".wrd"
    public static string getPath(int levelID, bool fullPath = false)
    {
        string prefix;
        // Following Ghidra C-AND short-circuit:
        //   default: "Level00"
        //   if (9 < levelID) prefix = "Level0";
        //     if (99 < levelID) prefix = "Level_";
        //       if (levelID < 1000) prefix = "Level";
        prefix = "Level00";
        if (9 < levelID)
        {
            prefix = "Level0";
            if (99 < levelID)
            {
                prefix = "Level_";
                if (levelID < 1000)
                {
                    prefix = "Level";
                }
            }
        }
        if (!fullPath)
        {
            return string.Concat(prefix, levelID.ToString(), ".wrd");
        }
        // 6-element Concat: dataPath + "/Resources_Bundle/" + MapInfoMgr_DATA_PATH + prefix + levelID + ".wrd"
        return string.Concat(
            UnityEngine.Application.dataPath,
            "/Resources_Bundle/",
            MapInfoMgr_DATA_PATH,
            prefix,
            levelID.ToString(),
            ".wrd"
        );
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__checkWrdFileExist.c RVA 0x18DDD54
    // Body: return File.Exists(getPath(levelID, true) + ".bytes");
    public static bool checkWrdFileExist(int levelID)
    {
        return System.IO.File.Exists(getPath(levelID, true) + ".bytes");
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__printToFile.c RVA 0x18E265C
    // Body: wr.WriteLine("=================Header=================");   // StringLiteral_2658
    //       wr.WriteLine(string.Format("mapMark:{0}", header.mapMark));         // 18464
    //       wr.WriteLine(string.Format("mapType:{0}", header.mapType));         // 18466
    //       wr.WriteLine(string.Format("mapVersion:{0}", header.mapVersion));   // 18467
    //       wr.WriteLine(string.Format("mapWidth:{0}", header.mapWidth));       // 18468
    //       wr.WriteLine(string.Format("mapHeight:{0}", header.mapHeight));     // 18463
    //       wr.WriteLine(string.Format("mapStructSize:{0}", header.mapStructSize)); // 18465
    //       wr.WriteLine("=================Data=================");      // 2657
    //       for (int i = 0; i < dataAry.Length; i++) {
    //         tagmapCODEDATA d = dataAry[i];
    //         if (d.mapCode != 0) {
    //           int y = (mapWidth==0) ? 0 : (i / mapWidth);
    //           int x = i - y*mapWidth;
    //           // actually re-read Ghidra: local_54 = uVar15 - iVar3 * iVar2; iVar3 = (int)uVar15 / iVar2 (mapWidth)
    //           // → x = i % mapWidth; y = i / mapWidth.  But ghidra writes y=local_58 = uVar15 / mapWidth, x=local_54.
    //           wr.WriteLine(string.Format("x:{0},y:{1},mapCode:{2}", x, y, d.mapCode));   // 21059
    //         }
    //       }
    //       wr.Flush();
    public static void printToFile(StreamWriter wr, tagmapHEADER header, tagmapCODEDATA[] dataAry)
    {
        if (wr == null) throw new NullReferenceException();
        wr.WriteLine("=================Header=================");
        if (header == null) throw new NullReferenceException();
        wr.WriteLine(string.Format("mapMark:{0}", header.mapMark));
        wr.WriteLine(string.Format("mapType:{0}", header.mapType));
        wr.WriteLine(string.Format("mapVersion:{0}", header.mapVersion));
        wr.WriteLine(string.Format("mapWidth:{0}", header.mapWidth));
        wr.WriteLine(string.Format("mapHeight:{0}", header.mapHeight));
        wr.WriteLine(string.Format("mapStructSize:{0}", header.mapStructSize));
        wr.WriteLine("=================Data=================");
        if (dataAry == null) throw new NullReferenceException();
        for (int i = 0; i < dataAry.Length; i++)
        {
            tagmapCODEDATA d = dataAry[i];
            if (d == null) throw new NullReferenceException();
            if (d.mapCode != 0)
            {
                int y = (header.mapWidth == 0) ? 0 : (i / header.mapWidth);
                int x = i - y * header.mapWidth;
                wr.WriteLine(string.Format("x:{0},y:{1},mapCode:{2}", x, y, d.mapCode));
            }
        }
        wr.Flush();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/WrdFileTool__LoadMapDataFromBundle.c RVA 0x18E3C50
    // Body: ResMgr rm = ResMgr.Instance;
    //       if (rm == null) return null;
    //       AssetBundleOP op = rm.MapDataBundleOP;   // offset 0x70
    //       if (op == null) return null;
    //       Type t = typeof(TextAsset);
    //       UnityEngine.Object obj = op.Load(filename, t);
    //       if (obj == null) return null;
    //       if (obj is not TextAsset) return null;
    //       return (TextAsset)obj;
    // NOTE: Ghidra's class-check uses metadata (op->Load returns Object, then `is TextAsset` check via
    //       il2cpp class slot table). C# version uses `as TextAsset` for the same semantic.
    private static TextAsset LoadMapDataFromBundle(string filename)
    {
        ResMgr rm = ResMgr.Instance;
        if (rm == null) return null;
        AssetBundleOP op = rm.MapDataBundleOP;
        if (op == null) return null;
        Type t = typeof(TextAsset);
        UnityEngine.Object obj = op.Load(filename, t);
        if (obj == null) return null;
        return obj as TextAsset;
    }
}
