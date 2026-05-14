// Source: Ghidra work/06_ghidra/decompiled_full/FrameControl/ — singleton FPS-throttled update dispatcher.

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

	public static FrameControl Instance { get { return s_instance; } }
	public static float Time { get { return s_time; } }
	public static float DeltaTime { get { return s_deltaTime; } }
	public float fSecondPerFrame { get { return _fSecondPerFrame; } }

	public static void CreateInstance()
	{
		if (s_instance == null) s_instance = new FrameControl();
	}

	public static void ReleaseInstance()
	{
		s_instance = null;
	}

	public void SetFPS(int nFps)
	{
		_fps = nFps;
		_fSecondPerFrame = (nFps > 0) ? (1f / nFps) : 0f;
	}

	public void AddRunner(FrameRunner runner)
	{
		if (runner == null) return;
		if (_runners == null) _runners = new List<FrameRunner>();
		_runners.Add(runner);
	}

	public void Update(float dTime)
	{
		_duration += dTime;
		if (_fSecondPerFrame > 0f && _duration < _fSecondPerFrame) return;
		s_deltaTime = _duration;
		s_time += _duration;
		_duration = 0f;
		if (_runners != null)
		{
			for (int i = 0; i < _runners.Count; i++)
			{
				var r = _runners[i];
				if (r != null) r(s_deltaTime);
			}
		}
	}

	public FrameControl()
	{
		_fps = 60;
		_fSecondPerFrame = 1f / 60f;
		_runners = new List<FrameRunner>();
	}
}
