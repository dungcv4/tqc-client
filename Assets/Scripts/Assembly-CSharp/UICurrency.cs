using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
	public UIImagePicker CurrencyIcon;

	public Text _Count;

	public void SetCurrency(string currencyTag, int count, bool bEnough = true)
	{ }

	public void SetCurrencyOnly(int count)
	{ }

	public void SetCurrencyWithColor(string currencyTag, int count, string colorTag)
	{ }

	public void SetCurrencyString(string currencyTag, string showText)
	{ }

	public void SetCurrencyStringOnly(string showText)
	{ }

	public UICurrency()
	{ }
}
