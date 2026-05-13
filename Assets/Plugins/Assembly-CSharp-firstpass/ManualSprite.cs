using Cpp2IlInjected;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SpriteManager 2/Manual Sprite")]
public class ManualSprite : SpriteBase
{
	public Vector2 lowerLeftPixel;

	public Vector2 pixelDimensions;

	public UVAnimation_Multi[] animations;

	protected UVAnimation_Multi curAnim;

	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void Init()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void Clear()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Setup(float w, float h, Vector2 lowerleftPixel, Vector2 pixeldimensions)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Setup(float w, float h, Vector2 lowerleftPixel, Vector2 pixeldimensions, Material material)
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

	public void AddAnimation(UVAnimation_Multi anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override bool StepAnim(float time)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void CallAnimCompleteDelegate()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnim(UVAnimation_Multi anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PlayAnimInReverse(UVAnimation_Multi anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnimInReverse(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void PlayAnimInReverse(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void DoAnim(UVAnimation_Multi anim)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void StopAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void UnpauseAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void AddToAnimatedList()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected override void RemoveFromAnimatedList()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Multi GetCurAnim()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public UVAnimation_Multi GetAnim(string name)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override int GetStateIndex(string stateName)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void SetState(int index)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetLowerLeftPixel(Vector2 lowerLeft)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetLowerLeftPixel(int x, int y)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPixelDimensions(Vector2 size)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPixelDimensions(int x, int y)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public override void DoMirror()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static ManualSprite Create(string name, Vector3 pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static ManualSprite Create(string name, Vector3 pos, Quaternion rotation)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public ManualSprite()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
