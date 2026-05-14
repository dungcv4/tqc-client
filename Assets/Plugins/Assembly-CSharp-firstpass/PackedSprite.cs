// Source: work/03_il2cpp_dump/dump.cs class 'PackedSprite' (TypeDefIndex 8XXX) +
//         work/06_ghidra/decompiled_full/PackedSprite/*.c bodies.
// Field offset layout verified from Ghidra _ctor.c + InitUVs.c + get_DefaultState.c:
//   0x280  pauseFrame      (int, inherited from AutoSpriteBase — ctor initializes to -1)
//   0x288  staticTexPath   (string, ctor sets to "")
//   0x290  staticTexGUID   (string, ctor sets to "")
//   0x298  _ser_stat_frame_info  (CSpriteFrame, ctor allocates new)
//   0x2A0..0x2C0  staticFrameInfo (SPRITE_FRAME struct: Rect uvs + 3 Vector2 = 40 bytes)
//   0x2C8  textureAnimations  (TextureAnim[])
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("SpriteManager 2/PackedSprite")]
public class PackedSprite : AutoSpriteBase
{
	[HideInInspector]
	public string staticTexPath;

	[HideInInspector]
	public string staticTexGUID;

	[HideInInspector]
	public CSpriteFrame _ser_stat_frame_info;

	[HideInInspector]
	public SPRITE_FRAME staticFrameInfo;

	public TextureAnim[] textureAnimations;

	// Source: Ghidra get_States.c RVA 0x01571F80 / set_States.c RVA 0x01571FEC
	// 1-1 get: lazy-init textureAnimations to `new TextureAnim[0]` if null, then return.
	// 1-1 set: textureAnimations = value (no validation).
	public override TextureAnim[] States
	{
		get
		{
			if (textureAnimations == null)
				textureAnimations = new TextureAnim[0];
			return textureAnimations;
		}
		set { textureAnimations = value; }
	}

	// Source: Ghidra get_DefaultFrame.c RVA 0x01571FFC
	// 1-1: return _ser_stat_frame_info (offset 0x298). No null guard — Ghidra emits direct load.
	public override CSpriteFrame DefaultFrame
	{
		get { return _ser_stat_frame_info; }
	}

	// Source: Ghidra get_DefaultState.c RVA 0x01572004
	// 1-1:
	//   if (textureAnimations == null || textureAnimations.Length == 0) return null;
	//   int idx = pauseFrame;            // inherited from AutoSpriteBase (offset 0x280)
	//   if (idx < textureAnimations.Length) return textureAnimations[idx];
	//   return null;
	// AutoSpriteBase.pauseFrame is private, so we access it via the public AutoSpriteBase.GetPauseFrame().
	public override TextureAnim DefaultState
	{
		get
		{
			if (textureAnimations == null) return null;
			if (textureAnimations.Length == 0) return null;
			int idx = GetPauseFrame();
			if (idx < textureAnimations.Length) return textureAnimations[idx];
			return null;
		}
	}

	// Source: Ghidra get_SupportsArbitraryAnimations.c RVA 0x01572208
	// 1-1: return true; (constant — PackedSprite always supports arbitrary animations)
	public override bool SupportsArbitraryAnimations
	{
		get { return true; }
	}

	// Source: Ghidra GetDefaultPixelSize.c RVA 0x01572048 (61 lines)
	// Complex method: if staticTexGUID is empty, return Vector2.zero. Otherwise call into
	// guid2Path/loader delegates to load the texture, then return (1/(2*pixelSize.x)) * width.
	// Full port requires PathFromGUIDDelegate/AssetLoaderDelegate Invoke virtual mapping.
	// TODO RVA 0x01572048 — pending delegate Invoke vtable resolution.
	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra Awake.c RVA 0x01572210
	// Body: lazy-allocate textureAnimations[0] if null. If _ser_stat_frame_info != null, copy via
	// CSpriteFrame.ToStruct into staticFrameInfo. Then AutoSpriteBase.Awake(this) (super) and a
	// virtual call at vtable+0x218 on this (likely InitUVs or PlayDefault).
	// TODO RVA 0x01572210 — depends on AutoSpriteBase.Awake port + virtual slot mapping.
	protected override void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra Start.c RVA 0x01572478
	// Body: if (!started && SpriteBase.Start(this) and some flag) {
	//          if (textureAnimations != null && pauseFrame < textureAnimations.Length &&
	//              Application.isPlaying) {
	//              virtual call vtable+0x4a8 (likely SetState or DoAnim) with pauseFrame
	//          }
	//       }
	// TODO RVA 0x01572478 — depends on SpriteBase.Start + virtual slot mapping.
	public override void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra Init.c RVA 0x0157252C
	// 1-1: SpriteRoot.Init(this) — skip AutoSpriteBase/SpriteBase intermediate ports, call directly.
	// In C# `base.Init()` walks the override chain. Neither AutoSpriteBase nor SpriteBase declares
	// Init(), so `base.Init()` resolves directly to SpriteRoot.Init — matches Ghidra semantics.
	protected override void Init()
	{
		base.Init();
	}

	// Source: Ghidra Copy.c RVA 0x01572534 (85 lines)
	// Complex: super.Copy(s), then type-check s as PackedSprite. If uvsInitialized: direct field
	// copy of staticFrameInfo. Otherwise CSpriteFrame.ToStruct from s._ser_stat_frame_info.
	// Copies staticFrameInfo to this.staticFrameInfo, then if curAnim is null copies to frameInfo
	// + uvRect (via virtual call vtable+0x298 or CalcSize). Else if curAnim is reverse, PlayAnim
	// else virtual call vtable+0x408 (SetCurFrame?). Finally SpriteRoot.SetBleedCompensation.
	// TODO RVA 0x01572534 — multi-step depends on AutoSpriteBase.Copy + SpriteRoot virtuals.
	public override void Copy(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra InitUVs.c RVA 0x01572978
	// 1-1: copy staticFrameInfo (5 Vector2 = 40 bytes) into base.frameInfo, then duplicate uvs
	// (first 2 vec2) into base.uvRect (Rect = 16 bytes). Raw offsets:
	//   sprite+0x6c..0x8c ← packed+0x2a0..0x2c0   (frameInfo struct)
	//   sprite+0x94, +0x9c ← packed+0x2a0, +0x2a8  (uvRect.position, uvRect.size — duplicate)
	// In C# field-named form: frameInfo = staticFrameInfo; uvRect = staticFrameInfo.uvs;
	public override void InitUVs()
	{
		this.frameInfo = staticFrameInfo;
		this.uvRect = staticFrameInfo.uvs;
	}

	// Source: Ghidra AddAnimation.c RVA 0x01572994
	// 1-1: grow base.animations (UVAnimation[] at offset 0x270, inherited from AutoSpriteBase)
	// by 1, copy old elements, append `anim` at the end. NOT textureAnimations (which is
	// PackedSprite's own TextureAnim[] at 0x2C8) — Ghidra clearly accesses offset 0x270.
	//   if (animations == null) NRE;
	//   var grown = new UVAnimation[animations.Length + 1];
	//   Array.Copy(animations → grown, 0..len);
	//   animations = grown;
	//   grown[grown.Length - 1] = anim;
	public void AddAnimation(UVAnimation anim)
	{
		if (animations == null) throw new System.NullReferenceException();
		UVAnimation[] grown = new UVAnimation[animations.Length + 1];
		System.Array.Copy(animations, 0, grown, 0, animations.Length);
		animations = grown;
		if (anim == null) throw new System.NullReferenceException();
		grown[grown.Length - 1] = anim;
	}

	// Source: Ghidra Aggregate.c RVA 0x01572A68 (366 lines)
	// Extremely complex: walks textureAnimations, calls TextureAnim.Allocate, copies frames via
	// CSpriteFrame.ctor, resolves GUID→path→Texture2D via delegates (param_2/param_3/param_4),
	// accumulates List<Texture2D>+List<CSpriteFrame>, then assigns to spriteFrames+sourceTextures.
	// TODO RVA 0x01572A68 — defer until delegate Invoke virtual slot mapping is stable.
	public override void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra Create.c RVA 0x01573580
	// 1-1: new GameObject(name) → transform.position = pos → AddComponent<PackedSprite>() → return.
	// Ghidra also emits a runtime type-check on the AddComponent result (klass match) — IL2CPP
	// inserts this implicitly; managed C# AddComponent<T>() is already type-safe.
	public static PackedSprite Create(string name, Vector3 pos)
	{
		GameObject go = new GameObject(name);
		Transform t = go.transform;
		if ((UnityEngine.Object)t == null) throw new System.NullReferenceException();
		t.position = pos;
		return go.AddComponent<PackedSprite>();
	}

	// Source: Ghidra Create_1.c RVA 0x015736C0
	// 1-1: same as Create(name, pos) but also sets transform.rotation.
	public static PackedSprite Create(string name, Vector3 pos, Quaternion rotation)
	{
		GameObject go = new GameObject(name);
		Transform t = go.transform;
		if ((UnityEngine.Object)t == null) throw new System.NullReferenceException();
		t.position = pos;
		t.rotation = rotation;
		return go.AddComponent<PackedSprite>();
	}

	// Source: Ghidra _ctor.c RVA 0x01573848
	// 1-1: staticTexPath="", staticTexGUID="", _ser_stat_frame_info=new CSpriteFrame(),
	//      pauseFrame=-1 (inherited AutoSpriteBase field), base() → SpriteBase..ctor.
	// Note: pauseFrame is private on AutoSpriteBase. Ghidra emits write to offset 0x280 BEFORE
	// the base() call — typically a C# field initializer pattern. We initialize via
	// SetPauseFrame (public accessor on AutoSpriteBase) after base() runs, which produces the
	// same final state. C# IL semantics: base ctor runs first, then field initializers wrap, so
	// SetPauseFrame here is the closest semantically faithful expression.
	public PackedSprite()
	{
		staticTexPath = "";
		staticTexGUID = "";
		_ser_stat_frame_info = new CSpriteFrame();
		SetPauseFrame(-1);
	}
}
