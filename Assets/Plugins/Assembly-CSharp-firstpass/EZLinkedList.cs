// 1-1 port from Ghidra concrete instantiation EZLinkedList<object> RVAs (libil2cpp.so 0x1F26F94..0x1F27FD0).
// dump.cs lists the generic form with RVA=-1 (IL2CPP doesn't emit per-generic-arg code; only the
// concrete `<object>` instantiation has real RVAs in the binary). Bodies translated from
// work/06_ghidra/decompiled_full/EZLinkedList/*.c.
//
// Generic constraint `where T : class, IEZLinkedListItem<T>` is required so we can dereference
// `.prev` / `.next` on T. Production binary erases this constraint at IL2CPP-runtime but the
// API surface (every instantiation is `EZLinkedList<SpriteMesh_Managed>` or
// `EZLinkedList<SpriteRoot>`-flavoured, both implementing IEZLinkedListItem<…>) makes it safe
// to express the constraint here. The actual T arg used by SpriteManager is SpriteMesh_Managed.
//
// Field offsets verified against ctor + Add Ghidra:
//   0x10 iters       0x18 freeIters
//   0x20 head        0x28 cur        0x30 nextItem        0x38 count

using System.Collections.Generic;

public class EZLinkedList<T> where T : class, IEZLinkedListItem<T>
{
    private List<EZLinkedListIterator<T>> iters;
    private List<EZLinkedListIterator<T>> freeIters;
    protected T head;
    protected T cur;
    protected T nextItem;
    protected int count;

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/get_Count.c RVA 0x1F26F94
    // 1-1: return count;
    public int get_Count() { return count; }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/get_Empty.c RVA 0x1F26F9C
    // 1-1: return head == null;
    public bool get_Empty() { return head == null; }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/get_Head.c RVA 0x1F26FAC
    // 1-1: return head;
    public T get_Head() { return head; }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/get_Current.c RVA 0x1F26FB4
    // 1-1: return cur;
    public T get_Current() { return cur; }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/set_Current.c RVA 0x1F26FBC
    // 1-1: cur = value;
    public void set_Current(T value) { cur = value; }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/Begin.c RVA 0x1F26FC4
    // Begin/End form an iterator-pool mechanism (freeIters + iters). Production uses these to
    // hand out short-lived iterators for nested traversal during AddSprite/RemoveSprite which
    // would otherwise clobber `cur`. Not on the current critical path (SpriteManager only uses
    // Rewind/MoveNext + cur for the simple traversal in AlreadyAdded). Implemented as a thin
    // pool that materialises an EZLinkedListIterator<T> on demand; matches production
    // semantics — call Begin to acquire, End to release.
    public EZLinkedListIterator<T> Begin()
    {
        if (iters == null)     iters     = new List<EZLinkedListIterator<T>>();
        if (freeIters == null) freeIters = new List<EZLinkedListIterator<T>>();
        EZLinkedListIterator<T> it;
        if (freeIters.Count > 0)
        {
            int last = freeIters.Count - 1;
            it = freeIters[last];
            freeIters.RemoveAt(last);
        }
        else
        {
            it = new EZLinkedListIterator<T>();
        }
        it.Begin(this);
        iters.Add(it);
        return it;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/End.c RVA 0x1F270E0
    // Returns iterator to the free pool. Mirrors Begin.
    public void End(EZLinkedListIterator<T> it)
    {
        if (it == null) return;
        if (iters     != null) iters.Remove(it);
        if (freeIters == null) freeIters = new List<EZLinkedListIterator<T>>();
        freeIters.Add(it);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/Rewind.c RVA 0x1F27198
    // 1-1:
    //   cur = head;
    //   if (cur == null) { nextItem = null; }
    //   else             { nextItem = cur.next; }
    //   return (head != null);
    public bool Rewind()
    {
        cur = head;
        if (cur == null)
        {
            nextItem = null;
        }
        else
        {
            nextItem = cur.next;
        }
        return head != null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/MoveNext.c RVA 0x1F2727C
    // 1-1:
    //   cur = nextItem;
    //   if (cur == null) return false;
    //   nextItem = cur.next;
    //   return (cur != null);    // always true at this point but Ghidra keeps the check
    public bool MoveNext()
    {
        cur = nextItem;
        if (cur == null) return false;
        nextItem = cur.next;
        return cur != null;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/Add.c RVA 0x1F2735C
    // 1-1:
    //   if (head == null) {                    // first insertion
    //       head = item;
    //       count++;
    //       return;
    //   }
    //   if (item == null) throw NullReferenceException();
    //   item.prev = head;                       // wire new item ↔ old head
    //   head.next = item;
    //   head = item;
    //   count++;
    // → list grows head-ward; head always points at the most-recently added node.
    public void Add(T item)
    {
        if (head == null)
        {
            head = item;
            count++;
            return;
        }
        if (item == null) throw new System.NullReferenceException();
        item.prev = head;
        head.next = item;
        head = item;
        count++;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/Remove.c RVA 0x1F274B8 (420 lines)
    // Long path — full body includes iterator-invalidation update for any outstanding
    // Begin()-acquired iterators that point at the node being removed. 1-1 condensed:
    //   if (item == null || head == null) return;
    //   /* fix up iterators in `iters` */
    //   for each EZLinkedListIterator<T> it in iters:
    //       if (it.cur == item) it.cur = item.next ?? item.prev;   // advance off the removed node
    //   /* relink neighbours */
    //   T prev = item.prev; T next = item.next;
    //   if (prev != null) prev.next = next;
    //   if (next != null) next.prev = prev;
    //   if (head == item) head = prev;       // head moves toward older nodes
    //   if (cur  == item) cur  = next ?? prev;
    //   item.prev = null; item.next = null;
    //   count--;
    public void Remove(T item)
    {
        if (item == null || head == null) return;
        if (iters != null)
        {
            for (int i = 0; i < iters.Count; i++)
            {
                var it = iters[i];
                if (it != null && (object)it.get_Current() == (object)item)
                {
                    it.set_Current(item.next != null ? item.next : item.prev);
                }
            }
        }
        T prev = item.prev;
        T nxt  = item.next;
        if (prev != null) prev.next = nxt;
        if (nxt  != null) nxt.prev  = prev;
        if ((object)head == (object)item) head = prev;
        if ((object)cur  == (object)item) cur  = nxt != null ? nxt : prev;
        item.prev = null;
        item.next = null;
        count--;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/Clear.c RVA 0x1F27DDC
    // 1-1: walk from head following .next, null-out each node's prev/next, then reset
    //      head=cur=nextItem=null, count=0.
    public void Clear()
    {
        T node = head;
        while (node != null)
        {
            T next = node.next;
            node.prev = null;
            node.next = null;
            node = next;
        }
        head = null;
        cur = null;
        nextItem = null;
        count = 0;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/EZLinkedList/_ctor.c RVA 0x1F27FD0
    // 1-1:
    //   iters     = new List<EZLinkedListIterator<T>>();
    //   freeIters = new List<EZLinkedListIterator<T>>();
    //   System.Object..ctor(this);
    public EZLinkedList()
    {
        iters     = new List<EZLinkedListIterator<T>>();
        freeIters = new List<EZLinkedListIterator<T>>();
    }
}
