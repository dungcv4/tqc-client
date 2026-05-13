// Source: work/03_il2cpp_dump/dump.cs TypeDefIndex 820 + Ghidra decompiled_full/LuaData/*.c
// Field offsets per dump.cs: lua@0x10, func@0x18, obsTable@0x20, stageTable@0x28, magicTable@0x30,
// playersTable@0x38, levelupTable@0x40, itemTable@0x48, matrixTable@0x50, AITable@0x58,
// officerTable@0x60, npcTalkTable@0x68, gameResourceTable@0x70.

using System.Collections.Generic;
using LuaInterface;

public class LuaData
{
    private static LuaData s_instance;
    private LuaState lua;            // 0x10
    private LuaFunction func;        // 0x18
    public DataPacket obsTable;      // 0x20
    public DataPacket stageTable;    // 0x28
    public DataPacket magicTable;    // 0x30
    public DataPacket playersTable;  // 0x38
    public DataPacket levelupTable;  // 0x40
    public DataPacket itemTable;     // 0x48
    public DataPacket matrixTable;   // 0x50
    public DataPacket AITable;       // 0x58
    public DataPacket officerTable;  // 0x60
    public DataPacket npcTalkTable;  // 0x68
    public List<DataPacket> gameResourceTable; // 0x70

    // Source: Ghidra get_Instance.c RVA 0x190954C — return s_instance
    public static LuaData get_Instance()
    {
        return s_instance;
    }

    // Source: Ghidra CreateInstance.c RVA 0x1909594
    // 1-1: if (s_instance == null) { s_instance = new LuaData(); if (s_instance == null) NRE; }
    public static void CreateInstance()
    {
        if (s_instance == null)
        {
            s_instance = new LuaData();
            if (s_instance == null) throw new System.NullReferenceException();
        }
    }

    // Source: Ghidra Start.c RVA 0x19096A4 — empty body (just `return`).
    private void Start() { }

    // Source: Ghidra LoadLuaData.c RVA 0x19096A8
    // Calls LoadSingleData 3 times. String literals resolved via global-metadata.dat by Ghidra
    // metadata index (18940/18909/19229):
    //   LoadSingleData(ref obsTable@0x20,      "objectInfo.obj")     ← idx 18940
    //   LoadSingleData(ref npcTalkTable@0x68,  "not Bind facebook")  ← idx 18909 (string as Lua key)
    //   LoadSingleData(ref playersTable@0x38,  "platform")           ← idx 19229
    public void LoadLuaData()
    {
        LoadSingleData(ref obsTable, "objectInfo.obj");
        LoadSingleData(ref npcTalkTable, "not Bind facebook");
        LoadSingleData(ref playersTable, "platform");
    }

    // Source: Ghidra LoadSingleData.c  RVA 0x1909738
    // 1. Call LuaFramework.Util.CallMethod<LuaTable>(sTable, "GetDSSDownloadURL" [lit 5796], empty array).
    // 2. If result LuaTable != null AND result.IsAlive (reference > 0 ≈ IsAlive):
    //      _object = new DataPacket(result);
    // 3. Else: UJDebug.LogErrorFormat("Lua/" + sTable, args) [lit 7775 = "Lua/"].
    // NOTE: literal 5796 = "GetDSSDownloadURL" — verbatim Lua function name lookup key.
    //       literal 7775 = "Lua/" — error format prefix.
    public void LoadSingleData(ref DataPacket _object, string sTable)
    {
        LuaInterface.LuaTable result = LuaFramework.Util.CallMethod<LuaInterface.LuaTable>(
            sTable, "GetDSSDownloadURL", new object[0]);
        if (result != null && result.IsAlive)
        {
            _object = new DataPacket(result);
            return;
        }
        UJDebug.LogErrorFormat("Lua/{0}", false, UJLogType.None, new object[] { sTable });
    }

    // Source: Ghidra OnDestroy.c RVA 0x1909934
    // 1. If func@0x18 != null && func.reference > 0: virtual call func.Dispose() (vtable slot 5).
    //    Then func = null.
    // 2. If lua@0x10 != null: LuaState.Dispose() (static thunk). Then lua = null.
    // 3. Panic if lua was null (Ghidra FUN_015cb8fc fallback).
    private void OnDestroy()
    {
        if (func != null && func.IsAlive)
        {
            func.Dispose();
            func = null;
        }
        if (lua != null)
        {
            lua.Dispose();
            lua = null;
            return;
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra .ctor.c RVA 0x190961C
    // 1-1: gameResourceTable = new List<DataPacket>();
    public LuaData()
    {
        gameResourceTable = new List<DataPacket>();
    }
}
