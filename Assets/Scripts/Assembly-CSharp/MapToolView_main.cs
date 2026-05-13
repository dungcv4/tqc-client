using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class MapToolView_main
{
	private enum WndState
	{
		main = 0
	}

	private string strObjID;

	private string strDeleteObjID;

	public Vector2 scrollPosition;

	private static MapToolView_main instance;

	private static WndState _curState;

	private static Dictionary<int, string[]> _DataTag;

	private GameObject mouseTile;

	public static string errorMsg;

	private static string oldErrorMsg;

	private float timeEndMsg;

	public static float LeftWndWidth;

	public static MapToolView_main Instance
	{
		get
		{ return default; }
		set
		{ }
	}

	public void onViewUpdate()
	{ }

	public void ShowErrorMsg()
	{ }

	public void objListWndHandle(int wndID)
	{ }

	public void editObjWndHandle(int wndID)
	{ }

	public void controlWndHandle(int wndID)
	{ }

	public void infoWndHandle(int wndID)
	{ }

	public void BackToLastStep()
	{ }

	public MapToolView_main()
	{ }

	static MapToolView_main()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
