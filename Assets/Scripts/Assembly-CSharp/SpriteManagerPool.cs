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

	// Source: Ghidra work/06_ghidra/decompiled_rva/SpriteManagerPool__get_Instance.c RVA 0x18D5E14
	// MonoBehaviour singleton: if _instance == null (Unity null-check), creates new GameObject(name)
	// then AddComponent<SpriteManagerPool>(), then calls checkShadow(). Throws null-deref on AddComponent fail.
	// StringLiteral_10429 is the GameObject name; not extracted — using "SpriteManagerPool" as label.
	public static SpriteManagerPool Instance
	{
		get
		{
			if ((UnityEngine.Object)_instance == null)
			{
				UnityEngine.GameObject go = new UnityEngine.GameObject("SpriteManagerPool");
				if ((UnityEngine.Object)go == null) throw new System.NullReferenceException();
				_instance = go.AddComponent<SpriteManagerPool>();
				if ((UnityEngine.Object)_instance == null) throw new System.NullReferenceException();
				_instance.checkShadow();
			}
			return _instance;
		}
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

	// Source: Ghidra work/06_ghidra/decompiled_rva/SpriteManagerPool__moveSpriteMgrToCenter.c RVA 0x18E4324
	// Sets this.transform.position = ((mapWidth*64)/2, 0, (mapHeight*64)/2).
	// Both axes use signed-/2 rounding; mapWidth/Height come from WrdFileMgr.Instance via its
	// _wrdData._mapHeader. If _wrdData is null, the dimension defaults to 0.
	// NOTE: Z is positive in Ghidra (not negated) — Lua caller flips Z elsewhere for boxColi only.
	public void moveSpriteMgrToCenter()
	{
		UnityEngine.GameObject go = base.gameObject;
		if ((UnityEngine.Object)go == null) throw new System.NullReferenceException();
		UnityEngine.Transform t = go.transform;
		if ((UnityEngine.Object)t == null) throw new System.NullReferenceException();

		WrdFileMgr wm = WrdFileMgr.Instance;
		if (wm == null) throw new System.NullReferenceException();
		int w = wm.getMapWidth();
		int h = wm.getMapHeight();

		// Ghidra signed-/2 path: result = (dim * 64) / 2 with rounding toward zero for negatives.
		// In C#, integer division on signed truncates toward zero, matching the asm sequence.
		float halfX = (float)((w * 64) / 2);
		float halfZ = (float)((h * 64) / 2);

		t.position = new UnityEngine.Vector3(halfX, 0f, halfZ);
	}

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

	// Source: Ghidra work/06_ghidra/decompiled_rva/SpriteManagerPool___ctor.c RVA 0x18E4F84
	// Allocates 10 Dictionary instances at field offsets 0x20..0x68 (8-byte fields).
	// Order matches dump.cs field layout. Then calls MonoBehaviour.ctor(this, 0).
	// Type pairs come from PTR_DAT_03461570/03461578 (string,Coroutine),
	// 03461580/03461588 (string,GameObject), 03461590/03461598 (string,SpriteManager).
	public SpriteManagerPool()
	{
		_smWaitLoading                   = new Dictionary<string, Coroutine>();    // 0x20
		_smPool                          = new Dictionary<string, GameObject>();   // 0x28
		_smPoolInst                      = new Dictionary<string, SpriteManager>();// 0x30
		_spritePFWaitLoading             = new Dictionary<string, Coroutine>();    // 0x38
		_spritePFPool                    = new Dictionary<string, GameObject>();   // 0x40
		_shadowSpriteManagerWaitLoading  = new Dictionary<string, Coroutine>();    // 0x48
		_shadowSpriteManagerPool         = new Dictionary<string, GameObject>();   // 0x50
		_shadowSpriteManagerPoolInst     = new Dictionary<string, SpriteManager>();// 0x58
		_shadowSpritePrefabWaitLoading   = new Dictionary<string, Coroutine>();    // 0x60
		_shadowSpritePrefabPool          = new Dictionary<string, GameObject>();   // 0x68
	}
}
