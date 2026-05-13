using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Demo
{
	internal class MSDKGUIPopUpWindow
	{
		public class CustomDesignContent
		{
			public string hint;

			public Dictionary<string, Dictionary<string, Action>> guiList;

			public CustomDesignContent()
			{ }
		}

		private static Rect popupWindowRect;

		private Dictionary<string, Dictionary<string, Action>> GUITitleActions;

		public const string GUI_BUTTON = "Button";

		public const string GUI_EDITOR_LAYOUT_BUTTON = "EditorGUILayoutButton";

		public const string GUI_EDITOR_LAYOUT_TEXTFIELD = "EditorGUILayoutTextField";

		private static CustomDesignContent _customDesignContent;

		private static float _relatedHeight;

		private static float _relatedWidth;

		private static float _windowBtnHeight;

		private static bool _show;

		private static void PopupWindowContent(int windowID)
		{ }

		public static bool RectGUIPopUpWindow(string title = "")
		{ return default; }

		private static Texture2D MakeTex(int width, int height, Color col)
		{ return default; }

		private static void SetCustomDesignContent(float height, float width, CustomDesignContent content)
		{ }

		public static void ShowPopUpWindow(float height, float width, CustomDesignContent content)
		{ }

		public static void ClosePopUpWindow()
		{ }

		public MSDKGUIPopUpWindow()
		{ }
	}
}
