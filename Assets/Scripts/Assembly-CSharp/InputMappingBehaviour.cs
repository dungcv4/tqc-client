using Cpp2IlInjected;
using Google.Play.InputMapping;
using UnityEngine;

public class InputMappingBehaviour : MonoBehaviour
{
	private enum InputEventIds
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
		WndForm_Map = 4,
		WndForm_Chat = 5,
		WndForm_Bag = 6,
		UseItem1 = 7,
		UseItem2 = 8,
		UseItem3 = 9,
		UseItem4 = 10,
		UseItem5 = 11,
		UseSkill1 = 12,
		UseSkill2 = 13,
		UseSkill3 = 14,
		UseSkill4 = 15,
		UseSkill5 = 16,
		UseSkill6 = 17,
		UseSkill7 = 18,
		UseSkill8 = 19,
		UseSkill9 = 20,
		UseSkill10 = 21,
		EnableAuto = 22
	}

	private class MyInputMappingProvider : InputMappingProvider
	{
		public InputMap OnProvideInputMap()
		{ return default; }

		public MyInputMappingProvider()
		{ }
	}

	private static MyInputMappingProvider _inputMappingProvider;

	public static void Init()
	{ }

	public static void Destroy()
	{ }

	public InputMappingBehaviour()
	{ }
}
