// Source: Ghidra work/06_ghidra/decompiled_full/AppInfo/get_GamePathUrl.c RVA 0x1a0c5ac
// 1-1: returns static field `gamePathUrl` (PTR_DAT_034490f0). .cctor initializes it to UrlGame.

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

	public static string GamePathUrl { get { return gamePathUrl; } }

	public AppInfo() { }

	// Source: Ghidra .cctor (not emitted; field default = UrlGame per static-init constant pool).
	static AppInfo()
	{
		gamePathUrl = UrlGame;
	}
}
