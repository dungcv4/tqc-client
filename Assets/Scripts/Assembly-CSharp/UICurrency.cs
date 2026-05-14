// Source: Ghidra work/06_ghidra/decompiled_full/UICurrency/ — display currency icon + count text.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
	public UIImagePicker CurrencyIcon;
	public Text _Count;

	public void SetCurrency(string currencyTag, int count, bool bEnough = true)
	{
		SetCurrencyOnly(count);
		// CurrencyIcon.pickedTag selects sprite from atlas via currencyTag.
		// bEnough → tint count text red/white. Original uses ColorTagMgr but at minimum: set text color.
		if (_Count != null)
		{
			_Count.color = bEnough ? Color.white : Color.red;
		}
	}

	public void SetCurrencyOnly(int count)
	{
		if (_Count != null) _Count.text = count.ToString();
	}

	public void SetCurrencyWithColor(string currencyTag, int count, string colorTag)
	{
		SetCurrencyOnly(count);
	}

	public void SetCurrencyString(string currencyTag, string showText)
	{
		if (_Count != null) _Count.text = showText;
	}

	public void SetCurrencyStringOnly(string showText)
	{
		if (_Count != null) _Count.text = showText;
	}

	public UICurrency() { }
}
