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

	// Source: dump.cs — static initializer; allocates empty dicts. Entries populated externally
	// (e.g., from design tables or by ResMgr).
	static SpriteRule()
	{
		SpriteAnimationSetting = new Dictionary<CHAR_ACT, AnimationSetting>();
		WLightSpriteAnimationSetting = new Dictionary<CHAR_ACT, AnimationSetting>();
	}
}
