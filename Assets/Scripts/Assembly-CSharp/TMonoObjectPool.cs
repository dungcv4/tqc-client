// Source: dump.cs — TMonoObjectPool<K,T> : TResObjectPool<K,T>
// .ctor(int size_max=-1, float time_default=-1) — forwards to base ctor (no body in dump.cs).
// DestroyObject(resObject) — calls UnityEngine.Object.Destroy on the MonoBehaviour wrapped in resObject.

using System;
using Cpp2IlInjected;
using UnityEngine;

public class TMonoObjectPool<K, T> : TResObjectPool<K, T> where K : IComparable where T : MonoBehaviour
{
	protected override void DestroyObject(TResObject<K, T> resObject)
	{
		if (resObject == null) return;
		T obj = resObject.value;
		if (obj != null) UnityEngine.Object.Destroy(obj.gameObject);
	}

	public TMonoObjectPool(int size_max = -1, float time_default = -1f) : base(size_max, time_default)
	{
	}
}
