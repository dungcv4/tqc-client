using Cpp2IlInjected;
using UnityEngine;

public class NewObjectSimulateOp : NewObjectAsyncOpBase
{
	private Object _asset;

	public RequestFile request
	{
		set
		{ }
	}

	public override Object[] values
	{
		get
		{ return default; }
	}

	public NewObjectSimulateOp(string errorMsg = "", Object asset = null)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
