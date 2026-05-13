using UnityEngine;
using System.IO;

public class RegenLuaWraps
{
    public static void Execute()
    {
        try
        {
            // Trigger tolua# wrap + binder regeneration per CustomSettings.customTypeList.
            // Equivalent of menu: "Lua / Gen LuaWrap + Binder".
            var menuType = System.Type.GetType("ToLuaMenu, Assembly-CSharp-Editor");
            if (menuType == null)
            {
                // Search across loaded assemblies
                foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
                {
                    var t = asm.GetType("ToLuaMenu");
                    if (t != null) { menuType = t; break; }
                }
            }
            if (menuType == null) { File.WriteAllText("/tmp/regen.txt", "ToLuaMenu type not found"); return; }

            // GenerateClassWraps + GenLuaBinder (private static, invoke via reflection)
            var bf = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static;
            var genWraps = menuType.GetMethod("GenerateClassWraps", bf);
            var genBinder = menuType.GetMethod("GenLuaBinder", bf);
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("menuType: " + menuType.FullName);
            sb.AppendLine("genWraps: " + genWraps);
            sb.AppendLine("genBinder: " + genBinder);

            if (genWraps != null) {
                try { genWraps.Invoke(null, null); sb.AppendLine("GenerateClassWraps OK"); }
                catch (System.Exception e1) { sb.AppendLine("GenerateClassWraps EX: " + (e1.InnerException?.Message ?? e1.Message) + "\n" + e1.InnerException?.StackTrace); }
            }
            if (genBinder != null) {
                try { genBinder.Invoke(null, null); sb.AppendLine("GenLuaBinder OK"); }
                catch (System.Exception e2) { sb.AppendLine("GenLuaBinder EX: " + (e2.InnerException?.Message ?? e2.Message) + "\n" + e2.InnerException?.StackTrace); }
            }

            File.WriteAllText("/tmp/regen.txt", sb.ToString());
            Debug.Log("[RegenLuaWraps]\n" + sb);
        }
        catch (System.Exception e)
        {
            File.WriteAllText("/tmp/regen.txt", "EX: " + (e.InnerException?.Message ?? e.Message) + "\n" + e.StackTrace);
            Debug.LogError("[RegenLuaWraps] EX: " + e);
        }
    }
}
