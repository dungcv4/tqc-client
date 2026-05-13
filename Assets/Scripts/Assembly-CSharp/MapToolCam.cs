using Cpp2IlInjected;
using UnityEngine;

public class MapToolCam : MonoBehaviour
{
	public float turnSpeed;

	public float panSpeed;

	public float zoomSpeed;

	private Vector3 mouseOrigin;

	private bool isPanning;

	private bool isRotating;

	private bool isZooming;

	public float moveSpeed;

	private static MapToolCam instance;

	public static MapToolCam Instance
	{
		get
		{ return default; }
		set
		{ }
	}

	private void Awake()
	{ }

	private void Start()
	{ }

	private void Update()
	{ }

	public MapToolCam()
	{ }
}
