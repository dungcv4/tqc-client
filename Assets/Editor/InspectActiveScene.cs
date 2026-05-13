using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectActiveScene
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            sb.AppendLine("Scene[" + i + "] name='" + s.name + "' path='" + s.path + "' loaded=" + s.isLoaded + " active=" + (SceneManager.GetActiveScene() == s));
            foreach (var root in s.GetRootGameObjects())
            {
                sb.AppendLine("  root: " + root.name + " active=" + root.activeSelf);
                foreach (var c in root.GetComponents<Component>())
                    sb.AppendLine("    comp: " + (c == null ? "<missing>" : c.GetType().Name));
            }
        }

        // DontDestroyOnLoad scene root objects
        sb.AppendLine("\n--- DontDestroyOnLoad root GOs ---");
        var allGos = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var go in allGos)
        {
            if (go.scene.name == "DontDestroyOnLoad" && go.transform.parent == null)
            {
                sb.AppendLine(go.name + " active=" + go.activeSelf);
                foreach (var c in go.GetComponents<Component>())
                    sb.AppendLine("  comp: " + (c == null ? "<missing>" : c.GetType().Name) + (c is Behaviour beh ? " enabled=" + beh.enabled : ""));
            }
        }

        // Check Main type singleton
        var mainType = System.Type.GetType("Main, Assembly-CSharp");
        var mainGos = Object.FindObjectsOfType(mainType, true);
        sb.AppendLine("\nMain instances in scene: " + mainGos.Length);
        foreach (var m in mainGos)
        {
            var mb = m as MonoBehaviour;
            sb.AppendLine("  on GO=" + mb.gameObject.name + " active=" + mb.gameObject.activeSelf + " enabled=" + mb.enabled + " scene=" + mb.gameObject.scene.name);
        }

        File.WriteAllText("/tmp/scene.txt", sb.ToString());
        Debug.Log("[InspectActiveScene] done");
    }
}
