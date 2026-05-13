using UnityEngine;
using System;
using System.Reflection;

public static class DiagnoseProc
{
    public static string Execute()
    {
        var sb = new System.Text.StringBuilder();
        try
        {
            Assembly asm = typeof(ProcFactory).Assembly;
            sb.AppendLine("Assembly: " + asm.GetName().Name);
            var t1 = asm.GetType("ProcessDelay");
            var t2 = asm.GetType("ProcessLunchGame");
            sb.AppendLine("Type ProcessDelay: " + (t1 != null ? t1.FullName : "NULL"));
            sb.AppendLine("Type ProcessLunchGame: " + (t2 != null ? t2.FullName : "NULL"));
            var baseType = typeof(CBaseProc);
            sb.AppendLine("baseType ProcessDelay assignable: " + (t1 != null ? baseType.IsAssignableFrom(t1).ToString() : "(skip)"));
            sb.AppendLine("baseType ProcessLunchGame assignable: " + (t2 != null ? baseType.IsAssignableFrom(t2).ToString() : "(skip)"));

            // Try AutoRegist manually
            try
            {
                ProcFactory.AutoRegist();
                sb.AppendLine("AutoRegist OK, count=" + ProcFactory.get_count());
            }
            catch (Exception ex)
            {
                sb.AppendLine("AutoRegist EXCEPTION: " + ex.GetType().Name + " : " + ex.Message);
                sb.AppendLine("Inner: " + (ex.InnerException != null ? ex.InnerException.ToString() : "(none)"));
                sb.AppendLine("Stack: " + ex.StackTrace);
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine("Top-level EXCEPTION: " + ex.GetType().Name + " : " + ex.Message);
        }
        return sb.ToString();
    }
}
