// Regen only stub wraps via ToLuaMenu reflection.
// Strategy: temporarily filter customTypeList to stub-only classes, run GenerateClassWraps, restore list.
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class RegenStubWrapsOnly
{
    static readonly HashSet<string> StubClassNames = new HashSet<string> {
        "UITweener.Style",
        "AutoSpriteBase",
        "ConfigVar",
        "CreateJobIMG",
        "FxPlayer",
        "GradientText",
        "ImageViewer",
        "InfiniteVerticalScroll",
        "TweenAlpha",
        "TweenPosition",
        "UI_Currency",
        "UI_Digital",
        "UIFixedProgressbarController",
        "UIImageColorPicker",
        "UISizeControl",
        "UISlot",
        "UITweenerGroup",
        "UJScrollRectSnap",
        "WndParticle",
        "AdvImage",
        "BaseEntityLua",
        "FxResource",
        "MagicFxLoader",
        "MagicFxLoader.FxLoader",
        "MagicFxLoader.FxLoaders",
        "MagicFxResource",
        "MarsSDK.Platform.BasePlatform",
        "UnityEngine.PlayMode",
        "UnityEngine.SceneManagement.LoadSceneMode",
        "UnityEngine.ScreenCapture",
    };

    public static void Execute()
    {
        try
        {
            var menuT = Type.GetType("ToLuaMenu, Assembly-CSharp-Editor") ?? Type.GetType("ToLuaMenu");
            var customT = Type.GetType("CustomSettings, Assembly-CSharp-Editor") ?? Type.GetType("CustomSettings");
            if (menuT == null || customT == null) { Debug.LogError("type lookup fail"); return; }

            var beAutoGen = menuT.GetField("beAutoGen", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            beAutoGen?.SetValue(null, true);

            var customTypeListField = customT.GetField("customTypeList", BindingFlags.Public | BindingFlags.Static);
            object originalList = customTypeListField.GetValue(null);
            Type elemType = originalList.GetType().GetElementType(); // BindType

            // Build filtered list
            Array original = (Array)originalList;
            var nameField = elemType.GetField("name");
            var filtered = new List<object>();
            foreach (object bt in original)
            {
                string name = (string)nameField.GetValue(bt);
                if (StubClassNames.Contains(name)) filtered.Add(bt);
            }
            Debug.Log("[RegenStubWrapsOnly] filtered " + filtered.Count + "/" + original.Length);

            Array filteredArr = Array.CreateInstance(elemType, filtered.Count);
            for (int i = 0; i < filtered.Count; i++) filteredArr.SetValue(filtered[i], i);

            // Swap, regen, restore
            customTypeListField.SetValue(null, filteredArr);
            try
            {
                var m = menuT.GetMethod("GenerateClassWraps", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                m.Invoke(null, null);
                Debug.Log("[RegenStubWrapsOnly] GenerateClassWraps completed");
            }
            finally
            {
                customTypeListField.SetValue(null, originalList);
                Debug.Log("[RegenStubWrapsOnly] customTypeList restored");
            }
            UnityEditor.AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            Debug.LogError("[RegenStubWrapsOnly] FAIL: " + e + (e.InnerException != null ? "\nINNER: " + e.InnerException : ""));
        }
    }
}
