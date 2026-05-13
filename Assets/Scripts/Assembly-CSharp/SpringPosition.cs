using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	public delegate void OnFinished();

	public static SpringPosition current;

	public Vector3 target;

	public float strength;

	public bool worldSpace;

	public bool ignoreTimeScale;

	public OnFinished onFinished;

	private Transform mTrans;

	private float mThreshold;

	private void Start()
	{ }

	private void Update()
	{ }

	private void NotifyListeners()
	{ }

	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{ return default; }

	public SpringPosition()
	{ }
}
