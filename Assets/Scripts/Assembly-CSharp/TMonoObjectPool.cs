using System;
using Cpp2IlInjected;
using UnityEngine;

public class TMonoObjectPool<K, T> : TResObjectPool<K, T> where K : IComparable where T : MonoBehaviour
{
	protected override void DestroyObject(TResObject<K, T> resObject)
	{ }

	public TMonoObjectPool(int size_max = -1, float time_default = -1f)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
