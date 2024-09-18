using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class AnimationExtractor : Editor
{
    [MenuItem("Tools/Extract Animations #A")]
    static void ExtractAndMoveMaterialsFromModel()
    {
        foreach (Object model in Selection.GetFiltered(typeof(GameObject),SelectionMode.Assets))
        {
            Debug.Log(model.name);
            string path = AssetDatabase.GetAssetPath(model);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string pathDirectory = Path.GetDirectoryName(path);
            string animsDir = Path.Combine(pathDirectory,"Anims");
            Debug.Log(pathDirectory);
            if (!Directory.Exists(animsDir))
            {
                Directory.CreateDirectory(animsDir);
            }
            
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach (var asset in assets)
            {
                Type assetType = asset.GetType();
                if (assetType == typeof(AnimationClip))
                {
                    var tempClip  = new AnimationClip();
                    var oldClip = (AnimationClip) asset;
                    if (!asset.name.Contains("_preview_"))
                    {
                        string animationName = $"{fileName}_{asset.name}.anim";
                        Debug.Log(animsDir);
                        string animPath = Path.Combine(animsDir, animationName);
                        Debug.Log(animPath);
                        if (File.Exists(animPath))
                        {
                            File.Delete(animPath);
                        }
                        EditorUtility.CopySerialized(oldClip, tempClip);
                        AssetDatabase.CreateAsset(tempClip, animPath);
                        Debug.Log($"Asset Name {oldClip.name} Type {asset.GetType()}");
                    }
                }
                    
            }
            AssetDatabase.Refresh();
        }
    }
}
