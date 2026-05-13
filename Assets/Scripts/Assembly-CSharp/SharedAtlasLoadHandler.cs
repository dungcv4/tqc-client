using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class SharedAtlasLoadHandler
{
	public delegate void OnFinished(Sprite sprite);

	private Image _img;

	private OnFinished _onFinished;

	public SharedAtlasLoadHandler(OnFinished callback)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SharedAtlasLoadHandler(Image image, OnFinished callback)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void LoadSprite(string spriteName)
	{ }

	public void OnSharedAtlasLoaded(Object[] sprites)
	{ }
}
