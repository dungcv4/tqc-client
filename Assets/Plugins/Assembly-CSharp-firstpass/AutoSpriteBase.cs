// Source: work/03_il2cpp_dump/dump.cs class 'AutoSpriteBase' (TypeDefIndex 8179) +
//         work/06_ghidra/decompiled_full/AutoSpriteBase/*.c bodies.
// Field offset layout verified from Ghidra accessors:
//   0x258  sourceTextures      (Texture2D[])      — get_SourceTextures RVA 0x0157C134
//   0x260  spriteFrames        (CSpriteFrame[])   — get_SpriteFrames   RVA 0x0157C13C
//   0x268  doNotTrimImages     (bool)             — get/set RVA 0x0157B71C
//   0x270  animations          (UVAnimation[])    — referenced in GetAnim/PlayAnim
//   0x278  curAnim             (UVAnimation)      — GetCurAnim RVA 0x0157B488
//   0x280  pauseFrame          (int, -1 sentinel) — SetPauseFrame/GetPauseFrame RVA 0x0157AA4
using Cpp2IlInjected;
using UnityEngine;

public abstract class AutoSpriteBase : SpriteBase, ISpriteAggregator, ISpritePackable
{
	protected Texture2D[] sourceTextures;

	protected CSpriteFrame[] spriteFrames;

	public bool doNotTrimImages;

	[HideInInspector]
	public UVAnimation[] animations;

	protected UVAnimation curAnim;

	private int pauseFrame = -1;

	public abstract TextureAnim[] States { get; set; }

	// Source: Ghidra get_DefaultFrame.c RVA 0x01579594
	// 1-1: var s = this.States (virtual vtable+0x608). If s != null && s.Length != 0:
	//      s[0] != null && s[0].spriteFrames != null && s[0].spriteFrames.Length != 0 → return s[0].spriteFrames[0].
	//      Otherwise NRE/IOOR fall-through.
	public virtual CSpriteFrame DefaultFrame
	{
		get
		{
			TextureAnim[] s = States;
			if (s == null || s.Length == 0) throw new System.NullReferenceException();
			if (s[0] == null || s[0].spriteFrames == null || s[0].spriteFrames.Length == 0)
				throw new System.NullReferenceException();
			return s[0].spriteFrames[0];
		}
	}

	// Source: Ghidra get_DefaultState.c RVA 0x01579624
	// 1-1: var s = this.States (virtual). If s == null → return null. Else if s.Length == 0 →
	//      return null. Else return s[0].
	public virtual TextureAnim DefaultState
	{
		get
		{
			TextureAnim[] s = States;
			if (s == null) return null;
			if (s.Length == 0) return null;
			return s[0];
		}
	}

	// Source: Ghidra get_SupportsArbitraryAnimations.c RVA 0x0157B51C — return false (constant).
	public virtual bool SupportsArbitraryAnimations { get { return false; } }

	// Source: Ghidra get_DoNotTrimImages.c RVA 0x0157B71C / set_DoNotTrimImages.c RVA 0x0157B724
	// 1-1: return/assign doNotTrimImages (byte at offset 0x268).
	public virtual bool DoNotTrimImages
	{
		get { return doNotTrimImages; }
		set { doNotTrimImages = value; }
	}

	// Source: Ghidra get_SourceTextures.c RVA 0x0157C134 — return sourceTextures (offset 0x258 = 600).
	public Texture2D[] SourceTextures { get { return sourceTextures; } }

	// Source: Ghidra get_SpriteFrames.c RVA 0x0157C13C — return spriteFrames (offset 0x260).
	public CSpriteFrame[] SpriteFrames { get { return spriteFrames; } }

	// Source: Ghidra ISpriteAggregator_get_gameObject.c RVA 0x0157C144 — return this.gameObject.
	GameObject ISpriteAggregator.gameObject { get { return gameObject; } }

	// Source: Ghidra ISpritePackable_get_gameObject.c RVA 0x0157C14C — return this.gameObject.
	GameObject ISpritePackable.gameObject { get { return gameObject; } }

	// Source: Ghidra GetDefaultPixelSize.c RVA 0x0157969C
	// 1-1:
	//   TextureAnim defState = DefaultState (virtual vtable+0x638);
	//   CSpriteFrame defFrame = DefaultFrame (virtual vtable+0x628);
	//   if (defState == null || defState.frameGUIDs == null) return Vector2.zero;
	//   if (defFrame == null || defFrame.uvs.width-or-height==0) Debug.LogWarning + return zero;
	//   if (guid2Path == null) NRE;
	//   string path = guid2Path(defState.frameGUIDs[0]);
	//   Texture2D tex = loader(path, typeof(Texture2D)) as Texture2D;
	//   if (tex != null) return (1/(2*defFrame.uvs.width)) * Vector2(tex.width, tex.height);
	//   // tex == null fallback: pull from spriteMesh.material.GetTexture("_MainTex").
	//   return defFrame.uvs.height * Vector2(mtex.width, ...).
	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		TextureAnim defState = DefaultState;
		CSpriteFrame defFrame = DefaultFrame;
		if (defState == null || defState.frameGUIDs == null) return Vector2.zero;
		if (defFrame == null || (defFrame.uvs.width == 0f && defFrame.uvs.height == 0f))
		{
			Debug.LogWarning("Sprite " + name + " has no default frame info.");
			return Vector2.zero;
		}
		if (guid2Path == null) throw new System.NullReferenceException();
		string path = guid2Path(defState.frameGUIDs[0]);
		if (loader == null) throw new System.NullReferenceException();
		Texture2D tex = loader(path, typeof(Texture2D)) as Texture2D;
		if ((UnityEngine.Object)tex != null)
		{
			float halfW = defFrame.uvs.width;
			float inv = (halfW == 0f) ? 0f : 1f / (halfW + halfW);
			return new Vector2(inv * tex.width, inv * tex.height);
		}
		// Fallback: managed spriteMesh.material.mainTexture
		if (m_spriteMesh == null) return Vector2.zero;
		Material mat = m_spriteMesh.material;
		if ((UnityEngine.Object)mat == null) return Vector2.zero;
		Texture mtex = mat.GetTexture("_MainTex");
		if ((UnityEngine.Object)mtex == null) return Vector2.zero;
		float scale = defFrame.uvs.height;
		return new Vector2(scale * mtex.width, scale * mtex.height);
	}

	// Source: Ghidra SetPauseFrame.c RVA 0x01579AA4 — pauseFrame = frame.
	public void SetPauseFrame(int frame) { pauseFrame = frame; }

	// Source: Ghidra GetPauseFrame.c RVA 0x01579AAC — return pauseFrame.
	public int GetPauseFrame() { return pauseFrame; }

	// Source: Ghidra Awake.c RVA 0x015722D0
	// 1-1: SpriteBase.Awake(); if States != null { animations = new UVAnimation[States.Length];
	//      for (i=0; i<States.Length; i++) {
	//          var a = new UVAnimation();
	//          animations[i] = a;
	//          a.SetAnim(States[i], i);
	//      }
	// }
	protected override void Awake()
	{
		base.Awake();
		TextureAnim[] s = States;
		if (s == null) return;
		animations = new UVAnimation[s.Length];
		for (int i = 0; i < s.Length; i++)
		{
			UVAnimation a = new UVAnimation();
			animations[i] = a;
			if (s[i] == null) break;
			a.SetAnim(s[i], i);
		}
	}

	// Source: Ghidra Clear.c RVA 0x01579D98
	// 1-1: SpriteBase.Clear(); if (curAnim != null) { PauseAnim(); curAnim = null; }
	public override void Clear()
	{
		base.Clear();
		if (curAnim == null) return;
		PauseAnim();
		curAnim = null;
	}

	// Source: Ghidra Setup.c RVA 0x01579DE8
	// 1-1: spriteMesh.material (virtual vtable+3) → Setup(w, h, material).
	public void Setup(float w, float h)
	{
		if (m_spriteMesh == null) throw new System.NullReferenceException();
		Material mat = m_spriteMesh.material;
		Setup(w, h, mat);
	}

	// Source: Ghidra Setup_1.c RVA 0x01579EB0
	// 1-1: width = w; height = h; if (managed) spriteMesh.set_material(material) (virtual vtable+3);
	//      Init() (virtual vtable+0x218 — likely SpriteRoot.Init or derived class override).
	public void Setup(float w, float h, Material material)
	{
		width = w;
		height = h;
		if (managed)
		{
			SpriteMesh_Managed sm = m_spriteMesh as SpriteMesh_Managed;
			if (sm == null) throw new System.NullReferenceException();
			// TODO: SpriteMesh.set_material is not on ISpriteMesh interface yet; defer.
		}
		Init();
	}

	// Source: Ghidra Copy.c RVA 0x015726A0
	// 1-1:
	//   SpriteBase.Copy(s);
	//   if (s == null || s is not AutoSpriteBase asb) return;
	//   if (asb.managed) {
	//       // Reference-share path: clone asb's animations array
	//       if (asb.animations == null || asb.animations.Length == 0) return;
	//       animations = new UVAnimation[asb.animations.Length];
	//       for (int i = 0; i < asb.animations.Length; i++) {
	//           if (asb.animations[i] == null) break;
	//           animations[i] = asb.animations[i].Clone();
	//       }
	//       NRE;   // (Ghidra fall-through tail at LAB_01672964)
	//   } else {
	//       // Non-managed: build fresh UVAnimation per State
	//       if (States == null) return;
	//       if (asb.States == null) return;
	//       animations = new UVAnimation[asb.States.Length];
	//       for (int i = 0; i < asb.States.Length; i++) {
	//           UVAnimation a = new UVAnimation();
	//           animations[i] = a;
	//           a.SetAnim(asb.States[i], i);
	//       }
	//   }
	public override void Copy(SpriteRoot s)
	{
		base.Copy(s);
		if (s == null) return;
		AutoSpriteBase asb = s as AutoSpriteBase;
		if (asb == null) return;
		if (!asb.managed)
		{
			TextureAnim[] srcStates = asb.States;
			if (States == null) return;
			if (srcStates == null) return;
			animations = new UVAnimation[srcStates.Length];
			for (int i = 0; i < srcStates.Length; i++)
			{
				UVAnimation a = new UVAnimation();
				animations[i] = a;
				if (srcStates[i] == null) break;
				a.SetAnim(srcStates[i], i);
			}
		}
		else
		{
			if (asb.animations == null) return;
			if (asb.animations.Length == 0) return;
			animations = new UVAnimation[asb.animations.Length];
			for (int i = 0; i < asb.animations.Length; i++)
			{
				if (asb.animations[i] == null) break;
				animations[i] = asb.animations[i].Clone();
			}
		}
	}

	// Source: Ghidra CopyAll.c RVA 0x01579FC4
	// 1-1:
	//   SpriteBase.Copy(s);
	//   if (s == null || s is not AutoSpriteBase asb) return;
	//   if (asb.States == null) return;
	//   States = new TextureAnim[asb.States.Length];   // virtual set_States vtable+0x618
	//   for (int i = 0; i < asb.States.Length; i++) {
	//       TextureAnim t = new TextureAnim();
	//       t.framerate = 15f;     // const 0x41700000
	//       t.Allocate();
	//       States[i] = t;
	//   }
	//   if (animations == null) {
	//       animations = new UVAnimation[States.Length];
	//   }
	//   for (int i = 0; i < animations.Length; i++) {
	//       if (States[i] == null) break;
	//       TextureAnim.Copy(States[i], asb.States[i]);
	//       UVAnimation a = new UVAnimation();
	//       animations[i] = a;
	//       a.SetAnim(States[i], i);
	//   }
	//   doNotTrimImages = asb.doNotTrimImages;
	public virtual void CopyAll(SpriteRoot s)
	{
		base.Copy(s);
		if (s == null) return;
		AutoSpriteBase asb = s as AutoSpriteBase;
		if (asb == null) return;
		TextureAnim[] srcStates = asb.States;
		if (srcStates == null) return;
		TextureAnim[] newStates = new TextureAnim[srcStates.Length];
		for (int i = 0; i < srcStates.Length; i++)
		{
			TextureAnim t = new TextureAnim();
			t.framerate = 15f;
			t.Allocate();
			newStates[i] = t;
		}
		States = newStates;
		if (animations == null)
		{
			animations = new UVAnimation[newStates.Length];
		}
		for (int i = 0; i < animations.Length; i++)
		{
			if (newStates[i] == null) break;
			newStates[i].Copy(srcStates[i]);
			UVAnimation a = new UVAnimation();
			animations[i] = a;
			a.SetAnim(newStates[i], i);
		}
		doNotTrimImages = asb.doNotTrimImages;
	}

	// Source: Ghidra StepAnim.c RVA 0x0157A330
	// 1-1 per-frame animation tick:
	//   if (curAnim == null) return false;
	//   timeSinceLastFrame += time;
	//   framesToAdvance = timeSinceLastFrame / timeBetweenAnimFrames;
	//   if (framesToAdvance < 1) {
	//       if (crossfadeFrames) SetColor(white-with-alpha=1-fraction);   // virtual vtable+0x318
	//       return true;
	//   }
	//   while (curAnim.GetNextFrame(ref nextFrameInfo)) {
	//       framesToAdvance -= 1; timeSinceLastFrame -= timeBetweenAnimFrames;
	//       if (framesToAdvance < 1) {
	//           if (crossfadeFrames) { ... } else apply nextFrameInfo to spriteMesh.vertices.
	//           ... bleed compensation + CalcSize OR vtable+0x298 (CalcEdges-equivalent) ...
	//           if (pauseFrame != -1 && curAnim.curFrame == pauseFrame) { PauseAnim(); pauseFrame=-1; }
	//           return true;
	//       }
	//   }
	//   // Animation ended.
	//   if (pauseFrame == -1) {
	//       if (crossfadeFrames) SetColor(white);
	//       switch (curAnim.onAnimEnd) {
	//           case Do_Nothing:        PauseAnim + uvRect = frameInfo.uvs + SetBleedCompensation + maybe CalcSize.
	//           case Revert_To_Static:  RevertToStatic.
	//           case Play_Default_Anim: invoke animCompleteDelegate; PlayAnim(defaultAnim). return false.
	//           case Hide:              vtable+0x378 (Hide(true)).
	//           case Deactivate:        gameObject.SetActive(false).
	//           case Destroy:           invoke delegate; vtable+0x258 (Delete); Object.Destroy(gameObject).
	//       }
	//       if (animFrameDelegate != null && onAnimEnd != Destroy) animFrameDelegate.Invoke(this, ...);
	//       if (!currentlyAnimating) { curAnim = null; }
	//   } else if (curAnim.curFrame == pauseFrame) {
	//       PauseAnim(); pauseFrame = -1;
	//   }
	//   return false;
	public override bool StepAnim(float time)
	{
		if (curAnim == null) return false;
		timeSinceLastFrame += time;
		framesToAdvance = timeSinceLastFrame / timeBetweenAnimFrames;
		if (framesToAdvance < 1f)
		{
			if (crossfadeFrames)
			{
				SetColor(new Color(1f, 1f, 1f, 1f - framesToAdvance));
			}
			return true;
		}
		while (curAnim.GetNextFrame(ref nextFrameInfo))
		{
			framesToAdvance      -= 1f;
			timeSinceLastFrame   -= timeBetweenAnimFrames;
			if (framesToAdvance < 1f)
			{
				if (crossfadeFrames)
				{
					// Apply nextFrameInfo to spriteMesh quad via vertex array (vtable+4 = get_vertices).
					if (m_spriteMesh != null)
					{
						Vector3[] verts = m_spriteMesh.vertices;
						if (verts != null && verts.Length >= 4)
						{
							float xMin = nextFrameInfo.topLeftOffset.x;
							float xMax = nextFrameInfo.bottomRightOffset.x;
							float yMin = nextFrameInfo.bottomRightOffset.y;
							float yMax = nextFrameInfo.topLeftOffset.y;
							verts[0] = new Vector3(xMin, yMax, 0f);
							verts[1] = new Vector3(xMin, yMin, 0f);
							verts[2] = new Vector3(xMax, yMin, 0f);
							verts[3] = new Vector3(xMax, yMax, 0f);
							if (curAnim != null && curAnim.frames != null)
							{
								int prevFrame = curAnim.curFrame;
								int prevDir   = curAnim.stepDir;
								int len = curAnim.frames.Length;
								int target = prevFrame; if (len + 1 < prevFrame) target = len + 1; if (prevFrame < -1) target = -1;
								curAnim.curFrame = target;
								if (prevDir < 0)
								{
									curAnim.stepDir = -1;
									curAnim.playInReverse = true;
								}
								else
								{
									curAnim.stepDir = 1;
								}
								SetColor(new Color(1f, 1f, 1f, 1f - framesToAdvance));
							}
						}
					}
				}
				uvRect = frameInfo.uvs;
				SetBleedCompensation(bleedCompensation);
				if (autoResize || pixelPerfect) CalcSize();
				else if ((int)anchor == 9) CalcSize();   // TEXTURE_OFFSET — recompute extents
				if (pauseFrame != -1 && curAnim != null && curAnim.curFrame == pauseFrame)
				{
					PauseAnim();
					pauseFrame = -1;
				}
				return true;
			}
			if (curAnim == null) break;
		}
		// Animation ended OR pauseFrame reached.
		if (pauseFrame == -1)
		{
			if (crossfadeFrames) SetColor(new Color(1f, 1f, 1f, 1f));
			if (curAnim != null)
			{
				switch ((int)curAnim.onAnimEnd)
				{
					case 0:   // Do_Nothing
						PauseAnim();
						uvRect = frameInfo.uvs;
						SetBleedCompensation(bleedCompensation);
						if (autoResize || pixelPerfect) CalcSize();
						break;
					case 1:   // Revert_To_Static
						RevertToStatic();
						break;
					case 2:   // Play_Default_Anim
						if (animCompleteDelegate != null) animCompleteDelegate.Invoke(this);
						PlayAnim(defaultAnim);
						return false;
					case 3:   // Hide
						Hide(true);
						break;
					case 4:   // Deactivate
						{
							GameObject go = gameObject;
							if (go == null) break;
							go.SetActive(false);
						}
						break;
					case 5:   // Destroy
						if (animCompleteDelegate != null) animCompleteDelegate.Invoke(this);
						Delete();
						UnityEngine.Object.Destroy(gameObject);
						break;
				}
				if (animCompleteDelegate != null && curAnim != null && (int)curAnim.onAnimEnd != 5)
				{
					animCompleteDelegate.Invoke(this);
				}
				if (!currentlyAnimating)
				{
					curAnim = null;
				}
			}
			return false;
		}
		if (curAnim != null && curAnim.curFrame == pauseFrame)
		{
			PauseAnim();
			pauseFrame = -1;
		}
		return false;
	}

	// Source: Ghidra CallAnimCompleteDelegate.c RVA 0x0157A918
	// 1-1: if (animCompleteDelegate != null) animCompleteDelegate.Invoke(this).
	protected void CallAnimCompleteDelegate()
	{
		if (animCompleteDelegate == null) return;
		animCompleteDelegate.Invoke(this);
	}

	// Source: Ghidra PlayAnim.c RVA 0x01578200
	// 1-1:
	//   if (deleted) return;
	//   if (!gameObject.activeInHierarchy) return;
	//   if (anim == null || anim.frames == null || anim.frames.Length == 0) return;
	//   if (!m_started) Awake();          // virtual vtable+0x208
	//   curAnim = anim;
	//   curAnim.numLoops = 0; curAnim.playInReverse = false; curAnim.frames = anim.frames;   (param_1[0x41] = anim.index)
	//   curAnimIndex = anim.index;
	//   if (anim.frames != null) {
	//       int len = anim.frames.Length;
	//       int targetFrame = clamp(frame-1, -1, len+1);
	//       curAnim.curFrame = targetFrame;
	//       pauseFrame = -1;
	//       float fr = anim.framerate;
	//       float invFr = (fr != 0) ? 1/fr : 1f;
	//       timeBetweenAnimFrames = invFr;
	//       framesToAdvance = invFr;
	//       if (anim.frames.Length < 2 || fr == 0) {
	//           // single-frame / zero-framerate path → PauseAnim + invoke complete + step
	//           if (animFrameDelegate != null) {
	//               if (fr == 0) animFrameDelegate.Invoke(this, targetFrame);
	//               else Invoke("StepAnim", 1/fr);
	//           }
	//           PauseAnim();
	//           StepAnim(0);
	//       } else {
	//           StepAnim(0);
	//           if (!currentlyAnimating) AddToAnimatedList();   // virtual vtable+0x4F8
	//       }
	//   }
	public void PlayAnim(UVAnimation anim, int frame)
	{
		if (deleted) return;
		if (!gameObject.activeInHierarchy) return;
		if (anim == null || anim.frames == null || anim.frames.Length == 0) return;
		if (!m_started) SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
		curAnim = anim;
		curAnimIndex = anim.index;
		anim.frames = anim.frames;        // keep — Ghidra sets curAnim.frames = anim.frames (alias)
		anim.numLoops = 0;
		anim.playInReverse = false;
		int len = anim.frames.Length;
		int target = frame - 1;
		if (len + 1 < target) target = len + 1;
		if (target < -1)      target = -1;
		anim.curFrame = target;
		pauseFrame = -1;
		float fr = anim.framerate;
		float invFr = (fr != 0f) ? 1f / fr : 1f;
		// Ghidra PlayAnim.c lines 62-63 (also PlayAnimInReverse.c line 41-42):
		//   *(float *)((long)param_1 + 0x224) = invFr;   // timeBetweenAnimFrames (+0x224)
		//   *(float *)(param_1 + 0x44)        = invFr;   // timeSinceLastFrame    (+0x220)
		// (param_1 is long*; +0x44 means +0x44*8 = +0x220 byte offset)
		// Previous port wrote framesToAdvance (+0x228) which has no effect — StepAnim
		// overwrites framesToAdvance from timeSinceLastFrame / timeBetweenAnimFrames.
		// The correct 1-1 binary semantic: seed timeSinceLastFrame = invFr so that
		// StepAnim(0f) immediately computes framesToAdvance = 1.0 and triggers the first
		// GetNextFrame → frame 0 UVs propagate to mesh.
		timeBetweenAnimFrames = invFr;
		timeSinceLastFrame    = invFr;
		if (anim.frames.Length < 2 || fr == 0f)
		{
			PauseAnim();
			if (animFrameDelegate != null)
			{
				if (fr == 0f)
				{
					animFrameDelegate.Invoke(this, target);
				}
				else
				{
					Invoke("StepAnim", 1f / fr);
				}
			}
			StepAnim(0f);
			return;
		}
		StepAnim(0f);
		if (currentlyAnimating) return;
		AddToAnimatedList();
	}

	// Source: Ghidra PlayAnim_1.c RVA 0x01572970 — PlayAnim(anim, 0).
	public void PlayAnim(UVAnimation anim) { PlayAnim(anim, 0); }

	// Source: Ghidra PlayAnim_2.c RVA 0x0157A950
	// 1-1: if animations == null → NRE; if (index >= animations.Length) Debug.LogError; else
	//      PlayAnim(animations[index], frame).
	public void PlayAnim(int index, int frame)
	{
		if (animations == null) throw new System.NullReferenceException();
		if (index >= animations.Length)
		{
			Debug.LogError("Specified animation index " + index.ToString() + " out of range.");
			return;
		}
		PlayAnim(animations[index], frame);
	}

	// Source: Ghidra PlayAnim_3.c RVA 0x0157AA54 — PlayAnim(index, 0) but inlined.
	public override void PlayAnim(int index)
	{
		if (animations == null) throw new System.NullReferenceException();
		if (index >= animations.Length)
		{
			Debug.LogError("Specified animation index " + index.ToString() + " out of range.");
			return;
		}
		PlayAnim(animations[index], 0);
	}

	// Source: Ghidra PlayAnim_4.c RVA 0x0157AB54
	// 1-1: linear search through animations[i].name (offset 0x30); if match → PlayAnim(found, frame).
	public void PlayAnim(string name, int frame)
	{
		if (animations == null) throw new System.NullReferenceException();
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] == null) break;
			if (animations[i].name == name)
			{
				PlayAnim(animations[i], frame);
				return;
			}
		}
	}

	// Source: Ghidra PlayAnim_5.c RVA 0x0157AC00 — PlayAnim(name, 0).
	public override void PlayAnim(string name) { PlayAnim(name, 0); }

	// Source: Ghidra PlayAnimInReverse.c RVA 0x01577874
	// 1-1:
	//   if (deleted) return;
	//   if (!gameObject.activeInHierarchy) return;
	//   curAnim = anim; curAnim.numLoops = 0; curAnim.playInReverse = false (initial); curAnim.frames = -1;
	//   if (anim.frames != null) {
	//       curAnim.numLoops = 0;
	//       curAnim.playInReverse = true;
	//       curAnim.curFrame = anim.frames.Length;
	//       if (anim != null) {
	//           float fr = anim.framerate;
	//           float invFr = (fr != 0) ? 1/fr : 1f;
	//           timeBetweenAnimFrames = invFr;
	//           framesToAdvance       = invFr;
	//           if (anim.frames.Length < 2 || fr == 0) {
	//               PauseAnim();
	//               if (animFrameDelegate != null) animFrameDelegate.Invoke(this, length);
	//               StepAnim(0);
	//           } else {
	//               StepAnim(0);
	//               if (!currentlyAnimating) AddToAnimatedList();
	//           }
	//       }
	//   }
	public void PlayAnimInReverse(UVAnimation anim)
	{
		if (deleted) return;
		if (!gameObject.activeInHierarchy) return;
		curAnim = anim;
		if (anim == null) return;
		anim.numLoops = 0;
		anim.playInReverse = false;
		anim.curFrame = -1;
		if (anim.frames == null) return;
		anim.numLoops = 0;
		anim.playInReverse = true;
		anim.curFrame = anim.frames.Length;
		float fr = anim.framerate;
		float invFr = (fr != 0f) ? 1f / fr : 1f;
		// Ghidra PlayAnim.c lines 62-63 (also PlayAnimInReverse.c line 41-42):
		//   *(float *)((long)param_1 + 0x224) = invFr;   // timeBetweenAnimFrames (+0x224)
		//   *(float *)(param_1 + 0x44)        = invFr;   // timeSinceLastFrame    (+0x220)
		// (param_1 is long*; +0x44 means +0x44*8 = +0x220 byte offset)
		// Previous port wrote framesToAdvance (+0x228) which has no effect — StepAnim
		// overwrites framesToAdvance from timeSinceLastFrame / timeBetweenAnimFrames.
		// The correct 1-1 binary semantic: seed timeSinceLastFrame = invFr so that
		// StepAnim(0f) immediately computes framesToAdvance = 1.0 and triggers the first
		// GetNextFrame → frame 0 UVs propagate to mesh.
		timeBetweenAnimFrames = invFr;
		timeSinceLastFrame    = invFr;
		if (anim.frames.Length < 2 || fr == 0f)
		{
			PauseAnim();
			if (animFrameDelegate != null)
			{
				animFrameDelegate.Invoke(this, anim.frames.Length);
			}
			StepAnim(0f);
			return;
		}
		StepAnim(0f);
		if (currentlyAnimating) return;
		AddToAnimatedList();
	}

	// Source: Ghidra PlayAnimInReverse_1.c RVA 0x01578090
	// 1-1:
	//   if (deleted) return;
	//   if (!gameObject.activeInHierarchy) return;
	//   if (!m_started) Awake();
	//   curAnim = anim;
	//   curAnim.numLoops = 0; curAnim.playInReverse = false; curAnim.frames = -1;
	//   if (anim.frames != null) {
	//       int len = anim.frames.Length;
	//       curAnim.numLoops = 0;
	//       int target = frame + 1;  if (target > len+1) target = len+1; if (target < -1) target = -1;
	//       curAnim.playInReverse = true;
	//       curAnim.curFrame = target;
	//       if (anim != null) {
	//           float fr = max(anim.framerate, EPS);   // DAT_0091c048 epsilon clamp
	//           anim.framerate = fr;
	//           timeBetweenAnimFrames = 1/fr; framesToAdvance = 1/fr;
	//           if (anim.frames.Length < 2) {
	//               if (animFrameDelegate != null) animFrameDelegate.Invoke(this, target);
	//               PauseAnim(); StepAnim(0);
	//           } else {
	//               StepAnim(0);
	//               if (!currentlyAnimating) AddToAnimatedList();
	//           }
	//       }
	//   }
	public void PlayAnimInReverse(UVAnimation anim, int frame)
	{
		if (deleted) return;
		if (!gameObject.activeInHierarchy) return;
		if (!m_started) SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
		curAnim = anim;
		if (anim == null) return;
		anim.numLoops = 0;
		anim.playInReverse = false;
		anim.curFrame = -1;
		if (anim.frames == null) return;
		int len = anim.frames.Length;
		anim.numLoops = 0;
		int target = frame + 1;
		if (target > len + 1) target = len + 1;
		if (target < -1)      target = -1;
		anim.playInReverse = true;
		anim.curFrame = target;
		float fr = anim.framerate;
		const float EPS = 1e-7f;   // matches DAT_0091c048 floating epsilon
		if (fr <= EPS) fr = EPS;
		anim.framerate = fr;
		timeBetweenAnimFrames = 1f / fr;
		framesToAdvance       = 1f / fr;
		if (anim.frames.Length < 2)
		{
			if (animFrameDelegate != null)
			{
				animFrameDelegate.Invoke(this, target);
			}
			PauseAnim();
			StepAnim(0f);
			return;
		}
		StepAnim(0f);
		if (currentlyAnimating) return;
		AddToAnimatedList();
	}

	// Source: Ghidra PlayAnimInReverse_2.c RVA 0x0157AC3C
	// 1-1: if animations == null NRE; if (index >= animations.Length) Debug.LogError; else PlayAnimInReverse(animations[index]).
	public override void PlayAnimInReverse(int index)
	{
		if (animations == null) throw new System.NullReferenceException();
		if (index >= animations.Length)
		{
			Debug.LogError("Specified animation index " + index.ToString() + " out of range.");
			return;
		}
		PlayAnimInReverse(animations[index]);
	}

	// Source: Ghidra PlayAnimInReverse_3.c RVA 0x0157AD38 — PlayAnimInReverse(animations[index], frame).
	public void PlayAnimInReverse(int index, int frame)
	{
		if (animations == null) throw new System.NullReferenceException();
		if (index >= animations.Length)
		{
			Debug.LogError("Specified animation index " + index.ToString() + " out of range.");
			return;
		}
		PlayAnimInReverse(animations[index], frame);
	}

	// Source: Ghidra PlayAnimInReverse_4.c RVA 0x0157AE3C
	// 1-1: linear search through animations[i].name; if match → set frame to last, then PlayAnimInReverse.
	// Else Debug.LogError("Specified animation name " + name + " not found").
	public override void PlayAnimInReverse(string name)
	{
		if (animations == null) throw new System.NullReferenceException();
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] == null) break;
			if (animations[i].name == name)
			{
				PlayAnimInReverse(animations[i]);
				return;
			}
		}
		Debug.LogError("Specified animation name " + name + " not found.");
	}

	// Source: Ghidra PlayAnimInReverse_5.c RVA 0x0157AF98
	// 1-1: if (animations == null) NRE. Otherwise forwards to PlayAnimInReverse(name+frame chain).
	// FUN_032a5a88 is an inline helper — the visible behavior is a linear search like _4 + frame arg.
	public void PlayAnimInReverse(string name, int frame)
	{
		if (animations == null) throw new System.NullReferenceException();
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] == null) break;
			if (animations[i].name == name)
			{
				PlayAnimInReverse(animations[i], frame);
				return;
			}
		}
		Debug.LogError("Specified animation name " + name + " not found.");
	}

	// Source: Ghidra DoAnim.c RVA 0x0157B108
	// 1-1: if (curAnim != null && curAnim.index == index && currentlyAnimating) return;
	//      else PlayAnim(index) (virtual vtable+0x4a8).
	public void DoAnim(int index)
	{
		if (curAnim != null && curAnim.index == index && currentlyAnimating) return;
		PlayAnim(index);
	}

	// Source: Ghidra DoAnim_1.c RVA 0x0157B138
	// 1-1: if (curAnim != null && curAnim.name == name && currentlyAnimating) return;
	//      else PlayAnim(name) (virtual vtable+0x4b8).
	public void DoAnim(string name)
	{
		if (curAnim != null && curAnim.name == name && currentlyAnimating) return;
		PlayAnim(name);
	}

	// Source: Ghidra DoAnim_2.c RVA 0x0157B198
	// 1-1: if (curAnim == anim && currentlyAnimating) return; else PlayAnim(anim, 0).
	public void DoAnim(UVAnimation anim)
	{
		if (curAnim == anim && currentlyAnimating) return;
		PlayAnim(anim, 0);
	}

	// Source: Ghidra SetCurFrame.c RVA 0x0157B1B8
	// 1-1:
	//   if (curAnim == null) return;
	//   if (!m_started) Awake();   // virtual vtable+0x208
	//   if (curAnim.frames == null) NRE;
	//   int len = curAnim.frames.Length;
	//   int adjusted = index - curAnim.stepDir;   // (offset 0x1c relative — stepDir or startFrame)
	//   int target = adjusted;
	//   if (len + 1 < adjusted) target = len + 1;
	//   if (adjusted < -1)      target = -1;
	//   curAnim.curFrame = target;
	//   framesToAdvance = timeBetweenAnimFrames;
	//   StepAnim(0f);   // virtual vtable+0x498
	public void SetCurFrame(int index)
	{
		if (curAnim == null) return;
		if (!m_started) SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
		if (curAnim.frames == null) throw new System.NullReferenceException();
		int len = curAnim.frames.Length;
		int adjusted = index - curAnim.stepDir;
		int target = adjusted;
		if (len + 1 < adjusted) target = len + 1;
		if (adjusted < -1)      target = -1;
		curAnim.curFrame = target;
		framesToAdvance = timeBetweenAnimFrames;
		StepAnim(0f);
	}

	// Source: Ghidra SetFrame.c RVA 0x0157B258
	// 1-1: PlayAnim(anim, 0); if (currentlyAnimating) PauseAnim(); SetCurFrame(frameNum).
	public void SetFrame(UVAnimation anim, int frameNum)
	{
		PlayAnim(anim, 0);
		if (currentlyAnimating) PauseAnim();
		SetCurFrame(frameNum);
	}

	// Source: Ghidra SetFrame_1.c RVA 0x0157B298
	// 1-1: PlayAnim(anim) (virtual vtable+0x4b8 — string overload); if currentlyAnimating PauseAnim;
	//      SetCurFrame(frameNum).
	public void SetFrame(string anim, int frameNum)
	{
		PlayAnim(anim);
		if (currentlyAnimating) PauseAnim();
		SetCurFrame(frameNum);
	}

	// Source: Ghidra SetFrame_2.c RVA 0x0157B2E0
	// 1-1: PlayAnim(anim) (virtual vtable+0x4a8 — int overload); if currentlyAnimating PauseAnim;
	//      SetCurFrame(frameNum).
	public void SetFrame(int anim, int frameNum)
	{
		PlayAnim(anim);
		if (currentlyAnimating) PauseAnim();
		SetCurFrame(frameNum);
	}

	// Source: Ghidra StopAnim.c RVA 0x0157B328
	// 1-1: SpriteBase.PauseAnim() (virtual vtable+0x508); reset curAnim curFrame/stepDir/frames=null;
	//      RevertToStatic().
	// curAnim.curFrame and stepDir are protected — use UVAnimation.Reset() which mirrors the IL2CPP
	// field reset (curFrame=0, stepDir=0, frames=null). Provides equivalent behaviour.
	public override void StopAnim()
	{
		PauseAnim();
		if (curAnim != null)
		{
			curAnim.Reset();
		}
		RevertToStatic();
	}

	// Source: Ghidra UnpauseAnim.c RVA 0x015786E0
	// 1-1: pauseFrame = -1; if curAnim != null → resume via vtable+0x4F8 (likely Resume).
	public void UnpauseAnim()
	{
		pauseFrame = -1;
		if (curAnim == null) return;
		// TODO: vtable+0x4F8 (Resume) — defer to SpriteBase virtual resolution.
	}

	// Source: Ghidra AddToAnimatedList.c RVA 0x0157B36C
	// 1-1: if (!currentlyAnimating && Application.isPlaying && gameObject.activeInHierarchy) {
	//          currentlyAnimating = true; animating = true; SpriteAnimationPump.Add(this);
	//      }
	protected override void AddToAnimatedList()
	{
		if (currentlyAnimating) return;
		if (!UnityEngine.Application.isPlaying) return;
		if (!gameObject.activeInHierarchy) return;
		currentlyAnimating = true;
		animating = true;
		SpriteAnimationPump.Add(this);
	}

	// Source: Ghidra RemoveFromAnimatedList.c RVA 0x0157B42C
	// 1-1: SpriteAnimationPump.Remove(this); currentlyAnimating = false; animating = false.
	protected override void RemoveFromAnimatedList()
	{
		SpriteAnimationPump.Remove(this);
		currentlyAnimating = false;
		animating = false;
	}

	// Source: Ghidra GetCurAnim.c RVA 0x0157B488 — return curAnim.
	public UVAnimation GetCurAnim() { return curAnim; }

	// Source: Ghidra GetAnim.c RVA 0x01577798
	// 1-1: linear search animations[i].name == name; return null if not found.
	public UVAnimation GetAnim(string name)
	{
		if (animations == null) throw new System.NullReferenceException();
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] == null) break;
			if (animations[i].name == name) return animations[i];
		}
		return null;
	}

	// Source: Ghidra GetStateIndex.c RVA 0x0157B490
	// 1-1: linear search animations[i].name; if String.Equals(name, animations[i].name) → return i.
	//      Else return -1 when off the end.
	public override int GetStateIndex(string stateName)
	{
		if (animations == null) throw new System.NullReferenceException();
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] == null) break;
			if (string.Equals(animations[i].name, stateName)) return i;
		}
		return -1;
	}

	// Source: Ghidra SetState.c RVA 0x0157B50C — virtual dispatch PlayAnim(int) at vtable+0x4A8.
	public override void SetState(int index) { PlayAnim(index); }

	// Source: Ghidra GetPackedMaterial.c RVA 0x0157B524 (~80 lines)
	// 1-1: errString = "Sprite " + name + " "; if managed && manager != null:
	//          return manager.ManagedRenderer.sharedMaterial;
	//      else if (!managed && spriteMesh != null): return spriteMesh.material (virtual vtable+3);
	//      else: GetComponent<Renderer>().sharedMaterial.
	// TODO RVA 0x0157B524 — full port pending Component<Renderer> GetComponent path.
	public virtual Material GetPackedMaterial(out string errString)
	{
		errString = "Sprite " + name + " ";
		if (managed)
		{
			if ((UnityEngine.Object)manager == null)
			{
				errString = "Sprite " + name + " has no Manager assigned.";
				return null;
			}
			Renderer r = manager.get_ManagedRenderer();
			if ((UnityEngine.Object)r == null) throw new System.NullReferenceException();
			return r.sharedMaterial;
		}
		if (m_spriteMesh != null)
		{
			return m_spriteMesh.material;
		}
		// Last resort: GetComponent<Renderer>().sharedMaterial — for unmanaged sprites without
		// a spriteMesh, the material lives on the GameObject's Renderer directly.
		Renderer rend = GetComponent<Renderer>();
		if ((UnityEngine.Object)rend == null) return null;
		return rend.sharedMaterial;
	}

	// Source: Ghidra Aggregate.c RVA 0x0157B730
	// 1-1: Walk States[]. For each state with frameGUIDs.Length > framePaths.Length:
	//   - allocate spriteFrames buffer of size frameGUIDs.Length
	//   - for each frame guid: resolve guid→path→Texture2D via guid2Path/load/path2Guid delegates,
	//     accumulate Texture2D into outTextures, CSpriteFrame into outFrames.
	//   For each state with frameGUIDs.Length <= framePaths.Length (no GUID mode):
	//   - load each Texture2D via path-only path: resolve framePaths[i]→guid (via path2Guid),
	//     accumulate similarly.
	//   After all states: sourceTextures = outTextures.ToArray(); spriteFrames = outFrames.ToArray().
	public virtual void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
		var outTextures = new System.Collections.Generic.List<Texture2D>();
		var outFrames   = new System.Collections.Generic.List<CSpriteFrame>();
		TextureAnim[] states = States;
		if (states == null) throw new System.NullReferenceException();
		for (int si = 0; si < states.Length; si++)
		{
			TextureAnim state = states[si];
			if (state == null) throw new System.NullReferenceException();
			state.Allocate();
			if (state.frameGUIDs == null || state.spriteFrames == null) throw new System.NullReferenceException();
			int guidCount = state.frameGUIDs.Length;
			int pathCount = (state.framePaths != null) ? state.framePaths.Length : 0;
			if (pathCount < guidCount)
			{
				// GUID-based path: build spriteFrames buffer + resolve per GUID.
				state.spriteFrames = new CSpriteFrame[guidCount];
				state.framePaths   = new string[guidCount];
				for (int fi = 0; fi < state.spriteFrames.Length; fi++)
				{
					state.spriteFrames[fi] = new CSpriteFrame();
				}
				for (int fi = 0; fi < state.frameGUIDs.Length; fi++)
				{
					if (guid2Path == null) throw new System.NullReferenceException();
					string path = guid2Path(state.frameGUIDs[fi]);
					state.framePaths[fi] = path;
					if (load == null) throw new System.NullReferenceException();
					object loaded = load(state.frameGUIDs[fi], typeof(Texture2D));
					Texture2D tex = loaded as Texture2D;
					outTextures.Add(tex);
					outFrames.Add(state.spriteFrames[fi]);
				}
			}
			else
			{
				// Path-based: each framePath[i] is resolved via path2Guid into guid.
				for (int fi = 0; fi < state.framePaths.Length; fi++)
				{
					if (guid2Path == null) throw new System.NullReferenceException();
					object loaded = guid2Path(state.framePaths[fi]);
					Texture2D tex = loaded as Texture2D;
					outTextures.Add(tex);
					if (fi < state.spriteFrames.Length)
					{
						outFrames.Add(state.spriteFrames[fi]);
					}
				}
				state.frameGUIDs = new string[0];
			}
		}
		sourceTextures = outTextures.ToArray();
		spriteFrames   = outFrames.ToArray();
	}

	// Source: Ghidra _ctor.c RVA 0x015738F8
	// 1-1: pauseFrame = -1 (offset 0x280); SpriteBase..ctor.
	// pauseFrame initializer expressed as `private int pauseFrame = -1;` field initializer above.
	protected AutoSpriteBase() { }
}
