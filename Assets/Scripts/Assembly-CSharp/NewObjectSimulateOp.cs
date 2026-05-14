// Source: Ghidra work/06_ghidra/decompiled_full/NewObjectSimulateOp/ (2 .c)
// Fields: _error@0x10 (inherited), _asset@0x18
// .ctor: System.Object..ctor + assigns _error, _asset.
// get_values: returns new Object[1] { _asset }
// set_request: empty (`return;`)

using Cpp2IlInjected;
using UnityEngine;

public class NewObjectSimulateOp : NewObjectAsyncOpBase
{
	private Object _asset;

	public RequestFile request
	{
		set { /* Source: Ghidra set_request.c — empty body. */ }
	}

	public override Object[] values
	{
		// Source: Ghidra get_values.c — returns new Object[1] { _asset }.
		get { return new Object[] { _asset }; }
	}

	// Source: Ghidra .ctor.c RVA 0x17bc9dc
	public NewObjectSimulateOp(string errorMsg = "", Object asset = null)
	{
		_error = errorMsg;
		_asset = asset;
	}
}
