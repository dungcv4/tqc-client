using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	[Range(0f, 1f)]
	public float alpha;

	private Graphic mWidget;

	private CanvasRenderer mCr;

	private CanvasGroup mCanvasGroup;

	private void OnEnable()
	{ }

	private void LateUpdate()
	{ }

	public AnimatedAlpha()
	{ }
}
