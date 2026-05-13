// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL (IL metadata extraction).
// Bodies: ported 1-1 from work/06_ghidra/decompiled_full/WndForm_Lua/<Method>.c (2026-05-12).
// String literals resolved via work/03_il2cpp_dump/stringliteral.json (numeric idx, not address).

// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x18F5868, 0x18F58B0, 0x18F58B8, 0x18F58C0, 0x18F591C, 0x18F5A94, 0x18F5CE4, 0x18F5E28,
//       0x18F6080, 0x18F62D4, 0x18F6428, 0x18F65DC, 0x18F67FC, 0x18F6800, 0x18F6A18, 0x18F6CB8,
//       0x18F6EF8, 0x18F702C, 0x18F7160, 0x18F7294, 0x18F73C8, 0x18F75BC, 0x18F7CE4, 0x18F8094,
//       0x18F80B4, 0x18F80DC, 0x18F8218, 0x18F844C, 0x18F85F4, 0x18F879C, 0x18F8A78, 0x18F8B48,
//       0x18F8C28, 0x18F8D00, 0x18F8DF4, 0x18F9038, 0x18F903C, 0x18F9040, 0x18F9044, 0x18F9048, 0x18F904C
// Ghidra dir: work/06_ghidra/decompiled_full/WndForm_Lua/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using LuaInterface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-stub  TypeDefIndex: 795
public class WndForm_Lua : WndForm
{
    private static WndForm_Lua s_instance;
    private LuaTable _LuaClass;
    private uint _eWndFormID;
    private string _sWndFormID;
    public static AsyncOperation _opUnloadUnusedAsset;

    // RVA: 0x18F5868  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/get_Instance.c
    public static WndForm_Lua Instance { get { return s_instance; } }

    // RVA: 0x18F58B0  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/get_LuaClass.c
    public LuaTable LuaClass { get { return _LuaClass; } }

    // RVA: 0x18F58B8  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/get_sWndFormID.c
    public string sWndFormID { get { return _sWndFormID; } }

    // RVA: 0x18F58C0  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/.ctor.c — file missing in batch.
    // Source: dump.cs TypeDefIndex 795 — public .ctor() with no args. Base WndForm() (ticked=true).
    // s_instance is assigned by Create flow elsewhere — parameterless ctor here is base wiring only.
    // TODO: confidence:medium — Ghidra .c file not present in decompiled_full/; signature 1-1 from dump.cs.
    public WndForm_Lua() : base()
    {
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_Lua/.ctor.c RVA 0x18F591C
    // 1-1 body (from disassembly):
    //   _sWndFormID = "";                                          // PTR_StringLiteral_0
    //   base..ctor(1, 0);                                          // WndForm_ctor(this, 1, 0)
    //   _eWndFormID = eWndFormID;                                  // *(this + 0x68) = param_2
    //   args = new object[2];                                      // FUN_015cb754(PTR_DAT_03446590=object[], 2)
    //   args[1] = (object)eWndFormID;                              // boxed via thunk_FUN_0155fe44(uint_type, &local_34)
    //   result = Util.CallMethod<object>("EWndFormID"[4925], "GetWndformName"[6104], args, typeof(string));
    //   if (result != null) _sWndFormID = (string)result;          // *plVar8 = lVar5
    // String literals: 4925="EWndFormID" (Lua class name), 6104="GetWndformName" (Lua method).
    public WndForm_Lua(uint eWndFormID) : base()
    {
        _sWndFormID = "";
        _eWndFormID = eWndFormID;
        // args[0] = null, args[1] = boxed eWndFormID (production layout per Ghidra plVar4[5] = lVar5).
        object[] args = new object[2];
        args[1] = (object)eWndFormID;
        object result = LuaFramework.Util.CallMethod<object>("EWndFormID", "GetWndformName", args);
        if (result != null)
        {
            _sWndFormID = (string)result;
        }
    }

    // RVA: 0x18F5A94  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/CreateLuaWnd.c
    // 1-1 from Ghidra: resolves Lua module via Util.CallMethod<object>(_sWndFormID, "Instance", new object[]{"WndForm_Lua"})
    // then registers via WndForm.SetWndFormC + WndForm.InitTables. Returns _LuaClass != null.
    // String literals: 12605="WndForm", 6609="Instance", 10193="SetWndFormC", 6569="InitTables".
    public bool CreateLuaWnd()
    {
        // Ghidra 1-1: Util.CallMethod<object>(_sWndFormID, "Instance", emptyArgs, typeof(LuaTable))
        // — production uses <object> not <LuaTable>. Same fix pattern as BaseProcLua.CreateLuaProc
        // (CallMethod<R> generic strict-cast returns default(R) on Lua-side type mismatch;
        // <object> preserves the lua table reference, then explicit `as LuaTable` cast resolves it).
        object result = LuaFramework.Util.CallMethod<object>(_sWndFormID, "Instance", new object[0]);
        _LuaClass = result as LuaTable;
        if (_LuaClass == null)
        {
            return false;
        }
        // Util.CallMethod("WndForm", "SetWndFormC", new object[]{_LuaClass, this});
        LuaFramework.Util.CallMethod("WndForm", "SetWndFormC", new object[] { _LuaClass, this });
        // Util.CallMethod("WndForm", "InitTables", new object[]{_LuaClass});
        LuaFramework.Util.CallMethod("WndForm", "InitTables", new object[] { _LuaClass });
        return true;
    }

    // RVA: 0x18F5CE4  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/GetPrefab.c
    // 1-1 from Ghidra: Util.CallMethod<object>(_sWndFormID, "GetPrefab", new object[]{_LuaClass, _sWndFormID}, typeof(string))
    // returns string or "" (StringLit_0). String literals: 5972="GetPrefab", 0="".
    public override string GetPrefab(uint eWndFormID, ArrayList args)
    {
        string result = LuaFramework.Util.CallMethod<string>(_sWndFormID, "GetPrefab", new object[] { _LuaClass, _sWndFormID });
        if (result != null)
        {
            return result;
        }
        return string.Empty;
    }

    // RVA: 0x18F5E28  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_Create.c
    // 1-1 from Ghidra: extract args[0] as LuaTable (castclass at PTR_DAT_03460ce0 = LuaTable),
    // call _sWndFormID.V_Create(_LuaClass, luaTableArg) → int (1 == true).
    // If returned true, calls "ProcessBase":"CheckHideMainUI" with empty args.
    // String literals: 12403="V_Create", 4034="CheckHideMainUI", 9214="ProcessBase".
    // PTR_DAT_03460ce0 identified as LuaInterface.LuaTable (cross-referenced from LuaState.PushLuaTable).
    protected override bool V_Create(ArrayList args)
    {
        // Source: Ghidra V_Create.c lines 30-48 — only extract args[0] if args.Count==1, castclass to LuaTable.
        LuaTable arg0 = null;
        if (args != null && args.Count == 1)
        {
            object o = args[0];
            if (o != null)
            {
                // Ghidra: FUN_015cbc7c() (invalid cast throw) if o is not LuaTable.
                arg0 = o as LuaTable;
                if (arg0 == null) throw new System.InvalidCastException("WndForm_Lua.V_Create: args[0] must be LuaTable, got " + o.GetType().Name);
            }
        }
        int rc = LuaFramework.Util.CallMethod<int>(_sWndFormID, "V_Create", new object[] { _LuaClass, arg0 });
        if (rc == 1)
        {
            // Ghidra: LuaFramework_Util__CallMethod("ProcessBase","CheckHideMainUI",
            //   **(undefined8 **)(WndForm_Lua_klass_static_fields + 0xb8), 0);
            // The +0xb8 deref is the static empty object[] singleton — i.e. CallMethod with no extra args.
            LuaFramework.Util.CallMethod("ProcessBase", "CheckHideMainUI", new object[0]);
            return true;
        }
        return false;
    }

    // RVA: 0x18F6080  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_AfterCreate.c
    // 1-1 from Ghidra: Util.CallMethod(_sWndFormID, "V_AfterCreate", [_LuaClass, luaTableArg])
    // Then Util.CallMethod<bool>("ProcessBase", "NeedUnloadUnusedAssetsAfterCreateWndFrom", [this])
    // If true → ResourceUnloader.DoUnloadNow(false). String literals: 12402="V_AfterCreate",
    // 8327="NeedUnloadUnusedAssetsAfterCreateWndFrom", 9214="ProcessBase".
    // Cast target PTR_DAT_03460ce0 = LuaTable (same as V_Create).
    protected override void V_AfterCreate(ArrayList args)
    {
        // Source: Ghidra V_AfterCreate.c lines 30-48 — castclass args[0] to LuaTable.
        LuaTable arg0 = null;
        if (args != null && args.Count == 1)
        {
            object o = args[0];
            if (o != null)
            {
                arg0 = o as LuaTable;
                if (arg0 == null) throw new System.InvalidCastException("WndForm_Lua.V_AfterCreate: args[0] must be LuaTable, got " + o.GetType().Name);
            }
        }
        LuaFramework.Util.CallMethod(_sWndFormID, "V_AfterCreate", new object[] { _LuaClass, arg0 });
        bool needUnload = LuaFramework.Util.CallMethod<bool>("ProcessBase", "NeedUnloadUnusedAssetsAfterCreateWndFrom", new object[] { this });
        if (needUnload)
        {
            ResourceUnloader.DoUnloadNow(false);
        }
    }

    // RVA: 0x18F62D4  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_ProcessKeyClick.c
    // (1-arg overload) — Ghidra file not present in batch; dump.cs Slot 16 says override of WndForm.V_ProcessKeyClick(KeyCode).
    // Ghidra of base WndForm (RVA 0x1A08074) at this slot just returns false. Lua bridge applies in 3-arg overload.
    // TODO: confidence:medium — .c not present in decompiled_full/; behavior mirrors base default per dump.cs slot.
    protected override bool V_ProcessKeyClick(KeyCode keyCode)
    {
        return false;
    }

    // RVA: 0x18F6428  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_ProcessKeyClick.c (overload shared)
    // TODO: confidence:medium — .c not present for 2-arg slot 17; mirrors base default behavior.
    protected override bool V_ProcessKeyClick(KeyCode keyCode, EventType eType)
    {
        return false;
    }

    // RVA: 0x18F65DC  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_ProcessKeyClick.c
    // 1-1 from Ghidra (3-arg overload only — slot 18): Util.CallMethod<int>(_sWndFormID, "V_ProcessKeyClick",
    //   [_LuaClass, (int)keyCode, (int)eType, ctrl], typeof(int)) == 1.
    // String literal 12412="V_ProcessKeyClick".
    protected override bool V_ProcessKeyClick(KeyCode keyCode, EventType eType, bool ctrl)
    {
        int rc = LuaFramework.Util.CallMethod<int>(_sWndFormID, "V_ProcessKeyClick",
            new object[] { _LuaClass, (int)keyCode, (int)eType, ctrl });
        return rc == 1;
    }

    // RVA: 0x18F67FC  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_Update.c — empty body.
    protected override void V_Update(float dTime)
    {
        // Ghidra body: empty function (return;)
    }

    // RVA: 0x18F6800  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/V_Destroy.c
    // 1-1 from Ghidra: Util.CallMethod(_sWndFormID, "V_Destroy", [_LuaClass]); then on Main.Instance.LuaState
    // → Util.CallMethod("ProcessBase","CheckHideMainUI", [Main.Instance._LuaClass]);
    // then Util.CallMethod<bool>("ProcessBase","NeedUnloadUnusedAssetsAfterDestroyWndFrom",[this]) → DoUnloadNow.
    // String literals: 12404="V_Destroy", 4034="CheckHideMainUI", 8328="NeedUnloadUnusedAssetsAfterDestroyWndFrom",
    //                  9214="ProcessBase".
    protected override void V_Destroy()
    {
        LuaFramework.Util.CallMethod(_sWndFormID, "V_Destroy", new object[] { _LuaClass });
        // Ghidra: CheckHideMainUI with the static empty-args singleton (no _LuaClass passed).
        LuaFramework.Util.CallMethod("ProcessBase", "CheckHideMainUI", new object[0]);
        bool needUnload = LuaFramework.Util.CallMethod<bool>("ProcessBase", "NeedUnloadUnusedAssetsAfterDestroyWndFrom", new object[] { this });
        if (needUnload)
        {
            ResourceUnloader.DoUnloadNow(false);
        }
    }

    // RVA: 0x18F6A18  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/WndPropertyData.c
    // 1-1 from Ghidra: if act != Field_Set (1) → log warning + return true.
    // Else Util.CallMethod<int>(_sWndFormID, "WndPropertyData", [_LuaClass, name, index, obj, clear]) == 1.
    // String literals: 12614="WndForm_Lua:WndProperty Only Support WndProperty.EAct.Field_Set",
    //                  12615="WndPropertyData".
    public bool WndPropertyData(WndProperty.EAct act, string name, int index, object obj, bool clear)
    {
        // PHASE B DIAG — TODO: remove after _jobBtnGroup fix verified
        // Use Unity-aware null check (uo != null handles destroyed Unity.Object — uo.name on destroyed throws NRE)
        string objStr;
        if (obj == null) objStr = "<null>";
        else if (obj is UnityEngine.Object uo)
        {
            objStr = (uo != null) ? uo.GetType().Name + ":" + uo.name : uo.GetType().Name + ":<destroyed>";
        }
        else objStr = obj.GetType().Name;
        string luaClassStr = (_LuaClass == null) ? "<NULL>" : "table";
        UnityEngine.Debug.Log($"[WndForm_Lua.WndPropertyData] sWndFormID='{_sWndFormID}' _LuaClass={luaClassStr} act={act} name='{name}' idx={index} obj={objStr} clear={clear}");

        if (act != WndProperty.EAct.Field_Set)
        {
            UJDebug.LogWarning("WndForm_Lua:WndProperty Only Support WndProperty.EAct.Field_Set");
            return true;
        }
        // Source: Ghidra work/06_ghidra/decompiled_full/WndForm_Lua/WndPropertyData.c RVA 0x18f6a18
        // Ghidra calls CallMethod<int>(PTR_StringLiteral_12605="WndForm", PTR_StringLiteral_12615="WndPropertyData", args)
        // — module is the LITERAL "WndForm" (base class name), NOT _sWndFormID.
        // String literal idx 12605: "WndForm" (verified in stringliteral.json).
        // Lua dispatch: WndForm.WndPropertyData(_LuaClass, name, index, obj, clear) — directly to base class.
        int rc = LuaFramework.Util.CallMethod<int>("WndForm", "WndPropertyData",
            new object[] { _LuaClass, name, index, obj, clear });
        UnityEngine.Debug.Log($"[WndForm_Lua.WndPropertyData]   → Lua returned rc={rc}");
        return rc == 1;
    }

    // RVA: 0x18F6CB8  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/BtnClick_CallBack.c
    // 1-1 from Ghidra: Util.CallMethod2(_sWndFormID, sLuaMethod, new object[]{_LuaClass, btn, data, (int)action, intValue, strValue}).
    public void BtnClick_CallBack(string sLuaMethod, Component btn, PointerEventData data, Action_Type action, int intValue, string strValue)
    {
        // DIAG 2026-05-13 — trace which Lua method gets fired per button click so we can see
        // if user is hitting _OnConnectClick (outer), _OnSwitchBtnClick (inner), or
        // _OnLoginWndAreaClick (background) etc.
        string btnPath = "<null>";
        if (btn != null && btn.gameObject != null)
        {
            var sb = new System.Text.StringBuilder(btn.gameObject.name);
            var t = btn.gameObject.transform.parent;
            while (t != null && sb.Length < 200) { sb.Insert(0, t.name + "/"); t = t.parent; }
            btnPath = sb.ToString();
        }
        UnityEngine.Debug.Log($"[DIAG BtnClick_CallBack] wnd='{_sWndFormID}' method='{sLuaMethod}' btn='{btnPath}'");
        LuaFramework.Util.CallMethod2(_sWndFormID, sLuaMethod,
            new object[] { _LuaClass, btn, data, (int)action, intValue, strValue });
    }

    // RVA: 0x18F6EF8  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/BtnDragBegin_CallBack.c
    // 1-1: Util.CallMethod(_sWndFormID, sLuaMethod, [_LuaClass, comp, eData]).
    public void BtnDragBegin_CallBack(string sLuaMethod, Component comp, PointerEventData eData)
    {
        LuaFramework.Util.CallMethod(_sWndFormID, sLuaMethod, new object[] { _LuaClass, comp, eData });
    }

    // RVA: 0x18F702C  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/BtnDrag_CallBack.c
    public void BtnDrag_CallBack(string sLuaMethod, Component comp, PointerEventData eData)
    {
        LuaFramework.Util.CallMethod(_sWndFormID, sLuaMethod, new object[] { _LuaClass, comp, eData });
    }

    // RVA: 0x18F7160  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/BtnDragEnd_CallBack.c
    public void BtnDragEnd_CallBack(string sLuaMethod, Component comp, PointerEventData eData)
    {
        LuaFramework.Util.CallMethod(_sWndFormID, sLuaMethod, new object[] { _LuaClass, comp, eData });
    }

    // RVA: 0x18F7294  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/BtnDrop_CallBack.c
    public void BtnDrop_CallBack(string sLuaMethod, Component comp, PointerEventData eData)
    {
        LuaFramework.Util.CallMethod(_sWndFormID, sLuaMethod, new object[] { _LuaClass, comp, eData });
    }

    // RVA: 0x18F73C8  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/BtnPress_CallBack.c
    // 1-1: Util.CallMethod(_sWndFormID, sLuaMethod, [_LuaClass, comp, boolean, eData, bFastest]).
    public void BtnPress_CallBack(string sLuaMethod, Component comp, bool boolean, PointerEventData eData, bool bFastest)
    {
        LuaFramework.Util.CallMethod(_sWndFormID, sLuaMethod,
            new object[] { _LuaClass, comp, boolean, eData, bFastest });
    }

    // RVA: 0x18F75BC  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/Event_AddListener.c
    // 1-1 from Ghidra switch table: cases 0..9 attach a UnityAction-bound EventCallBack to a UnityEvent
    // on a component fetched by GameObject.GetComponent<T>(). The component / event slot depends on eEventType:
    //   0 → Toggle.onValueChanged (bool)              -> ctor with bool overload
    //   1 → InputField.onValueChange (string)         -> int slot path with string overload (lVar5+0x50)
    //   2 → EventTrigger entry (TriggerEvent<object>) -> trigger.entry with eEventID, action<BaseEventData>
    //   3 → ScrollRect.onValueChanged (Vector2)       -> via Vector2 path at offset 0x138
    //   4 → Dropdown.onValueChanged (int) (offset 0x148 with object route via LAB_019f7b88)
    //   5 → Slider.onValueChanged (float) via Vector2 ctor at offset 0x68 (Slider uses float onChanged)
    //   6 → Scrollbar.onValueChanged (float)
    //   7 → Dropdown (offset 0x140)
    //   8 → InputField.onEndEdit (string)
    //   9 → InputField.onSubmit (string)
    // Slot offsets directly match dump.cs field layouts of each UI component; ported as switch with
    // the per-component event registration. EventCallBack.OnEvent overloads do the Lua dispatch.
    public void Event_AddListener(uint eEventType, uint eEventID, GameObject eventObj, string sLuaMethod)
    {
        if (eventObj == null)
        {
            return;
        }
        switch (eEventType)
        {
            case 0:
            {
                Toggle t = eventObj.GetComponent<Toggle>();
                if (t != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    t.onValueChanged.AddListener(new UnityAction<bool>(cb.OnEvent));
                }
                break;
            }
            case 1:
            {
                InputField inp = eventObj.GetComponent<InputField>();
                if (inp != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    inp.onValueChanged.AddListener(new UnityAction<string>(cb.OnEvent));
                }
                break;
            }
            case 2:
            {
                EventTrigger et = eventObj.GetComponent<EventTrigger>();
                if (et == null)
                {
                    et = eventObj.AddComponent<EventTrigger>();
                }
                var entry = new EventTrigger.Entry();
                entry.eventID = (EventTriggerType)eEventID;
                entry.callback = new EventTrigger.TriggerEvent();
                var cb = new EventCallBack(this, sLuaMethod);
                entry.callback.AddListener(new UnityAction<BaseEventData>(cb.OnEvent));
                if (et.triggers == null)
                {
                    et.triggers = new List<EventTrigger.Entry>();
                }
                et.triggers.Add(entry);
                break;
            }
            case 3:
            {
                ScrollRect sr = eventObj.GetComponent<ScrollRect>();
                if (sr != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    sr.onValueChanged.AddListener(new UnityAction<Vector2>(cb.OnEvent));
                }
                break;
            }
            case 4:
            {
                Dropdown dd = eventObj.GetComponent<Dropdown>();
                if (dd != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    dd.onValueChanged.AddListener(new UnityAction<int>(cb.OnEvent));
                }
                break;
            }
            case 5:
            {
                Slider sl = eventObj.GetComponent<Slider>();
                if (sl != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    sl.onValueChanged.AddListener(new UnityAction<float>(cb.OnEvent));
                }
                break;
            }
            case 6:
            {
                Scrollbar sb = eventObj.GetComponent<Scrollbar>();
                if (sb != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    sb.onValueChanged.AddListener(new UnityAction<float>(cb.OnEvent));
                }
                break;
            }
            case 7:
            {
                Dropdown dd = eventObj.GetComponent<Dropdown>();
                if (dd != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    dd.onValueChanged.AddListener(new UnityAction<int>(cb.OnEvent));
                }
                break;
            }
            case 8:
            {
                InputField inp = eventObj.GetComponent<InputField>();
                if (inp != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    inp.onEndEdit.AddListener(new UnityAction<string>(cb.OnEvent));
                }
                break;
            }
            case 9:
            {
                InputField inp = eventObj.GetComponent<InputField>();
                if (inp != null)
                {
                    var cb = new EventCallBack(this, sLuaMethod);
                    inp.onSubmit.AddListener(new UnityAction<string>(cb.OnEvent));
                }
                break;
            }
            default:
                return;
        }
    }

    // RVA: 0x18F7CE4  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/GetActiveToggles.c
    // 1-1 from Ghidra: builds new List<Toggle>(); iterates ToggleGroup.ActiveToggles(); for each toggle
    // calls toggle.IsActive() and if true adds toggle to list; returns list.ToArray().
    // (Ghidra shows MoveNext + Current + Dispose via virtual table on the IEnumerable<Toggle>.)
    public Toggle[] GetActiveToggles(ToggleGroup toggleGroup)
    {
        List<Toggle> list = new List<Toggle>();
        if (toggleGroup != null)
        {
            IEnumerable<Toggle> active = toggleGroup.ActiveToggles();
            if (active != null)
            {
                foreach (Toggle t in active)
                {
                    if (t != null && t.IsActive())
                    {
                        list.Add(t);
                    }
                }
            }
        }
        return list.ToArray();
    }

    // RVA: 0x18F8094  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/OpenPermissionSetting.c
    public void OpenPermissionSetting()
    {
        var pm = MarsSDK.Permission.PermissionManager.Instance();
        if (pm != null)
        {
            pm.OpenPermissionSetting();
        }
    }

    // RVA: 0x18F80B4  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/CheckPermission.c
    // Ghidra returns void (decompiler dropped the bool — verify against return-cast).
    // dump.cs declares bool — keep bool return, mirror manager call.
    public bool CheckPermission(string permission)
    {
        var pm = MarsSDK.Permission.PermissionManager.Instance();
        if (pm != null)
        {
            return pm.CheckPermission(permission);
        }
        throw new System.NullReferenceException();
    }

    // RVA: 0x18F80DC  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/RequestPermission.c
    // 1-1 from Ghidra: builds a PermissionCallback with three PermissionDelegate constructed from
    // <RequestPermission>b__33_0/1/2 (which are aliases of OnConfirm/OnCancel/OnDenied per Ghidra dump),
    // then PermissionManager.Instance().RequestPermission(req_permission, callback).
    public void RequestPermission(string req_permission)
    {
        var confirm = new MarsSDK.Permission.PermissionCallback.PermissionDelegate(this.OnConfirm);
        var cancel  = new MarsSDK.Permission.PermissionCallback.PermissionDelegate(this.OnCancel);
        var denied  = new MarsSDK.Permission.PermissionCallback.PermissionDelegate(this.OnDenied);
        var cb = new MarsSDK.Permission.PermissionCallback(confirm, cancel, denied);
        var pm = MarsSDK.Permission.PermissionManager.Instance();
        if (pm != null)
        {
            pm.RequestPermission(req_permission, cb);
        }
    }

    // RVA: 0x18F8218  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/RequestPermissions.c
    // 1-1 from Ghidra: build callback (same as above via b__34_0/1/2 aliases); join permissions
    // with brackets "[perm]" and UJDebug.Log("request permission:" + joined); then call manager.
    // String literals: 19489="request permission:", 12769="[", 13178="]", 0="".
    public void RequestPermissions(string[] req_permissions)
    {
        var confirm = new MarsSDK.Permission.PermissionCallback.PermissionDelegate(this.OnConfirm);
        var cancel  = new MarsSDK.Permission.PermissionCallback.PermissionDelegate(this.OnCancel);
        var denied  = new MarsSDK.Permission.PermissionCallback.PermissionDelegate(this.OnDenied);
        var cb = new MarsSDK.Permission.PermissionCallback(confirm, cancel, denied);
        if (req_permissions != null)
        {
            string joined = string.Empty;
            for (int i = 0; i < req_permissions.Length; i++)
            {
                joined = string.Concat(joined, "[", req_permissions[i], "]");
            }
            UJDebug.Log(string.Concat("request permission:", joined));
            var pm = MarsSDK.Permission.PermissionManager.Instance();
            if (pm != null)
            {
                pm.RequestPermissions(req_permissions, cb);
            }
        }
    }

    // RVA: 0x18F844C  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/OnConfirm.c
    // 1-1: join permissions with "###" separator; Util.CallMethod2(_sWndFormID, "V_OnPermissionConfirm", [_LuaClass, joined]).
    // String literals: 374="###", 12410="V_OnPermissionConfirm", 0="".
    public void OnConfirm(string[] permissions)
    {
        if (permissions == null)
        {
            return;
        }
        string joined = string.Empty;
        for (int i = 0; i < permissions.Length; i++)
        {
            if (joined.Length < 1)
            {
                joined = string.Concat(joined, permissions[i]);
            }
            else
            {
                joined = string.Concat(joined, "###", permissions[i]);
            }
        }
        LuaFramework.Util.CallMethod2(_sWndFormID, "V_OnPermissionConfirm", new object[] { _LuaClass, joined });
    }

    // RVA: 0x18F85F4  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/OnCancel.c
    // 1-1: same shape as OnConfirm but Lua method = "V_OnPermissionCancel" (12409).
    public void OnCancel(string[] permissions)
    {
        if (permissions == null)
        {
            return;
        }
        string joined = string.Empty;
        for (int i = 0; i < permissions.Length; i++)
        {
            if (joined.Length < 1)
            {
                joined = string.Concat(joined, permissions[i]);
            }
            else
            {
                joined = string.Concat(joined, "###", permissions[i]);
            }
        }
        LuaFramework.Util.CallMethod2(_sWndFormID, "V_OnPermissionCancel", new object[] { _LuaClass, joined });
    }

    // RVA: 0x18F879C  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/OnDenied.c
    // 1-1: join permissions with "###"; UJDebug.Log("request permission OnDenied:" + joined + " , send to " + _sWndFormID + "V_OnPermissionDenied");
    // then Util.CallMethod2(_sWndFormID, "V_OnPermissionDenied", [_LuaClass, joined]).
    // String literals: 19488="request permission OnDenied:", 12411="V_OnPermissionDenied",
    //                  134=" , send to ", 374="###", 0="".
    public void OnDenied(string[] permissions)
    {
        if (permissions == null)
        {
            return;
        }
        string joined = string.Empty;
        for (int i = 0; i < permissions.Length; i++)
        {
            if (joined.Length < 1)
            {
                joined = string.Concat(joined, permissions[i]);
            }
            else
            {
                joined = string.Concat(joined, "###", permissions[i]);
            }
        }
        UJDebug.Log("request permission OnDenied:" + joined + " , send to " + _sWndFormID + "V_OnPermissionDenied");
        LuaFramework.Util.CallMethod2(_sWndFormID, "V_OnPermissionDenied", new object[] { _LuaClass, joined });
    }

    // RVA: 0x18F8A78  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/LoadRawImage.c
    // 1-1: Main.Instance.StartCoroutine(LoadRawImageCoroutine(url, sLuaMethodd, arg)). Ghidra resolves
    // the MonoBehaviour via Main static lazy-init at offset 0x38 → Main.Instance, then calls
    // *(MonoBehaviour*)(LuaMgr + 0xb8 + 8) → MonoBehaviour. We use Main.Instance directly since
    // Main is a MonoBehaviour root.
    public void LoadRawImage(string url, string sLuaMethodd, string arg)
    {
        if (Main.Instance == null)
        {
            throw new System.NullReferenceException();
        }
        Main.Instance.StartCoroutine(LoadRawImageCoroutine(url, sLuaMethodd, arg));
    }

    // RVA: 0x18F8B48  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/LoadRawImageCoroutine.c
    // RVA: 0x18F94C8  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua.<LoadRawImageCoroutine>d__39/MoveNext.c
    // Source: Ghidra — d__39.MoveNext state machine translated 1-1 as C# IEnumerator.
    //   state 0: ts = Main.GetNowTimeStamp();
    //            fullUrl = String.Format("{0}?v={1}", ResourcesPath.PatchHost + url, (uint)ts);
    //            UJDebug.LogFormat("LoadRawImage:{0}", new object[]{fullUrl});
    //            www = UnityWebRequestTexture.GetTexture(fullUrl); yield www.SendWebRequest();
    //   state 1: if (!IsNullOrEmpty(www.error)) UJDebug.Log("LoadRawImage error:"+www.error); tex=null;
    //            else { dh = www.downloadHandler;  tex = ((DownloadHandlerTexture)dh).texture; }
    //            args = new object[3] { _LuaClass, tex, arg };
    //            LuaFramework.Util.CallMethod(_sWndFormID, sLuaMethodd, args);  (yield break implicit)
    // String literal addresses (resolved via stringliteral.json by index in IL2CPP metadata):
    //   PTR_StringLiteral_7686 — "LoadRawImage:{0}" (UJDebug.LogFormat spec).
    //   PTR_StringLiteral_21255 — "{0}?v={1}" (URL Format spec).
    //   PTR_StringLiteral_2643 — "LoadRawImage error:" (concat prefix).
    public IEnumerator LoadRawImageCoroutine(string url, string sLuaMethodd, string arg)
    {
        // state 0 — entry
        int ts = global::Main.GetNowTimeStamp();
        // Build URL: ResourcesPath.PatchHost concatenated with the per-call url, then Format
        // with the numeric (uint-boxed) timestamp as `v=` query param.
        string baseUrl = string.Concat(ResourcesPath.PatchHost, url);
        // TODO: PTR_StringLiteral_21255 — "{0}?v={1}" Format spec
        string fullUrl = string.Format("{0}?v={1}", baseUrl, (uint)ts);
        // TODO: PTR_StringLiteral_7686 — "LoadRawImage:{0}" LogFormat spec
        UJDebug.LogFormat("LoadRawImage:{0}", false, UJLogType.None, new object[] { fullUrl });
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(fullUrl);
        if (www == null)
        {
            throw new System.NullReferenceException();
        }
        yield return www.SendWebRequest();

        // state 1 resume
        if (www == null)
        {
            throw new System.NullReferenceException();
        }
        string err = www.error;
        bool errEmpty = string.IsNullOrEmpty(err);
        Texture tex = null;
        if (!errEmpty)
        {
            // TODO: PTR_StringLiteral_2643 — "LoadRawImage error:" prefix
            UJDebug.Log(string.Concat("LoadRawImage error:", www.error));
        }
        else
        {
            DownloadHandler dh = www.downloadHandler;
            // Ghidra: cast check to DownloadHandlerTexture (PTR_DAT_03458b88). If null
            // downloadHandler, error branch is taken (tex remains null).
            if (dh != null)
            {
                if (!(dh is DownloadHandlerTexture))
                {
                    throw new System.InvalidCastException();
                }
                tex = ((DownloadHandlerTexture)dh).texture;
            }
        }
        // args = new object[3] { _LuaClass, tex_or_null, arg };
        object[] cbArgs = new object[3];
        cbArgs[0] = this._LuaClass;
        cbArgs[1] = tex;
        cbArgs[2] = arg;
        LuaFramework.Util.CallMethod(this._sWndFormID, sLuaMethodd, cbArgs);
    }

    // RVA: 0x18F8C28  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/LoadRawImage2.c
    // 1-1: Main.Instance.StartCoroutine(LoadRawImageCoroutine2(url, sLuaMethodd, arg, timeStamp)).
    public void LoadRawImage2(string url, string sLuaMethodd, string arg, string timeStamp)
    {
        if (Main.Instance == null)
        {
            throw new System.NullReferenceException();
        }
        Main.Instance.StartCoroutine(LoadRawImageCoroutine2(url, sLuaMethodd, arg, timeStamp));
    }

    // RVA: 0x18F8D00  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/LoadRawImageCoroutine2.c
    // RVA: 0x18F9054  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua.<LoadRawImageCoroutine2>d__41/MoveNext.c
    // Source: Ghidra — d__41.MoveNext state machine translated 1-1. Mirrors d__39 except:
    //   - takes string `timeStamp` (already-formatted) instead of using Main.GetNowTimeStamp();
    //   - when timeStamp == null: uses StringLiteral_21173 (Format spec without "?v=" component);
    //   - else: uses StringLiteral_21255 ("{0}?v={1}") with timeStamp as string parameter.
    // String literal addresses:
    //   PTR_StringLiteral_7686  — "LoadRawImage:{0}" (LogFormat).
    //   PTR_StringLiteral_21173 — Format spec for url-only path (likely "{0}" or similar no-version).
    //   PTR_StringLiteral_21255 — "{0}?v={1}" (Format with version).
    //   PTR_StringLiteral_2643  — "LoadRawImage error:" (concat prefix).
    //   PTR_StringLiteral_0     — empty string literal (referenced but not consumed in main path).
    public IEnumerator LoadRawImageCoroutine2(string url, string sLuaMethodd, string arg, string timeStamp)
    {
        // state 0 — entry. NOTE: no Main.GetNowTimeStamp() call — uses timeStamp param directly.
        string baseUrl = string.Concat(ResourcesPath.PatchHost, url);
        string fullUrl;
        if (timeStamp == null)
        {
            // TODO: PTR_StringLiteral_21173 — Format spec (confidence:low, likely "{0}" pass-through)
            fullUrl = string.Format("{0}", baseUrl);
        }
        else
        {
            // TODO: PTR_StringLiteral_21255 — "{0}?v={1}"
            fullUrl = string.Format("{0}?v={1}", baseUrl, timeStamp);
        }
        // TODO: PTR_StringLiteral_7686 — "LoadRawImage:{0}"
        UJDebug.LogFormat("LoadRawImage:{0}", false, UJLogType.None, new object[] { fullUrl });
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(fullUrl);
        if (www == null)
        {
            throw new System.NullReferenceException();
        }
        yield return www.SendWebRequest();

        // state 1 resume
        if (www == null)
        {
            throw new System.NullReferenceException();
        }
        string err = www.error;
        bool errEmpty = string.IsNullOrEmpty(err);
        Texture tex = null;
        if (!errEmpty)
        {
            UJDebug.Log(string.Concat("LoadRawImage error:", www.error));
        }
        else
        {
            DownloadHandler dh = www.downloadHandler;
            if (dh != null)
            {
                if (!(dh is DownloadHandlerTexture))
                {
                    throw new System.InvalidCastException();
                }
                tex = ((DownloadHandlerTexture)dh).texture;
            }
        }
        object[] cbArgs = new object[3];
        cbArgs[0] = this._LuaClass;
        cbArgs[1] = tex;
        cbArgs[2] = arg;
        LuaFramework.Util.CallMethod(this._sWndFormID, sLuaMethodd, cbArgs);
    }

    // RVA: 0x18F8DF4  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/SetTextWithEllipsis.c
    // 1-1 from Ghidra: strip "<.*?>" via Regex.Replace; replace "\xa0" (NBSP) with " ";
    //   build TextGenerator; if RectTransform present, GetGenerationSettings then Populate(replaced, settings);
    //   if characterCountVisible < replaced.Length AND visible-2 > 0 →
    //     substring(stripped, 0, visible-2) + "…"; then original_value = original_value.Replace(stripped, ellipsisStr);
    //   textComponent.text = original_value;
    // String literals: 2310="<.*?>", 108=" ", 21294=NBSP (\xa0), 21390="…".
    public void SetTextWithEllipsis(Text textComponent, string value)
    {
        if (textComponent == null)
        {
            throw new System.NullReferenceException();
        }
        string stripped = System.Text.RegularExpressions.Regex.Replace(value, "<.*?>", " ");
        if (stripped == null)
        {
            throw new System.NullReferenceException();
        }
        string replaced = stripped.Replace(" ", " ");
        TextGenerator gen = new TextGenerator();
        RectTransform rt = textComponent.GetComponent<RectTransform>();
        if (rt == null)
        {
            throw new System.NullReferenceException();
        }
        Rect rect = rt.rect;
        TextGenerationSettings settings = textComponent.GetGenerationSettings(rect.size);
        gen.Populate(replaced, settings);
        int visible = gen.characterCountVisible;
        if (replaced != null)
        {
            if (visible < replaced.Length && (visible - 2) > 0)
            {
                string trimmed = stripped.Substring(0, visible - 2);
                string ellipsisStr = string.Concat(trimmed, "…");
                if (value == null)
                {
                    throw new System.NullReferenceException();
                }
                value = value.Replace(stripped, ellipsisStr);
            }
            textComponent.text = value;
        }
    }

    // RVA: 0x18F9038  Ghidra: work/06_ghidra/decompiled_full/WndForm_Lua/<RequestPermission>b__33_0.c
    // 1-1: body identical to OnConfirm (compiler-generated closure used as PermissionDelegate target).
    // Source: Ghidra confirms b__33_0 == OnConfirm body verbatim.
    private void RequestPermission_b__33_0(string[] permissions)
    {
        OnConfirm(permissions);
    }

    // RVA: 0x18F903C  Ghidra: <RequestPermission>b__33_1.c — body identical to OnCancel.
    private void RequestPermission_b__33_1(string[] permissions)
    {
        OnCancel(permissions);
    }

    // RVA: 0x18F9040  Ghidra: <RequestPermission>b__33_2.c — body identical to OnDenied.
    private void RequestPermission_b__33_2(string[] permissions)
    {
        OnDenied(permissions);
    }

    // RVA: 0x18F9044  Ghidra: <RequestPermissions>b__34_0.c — body identical to OnConfirm.
    private void RequestPermissions_b__34_0(string[] permissions)
    {
        OnConfirm(permissions);
    }

    // RVA: 0x18F9048  Ghidra: <RequestPermissions>b__34_1.c — body identical to OnCancel.
    private void RequestPermissions_b__34_1(string[] permissions)
    {
        OnCancel(permissions);
    }

    // RVA: 0x18F904C  Ghidra: <RequestPermissions>b__34_2.c — body identical to OnDenied.
    private void RequestPermissions_b__34_2(string[] permissions)
    {
        OnDenied(permissions);
    }
}
