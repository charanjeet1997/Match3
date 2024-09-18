namespace Games.Match3Game
{
	using UnityEditor;
	using UnityEngine;
	using System;
	using System.Collections;
	
	[CustomEditor(typeof(Tile))]
	public class TileEditor:Editor
	{

		#region PRIVATE_VARS
		Tile tile;
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS

		public override void OnInspectorGUI()
		{
			tile = (Tile)target;
			base.OnInspectorGUI();
			EditorGUILayout.BeginVertical();
			for(int i = 0; i < tile.neighbours.Count; i++)
			{
				GUILayout.BeginVertical();
				if (tile.neighbours[i] != null)
				{
					if (tile.neighbours[i].tileType != TileType.None)
					{
						Texture2D texture = AssetPreview.GetAssetPreview(tile.neighbours[i].TileRenderer.sprite.texture);
						int x = tile.neighbours[i].x;
						int y = tile.neighbours[i].y;
						if (GUILayout.Button(texture, GUILayout.Width(50), GUILayout.Height(50)))
						{
							Selection.activeGameObject = tile.neighbours[i].gameObject;
						}

						GUILayout.Label("X: " + x + " Y: " + i);
					}
				}

				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndVertical();
		}

		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}