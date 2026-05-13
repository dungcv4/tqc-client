using Cpp2IlInjected;
using UnityEngine;

public class MaterialPropertyModifier : MonoBehaviour
{
	public enum PropertyType
	{
		Float = 0,
		Vector = 1,
		Color = 2,
		Gradient = 3
	}

	[Header("Property")]
	public string propertyName;

	public PropertyType propertyType;

	[Header("Time")]
	public float delay;

	public float duration;

	public new AnimationCurve enabled;

	[Header("Hide Renderer")]
	public bool hideBeforeStarted;

	public bool hideAfterFinished;

	[Header("Value")]
	public AnimationCurve curve;

	public float fromFloat;

	public float toFloat;

	public Vector4 fromVector;

	public Vector4 toVector;

	public Color fromColor;

	public Color toColor;

	public bool hdrColor;

	public Gradient gradient;

	[Header("Multiplier")]
	public float valueMultiplier;

	public bool separateAlphaMultiplier;

	public float alphaValueMultiplier;

	[Header("如果要使用兩個以上一樣屬性的修改器請打勾：")]
	public bool discardBeforeAndAfter;

	public bool clearPropertyBlockOnDisable;

	private float startTime;

	private Renderer ren;

	private MaterialPropertyBlock propertyBlock;

	private int nameID;

	private float CurrentTime
	{
		get
		{ return default; }
	}

	private static AnimationCurve GetStepCurve()
	{ return default; }

	private static Keyframe GetEnabledKey(float time, bool val)
	{ return default; }

	private Color Multiply(Color color, float val)
	{ return default; }

	private void OnEnable()
	{ }

	private void OnDisable()
	{ }

	private void ClearPropertyBlock()
	{ }

	private void Update()
	{ }

	public MaterialPropertyModifier()
	{ }
}
