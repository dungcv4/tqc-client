using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Effects/UIDigital")]
public class UIDigital : MonoBehaviour
{
	public GameObject PlusObj;

	public GameObject SubObj;

	public UIImagePicker[] DigitalImgs;

	public int Value;

	public bool ZeroIsNegative;

	public void SetValue(int value)
	{ }

	private void OnValidate()
	{ }

	public UIDigital()
	{ }
}
