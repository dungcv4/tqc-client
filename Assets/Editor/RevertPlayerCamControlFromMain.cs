using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

// Revert the previous chế-cháo scene-edit. The correct 1-1 Ghidra port is to add
// PlayerCamControl in Main.SetupScreenSize (between _MainCamera = mainCam and
// DontDestroyOnLoad) — NOT to manually attach in scene to Main GameObject.
public class RevertPlayerCamControlFromMain
{
    [MenuItem("Tools/SGC-VIN/Revert PlayerCamControl from Main GO")]
    public static void Execute()
    {
        const string scenePath = "Assets/Scenes/GameEntry.unity";
        var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        if (!scene.IsValid()) { File.WriteAllText("/tmp/revert_pcc.txt", "scene invalid"); return; }

        GameObject mainGO = GameObject.Find("Main");
        if (mainGO == null) { File.WriteAllText("/tmp/revert_pcc.txt", "Main GO not found"); return; }
        var pcc = mainGO.GetComponent<PlayerCamControl>();
        if (pcc == null) { File.WriteAllText("/tmp/revert_pcc.txt", "no PlayerCamControl on Main"); return; }
        Object.DestroyImmediate(pcc, true);
        EditorUtility.SetDirty(mainGO);
        EditorSceneManager.MarkSceneDirty(scene);
        bool ok = EditorSceneManager.SaveScene(scene);
        File.WriteAllText("/tmp/revert_pcc.txt", "removed PlayerCamControl from Main, saved=" + ok);
    }
}
