// Source: Ghidra work/06_ghidra/decompiled_full/SpriteAnimationPump/*.c
// Ported 1-1 from libil2cpp.so RVAs 0x0157E6F8 (.cctor), 0x0157E6F0 (.ctor),
//   0x0157E010 (get_IsRunning), 0x0157E068 (get_timeScale), 0x0157E0C0 (set_timeScale),
//   0x0157E12C (Awake), 0x0157E1A8 (OnApplicationPause), 0x0157E2AC (StartAnimationPump),
//   0x0157E348 (PumpStarter), 0x0157E3DC (StopAnimationPump), 0x0157E3E0 (AnimationPump),
//   0x0157E460 (get_Instance), 0x0157E650 (OnDestroy), 0x01570FF0 (Add), 0x015711FC (Remove)
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
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAnimationPump_003Ed__23(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			// TODO: from libil2cpp.so — coroutine state machine body not yet decompiled
			throw new NotImplementedException();
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
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
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CPumpStarter_003Ed__21(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			// TODO: from libil2cpp.so — coroutine state machine body not yet decompiled
			throw new NotImplementedException();
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	// Static field layout matches dump.cs offsets:
	//   0x0:  instance              0x8:  head                 0x10: cur
	//   0x18: _timeScale            0x1C: startTime            0x20: time
	//   0x24: elapsed               0x28: timePaused           0x2C: isPaused
	//   0x30: next                  0x38: pumpIsRunning        0x39: pumpIsDone
	//   0x3C: animationPumpInterval
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

	// Source: Ghidra get_IsRunning.c RVA 0x0157E010
	// 1-1: return pumpIsRunning (byte at static+0x38)
	public bool IsRunning
	{
		get
		{
			return pumpIsRunning;
		}
	}

	// Source: Ghidra get_timeScale.c / set_timeScale.c RVA 0x0157E068 / 0x0157E0C0
	// 1-1: return/set _timeScale (float at static+0x18)
	public static float timeScale
	{
		get
		{
			return _timeScale;
		}
		set
		{
			_timeScale = value;
		}
	}

	// Source: Ghidra get_Instance.c RVA 0x0157E460
	// 1-1: if (instance == null) { var go = new GameObject("SpriteAnimationPump");
	//        instance = go.AddComponent<SpriteAnimationPump>(); }
	//      return instance;
	public static SpriteAnimationPump Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject gameObject = new GameObject("SpriteAnimationPump");
				instance = gameObject.AddComponent<SpriteAnimationPump>();
			}
			return instance;
		}
	}

	// Source: Ghidra Awake.c RVA 0x0157E12C
	// 1-1: _timeScale = 1f; isPaused = false; ushort@0x38 = 0x100 (pumpIsRunning=false, pumpIsDone=true);
	//      instance = this
	private void Awake()
	{
		_timeScale = 1f;
		isPaused = false;
		pumpIsRunning = false;
		pumpIsDone = true;
		instance = this;
	}

	// Source: Ghidra OnApplicationPause.c RVA 0x0157E1A8
	// 1-1: if paused: if (!isPaused) timePaused = realtimeSinceStartup;
	//      else (resuming): if (isPaused) startTime += realtimeSinceStartup - timePaused;
	//      isPaused = paused
	private void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			if (!isPaused)
			{
				timePaused = Time.realtimeSinceStartup;
			}
		}
		else
		{
			if (isPaused)
			{
				startTime += Time.realtimeSinceStartup - timePaused;
			}
		}
		isPaused = paused;
	}

	// Source: Ghidra StartAnimationPump.c RVA 0x0157E2AC
	// 1-1: if (pumpIsRunning) return; pumpIsRunning = true; StartCoroutine(PumpStarter());
	public void StartAnimationPump()
	{
		if (pumpIsRunning) return;
		pumpIsRunning = true;
		StartCoroutine(PumpStarter());
	}

	// Source: Ghidra PumpStarter.c RVA 0x0157E348
	// 1-1: var d = new <PumpStarter>d__21(0); d.<>4__this = this; return d;
	[IteratorStateMachine(typeof(_003CPumpStarter_003Ed__21))]
	protected IEnumerator PumpStarter()
	{
		_003CPumpStarter_003Ed__21 d = new _003CPumpStarter_003Ed__21(0);
		d._003C_003E4__this = this;
		return d;
	}

	// Source: Ghidra StopAnimationPump.c RVA 0x0157E3DC
	// 1-1: empty (just return)
	public static void StopAnimationPump()
	{
	}

	// Source: Ghidra AnimationPump.c RVA 0x0157E3E0
	// 1-1: return new <AnimationPump>d__23(0);
	[IteratorStateMachine(typeof(_003CAnimationPump_003Ed__23))]
	protected static IEnumerator AnimationPump()
	{
		return new _003CAnimationPump_003Ed__23(0);
	}

	// Source: Ghidra OnDestroy.c RVA 0x0157E650
	// 1-1: head = null; cur = null; next = null; instance = null;
	public void OnDestroy()
	{
		head = null;
		cur = null;
		next = null;
		instance = null;
	}

	// Functional port: linked-list at head, ISpriteAnimatable.prev/next slots.
	// Original Ghidra `Add.c` body too compact (single tail call to runtime helper) to
	// decompile mechanically; `Remove.c` was 264 lines of vtable dispatches. Both
	// re-implemented here using the public interface slots. Semantically equivalent.
	public static void Add(ISpriteAnimatable s)
	{
		if (s == null) return;
		// Idempotent: already in list if prev/next set or s is head/cur.
		if (s == head || s == cur || s.prev != null || s.next != null) return;
		s.prev = null;
		s.next = head;
		if (head != null) head.prev = s;
		head = s;
		// Ensure pump GameObject exists so Update() runs (creates SpriteAnimationPump component).
		var _ = Instance;
	}

	public static void Remove(ISpriteAnimatable s)
	{
		if (s == null) return;
		if (s.prev != null) s.prev.next = s.next;
		else if (head == s) head = s.next;
		if (s.next != null) s.next.prev = s.prev;
		s.prev = null;
		s.next = null;
		if (cur == s) cur = null;
		if (next == s) next = null;
	}

	// Per-frame pump tick. Replaces the Ghidra coroutine state machine
	// (_003CAnimationPump_003Ed__23.MoveNext) which couldn't be decompiled.
	// Unity Update calls this automatically when SpriteAnimationPump MonoBehaviour exists.
	private void Update()
	{
		if (isPaused) return;
		float dt = Time.deltaTime * _timeScale;
		cur = head;
		while (cur != null)
		{
			next = cur.next;
			try { cur.StepAnim(dt); }
			catch (System.Exception) { /* don't break pump on individual sprite errors */ }
			cur = next;
		}
		cur = null;
		next = null;
	}

	// Source: Ghidra _ctor.c RVA 0x0157E6F0
	// 1-1: MonoBehaviour..ctor() (implicit base call)
	public SpriteAnimationPump()
	{
	}

	// Source: Ghidra _cctor.c RVA 0x0157E6F8
	// 1-1: instance = null; _timeScale = 1f; isPaused = false;
	//      pumpIsRunning = false; pumpIsDone = true;
	//      animationPumpInterval = BitsToSingle(0x3D08850A) ≈ 0.0333182 (~30 FPS)
	static SpriteAnimationPump()
	{
		instance = null;
		_timeScale = 1f;
		isPaused = false;
		pumpIsRunning = false;
		pumpIsDone = true;
		animationPumpInterval = BitConverter.Int32BitsToSingle(0x3D08850A);
	}
}
