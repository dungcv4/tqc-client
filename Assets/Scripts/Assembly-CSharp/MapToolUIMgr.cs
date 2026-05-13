using Cpp2IlInjected;
using UnityEngine;

public class MapToolUIMgr : SimpleUIMgr
{
	public enum MapToolViewID
	{
		init = 0,
		main = 1,
		error = 2
	}

	public GUISkin skin;

	public static GUIStyle redText;

	private void Awake()
	{ }

	private void Start()
	{ }

	private void OnGUI()
	{ }

	public MapToolUIMgr()
	{ }

	static MapToolUIMgr()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
