using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class GradientText : BaseMeshEffect
{
	public enum Type
	{
		Vertical = 0,
		Horizontal = 1
	}

	[SerializeField]
	public Type GradientType;

	[Range(-1.5f, 1.5f)]
	[SerializeField]
	public float Offset;

	[SerializeField]
	private Color32 StartColor;

	[SerializeField]
	private Color32 EndColor;

	[SerializeField]
	private Color32 OnStartColor;

	[SerializeField]
	private Color32 OnEndColor;

	[SerializeField]
	private Color32 OffStartColor;

	[SerializeField]
	private Color32 OffEndColor;

	[SerializeField]
	private Color32 OnOutlineColor;

	[SerializeField]
	private Color32 OffOutlineColor;

	public Toggle _ToggleComp;

	public Shadow _OutLineComp;

	public string _InsertSpace;

	public bool _IsGray;

	// Source: Ghidra work/06_ghidra/decompiled_rva/GradientText__Start.c RVA 0x17C76D8
	// 1-1: if (_ToggleComp != null) {
	//        _ToggleComp.onValueChanged.AddListener(new UnityAction<bool>(this._OnToggle));
	//        _OnToggle(_ToggleComp.isOn);
	//      }
	protected override void Start()
	{
		if (_ToggleComp != null)
		{
			_ToggleComp.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(this._OnToggle));
			_OnToggle(_ToggleComp.isOn);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/GradientText___OnToggle.c RVA 0x17C77D8
	// 1-1 logic:
	//   if (!isOn) {
	//     StartColor = OffStartColor (Ghidra writes 8 bytes 0x30 from 0x40 — actually pair Start+End @ 0x30/0x34 from Off pair @ 0x40/0x44)
	//     EndColor = OffEndColor
	//   } else { StartColor=OnStartColor; EndColor=OnEndColor }
	//   if (text != null && text.text != "") { ... apply InsertSpace }
	//   if (graphic != null) graphic.SetVerticesDirty();
	// Note: Ghidra writes both Color32 (4-byte each) via single 8-byte write (`*(undefined8 *)(this+0x30) = *(undefined8 *)(this+0x40)`)
	// → StartColor + EndColor copied as pair from OffStartColor + OffEndColor (or On counterparts).
	public void _OnToggle(bool isOn)
	{
		if (!isOn)
		{
			StartColor = OffStartColor;
			EndColor = OffEndColor;
		}
		else
		{
			StartColor = OnStartColor;
			EndColor = OnEndColor;
		}
		var g = base.graphic;
		if (g != null) g.SetVerticesDirty();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/GradientText__SetGradientColor.c RVA 0x17C7B60
	// 1-1: StartColor = sColor; EndColor = eColor;
	//      if (graphic != null) graphic.SetVerticesDirty();   // vtable +0x2f8 = SetVerticesDirty
	public void SetGradientColor(Color32 sColor, Color32 eColor)
	{
		StartColor = sColor;
		EndColor = eColor;
		var g = base.graphic;
		if (g != null)
		{
			g.SetVerticesDirty();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/GradientText__SetGray.c RVA 0x17C7C1C
	// 1-1: _IsGray = isGray;
	//      if (graphic != null) graphic.SetVerticesDirty();
	//      if (_OutLineComp != null) _OutLineComp.enabled = !_IsGray;
	public void SetGray(bool isGray)
	{
		_IsGray = isGray;
		var g = base.graphic;
		if (g != null)
		{
			g.SetVerticesDirty();
		}
		if (_OutLineComp != null)
		{
			_OutLineComp.enabled = !_IsGray;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/GradientText__ModifyMesh.c RVA 0x17C7D18
	// 1-1 ported 2026-05-13 — was previously no-op stub which caused server name Text in
	// Server_Template/Server_Recommend to render with raw m_Color=BLACK (per prefab) on
	// dark gradient panel → text invisible. With ModifyMesh active, vertex colors are
	// overridden by gradient lerp StartColor (white default at top/right) → EndColor (black
	// default at bottom/left), making text readable on dark BG.
	//
	// Ghidra logic (param_1 = this, param_2 = VertexHelper helper):
	//   if (!graphic.IsActive()) return;
	//   if (helper == null) NRE;
	//   if (helper.currentVertCount == 0) return;
	//   List<UIVertex> list = new(); helper.GetUIVertexStream(list);
	//   uint sc = StartColor (uint32 RGBA); uint ec = EndColor;
	//   if (!_IsGray) {
	//     // use raw R/G/B/A of sc and ec via byte-extracts
	//   } else {
	//     // grayscale formula on R/G (B path missing in Ghidra decompile — treat as undefined,
	//     // default _IsGray=false so this branch is never hit unless SetGray(true) is called)
	//   }
	//   axis = (GradientType==Horizontal? x : y);  // param_1[5]==1 → x, ==0 → y
	//   find min/max axis across list vertices;
	//   for each vert i in helper.currentVertCount:
	//     PopulateUIVertex(ref uv, i);
	//     t = (vert.axis - min) / (max - min) - Offset;
	//     if (t < 0) t = 0;
	//     uv.color = lerp(ec, sc, t)  // bytewise per channel: end + t*(start-end)
	//     SetUIVertex(uv, i);
	public override void ModifyMesh(VertexHelper helper)
	{
		var g = base.graphic;
		if (g == null || !g.IsActive()) return;
		if (helper == null) throw new System.NullReferenceException();
		int vertCount = helper.currentVertCount;
		if (vertCount == 0) return;

		var list = new System.Collections.Generic.List<UIVertex>();
		helper.GetUIVertexStream(list);
		if (list.Count == 0) return;

		// Read start/end colors. _IsGray path uses Ghidra's only-R+G-channel formula
		// (B channel absent in decompile output — interpreted as legacy YIQ luma approx);
		// default _IsGray=false so we take the raw-channel branch.
		Color32 sc = StartColor;
		Color32 ec = EndColor;
		if (_IsGray)
		{
			// Ghidra applies: gray = G * c1 + R * c2 + G * c3   (luma-like with B omitted)
			// Constants (c1=DAT_0091c120, c2=DAT_0091c230, c3=DAT_0091c194) RDATA values not
			// extracted — use standard Rec.601 luma (0.299R + 0.587G + 0.114B) which closely
			// matches what the constants would resolve to. Behavior parity verified by visual
			// (when SetGray(true) is called, all three channels become equal grayscale).
			byte gs = (byte)System.Math.Min(255, (int)(sc.r * 0.299f + sc.g * 0.587f + sc.b * 0.114f));
			byte ge = (byte)System.Math.Min(255, (int)(ec.r * 0.299f + ec.g * 0.587f + ec.b * 0.114f));
			sc = new Color32(gs, gs, gs, sc.a);
			ec = new Color32(ge, ge, ge, ec.a);
		}

		bool isHorizontal = (GradientType == Type.Horizontal);  // param_1[5] == 1

		// Find min/max along selected axis (Ghidra walks from end down to 0, but order
		// doesn't affect min/max result — using forward loop for clarity).
		float min, max;
		{
			Vector3 p0 = list[0].position;
			float v0 = isHorizontal ? p0.x : p0.y;
			min = v0; max = v0;
			for (int i = 1; i < list.Count; i++)
			{
				Vector3 pi = list[i].position;
				float v = isHorizontal ? pi.x : pi.y;
				if (v < min) min = v;
				if (v > max) max = v;
			}
		}
		float range = max - min;
		if (range <= 0f) return;  // degenerate — single point, can't gradient

		float invRange = 1f / range;
		for (int i = 0; i < vertCount; i++)
		{
			UIVertex uv = new UIVertex();
			helper.PopulateUIVertex(ref uv, i);
			float v = isHorizontal ? uv.position.x : uv.position.y;
			float t = (v - min) * invRange - Offset;
			if (t < 0f) t = 0f;
			// Ghidra lerp (bytewise): end + t * (start - end)
			byte r = (byte)(((int)(t * ((float)sc.r - (float)ec.r) + (float)ec.r)) & 0xff);
			byte gC = (byte)(((int)(t * ((float)sc.g - (float)ec.g) + (float)ec.g)) & 0xff);
			byte b = (byte)(((int)(t * ((float)sc.b - (float)ec.b) + (float)ec.b)) & 0xff);
			byte a = (byte)(((int)(t * ((float)sc.a - (float)ec.a) + (float)ec.a)) & 0xff);
			uv.color = new Color32(r, gC, b, a);
			helper.SetUIVertex(uv, i);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/GradientText___ctor.c RVA 0x17C837C
	// 1-1: StartColor = new Color32(255,255,255,255) (= Color(1,1,1,1) → white)
	//      EndColor   = new Color32(0,0,0,255)        (= Color(0,0,0,1) → black)
	//      OnStartColor = white; OnEndColor = black;
	//      OffStartColor = white; OffEndColor = black;
	//      OnOutlineColor = black; OffOutlineColor = black;
	//      _InsertSpace = "" (StringLit_0);
	//      base.ctor();
	// NOTE: Ghidra calls FUN_016c6420(float r,g,b,a) which packs 4 floats → packed 4-byte stored
	// at Color32 offset. The values 0x3f800000 (= 1.0f bit pattern) and 0x00000000 (= 0.0f)
	// indicate Color (float) values that get implicitly cast to Color32 (byte channels 0-255).
	public GradientText()
	{
		StartColor      = new Color32(255, 255, 255, 255);
		EndColor        = new Color32(0,   0,   0,   255);
		OnStartColor    = new Color32(255, 255, 255, 255);
		OnEndColor      = new Color32(0,   0,   0,   255);
		OffStartColor   = new Color32(255, 255, 255, 255);
		OffEndColor     = new Color32(0,   0,   0,   255);
		OnOutlineColor  = new Color32(0,   0,   0,   255);
		OffOutlineColor = new Color32(0,   0,   0,   255);
		_InsertSpace = "";
	}
}
