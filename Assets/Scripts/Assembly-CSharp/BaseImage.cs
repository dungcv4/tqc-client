using System;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaseImage : MaskableGraphic, ISerializationCallbackReceiver, ILayoutElement, ICanvasRaycastFilter
{
	[SerializeField]
	[FormerlySerializedAs("m_Frame")]
	private Sprite m_Sprite;

	[NonSerialized]
	private Sprite m_OverrideSprite;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_GrayScaleAmount;

	private static Material m_DefaultMaterial;

	private Material m_DefaultMaterialEditor;

	private static Shader s_defaultShader;

	public Sprite sprite
	{
		get
		{ return default; }
		set
		{ }
	}

	public Sprite overrideSprite
	{
		get
		{ return default; }
		set
		{ }
	}

	public float GrayScaleAmount
	{
		get
		{ return default; }
		set
		{ }
	}

	public override Texture mainTexture
	{
		get
		{ return default; }
	}

	public float pixelsPerUnit
	{
		get
		{ return default; }
	}

	public virtual float minWidth
	{
		get
		{ return default; }
	}

	public virtual float preferredWidth
	{
		get
		{ return default; }
	}

	public virtual float flexibleWidth
	{
		get
		{ return default; }
	}

	public virtual float minHeight
	{
		get
		{ return default; }
	}

	public virtual float preferredHeight
	{
		get
		{ return default; }
	}

	public virtual float flexibleHeight
	{
		get
		{ return default; }
	}

	public virtual int layoutPriority
	{
		get
		{ return default; }
	}

	public static Shader defaultShader
	{
		get
		{ return default; }
	}

	public override Material defaultMaterial
	{
		get
		{ return default; }
	}

	private new void Start()
	{ }

	private void Update()
	{ }

	protected override void OnPopulateMesh(VertexHelper vh)
	{ }

	public void OnAfterDeserialize()
	{ }

	public void OnBeforeSerialize()
	{ }

	public virtual void CalculateLayoutInputHorizontal()
	{ }

	public virtual void CalculateLayoutInputVertical()
	{ }

	public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{ return default; }

	protected override void UpdateMaterial()
	{ }

	public override Material GetModifiedMaterial(Material baseMaterial)
	{ return default; }

	private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
	{ return default; }

	// Source: Ghidra work/06_ghidra/decompiled_rva/BaseImage___ctor.c RVA 0x017C5114
	// 1-1: just base.ctor — no field init.
	public BaseImage()
	{
	}
}
