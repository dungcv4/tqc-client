using Cpp2IlInjected;
using UnityEngine;

internal static class SetPropertyUtilityExt
{
	public static bool SetColor(ref Color currentValue, Color newValue)
	{ return default; }

	public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
