using System;
using Cpp2IlInjected;

public class BroadcastException : Exception
{
	public BroadcastException(string msg)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
