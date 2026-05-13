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
	{ }

	private void SetMeshAndMaterial()
	{ }

	protected override void OnPopulateMesh(VertexHelper vh)
	{ }

	public override void Rebuild(CanvasUpdate update)
	{ }

	protected override void OnDisable()
	{ }

	public CanvasMesh()
	{ }
}
