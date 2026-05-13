using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;

[DisallowMultipleComponent]
public class SGCSteamManager : MonoBehaviour
{
	protected static SGCSteamManager _instance;

	protected bool _initialized;

	[NoToLua]
	public const int STEAM_APP_ID = 2321430;

	protected static SGCSteamManager Instance
	{
		get
		{ return default; }
	}

	public static bool Initialized
	{
		get
		{ return default; }
	}

	public static void Init()
	{ }

	public static bool LoginMars()
	{ return default; }

	public static bool InitBilling(string serverID, string characterID, string characterName)
	{ return default; }

	public static void RequestPurchase(int designID, string extraInfo)
	{ }

	public static string GetMarsVersion()
	{ return default; }

	public static void CopyToClipboard(string text)
	{ }

	public static bool CheckMarsPasswordFormat(string passwd)
	{ return default; }

	[NoToLua]
	public static void OutputLog(string logString, string stackTrace, LogType type)
	{ }

	public static bool LoginByApple()
	{ return default; }

	public static bool IsBindApple()
	{ return default; }

	public static bool BindByApple()
	{ return default; }

	public static bool UnbindByApple()
	{ return default; }

	public static bool LoginByGoogle()
	{ return default; }

	public static bool IsBindGoogle()
	{ return default; }

	public static bool BindByGoogle()
	{ return default; }

	public static bool UnbindByGoogle()
	{ return default; }

	public static bool LoginByFacebook()
	{ return default; }

	public static bool IsBindFacebook()
	{ return default; }

	public static bool BindByFacebook()
	{ return default; }

	public static bool UnbindByFacebook()
	{ return default; }

	public SGCSteamManager()
	{ }
}
