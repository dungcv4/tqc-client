// Source: Ghidra work/06_ghidra/decompiled_full/MaterialPropertyModifier/ — animates a renderer material
// property (Float/Vector/Color/Gradient) over time using AnimationCurve / Gradient.

using Cpp2IlInjected;
using UnityEngine;

public class MaterialPropertyModifier : MonoBehaviour
{
	public enum PropertyType { Float = 0, Vector = 1, Color = 2, Gradient = 3 }

	[Header("Property")] public string propertyName;
	public PropertyType propertyType;

	[Header("Time")] public float delay;
	public float duration;
	public new AnimationCurve enabled;

	[Header("Hide Renderer")] public bool hideBeforeStarted;
	public bool hideAfterFinished;

	[Header("Value")] public AnimationCurve curve;
	public float fromFloat;
	public float toFloat;
	public Vector4 fromVector;
	public Vector4 toVector;
	public Color fromColor;
	public Color toColor;
	public bool hdrColor;
	public Gradient gradient;

	[Header("Multiplier")] public float valueMultiplier;
	public bool separateAlphaMultiplier;
	public float alphaValueMultiplier;

	[Header("Discard")] public bool discardBeforeAndAfter;
	public bool clearPropertyBlockOnDisable;

	private float startTime;
	private Renderer ren;
	private MaterialPropertyBlock propertyBlock;
	private int nameID;

	private float CurrentTime { get { return Time.time - startTime - delay; } }

	private static AnimationCurve GetStepCurve()
	{
		return new AnimationCurve(GetEnabledKey(0, false), GetEnabledKey(0.000001f, true));
	}

	private static Keyframe GetEnabledKey(float time, bool val)
	{
		return new Keyframe(time, val ? 1f : 0f);
	}

	private Color Multiply(Color color, float val)
	{
		Color c = color * val;
		if (separateAlphaMultiplier) c.a = color.a * alphaValueMultiplier;
		return c;
	}

	private void OnEnable()
	{
		startTime = Time.time;
		ren = GetComponent<Renderer>();
		if (propertyBlock == null) propertyBlock = new MaterialPropertyBlock();
		nameID = Shader.PropertyToID(propertyName);
	}

	private void OnDisable()
	{
		if (clearPropertyBlockOnDisable) ClearPropertyBlock();
	}

	private void ClearPropertyBlock()
	{
		if (ren != null && propertyBlock != null)
		{
			ren.GetPropertyBlock(propertyBlock);
			propertyBlock.Clear();
			ren.SetPropertyBlock(propertyBlock);
		}
	}

	private void Update()
	{
		if (ren == null || propertyBlock == null) return;
		float t = CurrentTime;
		bool active = (t >= 0f && (duration <= 0f || t <= duration));
		ren.enabled = active || !(hideBeforeStarted && t < 0) && !(hideAfterFinished && t > duration);
		if (!active && discardBeforeAndAfter) return;

		float lerp = (duration > 0f) ? Mathf.Clamp01(t / duration) : 1f;
		if (curve != null && curve.length > 0) lerp = curve.Evaluate(lerp);

		ren.GetPropertyBlock(propertyBlock);
		switch (propertyType)
		{
			case PropertyType.Float:
				propertyBlock.SetFloat(nameID, Mathf.LerpUnclamped(fromFloat, toFloat, lerp) * valueMultiplier);
				break;
			case PropertyType.Vector:
				propertyBlock.SetVector(nameID, Vector4.LerpUnclamped(fromVector, toVector, lerp) * valueMultiplier);
				break;
			case PropertyType.Color:
				propertyBlock.SetColor(nameID, Multiply(Color.LerpUnclamped(fromColor, toColor, lerp), valueMultiplier));
				break;
			case PropertyType.Gradient:
				if (gradient != null) propertyBlock.SetColor(nameID, Multiply(gradient.Evaluate(lerp), valueMultiplier));
				break;
		}
		ren.SetPropertyBlock(propertyBlock);
	}

	public MaterialPropertyModifier() { valueMultiplier = 1f; alphaValueMultiplier = 1f; }
}
