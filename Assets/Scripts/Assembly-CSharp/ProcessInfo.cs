// Source: Ghidra work/06_ghidra/decompiled_full/ProcessInfo/Run.c RVA 0x15b0150
// 1-1: reflect MethodInfo invoke via stored object+name+params. String literals 5149/5150 verified.

using Cpp2IlInjected;

public struct ProcessInfo
{
	private object[] m_params;
	private object m_obj;
	private string m_MethodName;

	public ProcessInfo(object[] objects, string methodName, object obj)
	{
		m_params = objects;
		m_MethodName = methodName;
		m_obj = obj;
	}

	public void Run()
	{
		if (m_obj != null)
		{
			System.Type t = m_obj.GetType();
			if (t == null) throw new System.NullReferenceException();
			System.Reflection.MethodInfo mi = t.GetMethod(m_MethodName);
			if (mi != null)
			{
				mi.Invoke(m_obj, m_params);
				return;
			}
			// String literal #5149 = "Error!! BFBuilder can't find method: {0}"
			UJDebug.Log(string.Format("Error!! BFBuilder can't find method: {0}", m_MethodName));
			return;
		}
		// String literal #5150 = "Error!! m_obj is null!"
		UJDebug.Log("Error!! m_obj is null!");
	}
}
