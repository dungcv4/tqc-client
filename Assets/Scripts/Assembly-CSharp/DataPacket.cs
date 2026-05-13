// Source: dump.cs class 'DataPacket' (TypeDefIndex: 819)
// Source: Ghidra work/06_ghidra/decompiled_full/DataPacket/.ctor.c RVA 0x01909280

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

public class DataPacket
{
    public LuaTable luaTable;
    public int maxID;
    public int dataCount;
    public List<int> keyList;

    // Source: Ghidra work/06_ghidra/decompiled_full/DataPacket/.ctor.c RVA 0x01909280
    // 1-1 logic:
    //   keyList = new List<int>(); (+0x20)
    //   System.Object.ctor()
    //   if (luaReturn == null) throw NullReferenceException;     // FUN_015cb8fc
    //   luaTable = luaReturn.get_Item(1);                         // +0x10 (LuaTable cast — null-tolerant)
    //   maxID = Convert.ToInt32(luaReturn.get_Item(3));           // +0x18
    //   dataCount = Convert.ToInt32(luaReturn.get_Item(4));       // +0x1c
    //   t2 = luaReturn.get_Item(2) as LuaTable
    //   if (t2 == null && dataCount > 0) throw NullReferenceException;
    //   for (i = 1; i <= dataCount; i++)
    //       keyList.Add(Convert.ToInt32(t2.get_Item(i)));         // List<int>.Add inlined with capacity grow
    public DataPacket(LuaTable luaReturn)
    {
        keyList = new List<int>();
        if (luaReturn == null) throw new System.NullReferenceException();
        // luaTable: get_Item(1). If null → store null; else cast to LuaTable (Ghidra type-check).
        object o1 = luaReturn[1];
        luaTable = o1 == null ? null : (LuaTable)o1;
        maxID = System.Convert.ToInt32(luaReturn[3]);
        dataCount = System.Convert.ToInt32(luaReturn[4]);
        object o2 = luaReturn[2];
        if (o2 == null)
        {
            if (dataCount > 0) throw new System.NullReferenceException();
            return;
        }
        LuaTable t2 = (LuaTable)o2;
        for (int i = 1; i <= dataCount; i++)
        {
            keyList.Add(System.Convert.ToInt32(t2[i]));
        }
    }

}
