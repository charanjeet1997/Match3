using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorTool.ColorPallete
{
    [Serializable]
    public class ColorData
    {
        public List<Color> colors;

        public ColorData()
        {
            colors = new List<Color>();
        }
    }


    // Unique color detection script. This script will find all the unique colors in the scene.
    public static class UniqueColorDetection
    {
        public static List<Color> colorPalette;

        //Initialize when editor loads
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            colorPalette = new List<Color>();
            LoadColorData();
            EditorApplication.update += GetUniqueColors;
            EditorApplication.quitting += Quit;
        }
        
        private static void GetUniqueColors()
        {
            // Debug.Log("Running");

            if (EditorWindow.focusedWindow != null &&
                !EditorWindow.focusedWindow.ToString().Contains("UnityEditor.ColorPicker"))
            {
                // Get all the components in the scene
                var components = GameObject.FindObjectsOfType<Component>();

                // Loop through all the components
                foreach (var component in components)
                {
                    // Check if the component has a Color property
                    var colorProp = component.GetType().GetProperty("color");
                    if (colorProp == null) continue;

                    // Get the color value
                    var color = (Color)colorProp.GetValue(component, null);

                    // Check if the color is already in the list
                    if (colorPalette.Contains(color)) continue;

                    // Add the color to the list
                    colorPalette.Add(color);
                }
            }
        }
        private static void Quit()
        {
            Debug.Log("Quitting the Editor");
            SaveColorData();
        }

        private static void LoadColorData()
        {
            string path = Application.dataPath + "/ExternalFramework/IMColorPallet/Editor/ColorData.json";
            if (System.IO.File.Exists(path))
            {
                string dataAsJson = System.IO.File.ReadAllText(path);
                ColorData data = JsonUtility.FromJson<ColorData>(dataAsJson);
                colorPalette = data.colors;
            }
        }
        
        private static void SaveColorData()
        {
            ColorData data = new ColorData();
            data.colors = colorPalette;
            string dataAsJson = JsonUtility.ToJson(data);
            string path = Application.dataPath + "/ExternalFramework/IMColorPallet/Editor/ColorData.json";
            System.IO.File.WriteAllText(path, dataAsJson);
        }
    }
}