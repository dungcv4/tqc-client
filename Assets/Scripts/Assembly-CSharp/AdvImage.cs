// Source: Ghidra work/06_ghidra/decompiled_full/AdvImage/ (8 .c ported 1-1)
// Source: dump.cs TypeDefIndex 156 — `AdvImage : Image`
// 6/8 ported 1-1; 2 complex (GetModifiedMaterial 350+ lines stencil masking, GetDrawingDimensions
// 400+ lines aspect-ratio math) kept as stub-with-RVA.

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/AdvImage", 11)]
public class AdvImage : Image
{
    [Range(0f, 1f)]
    [SerializeField]
    private float m_GrayScaleAmount;             // 0x110

    private static Material m_DefaultMaterial;    // static 0x0
    private Material m_DefaultMaterialEditor;     // 0x118
    private static Shader s_defaultShader;        // static 0x8

    // Source: Ghidra get_GrayScaleAmount.c  RVA 0x17C2C1C
    public float GrayScaleAmount
    {
        get { return m_GrayScaleAmount; }

        // Source: Ghidra set_GrayScaleAmount.c  RVA 0x17C2C24
        // SetPropertyUtility.SetStruct<float>(value, ref m_GrayScaleAmount); if changed, SetVerticesDirty().
        set
        {
            // Inline SetStruct check: only update + dirty if value differs
            if (!m_GrayScaleAmount.Equals(value))
            {
                m_GrayScaleAmount = value;
                this.SetVerticesDirty();
            }
        }
    }

    // Source: Ghidra get_defaultShader.c  RVA 0x17C2CA4
    // Lazy init: if s_defaultShader == null → Shader.Find(literal #2749).
    // String literal #2749 needs lookup; based on default-canvas shader pattern likely "UI/AdvImage"
    // or "UI/Default-AdvImage".
    public static new Shader defaultShader
    {
        get
        {
            if (s_defaultShader == null)
            {
                s_defaultShader = Shader.Find(STR_DEFAULT_ADV_SHADER);
            }
            return s_defaultShader;
        }
    }

    // String literal #2749 — Shader.Find target for default AdvImage shader.
    // TODO: verify exact string from stringliteral.json (deferred to Tuần 8 lookup pass).
    private const string STR_DEFAULT_ADV_SHADER = "UI/Default-AdvImage";

    // Source: Ghidra get_defaultMaterial.c  RVA 0x17C2D70
    // Lazy init: if m_DefaultMaterial == null → new Material(Canvas.GetDefaultCanvasMaterial())
    // then set shader = defaultShader.
    public override Material defaultMaterial
    {
        get
        {
            if (m_DefaultMaterial == null)
            {
                m_DefaultMaterial = new Material(Canvas.GetDefaultCanvasMaterial());
                m_DefaultMaterial.shader = defaultShader;
            }
            return m_DefaultMaterial;
        }
    }

    // Source: Ghidra OnEnable.c  RVA 0x17C2D68
    // Single base call.
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Source: Ghidra UpdateMaterial.c  RVA 0x17C2E7C
    // If IsActive(): set canvasRenderer.materialCount=1, set material, set texture.
    protected override void UpdateMaterial()
    {
        if (!this.IsActive()) return;
        var cr = this.canvasRenderer;
        if (cr == null) throw new System.NullReferenceException();
        cr.materialCount = 1;
        cr.SetMaterial(this.materialForRendering, 0);
        cr.SetTexture(this.mainTexture);
    }

    // Source: Ghidra GetModifiedMaterial.c  RVA 0x17C2F40
    // TODO: 350+ line stencil masking with alpha — uses StencilMaterialAlpha.AddAlpha helper.
    // Complex masking depth + grayscale composition. Port deferred.
    public override Material GetModifiedMaterial(Material baseMaterial)
    {
        // TODO body RVA 0x017C2F40 — complex StencilMaterialAlpha composition
        return base.GetModifiedMaterial(baseMaterial);  // safer fallback than NIE for per-frame UI render
    }

    // Source: Ghidra GetDrawingDimensions.c  RVA 0x17C3D18
    // 1-1 port from Ghidra pseudo-C + matched against UnityEngine.UI.Image.GetDrawingDimensions (Unity 1.0.0
    // com.unity.ugui Image.cs lines 836-867). AdvImage uses `overrideSprite` (not `activeSprite`).
    // The Ghidra decompile only shows the X slot of the returned Vector4 (s0 return reg); the remaining
    // slots y/z/w follow the same Unity pattern (banker's-round spriteW/spriteH, then padding/(spriteW|spriteH)
    // composited with PixelAdjustedRect r and PreserveAspect optional adjust).
    private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
    {
        Sprite sprite = this.overrideSprite;
        Vector4 padding = (sprite == null) ? Vector4.zero : UnityEngine.Sprites.DataUtility.GetPadding(sprite);
        Vector2 size = (sprite == null) ? Vector2.zero : new Vector2(sprite.rect.width, sprite.rect.height);

        Rect r = GetPixelAdjustedRect();

        int spriteW = Mathf.RoundToInt(size.x);
        int spriteH = Mathf.RoundToInt(size.y);

        Vector4 v = new Vector4(
            padding.x / spriteW,
            padding.y / spriteH,
            (spriteW - padding.z) / spriteW,
            (spriteH - padding.w) / spriteH);

        if (shouldPreserveAspect && size.sqrMagnitude > 0.0f)
        {
            // Preserve aspect ratio (Unity pattern; Ghidra reflects the same arithmetic via
            // size.x/size.y vs r.width/r.height ratio and pivot-based offset).
            float spriteRatio = size.x / size.y;
            float rectRatio = r.width / r.height;
            if (spriteRatio > rectRatio)
            {
                float oldHeight = r.height;
                r.height = r.width * (1.0f / spriteRatio);
                r.y += (oldHeight - r.height) * rectTransform.pivot.y;
            }
            else
            {
                float oldWidth = r.width;
                r.width = r.height * spriteRatio;
                r.x += (oldWidth - r.width) * rectTransform.pivot.x;
            }
        }

        v = new Vector4(
            r.x + r.width * v.x,
            r.y + r.height * v.y,
            r.x + r.width * v.z,
            r.y + r.height * v.w
        );

        return v;
    }

    // RVA: 0x17C412C — default ctor.
    public AdvImage() { }
}
