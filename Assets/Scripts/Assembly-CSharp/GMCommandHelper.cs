using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class GMCommandHelper : MonoBehaviour
{
	private class CommandData
	{
		public string cmd;

		public string comment;

		public CommandData()
		{ }
	}

	private const int BTN_HEIGHT = 40;

	private static List<CommandData> _datas;

	private static bool _initDone;

	private static GameObject _obj;

	private Rect windowRect;

	private Vector2 scrollPos;

	private string cmdText;

	private GUIStyle commentStyle;

	private GUIStyle cmdFieldStyle;

	private GUIStyle cmdBtnStyle;

	private bool collapse;

	public static void Init()
	{ }

	public static void GetGMCommands(string cmd, string comment)
	{ }

	public static void InitDone()
	{ }

	private void OnEnable()
	{ }

	private void OnGUI()
	{ }

	private void ToolWindow(int windowID)
	{ }

	private void DrawCommand(string cmd, string comment)
	{ }

	private void SendCommand()
	{ }

	public GMCommandHelper()
	{ }

	static GMCommandHelper()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
