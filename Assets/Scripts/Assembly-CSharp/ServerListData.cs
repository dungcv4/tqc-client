// Source: dump.cs class 'ServerListData' (TypeDefIndex: 66)
// Source: Ghidra work/06_ghidra/decompiled_full/ServerListData/.ctor.c RVA 0x015C6DD8

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

public class ServerListData
{
    public int id;
    public int realServerID;
    public string name;
    public string ip;
    public int port;
    public string regionID;
    public int status;
    public int recommend;
    public string mergeServerID;

    // Source: Ghidra work/06_ghidra/decompiled_full/ServerListData/.ctor.c RVA 0x015C6DD8
    // 1-1: System.Object.ctor; id/realServerID = 0 (packed at +0x10);
    //   name = ip = regionID = mergeServerID = string.Empty (StringLiteral from class-vtable +0xB8);
    //   port = 0 (at +0x28); status/recommend = 0 (packed at +0x38).
    public ServerListData()
    {
        id = 0;
        realServerID = 0;
        name = string.Empty;
        ip = string.Empty;
        port = 0;
        regionID = string.Empty;
        status = 0;
        recommend = 0;
        mergeServerID = string.Empty;
    }

}
