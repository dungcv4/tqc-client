using System.Collections;
using Cpp2IlInjected;
using UnityEngine.UI;

public class WndForm_Loading_Long : WndForm
{
	private Slider _loadingBar;

	private static WndForm_Loading_Long s_instance;

	private bool _isEnd;

	private bool _isLoading;

	private float nowPercent;

	private float _delay;

	public static WndForm_Loading_Long Instance
	{
		get
		{ return default; }
	}

	public bool isLoading
	{
		get
		{ return default; }
	}

	public float Percent
	{
		set
		{ }
	}

	protected override bool V_Create(ArrayList args)
	{ return default; }

	protected override void V_Update(float dTime)
	{ }

	public void StartLoading()
	{ }

	public float GetNowPercent()
	{ return default; }

	public WndForm_Loading_Long()
	{ }
}
