// #if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

public class SceneSelector : EditorWindow
{
    [MenuItem("Window/Transmoderna/SceneManager")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SceneSelector>("Scene Manager");
    }
    string[] files;
    private void OnGUI()
    {
        if (Directory.Exists("Assets/Transmoderna/Scenes"))
        {
            files = Directory.GetFiles("Assets/Transmoderna/Scenes", "*.unity");
            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
            {
                if (Path.GetExtension(files[fileIndex]) == ".unity")
                {   
                    GUILayout.Space(10);
                    if (GUILayout.Button(Path.GetFileNameWithoutExtension(files[fileIndex])))
                    {
                        EditorSceneManager.OpenScene(files[fileIndex]);
                    }
                }
            }
        }
    }
}
// #endif