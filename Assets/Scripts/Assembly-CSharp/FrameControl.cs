using System.Collections.Generic;
using Cpp2IlInjected;

public class FrameControl
{
	public delegate void FrameRunner(float dTime);

	private static FrameControl s_instance;

	private static float s_time;

	private static float s_deltaTime;

	private int _fps;

	private float _fSecondPerFrame;

	private float _duration;

	private List<FrameRunner> _runners;

	public static FrameControl Instance
	{
		get
		{ return default; }
	}

	public static float Time
	{
		get
		{ return default; }
	}

	public static float DeltaTime
	{
		get
		{ return default; }
	}

	public float fSecondPerFrame
	{
		get
		{ return default; }
	}

	public static void CreateInstance()
	{ }

	public static void ReleaseInstance()
	{ }

	public void SetFPS(int nFps)
	{ }

	public void AddRunner(FrameRunner runner)
	{ }

	public void Update(float dTime)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/FrameControl___ctor.c RVA 0x?
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public FrameControl()
	{
	}
}
