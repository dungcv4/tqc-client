// Source: Ghidra work/06_ghidra/decompiled_full/CanvasMesh/ — render a Mesh via CanvasRenderer.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[ExecuteInEditMode]
public class CanvasMesh : Graphic
{
	public Mesh mesh;
	private CanvasRenderer ren;

	protected override void OnEnable()
	{
		base.OnEnable();
		ren = GetComponent<CanvasRenderer>();
		SetMeshAndMaterial();
	}

	private void SetMeshAndMaterial()
	{
		if (ren == null) return;
		if (mesh != null) ren.SetMesh(mesh);
		ren.SetMaterial(material, mainTexture);
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	public override void Rebuild(CanvasUpdate update)
	{
		if (update == CanvasUpdate.PreRender) SetMeshAndMaterial();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (ren != null) ren.Clear();
	}

	public CanvasMesh() { }
}
