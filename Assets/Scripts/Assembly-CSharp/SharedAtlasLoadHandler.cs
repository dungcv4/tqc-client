// Source: Ghidra work/06_ghidra/decompiled_full/SharedAtlasLoadHandler/ (LoadSprite + OnSharedAtlasLoaded)
// Fields: _img@0x10 (Image), _onFinished@0x18 (delegate)
// LoadSprite: create CBNewObjectLoader bound to OnSharedAtlasLoaded; call ResourcesLoader.GetObjectTypeAssetDynamic(UIATLAS_SHARED=1, spriteName, loader);
//             log warning if loader path has errors.
// OnSharedAtlasLoaded: pick sprites[0]; if _img != null → _img.sprite = sprite; invoke _onFinished(sprite).

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
		_onFinished = callback;
	}

	public SharedAtlasLoadHandler(Image image, OnFinished callback)
	{
		_img = image;
		_onFinished = callback;
	}

	// Source: Ghidra LoadSprite.c RVA 0x17bfea4
	public void LoadSprite(string spriteName)
	{
		// CBNewObjectLoader delegate bound to OnSharedAtlasLoaded → invoked when load completes.
		CBNewObjectLoader loader = OnSharedAtlasLoaded;
		var op = ResourcesLoader.GetObjectTypeAssetDynamic(
			ResourcesLoader.AssetType.UIATLASES_SHARED,
			spriteName,
			loader);
		if (op == null) throw new System.NullReferenceException();
		if (!string.IsNullOrEmpty(op.error))
		{
			UJDebug.LogWarning(string.Format("SharedAtlasLoadHandler.LoadSprite error '{0}' for '{1}'", op.error, spriteName));
		}
	}

	// Source: Ghidra OnSharedAtlasLoaded.c RVA 0x17c00b8
	// 1-1: if sprites != null && sprites[0] != null:
	//      cast to Sprite → if _img != null set _img.sprite = sprite; invoke _onFinished(sprite).
	public void OnSharedAtlasLoaded(Object[] sprites)
	{
		if (sprites == null || sprites.Length == 0) return;
		Object obj = sprites[0];
		if (obj == null) return;
		Sprite sp = obj as Sprite;
		if (_img != null) _img.sprite = sp;
		if (_onFinished != null) _onFinished(sp);
	}
}
