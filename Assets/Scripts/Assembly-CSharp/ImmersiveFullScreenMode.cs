using Cpp2IlInjected;
using UnityEngine;

public class ImmersiveFullScreenMode : MonoBehaviour
{
	private const int SYSTEM_UI_FLAG_HIDE_NAVIGATION = 2;

	private const int SYSTEM_UI_FLAG_FULLSCREEN = 4;

	private const int SYSTEM_UI_FLAG_IMMERSIVE_STICKY = 4096;

	private static AndroidJavaObject DecorView;

	private static bool Immersive_state;

	private static AndroidJavaClass jc;

	private static AndroidJavaObject activity;

	public static bool IsImmersiveModeEnabled
	{
		get
		{ return default; }
	}

	private static int GetSDKLevel()
	{ return default; }

	public static void ImmersiveMode_Switch()
	{ }

	private void Start()
	{ }

	public ImmersiveFullScreenMode()
	{ }
}
