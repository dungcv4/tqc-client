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

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteManagerPool/Awake.c RVA 0x18E41B4
	// 1-1: GameObject go = this.gameObject; DontDestroyOnLoad(go).
	private void Awake()
	{
		UnityEngine.GameObject go = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(go);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteManagerPool/Start.c RVA 0x18E4220
	// 1-1:
	//   _instance = this;                                              // **(WndRoot_static+0xb8)? — actually
	//                                                                  // PTR_DAT_0344a8f8 = SpriteManagerPool
	//                                                                  // type-info; +0xb8 → static fields;
	//                                                                  // *8 → _instance slot.
	//   moveSpriteMgrToCenter();
	//   checkShadow();
	//   RoleETC1AdditiveShader   = Shader.Find("Sango Online Classic/Role ETC1 Additive");      // lit 9806
	//   RoleETC1TransparentShader= Shader.Find("Sango Online Classic/Role ETC1 Transparent");   // lit 9807
	//   RoleETC1GrayShader       = Shader.Find("Sango Online Classic/Role ETC1_gray");          // lit 9808
	private void Start()
	{
		_instance = this;
		moveSpriteMgrToCenter();
		checkShadow();
		RoleETC1AdditiveShader    = UnityEngine.Shader.Find("Sango Online Classic/Role ETC1 Additive");
		RoleETC1TransparentShader = UnityEngine.Shader.Find("Sango Online Classic/Role ETC1 Transparent");
		RoleETC1GrayShader        = UnityEngine.Shader.Find("Sango Online Classic/Role ETC1_gray");
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

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteManagerPool_StateMachines/getSpriteMgr_MoveNext.c RVA 0x18E59B8
	// + entry-thunk Ghidra .../SpriteManagerPool/getSpriteMgr.c RVA 0x18E43E8
	// 1-1 mapping (compiler-generated state machine class `_003CgetSpriteMgr_003Ed__23` left in
	//  this file as a dead-stub shell since the production binary advertises it via the
	//  `IteratorStateMachine` attribute; we drop the attribute on the public method since the
	//  compiler will auto-generate its own equivalent state-machine for this body):
	//   if (_smPool.ContainsKey(spriteName)) yield break;                       // line 44-50 fast-exit
	//   IUJObjectOperation op = ResourcesLoader.GetObjectTypeAssetDynamic(
	//       AssetType.MODEL /* = 12 */,
	//       spriteName + "_manager"                                              // lit 13650 "_manager"
	//   );
	//   while (!op.isDone) {
	//       loadTime += WAITTIME;                                                // DAT_0091c238 ≈ 1/30
	//       progress = op.progress;                                              // unused outside this loop
	//       yield return new WaitForSeconds(WAITTIME);
	//   }
	//   if (op.values != null && op.values[0] != null) {                          // op_Inequality with null
	//       _smPool.Add(spriteName, (GameObject)op.values[0]);
	//   }
	//   _smWaitLoading[spriteName] = null;                                        // mark not-waiting
	private IEnumerator getSpriteMgr(string spriteName)
	{
		if (_smPool == null || _spritePFPool == null || _smWaitLoading == null)
			throw new System.NullReferenceException();
		if (_smPool.ContainsKey(spriteName)) yield break;
		IUJObjectOperation op = ResourcesLoader.GetObjectTypeAssetDynamic(
			ResourcesLoader.AssetType.MODEL,
			spriteName + "_manager",
			null);
		float loadTime = 0f;
		float progress = 0f;
		while (op != null && !op.isDone)
		{
			loadTime += WAITTIME;
			progress = op.progress;
			yield return new UnityEngine.WaitForSeconds(WAITTIME);
		}
		if (op != null && op.values != null && op.values.Length > 0)
		{
			UnityEngine.Object asset = op.values[0];
			if ((UnityEngine.Object)asset != null)
			{
				_smPool[spriteName] = (UnityEngine.GameObject)asset;
			}
		}
		_smWaitLoading[spriteName] = null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteManagerPool_StateMachines/getSpritePrefab_MoveNext.c RVA 0x18E5DD0
	// + entry-thunk Ghidra .../SpriteManagerPool/getSpritePrefab.c RVA 0x18E4498
	// Body parallels getSpriteMgr except:
	//   • cache check + final store target is _spritePFPool (offset 0x40), not _smPool
	//   • wait dict is _spritePFWaitLoading (offset 0x38)
	//   • asset key is `spriteName` directly (no "_manager" suffix)
	private IEnumerator getSpritePrefab(string spriteName)
	{
		if (_spritePFPool == null || _spritePFWaitLoading == null)
			throw new System.NullReferenceException();
		if (_spritePFPool.ContainsKey(spriteName)) yield break;
		IUJObjectOperation op = ResourcesLoader.GetObjectTypeAssetDynamic(
			ResourcesLoader.AssetType.MODEL,
			spriteName,
			null);
		float loadTime = 0f;
		float progress = 0f;
		while (op != null && !op.isDone)
		{
			loadTime += WAITTIME;
			progress = op.progress;
			yield return new UnityEngine.WaitForSeconds(WAITTIME);
		}
		if (op != null && op.values != null && op.values.Length > 0)
		{
			UnityEngine.Object asset = op.values[0];
			if ((UnityEngine.Object)asset != null)
			{
				_spritePFPool[spriteName] = (UnityEngine.GameObject)asset;
			}
		}
		_spritePFWaitLoading[spriteName] = null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteManagerPool/IsSpriteReady.c RVA 0x18D5F38
	// 1-1 mapping:
	//   if (_smPool == null) NRE;
	//   if (_smPool.ContainsKey(spriteName)) {
	//       if (_spritePFPool == null) NRE;
	//       if (_spritePFPool.ContainsKey(spriteName)) return true;
	//   }
	//   if (_smWaitLoading == null) NRE;
	//   if (!_smWaitLoading.ContainsKey(spriteName)) {
	//       Coroutine c = StartCoroutine(getSpriteMgr(spriteName));
	//       _smWaitLoading[spriteName] = c;
	//   }
	//   if (_spritePFWaitLoading == null) NRE;
	//   if (!_spritePFWaitLoading.ContainsKey(spriteName)) {
	//       Coroutine c = StartCoroutine(getSpritePrefab(spriteName));
	//       _spritePFWaitLoading[spriteName] = c;
	//   }
	//   return false;
	public bool IsSpriteReady(string spriteName)
	{
		if (_smPool == null) throw new System.NullReferenceException();
		if (_smPool.ContainsKey(spriteName))
		{
			if (_spritePFPool == null) throw new System.NullReferenceException();
			if (_spritePFPool.ContainsKey(spriteName)) return true;
		}
		if (_smWaitLoading == null) throw new System.NullReferenceException();
		if (!_smWaitLoading.ContainsKey(spriteName))
		{
			UnityEngine.Coroutine c = StartCoroutine(getSpriteMgr(spriteName));
			_smWaitLoading[spriteName] = c;
		}
		if (_spritePFWaitLoading == null) throw new System.NullReferenceException();
		if (!_spritePFWaitLoading.ContainsKey(spriteName))
		{
			UnityEngine.Coroutine c = StartCoroutine(getSpritePrefab(spriteName));
			_spritePFWaitLoading[spriteName] = c;
		}
		return false;
	}

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
