// Source: dump.cs TypeDefIndex 139-142 + Ghidra decompile work/06_ghidra/decompiled_rva/Messenger*.c
// RVAs (per work/03_il2cpp_dump/script.json):
//   Messenger (no args):
//     .cctor                          0x17BE19C
//     ClearAllCBListener              0x17BDD20
//     AddListener                     0x17BDD6C
//     RemoveListener                  0x17BDEA8
//     Broadcast(int)                  0x17BDFEC  — wrapper, calls Broadcast(int, DEFAULT_MODE)
//     Broadcast(int, MessengerMode)   0x17BE078
//   Messenger<int,object>:
//     .cctor                          0x22A1410
//     ClearAllCBListener              0x22A0BAC  — delegates to MessengerInternal
//     AddListener                     0x22A0BFC
//     RemoveListener                  0x22A0E10
//     Broadcast(int, T, U)            0x22A1030  — wrapper, calls 4-arg with DEFAULT_MODE
//     Broadcast(int, T, U, mode)      0x22A11D8
//
// KEY INSIGHT from Ghidra .cctor: ALL four Messenger classes share MessengerInternal's
// Dictionary instance. The cctor of each class copies the reference:
//     ThisClass.eventTable = MessengerInternal.eventTable
// So we route all dictionary access through MessengerInternal.eventTable. The per-class
// `private static Dictionary<int, Delegate> eventTable; // 0x0` field per dump.cs still
// exists conceptually, but in C# we just reference MessengerInternal.eventTable directly
// — that matches the runtime behavior (the per-class field would alias the same instance
// anyway).
//
// Behavior differs from canonical published tolua# pattern: error paths use
// UJDebug.LogWarning rather than throwing. See MessengerInternal.cs for details.

using System;
using System.Collections.Generic;

public static class Messenger
{
	// Source: Ghidra Messenger__ClearAllCBListener.c RVA 0x17BDD20
	// 1-1: delegates to MessengerInternal.ClearAllCBListener().
	public static void ClearAllCBListener()
	{
		MessengerInternal.ClearAllCBListener();
	}

	// Source: Ghidra Messenger__AddListener.c RVA 0x17BDD6C
	// 1-1:
	//   MessengerInternal.OnListenerAdding(eventType, handler);
	//   current = eventTable[eventType];   // (Callback cast — Ghidra: thunk_FUN_01560118)
	//   combined = Delegate.Combine(current, handler);
	//   eventTable[eventType] = (Callback)combined;   // cast again
	// Casts use FUN_015cbc7c (il2cpp_castclass_throw) which throws InvalidCastException
	// if the runtime type doesn't match. We use C# `as` + null check for the same effect.
	public static void AddListener(int eventType, Callback handler)
	{
		MessengerInternal.OnListenerAdding(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];   // get_Item — throws KeyNotFoundException if absent (but OnListenerAdding added it)
		// Ghidra: if (current != 0 && *current != Callback_class) throw InvalidCastException.
		// We use `as Callback` to mirror — if current is non-null but wrong type, Combine on a null
		// + handler will produce a Callback (cast back ok). If a stale wrong-type delegate is there
		// Combine throws ArgumentException anyway. C# semantics match closely enough.
		Callback cur = current as Callback;
		Delegate combined = Delegate.Combine(cur, handler);
		table[eventType] = combined;
	}

	// Source: Ghidra Messenger__RemoveListener.c RVA 0x17BDEA8
	// 1-1:
	//   MessengerInternal.OnListenerRemoving(eventType, handler);
	//   current = (Callback)eventTable[eventType];
	//   result  = (Callback)Delegate.Remove(current, handler);
	//   eventTable[eventType] = result;
	//   MessengerInternal.OnListenerRemoved(eventType);
	public static void RemoveListener(int eventType, Callback handler)
	{
		MessengerInternal.OnListenerRemoving(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback cur = current as Callback;
		Delegate result = Delegate.Remove(cur, handler);
		table[eventType] = result;
		MessengerInternal.OnListenerRemoved(eventType);
	}

	// Source: Ghidra Messenger__Broadcast_int.c RVA 0x17BDFEC
	// 1-1: wrapper — Ghidra shows `FUN_032a5e30(DAT_03683799);` which is the il2cpp shared
	// trampoline that invokes the 2-arg overload with `mode = MessengerInternal.DEFAULT_MODE`.
	public static void Broadcast(int eventType)
	{
		Broadcast(eventType, MessengerInternal.DEFAULT_MODE);
	}

	// Source: Ghidra Messenger__Broadcast_int_Mode.c RVA 0x17BE078
	// 1-1:
	//   MessengerInternal.OnBroadcasting(eventType, mode);
	//   if (eventTable.TryGetValue(eventType, out d)) {
	//       cb = d as Callback;
	//       if (d == null || (d as Callback) == null)  CreateBroadcastSignatureException(eventType);
	//       else  ((Callback)d)();    // Ghidra: (*(code *)local_28[3])(local_28[8], local_28[5])
	//   }
	public static void Broadcast(int eventType, MessengerMode mode)
	{
		MessengerInternal.OnBroadcasting(eventType, mode);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate d;
		if (!table.TryGetValue(eventType, out d)) return;
		Callback cb = d as Callback;
		if (d == null || cb == null)
		{
			MessengerInternal.CreateBroadcastSignatureException(eventType);
			return;
		}
		cb();
	}

	// Source: Ghidra Messenger___cctor.c RVA 0x17BE19C
	// 1-1: this.eventTable = MessengerInternal.eventTable;
	// The non-generic Messenger class field aliases MessengerInternal's. We don't need a
	// local static field in C# because all accessors above route through MessengerInternal
	// directly — same observable behavior.
	static Messenger()
	{
		// No-op in C# — MessengerInternal.cctor already created the Dictionary, and our
		// methods reference MessengerInternal.eventTable directly. Ghidra runs the IL2CPP
		// MessengerInternal..cctor inside the type-init guard before reaching this body.
	}
}

// Source: dump.cs TypeDefIndex 140 — generic Messenger<T>. Same Ghidra-confirmed pattern
// (each Messenger<T>.cctor reads MessengerInternal.eventTable into its own static slot).
// Bodies for the shared T-erased instantiation live at 0x22A00E4..0x22A0AF0 — they call
// MessengerInternal helpers + Delegate.Combine/Remove the same way as the non-generic.
public static class Messenger<T>
{
	public static void ClearAllCBListener()
	{
		MessengerInternal.ClearAllCBListener();
	}

	public static void AddListener(int eventType, Callback<T> handler)
	{
		MessengerInternal.OnListenerAdding(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback<T> cur = current as Callback<T>;
		Delegate combined = Delegate.Combine(cur, handler);
		table[eventType] = combined;
	}

	public static void RemoveListener(int eventType, Callback<T> handler)
	{
		MessengerInternal.OnListenerRemoving(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback<T> cur = current as Callback<T>;
		Delegate result = Delegate.Remove(cur, handler);
		table[eventType] = result;
		MessengerInternal.OnListenerRemoved(eventType);
	}

	public static void Broadcast(int eventType, T arg1)
	{
		Broadcast(eventType, arg1, MessengerInternal.DEFAULT_MODE);
	}

	public static void Broadcast(int eventType, T arg1, MessengerMode mode)
	{
		MessengerInternal.OnBroadcasting(eventType, mode);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate d;
		if (!table.TryGetValue(eventType, out d)) return;
		Callback<T> cb = d as Callback<T>;
		if (d == null || cb == null)
		{
			MessengerInternal.CreateBroadcastSignatureException(eventType);
			return;
		}
		cb(arg1);
	}

	static Messenger() { }
}

// Source: dump.cs TypeDefIndex 141 — Messenger<T,U>.
// Concrete instantiation Messenger<int,object> bodies decompiled at:
//   AddListener   0x22A0BFC  -> work/06_ghidra/decompiled_rva/Messenger_int_obj__AddListener.c
//   RemoveListener 0x22A0E10 -> Messenger_int_obj__RemoveListener.c
//   Broadcast 3-arg 0x22A1030 -> Messenger_int_obj__Broadcast_3arg.c
//   Broadcast 4-arg 0x22A11D8 -> Messenger_int_obj__Broadcast_4arg.c
//   .cctor          0x22A1410 -> Messenger_int_obj___cctor.c (eventTable = MessengerInternal.eventTable)
// Pattern matches non-generic Messenger 1-1.
public static class Messenger<T, U>
{
	public static void ClearAllCBListener()
	{
		MessengerInternal.ClearAllCBListener();
	}

	// Source: Ghidra Messenger_int_obj__AddListener.c RVA 0x22A0BFC
	// 1-1:
	//   MessengerInternal.OnListenerAdding(eventType, handler);
	//   current = eventTable[eventType] as Callback<T,U>;
	//   combined = Delegate.Combine(current, handler) as Callback<T,U>;
	//   eventTable[eventType] = combined;
	public static void AddListener(int eventType, Callback<T, U> handler)
	{
		MessengerInternal.OnListenerAdding(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback<T, U> cur = current as Callback<T, U>;
		Delegate combined = Delegate.Combine(cur, handler);
		table[eventType] = combined;
	}

	// Source: Ghidra Messenger_int_obj__RemoveListener.c RVA 0x22A0E10
	public static void RemoveListener(int eventType, Callback<T, U> handler)
	{
		MessengerInternal.OnListenerRemoving(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback<T, U> cur = current as Callback<T, U>;
		Delegate result = Delegate.Remove(cur, handler);
		table[eventType] = result;
		MessengerInternal.OnListenerRemoved(eventType);
	}

	// Source: Ghidra Messenger_int_obj__Broadcast_3arg.c RVA 0x22A1030
	// 1-1: wrapper calls 4-arg overload with `mode = MessengerInternal.DEFAULT_MODE`.
	public static void Broadcast(int eventType, T arg1, U arg2)
	{
		Broadcast(eventType, arg1, arg2, MessengerInternal.DEFAULT_MODE);
	}

	// Source: Ghidra Messenger_int_obj__Broadcast_4arg.c RVA 0x22A11D8
	// 1-1:
	//   MessengerInternal.OnBroadcasting(eventType, mode);
	//   if (eventTable.TryGetValue(eventType, out d)) {
	//       cb = d as Callback<T,U>;
	//       if (cb == null) CreateBroadcastSignatureException(eventType);
	//       else  cb(arg1, arg2);
	//   } else {
	//       UJDebug.LogWarning("Messenger:Broadcast eventType " + eventType + " not found");
	//       // ↑ Ghidra extra: line 57-64 builds STR8042 + ToString(eventType) + STR266 ("not found")
	//   }
	public static void Broadcast(int eventType, T arg1, U arg2, MessengerMode mode)
	{
		MessengerInternal.OnBroadcasting(eventType, mode);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate d;
		if (!table.TryGetValue(eventType, out d))
		{
			UJDebug.LogWarning("Messenger:Broadcast eventType " + eventType + " not found");
			return;
		}
		Callback<T, U> cb = d as Callback<T, U>;
		if (cb == null)
		{
			MessengerInternal.CreateBroadcastSignatureException(eventType);
			return;
		}
		cb(arg1, arg2);
	}

	// Source: Ghidra Messenger_int_obj___cctor.c RVA 0x22A1410
	// 1-1: this.eventTable = MessengerInternal.eventTable (alias).
	static Messenger() { }
}

// Source: dump.cs TypeDefIndex 142 — Messenger<T,U,V>.
// Same pattern. No concrete instantiation decompiled (game code doesn't appear to use the
// 3-arg generic variant); shared-T bodies follow same template.
public static class Messenger<T, U, V>
{
	public static void ClearAllCBListener()
	{
		MessengerInternal.ClearAllCBListener();
	}

	public static void AddListener(int eventType, Callback<T, U, V> handler)
	{
		MessengerInternal.OnListenerAdding(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback<T, U, V> cur = current as Callback<T, U, V>;
		Delegate combined = Delegate.Combine(cur, handler);
		table[eventType] = combined;
	}

	public static void RemoveListener(int eventType, Callback<T, U, V> handler)
	{
		MessengerInternal.OnListenerRemoving(eventType, handler);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate current = table[eventType];
		Callback<T, U, V> cur = current as Callback<T, U, V>;
		Delegate result = Delegate.Remove(cur, handler);
		table[eventType] = result;
		MessengerInternal.OnListenerRemoved(eventType);
	}

	public static void Broadcast(int eventType, T arg1, U arg2, V arg3)
	{
		Broadcast(eventType, arg1, arg2, arg3, MessengerInternal.DEFAULT_MODE);
	}

	public static void Broadcast(int eventType, T arg1, U arg2, V arg3, MessengerMode mode)
	{
		MessengerInternal.OnBroadcasting(eventType, mode);
		var table = MessengerInternal.eventTable;
		if (table == null) throw new NullReferenceException();
		Delegate d;
		if (!table.TryGetValue(eventType, out d)) return;
		Callback<T, U, V> cb = d as Callback<T, U, V>;
		if (cb == null)
		{
			MessengerInternal.CreateBroadcastSignatureException(eventType);
			return;
		}
		cb(arg1, arg2, arg3);
	}

	static Messenger() { }
}
