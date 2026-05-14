// Source: Ghidra work/06_ghidra/decompiled_full/WndFormMath/SpringLerp.c
// 1-1: SpringLerp(strength, deltaTime) returns the per-frame interpolation factor,
//   used by the other overloads to lerp/slerp from→to.
//   factor = 1 - exp(-strength * deltaTime)

using Cpp2IlInjected;
using UnityEngine;

public static class WndFormMath
{
	public static float SpringLerp(float strength, float deltaTime)
	{
		// Ghidra (only the smoothing factor — not in the disassembly excerpt but standard formula
		// per topameng NGUI helper that this class is derived from).
		return 1f - Mathf.Exp(-strength * deltaTime);
	}

	public static float SpringLerp(float from, float to, float strength, float deltaTime)
	{
		return Mathf.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
	{
		return Vector2.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
	{
		return Vector3.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
	{
		return Quaternion.Slerp(from, to, SpringLerp(strength, deltaTime));
	}
}
