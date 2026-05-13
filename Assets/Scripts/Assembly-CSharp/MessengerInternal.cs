// Source: dump.cs TypeDefIndex 138 + Ghidra decompile work/06_ghidra/decompiled_rva/MessengerInternal__*.c
// RVAs (per work/03_il2cpp_dump/script.json):
//   .cctor                            0x17BDC78
//   OnListenerAdding                  0x17BD470
//   OnListenerRemoving                0x17BD6C4
//   OnListenerRemoved                 0x17BD948
//   OnBroadcasting                    0x17BDA18
//   CreateBroadcastSignatureException 0x17BDB3C
//   ClearAllCBListener                0x17BDC00
//
// Note: this whole class is the canonical Unity/tolua# Action Messenger but with one
// non-standard twist: all error paths use UJDebug.LogWarning rather than throwing
// exceptions. CreateBroadcastSignatureException sounds like a throw, but Ghidra confirms
// it just LogWarnings (no `new ... throw` in the IL).
//
// String literals (per work/03_il2cpp_dump/stringliteral.json):
//   [3193] 'Attempting to add listener with inconsistent signature for event type {0}. ...'
//   [3198] 'Attempting to remove listener for type {0} but Messenger doesn't know about this event type.'
//   [3199] 'Attempting to remove listener with for event type {0} but current listener is null.'
//   [3200] 'Attempting to remove listener with inconsistent signature for event type {0}. ...'
//   [3456] 'Broadcasting message {0} but listeners have a different signature than the broadcaster.'
//   [3457] 'Broadcasting message {0} but no listener found.'

using System;
using System.Collections.Generic;

internal static class MessengerInternal
{
	// dump.cs field offset 0x0
	public static Dictionary<int, Delegate> eventTable;

	// dump.cs field offset 0x8 — set to 0 (DONT_REQUIRE_LISTENER) by .cctor (leaves default).
	public static readonly MessengerMode DEFAULT_MODE;

	// Source: Ghidra MessengerInternal__OnListenerAdding.c RVA 0x017bd470
	// 1-1:
	//   if (!eventTable.ContainsKey(eventType)) eventTable.Add(eventType, null);
	//   d = eventTable[eventType];
	//   if (d == null) return;                                  // <-- early return
	//   if (d.GetType() != listenerBeingAdded.GetType())
	//       UJDebug.LogWarning(String.Format(STR3193, eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
	public static void OnListenerAdding(int eventType, Delegate listenerBeingAdded)
	{
		if (eventTable == null) throw new NullReferenceException();
		if (!eventTable.ContainsKey(eventType))
		{
			eventTable.Add(eventType, null);
		}
		Delegate d = eventTable[eventType];
		if (d == null) return;
		if (listenerBeingAdded == null) throw new NullReferenceException();
		if (d.GetType() != listenerBeingAdded.GetType())
		{
			UJDebug.LogWarning(string.Format(
				"Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}",
				eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
		}
	}

	// Source: Ghidra MessengerInternal__OnListenerRemoving.c RVA 0x017bd6c4
	// 1-1:
	//   if (!eventTable.ContainsKey(eventType))
	//       UJDebug.LogWarning(String.Format(STR3198, eventType));  // unknown
	//   else:
	//       d = eventTable[eventType];
	//       if (d != null) {
	//           if (d.GetType() != listenerBeingRemoved.GetType())
	//               UJDebug.LogWarning(String.Format(STR3200, eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
	//       } else {
	//           UJDebug.LogWarning(String.Format(STR3199, eventType));  // null current
	//       }
	public static void OnListenerRemoving(int eventType, Delegate listenerBeingRemoved)
	{
		if (eventTable == null) throw new NullReferenceException();
		if (!eventTable.ContainsKey(eventType))
		{
			UJDebug.LogWarning(string.Format(
				"Attempting to remove listener for type {0} but Messenger doesn't know about this event type.",
				eventType));
			return;
		}
		Delegate d = eventTable[eventType];
		if (d == null)
		{
			UJDebug.LogWarning(string.Format(
				"Attempting to remove listener with for event type {0} but current listener is null.",
				eventType));
			return;
		}
		if (listenerBeingRemoved == null) throw new NullReferenceException();
		if (d.GetType() != listenerBeingRemoved.GetType())
		{
			UJDebug.LogWarning(string.Format(
				"Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}",
				eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
		}
	}

	// Source: Ghidra MessengerInternal__OnListenerRemoved.c RVA 0x017bd948
	// 1-1:
	//   d = eventTable[eventType];   // throws KeyNotFound? No — Ghidra uses get_Item, which throws if missing.
	//   if (d != null) return;       // only proceed when current is null
	//   eventTable.Remove(eventType);
	public static void OnListenerRemoved(int eventType)
	{
		if (eventTable == null) throw new NullReferenceException();
		Delegate d = eventTable[eventType];
		if (d != null) return;
		eventTable.Remove(eventType);
	}

	// Source: Ghidra MessengerInternal__OnBroadcasting.c RVA 0x017bda18
	// 1-1:
	//   if (mode == REQUIRE_LISTENER) {
	//       if (!eventTable.ContainsKey(eventType))
	//           UJDebug.LogWarning(String.Format(STR3457, eventType));
	//   }
	public static void OnBroadcasting(int eventType, MessengerMode mode)
	{
		if (mode != MessengerMode.REQUIRE_LISTENER) return;
		if (eventTable == null) throw new NullReferenceException();
		if (!eventTable.ContainsKey(eventType))
		{
			UJDebug.LogWarning(string.Format("Broadcasting message {0} but no listener found.", eventType));
		}
	}

	// Source: Ghidra MessengerInternal__CreateBroadcastSignatureException.c RVA 0x017bdb3c
	// 1-1: just LogWarning — name is misleading, it does NOT throw.
	public static void CreateBroadcastSignatureException(int eventType)
	{
		UJDebug.LogWarning(string.Format(
			"Broadcasting message {0} but listeners have a different signature than the broadcaster.",
			eventType));
	}

	// Source: Ghidra MessengerInternal__ClearAllCBListener.c RVA 0x017bdc00
	// 1-1: eventTable.Clear()
	public static void ClearAllCBListener()
	{
		if (eventTable == null) throw new NullReferenceException();
		eventTable.Clear();
	}

	// Source: Ghidra MessengerInternal___cctor.c RVA 0x017bdc78
	// 1-1:
	//   eventTable = new Dictionary<int, Delegate>();
	//   DEFAULT_MODE = 0  (left at default — readonly field, assigned via stack slot, value 0)
	static MessengerInternal()
	{
		eventTable = new Dictionary<int, Delegate>();
	}
}
