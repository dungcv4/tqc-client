// Source: dump.cs — ListenerException : Exception with ctor(string msg) at RVA 0x17BD408.
// Standard Exception subclass — forward msg to base.

using System;
using Cpp2IlInjected;

public class ListenerException : Exception
{
	public ListenerException(string msg) : base(msg)
	{
	}
}
