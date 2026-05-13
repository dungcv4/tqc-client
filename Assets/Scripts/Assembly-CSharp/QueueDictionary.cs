// Source: work/03_il2cpp_dump/dump.cs class 'QueueDictionary' (TypeDefIndex 147)
// Bodies ported 1-1 from Ghidra decompiled_rva/QueueDictionary_oo__*.c (object,object instantiation).

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

// Source: Il2CppDumper-stub  TypeDefIndex: 147
public class QueueDictionary<TKey, TValue>
{
    // Field offsets: _queue=0x10, _dictionary=0x18, _syncRoot=0x20
    private readonly LinkedList<KeyValuePair<TKey, TValue>> _queue;
    private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> _dictionary;
    private readonly object _syncRoot;

    // Source: Ghidra work/06_ghidra/decompiled_rva/QueueDictionary_oo__Dequeue.c RVA 0x0233e638
    public TValue Dequeue()
    {
        object syncRoot = _syncRoot;
        bool lockTaken = false;
        try
        {
            Monitor.Enter(syncRoot, ref lockTaken);
            if (_queue == null)
            {
                throw new NullReferenceException();
            }
            LinkedListNode<KeyValuePair<TKey, TValue>> first = _queue.First;
            _queue.RemoveFirst();
            if (first == null)
            {
                throw new NullReferenceException();
            }
            if (_dictionary == null)
            {
                throw new NullReferenceException();
            }
            _dictionary.Remove(first.Value.Key);
            return first.Value.Value;
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(syncRoot);
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/QueueDictionary_oo__Enqueue.c RVA 0x0233e758
    public void Enqueue(TKey key, TValue value)
    {
        object syncRoot = _syncRoot;
        bool lockTaken = false;
        try
        {
            Monitor.Enter(syncRoot, ref lockTaken);
            if (_queue == null)
            {
                throw new NullReferenceException();
            }
            KeyValuePair<TKey, TValue> kvp = new KeyValuePair<TKey, TValue>(key, value);
            LinkedListNode<KeyValuePair<TKey, TValue>> node = _queue.AddLast(kvp);
            if (_dictionary == null)
            {
                throw new NullReferenceException();
            }
            _dictionary.Add(key, node);
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(syncRoot);
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/QueueDictionary_oo__get_Count.c RVA 0x0233e89c
    public int Count
    {
        get
        {
            if (_queue == null)
            {
                throw new NullReferenceException();
            }
            return _queue.Count;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/QueueDictionary_oo__ContainsKey.c RVA 0x0233e8b8
    public bool ContainsKey(TKey key)
    {
        if (_dictionary == null)
        {
            throw new NullReferenceException();
        }
        return _dictionary.ContainsKey(key);
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/QueueDictionary_oo__GetByKey.c RVA 0x0233e8dc
    public TValue GetByKey(TKey key)
    {
        if (_dictionary == null)
        {
            throw new NullReferenceException();
        }
        if (!_dictionary.ContainsKey(key))
        {
            return default(TValue);
        }
        LinkedListNode<KeyValuePair<TKey, TValue>> node = _dictionary[key];
        if (node == null)
        {
            throw new NullReferenceException();
        }
        return node.Value.Value;
    }

    // Source: Ghidra work/06_ghidra/decompiled_rva/QueueDictionary_oo___ctor.c RVA 0x0233e948
    public QueueDictionary()
    {
        _queue = new LinkedList<KeyValuePair<TKey, TValue>>();
        _dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
        _syncRoot = new object();
    }
}
