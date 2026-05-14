// Source: Ghidra work/06_ghidra/decompiled_full/NewObjectResourceOp/ (3 .c)
// Fields: _error@0x10 (inherited), _names@0x18 (string[])
// .ctor: stores errorMsg + names.
// get_values: Resources.Load for each name in _names → returns Object[_names.Length].

using Cpp2IlInjected;
using UnityEngine;

public class NewObjectResourceOp : NewObjectAsyncOpBase
{
	private string[] _names;

	public RequestFile request
	{
		set { /* Source: Ghidra set_request.c — empty body. */ }
	}

	public override Object[] values
	{
		// Source: Ghidra get_values.c RVA 0x17bc8d4
		// new Object[_names.Length] populated via Resources.Load(name) per index.
		get
		{
			if (_names == null) throw new System.NullReferenceException();
			Object[] arr = new Object[_names.Length];
			for (int i = 0; i < _names.Length; i++)
			{
				arr[i] = Resources.Load(_names[i]);
			}
			return arr;
		}
	}

	// Source: Ghidra .ctor.c RVA 0x17bc890 — _error = errorMsg; _names = names.
	public NewObjectResourceOp(string[] names, string errorMsg = "")
	{
		_error = errorMsg;
		_names = names;
	}
}
