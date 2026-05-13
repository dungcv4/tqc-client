using Cpp2IlInjected;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
	public Transform[] backgrounds;

	public float parallaxScale;

	public float[] parallaxReductionFactor;

	public float smoothing;

	private Transform cam;

	private Vector3 previousCamPos;

	private void Awake()
	{ }

	private void Start()
	{ }

	private void Update()
	{ }

	public BackgroundParallax()
	{ }
}
