using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;

public class SpriteAnimationPump : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CAnimationPump_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		[DebuggerHidden]
		public _003CAnimationPump_003Ed__23(int _003C_003E1__state)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private bool MoveNext()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

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
	private sealed class _003CPumpStarter_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SpriteAnimationPump _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		[DebuggerHidden]
		public _003CPumpStarter_003Ed__21(int _003C_003E1__state)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private bool MoveNext()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

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

	private static SpriteAnimationPump instance;

	protected static ISpriteAnimatable head;

	protected static ISpriteAnimatable cur;

	private static float _timeScale;

	private static float startTime;

	private static float time;

	private static float elapsed;

	private static float timePaused;

	private static bool isPaused;

	private static ISpriteAnimatable next;

	protected static bool pumpIsRunning;

	protected static bool pumpIsDone;

	public static float animationPumpInterval;

	public bool IsRunning
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public static float timeScale
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public static SpriteAnimationPump Instance
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	private void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private void OnApplicationPause(bool paused)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void StartAnimationPump()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	[IteratorStateMachine(typeof(_003CPumpStarter_003Ed__21))]
	protected IEnumerator PumpStarter()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static void StopAnimationPump()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	[IteratorStateMachine(typeof(_003CAnimationPump_003Ed__23))]
	protected static IEnumerator AnimationPump()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void OnDestroy()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static void Add(ISpriteAnimatable s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static void Remove(ISpriteAnimatable s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SpriteAnimationPump()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	static SpriteAnimationPump()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
