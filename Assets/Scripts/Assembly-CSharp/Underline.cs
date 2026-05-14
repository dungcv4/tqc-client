// Source: Ghidra work/06_ghidra/decompiled_full/Underline/ — resizes an underline image to match Text width.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Underline : MonoBehaviour
{
	public Text sourceTextObj;
	public Image underlineImgObj;

	private void Start()
	{
		ProcessUnderlineImage();
	}

	private void OnEnable()
	{
		StartCoroutine(DelayUpdateUnderline());
	}

	public void ProcessUnderlineImage()
	{
		if (sourceTextObj == null || underlineImgObj == null) return;
		float width = sourceTextObj.preferredWidth;
		RectTransform rt = underlineImgObj.rectTransform;
		if (rt != null)
		{
			Vector2 size = rt.sizeDelta;
			size.x = width;
			rt.sizeDelta = size;
		}
	}

	private IEnumerator DelayUpdateUnderline()
	{
		yield return null;
		ProcessUnderlineImage();
	}

	public Underline() { }
}
