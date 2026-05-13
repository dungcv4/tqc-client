using Cpp2IlInjected;
using UnityEngine;

public class SpriteMirror : SpriteRootMirror
{
	public Vector2 lowerLeftPixel;

	public Vector2 pixelDimensions;

	public override void Mirror(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override bool DidChange(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SpriteMirror()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
