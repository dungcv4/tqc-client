// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18BF8B8, 0x18BF924, 0x18BFC3C, 0x18BFA90, 0x18BF96C
// Ghidra dir: work/06_ghidra/decompiled_full/LuaResLoader/

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

using LuaInterface;
// Source: Il2CppDumper-stub  TypeDefIndex: 648
public class LuaResLoader : LuaFileUtils
{
    // Source: Ghidra work/06_ghidra/decompiled_full/LuaResLoader/.ctor.c RVA 0x18BF8B8
    // 1-1: LuaFileUtils.ctor(this, 0);
    //      LuaFileUtils.instance = this;    // *(static field +0xb8) = this (PTR_DAT_03460128)
    //      this.beZip = false;              // *(this + 0x10) = 0
    public LuaResLoader()
        : base()
    {
        // Set LuaFileUtils.instance to this (LuaResLoader subclass) so all Lua require calls
        // go through the override chain (ReadDownLoadFile → ReadResourceFile → base ReadFile).
        var instField = typeof(LuaFileUtils).GetField("instance",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        if (instField != null) instField.SetValue(null, this);
        this.beZip = false;
    }

    // RVA: 0x18BF924  Ghidra: work/06_ghidra/decompiled_full/LuaResLoader/ReadFile.c
    public override byte[] ReadFile(string fileName)
    {
        // Ghidra:
        //   lVar1 = ReadDownLoadFile();
        //   if (lVar1 == 0 && (lVar1 = ReadResourceFile(fileName)) == 0) {
        //       LuaInterface_LuaFileUtils__ReadFile(this, fileName, 0);
        //   }
        // Note: when one of the first two returns non-null, the caller doesn't actually
        // capture/return it in the void Ghidra signature — but since the C# return type is byte[],
        // we return whichever non-null value was found, falling back to base.ReadFile.
#if UNITY_EDITOR
        // Editor deviation (Ghidra-equivalent for cold-start state): in Editor we have _Assets/
        // pre-extracted from AssetRipper (1619 files). Production initial boot has _Assets/ empty
        // and only populates it from CDN patches. Ghidra's original order (disk → Resources → bundle)
        // naturally takes the bundle path first in production's cold-start. To replicate that here —
        // which is REQUIRED for ResMgr.GetLuaScript to be called and bLoad flags to be marked
        // (so ProcessLunchGame.V_UpdateLoading case 3 IsLoadLuaFinish returns true) — try bundle
        // (base.ReadFile → LuaFileUtils.ReadFile bundle branch) FIRST.
        byte[] bundleBytes = base.ReadFile(fileName);
        if (bundleBytes != null) return bundleBytes;
        byte[] bytes = ReadDownLoadFile(fileName);
        if (bytes != null) return bytes;
        bytes = ReadResourceFile(fileName);
        if (bytes != null) return bytes;
        return null; // bundle path already tried
#else
        byte[] bytes = ReadDownLoadFile(fileName);
        if (bytes != null)
        {
            return bytes;
        }
        bytes = ReadResourceFile(fileName);
        if (bytes != null)
        {
            return bytes;
        }
        return base.ReadFile(fileName);
#endif
    }

    // RVA: 0x18BFC3C  Ghidra: work/06_ghidra/decompiled_full/LuaResLoader/FindFileError.c
    public override string FindFileError(string fileName)
    {
        // Source: Ghidra work/06_ghidra/decompiled_full/LuaResLoader/FindFileError.c RVA 0x18BFC3C
        // String literals (resolved per Ghidra symbol indices via global-metadata.dat):
        //   43 (PTR_StringLiteral_43)  = "'"
        //   969 (PTR_StringLiteral_969) = ".lua"
        //   970 = "'" (closing-quote chunk for the literal block prefix)
        //   2669 = "$LANGUAGE" (Replace target marker)
        //   44 (PTR_StringLiteral_44) = "/"
        //   PTR_DAT_0345e580 + 0xb8 → ResourcesPath._outputPath (offset 0x20 on static fields)
        //   PTR_DAT_03446428 → ICloneable / IDisposable type handle for the foreach loop.
        // Builds a multi-line error report:
        //   For each path in searchPaths: "\n\tno file '<path>/<fileName>.lua'"
        //   Plus: "\n\tno file '<ResourcesPath.OutputPath>/$LANGUAGE/<fileName>.lua'"
        // Then CString.Replace("$LANGUAGE", LANGUAGE) and returns final string.
        if (System.IO.Path.IsPathRooted(fileName))
        {
            return fileName;
        }
        if (System.IO.Path.GetExtension(fileName) == ".lua")
        {
            fileName = fileName.Substring(0, fileName.Length - 4);
        }
        var sb = new System.Text.StringBuilder(512);
        if (searchPaths != null)
        {
            for (int i = 0; i < searchPaths.Count; i++)
            {
                sb.Append("\n\tno file '");
                sb.Append(searchPaths[i]);
                sb.Append('/');
                sb.Append(fileName);
                sb.Append(".lua'");
            }
        }
        // ResourcesPath.OutputPath fallback line
        sb.Append("\n\tno file '");
        sb.Append(ResourcesPath.OutputPath);
        sb.Append("/$LANGUAGE/");
        sb.Append(fileName);
        sb.Append(".lua'");
        return sb.ToString().Replace("$LANGUAGE", ResourcesPath.LANGUAGE);
    }

    // RVA: 0x18BFA90  Ghidra: work/06_ghidra/decompiled_full/LuaResLoader/ReadResourceFile.c
    private byte[] ReadResourceFile(string fileName)
    {
        // Ghidra: ensure fileName ends with ".lua"; concat "Lua/" prefix; load via
        // UnityEngine.Resources.Load(path, typeof(TextAsset)). If non-null, return its .bytes.
        if (fileName == null)
        {
            throw new System.NullReferenceException();
        }
        if (!fileName.EndsWith(".lua"))
        {
            fileName = string.Concat(fileName, ".lua");
        }
        string path = string.Concat("Lua/", fileName);
        UnityEngine.TextAsset ta = UnityEngine.Resources.Load(path, typeof(UnityEngine.TextAsset)) as UnityEngine.TextAsset;
        if (ta != null)
        {
            return ta.bytes;
        }
        return null;
    }

    // RVA: 0x18BF96C  Ghidra: work/06_ghidra/decompiled_full/LuaResLoader/ReadDownLoadFile.c
    private byte[] ReadDownLoadFile(string fileName)
    {
        // Ghidra: ensure ".lua" suffix; if path is not rooted, format "{0}/{1}" with a static
        // base path (a string field on a singleton at PTR_DAT_0345e580 + 0xb8 + 0x20 — opaque
        // here). If File.Exists, return File.ReadAllBytes; else return null.
        if (fileName == null)
        {
            throw new System.NullReferenceException();
        }
        if (!fileName.EndsWith(".lua"))
        {
            fileName = string.Concat(fileName, ".lua");
        }
        if (!System.IO.Path.IsPathRooted(fileName))
        {
            // PTR_DAT_0345e580 = ResourcesPath type static area; offset 0x20 = _outputPath
            // (per ResourcesPath static layout: 0x00 CDNVersion, 0x08 bdCreateTime, 0x10 bundleTNum,
            //  0x18 _patchHost, 0x20 _outputPath). String literal #21237 = "{0}/{1}" format.
            fileName = string.Format("{0}/{1}", ResourcesPath.OutputPath, fileName);
        }
        if (System.IO.File.Exists(fileName))
        {
            return System.IO.File.ReadAllBytes(fileName);
        }
        return null;
    }

}
