// Source: Ghidra work/06_ghidra/decompiled_full/GridMesh/Start.c RVA 0x18d2248 (284-line procedural mesh)
// Source: dump.cs — .cctor body is `private static void .cctor() { }` (empty, just zero-init defaults)
//
// Start() builds a Mesh with a procedural grid of width GridWidth × GridHeight (vertices, UVs, triangles).
// Body is non-trivial (~284 lines of vector math in Ghidra). For Editor diagnostic the mesh is generated
// once on Awake/Start of any scene object holding this component. If the component is not present in any
// loaded scene, Start() is never called.

using Cpp2IlInjected;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour
{
	public static int GridWidth;
	public static int GridHeight;
	private int GridLength;

	// Source: Ghidra Start.c RVA 0x18d2248 — builds a (GridWidth × GridHeight) grid mesh.
	// Body length ~284 lines of vertex/triangle/UV array construction. Loops:
	//   - allocate verts[(GridWidth+1)*(GridHeight+1)], tris[GridWidth*GridHeight*6], uvs[same as verts]
	//   - fill verts: vertex(i,j) = (i, 0, j) * tileSize
	//   - fill uvs: (i/GridWidth, j/GridHeight)
	//   - fill tris per quad: (i,j)-(i+1,j)-(i,j+1) + (i+1,j)-(i+1,j+1)-(i,j+1)
	//   - mesh.vertices/triangles/uvs assignments + RecalculateNormals/Bounds
	private void Start()
	{
		if (GridWidth <= 0 || GridHeight <= 0) return;
		MeshFilter mf = GetComponent<MeshFilter>();
		if (mf == null) return;
		Mesh mesh = new Mesh();
		int vcountX = GridWidth + 1;
		int vcountY = GridHeight + 1;
		Vector3[] verts = new Vector3[vcountX * vcountY];
		Vector2[] uvs = new Vector2[verts.Length];
		int[] tris = new int[GridWidth * GridHeight * 6];
		for (int j = 0; j < vcountY; j++)
		{
			for (int i = 0; i < vcountX; i++)
			{
				int k = j * vcountX + i;
				verts[k] = new Vector3((float)i, 0f, (float)j);
				uvs[k] = new Vector2((float)i / (float)GridWidth, (float)j / (float)GridHeight);
			}
		}
		int t = 0;
		for (int j = 0; j < GridHeight; j++)
		{
			for (int i = 0; i < GridWidth; i++)
			{
				int v0 = j * vcountX + i;
				int v1 = v0 + 1;
				int v2 = v0 + vcountX;
				int v3 = v2 + 1;
				tris[t++] = v0; tris[t++] = v2; tris[t++] = v1;
				tris[t++] = v1; tris[t++] = v2; tris[t++] = v3;
			}
		}
		mesh.vertices = verts;
		mesh.uv = uvs;
		mesh.triangles = tris;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mf.mesh = mesh;
		GridLength = GridWidth * GridHeight;
	}

	public GridMesh() { }

	// Source: dump.cs — empty body.
	static GridMesh() { }
}
