using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;

namespace EditorTool.ColorPallete
{
    public class IMColorPaletteEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition;
        private float tileSize = 100;
        private Color colorToAdd;
        private int selectedTile = 0;

        [MenuItem("Tools/IMColorPalette")]
        public static void ShowWindow()
        {
            GetWindow<IMColorPaletteEditorWindow>("IMColorPalette");
        }

        [MenuItem("Tools/IMColorPalette", true, 1)]
        public static bool ValidateShowWindow()
        {
            return !EditorApplication.isPlayingOrWillChangePlaymode;
        }

        [Shortcut("IMColorPalette", KeyCode.Alpha2, ShortcutModifiers.Shift)]
        private static void OpenColorPalette()
        {
            ShowWindow();
        }
        private void OnGUI()
        {
            // Start a scroll view to display the color palette
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            // Calculate the number of columns to display the color tiles
            int columns = Mathf.FloorToInt(position.width / tileSize);

            // Use a grid layout to display the color tiles
            GUILayout.BeginHorizontal();
            for (int i = 0; i < UniqueColorDetection.colorPalette.Count; i++)
            {
                // Draw a tile for each color in the color palette
                Color color = UniqueColorDetection.colorPalette[i];
                Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(tileSize), GUILayout.Height(tileSize));
                GUI.DrawTexture(rect, Texture2D.whiteTexture);
                GUI.color = color;
                GUI.DrawTexture(rect, Texture2D.whiteTexture);
                GUI.color = Color.white;

                // Add an event handler to detect clicks on the color tile
                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    // Copy the color to the clipboard on click
                    EditorGUIUtility.systemCopyBuffer = "#" + ColorUtility.ToHtmlStringRGB(color);
                    selectedTile = i;
                    Debug.Log("Click detected");
                }

                // Draw an outline around the selected tile
                if (i == selectedTile)
                {
                    Handles.BeginGUI();
                    Handles.color = Color.cyan;
                    Handles.DrawAAPolyLine(5f, new Vector3[]
                    {
                        new Vector3(rect.xMin, rect.yMax),
                        new Vector3(rect.xMin, rect.yMin),
                        new Vector3(rect.xMax, rect.yMin),
                        new Vector3(rect.xMax, rect.yMax),
                        new Vector3(rect.xMin, rect.yMax)
                    });
                    Handles.EndGUI();
                }

                // Add a new column after the specified number of tiles
                if ((i + 1) % columns == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }

            GUILayout.EndHorizontal();

            // End the scroll view
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal(new GUIStyle("helpbox"));

            colorToAdd = EditorGUILayout.ColorField(colorToAdd);

            // Add a button to add a color to the color palette
            if (GUILayout.Button("Add Color"))
            {
                AddColor();
            }

            // Add a button to remove a color from the color palette
            if (GUILayout.Button("Remove Color"))
            {
                RemoveColor();
            }

            EditorGUILayout.EndHorizontal();
            Repaint();
        }

        private void AddColor()
        {
            Debug.Log("Add Color called");
            // Show the color picker on button click

            // Check if the color is not in the palette
            if (!UniqueColorDetection.colorPalette.Contains(colorToAdd))
            {
                // Add the color to the palette
                UniqueColorDetection.colorPalette.Add(colorToAdd);
            }
        }

        private void RemoveColor()
        {
            UniqueColorDetection.colorPalette.RemoveAt(selectedTile);
        }
    }
}