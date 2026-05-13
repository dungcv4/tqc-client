using Cpp2IlInjected;

public static class WRD_CONST
{
	public static int HEADER_SIZE;

	public static int CODE_SIZE;

	public const uint MAP_CODE_WALL = uint.MaxValue;

	public const uint MAP_CODE_FREE = 4294967294u;

	public const uint MAP_CODE_EMPTY = 0u;

	public const int SERVER_GRID_SIZE = 64;

	public const int CLIENT_GRID_SIZE = 1;

	static WRD_CONST()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
