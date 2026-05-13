// Source: dump.cs TypeDefIndex 774 (open generic). Bodies ported from TResource<object> instantiation.
// Signatures preserved 1-1; bodies ported 2026-05-12 from work/06_ghidra/decompiled_rva/TResource_object__*.c
// Field layout (inherited from ResourceBase + own):
//   name@0x10, _isLoad@0x18, _isDone@0x19 (from ResourceBase)
//   callback@0x20, data@0x28, assetType@0x30 (TResource<T>)

using System;
using UnityEngine;

[Serializable]
public class TResource<T> : ResourceBase where T : class
{
	public delegate void cbFunction(TResource<T> res);

	public cbFunction callback;       // offset 0x20

	public T data;                    // offset 0x28

	public ResourcesLoader.AssetType assetType; // offset 0x30

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResource_object__OnLoaded.c RVA 0x02461998
	// 1. _isDone = true (offset 0x19).
	// 2. If objs != null:
	//      bounds check (objs.Length == 0 -> throw IndexOutOfRange via FUN_015cb904).
	//      obj0 = objs[0].
	//      If UnityEngine.Object.op_Inequality(obj0, null):
	//         data = (T)Convert.ChangeType(obj0, typeof(T)).
	// 3. Invoke callback(this) via vtable slot 0x18 if callback != null.
	protected virtual void OnLoaded(UnityEngine.Object[] objs)
	{
		_isDone = true;
		if (objs != null)
		{
			if (objs.Length == 0)
			{
				throw new System.IndexOutOfRangeException();
			}
			UnityEngine.Object obj0 = objs[0];
			if (obj0 != null)
			{
				data = (T)System.Convert.ChangeType(obj0, typeof(T));
			}
		}
		if (callback != null)
		{
			callback(this);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResource_object__Load.c RVA 0x02461B74
	// 1. If !_isLoad:
	//      ResourceBase.Load() (sets _isLoad=true, _isDone=false).
	//      If !string.IsNullOrEmpty(name):
	//         loader = new CBNewObjectLoader(this.OnLoaded);
	//         op = ResourcesLoader.GetObjectTypeAssetDynamic((int)assetType, name, loader);
	//         return op != null.
	//      Else: _isDone = true (fall through).
	// 2. Fall-through return: data != null (param_1[5] check at offset 0x28).
	public override bool Load()
	{
		if (!_isLoad)
		{
			base.Load();
			if (!string.IsNullOrEmpty(name))
			{
				CBNewObjectLoader loader = new CBNewObjectLoader(this.OnLoaded);
				IUJObjectOperation op = ResourcesLoader.GetObjectTypeAssetDynamic((int)assetType, name, loader);
				return op != null;
			}
			_isDone = true;
		}
		return data != null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResource_object__Unload.c RVA 0x02461C64
	// data = null (offset 0x28); call base ResourceBase.Unload() which clears _isLoad/_isDone.
	public override void Unload()
	{
		data = null;
		base.Unload();
	}

	// Source: dump.cs — default empty ctor. RVA 0x2461C88 (TResource<object>..ctor).
	public TResource()
	{
	}
}
