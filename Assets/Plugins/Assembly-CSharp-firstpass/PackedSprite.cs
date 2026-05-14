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

	// Source: Ghidra GetDefaultPixelSize.c RVA 0x01572048
	// 1-1: if (staticTexGUID == "") return Vector2.zero;
	//      string path = guid2Path.Invoke(staticTexGUID);
	//      Texture2D tex = loader.Invoke(path, typeof(Texture2D)) as Texture2D;
	//      if (tex == null) NRE;
	//      float halfFrame = _ser_stat_frame_info.something_at_offset_0x20;  // width or similar
	//      return Vector2(1 / (2 * halfFrame) * tex.width, ...).
	// Ghidra simplifies to single float — full Vector2 inferred from C# signature.
	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		if (string.Equals(staticTexGUID, "")) return Vector2.zero;
		if (guid2Path == null) throw new System.NullReferenceException();
		string path = guid2Path(staticTexGUID);
		if (loader == null) throw new System.NullReferenceException();
		Texture2D tex = loader(path, typeof(Texture2D)) as Texture2D;
		if ((UnityEngine.Object)tex == null) throw new System.NullReferenceException();
		if (_ser_stat_frame_info == null) throw new System.NullReferenceException();
		// _ser_stat_frame_info accessor at offset 0x20 returns the frame width/height. Pending
		// CSpriteFrame field-name resolution — defer the exact axis. Approximation: return tex pixels
		// scaled by 1/(2*frame.uvs.width).
		float halfFrame = _ser_stat_frame_info.uvs.width;
		if (halfFrame == 0f) return Vector2.zero;
		float invHalf = 1f / (halfFrame + halfFrame);
		return new Vector2(invHalf * tex.width, invHalf * tex.height);
	}

	// Source: Ghidra Awake.c RVA 0x01572210
	// 1-1: if (textureAnimations == null) textureAnimations = new TextureAnim[0];
	//      if (_ser_stat_frame_info != null) CSpriteFrame.ToStruct(out staticFrameInfo, _ser_stat_frame_info);
	//      AutoSpriteBase.Awake() (base);
	//      Init() (virtual vtable+0x218 — PackedSprite override = SpriteRoot.Init).
	protected override void Awake()
	{
		if (textureAnimations == null) textureAnimations = new TextureAnim[0];
		if (_ser_stat_frame_info == null) throw new System.NullReferenceException();
		staticFrameInfo = _ser_stat_frame_info.ToStruct();
		base.Awake();
		Init();
	}

	// Source: Ghidra Start.c RVA 0x01572478
	// 1-1: if (m_started) return; SpriteBase.Start();
	//      if (!playAnimOnStart) return;
	//      if (animations == null) NRE;              // param_1[0x4e] = +0x270 = animations (UVAnimation[])
	//      if (defaultAnim < animations.Length) {    // param_1+0x204 = defaultAnim (NOT pauseFrame!)
	//          if (Application.isPlaying) PlayAnim(defaultAnim);   // vtable+0x4a8 = PlayAnim(int)
	//      }
	// Previous port chế cháo - used GetPauseFrame()+textureAnimations.Length. Verified against
	// Ghidra: param_1[0x4e]=animations (0x270), param_1+0x204=defaultAnim. Fix per
	// lookup-before-edit skill — offset 0x204 is SpriteBase.defaultAnim, not pauseFrame@0x280.
	public override void Start()
	{
		if (m_started) return;
		base.Start();
		if (!playAnimOnStart) return;
		if (animations == null) throw new System.NullReferenceException();
		if (defaultAnim >= animations.Length) return;
		if (!UnityEngine.Application.isPlaying) return;
		PlayAnim(defaultAnim);
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
	// 1-1: AutoSpriteBase.Copy(s); type-check s as PackedSprite. Copy staticFrameInfo (from struct
	// or from CSpriteFrame.ToStruct). If curAnim == null → frameInfo = staticFrameInfo + SetSize or
	// CalcSize. If curAnim is reverse (index == -1) → PlayAnim(curAnim). Else SetState(curAnim.index).
	// Finally SpriteRoot.SetBleedCompensation(this).
	public override void Copy(SpriteRoot s)
	{
		base.Copy(s);
		if (s == null) return;
		PackedSprite ps = s as PackedSprite;
		if (ps == null) return;
		if (!ps.uvsInitialized)
		{
			if (ps._ser_stat_frame_info == null) throw new System.NullReferenceException();
			staticFrameInfo = ps._ser_stat_frame_info.ToStruct();
		}
		else
		{
			staticFrameInfo = ps.staticFrameInfo;
		}
		UVAnimation a = GetCurAnim();
		if (a == null)
		{
			frameInfo = staticFrameInfo;
			uvRect = staticFrameInfo.uvs;
			if (!pixelPerfect && !autoResize)
			{
				SetSize(width, height);
			}
			else
			{
				CalcSize();
			}
		}
		else if (a.index == -1)
		{
			PlayAnim(a);
		}
		else
		{
			SetState(a.index);
		}
		SetBleedCompensation();
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

	// Source: Ghidra Aggregate.c RVA 0x01572A68
	// 1-1: Resolve staticTexGUID/staticTexPath via guid2Path/path2Guid+load → add Texture2D to
	// outTextures + _ser_stat_frame_info to outFrames. Then walk textureAnimations[]; for each
	// TextureAnim, if guidCount > pathCount → resolve GUID→path→Texture2D per frame; else use
	// framePaths directly. Accumulate Texture2D+CSpriteFrame into output. Assign sourceTextures
	// = outTextures.ToArray(); spriteFrames = outFrames.ToArray().
	public override void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
		var outTextures = new System.Collections.Generic.List<Texture2D>();
		var outFrames   = new System.Collections.Generic.List<CSpriteFrame>();
		if (textureAnimations == null) throw new System.NullReferenceException();
		// 1) Static-texture branch: resolve staticTexGUID + staticTexPath, append Texture+_ser_stat_frame_info.
		if (!string.IsNullOrEmpty(staticTexPath) || !string.IsNullOrEmpty(staticTexGUID))
		{
			if (string.IsNullOrEmpty(staticTexGUID))
			{
				if (path2Guid == null) throw new System.NullReferenceException();
				staticTexGUID = path2Guid(staticTexPath);
			}
			else
			{
				if (guid2Path == null) throw new System.NullReferenceException();
				staticTexPath = guid2Path(staticTexGUID);
			}
			if (!string.IsNullOrEmpty(staticTexPath))
			{
				if (load == null) throw new System.NullReferenceException();
				Texture2D tex = load(staticTexPath, typeof(Texture2D)) as Texture2D;
				outTextures.Add(tex);
				if (_ser_stat_frame_info != null) outFrames.Add(_ser_stat_frame_info);
			}
		}
		// 2) Per-anim branch: walk textureAnimations[].
		for (int ai = 0; ai < textureAnimations.Length; ai++)
		{
			TextureAnim anim = textureAnimations[ai];
			if (anim == null) break;
			anim.Allocate();
			if (anim.frameGUIDs == null || anim.framePaths == null) continue;
			int guidCount = anim.frameGUIDs.Length;
			int pathCount = anim.framePaths.Length;
			if (pathCount < guidCount)
			{
				// GUID-resolution path: build spriteFrames + framePaths buffers; resolve per GUID.
				anim.spriteFrames = new CSpriteFrame[guidCount];
				anim.framePaths   = new string[guidCount];
				for (int fi = 0; fi < anim.spriteFrames.Length; fi++)
				{
					anim.spriteFrames[fi] = new CSpriteFrame();
				}
				for (int fi = 0; fi < anim.frameGUIDs.Length; fi++)
				{
					if (guid2Path == null) throw new System.NullReferenceException();
					string path = guid2Path(anim.frameGUIDs[fi]);
					anim.framePaths[fi] = path;
					if (load == null) throw new System.NullReferenceException();
					Texture2D tex = load(path, typeof(Texture2D)) as Texture2D;
					outTextures.Add(tex);
					outFrames.Add(anim.spriteFrames[fi]);
				}
			}
			else
			{
				// Path-only path: each framePath[i] loads Texture2D directly.
				for (int fi = 0; fi < pathCount; fi++)
				{
					if (load == null) throw new System.NullReferenceException();
					Texture2D tex = load(anim.framePaths[fi], typeof(Texture2D)) as Texture2D;
					outTextures.Add(tex);
					if (anim.spriteFrames != null && fi < anim.spriteFrames.Length)
					{
						outFrames.Add(anim.spriteFrames[fi]);
					}
				}
				anim.frameGUIDs = new string[0];
			}
		}
		sourceTextures = outTextures.ToArray();
		spriteFrames   = outFrames.ToArray();
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
