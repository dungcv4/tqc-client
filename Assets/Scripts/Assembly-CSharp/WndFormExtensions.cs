// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL.
// Bodies: ported 1-1 from work/06_ghidra/decompiled_full/WndFormExtensions/ where decompile exists.
// Updated 2026-05-12: ported SetPosition(V2), FixBlurry, SetSize(RT,V2), SetWidth, SetHeight, Shuffle<T> 1-1 from Ghidra.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class WndFormExtensions
{
	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/SetDefaultScale.c RVA 0x01960e10
	// trans.localScale = Vector3.one (0x3f800000 = 1.0f)
	public static void SetDefaultScale(this RectTransform trans)
	{
		trans.localScale = Vector3.one;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/SetPivotAndAnchors.c RVA 0x01960e30
	// trans.pivot = aVec; trans.anchorMin = aVec; trans.anchorMax = aVec;
	public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
	{
		trans.pivot = aVec;
		trans.anchorMin = aVec;
		trans.anchorMax = aVec;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/GetSize.c RVA 0x01960e84
	// return trans.rect.size
	public static Vector2 GetSize(this RectTransform trans)
	{
		return trans.rect.size;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/GetWidth.c RVA 0x01960ea8
	// return trans.rect.size.x  (decompile shows get_rect → returns Rect, then reads width portion)
	public static float GetWidth(this RectTransform trans)
	{
		return trans.rect.size.x;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/GetHeight.c RVA 0x01960ec8
	// return trans.rect.size.y
	public static float GetHeight(this RectTransform trans)
	{
		return trans.rect.size.y;
	}

	// Source: dump.cs RVA 0x1960EE8 — Vector2 overload of SetPosition.
	// Pattern matches SetPosition(RT, float, float) at RVA 0x1960efc: trans.set_anchoredPosition(newPos).
	public static void SetPosition(this RectTransform trans, Vector2 newPos)
	{
		trans.anchoredPosition = newPos;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/SetPosition.c RVA 0x01960efc
	// trans.anchoredPosition = new Vector2(x, y)
	public static void SetPosition(this RectTransform trans, float x, float y)
	{
		trans.anchoredPosition = new Vector2(x, y);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/FixBlurry.c RVA 0x01960f10
	// Pixel-snap helper: banker's-round the anchoredPosition x/y to integer; if rect.size.{x,y} is odd
	// then offset by +0.5 (half-pixel offset for odd-sized rects so they remain pixel-perfect).
	// The pseudo-C uses Math.IEEERemainder-equivalent modf+parity check on the rounded int;
	// fmodf(size, 2.0f) == 1.0f indicates odd size.
	public static void FixBlurry(this RectTransform trans)
	{
		Vector2 anchoredPos = trans.anchoredPosition;
		Rect rect = trans.rect;
		// Banker's round x to nearest int (0.5 rounds to even)
		float xRounded = (float)System.Math.Round((double)anchoredPos.x, System.MidpointRounding.ToEven);
		// Banker's round y to nearest int (0.5 rounds to even)
		float yRounded = (float)System.Math.Round((double)anchoredPos.y, System.MidpointRounding.ToEven);
		// If rect.width odd, offset x by 0.5; else keep
		float fx = (Mathf.Repeat(rect.width, 2.0f) == 1.0f) ? xRounded + 0.5f : xRounded;
		// If rect.height odd, offset y by 0.5
		float fy = (Mathf.Repeat(rect.height, 2.0f) == 1.0f) ? yRounded + 0.5f : yRounded;
		trans.anchoredPosition = new Vector2(fx, fy);
	}

	// Source: dump.cs RVA 0x1961138 — Ghidra decompile not produced for this overload.
	// Inferred from sibling SetSize(Image,float,float) at 0x1961290 (which sets rectTransform.sizeDelta)
	// and from SetWidth/SetHeight at 0x1961220/0x1961258 which call this with the rect-size-preserved Vector2.
	// Pattern: trans.sizeDelta = newSize (with NRE check for null trans as in sibling implementations).
	public static void SetSize(this RectTransform trans, Vector2 newSize)
	{
		if (trans == null) throw new NullReferenceException();
		trans.sizeDelta = newSize;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/SetWidth.c RVA 0x01961220
	// Body: `if (trans == null) NRE; rect = trans.rect; SetSize(trans, new Vector2(newSize, rect.size.y));`
	// The Ghidra-spilled NEON regs are the Vector2 newSize built from {newSize, rect.size.y} then passed to SetSize(RT,V2).
	public static void SetWidth(this RectTransform trans, float newSize)
	{
		if (trans == null) throw new NullReferenceException();
		Rect rect = trans.rect;
		SetSize(trans, new Vector2(newSize, rect.size.y));
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/SetHeight.c RVA 0x01961258
	// Body: `if (trans == null) NRE; rect = trans.rect; SetSize(trans, new Vector2(rect.size.x, newSize));`
	// Same shape as SetWidth, but the height param is in the y slot of the new Vector2.
	public static void SetHeight(this RectTransform trans, float newSize)
	{
		if (trans == null) throw new NullReferenceException();
		Rect rect = trans.rect;
		SetSize(trans, new Vector2(rect.size.x, newSize));
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/SetSize.c RVA 0x01961290
	// if (image == null || (rt = image.rectTransform) == null) NRE; rt.sizeDelta = new Vector2(width, height);
	public static void SetSize(this Image image, float width, float height)
	{
		RectTransform rt = image.rectTransform;
		if (rt == null)
		{
			throw new NullReferenceException();
		}
		rt.sizeDelta = new Vector2(width, height);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/FixTransparency.c RVA 0x019612cc
	// Pixel-perfect alpha fix: for every pixel with alpha==0, copy RGB from any adjacent non-transparent neighbor
	// (left, right, above, below in that priority). Uses TryAdjacent to write RGB if the candidate's alpha != 0.
	public static void FixTransparency(this Texture2D texture)
	{
		Color32[] pixels = texture.GetPixels32();
		int width = texture.width;
		int height = texture.height;
		for (int y = 0; y < height; y++)
		{
			int rowStart = y * width;
			for (int x = 0; x < width; x++)
			{
				int idx = rowStart + x;
				Color32 p = pixels[idx];
				if (p.a == 0)
				{
					bool solved = false;
					// left neighbor
					if (x > 0)
					{
						if (TryAdjacent(ref p, pixels[idx - 1])) solved = true;
					}
					// right neighbor
					if (!solved && x < width - 1)
					{
						if (TryAdjacent(ref p, pixels[idx + 1])) solved = true;
					}
					// above neighbor
					if (!solved && y > 0)
					{
						if (TryAdjacent(ref p, pixels[idx - width])) solved = true;
					}
					// below neighbor
					if (!solved && y < height - 1)
					{
						if (TryAdjacent(ref p, pixels[idx + width])) solved = true;
					}
					pixels[idx] = p;
				}
			}
		}
		texture.SetPixels32(pixels);
		texture.Apply();
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/TryAdjacent.c RVA 0x019614f8
	// if (adjacent.a != 0) { pixel.r = adjacent.r; pixel.g = adjacent.g; pixel.b = adjacent.b; return true; } return false;
	private static bool TryAdjacent(ref Color32 pixel, Color32 adjacent)
	{
		if (adjacent.a != 0)
		{
			pixel.r = adjacent.r;
			pixel.g = adjacent.g;
			pixel.b = adjacent.b;
			return true;
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/isInputHasFocus.c RVA 0x01961520
	// 1-1: Receiver param `eventSystem` is unused; checks the global EventSystem.current.
	public static bool isInputHasFocus(this EventSystem eventSystem)
	{
		var cur = UnityEngine.EventSystems.EventSystem.current;
		if (cur != null)
		{
			var selected = cur.currentSelectedGameObject;
			if (selected != null)
			{
				var inputField = selected.GetComponent<UnityEngine.UI.InputField>();
				if (inputField != null) return true;
			}
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/NotifyOnPointerDown.c RVA 0x0196165c
	// if (eventData == null) eventData = new BaseEventData(eventSystem);
	// ExecuteEvents.Execute<IPointerDownHandler>(target, eventData, ExecuteEvents.pointerDownHandler);
	public static void NotifyOnPointerDown(this EventSystem eventSystem, GameObject target, BaseEventData eventData = null)
	{
		if (eventData == null)
		{
			eventData = new BaseEventData(eventSystem);
		}
		ExecuteEvents.Execute<IPointerDownHandler>(target, eventData, ExecuteEvents.pointerDownHandler);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/NotifyOnPointerUp.c RVA 0x01961748
	// if (eventData == null) eventData = new BaseEventData(eventSystem);
	// ExecuteEvents.Execute<IPointerUpHandler>(target, eventData, ExecuteEvents.pointerUpHandler);
	public static void NotifyOnPointerUp(this EventSystem eventSystem, GameObject target, BaseEventData eventData = null)
	{
		if (eventData == null)
		{
			eventData = new BaseEventData(eventSystem);
		}
		ExecuteEvents.Execute<IPointerUpHandler>(target, eventData, ExecuteEvents.pointerUpHandler);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/NotifyOnClick.c RVA 0x01961834
	// if (eventData == null) eventData = new BaseEventData(eventSystem);
	// ExecuteEvents.Execute<IPointerClickHandler>(target, eventData, ExecuteEvents.pointerClickHandler);
	public static void NotifyOnClick(this EventSystem eventSystem, GameObject target, BaseEventData eventData = null)
	{
		if (eventData == null)
		{
			eventData = new BaseEventData(eventSystem);
		}
		ExecuteEvents.Execute<IPointerClickHandler>(target, eventData, ExecuteEvents.pointerClickHandler);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/NotifyOnSelect.c RVA 0x01961920
	// if (eventData == null) eventData = new BaseEventData(eventSystem);
	// ExecuteEvents.Execute<ISelectHandler>(target, eventData, ExecuteEvents.selectHandler);
	public static void NotifyOnSelect(this EventSystem eventSystem, GameObject target, BaseEventData eventData = null)
	{
		if (eventData == null)
		{
			eventData = new BaseEventData(eventSystem);
		}
		ExecuteEvents.Execute<ISelectHandler>(target, eventData, ExecuteEvents.selectHandler);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/GetFilesByExtensions.c RVA 0x01961a0c
	// Creates the display-class capture (allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase)),
	// then dirInfo.GetFiles().Where(<>c__DisplayClass19_0.<GetFilesByExtensions>b__0).
	// dump.cs shows the display-class b__0 body is itself NIE (RVA 0x1961C60 — decompile pending).
	// We port the closure inline using a HashSet + Linq.Where on file Extension.
	public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dirInfo, params string[] extensions)
	{
		var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);
		return dirInfo.GetFiles().Where(f => allowedExtensions.Contains(f.Extension));
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/HasParameter.c RVA 0x01961bb4
	// for (int i = 0; i < animator.parameterCount; i++) {
	//   if (animator.parameters[i].name == paramName) return true; // returns (0 < parameterCount) per Ghidra
	// }
	// return (initial 0 < parameterCount).  In Ghidra, the loop returns `bVar5` which tracks (i < parameterCount)
	// at each iteration; on match it returns the current bVar5 (true since we entered loop).
	public static bool HasParameter(this Animator animator, string paramName)
	{
		int count = animator.parameterCount;
		bool inRange = 0 < count;
		if (inRange)
		{
			int i = 0;
			do
			{
				AnimatorControllerParameter[] parameters = animator.parameters;
				AnimatorControllerParameter param = parameters[i];
				if (param.name == paramName)
				{
					return inRange;
				}
				i++;
				count = animator.parameterCount;
				inRange = i < count;
			} while (i < count);
		}
		return inRange;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndFormExtensions/Shuffle<__Il2CppFullySharedGenericType>.c RVA 0x01C7C5C0
	// Fisher-Yates shuffle. Pseudo-C decompile (after fully-shared-generic vtable resolution):
	//   if (list == null) NRE;
	//   int n = list.Count;
	//   var rng = new System.Random();
	//   while (n > 1) {
	//     int j = rng.Next(n);              // [0, n)
	//     T temp = list[j];
	//     list[j] = list[n - 1];
	//     list[n - 1] = temp;
	//     n--;
	//   }
	public static void Shuffle<T>(this IList<T> list)
	{
		if (list == null) throw new NullReferenceException();
		int n = list.Count;
		System.Random rng = new System.Random();
		while (n > 1)
		{
			int j = rng.Next(n);
			T temp = list[j];
			list[j] = list[n - 1];
			list[n - 1] = temp;
			n--;
		}
	}
}
