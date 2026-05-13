using System;
using System.Collections.Generic;
using Cpp2IlInjected;

internal static class MessengerInternal
{
	public static Dictionary<int, Delegate> eventTable;

	public static readonly MessengerMode DEFAULT_MODE;

	public static void OnListenerAdding(int eventType, Delegate listenerBeingAdded)
	{ }

	public static void OnListenerRemoving(int eventType, Delegate listenerBeingRemoved)
	{ }

	public static void OnListenerRemoved(int eventType)
	{ }

	public static void OnBroadcasting(int eventType, MessengerMode mode)
	{ }

	public static void CreateBroadcastSignatureException(int eventType)
	{ }

	public static void ClearAllCBListener()
	{ }

	static MessengerInternal()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
