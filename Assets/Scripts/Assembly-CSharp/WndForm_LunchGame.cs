// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/ (9 .c — all methods 1-1)
// Source: dump.cs TypeDefIndex 796 — WndForm_LunchGame : WndForm
// Field offsets (instance): _logoGobj@0x60, _logo_CNGobj@0x68, _warnGobj@0x70,
//   _warnGobj_China@0x78, _loadingBar@0x80. Static s_instance at type+0xb8.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WndForm_LunchGame : WndForm
{
	private static WndForm_LunchGame s_instance;

	private GameObject _logoGobj;            // 0x60
	private GameObject _logo_CNGobj;         // 0x68
	private GameObject _warnGobj;            // 0x70
	private GameObject _warnGobj_China;      // 0x78
	private Slider _loadingBar;              // 0x80

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/get_Instance.c
	// RVA: 0x18F995C — Body returns static s_instance (type+0xb8).
	public static WndForm_LunchGame Instance => s_instance;

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/get_LoadingBar.c
	// RVA: 0x18F99A4 — Body: return *(this+0x80) i.e. the _loadingBar field.
	public Slider LoadingBar => _loadingBar;

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/IsPrefabInResource.c
	// RVA: 0x18F99AC — Body: return 1 (true).
	public override bool IsPrefabInResource()
	{
		return true;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/V_Create.c
	// RVA: 0x18F99B4 — Body: s_instance = this; return 1 (true).
	// Note: Ghidra writes `**(undefined8 **)(*(long *)puVar1 + 0xb8) = param_1;`
	//   = "WndForm_LunchGame.s_instance = this" — the singleton publish point.
	protected override bool V_Create(ArrayList args)
	{
		s_instance = this;
		return true;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/V_Update.c
	// RVA: 0x18F9A14 — Body empty (no-op). Override blocks base V_Update tick.
	protected override void V_Update(float dTime)
	{
		// 1-1 empty body per Ghidra.
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/showLOGO.c
	// RVA: 0x18F9A18 — Body: if (_logoGobj != null) _logoGobj.SetActive(enable); else NRE.
	public void showLOGO(bool enable)
	{
		if (_logoGobj == null) throw new System.NullReferenceException();
		_logoGobj.SetActive(enable);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/showLOGO_CN.c
	// RVA: 0x18F9A38 — Symmetric to showLOGO but for _logo_CNGobj.
	public void showLOGO_CN(bool enable)
	{
		if (_logo_CNGobj == null) throw new System.NullReferenceException();
		_logo_CNGobj.SetActive(enable);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/showWARN.c
	// RVA: 0x18F9A58 — Body empty (no-op). showWARN does NOT touch _warnGobj
	//   in the binary; the field exists but is unused by this method (dump-only).
	public void showWARN(bool enable)
	{
		// 1-1 empty body per Ghidra.
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/WndForm_LunchGame/.ctor.c
	// RVA: 0x18F9A5C — Body: WndForm.__ctor(this, ticked=1) — only base() call.
	public WndForm_LunchGame() : base(true)
	{
	}
}
