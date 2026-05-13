using Cpp2IlInjected;
using UnityEngine;

public class ExampleWheelController : MonoBehaviour
{
	private static class Uniforms
	{
		internal static readonly int _MotionAmount;

		static Uniforms()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public float acceleration;

	public Renderer motionVectorRenderer;

	private Rigidbody m_Rigidbody;

	private void Start()
	{ }

	private void Update()
	{ }

	public ExampleWheelController()
	{ }
}
