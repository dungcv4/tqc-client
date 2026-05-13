// Run ToLuaMenu.GenLuaDelegates() via reflection — regenerates Assets/Source/Generate/DelegateFactory.cs
using System;
using System.Reflection;
using UnityEngine;

public class RunGenLuaDelegates
{
    public static void Execute()
    {
        try
        {
            var t = System.Type.GetType("ToLuaMenu, Assembly-CSharp-Editor")
                  ?? System.Type.GetType("ToLuaMenu");
            if (t == null) { Debug.LogError("[RunGenLuaDelegates] ToLuaMenu type not found"); return; }

            var m = t.GetMethod("GenLuaDelegates", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (m == null) { Debug.LogError("[RunGenLuaDelegates] GenLuaDelegates method not found"); return; }

            // Set beAutoGen=true so it doesn't prompt about EditorApplication.isCompiling
            var beAutoGen = t.GetField("beAutoGen", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (beAutoGen != null) beAutoGen.SetValue(null, true);

            m.Invoke(null, null);
            Debug.Log("[RunGenLuaDelegates] Completed");
        }
        catch (Exception e)
        {
            Debug.LogError("[RunGenLuaDelegates] FAIL: " + e + "\n" + (e.InnerException != null ? "INNER: " + e.InnerException : ""));
        }
    }
}
