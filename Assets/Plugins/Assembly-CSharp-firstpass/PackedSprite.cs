using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("SpriteManager 2/PackedSprite")]
public class PackedSprite : AutoSpriteBase
{
	[HideInInspector]
	public string staticTexPath;

	[HideInInspector]
	public string staticTexGUID;

	[HideInInspector]
	public CSpriteFrame _ser_stat_frame_info;

	[HideInInspector]
	public SPRITE_FRAME staticFrameInfo;

	public TextureAnim[] textureAnimations;

	public override TextureAnim[] States
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public override CSpriteFrame DefaultFrame
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public override TextureAnim DefaultState
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public override bool SupportsArbitraryAnimations
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void Init()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Copy(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void InitUVs()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void AddAnimation(UVAnimation anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static PackedSprite Create(string name, Vector3 pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static PackedSprite Create(string name, Vector3 pos, Quaternion rotation)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public PackedSprite()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
