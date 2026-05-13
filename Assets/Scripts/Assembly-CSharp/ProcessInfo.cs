using Cpp2IlInjected;

public struct ProcessInfo
{
	private object[] m_params;

	private object m_obj;

	private string m_MethodName;

	public ProcessInfo(object[] objects, string methodName, object obj)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Run()
	{ }
}
