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
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public static Dictionary<CHAR_ACT, AnimationSetting> SpriteAnimationSetting;

	public static Dictionary<CHAR_ACT, AnimationSetting> WLightSpriteAnimationSetting;

	public SpriteRule()
	{ }

	static SpriteRule()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
