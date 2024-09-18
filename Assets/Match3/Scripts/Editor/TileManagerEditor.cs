using UnityEditor;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	[CustomEditor(typeof(TileManager))]
	public class TileManagerEditor : Editor
	{

		#region PRIVATE_VARS
		TileManager tileManager;
		Match3GameConfiguration config => tileManager.config;
		int width = 0, height = 0;
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			tileManager = (TileManager)target;

			GUILayout.BeginVertical();
			// EditorGUILayout.PropertyField(tiles_Prop);
			serializedObject.ApplyModifiedProperties();
			width = tileManager.config.Width;
			height = tileManager.config.Height;
			GUILayout.BeginHorizontal();
			if(tileManager.Tiles.Length > 0)
			{
				for (int x = 0; x < width; x++)
				{
					EditorGUILayout.BeginVertical();
					for(int y = height-1; y >= 0; y--)
					{
						GUILayout.BeginVertical();
						if (tileManager.Tiles[x, y] != null)
						{
							Texture2D texture = AssetPreview.GetAssetPreview(Array.Find(config.tilesData, t => t.tileName == tileManager.Tiles[x, y].tileName).tileIcon.texture);
							if(GUILayout.Button(texture, GUILayout.Width(50), GUILayout.Height(50)))
							{
								Selection.activeGameObject = tileManager.Tiles[x, y].gameObject;
							}
							GUILayout.Label("X: " + x + " Y: " + y);
						}

						EditorGUILayout.EndVertical();
					}
					EditorGUILayout.EndVertical();
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
		}

		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}