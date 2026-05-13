using UnityEngine;
using System;

public static class DiagnoseProxy
{
    public static string Execute()
    {
        var sb = new System.Text.StringBuilder();
        try
        {
            sb.AppendLine("Test: new GameObject('Test') then AddComponent<RectTransform>...");
            var go = new GameObject("__test_proxywnd");
            sb.AppendLine("  GO created, has Transform: " + (go.GetComponent<Transform>() != null));
            sb.AppendLine("  GO has RectTransform: " + (go.GetComponent<RectTransform>() != null));
            try
            {
                var rt = go.AddComponent<RectTransform>();
                sb.AppendLine("  AddComponent<RectTransform>: " + (rt != null ? "OK" : "NULL"));
            }
            catch (Exception e)
            {
                sb.AppendLine("  AddComponent<RectTransform> threw: " + e.Message);
            }
            UnityEngine.Object.DestroyImmediate(go);

            // Try the ProxyWndForm ctor directly
            sb.AppendLine();
            sb.AppendLine("Test: new ProxyWndForm('layer10', 0)...");
            try
            {
                var pwf = new ProxyWndForm("layer10", 0);
                sb.AppendLine("  ProxyWndForm created OK");
            }
            catch (Exception e)
            {
                sb.AppendLine("  ProxyWndForm threw: " + e.GetType().Name + " : " + e.Message);
                sb.AppendLine("  StackTrace: " + e.StackTrace);
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine("Top EXCEPTION: " + ex.GetType().Name + " : " + ex.Message);
        }
        return sb.ToString();
    }
}
