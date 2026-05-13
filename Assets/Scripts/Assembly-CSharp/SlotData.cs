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

    // Source: dump.cs RVA 0x1A028D0 — no Ghidra .ctor.c (empty body inferred per default pattern).
    public SlotData() { }

    // Source: dump.cs RVA 0x1A0298C — copy ctor. No standalone .c in decompiled_full/SlotData/.
    // Body inferred from Clone.c usage (which calls this overload to duplicate `this`): canonical
    // field-by-field copy. Confidence:medium — alternative would be partial copy but no evidence.
    public SlotData(SlotData src)
    {
        if (src == null) return;
        this.nSlotDataID    = src.nSlotDataID;
        this.strIcon        = src.strIcon;
        this.nSlotType      = src.nSlotType;
        this.nCount         = src.nCount;
        this.CountText      = src.CountText;
        this.RightTopText   = src.RightTopText;
        this.nParam1        = src.nParam1;
        this.nParam2        = src.nParam2;
        this.nParam3        = src.nParam3;
        this._bLock         = src._bLock;
        this.LeftBottomTag  = src.LeftBottomTag;
        this.LeftTopTag     = src.LeftTopTag;
        this.RightTopTag    = src.RightTopTag;
        this.RestrictColorTag = src.RestrictColorTag;
        this.luaData        = src.luaData;
    }

    // Source: Ghidra Clone.c  RVA 0x1A02A5C
    // Allocates new SlotData via il2cpp_new(SlotData_TypeInfo), then calls SlotData..ctor(this, src=param_1).
    // C# equivalent: return new SlotData(this);
    public SlotData Clone()
    {
        return new SlotData(this);
    }
}
