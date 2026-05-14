// Source: Ghidra work/06_ghidra/decompiled_full/TextureLoadHandler/ (Load + OnTextureLoaded + CancelRequest)
// Fields: _img@0x10 (RawImage), _onFinished@0x18 (delegate), _cancel@0x20 (bool)

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
		_img = image;
	}

	public TextureLoadHandler(RawImage image, OnFinished callback)
	{
		_img = image;
		_onFinished = callback;
	}

	// Source: Ghidra OnTextureLoaded.c RVA 0x17c28b0
	// 1-1: if texs != null && texs[0] != null && !_cancel && _img != null:
	//      _img.texture = (Texture)tex; _img.enabled = true; invoke _onFinished(_img).
	public void OnTextureLoaded(Object[] texs)
	{
		if (texs == null || texs.Length == 0) return;
		Object obj = texs[0];
		if (obj == null) return;
		if (_cancel) return;
		if (_img == null) return;
		Texture tex = obj as Texture;
		_img.texture = tex;
		_img.enabled = true;
		if (_onFinished != null) _onFinished(_img);
	}

	// Source: Ghidra Load.c RVA 0x17c2a10
	public void Load(ResourcesLoader.AssetType type, string texName)
	{
		CBNewObjectLoader loader = OnTextureLoaded;
		ResourcesLoader.GetObjectTypeAssetDynamic(type, texName, loader);
	}

	// Source: Ghidra CancelRequest.c RVA 0x17c2ac8 — _cancel = true.
	public void CancelRequest() { _cancel = true; }
}
