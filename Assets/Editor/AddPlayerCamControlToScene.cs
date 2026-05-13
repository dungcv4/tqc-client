using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

// One-time scene-setup utility. Production GameEntry scene contains a PlayerCamControl
// component on the UI Camera GameObject; our reconstructed GameEntry.unity scene is
// missing it. Production line 156 (SystemSetting:LoadFromConfigVar → SetCameraPerspective)
// directly indexes `PlayerCamControl.Instance.CAM_HEIGHT` without nil guard → crash.
//
// This script:
//   1. Opens GameEntry.unity (saves any pending Play state)
//   2. Finds the Main GameObject (or creates a child holder)
//   3. AddComponent<PlayerCamControl>()
//   4. Saves the scene
//
// Run via Tools menu OR mcp execute_script.
public class AddPlayerCamControlToScene
{
    [MenuItem("Tools/SGC-VIN/Add PlayerCamControl to GameEntry")]
    public static void Execute()
    {
        const string scenePath = "Assets/Scenes/GameEntry.unity";
        var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        if (!scene.IsValid()) { Debug.LogError("Cannot open " + scenePath); return; }

        // Find existing PlayerCamControl in scene
        var existing = Object.FindObjectsOfType<PlayerCamControl>(true);
        if (existing != null && existing.Length > 0)
        {
            File.WriteAllText("/tmp/add_pcc.txt", "Already present: " + existing.Length + " PlayerCamControl in scene");
            Debug.Log("[AddPlayerCamControlToScene] Already present.");
            return;
        }

        // Attach to Main GameObject if it exists; else create a new holder
        GameObject host = GameObject.Find("Main");
        if (host == null)
        {
            host = new GameObject("PlayerCamControl");
            EditorSceneManager.MoveGameObjectToScene(host, scene);
        }
        var pcc = host.AddComponent<PlayerCamControl>();
        EditorUtility.SetDirty(host);
        EditorSceneManager.MarkSceneDirty(scene);
        bool ok = EditorSceneManager.SaveScene(scene);
        File.WriteAllText("/tmp/add_pcc.txt",
            "Attached to '" + host.name + "', component=" + (pcc != null) + ", saved=" + ok);
        Debug.Log("[AddPlayerCamControlToScene] Saved scene, host=" + host.name);
    }
}
