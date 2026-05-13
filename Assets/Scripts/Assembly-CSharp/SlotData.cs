// Source: Ghidra work/06_ghidra/decompiled_full/SlotData/ (1 .c: Clone)
// Source: dump.cs TypeDefIndex 288
// Note: SlotData/.ctor.c and SlotData/.ctor(SlotData).c are NOT in decompiled_full/.
// no-arg ctor is the default empty body; copy ctor body inferred from Clone usage (canonical
// field-by-field copy — the only sensible semantic given dump.cs explicit ctor RVA + Clone.c
// calling it). Documented per CLAUDE.md §D6 confidence:medium.

using System;

[Serializable]
public class SlotData
{
    public int nSlotDataID;       // 0x10
    public string strIcon;        // 0x18
    public int nSlotType;         // 0x20
    public int nCount;            // 0x24
    public string CountText;      // 0x28
    public string RightTopText;   // 0x30
    public int nParam1;           // 0x38
    public int nParam2;           // 0x3C
    public int nParam3;           // 0x40
    public bool _bLock;           // 0x44
    public string LeftBottomTag;  // 0x48
    public string LeftTopTag;     // 0x50
    public string RightTopTag;    // 0x58
    public string RestrictColorTag; // 0x60
    public object luaData;        // 0x68

    // Source: Ghidra work/06_ghidra/decompiled_rva/SlotData___ctor.c RVA 0x1A028D0
    // 1-1 port: base.ctor(); _bLock=false; then init ALL 7 string fields to ""
    // (PTR_StringLiteral_0). Without these, fields default to null and Lua-side
    // SetSlotData's `if slotData.strIcon == "" then ...lookup...` skips (nil != "")
    // → strIcon stays null → UISlot.SetSlotDataLua passes null to IconTextureMgr →
    // "The input asset name cannot be null" throw at AssetBundleOP.LoadAsync.
    public SlotData()
    {
        this._bLock = false;
        this.strIcon          = string.Empty;
        this.CountText        = string.Empty;
        this.RightTopText     = string.Empty;
        this.LeftBottomTag    = string.Empty;
        this.LeftTopTag       = string.Empty;
        this.RightTopTag      = string.Empty;
        this.RestrictColorTag = string.Empty;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/SlotData___ctor_copy.c RVA 0x1A0298C
    // 1-1: base.ctor(); if (src==null) NRE; else field-by-field copy of 15 fields
    // (matches Ghidra offsets 0x10, 0x18, 0x20, 0x28, 0x30, 0x38, 0x40, 0x44, 0x48,
    // 0x50, 0x58, 0x60, 0x68).
    public SlotData(SlotData src)
    {
        if (src == null) throw new System.NullReferenceException();  // Ghidra FUN_015cb8fc
        this.nSlotDataID    = src.nSlotDataID;       // 0x10
        this.strIcon        = src.strIcon;           // 0x18
        this.nSlotType      = src.nSlotType;         // 0x20
        this.nCount         = src.nCount;            // 0x24 — int32 read at 0x28? No: 0x24 nCount + 0x28 CountText
        this.CountText      = src.CountText;         // 0x28
        this.RightTopText   = src.RightTopText;     // 0x30
        this.nParam1        = src.nParam1;           // 0x38 — Ghidra reads undefined8 at 0x38 (covers 0x38 + 0x3C if both int32)
        this.nParam2        = src.nParam2;           // 0x3C
        this.nParam3        = src.nParam3;           // 0x40
        this._bLock         = src._bLock;            // 0x44 byte
        this.LeftBottomTag  = src.LeftBottomTag;     // 0x48
        this.LeftTopTag     = src.LeftTopTag;        // 0x50
        this.RightTopTag    = src.RightTopTag;       // 0x58
        this.RestrictColorTag = src.RestrictColorTag;// 0x60
        this.luaData        = src.luaData;           // 0x68
    }

    // Source: Ghidra Clone.c  RVA 0x1A02A5C
    // Allocates new SlotData via il2cpp_new(SlotData_TypeInfo), then calls SlotData..ctor(this, src=param_1).
    // C# equivalent: return new SlotData(this);
    public SlotData Clone()
    {
        return new SlotData(this);
    }
}
