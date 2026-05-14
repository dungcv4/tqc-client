// Source: work/03_il2cpp_dump/dump.cs class 'SpriteBase' + work/06_ghidra/decompiled_full/SpriteBase/*.c bodies.
// Field offset layout verified from Ghidra accessors:
//   0x200 isMirror (inherited SpriteRoot)
//   0x201 playAnimOnStart  0x202 crossfadeFrames  0x204 defaultAnim (int)
//   0x208 curAnimIndex     0x210 animCompleteDelegate  0x218 animFrameDelegate
//   0x220 timeSinceLastFrame  0x224 timeBetweenAnimFrames  0x228 framesToAdvance
//   0x22c animating  0x22d currentlyAnimating  0x230..0x257 nextFrameInfo (SPRITE_FRAME)
using Cpp2IlInjected;
using UnityEngine;

[ExecuteInEditMode]
public abstract class SpriteBase : SpriteRoot, ISpriteAnimatable
{
	public delegate void AnimCompleteDelegate(SpriteBase sprite);

	public delegate void AnimFrameDelegate(SpriteBase sprite, int frame);

	public bool playAnimOnStart;

	[HideInInspector]
	public bool crossfadeFrames;

	public int defaultAnim;

	// HAND-FIX: promote to internal so AutoSpriteBase (assembly-mate) can write fields per IL2CPP semantics.
	internal int curAnimIndex;

	internal AnimCompleteDelegate animCompleteDelegate;

	internal AnimFrameDelegate animFrameDelegate;

	internal float timeSinceLastFrame;

	internal float timeBetweenAnimFrames;

	internal float framesToAdvance;

	internal bool animating;

	internal bool currentlyAnimating;

	internal SPRITE_FRAME nextFrameInfo;

	// Source: Ghidra get_Animating.c RVA 0x01581320 — return animating (offset 0x22c).
	// Source: Ghidra set_Animating.c RVA 0x01581328 — if (value) PlayAnim(curAnimIndex) (virtual vtable+0x4A8).
	public bool Animating
	{
		get { return animating; }
		set
		{
			if (!value) return;
			PlayAnim(curAnimIndex);
		}
	}

	// Source: Ghidra get_CurAnimIndex.c RVA 0x01581344 / set_CurAnimIndex.c RVA 0x0158134C
	// 1-1: return/assign curAnimIndex (offset 0x208).
	public int CurAnimIndex
	{
		get { return curAnimIndex; }
		set { curAnimIndex = value; }
	}

	// Source: Ghidra Awake.c RVA 0x0157FFEC
	// 1-1: identical body to SpriteRoot.Awake — SpriteBase shadows the override with the same logic.
	// Equivalent to `base.Awake()` since the SpriteRoot.Awake reads/writes same fields.
	protected override void Awake()
	{
		base.Awake();
	}

	// Source: Ghidra Start.c RVA 0x01580230
	// 1-1: SpriteRoot.Start(); if (spriteMesh != null) spriteMesh.UseUV2 = crossfadeFrames
	//      (virtual vtable+8 on ISpriteMesh = set_UseUV2).
	public override void Start()
	{
		base.Start();
		if (m_spriteMesh == null) return;
		m_spriteMesh.UseUV2 = crossfadeFrames;
	}

	// Source: Ghidra Clear.c RVA 0x0158061C
	// 1-1: SpriteRoot.Clear(); animCompleteDelegate = null (offset 0x210).
	public override void Clear()
	{
		base.Clear();
		animCompleteDelegate = null;
	}

	// Source: Ghidra Delete.c RVA 0x015806B8
	// 1-1: if (currentlyAnimating) PauseAnim (virtual vtable+0x508); SpriteRoot.Delete().
	public override void Delete()
	{
		if (currentlyAnimating) PauseAnim();
		base.Delete();
	}

	// Source: Ghidra OnDisable.c RVA 0x01580834
	// 1-1: SpriteRoot.OnDisable(); if (currentlyAnimating) { PauseAnim(); currentlyAnimating = true; }
	// (resume-on-enable gate — preserve animating state across enable/disable cycles).
	protected override void OnDisable()
	{
		base.OnDisable();
		if (!currentlyAnimating) return;
		PauseAnim();
		currentlyAnimating = true;
	}

	// Source: Ghidra OnEnable.c RVA 0x0158090C
	// 1-1: SpriteRoot.OnEnable(); if (Application.isPlaying && currentlyAnimating) {
	//         currentlyAnimating = false; vtable+0x4F8 (AddToAnimatedList — restart anim pump).
	//      }
	protected override void OnEnable()
	{
		base.OnEnable();
		if (!UnityEngine.Application.isPlaying) return;
		if (!currentlyAnimating) return;
		currentlyAnimating = false;
		AddToAnimatedList();
	}

	// Source: Ghidra Copy.c RVA 0x01580AAC
	// 1-1: SpriteRoot.Copy(s); if (s is SpriteBase sb) { defaultAnim = sb.defaultAnim; playAnimOnStart = sb.playAnimOnStart; }
	public override void Copy(SpriteRoot s)
	{
		base.Copy(s);
		if (s == null) return;
		SpriteBase sb = s as SpriteBase;
		if (sb == null) return;
		defaultAnim = sb.defaultAnim;
		playAnimOnStart = sb.playAnimOnStart;
	}

	// Source: Ghidra Hide.c RVA 0x01580F74
	// 1-1: SpriteRoot.Hide(tf); if (tf && currentlyAnimating) PauseAnim (virtual vtable+0x508).
	public override void Hide(bool tf)
	{
		base.Hide(tf);
		if (!tf) return;
		if (!currentlyAnimating) return;
		PauseAnim();
	}

	// Source: Ghidra SetAnimCompleteDelegate.c RVA 0x01581090
	// 1-1: animCompleteDelegate = del (offset 0x210).
	public void SetAnimCompleteDelegate(AnimCompleteDelegate del)
	{
		animCompleteDelegate = del;
	}

	// Source: Ghidra SetAnimFrameDelegate.c RVA 0x015810A0
	// 1-1: animFrameDelegate = del (offset 0x218).
	public void SetAnimFrameDelegate(AnimFrameDelegate del)
	{
		animFrameDelegate = del;
	}

	// Source: Ghidra SetSpriteResizedDelegate.c RVA 0x015810B0
	// 1-1: resizedDelegate = del (offset 0x1c8 on SpriteRoot).
	public void SetSpriteResizedDelegate(SpriteResizedDelegate del)
	{
		resizedDelegate = del;
	}

	// Source: Ghidra AddSpriteResizedDelegate.c RVA 0x015810C0
	// 1-1: resizedDelegate = (SpriteResizedDelegate)Delegate.Combine(resizedDelegate, del).
	public void AddSpriteResizedDelegate(SpriteResizedDelegate del)
	{
		resizedDelegate = (SpriteResizedDelegate)System.Delegate.Combine(resizedDelegate, del);
	}

	// Source: Ghidra RemoveSpriteresizedDelegate.c RVA 0x01581154
	// 1-1: resizedDelegate = (SpriteResizedDelegate)Delegate.Remove(resizedDelegate, del).
	public void RemoveSpriteresizedDelegate(SpriteResizedDelegate del)
	{
		resizedDelegate = (SpriteResizedDelegate)System.Delegate.Remove(resizedDelegate, del);
	}

	// Source: Ghidra StepAnim.c RVA 0x015811E8 — return false; (base no-op; AutoSpriteBase overrides).
	public virtual bool StepAnim(float time) { return false; }

	// Source: Ghidra PlayAnim.c RVA 0x015811F0 — return; (base no-op; AutoSpriteBase overrides).
	public virtual void PlayAnim(int index) { }

	// Source: Ghidra PlayAnim_1.c RVA 0x015811F4 — return; (base no-op; AutoSpriteBase overrides).
	public virtual void PlayAnim(string name) { }

	// Source: Ghidra PlayAnimInReverse.c RVA 0x015811F8 — return; (base no-op).
	public virtual void PlayAnimInReverse(int index) { }

	// Source: Ghidra PlayAnimInReverse_1.c RVA 0x015811FC — return; (base no-op).
	public virtual void PlayAnimInReverse(string name) { }

	// Source: Ghidra SetFramerate.c RVA 0x01581200
	// 1-1: timeBetweenAnimFrames = 1f / fps (offset 0x224).
	public void SetFramerate(float fps)
	{
		timeBetweenAnimFrames = 1f / fps;
	}

	// Source: Ghidra PauseAnim.c RVA 0x01581074
	// 1-1: if (currentlyAnimating) RemoveFromAnimatedList() (virtual vtable+0x508).
	public void PauseAnim()
	{
		if (!currentlyAnimating) return;
		RemoveFromAnimatedList();
	}

	// Source: Ghidra StopAnim.c RVA 0x01581210 — return; (base no-op; AutoSpriteBase overrides).
	public virtual void StopAnim() { }

	// Source: Ghidra RevertToStatic.c RVA 0x01581214
	// 1-1: if (currentlyAnimating) vtable+0x4E8 (StopAnim);
	//      InitUVs() (virtual vtable+0x248);
	//      SetBleedCompensation(bleedComp.x, bleedComp.y);
	//      if (autoResize || pixelPerfect) CalcSize().
	public void RevertToStatic()
	{
		if (currentlyAnimating) StopAnim();
		InitUVs();
		SetBleedCompensation(bleedCompensation.x, bleedCompensation.y);
		if (!autoResize && !pixelPerfect) return;
		CalcSize();
	}

	protected abstract void AddToAnimatedList();

	protected abstract void RemoveFromAnimatedList();

	// Source: Ghidra IsAnimating.c RVA 0x01581318 — return animating (offset 0x22c).
	public bool IsAnimating() { return animating; }

	// Source: Ghidra _ctor.c RVA 0x01581354
	// 1-1: nextFrameInfo.uvs.position = (1,1) (offset 0x230); nextFrameInfo.uvs.size = (1,1) (0x238);
	//      nextFrameInfo.scaleFactor = (1,1) (0x240); nextFrameInfo.topLeftOffset = (1,1) (0x248);
	//      nextFrameInfo.bottomRightOffset = (1,1) (0x250). base() — SpriteRoot..ctor.
	protected SpriteBase()
	{
		nextFrameInfo.uvs = new Rect(1f, 1f, 1f, 1f);
		nextFrameInfo.scaleFactor       = Vector2.one;
		nextFrameInfo.topLeftOffset     = Vector2.one;
		nextFrameInfo.bottomRightOffset = Vector2.one;
	}
}
