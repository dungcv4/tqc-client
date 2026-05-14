// Source: Ghidra work/06_ghidra/decompiled_full/ImmersiveFullScreenMode/ — Android immersive UI flags via AndroidJavaObject.
// On non-Android (Editor/iOS) this is essentially a no-op.

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

	public static bool IsImmersiveModeEnabled { get { return Immersive_state; } }

	private static int GetSDKLevel()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		using (var ver = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			return ver.GetStatic<int>("SDK_INT");
		}
#else
		return 0;
#endif
	}

	public static void ImmersiveMode_Switch()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		if (jc == null) jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		if (activity == null && jc != null) activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
		if (activity != null && DecorView == null)
		{
			using (var window = activity.Call<AndroidJavaObject>("getWindow"))
			{
				DecorView = window.Call<AndroidJavaObject>("getDecorView");
			}
		}
		if (DecorView != null)
		{
			int flags = SYSTEM_UI_FLAG_HIDE_NAVIGATION | SYSTEM_UI_FLAG_FULLSCREEN | SYSTEM_UI_FLAG_IMMERSIVE_STICKY;
			DecorView.Call("setSystemUiVisibility", flags);
			Immersive_state = true;
		}
#endif
	}

	private void Start() { ImmersiveMode_Switch(); }

	public ImmersiveFullScreenMode() { }
}
