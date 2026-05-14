// Source: work/03_il2cpp_dump/dump.cs class 'TextureAnim' (TypeDefIndex 8178) +
//         work/06_ghidra/decompiled_full/TextureAnim/*.c bodies.
// Field offset layout verified from dump.cs:
//   0x10 name           (string)
//   0x18 loopCycles     (int)
//   0x1c loopReverse    (bool)
//   0x20 framerate      (float, init to 15.0f = 0x41700000)
//   0x24 onAnimEnd      (ANIM_END_ACTION enum)
//   0x28 framePaths     (string[])
//   0x30 frameGUIDs     (string[])
//   0x38 spriteFrames   (CSpriteFrame[])
using System;
using UnityEngine;

[Serializable]
public class TextureAnim
{
	public string name;

	public int loopCycles;

	public bool loopReverse;

	public float framerate;

	public UVAnimation.ANIM_END_ACTION onAnimEnd;

	[HideInInspector]
	public string[] framePaths;

	[HideInInspector]
	public string[] frameGUIDs;

	[HideInInspector]
	public CSpriteFrame[] spriteFrames;

	// Source: Ghidra Allocate.c RVA 0x015733D0
	// 1-1:
	//   if (framePaths == null) framePaths = new string[0];
	//   if (frameGUIDs == null) frameGUIDs = new string[0];
	//   if (spriteFrames != null && spriteFrames.Length == frameGUIDs.Length) return;  // already sized
	//   int newLen = max(framePaths.Length, frameGUIDs.Length);
	//   spriteFrames = new CSpriteFrame[newLen];
	//   for (int i = 0; i < newLen; i++) spriteFrames[i] = new CSpriteFrame();
	public void Allocate()
	{
		if (framePaths == null) framePaths = new string[0];
		if (frameGUIDs == null) frameGUIDs = new string[0];
		if (spriteFrames != null && spriteFrames.Length == frameGUIDs.Length) return;
		int newLen = (framePaths.Length > frameGUIDs.Length) ? framePaths.Length : frameGUIDs.Length;
		spriteFrames = new CSpriteFrame[newLen];
		for (int i = 0; i < newLen; i++)
		{
			spriteFrames[i] = new CSpriteFrame();
		}
	}

	// Source: Ghidra _ctor.c RVA 0x01579328
	// 1-1: framerate = 15.0f (0x41700000); System.Object..ctor; Allocate().
	public TextureAnim()
	{
		framerate = 15f;
		Allocate();
	}

	// Source: Ghidra _ctor_1.c RVA 0x0157934C
	// 1-1: framerate = 15.0f; System.Object..ctor; name = n; Allocate().
	public TextureAnim(string n)
	{
		framerate = 15f;
		name = n;
		Allocate();
	}

	// Source: Ghidra Copy.c RVA 0x0157938C
	// 1-1: if (a == null) NRE;
	//      copy name/loopCycles/loopReverse/framerate/onAnimEnd;
	//      if a.framePaths != null: clone array;
	//      if a.frameGUIDs != null: clone array;
	//      if a.spriteFrames != null: spriteFrames = new CSpriteFrame[a.spriteFrames.Length],
	//         for each: spriteFrames[i] = new CSpriteFrame(a.spriteFrames[i]).
	public void Copy(TextureAnim a)
	{
		if (a == null) throw new System.NullReferenceException();
		name        = a.name;
		loopCycles  = a.loopCycles;
		loopReverse = a.loopReverse;
		framerate   = a.framerate;
		onAnimEnd   = a.onAnimEnd;
		if (a.framePaths == null) throw new System.NullReferenceException();
		framePaths = new string[a.framePaths.Length];
		System.Array.Copy(a.framePaths, framePaths, a.framePaths.Length);
		if (a.frameGUIDs == null) throw new System.NullReferenceException();
		frameGUIDs = new string[a.frameGUIDs.Length];
		System.Array.Copy(a.frameGUIDs, frameGUIDs, a.frameGUIDs.Length);
		if (a.spriteFrames == null) throw new System.NullReferenceException();
		spriteFrames = new CSpriteFrame[a.spriteFrames.Length];
		for (int i = 0; i < a.spriteFrames.Length; i++)
		{
			spriteFrames[i] = new CSpriteFrame(a.spriteFrames[i]);
		}
	}
}
