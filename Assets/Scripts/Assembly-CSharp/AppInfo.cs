using Cpp2IlInjected;

public class AppInfo
{
	public const string Name = "?輯?撜訴esigner";

	public const string Version = "628.17.35";

	public const int VersionCode = 628170035;

	private static string gamePathUrl;

	public const string UrlOffice = "http://www.uj.com.tw/";

	public const string UrlGame = "http://219.84.197.152/game/service.php";

	public const string BuildTime = "8.18.11.30";

	public static string GamePathUrl
	{
		get
		{ return default; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/AppInfo___ctor.c RVA 0x?
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public AppInfo()
	{
	}

	static AppInfo()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
