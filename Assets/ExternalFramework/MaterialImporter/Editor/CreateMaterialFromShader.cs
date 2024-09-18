using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

public class CreateMaterialFromShader : Editor
{
    private static CreateMaterialFromShader Instance;
    private static ModelImporter modelImporter;
    static string materialFolderPath = string.Empty;
    static string texturesFolderPath = string.Empty;
    private void OnEnable()
    {
        Instance = this;
    }

    [MenuItem("Tools/CreateMaterialFromSelectedShader %m")]
    static void CreateMaterial()
    {
        foreach(Object shader in Selection.GetFiltered(typeof(Shader),SelectionMode.Assets))
        {
            string path = AssetDatabase.GetAssetPath(shader);
            Type type = shader.GetType();
            if (type == typeof(Shader))
            {
                Debug.Log(shader.name);
                string shaderPath = Path.GetDirectoryName(path);
                Debug.Log(shaderPath);
                Material mat = new Material((Shader)shader);
                mat.name = shader.name.Split('/')[1]+"_Mat";
                AssetDatabase.CreateAsset(mat,shaderPath+$"/{mat.name}.mat");
            }
        }
    }
    [MenuItem("Tools/Extract Textures And Materials #x")]
    static void ExtractAndMoveMaterialsFromModel()
    {
        foreach (Object model in Selection.GetFiltered(typeof(GameObject),SelectionMode.Assets))
        {
            Debug.Log(model.name);
            string path = AssetDatabase.GetAssetPath(model);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileNameWithExt = Path.GetFileName(path);
            string pathDirectory = Path.GetDirectoryName(path);
            string[] dirSplit = pathDirectory.Split('\\');
            string direName = dirSplit[dirSplit.Length - 1];
            string directory = pathDirectory;
            if (direName != fileName)
            {
                directory = pathDirectory + $"/{fileName}";
                if (!Directory.Exists(directory))
                {
                    AssetDatabase.CreateFolder(pathDirectory,fileName);
                    Debug.Log("Directory"+ directory);
                }
            }
            
            string fileNewPath = $"{directory}/{fileNameWithExt}";
            materialFolderPath = $"{directory}/Materials";
            texturesFolderPath = $"{directory}/Textures";
            if (!Directory.Exists(materialFolderPath))
            {
                AssetDatabase.CreateFolder(directory,"Materials");
            }
            if (!Directory.Exists(texturesFolderPath))
            {
                AssetDatabase.CreateFolder(directory, "Textures");
            }

            AssetDatabase.MoveAsset(path,fileNewPath);
            Debug.Log(fileNewPath);
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(fileNewPath);
            foreach (var asset in assets)
            {
                Type assetType = asset.GetType();
                if (assetType == typeof(Material))
                {
                    string materialName = asset.name +".mat";
                    string matPath = Path.Combine(materialFolderPath, materialName);
                    matPath = AssetDatabase.GenerateUniqueAssetPath(matPath);
                    AssetDatabase.ExtractAsset(asset, matPath);
                    Debug.Log($"Asset Name {asset.name} Type {asset.GetType()}");
                }
                    
            }
            AssetDatabase.Refresh();
            if (File.Exists(fileNewPath))
            {
                Debug.Log("FileExist");
                modelImporter = AssetImporter.GetAtPath(fileNewPath) as ModelImporter;
            }
            AssetDatabase.Refresh();
            modelImporter = AssetImporter.GetAtPath(fileNewPath) as ModelImporter;
            modelImporter.SearchAndRemapMaterials(ModelImporterMaterialName.BasedOnMaterialName, ModelImporterMaterialSearch.Local);
        }
    }
}
