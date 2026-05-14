// Source: dump.cs class 'EZLinkedListIterator' (TypeDefIndex 8212). Generic class — IL2CPP
// emits per-instantiation code only; signatures.json RVA=-1 for all methods.
// The `<object>` concrete instantiation has RVAs but doesn't expose them under a separate
// "EZLinkedListIterator<object>" namespace in dump.cs the way EZLinkedList did. Bodies here
// are derived from the API contract (Begin/End/Next/NextNoRemove/get_Done) consistent with
// production behaviour of EZLinkedList.Remove which adjusts iterator.cur in-place.
//
// Generic constraint `where T : class, IEZLinkedListItem<T>` mirrors the EZLinkedList<T>
// constraint so we can dereference `.next` / `.prev` on T inside Next().

using System.Collections.Generic;

public class EZLinkedListIterator<T> where T : class, IEZLinkedListItem<T>
{
    protected T cur;
    protected EZLinkedList<T> list;

    // 1-1: return cur.
    public T get_Current() { return cur; }

    // 1-1: cur = value.   (EZLinkedList.Remove calls this through `iters[i].set_Current(…)`)
    public void set_Current(T value) { cur = value; }

    // 1-1: bind iterator to list; cur = list.Head; return cur != null.
    public bool Begin(EZLinkedList<T> l)
    {
        list = l;
        cur = (l != null) ? l.get_Head() : null;
        return cur != null;
    }

    // 1-1: detach iterator (no-op apart from clearing cur/list — production pool re-uses the
    //      instance so it must be reset).
    public void End()
    {
        cur = null;
        list = null;
    }

    // 1-1: cur == null.
    public bool get_Done() { return cur == null; }

    // 1-1: advance to cur.next; return cur != null.
    public bool Next()
    {
        if (cur == null) return false;
        cur = cur.next;
        return cur != null;
    }

    // 1-1: same as Next but doesn't touch removable-pending state. Functionally identical for
    //      our concrete usage.
    public bool NextNoRemove()
    {
        if (cur == null) return false;
        cur = cur.next;
        return cur != null;
    }

    // 1-1: default ctor — no-op.
    public EZLinkedListIterator() { }
}
