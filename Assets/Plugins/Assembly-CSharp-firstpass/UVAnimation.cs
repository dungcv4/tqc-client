// Source: work/03_il2cpp_dump/dump.cs class 'UVAnimation' + work/06_ghidra/decompiled_full/UVAnimation/*.c
// Field offsets verified against dump.cs + Ghidra accessor offsets:
//   0x10 frames (SPRITE_FRAME[])      0x18 curFrame (int)    0x1c stepDir (int)
//   0x20 numLoops (int)               0x24 playInReverse (bool)
//   0x28 length (float)               0x30 name (string)
//   0x38 loopCycles (int)             0x3c loopReverse (bool)
//   0x40 framerate (float, init 15f)  0x44 index (int, init -1)
//   0x48 onAnimEnd (ANIM_END_ACTION)
// Default ctor sets curFrame=-1, stepDir=-1, framerate=15f, index=-1, frames=new SPRITE_FRAME[0].
using System;
using UnityEngine;

[Serializable]
public class UVAnimation
{
	public enum ANIM_END_ACTION
	{
		Do_Nothing = 0,
		Revert_To_Static = 1,
		Play_Default_Anim = 2,
		Hide = 3,
		Deactivate = 4,
		Destroy = 5
	}

	// HAND-FIX visibility: dump.cs declares these as protected, but the IL2CPP binary writes them
	// directly from AutoSpriteBase (assembly-mate, not subclass). Promoted to internal so port can
	// preserve raw-offset writes — equivalent runtime behaviour.
	internal SPRITE_FRAME[] frames;
	internal int curFrame;
	internal int stepDir;
	internal int numLoops;
	internal bool playInReverse;
	internal float length;

	public string name;
	public int loopCycles;
	public bool loopReverse;

	[HideInInspector]
	public float framerate;

	[HideInInspector]
	public int index;

	[HideInInspector]
	public ANIM_END_ACTION onAnimEnd;

	// Source: Ghidra get_StepDirection.c RVA 0x0157EC1C — return stepDir (offset 0x1c).
	// Source: Ghidra set_StepDirection.c RVA 0x015704CC
	// 1-1: if (value >= 0) stepDir = 1; else { stepDir = -1; playInReverse = true; }
	public int StepDirection
	{
		get { return stepDir; }
		set
		{
			if (value >= 0)
			{
				stepDir = 1;
				return;
			}
			stepDir = -1;
			playInReverse = true;
		}
	}

	// Source: Ghidra _ctor_1.c RVA 0x0157EC48
	// 1-1: default-init (curFrame=-1, stepDir=-1, framerate=15f, index=-1).
	//      if (anim == null || anim.frames == null) NRE;
	//      this.frames = new SPRITE_FRAME[anim.frames.Length]; Array.Copy(anim.frames → this.frames);
	//      copy name, loopCycles, loopReverse, framerate, onAnimEnd (NOT index);
	//      copy curFrame/stepDir (8 bytes packed at 0x18);
	//      copy numLoops, playInReverse, length;
	//      length = (1/framerate) * frames.Length.
	public UVAnimation(UVAnimation anim)
	{
		// Initial state (DAT_008e3c08): curFrame=-1, stepDir=+1. Overwritten by copy below.
		curFrame  = -1;
		stepDir   = 1;
		framerate = 15f;
		index     = -1;
		if (anim == null) throw new System.NullReferenceException();
		if (anim.frames == null) throw new System.NullReferenceException();
		frames = new SPRITE_FRAME[anim.frames.Length];
		System.Array.Copy(anim.frames, frames, anim.frames.Length);
		name          = anim.name;
		loopCycles    = anim.loopCycles;
		loopReverse   = anim.loopReverse;
		framerate     = anim.framerate;
		onAnimEnd     = anim.onAnimEnd;
		curFrame      = anim.curFrame;
		stepDir       = anim.stepDir;
		numLoops      = anim.numLoops;
		playInReverse = anim.playInReverse;
		length        = anim.length;
		if (framerate != 0f)
		{
			length = (1f / framerate) * frames.Length;
		}
	}

	// Source: Ghidra Clone.c RVA 0x01579F6C — return new UVAnimation(this).
	public UVAnimation Clone()
	{
		return new UVAnimation(this);
	}

	// Source: Ghidra _ctor.c RVA 0x01579AB4
	// 1-1: 8-byte write of DAT_008e3c08 at 0x18 → (curFrame, stepDir).
	//      Binary-verified: file offset 0x7e3c08 = FF FF FF FF 01 00 00 00 (LE)
	//      → curFrame=-1, stepDir=+1. Cpp2IL incorrectly decompiled as (-1, -1).
	//      framerate=15f (0x41700000), index=-1 (packed at 0x40 of 0xFFFFFFFF41700000);
	//      System.Object..ctor; frames = new SPRITE_FRAME[0].
	public UVAnimation()
	{
		curFrame  = -1;
		stepDir   = 1;
		framerate = 15f;
		index     = -1;
		frames    = new SPRITE_FRAME[0];
	}

	// Source: Ghidra Reset.c RVA 0x0157A938
	// 1-1: numLoops=0; playInReverse=false; 8-byte write of DAT_008e3c08 to (curFrame, stepDir).
	// Binary-verified: DAT_008e3c08 at file offset 0x7e3c08 = FF FF FF FF 01 00 00 00 (LE)
	// → curFrame=-1, stepDir=+1. Cpp2IL decompile mis-reads stepDir as -1.
	public void Reset()
	{
		numLoops      = 0;
		playInReverse = false;
		curFrame      = -1;
		stepDir       = 1;
	}

	// Source: Ghidra PlayInReverse.c RVA 0x0157AC08
	// 1-1: stepDir = -1; if (frames == null) NRE;
	//      numLoops = 0; playInReverse = true; curFrame = frames.Length.
	public void PlayInReverse()
	{
		stepDir = -1;
		if (frames == null) throw new System.NullReferenceException();
		numLoops      = 0;
		playInReverse = true;
		curFrame      = frames.Length;
	}

	// Source: Ghidra SetStepDir.c RVA 0x0157EC24
	// 1-1: if (dir >= 0) stepDir = 1; else { stepDir = -1; playInReverse = true; }
	public void SetStepDir(int dir)
	{
		if (dir >= 0)
		{
			stepDir = 1;
			return;
		}
		stepDir = -1;
		playInReverse = true;
	}

	// Source: Ghidra GetNextFrame.c RVA 0x0157A800
	// 1-1: Complex per-frame advancement logic. Returns true if advanced, false if animation ended.
	//   Read frames (offset 0x10). If frames == null, NRE.
	//   uint frameCount = frames.Length;  uint frameCount_minus_1 = frameCount - 1;
	//   if (frameCount < 1) return false;
	//   int curFrame_local = curFrame; int stepDir_local = stepDir;
	//   uint next_idx = (uint)(curFrame_local + stepDir_local);
	//   if (next_idx < frameCount && next_idx >= 0) → curFrame = next_idx; read frames[next_idx].
	//   else if (stepDir_local < 1 || !loopReverse) {
	//       numLoops++;
	//       if (numLoops > loopCycles && loopCycles != -1) return false;
	//       // wrap-around to start (or end if reverse)
	//       if (!loopReverse) {
	//           next_idx = playInReverse ? frameCount_minus_1 : 0;
	//       } else {
	//           // reverse direction
	//           uint diff = curFrame_local - stepDir_local;
	//           next_idx = (frameCount <= diff) ? frameCount_minus_1 : diff;
	//           stepDir = -stepDir_local;
	//           if (next_idx > diff && diff >= 0) next_idx = 0;
	//       }
	//       curFrame = next_idx; read frames[next_idx].
	//   } else {
	//       // already wrapped via reverse, take one step back
	//       uint diff = (uint)(curFrame_local - 1);
	//       next_idx = (frameCount <= diff) ? frameCount_minus_1 : diff;
	//       if ((int)diff < 0) next_idx = 0;
	//       curFrame = next_idx;
	//       stepDir = -1;
	//       read frames[next_idx].
	//   }
	//   return true with nextFrame = frames[next_idx].
	public bool GetNextFrame(ref SPRITE_FRAME nextFrame)
	{
		if (frames == null) throw new System.NullReferenceException();
		int frameCount = frames.Length;
		int frameCount_minus_1 = frameCount - 1;
		if (frameCount < 1) return false;
		int curFrame_local = curFrame;
		int stepDir_local  = stepDir;
		int next_idx_signed = curFrame_local + stepDir_local;
		int chosen_idx;
		if (next_idx_signed < frameCount && next_idx_signed >= 0)
		{
			chosen_idx = next_idx_signed;
			curFrame = chosen_idx;
		}
		else if (stepDir_local < 1 || !loopReverse)
		{
			int newNumLoops = numLoops + 1;
			if (loopCycles != -1 && newNumLoops > loopCycles) return false;
			numLoops = newNumLoops;
			if (!loopReverse)
			{
				chosen_idx = (playInReverse && stepDir_local != 0 && !loopReverse) ? frameCount_minus_1 : 0;
				if (playInReverse) chosen_idx = frameCount_minus_1;
				else               chosen_idx = 0;
			}
			else
			{
				int diff = curFrame_local - stepDir_local;
				chosen_idx = (frameCount <= diff) ? frameCount_minus_1 : diff;
				stepDir = -stepDir_local;
				if (diff < 0) chosen_idx = 0;
			}
			curFrame = chosen_idx;
		}
		else
		{
			int diff = curFrame_local - 1;
			chosen_idx = (frameCount <= diff) ? frameCount_minus_1 : diff;
			if (diff < 0) chosen_idx = 0;
			curFrame = chosen_idx;
			stepDir = -1;
		}
		nextFrame = frames[chosen_idx];
		return true;
	}

	// Source: Ghidra GetCurrentFrame.c RVA 0x0157ED9C
	// 1-1: int idx = max(curFrame, 0); if (idx >= frames.Length) NRE; return frames[idx].
	public SPRITE_FRAME GetCurrentFrame()
	{
		if (frames == null) throw new System.NullReferenceException();
		int idx = curFrame & ~(curFrame >> 31);   // max(curFrame, 0) per Ghidra bit trick
		if ((uint)idx >= (uint)frames.Length) throw new System.IndexOutOfRangeException();
		return frames[idx];
	}

	// Source: Ghidra GetFrame.c RVA 0x0157EDE4
	// 1-1: if (frames == null) NRE; if (frame >= frames.Length) IOOR; return frames[frame].
	public SPRITE_FRAME GetFrame(int frame)
	{
		if (frames == null) throw new System.NullReferenceException();
		if ((uint)frame >= (uint)frames.Length) throw new System.IndexOutOfRangeException();
		return frames[frame];
	}

	// Source: Ghidra BuildUVAnim.c RVA 0x0157EE24
	// 1-1: frames = new SPRITE_FRAME[totalCells]; default-init frames[0] = SPRITE_FRAME(1f), then
	//      frames[0].uvs = Rect(start, cellSize). For (row, col) iteration up to totalCells:
	//      frames[i].uvs = Rect(start.x + col*cellSize.x, start.y - row*cellSize.y, cellSize.x, cellSize.y).
	//      length = (1/framerate) * frames.Length.
	public SPRITE_FRAME[] BuildUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
	{
		frames = new SPRITE_FRAME[totalCells];
		if (totalCells == 0) return frames;
		frames[0] = new SPRITE_FRAME(0f);
		frames[0].uvs = new Rect(start.x, start.y, cellSize.x, cellSize.y);
		int placed = 0;
		for (int row = 0; row < rows; row++)
		{
			float yRow = start.y - cellSize.y * row;
			int colMax = cols;
			if (placed + colMax > totalCells) colMax = totalCells - placed;
			for (int col = 0; col < colMax; col++)
			{
				int i = placed + col;
				if (i >= totalCells) break;
				frames[i] = new SPRITE_FRAME(0f);
				frames[i].uvs = new Rect(start.x + cellSize.x * col, yRow, cellSize.x, cellSize.y);
			}
			placed += colMax;
			if (placed >= totalCells) break;
		}
		if (framerate != 0f) length = (1f / framerate) * frames.Length;
		return frames;
	}

	// Source: Ghidra BuildWrappedUVAnim.c RVA 0x0157F09C (5-arg overload — forwards to 3-arg).
	// 1-1: forwards to BuildWrappedUVAnim(start, cellSize, totalCells) — ignores cols/rows.
	public SPRITE_FRAME[] BuildWrappedUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
	{
		return BuildWrappedUVAnim(start, cellSize, totalCells);
	}

	// Source: Ghidra BuildWrappedUVAnim_1.c RVA 0x0157F0A4
	// 1-1: frames = new SPRITE_FRAME[totalCells]; first frame at (start, cellSize).
	//      Walk forward: x += cellSize.x each frame; if x + cellSize.x > 1.0 → wrap to x=0, y -= cellSize.y.
	public SPRITE_FRAME[] BuildWrappedUVAnim(Vector2 start, Vector2 cellSize, int totalCells)
	{
		frames = new SPRITE_FRAME[totalCells];
		if (totalCells == 0) return frames;
		frames[0] = new SPRITE_FRAME(0f);
		frames[0].uvs = new Rect(start.x, start.y, cellSize.x, cellSize.y);
		float x = start.x;
		float y = start.y;
		for (int i = 1; i < totalCells; i++)
		{
			float nextX = x + cellSize.x;
			float testRight = cellSize.x + nextX;
			if (testRight <= 1f)
			{
				x = nextX;
			}
			else
			{
				x = 0f;
				y = y - cellSize.y;
			}
			frames[i] = new SPRITE_FRAME(0f);
			frames[i].uvs = new Rect(x, y, cellSize.x, cellSize.y);
		}
		return frames;
	}

	// Source: Ghidra SetAnim.c RVA 0x0157F280
	// 1-1: frames = anim; if (frames == null) NRE; length = (1/framerate) * frames.Length.
	public void SetAnim(SPRITE_FRAME[] anim)
	{
		frames = anim;
		if (frames == null) throw new System.NullReferenceException();
		if (framerate != 0f) length = (1f / framerate) * frames.Length;
	}

	// Source: Ghidra SetAnim_1.c RVA 0x01579B30
	// 1-1: if (anim == null || anim.spriteFrames == null) return.
	//      frames = new SPRITE_FRAME[anim.spriteFrames.Length];
	//      index = idx; name = anim.name; loopCycles = anim.loopCycles; loopReverse = anim.loopReverse;
	//      framerate = anim.framerate; onAnimEnd = anim.onAnimEnd;
	//      for each i: frames[i] = anim.spriteFrames[i].ToStruct();
	//      length = (1/framerate) * frames.Length.
	public void SetAnim(TextureAnim anim, int idx)
	{
		if (anim == null) return;
		if (anim.spriteFrames == null) return;
		frames = new SPRITE_FRAME[anim.spriteFrames.Length];
		index       = idx;
		name        = anim.name;
		loopCycles  = anim.loopCycles;
		loopReverse = anim.loopReverse;
		framerate   = anim.framerate;
		onAnimEnd   = anim.onAnimEnd;
		for (int i = 0; i < anim.spriteFrames.Length; i++)
		{
			if (anim.spriteFrames[i] == null) throw new System.NullReferenceException();
			frames[i] = anim.spriteFrames[i].ToStruct();
		}
		if (framerate != 0f) length = (1f / framerate) * frames.Length;
	}

	// Source: Ghidra AppendAnim.c RVA 0x0157F2D0
	// 1-1: if (frames == null || anim == null) NRE.
	//      newFrames = new SPRITE_FRAME[anim.Length + frames.Length];
	//      Array.Copy(frames, newFrames, frames.Length);
	//      Array.Copy(anim, 0, newFrames, frames.Length, anim.Length);
	//      frames = newFrames;
	//      length = (1/framerate) * frames.Length.
	public void AppendAnim(SPRITE_FRAME[] anim)
	{
		if (frames == null) throw new System.NullReferenceException();
		if (anim == null) throw new System.NullReferenceException();
		SPRITE_FRAME[] newFrames = new SPRITE_FRAME[anim.Length + frames.Length];
		System.Array.Copy(frames, 0, newFrames, 0, frames.Length);
		System.Array.Copy(anim,   0, newFrames, frames.Length, anim.Length);
		frames = newFrames;
		if (framerate != 0f) length = (1f / framerate) * frames.Length;
	}

	// Source: Ghidra SetCurrentFrame.c RVA 0x01570498
	// 1-1: if (frames == null) NRE; clamp f to [-1, frames.Length+1]; curFrame = clamped.
	public void SetCurrentFrame(int f)
	{
		if (frames == null) throw new System.NullReferenceException();
		int clamped = f;
		int upperLimit = frames.Length + 1;
		if (clamped > upperLimit) clamped = upperLimit;
		if (clamped < -1)         clamped = -1;
		curFrame = clamped;
	}

	// Source: Ghidra SetPosition.c RVA 0x0157F3A0
	// 1-1: pos clamped to [0, 1].
	//   if (loopCycles < 1) curFrame = (int)(clampedPos * (frames.Length - 1));
	//   else {
	//       float loopSize = 1f / (loopCycles + 1);
	//       numLoops = (int)(clampedPos / loopSize);
	//       float residual = (clampedPos - loopSize * numLoops) / loopSize;
	//       if (!loopReverse) curFrame = (int)(residual * (frames.Length - 1));
	//       else if (residual < 0.5f) {
	//           curFrame = (int)(2*residual * (frames.Length - 1));
	//           stepDir  = 1;
	//       } else {
	//           curFrame = frames.Length - 1 - (int)(2*(residual - 0.5f) * (frames.Length - 1));
	//           stepDir  = -1;
	//       }
	//   }
	public void SetPosition(float pos)
	{
		float clampedPos = pos;
		if (clampedPos > 1f) clampedPos = 1f;
		if (clampedPos < 0f) clampedPos = 0f;
		if (loopCycles < 1)
		{
			if (frames == null) throw new System.NullReferenceException();
			curFrame = (int)(clampedPos * (frames.Length - 1));
			return;
		}
		float loopSize = 1f / (loopCycles + 1);
		numLoops = (int)(clampedPos / loopSize);
		float residual = (clampedPos - loopSize * numLoops) / loopSize;
		if (frames == null) throw new System.NullReferenceException();
		float lenMinus1 = frames.Length - 1;
		if (!loopReverse)
		{
			curFrame = (int)(residual * lenMinus1);
			return;
		}
		if (residual < 0.5f)
		{
			curFrame = (int)((residual + residual) * lenMinus1);
			stepDir  = 1;
			return;
		}
		curFrame = (frames.Length - 1) - (int)((residual - 0.5f) * 2f * lenMinus1);
		stepDir  = -1;
	}

	// Source: Ghidra SetClipPosition.c RVA 0x0157F558
	// 1-1: if (frames == null) NRE; curFrame = (int)((frames.Length - 1) * pos).
	public void SetClipPosition(float pos)
	{
		if (frames == null) throw new System.NullReferenceException();
		curFrame = (int)((frames.Length - 1) * pos);
	}

	// Source: Ghidra CalcLength.c RVA 0x0157ED68
	// 1-1: if (frames == null) NRE; length = (1/framerate) * frames.Length.
	protected void CalcLength()
	{
		if (frames == null) throw new System.NullReferenceException();
		length = (1f / framerate) * frames.Length;
	}

	// Source: Ghidra GetLength.c RVA 0x0157F5A0 — return length (offset 0x28).
	public float GetLength() { return length; }

	// Source: Ghidra GetDuration.c RVA 0x0157F5A8
	// 1-1: if (loopCycles < 0) return -1f.
	//      float v = length; if (loopReverse) v *= 2; return v + v * loopCycles = v * (1 + loopCycles).
	public float GetDuration()
	{
		if (loopCycles < 0) return -1f;
		float v = length;
		if (loopReverse) v += v;
		return v + v * loopCycles;
	}

	// Source: Ghidra GetFrameCount.c RVA 0x01578074 — return frames.Length (NRE if frames is null).
	public int GetFrameCount()
	{
		if (frames == null) throw new System.NullReferenceException();
		return frames.Length;
	}

	// Source: Ghidra GetFramesDisplayed.c RVA 0x0157F5DC
	// 1-1: if (loopCycles == -1) return -1; else {
	//          int n = frames.Length + loopCycles * frames.Length;
	//          n <<= (loopReverse ? 1 : 0);
	//          return n;
	//      }
	public int GetFramesDisplayed()
	{
		if (loopCycles == -1) return -1;
		if (frames == null) throw new System.NullReferenceException();
		int n = frames.Length + loopCycles * frames.Length;
		return n << (loopReverse ? 1 : 0);
	}

	// Source: Ghidra GetCurPosition.c RVA 0x0157F618 — return curFrame (offset 0x18).
	public int GetCurPosition() { return curFrame; }
}
