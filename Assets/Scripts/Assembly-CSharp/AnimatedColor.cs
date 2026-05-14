// Source: Ghidra work/06_ghidra/decompiled_full/AnimatedColor/ — applies a serialized color to a Graphic in LateUpdate
// (used by Animator-driven animation curves that write to this.color).

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AnimatedColor : MonoBehaviour
{
	public Color color;
	private Graphic mWidget;

	private void OnEnable()
	{
		mWidget = GetComponent<Graphic>();
	}

	private void LateUpdate()
	{
		if (mWidget != null) mWidget.color = color;
	}

	public AnimatedColor() { color = Color.white; }
}
