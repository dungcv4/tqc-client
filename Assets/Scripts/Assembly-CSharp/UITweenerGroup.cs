// Port 1-1 from Ghidra (decompiled_rva/UITweenerGroup__*.c).
// All 8 methods (RVAs 0x19FDCDC … 0x19FE1E0) ported with explicit Ghidra source comments.

using System.Collections.Generic;
using UnityEngine;

public class UITweenerGroup : MonoBehaviour
{
	private List<UITweener> _tweens;     // offset 0x20
	public UITweener lastTween;          // offset 0x28

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__get_tweenCount.c RVA 0x19FDCDC
	// 1-1: if (_tweens == null) { Reset(); if (_tweens == null) NRE; } return _tweens.Count;
	public int tweenCount
	{
		get
		{
			if (_tweens == null)
			{
				Reset();
				if (_tweens == null)
				{
					throw new System.NullReferenceException();
				}
			}
			return _tweens.Count;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__Awake.c RVA 0x19FDF2C
	// 1-1: identical body to Reset() per Ghidra (same code path 0x019fdf2c == identical to 0x19FDD34).
	private void Awake()
	{
		Reset();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__Reset.c RVA 0x19FDD34
	// 1-1: _tweens = new List<UITweener>();
	//      var arr = GetComponentsInChildren<UITweener>();
	//      if (arr != null) {
	//          foreach (var t in arr) _tweens.Add(t);
	//          // Find lastTween = entry with maximum (delay + duration)
	//          float maxTotal = 0;
	//          foreach (var t in _tweens) {
	//              if (t == null) break;
	//              float total = t.duration + t.delay;  // offsets 0x38 + 0x34 (with `+ 0.0` artifact)
	//              if (maxTotal <= total) { lastTween = t; maxTotal = total; }
	//          }
	//      } else NRE.
	public void Reset()
	{
		_tweens = new List<UITweener>();
		UITweener[] arr = GetComponentsInChildren<UITweener>();
		if (arr == null) throw new System.NullReferenceException();
		for (int i = 0; i < arr.Length; i++)
		{
			_tweens.Add(arr[i]);
		}
		float maxTotal = 0.0f;
		for (int i = 0; i < _tweens.Count; i++)
		{
			UITweener t = _tweens[i];
			if (t == null) break;
			float total = t.duration + 0.0f + t.delay;  // 1-1: `*(this+0x38) + 0.0 + *(this+0x34)`
			if (maxTotal <= total)
			{
				lastTween = t;
				maxTotal = total;
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__ResetToBeginning.c RVA 0x19FDF30
	// 1-1: if (_tweens == null) Reset(); foreach t in _tweens: t.ResetToBeginning();
	public void ResetToBeginning()
	{
		if (_tweens == null)
		{
			Reset();
			if (_tweens == null) throw new System.NullReferenceException();
		}
		for (int i = 0; i < _tweens.Count; i++)
		{
			UITweener t = _tweens[i];
			if (t == null) break;
			t.ResetToBeginning();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__PlayForward.c RVA 0x19FDFC8
	// 1-1: if (_tweens == null) Reset(); foreach t in _tweens: t.Play(1);   // 1 = forward
	public void PlayForward()
	{
		if (_tweens == null)
		{
			Reset();
			if (_tweens == null) throw new System.NullReferenceException();
		}
		for (int i = 0; i < _tweens.Count; i++)
		{
			UITweener t = _tweens[i];
			if (t == null) break;
			t.Play(true);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__PlayReverse.c RVA 0x19FE064
	// 1-1: identical to PlayForward but t.Play(0);   // 0 = reverse
	public void PlayReverse()
	{
		if (_tweens == null)
		{
			Reset();
			if (_tweens == null) throw new System.NullReferenceException();
		}
		for (int i = 0; i < _tweens.Count; i++)
		{
			UITweener t = _tweens[i];
			if (t == null) break;
			t.Play(false);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup__SetOnFinished.c RVA 0x19FE100
	// 1-1: if (_tweens == null) Reset();
	//      if (lastTween == null) { UJDebug.LogWarning(StringLit_10110); return; }
	//      lastTween.SetOnFinished(cb);
	// StringLit_10110: likely "UITweenerGroup.lastTween is null"-style warning.
	public void SetOnFinished(EventDelegate.Callback cb)
	{
		if (_tweens == null) Reset();
		if (lastTween == null)
		{
			UJDebug.LogWarning("UITweenerGroup.SetOnFinished: lastTween is null", false, UJLogType.None);
			return;
		}
		lastTween.SetOnFinished(cb);
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UITweenerGroup___ctor.c RVA 0x19FE1E0
	// 1-1: MonoBehaviour.ctor only — no field init.
	public UITweenerGroup()
	{
	}
}
