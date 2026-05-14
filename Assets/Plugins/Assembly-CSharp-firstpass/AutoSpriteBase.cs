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

	// Source: Ghidra GetDefaultPixelSize.c RVA 0x0157969C (145 lines)
	// Pulls texture from DefaultState.spriteFrames[0] or DefaultFrame, loads via guid2Path + loader
	// delegates, returns 1/(2*defaultFrame.width) * texture.width as the inverse-uv pixel scale.
	// TODO RVA 0x0157969C — full port pending delegate Invoke vtable + DefaultFrame ergonomic.
	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		throw new AnalysisFailedException("No IL was generated.");
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

	// Source: Ghidra Copy.c RVA 0x015726A0 (122 lines)
	// SpriteBase.Copy(s); then complex UVAnimation array clone/copy logic. If !s.managed, fresh
	// UVAnimation per States[i]. Else clone s.animations directly.
	// TODO RVA 0x015726A0 — defer UVAnimation Clone + SetAnim chain pending UVAnimation port.
	public override void Copy(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra CopyAll.c RVA 0x01579FC4 (132 lines)
	// SpriteBase.Copy(s); then allocate spriteFrames clone, allocate animations clone via TextureAnim
	// Allocate per state, then UVAnimation SetAnim chain, then copy doNotTrimImages flag.
	// TODO RVA 0x01579FC4 — defer pending TextureAnim+UVAnimation port.
	public virtual void CopyAll(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra StepAnim.c RVA 0x0157A330 (228 lines)
	// Complex per-frame animation tick: advances timeSinceLastFrame, calls UVAnimation.GetNextFrame,
	// applies bleed compensation, handles loop/once/forwards/reverse, dispatches AnimCompleteDelegate
	// when end-of-animation reached, branches on UVAnimation.onAnimEnd enum (5 cases: do-nothing,
	// revert-to-static, complete-callback, hide, deactivate, destroy).
	// TODO RVA 0x0157A330 — multi-state branch port pending UVAnimation.GetNextFrame + UVAnimation
	// fields (loop direction, onAnimEnd enum).
	public override bool StepAnim(float time)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra CallAnimCompleteDelegate.c RVA 0x0157A918
	// 1-1: if (animCompleteDelegate != null) animCompleteDelegate.Invoke(this).
	protected void CallAnimCompleteDelegate()
	{
		if (animCompleteDelegate == null) return;
		animCompleteDelegate.Invoke(this);
	}

	// Source: Ghidra PlayAnim.c RVA 0x01578200 (~100 lines)
	// 1-1: if (deleted) return; if (!gameObject.activeInHierarchy) return; if (anim == null) NRE;
	//      curAnim = anim; if (!m_started) Awake(); ...timing setup + framerate compute + invoke
	//      animFrameDelegate. If anim.framerate is 0 → PauseAnim, else schedule Invoke "StepAnim".
	// TODO RVA 0x01578200 — port pending UVAnimation field map (frames, framerate, loopMode).
	public void PlayAnim(UVAnimation anim, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
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
	// 1-1: similar to PlayAnim but sets curAnim.direction = REVERSE (offset 0x24 = 1) and starts
	// from end-frame. Pending UVAnimation field map.
	// TODO RVA 0x01577874 — port pending UVAnimation reverse-direction support.
	public void PlayAnimInReverse(UVAnimation anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra PlayAnimInReverse_1.c RVA 0x01578090 — PlayAnimInReverse with frame target.
	// TODO RVA 0x01578090.
	public void PlayAnimInReverse(UVAnimation anim, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
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
	// 1-1: stub forwards to bytecode helper FUN_032a5a88 (likely PlayAnimInReverse with name+frame).
	// TODO RVA 0x0157AF98 — pending FUN_032a5a88 mapping.
	public void PlayAnimInReverse(string name, int frame)
	{
		throw new AnalysisFailedException("No IL was generated.");
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

	// Source: Ghidra SetCurFrame.c RVA 0x0157B1B8 (~40 lines)
	// 1-1: if (curAnim == null) return; if (!m_started) Awake();
	//      curAnim.curFrame = clamp(index - curAnim.startFrame, -1, frames.Length + 1).
	//      framesToAdvance = timeBetweenAnimFrames; virtual vtable+0x498 (StepAnim or NextFrame).
	// TODO RVA 0x0157B1B8 — pending UVAnimation curFrame/startFrame field exposure.
	public void SetCurFrame(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
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
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra Aggregate.c RVA 0x0157B730 (337 lines)
	// Walks States[], allocates TextureAnim per state, allocates per-frame CSpriteFrame from
	// state.frames.Length, resolves GUID→path→Texture2D via delegates, accumulates List<Texture2D>+
	// List<CSpriteFrame>, then assigns ToArray results to sourceTextures+spriteFrames.
	// TODO RVA 0x0157B730 — extreme complexity; defer until TextureAnim+CSpriteFrame are fully exposed.
	public virtual void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra _ctor.c RVA 0x015738F8
	// 1-1: pauseFrame = -1 (offset 0x280); SpriteBase..ctor.
	// pauseFrame initializer expressed as `private int pauseFrame = -1;` field initializer above.
	protected AutoSpriteBase() { }
}
