// Source: Ghidra work/06_ghidra/decompiled_full/WndProperty/ (6 .c) — all 6 methods ported 1-1.
// Source: dump.cs TypeDefIndex 217
// PTR_DAT_0345e010 = typeof(WndForm_Lua); PTR_DAT_034464c0 = IList interface handle (for indexer dispatch).
// Reflection-based property/field navigation with optional array/IList indexer chaining.

using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Progs/Property Assign")]
public class WndProperty : IWndComponent
{
    public WndProperty.PropertyDecl[] _properties;  // 0x20

    // Reflection binding flags — Ghidra uses 0x34 = Public(0x18) | NonPublic(0x20) | Static(0x4).
    // For instance fields we also need Instance(0x4). Use full set.
    private const BindingFlags BIND_ANY = BindingFlags.Public | BindingFlags.NonPublic
                                        | BindingFlags.Instance | BindingFlags.Static;

    // Source: Ghidra InitComponent.c  RVA 0x195CFA8
    // For each non-null prop with non-empty _name:
    //   owner = (first iter) wnd, else propagated by GetField/GetProperty walk
    //   If owner.GetType() is WndForm_Lua subclass: WndPropertyData(act, name, index, obj, bClear=false)
    //   Else switch _act: 0=GetField, 1=SetField(false), 2=GetProperty, 3=SetProperty(false)
    //   On false return → stop iteration.
    public override void InitComponent(WndForm wnd)
    {
        // PHASE B DIAG — TODO: remove after _jobBtnGroup fix verified
        var hostName = (this != null && this.gameObject != null) ? this.gameObject.name : "<null>";
        var wndName = (wnd != null) ? wnd.GetType().Name : "<null>";
        int propsLen = (_properties != null) ? _properties.Length : -1;
        Debug.Log($"[WndProperty.InitComponent] host={hostName} wnd={wndName} props={propsLen}");

        if (wnd == null) return;
        if (_properties == null) return;
        object owner = null;
        for (int i = 0; i < _properties.Length; i++)
        {
            var prop = _properties[i];
            if (prop == null) continue;
            if (string.IsNullOrEmpty(prop._name)) continue;
            if (owner == null) owner = wnd;
            Type t = owner.GetType();

            // PHASE B DIAG — Unity-aware null check (destroyed Object.name throws NRE)
            string objStr;
            if (prop._object == null) objStr = "<null>";
            else
            {
                var po = prop._object;  // UnityEngine.Object
                objStr = (po != null) ? po.GetType().Name + ":" + po.name : po.GetType().Name + ":<destroyed>";
            }
            Debug.Log($"[WndProperty.InitComponent]   prop[{i}] act={prop._act} name='{prop._name}' idx={prop._index} obj={objStr} ownerType={t.Name}");

            if (t == typeof(WndForm_Lua) || t.IsSubclassOf(typeof(WndForm_Lua)))
            {
                WndForm_Lua wfl = owner as WndForm_Lua;
                if (wfl == null) throw new System.NullReferenceException();
                bool ok = wfl.WndPropertyData(prop._act, prop._name, prop._index, prop._object, false);
                Debug.Log($"[WndProperty.InitComponent]   → WndPropertyData(Lua) returned ok={ok}");
                if (!ok) return;
                continue;
            }
            bool result;
            switch ((int)prop._act)
            {
                case 0: result = GetField(ref owner, prop); break;
                case 1: result = SetField(ref owner, prop, false); break;
                case 2: result = GetProperty(ref owner, prop); break;
                case 3: result = SetProperty(ref owner, prop, false); break;
                default: continue;
            }
            if (!result) return;
        }
    }

    // Source: Ghidra DinitComponent.c  RVA 0x19600D0
    // Mirror of InitComponent with bClear=true on Field_Set/Property_Set actions.
    public override void DinitComponent(WndForm wnd)
    {
        if (wnd == null) return;
        if (_properties == null) return;
        object owner = null;
        for (int i = 0; i < _properties.Length; i++)
        {
            var prop = _properties[i];
            if (prop == null) continue;
            if (string.IsNullOrEmpty(prop._name)) continue;
            if (owner == null) owner = wnd;
            Type t = owner.GetType();
            if (t == typeof(WndForm_Lua) || t.IsSubclassOf(typeof(WndForm_Lua)))
            {
                WndForm_Lua wfl = owner as WndForm_Lua;
                if (wfl == null) throw new System.NullReferenceException();
                wfl.WndPropertyData(prop._act, prop._name, prop._index, prop._object, true);
                continue;
            }
            bool result;
            switch ((int)prop._act)
            {
                case 0: result = GetField(ref owner, prop); break;
                case 1: result = SetField(ref owner, prop, true); break;
                case 2: result = GetProperty(ref owner, prop); break;
                case 3: result = SetProperty(ref owner, prop, true); break;
                default: continue;
            }
            if (!result) return;
        }
    }

    // Source: Ghidra GetField.c  RVA 0x195D1D4
    // Get field by name → read value → if array, index by prop._index; if IList, invoke get_Item.
    private bool GetField(ref object owner, WndProperty.PropertyDecl prop)
    {
        if (owner == null) return false;
        Type t = owner.GetType();
        if (prop == null) return false;
        FieldInfo fi = t.GetField(prop._name, BIND_ANY);
        if (fi == null) return false;
        object val = fi.GetValue(owner);
        owner = val;
        if (owner == null) return false;
        Type ft = fi.FieldType;
        if (ft.IsArray)
        {
            Array arr = owner as Array;
            if (arr == null) return false;
            if (prop._index < 0 || prop._index >= arr.Length) return false;
            owner = arr.GetValue(prop._index);
            return owner != null;
        }
        if (owner is IList list)
        {
            if (prop._index < 0 || prop._index >= list.Count) return false;
            owner = list[prop._index];
            return owner != null;
        }
        return true;
    }

    // Source: Ghidra SetField.c  RVA 0x195D804
    // If bClear: set field to null/default. Else: navigate (array/IList) and assign prop._object.
    private bool SetField(ref object owner, WndProperty.PropertyDecl prop, bool bClear)
    {
        if (owner == null) return false;
        Type t = owner.GetType();
        if (prop == null) return false;
        FieldInfo fi = t.GetField(prop._name, BIND_ANY);
        if (fi == null) return false;
        Type ft = fi.FieldType;

        if (bClear)
        {
            // For arrays/IList: zero out element at index. For scalar: set field to null/default.
            object val = fi.GetValue(owner);
            if (ft.IsArray)
            {
                Array arr = val as Array;
                if (arr == null) return false;
                if (prop._index < 0 || prop._index >= arr.Length) return false;
                arr.SetValue(null, prop._index);
                return true;
            }
            if (val is IList list)
            {
                if (prop._index < 0 || prop._index >= list.Count) return false;
                list[prop._index] = null;
                return true;
            }
            fi.SetValue(owner, ft.IsValueType ? Activator.CreateInstance(ft) : null);
            return true;
        }

        // bClear == false: assign prop._object.
        object curr = fi.GetValue(owner);
        if (ft.IsArray)
        {
            Array arr = curr as Array;
            if (arr == null) return false;
            if (prop._index < 0 || prop._index >= arr.Length) return false;
            arr.SetValue(prop._object, prop._index);
            return true;
        }
        if (curr is IList listw)
        {
            if (prop._index < 0 || prop._index >= listw.Count) return false;
            listw[prop._index] = prop._object;
            return true;
        }
        fi.SetValue(owner, prop._object);
        return true;
    }

    // Source: Ghidra GetProperty.c  RVA 0x195EB6C
    // Same as GetField but uses Type.GetProperty + PropertyInfo.GetValue.
    private bool GetProperty(ref object owner, WndProperty.PropertyDecl prop)
    {
        if (owner == null) return false;
        Type t = owner.GetType();
        if (prop == null) return false;
        PropertyInfo pi = t.GetProperty(prop._name, BIND_ANY);
        if (pi == null) return false;
        object val = pi.GetValue(owner, null);
        owner = val;
        if (owner == null) return false;
        Type ft = pi.PropertyType;
        if (ft.IsArray)
        {
            Array arr = owner as Array;
            if (arr == null) return false;
            if (prop._index < 0 || prop._index >= arr.Length) return false;
            owner = arr.GetValue(prop._index);
            return owner != null;
        }
        if (owner is IList list)
        {
            if (prop._index < 0 || prop._index >= list.Count) return false;
            owner = list[prop._index];
            return owner != null;
        }
        return true;
    }

    // Source: Ghidra SetProperty.c  RVA 0x195F42C
    // Same as SetField but uses PropertyInfo.SetValue.
    private bool SetProperty(ref object owner, WndProperty.PropertyDecl prop, bool bClear)
    {
        if (owner == null) return false;
        Type t = owner.GetType();
        if (prop == null) return false;
        PropertyInfo pi = t.GetProperty(prop._name, BIND_ANY);
        if (pi == null) return false;
        Type ft = pi.PropertyType;

        if (bClear)
        {
            object val = pi.GetValue(owner, null);
            if (ft.IsArray)
            {
                Array arr = val as Array;
                if (arr == null) return false;
                if (prop._index < 0 || prop._index >= arr.Length) return false;
                arr.SetValue(null, prop._index);
                return true;
            }
            if (val is IList list)
            {
                if (prop._index < 0 || prop._index >= list.Count) return false;
                list[prop._index] = null;
                return true;
            }
            pi.SetValue(owner, ft.IsValueType ? Activator.CreateInstance(ft) : null, null);
            return true;
        }

        object curr = pi.GetValue(owner, null);
        if (ft.IsArray)
        {
            Array arr = curr as Array;
            if (arr == null) return false;
            if (prop._index < 0 || prop._index >= arr.Length) return false;
            arr.SetValue(prop._object, prop._index);
            return true;
        }
        if (curr is IList listw)
        {
            if (prop._index < 0 || prop._index >= listw.Count) return false;
            listw[prop._index] = prop._object;
            return true;
        }
        pi.SetValue(owner, prop._object, null);
        return true;
    }

    // Source: Ghidra .ctor.c  RVA 0x19602FC — empty body (default).
    public WndProperty() { }

    // Source: dump.cs TypeDefIndex 215
    public enum EAct
    {
        Field_Get = 0,
        Field_Set = 1,
        Property_Get = 2,
        Property_Set = 3,
    }

    // Source: dump.cs TypeDefIndex 216
    [Serializable]
    public class PropertyDecl
    {
        public WndProperty.EAct _act;          // 0x10
        public string _name;                    // 0x18
        public int _index;                      // 0x20
        public UnityEngine.Object _object;      // 0x28

        // Source: Ghidra PropertyDecl/.ctor.c RVA 0x1960304 — _act = Field_Set; _name = "".
        public PropertyDecl()
        {
            _act = WndProperty.EAct.Field_Set;
            _name = string.Empty;
        }
    }
}
