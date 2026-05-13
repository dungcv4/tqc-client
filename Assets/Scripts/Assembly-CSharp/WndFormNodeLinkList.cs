// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1A09964, 0x1A0996C, 0x1A09974, 0x1A0997C, 0x1A05F44, 0x1A06958, 0x1A06B20
// Ghidra dir: work/06_ghidra/decompiled_full/WndFormNodeLinkList/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 299
internal class WndFormNodeLinkList
{
    private WndFormNode _head;
    private WndFormNode _end;
    private bool _orderChanged;

    // RVA: 0x1A09964  Ghidra: work/06_ghidra/decompiled_full/WndFormNodeLinkList/get_Head.c
    public WndFormNode Head { get { return _head; } }

    // RVA: 0x1A0996C  Ghidra: work/06_ghidra/decompiled_full/WndFormNodeLinkList/get_End.c
    public WndFormNode End { get { return _end; } }

    // RVA: 0x1A09974  Ghidra: work/06_ghidra/decompiled_full/WndFormNodeLinkList/get_OrderChanged.c
    public bool OrderChanged { get { return _orderChanged; } set { _orderChanged = value; } }

    // RVA: 0x1A0997C  Ghidra: work/06_ghidra/decompiled_full/WndFormNodeLinkList/set_OrderChanged.c
    
    // Source: Ghidra Push.c  RVA 0x1A05F44
    // Doubly-linked-list insert with _top-priority ordering:
    //   Empty list → node becomes head/end.
    //   If node is regular AND _end is top: walk backwards from _end via _front to find first
    //     non-top "cursor". If past head → prepend. Else if cursor._next == null → append at end.
    //     Else → insert between cursor and cursor._next.
    //   Else (default append-at-end): _end._next=node, node._front=_end, node._next=null, _end=node.
    //   Always set _orderChanged = true.
    public void Push(WndFormNode node)
    {
        if (_head == null && _end == null)
        {
            // Empty list — single-node insert.
            _head = node;
            _end = node;
            if (node == null) throw new System.NullReferenceException();
            node._front = null;
            node._next = null;
            _orderChanged = true;
            return;
        }

        if (node == null) throw new System.NullReferenceException();

        if (!node._top)
        {
            // node is regular
            if (_end == null) throw new System.NullReferenceException();
            if (_end._top)
            {
                // _end is a top node — walk back to first non-top node, then insert AFTER it.
                WndFormNode cursor = _end;
                while (true)
                {
                    cursor = cursor._front;
                    if (cursor == null)
                    {
                        // Reached past head — all nodes were _top; prepend node.
                        if (_head == null) throw new System.NullReferenceException();
                        _head._front = node;
                        node._next = _head;
                        node._front = null;
                        _head = node;
                        _orderChanged = true;
                        return;
                    }
                    if (!cursor._top) break;
                }
                // cursor is the last non-top node walking from _end
                if (cursor._next == null)
                {
                    // Append at end of non-top portion = end of list (since _end._top means rest are top after cursor).
                    cursor._next = node;
                    node._front = cursor;
                    node._next = null;
                    _end = node;
                }
                else
                {
                    // Insert between cursor and cursor._next.
                    cursor._next._front = node;
                    node._next = cursor._next;
                    cursor._next = node;
                    node._front = cursor;
                }
                _orderChanged = true;
                return;
            }
            // _end is also regular — fall through to append-at-end.
        }
        else if (_end == null)
        {
            // node._top set but list is empty (covered by first guard above).
            throw new System.NullReferenceException();
        }

        // Append at end (regular+regular OR top node).
        _end._next = node;
        node._front = _end;
        node._next = null;
        _end = node;
        _orderChanged = true;
    }

    // Source: Ghidra Remove.c  RVA 0x1A06958
    // Doubly-linked-list unlink:
    //   node==null → panic.
    //   front=node._front, next=node._next.
    //   If front==null (node is head):
    //     If next==null: _head=null, _end=null.
    //     Else: _head=next, _head._front=null, node._next=null.
    //   Else if next==null (node is end):
    //     _end=front, _end._next=null, node._front=null.
    //   Else (middle):
    //     front._next=next, next._front=front, node._next=null, node._front=null.
    public void Remove(WndFormNode node)
    {
        if (node == null) throw new System.NullReferenceException();

        WndFormNode front = node._front;
        WndFormNode next = node._next;

        if (front == null)
        {
            if (next == null)
            {
                _head = null;
                _end = null;
            }
            else
            {
                _head = next;
                if (_head == null) throw new System.NullReferenceException();
                _head._front = null;
                node._next = null;
            }
        }
        else
        {
            if (next == null)
            {
                _end = front;
                if (_end == null) throw new System.NullReferenceException();
                _end._next = null;
                node._front = null;
            }
            else
            {
                front._next = next;
                if (next == null) throw new System.NullReferenceException();
                next._front = front;
                node._next = null;
                node._front = null;
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/WndFormNodeLinkList/.ctor.c RVA 0x01a06b20
    // 1-1: empty body (only base Object.ctor — implicit in C#).
    public WndFormNodeLinkList()
    {
    }
}
