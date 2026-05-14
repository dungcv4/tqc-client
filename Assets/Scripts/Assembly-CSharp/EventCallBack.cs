// Source: Ghidra work/06_ghidra/decompiled_full/EventCallBack/_ctor.c + 6 OnEvent_*.c
// RVAs: ctor 0x18F50E4, OnEvent(bool) 0x18F5170, OnEvent(int) 0x18F52AC,
//        OnEvent(string) 0x18F53E4, OnEvent(BaseEventData) 0x18F54E8,
//        OnEvent(Vector2) 0x18F55EC, OnEvent(float) 0x18F5730.
// dump.cs TypeDefIndex: 792 (fields _parent@0x10, _sLuaMethod@0x18).
//
// Used by WndForm_Lua.Event_AddListener (RVA 0x18F75BC): for each event-type case the
// runtime constructs `new EventCallBack(this, sLuaMethod)` then wires
// `new UnityAction<T>(cb.OnEvent)` to the matching UnityEvent on the bound component.
// When the Unity event fires, the OnEvent overload boxes its argument and calls
// `LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, [_parent.LuaClass, arg])`
// so Lua picks up the dispatch via `<sWndFormID>.<luaMethod>(self, arg)`.

using UnityEngine;
using UnityEngine.EventSystems;

public class EventCallBack
{
    // _parent @ 0x10  (Ghidra: *(long*)(this + 0x10) = WndForm_Lua instance)
    private WndForm_Lua _parent;

    // _sLuaMethod @ 0x18 (initialized to PTR_StringLiteral_0_034465a0 in ctor, then overwritten by param_3)
    private string _sLuaMethod;

    // Source: Ghidra _ctor.c RVA 0x18F50E4
    // 1-1 mapping:
    //   *(this + 0x18) = empty-string literal;      // initialize _sLuaMethod to ""
    //   System_Object..ctor(this);                  // base ctor — C# auto, no explicit call needed
    //   *(this + 0x10) = parent;                    // _parent = parent
    //   *(this + 0x18) = sLuaMethod;                // _sLuaMethod = sLuaMethod (overrides the "" init)
    public EventCallBack(WndForm_Lua parent, string sLuaMethod)
    {
        this._sLuaMethod = string.Empty;
        this._parent = parent;
        this._sLuaMethod = sLuaMethod;
    }

    // Source: Ghidra OnEvent_bool.c RVA 0x18F5170
    // 1-1 mapping:
    //   if (_parent == null) throw NullReferenceException();    // Ghidra: FUN_015cb8fc at fall-through
    //   sWndFormID  = _parent + 0x70;                // _parent._sWndFormID
    //   sLuaMethod  = this + 0x18;                   // _sLuaMethod
    //   args        = new object[2];                 // FUN_015cb754(System.Object[]_type, 2)
    //   lvar        = _parent + 0x60;                // _parent._LuaClass
    //   args[0]     = lvar;                          // checked cast to System.Object (always succeeds)
    //   args[1]     = box(bOn);                      // System.Boolean box via thunk_FUN_0155fe44
    //   LuaFramework.Util.CallMethod(sWndFormID, sLuaMethod, args);
    // Note: parent.LuaClass property returns _LuaClass (private field at 0x60).
    //       parent.sWndFormID property returns _sWndFormID (private field at 0x70).
    public void OnEvent(bool bOn)
    {
        if (_parent == null) throw new System.NullReferenceException();
        object[] args = new object[2];
        args[0] = _parent.LuaClass;
        args[1] = bOn;
        LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, args);
    }

    // Source: Ghidra OnEvent_int.c RVA 0x18F52AC — same shape, args[1] = box(iIdx).
    public void OnEvent(int iIdx)
    {
        if (_parent == null) throw new System.NullReferenceException();
        object[] args = new object[2];
        args[0] = _parent.LuaClass;
        args[1] = iIdx;
        LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, args);
    }

    // Source: Ghidra OnEvent_string.c RVA 0x18F53E4 — args[1] = value (reference type, no boxing).
    public void OnEvent(string value)
    {
        if (_parent == null) throw new System.NullReferenceException();
        object[] args = new object[2];
        args[0] = _parent.LuaClass;
        args[1] = value;
        LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, args);
    }

    // Source: Ghidra OnEvent_BaseEventData.c RVA 0x18F54E8 — args[1] = eventData (reference type).
    public void OnEvent(BaseEventData eventData)
    {
        if (_parent == null) throw new System.NullReferenceException();
        object[] args = new object[2];
        args[0] = _parent.LuaClass;
        args[1] = eventData;
        LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, args);
    }

    // Source: Ghidra OnEvent_Vector2.c RVA 0x18F55EC — args[1] = box(pos), Vector2 struct.
    public void OnEvent(Vector2 pos)
    {
        if (_parent == null) throw new System.NullReferenceException();
        object[] args = new object[2];
        args[0] = _parent.LuaClass;
        args[1] = pos;
        LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, args);
    }

    // Source: Ghidra OnEvent_float.c RVA 0x18F5730 — args[1] = box(value), float.
    public void OnEvent(float value)
    {
        if (_parent == null) throw new System.NullReferenceException();
        object[] args = new object[2];
        args[0] = _parent.LuaClass;
        args[1] = value;
        LuaFramework.Util.CallMethod(_parent.sWndFormID, _sLuaMethod, args);
    }
}
