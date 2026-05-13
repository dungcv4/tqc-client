using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class TextureLoadHandler
{
	public delegate void OnFinished(RawImage __img);

	private RawImage _img;

	private OnFinished _onFinished;

	private bool _cancel;

	public TextureLoadHandler(RawImage image)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public TextureLoadHandler(RawImage image, OnFinished callback)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void OnTextureLoaded(Object[] texs)
	{ }

	public void Load(ResourcesLoader.AssetType type, string texName)
	{ }

	public void CancelRequest()
	{ }
}
