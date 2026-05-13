// Source: Ghidra work/06_ghidra/decompiled_full/UJCoroutine/ (3 methods + inferred ctor)
// Source: dump.cs TypeDefIndex 117 — `public class UJCoroutine : IEnumerator`
// 3/3 methods ported 1-1 (ctor inferred since .ctor.c not in Ghidra dump but field semantics clear).

using System.Collections;
using System.Collections.Generic;

public class UJCoroutine : IEnumerator
{
    private Stack<IEnumerator> _ators;  // 0x10
    private object _current;            // 0x18

    // Source: Ghidra (no .ctor.c) — inferred from MoveNext field usage.
    // RVA: 0x17BA0E0
    // Allocates stack + pushes initial enumerator.
    public UJCoroutine(IEnumerator ator)
    {
        _ators = new Stack<IEnumerator>();
        _ators.Push(ator);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/UJCoroutine/get_Current.c
    // RVA: 0x17BA19C
    // Trivial getter for _current field (offset 0x18).
    public object Current
    {
        get { return _current; }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/UJCoroutine/MoveNext.c
    // RVA: 0x17BA1A4
    // Algorithm (Ghidra vtable dispatch on `+0x12e + 0xb0 + 0x138` = IL2CPP IEnumerator interface table):
    //   if _ators.Count > 0:
    //     _current = null
    //     inner = _ators.Peek()
    //     if !inner.MoveNext():  _ators.Pop()
    //     else:
    //       if inner.Current is IEnumerator: _ators.Push(inner.Current)
    //       else: _current = inner.Current
    //   return _ators.Count > 0
    public bool MoveNext()
    {
        if (_ators == null) throw new System.NullReferenceException();
        if (_ators.Count > 0)
        {
            _current = null;
            IEnumerator inner = _ators.Peek();
            if (inner == null) throw new System.NullReferenceException();
            bool advanced = inner.MoveNext();
            if (!advanced)
            {
                _ators.Pop();
            }
            else
            {
                object cur = inner.Current;
                if (cur is IEnumerator nested)
                {
                    _ators.Push(nested);
                }
                else
                {
                    _current = cur;
                }
            }
        }
        return _ators.Count > 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/UJCoroutine/Reset.c
    // RVA: 0x17BA664
    // Unconditionally throws InvalidOperationException (string literal #5190 = default Reset message).
    public void Reset()
    {
        throw new System.InvalidOperationException();
    }
}
