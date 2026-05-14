// Source: Ghidra work/06_ghidra/decompiled_full/SetPropertyUtilityExt/ (4 .c files).
// Same shape as SetPropertyUtility but with explicit SetColor overload (Ghidra inlines Color field compare).

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

internal static class SetPropertyUtilityExt
{
	// Source: Ghidra SetColor.c RVA 0x17c5bd4 — return true if any component differs; write+true, else false.
	public static bool SetColor(ref Color currentValue, Color newValue)
	{
		if (currentValue.r == newValue.r
			&& currentValue.g == newValue.g
			&& currentValue.b == newValue.b
			&& currentValue.a == newValue.a) return false;
		currentValue = newValue;
		return true;
	}

	// Source: Ghidra SetStruct_float_.c RVA 0x1c452e8 + generic shared struct.
	public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
	{
		if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
		currentValue = newValue;
		return true;
	}

	// Source: Ghidra SetClass_object_.c RVA 0x1c4528c — identical to SetPropertyUtility.SetClass.
	public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
	{
		if (newValue == null && currentValue == null) return false;
		if (currentValue != null && currentValue.Equals(newValue)) return false;
		currentValue = newValue;
		return true;
	}
}
