// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'EZLinkedList'
// To port a method: replace `throw new System.NotImplementedException();`
// with body translated from the listed Ghidra .c file.
// Move ported file to unity_project/Assets/Scripts/Ported/<area>/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8214
public class EZLinkedList<T>
{
    private List<EZLinkedListIterator<T>> iters;
    private List<EZLinkedListIterator<T>> freeIters;
    protected T head;
    protected T cur;
    protected T nextItem;
    protected int count;

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public int get_Count() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public bool get_Empty() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public T get_Head() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public T get_Current() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public void set_Current(T value) { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public EZLinkedListIterator<T> Begin() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public void End(EZLinkedListIterator<T> it) { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public bool Rewind() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public bool MoveNext() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public void Add(T item) { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public void Remove(T item) { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public void Clear() { throw new System.NotImplementedException(); }

    // RVA: -1  Ghidra: not yet decompiled — re-run work/06_ghidra/dump_must_port.py
    public EZLinkedList() { throw new System.NotImplementedException(); }

}
