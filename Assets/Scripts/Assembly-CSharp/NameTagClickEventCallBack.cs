// Source: Ghidra work/06_ghidra/decompiled_full/NameTagClickEventCallBack/{_ctor,OnEvent}.c
// RVAs: ctor 0x17B7A1C, OnEvent 0x17B7A44.
// dump.cs TypeDefIndex 103: field _uid @ 0x10.
//
// Created by BaseProcLua.Event_NameTagClickListener (RVA 0x15E39A8) when an entity name-tag
// receives PointerClick — `cb = new NameTagClickEventCallBack(UID)` then
// `trigger.callback.AddListener(new UnityAction<BaseEventData>(cb.OnEvent))`.
// The OnEvent body calls `LuaFramework.Util.CallMethod("ProcessBase", "OnNameTagClick", [UID])`
// so the click is dispatched to Lua's `ProcessBase.OnNameTagClick(self, uid)`.
//
// String literals resolved from work/03_il2cpp_dump/script.json:
//   index 8724 → "OnNameTagClick"
//   index 9214 → "ProcessBase"

using UnityEngine.EventSystems;

public class NameTagClickEventCallBack
{
    // _uid @ 0x10
    private int _uid;

    // Source: Ghidra _ctor.c RVA 0x17B7A1C
    // 1-1: System.Object..ctor(this); *(this + 0x10) = UID;
    public NameTagClickEventCallBack(int UID)
    {
        this._uid = UID;
    }

    // Source: Ghidra OnEvent.c RVA 0x17B7A44
    // 1-1:
    //   args = new object[1];           // FUN_015cb754(System.Object[]_type, 1)
    //   args[0] = box(_uid);            // thunk_FUN_0155fe44(System.Int32_type, &_uid)
    //   LuaFramework.Util.CallMethod("ProcessBase", "OnNameTagClick", args);
    public void OnEvent(BaseEventData eventData)
    {
        object[] args = new object[1];
        args[0] = _uid;
        LuaFramework.Util.CallMethod("ProcessBase", "OnNameTagClick", args);
    }
}
