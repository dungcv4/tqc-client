using System;
using System.Runtime.InteropServices;
using Cpp2IlInjected;
using tss;

public static class Tp2Sdk
{
	private const int TssSDKCmd_CommQuery = 18;

	public static void Tp2RegistTssInfoReceiver(TssInfoReceiver receiver)
	{ }

	public static string Tp2DecTssInfo(string info)
	{ return default; }

	public static void Tp2SdkInitEx(int gameId, string appKey)
	{ }

	public static void Tp2UserLogin(int accountType, int worldId, string openId, string roleId)
	{ }

	public static void Tp2SetGamestatus(Tp2GameStatus status)
	{ }

	public static string Tp2Ioctl(string cmd)
	{ return default; }

	private static IntPtr ReadIntPtr(IntPtr addr, int off)
	{ return default; }

	[PreserveSig]
	private static extern int tp2_sdk_init_ex(int gameId, string appKey);

	[PreserveSig]
	private static extern int tp2_setuserinfo(int accountType, int worldId, string openId, string roleId);

	[PreserveSig]
	private static extern int tp2_setoptions(int options);

	[PreserveSig]
	private static extern IntPtr tp2_sdk_ioctl(int request, string param);

	[PreserveSig]
	private static extern int tp2_free_anti_data(IntPtr info);
}
