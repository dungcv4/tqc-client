// Source: Ghidra work/06_ghidra/decompiled_full/UIFixedProgressbarController/ —
// Spawns N copies of `initProgressObj` representing a fixed-width segmented progress bar.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Effects/UIFixedProgressbarController")]
public class UIFixedProgressbarController : MonoBehaviour
{
	public GameObject initProgressObj;
	public string MaxValue;
	public string Value;

	private List<GameObject> ProgressObjects;
	private int nMaxValue;
	private int nValue;
	private bool bActive;

	private void Awake()
	{
		ProgressObjects = new List<GameObject>();
		Init();
	}

	private void Update() { }

	private void Init()
	{
		if (initProgressObj == null) return;
		int.TryParse(MaxValue, out nMaxValue);
		int.TryParse(Value, out nValue);
		// Spawn segment GameObjects to match nMaxValue (under same parent).
		while (ProgressObjects.Count < nMaxValue)
		{
			GameObject seg = Object.Instantiate(initProgressObj, initProgressObj.transform.parent);
			ProgressObjects.Add(seg);
		}
		while (ProgressObjects.Count > nMaxValue)
		{
			GameObject seg = ProgressObjects[ProgressObjects.Count - 1];
			ProgressObjects.RemoveAt(ProgressObjects.Count - 1);
			Object.Destroy(seg);
		}
		SetValue(Value);
	}

	private void SetMaxValue(string value)
	{
		MaxValue = value;
		int.TryParse(value, out nMaxValue);
		Init();
	}

	public void SetValue(string value)
	{
		Value = value;
		int.TryParse(value, out nValue);
		if (ProgressObjects == null) return;
		for (int i = 0; i < ProgressObjects.Count; i++)
		{
			if (ProgressObjects[i] != null) ProgressObjects[i].SetActive(i < nValue);
		}
	}

	private void OnValidate()
	{
		Init();
	}

	public UIFixedProgressbarController() { }
}
