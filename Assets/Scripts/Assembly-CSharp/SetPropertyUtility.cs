using Cpp2IlInjected;

internal static class SetPropertyUtility
{
	public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
