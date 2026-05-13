using System;
using System.Collections.Generic;
using Cpp2IlInjected;

// Source: dump.cs (TypeDefIndex 139-142). Production .cctor RVAs:
//   Messenger          @ 0x17BE19C
//   Messenger<T>       @ 0x22A0AF0 (FullySharedGenericType inst)
//   Messenger<T,U>     @ 0x22A1574 (FullySharedGenericType inst)
//   Messenger<T,U,V>   similar generic inst
// Body pattern (standard tolua# Messenger): eventTable = new Dictionary<int, Delegate>().
// AssetRipper stubs threw AnalysisFailedException; replaced with production-equivalent init.

public static class Messenger
{
	private static Dictionary<int, Delegate> eventTable;

	public static void ClearAllCBListener()
	{ }

	public static void AddListener(int eventType, Callback handler)
	{ }

	public static void RemoveListener(int eventType, Callback handler)
	{ }

	public static void Broadcast(int eventType)
	{ }

	public static void Broadcast(int eventType, MessengerMode mode)
	{ }

	// Source: dump.cs RVA 0x17BE19C — eventTable = new Dictionary<int, Delegate>().
	static Messenger()
	{
		eventTable = new Dictionary<int, Delegate>();
	}
}
public static class Messenger<T>
{
	private static Dictionary<int, Delegate> eventTable;

	public static void ClearAllCBListener()
	{ }

	public static void AddListener(int eventType, Callback<T> handler)
	{ }

	public static void RemoveListener(int eventType, Callback<T> handler)
	{ }

	public static void Broadcast(int eventType, T arg1)
	{ }

	public static void Broadcast(int eventType, T arg1, MessengerMode mode)
	{ }

	// Source: dump.cs RVA 0x22A0AF0 — generic Messenger<T> cctor pattern.
	static Messenger()
	{
		eventTable = new Dictionary<int, Delegate>();
	}
}
public static class Messenger<T, U>
{
	private static Dictionary<int, Delegate> eventTable;

	public static void ClearAllCBListener()
	{ }

	public static void AddListener(int eventType, Callback<T, U> handler)
	{ }

	public static void RemoveListener(int eventType, Callback<T, U> handler)
	{ }

	public static void Broadcast(int eventType, T arg1, U arg2)
	{ }

	public static void Broadcast(int eventType, T arg1, U arg2, MessengerMode mode)
	{ }

	// Source: dump.cs RVA 0x22A1574 — generic Messenger<T,U> cctor pattern.
	static Messenger()
	{
		eventTable = new Dictionary<int, Delegate>();
	}
}
public static class Messenger<T, U, V>
{
	private static Dictionary<int, Delegate> eventTable;

	public static void ClearAllCBListener()
	{ }

	public static void AddListener(int eventType, Callback<T, U, V> handler)
	{ }

	public static void RemoveListener(int eventType, Callback<T, U, V> handler)
	{ }

	public static void Broadcast(int eventType, T arg1, U arg2, V arg3)
	{ }

	public static void Broadcast(int eventType, T arg1, U arg2, V arg3, MessengerMode mode)
	{ }

	// Source: dump.cs — generic Messenger<T,U,V> cctor pattern.
	static Messenger()
	{
		eventTable = new Dictionary<int, Delegate>();
	}
}
