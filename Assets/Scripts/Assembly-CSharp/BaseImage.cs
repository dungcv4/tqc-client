// Source: Ghidra work/06_ghidra/decompiled_full/BaseImage/ — custom MaskableGraphic with sprite +
// grayscale support. Simplified port covering the essential rendering path (Simple mode).

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
		get { return m_Sprite; }
		set
		{
			if (m_Sprite != value)
			{
				m_Sprite = value;
				SetAllDirty();
			}
		}
	}

	public Sprite overrideSprite
	{
		get { return m_OverrideSprite != null ? m_OverrideSprite : m_Sprite; }
		set
		{
			if (m_OverrideSprite != value)
			{
				m_OverrideSprite = value;
				SetAllDirty();
			}
		}
	}

	public float GrayScaleAmount
	{
		get { return m_GrayScaleAmount; }
		set { m_GrayScaleAmount = value; SetMaterialDirty(); }
	}

	public override Texture mainTexture
	{
		get
		{
			Sprite s = overrideSprite;
			if (s != null && s.texture != null) return s.texture;
			if (material != null && material.mainTexture != null) return material.mainTexture;
			return s_WhiteTexture;
		}
	}

	public float pixelsPerUnit
	{
		get
		{
			Sprite s = overrideSprite;
			float spritePpu = (s != null) ? s.pixelsPerUnit : 100f;
			float referencePpu = 100f;
			Canvas c = canvas;
			if (c != null) referencePpu = c.referencePixelsPerUnit;
			return spritePpu / referencePpu;
		}
	}

	public virtual float minWidth { get { return 0f; } }
	public virtual float preferredWidth
	{
		get
		{
			Sprite s = overrideSprite;
			if (s == null) return 0f;
			return s.rect.size.x / pixelsPerUnit;
		}
	}
	public virtual float flexibleWidth { get { return -1f; } }
	public virtual float minHeight { get { return 0f; } }
	public virtual float preferredHeight
	{
		get
		{
			Sprite s = overrideSprite;
			if (s == null) return 0f;
			return s.rect.size.y / pixelsPerUnit;
		}
	}
	public virtual float flexibleHeight { get { return -1f; } }
	public virtual int layoutPriority { get { return 0; } }

	public static Shader defaultShader
	{
		get
		{
			if (s_defaultShader == null) s_defaultShader = Shader.Find("UI/Default");
			return s_defaultShader;
		}
	}

	public override Material defaultMaterial
	{
		get
		{
			if (m_DefaultMaterial == null && defaultShader != null) m_DefaultMaterial = new Material(defaultShader);
			return m_DefaultMaterial != null ? m_DefaultMaterial : base.defaultMaterial;
		}
	}

	private new void Start() { }

	private void Update() { }

	// Source: Ghidra OnPopulateMesh.c — simple quad mode with sprite UV.
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		Vector4 v = GetDrawingDimensions(false);
		Vector4 uv = (overrideSprite != null) ? UnityEngine.Sprites.DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;
		Color32 c = color;
		vh.Clear();
		vh.AddVert(new Vector3(v.x, v.y), c, new Vector2(uv.x, uv.y));
		vh.AddVert(new Vector3(v.x, v.w), c, new Vector2(uv.x, uv.w));
		vh.AddVert(new Vector3(v.z, v.w), c, new Vector2(uv.z, uv.w));
		vh.AddVert(new Vector3(v.z, v.y), c, new Vector2(uv.z, uv.y));
		vh.AddTriangle(0, 1, 2);
		vh.AddTriangle(2, 3, 0);
	}

	public void OnAfterDeserialize()
	{
		if (m_GrayScaleAmount < 0f) m_GrayScaleAmount = 0f;
		if (m_GrayScaleAmount > 1f) m_GrayScaleAmount = 1f;
	}

	public void OnBeforeSerialize() { }

	public virtual void CalculateLayoutInputHorizontal() { }
	public virtual void CalculateLayoutInputVertical() { }

	public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{
		Sprite s = overrideSprite;
		if (s == null) return true;
		Vector2 local;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out local)) return false;
		return rectTransform.rect.Contains(local);
	}

	protected override void UpdateMaterial()
	{
		base.UpdateMaterial();
	}

	public override Material GetModifiedMaterial(Material baseMaterial)
	{
		return baseMaterial;
	}

	// Source: Ghidra GetDrawingDimensions.c — returns rect bounds (x,y,z,w) = (min.x,min.y,max.x,max.y).
	private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
	{
		Rect r = rectTransform.rect;
		return new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
	}

	public BaseImage() { }
}
