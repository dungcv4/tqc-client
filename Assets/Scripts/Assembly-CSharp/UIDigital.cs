// Source: Ghidra work/06_ghidra/decompiled_full/UIDigital/ — display number using sprite digits.

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
	{
		Value = value;
		if (DigitalImgs == null || DigitalImgs.Length == 0) return;
		bool negative = value < 0 || (value == 0 && ZeroIsNegative);
		int abs = (value < 0) ? -value : value;

		if (PlusObj != null) PlusObj.SetActive(!negative && value != 0);
		if (SubObj != null) SubObj.SetActive(negative);

		for (int i = 0; i < DigitalImgs.Length; i++)
		{
			if (DigitalImgs[i] == null) continue;
			int digit = abs % 10;
			abs /= 10;
			// First slot always shown (units); higher digits only if number large enough
			bool show = (i == 0) || (value != 0 && (Value < 0 ? -Value : Value) >= Mathf.Pow(10, i));
			DigitalImgs[i].gameObject.SetActive(show);
			// Source: Ghidra SetValue.c — calls UIImagePicker__SetCurTagByIndex(picker, param_2 % 10).
			DigitalImgs[i].SetCurTagByIndex(digit);
		}
	}

	private void OnValidate()
	{
		SetValue(Value);
	}

	public UIDigital() { }
}
