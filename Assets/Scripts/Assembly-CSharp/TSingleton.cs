using Cpp2IlInjected;

public class TSingleton<T> where T : class, new()
{
	private static T s_instance;

	public static T Instance
	{
		get
		{ return default; }
	}

	public static void CreateInstance()
	{ }

	public static void ReleaseInstance()
	{ }

	public TSingleton()
	{ }
}
