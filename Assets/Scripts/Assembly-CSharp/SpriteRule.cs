// Source: dump.cs — SpriteRule + AnimationSetting.
// AnimationSetting.ctor stores fields. cctor (RVA 0x18E69A4) initializes static dicts.

using System.Collections.Generic;
using Cpp2IlInjected;

public class SpriteRule
{
	public class AnimationSetting
	{
		public int actionIndex;
		public float FPS;
		public int loopCycle;
		public bool loopReverse;
		public UVAnimation.ANIM_END_ACTION endAction;

		public AnimationSetting(int _actionIndex, float _fps = 10f, int _loopCycle = 0, bool _loopReverse = false, UVAnimation.ANIM_END_ACTION _endAction = UVAnimation.ANIM_END_ACTION.Play_Default_Anim)
		{
			actionIndex = _actionIndex;
			FPS = _fps;
			loopCycle = _loopCycle;
			loopReverse = _loopReverse;
			endAction = _endAction;
		}
	}

	public static Dictionary<CHAR_ACT, AnimationSetting> SpriteAnimationSetting;
	public static Dictionary<CHAR_ACT, AnimationSetting> WLightSpriteAnimationSetting;

	public SpriteRule() { }

	// Source: Ghidra work/06_ghidra/decompiled_full/SpriteRule/.cctor.c (RVA 0x18e69a4)
	// Populates 9 entries per dict, keys 0-8 = CHAR_ACT STAND..ATTACK3.
	// STAND/RUN: loopCycle=-1 (infinite), endAction=Do_Nothing. STAND also loopReverse=true.
	// Other actions: loopCycle=0, endAction=Play_Default_Anim (main dict) or Hide (WLight).
	// All FPS=10.0.
	static SpriteRule()
	{
		var Do_Nothing = UVAnimation.ANIM_END_ACTION.Do_Nothing;
		var Play_Default_Anim = UVAnimation.ANIM_END_ACTION.Play_Default_Anim;
		var Hide = UVAnimation.ANIM_END_ACTION.Hide;

		SpriteAnimationSetting = new Dictionary<CHAR_ACT, AnimationSetting>
		{
			{ (CHAR_ACT)0, new AnimationSetting(0, 10f, -1, true,  Do_Nothing) },         // STAND
			{ (CHAR_ACT)1, new AnimationSetting(1, 10f, -1, false, Do_Nothing) },         // RUN
			{ (CHAR_ACT)2, new AnimationSetting(2, 10f,  0, false, Play_Default_Anim) },  // ATTACK
			{ (CHAR_ACT)3, new AnimationSetting(3, 10f,  0, false, Play_Default_Anim) },  // DAMAGE
			{ (CHAR_ACT)4, new AnimationSetting(4, 10f,  0, false, Do_Nothing) },         // DEAD
			{ (CHAR_ACT)5, new AnimationSetting(5, 10f,  0, false, Play_Default_Anim) },  // SKILL1
			{ (CHAR_ACT)6, new AnimationSetting(6, 10f,  0, false, Play_Default_Anim) },  // SKILL2
			{ (CHAR_ACT)7, new AnimationSetting(7, 10f,  0, false, Play_Default_Anim) },  // ATTACK2
			{ (CHAR_ACT)8, new AnimationSetting(8, 10f,  0, false, Play_Default_Anim) },  // ATTACK3
		};

		WLightSpriteAnimationSetting = new Dictionary<CHAR_ACT, AnimationSetting>
		{
			{ (CHAR_ACT)0, new AnimationSetting(0, 10f, -1, true,  Do_Nothing) },
			{ (CHAR_ACT)1, new AnimationSetting(1, 10f, -1, false, Do_Nothing) },
			{ (CHAR_ACT)2, new AnimationSetting(2, 10f,  0, false, Hide) },
			{ (CHAR_ACT)3, new AnimationSetting(3, 10f,  0, false, Hide) },
			{ (CHAR_ACT)4, new AnimationSetting(4, 10f,  0, false, Do_Nothing) },
			{ (CHAR_ACT)5, new AnimationSetting(5, 10f,  0, false, Hide) },
			{ (CHAR_ACT)6, new AnimationSetting(6, 10f,  0, false, Hide) },
			{ (CHAR_ACT)7, new AnimationSetting(7, 10f,  0, false, Hide) },
			{ (CHAR_ACT)8, new AnimationSetting(8, 10f,  0, false, Hide) },
		};
	}
}
