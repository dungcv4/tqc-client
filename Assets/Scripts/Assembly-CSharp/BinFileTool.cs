// Source: work/03_il2cpp_dump/dump.cs class 'BinFileTool' (TypeDefIndex 714, line 47391)
// Static class — single static field MapInfoMgr_DATA_PATH @ 0x0
// Bodies ported 1-1 from work/06_ghidra/decompiled_rva/BinFileTool__*.c

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BinFileTool
{
    public static string MapInfoMgr_DATA_PATH;

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool___cctor.c RVA 0x18D21DC
    // Body: MapInfoMgr_DATA_PATH = StringLiteral_4571 "Data/MapData/";
    static BinFileTool()
    {
        MapInfoMgr_DATA_PATH = "Data/MapData/";
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__backToFile.c RVA 0x18D1744
    // Body: if (checkBinFileExist(levelID)) {
    //         string src = getPath(levelID, true) + ".bytes";
    //         File.Copy(src, src + ".bak", overwrite: true);
    //       }
    public static void backToFile(int levelID)
    {
        if (checkBinFileExist(levelID))
        {
            string src = getPath(levelID, true) + ".bytes";
            string dst = src + ".bak";
            File.Copy(src, dst, true);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__readMapConnectionPoints.c RVA 0x18D189C
    // Body:
    //   dicMapConnection = new Dictionary<ushort, List<MapConnectionPoint>>();
    //   string assetName = fileName + ".bytes";   // StringLiteral_953
    //   TextAsset asset = LoadMapDataFromBundle(assetName);
    //   if (asset == null) { UJDebug.LogError("bin file not exist:" + fileName); return 0; }
    //   tageventHEADER hdr = new tageventHEADER();
    //   byte[] bytes = asset.bytes;
    //   MemoryStream ms = new MemoryStream(bytes);
    //   BinaryReader br = new BinaryReader(ms);
    //   byte[] hb = br.ReadBytes(BIN_CONST.BIN_HEADER_SIZE);
    //   hdr.ByteArrayToStructure(hb, 0);
    //   int total = hdr.eveTotalNumber;
    //   ushort levelID = FileNameToLevelID(fileName);
    //   for (i = 0; i < total; i++) {
    //     byte[] db = br.ReadBytes(BIN_CONST.BIN_DATA_SIZE);
    //     tageventDATA d = new tageventDATA();
    //     d.ByteArrayToStructure(db, 0);
    //     if (d.eveItem != null && string.op_Equality(<eveItem[0]_as_string?> "defProcWarpPoint")) {  // StringLiteral_16020
    //       int target_map_code_key = d.eveData[1];   // (lVar13 + 0x24 = eveData[1])
    //       if (!dic.ContainsKey(target_map_code_key)) dic[target_map_code_key] = new List<MapConnectionPoint>();
    //       int eveCode = d.eveCode;     // (param_1+0x1c at lVar10+0x1c)
    //       if (eveCode < 0x32) {
    //         var list = dic[target_map_code_key];
    //         MapConnectionPoint p = new MapConnectionPoint {
    //             map_code = (ushort)levelID,
    //             target_map_code = (ushort)target_map_code_key,
    //             map_pos = new Vector2(d.eveX, d.eveCode),
    //             target_map_pos = new Vector2(d.eveData[2], d.eveData[3]),
    //         };
    //         list.Add(p);
    //       }
    //     }
    //   }
    //   return levelID;
    //
    // NOTE: This method is large; the Ghidra body decompile has tangled string-comparison logic
    //   reading what appears to be d.eveData[0] (string via il2cpp class metadata) compared to
    //   "defProcWarpPoint". The exact string-field interpretation is ambiguous without source.
    //   STUB with TODO — large method outside the critical-path requirement.
    public static ushort readMapConnectionPoints(string fileName, out Dictionary<ushort, List<MapConnectionPoint>> dicMapConnection)
    {
        // TODO 1-1 port: see work/06_ghidra/decompiled_rva/BinFileTool__readMapConnectionPoints.c, RVA 0x18D189C
        // Large method with ambiguous string-field interpretation around eveData[0] == "defProcWarpPoint".
        throw new System.NotImplementedException("TODO 1-1 port: BinFileTool.readMapConnectionPoints — see Ghidra RVA 0x18D189C");
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__readFile.c RVA 0x18D13DC
    // Body: string fname = getFileName(levelID);
    //       TextAsset asset = LoadMapDataFromBundle(fname);
    //       if (asset == null) { UJDebug.Log("bin file not exist:" + fname); return null; }
    //       byte[] bytes = asset.bytes;
    //       MemoryStream ms = new MemoryStream(bytes);
    //       BinaryReader br = new BinaryReader(ms);
    //       byte[] hb = br.ReadBytes(BIN_CONST.BIN_HEADER_SIZE);
    //       headerClass.ByteArrayToStructure(hb, 0);
    //       int total = headerClass.eveTotalNumber;
    //       tageventDATA[] arr = new tageventDATA[total];
    //       for (i = 0; i < total; i++) {
    //         byte[] db = br.ReadBytes(BIN_CONST.BIN_DATA_SIZE);
    //         tageventDATA d = new tageventDATA();
    //         d.ByteArrayToStructure(db, 0);
    //         arr[i] = d;
    //       }
    //       return arr;
    public static tageventDATA[] readFile(int levelID, tageventHEADER headerClass)
    {
        string fname = getFileName(levelID);
        TextAsset asset = LoadMapDataFromBundle(fname);
        if (asset == null)
        {
            UJDebug.Log("bin file not exist:" + fname);
            return null;
        }
        byte[] bytes = asset.bytes;
        MemoryStream ms = new MemoryStream(bytes);
        BinaryReader br = new BinaryReader(ms);
        byte[] hb = br.ReadBytes(BIN_CONST.BIN_HEADER_SIZE);
        if (headerClass == null) throw new NullReferenceException();
        headerClass.ByteArrayToStructure(hb, 0);
        uint total = (uint)headerClass.eveTotalNumber;
        tageventDATA[] arr = new tageventDATA[total];
        if ((int)total < 1)
        {
            return arr;
        }
        for (uint i = 0; i < total; i++)
        {
            byte[] db = br.ReadBytes(BIN_CONST.BIN_DATA_SIZE);
            tageventDATA d = new tageventDATA();
            d.ByteArrayToStructure(db, 0);
            arr[i] = d;
        }
        return arr;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__writeToFile.c RVA 0x18D11E4
    // Body: int levelID = binData.levelID;
    //       string path = getPath(levelID, true) + ".bytes";
    //       tageventHEADER hdr = binData.headerClass;
    //       tageventDATA[] arr = binData.dataAry;
    //       byte[] hb = hdr.StructureToByteArray();
    //       FileStream fs = new FileStream(path, FileMode.Create);
    //       BinaryWriter bw = new BinaryWriter(fs);
    //       bw.Write(hb);
    //       for (i = 0; i < hdr.eveTotalNumber; i++) {
    //         byte[] db = arr[i].StructureToByteArray();
    //         bw.Write(db);
    //       }
    //       bw.Flush();
    //       fs.Close();
    public static void writeToFile(BinData binData)
    {
        if (binData == null) throw new NullReferenceException();
        int levelID = binData.levelID;
        string path = getPath(levelID, true) + ".bytes";
        tageventHEADER hdr = binData.headerClass;
        tageventDATA[] arr = binData.dataAry;
        if (hdr == null) throw new NullReferenceException();
        byte[] hb = hdr.StructureToByteArray();
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(hb);
        if (hdr.eveTotalNumber > 0)
        {
            if (arr == null) throw new NullReferenceException();
            for (int i = 0; i < hdr.eveTotalNumber; i++)
            {
                if (arr[i] == null) throw new NullReferenceException();
                byte[] db = arr[i].StructureToByteArray();
                bw.Write(db);
            }
        }
        bw.Flush();
        fs.Close();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__FileNameToLevelID.c RVA 0x18D1FF4
    // Body: if (fileName.StartsWith("Level_")) {           // 7584
    //         if (!fileName.EndsWith(".bin")) return 0;    // 949
    //         return ushort.Parse(fileName.Substring(6, fileName.Length - 10));
    //       }
    //       else if (fileName.StartsWith("Level")) {       // 7580
    //         if (!fileName.EndsWith(".bin")) return 0;
    //         return ushort.Parse(fileName.Substring(5, fileName.Length - 9));
    //       }
    //       return 0;
    public static ushort FileNameToLevelID(string sFileName)
    {
        if (sFileName == null) throw new NullReferenceException();
        if (sFileName.StartsWith("Level_"))
        {
            if (!sFileName.EndsWith(".bin")) return 0;
            return ushort.Parse(sFileName.Substring(6, sFileName.Length - 10));
        }
        if (sFileName.StartsWith("Level"))
        {
            if (!sFileName.EndsWith(".bin")) return 0;
            return ushort.Parse(sFileName.Substring(5, sFileName.Length - 9));
        }
        return 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__getFileName.c RVA 0x18D20F8
    // Body: prefix-selector identical to WrdFileTool.getPath:
    //   <10:"Level00", 10..99:"Level0", 100..999:"Level", >=1000:"Level_"
    //   return prefix + levelID + ".bin"  (StringLiteral_949)
    public static string getFileName(int levelID)
    {
        string prefix = "Level00";
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
        return string.Concat(prefix, levelID.ToString(), ".bin");
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__getPath.c RVA 0x18D0A30
    // Body: prefix selector identical (Level00/Level0/Level/Level_), suffix ".bin" (949).
    //       fullPath: dataPath + "/resources_bundle/" + MapInfoMgr_DATA_PATH + prefix + levelID + ".bin"  (StringLiteral_1020 lowercase!)
    //       !fullPath: MapInfoMgr_DATA_PATH + prefix + levelID + ".bin"
    // NOTE: lowercase "/resources_bundle/" (StringLiteral_1020) vs WrdFileTool's uppercase "/Resources_Bundle/" (StringLiteral_1005).
    public static string getPath(int levelID, bool fullPath = false)
    {
        string prefix = "Level00";
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
        if (fullPath)
        {
            return string.Concat(
                UnityEngine.Application.dataPath,
                "/resources_bundle/",
                MapInfoMgr_DATA_PATH,
                prefix,
                levelID.ToString(),
                ".bin"
            );
        }
        // 4-element Concat: MapInfoMgr_DATA_PATH + prefix + levelID + ".bin"
        return string.Concat(MapInfoMgr_DATA_PATH, prefix, levelID.ToString(), ".bin");
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__checkBinFileExist.c RVA 0x18D181C
    // Body: return File.Exists(getPath(levelID, true) + ".bytes");
    public static bool checkBinFileExist(int levelID)
    {
        return System.IO.File.Exists(getPath(levelID, true) + ".bytes");
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__printHeader.c RVA 0x18D0440
    // Body: UJDebug.Log(string.Format("eveMark:{0}", header.eveMark));               // 16545
    //       UJDebug.Log(string.Format("eveTotalNumber:{0}", header.eveTotalNumber)); // 16546
    //       UJDebug.Log(string.Format("eveCombineTotal:{0}", header.eveCombineTotal)); // 16539
    //       UJDebug.Log(string.Format("eveCombineOffset:{0}", header.eveCombineOffset)); // 16538
    public static void printHeader(tageventHEADER header)
    {
        if (header == null) throw new NullReferenceException();
        UJDebug.Log(string.Format("eveMark:{0}", header.eveMark));
        UJDebug.Log(string.Format("eveTotalNumber:{0}", header.eveTotalNumber));
        UJDebug.Log(string.Format("eveCombineTotal:{0}", header.eveCombineTotal));
        UJDebug.Log(string.Format("eveCombineOffset:{0}", header.eveCombineOffset));
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__printData.c RVA 0x18D05E8
    // Body: for (i = 0; i < dataAry.Length; i++) {
    //         tageventDATA d = dataAry[i];
    //         UJDebug.Log(string.Format("eveCode:{0}", d.eveCode));   // 16537
    //         UJDebug.Log(string.Format("eveX:{0}", d.eveX));         // 16548
    //         UJDebug.Log(string.Format("eveY:{0}", d.eveY));         // 16550
    //         for (j = 0; j < 0x10; j++) UJDebug.Log(string.Format("eveItem{0}:{1}", j, d.eveItem[j]));  // 16544
    //         for (j = 0; j < 0x20; j++) UJDebug.Log(string.Format("eveData{0}:{1}", j, d.eveData[j]));  // 16541
    //       }
    public static void printData(tageventDATA[] dataAry)
    {
        if (dataAry == null) throw new NullReferenceException();
        for (int i = 0; i < dataAry.Length; i++)
        {
            tageventDATA d = dataAry[i];
            if (d == null) throw new NullReferenceException();
            UJDebug.Log(string.Format("eveCode:{0}", d.eveCode));
            UJDebug.Log(string.Format("eveX:{0}", d.eveX));
            UJDebug.Log(string.Format("eveY:{0}", d.eveY));
            for (int j = 0; j < 0x10; j++)
            {
                if (d.eveItem == null) throw new NullReferenceException();
                UJDebug.Log(string.Format("eveItem{0}:{1}", j, d.eveItem[j]));
            }
            for (int j = 0; j < 0x20; j++)
            {
                if (d.eveData == null) throw new NullReferenceException();
                UJDebug.Log(string.Format("eveData{0}:{1}", j, d.eveData[j]));
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__printToFile.c RVA 0x18D0CB0
    // Body: wr.WriteLine("=================Header=================");   // 2658
    //       wr.WriteLine(string.Format("eveMark:{0}", header.eveMark));         // 16545
    //       wr.WriteLine(string.Format("eveTotalNumber:{0}", header.eveTotalNumber)); // 16546
    //       wr.WriteLine(string.Format("eveCombineTotal:{0}", header.eveCombineTotal)); // 16539
    //       wr.WriteLine(string.Format("eveCombineOffset:{0}", header.eveCombineOffset)); // 16538
    //       wr.WriteLine("=================Data=================");     // 2657
    //       for (i=0; i<dataAry.Length; i++) {
    //         tageventDATA d = dataAry[i];
    //         wr.WriteLine(string.Format("eveCode:{0}", d.eveCode));   // 16537
    //         wr.WriteLine(string.Format("eveX:{0}", d.eveX));         // 16548
    //         wr.WriteLine(string.Format("eveY:{0}", d.eveY));         // 16550
    //         for (j=0; j<0x10; j++) wr.WriteLine(string.Format("eveItem{0}:{1}", j, d.eveItem[j])); // 16544
    //         for (j=0; j<0x20; j++) wr.WriteLine(string.Format("eveData{0}:{1}", j, d.eveData[j])); // 16541
    //       }
    //       wr.Flush();
    public static void printToFile(StreamWriter wr, tageventHEADER header, tageventDATA[] dataAry)
    {
        if (wr == null) throw new NullReferenceException();
        wr.WriteLine("=================Header=================");
        if (header == null) throw new NullReferenceException();
        wr.WriteLine(string.Format("eveMark:{0}", header.eveMark));
        wr.WriteLine(string.Format("eveTotalNumber:{0}", header.eveTotalNumber));
        wr.WriteLine(string.Format("eveCombineTotal:{0}", header.eveCombineTotal));
        wr.WriteLine(string.Format("eveCombineOffset:{0}", header.eveCombineOffset));
        wr.WriteLine("=================Data=================");
        if (dataAry == null) throw new NullReferenceException();
        for (int i = 0; i < dataAry.Length; i++)
        {
            tageventDATA d = dataAry[i];
            if (d == null) throw new NullReferenceException();
            wr.WriteLine(string.Format("eveCode:{0}", d.eveCode));
            wr.WriteLine(string.Format("eveX:{0}", d.eveX));
            wr.WriteLine(string.Format("eveY:{0}", d.eveY));
            for (int j = 0; j < 0x10; j++)
            {
                if (d.eveItem == null) throw new NullReferenceException();
                wr.WriteLine(string.Format("eveItem{0}:{1}", j, d.eveItem[j]));
            }
            for (int j = 0; j < 0x20; j++)
            {
                if (d.eveData == null) throw new NullReferenceException();
                wr.WriteLine(string.Format("eveData{0}:{1}", j, d.eveData[j]));
            }
        }
        wr.Flush();
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/BinFileTool__LoadMapDataFromBundle.c RVA 0x18D1E3C
    // Body: ResMgr rm = ResMgr.Instance;
    //       if (rm == null) return null;
    //       AssetBundleOP op = rm.MapDataBundleOP;   // offset 0x70
    //       if (op == null) return null;
    //       UnityEngine.Debug.Log("!=null");   // StringLiteral_319 — yes, literal debug log
    //       Type t = typeof(TextAsset);
    //       UnityEngine.Object obj = op.Load(filename, t);
    //       if (obj == null) return null;
    //       return obj as TextAsset;
    private static TextAsset LoadMapDataFromBundle(string filename)
    {
        ResMgr rm = ResMgr.Instance;
        if (rm == null) return null;
        AssetBundleOP op = rm.MapDataBundleOP;
        if (op == null) return null;
        UnityEngine.Debug.Log("!=null");
        Type t = typeof(TextAsset);
        UnityEngine.Object obj = op.Load(filename, t);
        if (obj == null) return null;
        return obj as TextAsset;
    }
}
