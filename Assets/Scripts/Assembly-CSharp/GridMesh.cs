using Cpp2IlInjected;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour
{
	public static int GridWidth;

	public static int GridHeight;

	private int GridLength;

	private void Start()
	{ }

	public GridMesh()
	{ }

	static GridMesh()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
