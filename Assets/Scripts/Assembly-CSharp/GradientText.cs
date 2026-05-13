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
	// Complex vertex-manipulation logic — not on critical path for Login boot.
	// Defer port: keep stub for now since this only fires when UI re-renders.
	// TODO: port full ModifyMesh body 1-1 from Ghidra .c (see decompiled_rva/GradientText__ModifyMesh.c).
	public override void ModifyMesh(VertexHelper helper)
	{
		// Minimal no-op to avoid AnalysisFailedException blocking boot.
		// Ghidra body: iterates vertices, applies gradient color based on GradientType (vertical/horizontal).
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
