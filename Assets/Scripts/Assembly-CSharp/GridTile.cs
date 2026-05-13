using Cpp2IlInjected;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GridTile : MonoBehaviour
{
	public float tileSize;

	private MeshRenderer meshRenderer;

	private MeshFilter filter;

	private Color tileColor;

	public bool bCustomize;

	public float _tileRightX;

	public float _tileTopY;

	public float _tileLeftX;

	public float _tileBottomY;

	private void Awake()
	{ }

	private void Start()
	{ }

	public void setColor(Color col)
	{ }

	public void setCustomizeSize(int tileRightX, int tileTopY, int tileLeftX, int tileBottomY)
	{ }

	public GridTile()
	{ }
}
