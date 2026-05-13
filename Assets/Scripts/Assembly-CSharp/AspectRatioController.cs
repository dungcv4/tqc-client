using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.Events;

public class AspectRatioController : MonoBehaviour
{
	[Serializable]
	public class ResolutionChangedEvent : UnityEvent<int, int, bool>
	{
		public ResolutionChangedEvent()
		{ }
	}

	private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	public struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	private enum ResizeState
	{
		None = 0,
		Resizing = 1,
		AeroSnap = 2
	}

	[CompilerGenerated]
	private sealed class _003CDelayedQuit_003Ed__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AspectRatioController _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{ return default; }
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{ return default; }
		}

		[DebuggerHidden]
		public _003CDelayedQuit_003Ed__51(int _003C_003E1__state)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private bool MoveNext()
		{ return default; }

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public ResolutionChangedEvent resolutionChangedEvent;

	[SerializeField]
	private bool allowFullscreen;

	[SerializeField]
	private float aspectRatioWidth;

	[SerializeField]
	private float aspectRatioHeight;

	[SerializeField]
	private int minWidthPixel;

	[SerializeField]
	private int minHeightPixel;

	[SerializeField]
	private int maxWidthPixel;

	[SerializeField]
	private int maxHeightPixel;

	private float aspect;

	private int setWidth;

	private int setHeight;

	private bool wasFullscreenLastFrame;

	private bool started;

	private int pixelHeightOfCurrentScreen;

	private int pixelWidthOfCurrentScreen;

	private bool quitStarted;

	private const int WM_SIZING = 532;

	private const int WMSZ_LEFT = 1;

	private const int WMSZ_RIGHT = 2;

	private const int WMSZ_TOP = 3;

	private const int WMSZ_BOTTOM = 6;

	private const int GWLP_WNDPROC = -4;

	private WndProcDelegate wndProcDelegate;

	private const string UNITY_WND_CLASSNAME = "UnityWndClass";

	private IntPtr unityHWnd;

	private IntPtr oldWndProcPtr;

	private IntPtr newWndProcPtr;

	private float checkResizeInterval;

	private float checkResizeTimer;

	private int prevFrameWidth;

	private int prevFrameHeight;

	private ResizeState resizeState;

	[PreserveSig]
	private static extern uint GetCurrentThreadId();

	[PreserveSig]
	private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

	[PreserveSig]
	private static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpEnumFunc, IntPtr lParam);

	[PreserveSig]
	private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	[PreserveSig]
	private static extern bool GetWindowRect(IntPtr hwnd, ref RECT lpRect);

	[PreserveSig]
	private static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

	[PreserveSig]
	private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	[PreserveSig]
	private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	private void Start()
	{ }

	public void SetAspectRatio(float newAspectWidth, float newAspectHeight, bool apply)
	{ }

	private IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{ return default; }

	private void Update()
	{ }

	private static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
	{ return default; }

	private bool ApplicationWantsToQuit()
	{ return default; }

	[IteratorStateMachine(typeof(_003CDelayedQuit_003Ed__51))]
	private IEnumerator DelayedQuit()
	{ return default; }

	private bool IsResizing()
	{ return default; }

	public AspectRatioController()
	{ }
}
