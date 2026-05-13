using Cpp2IlInjected;
using Opencoding.CommandHandlerSystem;
using UnityEngine;

public class GMCommand : MonoBehaviour
{
	public static GMCommand instance;

	private WndForm _wndForm;

	private void Start()
	{ }

	private void Awake()
	{ }

	[CommandHandler(Description = "顯示 Lua DebugCommand", Name = "slua")]
	public void ShowLuaDebugCommand(string command = "")
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua")]
	public void CallLuaDebugCommand(string command)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua1")]
	public void CallLuaDebugCommand1(string command, string arg)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua2")]
	public void CallLuaDebugCommand2(string command, string arg, string arg2)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua3")]
	public void CallLuaDebugCommand3(string command, string arg, string arg2, string arg3)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua4")]
	public void CallLuaDebugCommand4(string command, string arg, string arg2, string arg3, string arg4)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua5")]
	public void CallLuaDebugCommand5(string command, string arg, string arg2, string arg3, string arg4, string arg5)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua6")]
	public void CallLuaDebugCommand6(string command, string arg, string arg2, string arg3, string arg4, string arg5, string arg6)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua7")]
	public void CallLuaDebugCommand7(string command, string arg, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua8")]
	public void CallLuaDebugCommand8(string command, string arg, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua9")]
	public void CallLuaDebugCommand9(string command, string arg, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9)
	{ }

	[CommandHandler(Description = "呼叫Lua", Name = "clua10")]
	public void CallLuaDebugCommand10(string command, string arg, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10)
	{ }

	[CommandHandler(Description = "顯示未載入 Lua", Name = "showunloadluafile")]
	public void ShowUnloadLuaFile()
	{ }

	[CommandHandler(Description = "設定Log等級", Name = "setloglevel")]
	public void SetLogLevel(int level)
	{ }

	[CommandHandler(Description = "開關音效", Name = "sound")]
	public void SwitchSound()
	{ }

	[CommandHandler(Description = "切換顯示系統資訊", Name = "fps")]
	public void SwitchAFPSCounter()
	{ }

	public GMCommand()
	{ }
}
