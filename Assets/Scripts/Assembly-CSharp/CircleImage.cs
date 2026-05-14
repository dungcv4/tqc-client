// Source: Ghidra work/06_ghidra/decompiled_full/CircleImage/ — circle/ring mesh generator.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Circle Image")]
public class CircleImage : BaseImage
{
	[Range(0f, 1f)]
	public float fillPercent;

	public bool fill;

	public float thickness;

	[Range(3f, 100f)]
	public int segements;

	private List<Vector3> innerVertices;
	private List<Vector3> outterVertices;

	private new void Awake()
	{
		if (innerVertices == null) innerVertices = new List<Vector3>();
		if (outterVertices == null) outterVertices = new List<Vector3>();
	}

	private void Update() { }

	// Build a ring (or fill disc) mesh with `segements` segments scaled to rectTransform.rect.
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		if (segements < 3) segements = 3;
		if (innerVertices == null) innerVertices = new List<Vector3>();
		if (outterVertices == null) outterVertices = new List<Vector3>();
		innerVertices.Clear();
		outterVertices.Clear();

		Rect rect = rectTransform.rect;
		float radius = Mathf.Min(rect.width, rect.height) * 0.5f;
		Vector2 center = rect.center;
		Color32 c = color;

		int count = Mathf.Clamp(Mathf.CeilToInt(segements * fillPercent), 0, segements);
		if (count == 0) return;

		float angleStep = (2f * Mathf.PI) / segements;
		float innerR = fill ? 0f : Mathf.Max(0f, radius - thickness);

		for (int i = 0; i <= count; i++)
		{
			float a = angleStep * i - Mathf.PI * 0.5f;
			float cs = Mathf.Cos(a);
			float sn = Mathf.Sin(a);
			outterVertices.Add(new Vector3(center.x + cs * radius, center.y + sn * radius, 0));
			innerVertices.Add(new Vector3(center.x + cs * innerR, center.y + sn * innerR, 0));
		}

		for (int i = 0; i < outterVertices.Count; i++)
		{
			vh.AddVert(outterVertices[i], c, new Vector2(0.5f + (outterVertices[i].x - center.x) / (rect.width), 0.5f + (outterVertices[i].y - center.y) / (rect.height)));
			vh.AddVert(innerVertices[i], c, new Vector2(0.5f + (innerVertices[i].x - center.x) / (rect.width), 0.5f + (innerVertices[i].y - center.y) / (rect.height)));
		}

		for (int i = 0; i < count; i++)
		{
			int o0 = i * 2;
			int i0 = i * 2 + 1;
			int o1 = (i + 1) * 2;
			int i1 = (i + 1) * 2 + 1;
			vh.AddTriangle(o0, o1, i0);
			vh.AddTriangle(i0, o1, i1);
		}
	}

	public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{
		Vector2 local;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out local)) return false;
		return Contains(local, outterVertices, innerVertices);
	}

	// Source: Ghidra Contains.c — ray cast crossing test.
	private bool Contains(Vector2 p, List<Vector3> outterVertices, List<Vector3> innerVertices)
	{
		if (outterVertices == null || outterVertices.Count < 3) return false;
		int outCross = 0;
		RayCrossing(p, outterVertices, ref outCross);
		bool inOuter = (outCross & 1) == 1;
		if (!inOuter || fill) return inOuter;
		if (innerVertices == null || innerVertices.Count < 3) return inOuter;
		int inCross = 0;
		RayCrossing(p, innerVertices, ref inCross);
		bool inInner = (inCross & 1) == 1;
		return inOuter && !inInner;
	}

	// Ray crossing algorithm — count crossings from p going right.
	private void RayCrossing(Vector2 p, List<Vector3> vertices, ref int crossNumber)
	{
		for (int i = 0; i < vertices.Count; i++)
		{
			Vector3 a = vertices[i];
			Vector3 b = vertices[(i + 1) % vertices.Count];
			if (((a.y <= p.y) && (b.y > p.y)) || ((a.y > p.y) && (b.y <= p.y)))
			{
				float vt = (p.y - a.y) / (b.y - a.y);
				if (p.x < a.x + vt * (b.x - a.x)) crossNumber++;
			}
		}
	}

	public CircleImage() { }
}
