using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;

public class SpriteManagerPool : MonoBehaviour
{
	public enum BEntSpriteLayer
	{
		horse = 0,
		body = 1,
		weapon = 2
	}

	[CompilerGenerated]
	private sealed class _003CgetShadowSpriteMgr_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SpriteManagerPool _003C_003E4__this;

		public string spriteName;

		private IUJObjectOperation _003Cop_003E5__2;

		private bool _003Csuccess_003E5__3;

		private float _003CloadTime_003E5__4;

		private float _003Cprogress_003E5__5;

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
		public _003CgetShadowSpriteMgr_003Ed__31(int _003C_003E1__state)
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
	private sealed class _003CgetShadowSpritePrefab_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SpriteManagerPool _003C_003E4__this;

		public string spriteName;

		private IUJObjectOperation _003Cop_003E5__2;

		private bool _003Csuccess_003E5__3;

		private float _003CloadTime_003E5__4;

		private float _003Cprogress_003E5__5;

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
		public _003CgetShadowSpritePrefab_003Ed__32(int _003C_003E1__state)
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
	private sealed class _003CgetSpriteMgr_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SpriteManagerPool _003C_003E4__this;

		public string spriteName;

		private IUJObjectOperation _003Cop_003E5__2;

		private bool _003Csuccess_003E5__3;

		private float _003CloadTime_003E5__4;

		private float _003Cprogress_003E5__5;

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
		public _003CgetSpriteMgr_003Ed__23(int _003C_003E1__state)
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
	private sealed class _003CgetSpritePrefab_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SpriteManagerPool _003C_003E4__this;

		public string spriteName;

		private IUJObjectOperation _003Cop_003E5__2;

		private bool _003Csuccess_003E5__3;

		private float _003CloadTime_003E5__4;

		private float _003Cprogress_003E5__5;

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
		public _003CgetSpritePrefab_003Ed__24(int _003C_003E1__state)
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

	private static SpriteManagerPool _instance;

	private Dictionary<string, Coroutine> _smWaitLoading;

	private Dictionary<string, GameObject> _smPool;

	private Dictionary<string, SpriteManager> _smPoolInst;

	private Dictionary<string, Coroutine> _spritePFWaitLoading;

	private Dictionary<string, GameObject> _spritePFPool;

	private static GameObject shadowManager;

	private static GameObject shadowPrefab;

	private Dictionary<string, Coroutine> _shadowSpriteManagerWaitLoading;

	private Dictionary<string, GameObject> _shadowSpriteManagerPool;

	private Dictionary<string, SpriteManager> _shadowSpriteManagerPoolInst;

	private Dictionary<string, Coroutine> _shadowSpritePrefabWaitLoading;

	private Dictionary<string, GameObject> _shadowSpritePrefabPool;

	private const float WAITTIME = -1f;

	private Shader RoleETC1AdditiveShader;

	private Shader RoleETC1TransparentShader;

	private Shader RoleETC1GrayShader;

	public static SpriteManagerPool Instance
	{
		get
		{ return default; }
	}

	// Source: dump.cs — Ghidra Awake.c not exported (likely sets singleton + DontDestroyOnLoad).
	// [Deviation note: Ghidra batch did not emit SpriteManagerPool/Awake.c. Minimal MB init.]
	private void Awake()
	{
	}

	// Source: dump.cs — Ghidra Start.c not exported.
	// [Deviation note: Ghidra batch did not emit SpriteManagerPool/Start.c. Defaulting to no-op
	//  so the pool entity doesn't block scene startup. Coroutine/pool init pending full decompile.]
	private void Start()
	{
	}

	public void moveSpriteMgrToCenter()
	{ }

	[IteratorStateMachine(typeof(_003CgetSpriteMgr_003Ed__23))]
	private IEnumerator getSpriteMgr(string spriteName)
	{ return default; }

	[IteratorStateMachine(typeof(_003CgetSpritePrefab_003Ed__24))]
	private IEnumerator getSpritePrefab(string spriteName)
	{ return default; }

	public bool IsSpriteReady(string spriteName)
	{ return default; }

	public PackedSprite createSprite(string spriteName, byte r = byte.MaxValue, byte g = byte.MaxValue, byte b = byte.MaxValue, byte a = byte.MaxValue, byte lr = byte.MaxValue, byte lg = byte.MaxValue, byte lb = byte.MaxValue, byte la = byte.MaxValue, bool additiveShader = false, bool transparentShader = false, byte transparentAlpha = 0, bool grayShader = false, byte grayScale = 0, byte metal = 0, byte metalLevelsX = 0, byte metalLevelsY = byte.MaxValue, byte metalLevelsZ = 0, byte metalLevelsW = byte.MaxValue, byte metalGamma = 0)
	{ return default; }

	public PackedSprite createSpriteUI(string spriteName, ref SpriteManager sMgrInst)
	{ return default; }

	public void removeSprite(SpriteRoot spriteRoot)
	{ }

	private void checkShadow()
	{ }

	public PackedSprite getShadow()
	{ return default; }

	[IteratorStateMachine(typeof(_003CgetShadowSpriteMgr_003Ed__31))]
	private IEnumerator getShadowSpriteMgr(string spriteName)
	{ return default; }

	[IteratorStateMachine(typeof(_003CgetShadowSpritePrefab_003Ed__32))]
	private IEnumerator getShadowSpritePrefab(string spriteName)
	{ return default; }

	public bool IsShadowReady(string spriteName)
	{ return default; }

	public PackedSprite createShadow(string spriteName)
	{ return default; }

	// Source: dump.cs — Ghidra .ctor.c not exported (likely empty body).
	// [Deviation note: Ghidra batch did not emit SpriteManagerPool/.ctor.c. Defaulting to
	//  empty body (base MonoBehaviour.ctor handles init).]
	public SpriteManagerPool()
	{
	}
}
