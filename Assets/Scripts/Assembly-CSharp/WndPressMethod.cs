using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Press Method")]
public class WndPressMethod : IWndComponent, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	[CompilerGenerated]
	private sealed class _003CExecuteContinuePress_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WndPressMethod _003C_003E4__this;

		public PointerEventData eventData;

		private int _003Ccount_003E5__2;

		private float _003Cduedate_003E5__3;

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
		public _003CExecuteContinuePress_003Ed__23(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CWaitLongPress_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WndPressMethod _003C_003E4__this;

		public float waitTime;

		public PointerEventData eventData;

		private float _003Cduedate_003E5__2;

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
		public _003CWaitLongPress_003Ed__22(int _003C_003E1__state)
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

	public string _methodName;

	public Component _comp;

	public bool _useLongPress;

	public float _longPressPeriod;

	private bool _longPressWaiting;

	public bool _useContinuePress;

	private bool _continuePressing;

	private float _continueCurrent;

	public int _continueFasterPeriod;

	public float _continueBetween;

	public float _continueBetweenLess;

	public bool _continueNoWait;

	public bool _autoClickFx;

	public TweenScale _TweenScale;

	private Vector3 _DefaultLocalScale;

	private bool isInitTweenScale;

	private WndAudioClip wndClip;

	private WndForm _wnd;

	private MethodInfo _method;

	private object[] _methodParams;

	public override void InitComponent(WndForm wnd)
	{ }

	public override void DinitComponent(WndForm wnd)
	{ }

	[IteratorStateMachine(typeof(_003CWaitLongPress_003Ed__22))]
	private IEnumerator WaitLongPress(float waitTime, PointerEventData eventData)
	{ return default; }

	[IteratorStateMachine(typeof(_003CExecuteContinuePress_003Ed__23))]
	private IEnumerator ExecuteContinuePress(PointerEventData eventData)
	{ return default; }

	public void OnPointerDown(PointerEventData eventData)
	{ }

	public void OnPointerUp(PointerEventData eventData)
	{ }

	private void OnEnable()
	{ }

	public WndPressMethod()
	{ }
}
