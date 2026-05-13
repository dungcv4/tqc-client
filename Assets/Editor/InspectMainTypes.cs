using System.IO;
using UnityEngine;
using System.Reflection;

public class InspectMainTypes
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        // Enumerate all types named "Main" in all loaded assemblies
        foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                foreach (var t in asm.GetTypes())
                {
                    if (t.Name == "Main" || t.FullName == "Main")
                    {
                        sb.AppendLine("FOUND Main: " + t.AssemblyQualifiedName);
                    }
                }
            }
            catch { }
        }

        // Get the Main on the GO
        var go = GameObject.Find("Main");
        if (go != null)
        {
            foreach (var c in go.GetComponents<Component>())
            {
                if (c != null) sb.AppendLine("GO 'Main' has component type: " + c.GetType().AssemblyQualifiedName);
            }
        }

        File.WriteAllText("/tmp/main_types.txt", sb.ToString());
        Debug.Log("[InspectMainTypes] done");
    }
}
