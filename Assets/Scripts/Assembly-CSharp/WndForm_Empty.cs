using Cpp2IlInjected;

public class WndForm_Empty : WndForm
{
	private static WndForm_Empty s_instance;

	public static WndForm_Empty Instance
	{
		get
		{ return default; }
	}

	public WndForm_Empty()
	{ }
}
