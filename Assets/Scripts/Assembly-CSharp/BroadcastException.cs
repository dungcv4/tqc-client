// Source: dump.cs TypeDefIndex (search BroadcastException) — public class : Exception,
// ctor(string). Trivially passes message to base.
using System;

public class BroadcastException : Exception
{
	public BroadcastException(string msg) : base(msg) { }
}
