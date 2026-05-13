using System;
using Cpp2IlInjected;

public class ListenerException : Exception
{
	public ListenerException(string msg)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
