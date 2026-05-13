// Run ToLuaMenu.GenerateClassWraps() via reflection — regen all *Wrap.cs in Assets/Source/Generate/
using System;
using System.Reflection;
using UnityEngine;

public class RunGenLuaWrap
{
    public static void Execute()
    {
        try
        {
            var t = Type.GetType("ToLuaMenu, Assembly-CSharp-Editor") ?? Type.GetType("ToLuaMenu");
            if (t == null) { Debug.LogError("[RunGenLuaWrap] ToLuaMenu type not found"); return; }

            // Set beAutoGen=true so it doesn't prompt about isCompiling
            var beAutoGen = t.GetField("beAutoGen", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (beAutoGen != null) beAutoGen.SetValue(null, true);

            var m = t.GetMethod("GenerateClassWraps", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (m == null) { Debug.LogError("[RunGenLuaWrap] GenerateClassWraps method not found"); return; }

            m.Invoke(null, null);
            Debug.Log("[RunGenLuaWrap] Completed — refreshing assets");
            UnityEditor.AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            Debug.LogError("[RunGenLuaWrap] FAIL: " + e + (e.InnerException != null ? "\nINNER: " + e.InnerException : ""));
        }
    }
}
