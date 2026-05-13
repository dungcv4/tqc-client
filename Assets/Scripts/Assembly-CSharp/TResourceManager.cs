// Source: dump.cs TypeDefIndex 778 (open generic). Bodies ported from TResourceManager<object,object> instantiation.
// Signatures preserved 1-1; bodies ported 2026-05-12 from work/06_ghidra/decompiled_rva/TResourceManager_oo__*.c
// Field layout:
//   param_1+0x10: mapResource (Dictionary<K,R>)
//   param_1+0x18: mapResourceKeys (List<K>)
//   param_1+0x20: dirty (bool)
//   param_1+0x28: callback (cbFunction)

using System;
using System.Collections.Generic;

public class TResourceManager<K, R> where K : IComparable where R : ResourceBase
{
	public delegate void cbFunction(R res);

	protected Dictionary<K, R> mapResource;     // 0x10

	protected List<K> mapResourceKeys;          // 0x18

	protected bool dirty;                       // 0x20

	public cbFunction callback;                 // 0x28

	// Source: dump.cs — get_Count returns mapResource.Count (standard pattern for IL2CPP generic manager get_Count RVA 0x2460B1C).
	// Falls through to FUN_015cb8fc if mapResource null in IL2CPP (NullReference); we use mapResource.Count to preserve that semantic.
	private int Count
	{
		get
		{
			return mapResource.Count;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResourceManager_oo__Update.c RVA 0x02460544
	// 1. If mapResource == null: NullReference (FUN_015cb8fc). Implicit via .Count below.
	// 2. If mapResource.Count > 0:
	//      If dirty: dirty = false; rebuild mapResourceKeys = new List<K>(mapResource.Keys).
	//      If mapResourceKeys != null:
	//         foreach key: R res = mapResource[key]; if res._isDone: callback?.Invoke(res); virtual Remove(key).
	// 3. Returns int via tail call to Dictionary.get_Count (return value is mapResource.Count post-update).
	public virtual int Update()
	{
		if (mapResource.Count > 0)
		{
			if (dirty)
			{
				dirty = false;
				mapResourceKeys = new List<K>(mapResource.Keys);
			}
			if (mapResourceKeys != null)
			{
				foreach (K key in mapResourceKeys)
				{
					R res = mapResource[key];
					if (res.get_isDone())
					{
						if (callback != null)
						{
							callback(res);
						}
						Remove(key);
					}
				}
			}
		}
		return mapResource.Count;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResourceManager_oo__Find.c RVA 0x0246078C
	// 1. If mapResource == null: NullReference (FUN_015cb8fc).
	// 2. If !mapResource.ContainsKey(id) return default(R).
	// 3. Return mapResource[id].
	public virtual R Find(K id)
	{
		if (!mapResource.ContainsKey(id))
		{
			return null;
		}
		return mapResource[id];
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResourceManager_oo__Add.c RVA 0x024607F4
	// 1. If res != null:
	//      res.Load() (virtual slot 0x178).
	//      If !res._isDone && !string.IsNullOrEmpty(res.name) && Find(id) == null:
	//         mapResource.Add(id, res).
	//         dirty = true.
	//         return true.
	// 2. Else return false.
	public virtual bool Add(K id, R res)
	{
		if (res != null)
		{
			res.Load();
			if (!res.get_isDone() && !string.IsNullOrEmpty(res.name) && Find(id) == null)
			{
				mapResource.Add(id, res);
				dirty = true;
				return true;
			}
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResourceManager_oo__Remove.c RVA 0x02460898
	// 1. res = Find(id) (virtual slot 0x188).
	// 2. If res != null:
	//      res.Unload() (virtual slot 0x188 -> Unload).
	//      mapResource.Remove(id).
	//      dirty = true.
	// 3. Return res != null.
	public virtual bool Remove(K id)
	{
		R res = Find(id);
		if (res != null)
		{
			res.Unload();
			mapResource.Remove(id);
			dirty = true;
			return true;
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResourceManager_oo__Clear.c RVA 0x02460914
	// 1. If dirty:
	//      mapResourceKeys = new List<K>(mapResource.Keys).
	// 2. If mapResourceKeys != null:
	//      foreach key: mapResource[key].Unload().
	//      mapResourceKeys.Clear() (System.Array.Clear of internal buffer + count=0).
	//      mapResource.Clear().
	//      dirty = false.
	public virtual void Clear()
	{
		if (dirty)
		{
			mapResourceKeys = new List<K>(mapResource.Keys);
		}
		if (mapResourceKeys != null)
		{
			foreach (K key in mapResourceKeys)
			{
				mapResource[key].Unload();
			}
			mapResourceKeys.Clear();
			mapResource.Clear();
			dirty = false;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/TResourceManager_oo___ctor.c RVA 0x2460B40
	// 1-1 with Ghidra .ctor body:
	//   mapResource = new Dictionary<K,R>();    // *(this + 0x10)
	//   mapResourceKeys = new List<K>();        // *(this + 0x18)
	//   base.ctor();                              // System.Object.ctor
	// Field offsets verified against Ghidra (instance offsets 0x10 / 0x18).
	public TResourceManager()
	{
		mapResource = new Dictionary<K, R>();
		mapResourceKeys = new List<K>();
	}
}
