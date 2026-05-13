// Port 1-1 from Ghidra (decompiled_rva/NewObjectAsyncOpBase__*.c).
// All 7 methods (RVAs 0x17BBAC0 … 0x17BBAEC) ported with explicit Ghidra source comments.

using UnityEngine;

public class NewObjectAsyncOpBase : IUJObjectOperation, IUJAsyncOperation
{
	protected string _error;  // offset 0x10

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase__get_isDone.c RVA 0x17BBAC0
	// 1-1: return 1;
	public virtual bool isDone
	{
		get
		{
			return true;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase__get_progress.c RVA 0x17BBAC8
	// 1-1: return ZEXT816(0x3f800000);   (IEEE float bit pattern of 1.0f)
	public virtual float progress
	{
		get
		{
			return 1.0f;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase__get_error.c RVA 0x17BBAD0
	// 1-1: return *(this + 0x10);    (= _error)
	public virtual string error
	{
		get
		{
			return _error;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase__get_bundleBytes.c RVA 0x17BBAD8
	// 1-1: return 0;
	public virtual long bundleBytes
	{
		get
		{
			return 0L;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase__get_values.c RVA 0x17BBAE0
	// 1-1: return 0;  (null reference)
	public virtual Object[] values
	{
		get
		{
			return null;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase__ImmDestroy.c RVA 0x17BBAE8
	// 1-1: empty return;
	public virtual void ImmDestroy()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOpBase___ctor.c RVA 0x17BBAEC
	// 1-1: System_Object___ctor(this, 0);   (no field init — base.ctor only)
	public NewObjectAsyncOpBase()
	{
	}
}
