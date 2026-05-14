// Source: Ghidra work/06_ghidra/decompiled_full/SetPropertyUtility/ (3 .c files)
// Generic property-change helper: writes newValue if different and returns true if changed.
// 1-1 logic from SetClass<object>, SetStruct<float>, SetStruct<__Il2CppFullySharedGenericStructType>:
//   if both null/equal → no change, return false
//   else → write and return true.

using System.Collections.Generic;
using Cpp2IlInjected;

internal static class SetPropertyUtility
{
	// Source: Ghidra SetClass_object_.c RVA 0x1c443fc
	public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
	{
		if (newValue == null && currentValue == null) return false;
		if (currentValue != null && currentValue.Equals(newValue)) return false;
		currentValue = newValue;
		return true;
	}

	// Source: Ghidra SetStruct_float_.c RVA 0x1c44458 + SetStruct generic shared struct.
	public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
	{
		if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
		currentValue = newValue;
		return true;
	}
}
