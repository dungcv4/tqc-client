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

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteManagerPool/createSprite.c RVA 0x18D6094
	// 1-1 mapping (~300-line method); string literals resolved from work/03_il2cpp_dump/script.json:
	//   13237 "_"    13271 "_Color"   13328 "_Grayscale"  13363 "_LightColor"
	//   13378 "_Metal"   13379 "_MetalGamma"   13380 "_MetalLevels"   13481 "_Transparent"
	//   13629 "_additive"   13639 "_gray_"   13669 "_transparent"
	//   10428 "SpriteManager or SpritePrefab is null...createSprite failed...({0})"
	//   16490 "entity"  (layer name for instantiated SpriteManager GO)
	// Field offsets used here (from ctor + Start):
	//   _smPool       @ 0x28   _smPoolInst   @ 0x30
	//   _spritePFPool @ 0x40
	//   RoleETC1AdditiveShader    @ 0x70
	//   RoleETC1TransparentShader @ 0x78
	//   RoleETC1GrayShader        @ 0x80
	public PackedSprite createSprite(string spriteName, byte r = byte.MaxValue, byte g = byte.MaxValue, byte b = byte.MaxValue, byte a = byte.MaxValue, byte lr = byte.MaxValue, byte lg = byte.MaxValue, byte lb = byte.MaxValue, byte la = byte.MaxValue, bool additiveShader = false, bool transparentShader = false, byte transparentAlpha = 0, bool grayShader = false, byte grayScale = 0, byte metal = 0, byte metalLevelsX = 0, byte metalLevelsY = byte.MaxValue, byte metalLevelsZ = 0, byte metalLevelsW = byte.MaxValue, byte metalGamma = 0)
	{
		// Ghidra line 65-68: if (!IsSpriteReady(spriteName)) return null;
		if (!IsSpriteReady(spriteName)) return null;

		// Ghidra line 69-103: build the base hash key
		//   key = spriteName + "_" + ColorUtility.ToHtmlStringRGBA(Color(r,g,b,a)/255)
		//                    + "_" + ColorUtility.ToHtmlStringRGBA(Color(lr,lg,lb,la)/255)
		string[] arr5 = new string[5];
		arr5[0] = spriteName;
		arr5[1] = "_";
		arr5[2] = UnityEngine.ColorUtility.ToHtmlStringRGBA(
			new UnityEngine.Color((float)r / 255f, (float)g / 255f, (float)b / 255f, (float)a / 255f));
		arr5[3] = "_";
		arr5[4] = UnityEngine.ColorUtility.ToHtmlStringRGBA(
			new UnityEngine.Color((float)lr / 255f, (float)lg / 255f, (float)lb / 255f, (float)la / 255f));
		string hashKey = string.Concat(arr5);

		// Ghidra line 104-174: append shader-specific suffix
		if (additiveShader)
		{
			// Line 173: hashKey += "_additive"
			hashKey = string.Concat(hashKey, "_additive");
		}
		else if (transparentShader)
		{
			// Line 165-169: hashKey += "_transparent"; if (transparentAlpha != 0) hashKey += "_" + transparentAlpha
			hashKey = string.Concat(hashKey, "_transparent");
			if (transparentAlpha != 0)
			{
				hashKey = string.Concat(hashKey, "_", transparentAlpha.ToString());
			}
		}
		else if (grayShader)
		{
			// Line 106-161: build 15-element string concat for gray-mode metallic params.
			string[] arr15 = new string[15];
			arr15[0]  = hashKey;
			arr15[1]  = "_gray_";
			arr15[2]  = grayScale.ToString();
			arr15[3]  = "_";
			arr15[4]  = metal.ToString();
			arr15[5]  = "_";
			arr15[6]  = metalLevelsX.ToString();
			arr15[7]  = "_";
			arr15[8]  = metalLevelsY.ToString();
			arr15[9]  = "_";
			arr15[10] = metalLevelsZ.ToString();
			arr15[11] = "_";
			arr15[12] = metalLevelsW.ToString();
			arr15[13] = "_";
			arr15[14] = metalGamma.ToString();
			hashKey = string.Concat(arr15);
		}

		// Ghidra line 175-300: look up `_smPoolInst[hashKey]` (cached SpriteManager).
		// If MISS → instantiate from _smPool[spriteName] prefab, configure material, cache.
		// If HIT  → just retrieve sm.
		if (_smPoolInst == null) throw new System.NullReferenceException();
		SpriteManager sm = null;
		if (!_smPoolInst.ContainsKey(hashKey))
		{
			// Ghidra line 179-184: _smPool[spriteName] is the GameObject prefab for the manager.
			if (_smPool == null) throw new System.NullReferenceException();
			if (!_smPool.ContainsKey(spriteName))
			{
				// Ghidra LAB_019d6968 fallback: log lit 10428 + return null.
				string msg = string.Format("SpriteManager or SpritePrefab is null...createSprite failed...({0})", spriteName);
				UJDebug.Log(msg);
				return null;
			}

			// Ghidra line 185-209: instantiate prefab under this.transform, name = hashKey, layer = "entity".
			UnityEngine.GameObject prefabSM = _smPool[spriteName];
			if ((UnityEngine.Object)prefabSM != null)
			{
				UnityEngine.GameObject hostGO = base.gameObject;
				if ((UnityEngine.Object)hostGO == null) throw new System.NullReferenceException();
				UnityEngine.Transform parent = hostGO.transform;
				UnityEngine.GameObject inst = UnityEngine.Object.Instantiate(prefabSM, parent);
				if ((UnityEngine.Object)inst == null) throw new System.NullReferenceException();
				inst.name = hashKey;
				inst.layer = UnityEngine.LayerMask.NameToLayer("entity");
				sm = inst.GetComponent<SpriteManager>();
				_smPoolInst[hashKey] = sm;
			}

			// Ghidra line 213-294: shader / material setup on the freshly instantiated SpriteManager.
			if ((UnityEngine.Object)sm != null)
			{
				UnityEngine.Renderer rnd = sm.GetComponent<UnityEngine.Renderer>();
				if ((UnityEngine.Object)rnd != null)
				{
					UnityEngine.Material mat = rnd.material;
					if ((UnityEngine.Object)mat != null)
					{
						if (additiveShader)
						{
							// Line 276-279
							mat.shader = this.RoleETC1AdditiveShader;
						}
						else if (transparentShader)
						{
							// Line 264-272
							mat.shader = this.RoleETC1TransparentShader;
							if (transparentAlpha != 0)
							{
								mat.SetFloat("_Transparent", (float)transparentAlpha / 100f);
							}
						}
						else if (grayShader)
						{
							// Line 231-260
							mat.shader = this.RoleETC1GrayShader;
							mat.SetFloat("_Grayscale", (float)grayScale / 100f);
							mat.SetFloat("_Metal", (float)metal / 100f);
							mat.SetVector("_MetalLevels", new UnityEngine.Vector4(
								(float)metalLevelsX, (float)metalLevelsY, (float)metalLevelsZ, (float)metalLevelsW));
							mat.SetFloat("_MetalGamma", (float)metalGamma / 100f);
						}
						// Line 282-290: always-applied colors.
						mat.SetColor("_Color",
							new UnityEngine.Color((float)r / 255f, (float)g / 255f, (float)b / 255f, (float)a / 255f));
						mat.SetColor("_LightColor",
							new UnityEngine.Color((float)lr / 255f, (float)lg / 255f, (float)lb / 255f, (float)la / 255f));
						// Line 291
						rnd.sortingOrder = 1;
					}
				}
			}
		}
		else
		{
			// Ghidra line 296-300: cache hit — retrieve sm.
			sm = _smPoolInst[hashKey];
		}

		// Ghidra line 301-348: now look up sprite prefab + delegate to SpriteManager.CreateSprite.
		if (_spritePFPool == null) throw new System.NullReferenceException();
		UnityEngine.GameObject spritePF = null;
		if (_spritePFPool.ContainsKey(spriteName))
		{
			spritePF = _spritePFPool[spriteName];
		}

		if ((UnityEngine.Object)sm == null || (UnityEngine.Object)spritePF == null)
		{
			// Ghidra LAB_019d6968: log lit 10428 + return null.
			string msg = string.Format("SpriteManager or SpritePrefab is null...createSprite failed...({0})", spriteName);
			UJDebug.Log(msg);
			return null;
		}

		// Ghidra line 326-337: result = sm.CreateSprite(spritePF) — cast to PackedSprite.
		// The Cpp2IL type check (PTR_DAT_03446a60 = PackedSprite klass) corresponds to C#
		// `(PackedSprite)spriteRoot`, which throws InvalidCastException on type mismatch.
		SpriteRoot spriteRoot = sm.CreateSprite(spritePF);
		if (spriteRoot == null) return null;
		return (PackedSprite)spriteRoot;
	}

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
