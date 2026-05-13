// Source: work/03_il2cpp_dump/dump.cs class 'TResObjectPool'
// Bodies ported 1-1 from Ghidra decompiled_rva/TResObjectPool_oo__*.c (object,object instantiation).
// See // Source: comments per method.

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

// Source: Il2CppDumper-stub  TypeDefIndex: 780
public class TResObjectPool<K, T>
{
    protected List<TResObject<K, T>> listResObject;
    protected int _sizeMax;
    public float timeDefault;

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__UpdateObject.c RVA 0x0245edb8
    // Empty body (decompile shows only `return;`).
    protected virtual void UpdateObject(TResObject<K, T> resObject, float elapsedTime)
    {
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__DestroyObject.c RVA 0x0245edbc
    // Empty body (decompile shows only `return;`).
    protected virtual void DestroyObject(TResObject<K, T> resObject)
    {
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__get_sizeMax.c RVA 0x0245edc0
    public int sizeMax
    {
        get
        {
            return _sizeMax;
        }
        // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__set_sizeMax.c RVA 0x0245edc8
        set
        {
            if (_sizeMax == value)
            {
                return;
            }
            if (value >= 0)
            {
                while (listResObject != null && listResObject.Count > value)
                {
                    int lastIdx = listResObject.Count - 1;
                    TResObject<K, T> last = listResObject[lastIdx];
                    listResObject.RemoveAt(lastIdx);
                    DestroyObject(last);
                }
            }
            _sizeMax = value;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo___ctor.c RVA 0x0245ee74
    public TResObjectPool(int size_max = -1, float time_default = -1f)
    {
        listResObject = new List<TResObject<K, T>>();
        timeDefault = -1f;
        // base() implicit System_Object___ctor
        this.sizeMax = size_max;
        timeDefault = time_default;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__Update.c RVA 0x0245ef28
    public int Update(float elapsedTime)
    {
        if (listResObject == null)
        {
            return 0;
        }
        int i = 0;
        while (i < listResObject.Count)
        {
            TResObject<K, T> obj = listResObject[i];
            UpdateObject(obj, elapsedTime);
            if (obj == null)
            {
                break;
            }
            if (obj.time >= 0f)
            {
                obj.time -= elapsedTime;
                if (obj.time < 0f)
                {
                    if (listResObject == null)
                    {
                        break;
                    }
                    listResObject.RemoveAt(i);
                    DestroyObject(obj);
                    i--;
                }
            }
            if (listResObject == null)
            {
                break;
            }
            i++;
        }
        return listResObject != null ? listResObject.Count : 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__Find.c RVA 0x0245f008
    public T Find(K key, float time = 0f, bool moveTop = true)
    {
        if (listResObject == null)
        {
            return default(T);
        }
        TResObject<K, T> found = null;
        foreach (TResObject<K, T> item in listResObject)
        {
            if (item != null && item.Compare(key) == 0)
            {
                found = item;
                break;
            }
        }
        if (found == null)
        {
            return default(T);
        }
        if (time != 0f)
        {
            found.time = time;
        }
        if (moveTop)
        {
            listResObject.Remove(found);
            listResObject.Insert(0, found);
        }
        return found.value;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__Add.c RVA 0x0245f238
    public bool Add(K key, T value, float time)
    {
        if (listResObject == null)
        {
            return false;
        }
        TResObject<K, T> obj = new TResObject<K, T>();
        obj.key = key;
        obj.value = value;
        obj.time = time;
        listResObject.Insert(0, obj);
        if (_sizeMax >= 0)
        {
            if (listResObject != null && _sizeMax < listResObject.Count)
            {
                // RemoveRange(start, count): Ghidra calls RemoveRange(_sizeMax, count-1) — interpret as
                // removing the tail beyond sizeMax: count = listCount - _sizeMax
                int toRemove = listResObject.Count - _sizeMax;
                listResObject.RemoveRange(_sizeMax, toRemove);
            }
        }
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__Remove.c RVA 0x0245f328
    public T Remove(K key, bool isdestroy = true)
    {
        if (listResObject == null)
        {
            return default(T);
        }
        TResObject<K, T> found = null;
        foreach (TResObject<K, T> item in listResObject)
        {
            if (item != null && item.Compare(key) == 0)
            {
                found = item;
                break;
            }
        }
        if (found == null)
        {
            return default(T);
        }
        listResObject.Remove(found);
        if (!isdestroy)
        {
            return found.value;
        }
        DestroyObject(found);
        return default(T);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__Clear.c RVA 0x0245f52c
    public void Clear(bool isdestroy = true)
    {
        if (listResObject == null)
        {
            return;
        }
        while (listResObject.Count > 0)
        {
            TResObject<K, T> item = listResObject[0];
            if (listResObject == null)
            {
                break;
            }
            listResObject.RemoveAt(0);
            if (isdestroy)
            {
                DestroyObject(item);
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/TResObjectPool_oo__get_Count.c RVA 0x0245f5c4
    private int Count
    {
        get
        {
            if (listResObject == null)
            {
                throw new NullReferenceException();
            }
            return listResObject.Count;
        }
    }
}
