// Source: Ghidra work/06_ghidra/decompiled_full/ExampleWheelController/ (Start + Update)
// Field offsets: acceleration@0x20, motionVectorRenderer@0x28, m_Rigidbody@0x30.
// Constants: 0x42c80000=100 (maxAngularVelocity), KeyCode 0x111=LeftArrow/0x112=RightArrow,
//            ForceMode.VelocityChange=5, clamp to -0.25.

using Cpp2IlInjected;
using UnityEngine;

public class ExampleWheelController : MonoBehaviour
{
	private static class Uniforms
	{
		internal static readonly int _MotionAmount;

		// Source: Ghidra .cctor — Shader.PropertyToID("_MotionAmount").
		static Uniforms()
		{
			_MotionAmount = Shader.PropertyToID("_MotionAmount");
		}
	}

	public float acceleration;
	public Renderer motionVectorRenderer;
	private Rigidbody m_Rigidbody;

	private void Start()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		if (m_Rigidbody == null) throw new System.NullReferenceException();
		m_Rigidbody.maxAngularVelocity = 100f;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (m_Rigidbody == null) return;
			m_Rigidbody.AddRelativeTorque(-acceleration, 0f, 0f, ForceMode.VelocityChange);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			if (m_Rigidbody == null) return;
			m_Rigidbody.AddRelativeTorque(acceleration, 0f, 0f, ForceMode.VelocityChange);
		}
		if (m_Rigidbody == null) return;
		float av = m_Rigidbody.angularVelocity.x;
		if (motionVectorRenderer == null) return;
		Material mat = motionVectorRenderer.material;
		if (mat == null) return;
		float t = av / -100f;
		if (t < -0.25f) t = -0.25f;
		mat.SetFloat(Uniforms._MotionAmount, t);
	}

	public ExampleWheelController() { }
}
