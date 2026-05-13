using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Demo
{
	internal class MSDKGUIStyle
	{
		public static GUIStyle titleStyle;

		public static GUIStyle buttonStyle;

		public static GUIStyle areaTitleStyle;

		public static GUIStyle textFieldStyle;

		public static GUIStyle toggleStyle;

		public static Dictionary<Rect, bool> windowRectToggle;

		public static void InitUI()
		{ }

		public static Rect GUIWindowBox(Rect rect, out Vector2 newPos, string ti) { newPos = default; return default; }

		public static void SetWindowsMiniToggle(bool v)
		{ }

		public static string EditorGUILayoutTextField(string t, string s, float labelWidth, float fieldWidth)
		{ return default; }

		public static int EditorGUILayoutIntField(string t, int s, float labelWidth, float fieldWidth)
		{ return default; }

		public static string EditorGUILayoutPasswordField(string t, string s, float labelWidth, float fieldWidth)
		{ return default; }

		public static void EditorGUILayoutLabelField(string t, string s, float labelWidth, float fieldWidth)
		{ }

		public static bool EditorGUILayoutButton(string t, float btnHeight, float btnWidth)
		{ return default; }

		public static bool EditorGUILayoutToggle(string t, bool selected, float labelWidth, float fieldWidth)
		{ return default; }

		public MSDKGUIStyle()
		{ }

		static MSDKGUIStyle()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
