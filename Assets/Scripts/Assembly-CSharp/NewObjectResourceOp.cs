using Cpp2IlInjected;
using UnityEngine;

public class NewObjectResourceOp : NewObjectAsyncOpBase
{
	private string[] _names;

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

	public NewObjectResourceOp(string[] names, string errorMsg = "")
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
