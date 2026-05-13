using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Circle Image")]
public class CircleImage : BaseImage
{
	[Tooltip("\ufffd\ufffdΩή\ufffd\ufffdζ\ufffdR\ufffd\ufffd\ufffd")]
	[Range(0f, 1f)]
	public float fillPercent;

	[Tooltip("\ufffdO\ufffd_\ufffd\ufffdR\ufffd\ufffd\ufffd")]
	public bool fill;

	[Tooltip("\ufffd\ufffd\ufffd\ufffd\ufffde\ufffd\ufffd")]
	public float thickness;

	[Range(3f, 100f)]
	[Tooltip("\ufffd\ufffd\ufffd")]
	public int segements;

	private List<Vector3> innerVertices;

	private List<Vector3> outterVertices;

	private new void Awake()
	{ }

	private void Update()
	{ }

	protected override void OnPopulateMesh(VertexHelper vh)
	{ }

	public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{ return default; }

	private bool Contains(Vector2 p, List<Vector3> outterVertices, List<Vector3> innerVertices)
	{ return default; }

	private void RayCrossing(Vector2 p, List<Vector3> vertices, ref int crossNumber)
	{ }

	public CircleImage()
	{ }
}
