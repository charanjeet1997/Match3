namespace Games.Match3Game
{
	using System.IO;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;
	using System;

	public class GameEditor : EditorWindow
	{
		Match3GameConfiguration config;
		Vector2 scrollPos;
		int selectedLevelIndex;
		private LevelData levelDataInstance = null;
		private string levelName;
		[MenuItem("Match3/GameEditor")]
		public static void ShowWindow()
		{
			GameEditor window = (GameEditor)EditorWindow.GetWindow(typeof(GameEditor));
			window.Show();
		}

		private void OnEnable()
		{
			if(config == null)
				return;
			if(config.selectedBoardIndex < 0 || config.selectedBoardIndex >= config.levelDatas.Count)
				config.selectedBoardIndex = 0;
			
		}

		private void OnGUI()
		{
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos,true,true);
			EditorGUILayout.BeginVertical("Box");
			GUILayout.Label("Game Editor", EditorStyles.boldLabel);
			
			config = EditorGUILayout.ObjectField("Board Configuration",config, typeof(Match3GameConfiguration), true) as Match3GameConfiguration;
			
			if (config != null)
			{
				if(selectedLevelIndex != config.selectedBoardIndex)
				{
					selectedLevelIndex = config.selectedBoardIndex;
					config.SetConfiguration(config.selectedBoardIndex);
				}
				config.selectedBoardIndex = EditorGUILayout.IntField("Select Level", config.selectedBoardIndex);
				levelName = $"Level {config.levelDatas.Count}";
				
				if (GUILayout.Button("Load All Levels"))
				{
					LoadLevel();
				}

				if (levelDataInstance == null)
				{
					if(GUILayout.Button("Create New Level"))
					{
						config.levelDatas.Add(new LevelData(levelName, config.Width, config.Height));
					}
					EditorGUILayout.EndVertical();
					EditorGUILayout.EndScrollView();
					return;
				}

				if (config.levelDatas.Count == 0 || config.levelDatas.Count <= selectedLevelIndex)
				{
					if(GUILayout.Button("Create New Level"))
					{
						config.levelDatas.Add(new LevelData(levelName, config.Width, config.Height));
					}
					EditorGUILayout.EndVertical();
					EditorGUILayout.EndScrollView();
					return;
				}
				if(levelDataInstance != config.GetBoardData(config.selectedBoardIndex))
				{
					levelDataInstance = config.GetBoardData(config.selectedBoardIndex);
				}
				//DrawChallenge();
				selectedLevelIndex = config.selectedBoardIndex;
				config.Width = EditorGUILayout.IntField("Width", config.Width);
				config.Height = EditorGUILayout.IntField("Height", config.Height);
				config.TileGap = EditorGUILayout.FloatField("Tile Gap", config.TileGap);
				List<TileData> normalTiles = new List<TileData>();
				List<TileData> powerUpTiles = new List<TileData>();
				List<TileData> emptyTiles = new List<TileData>();
				foreach (var tileData in config.tilesData)
				{
					switch (tileData.tileType)
					{
						case TileType.Normal:
							normalTiles.Add(tileData);
							break;
						case TileType.PowerUP:
							powerUpTiles.Add(tileData);
							break;
						case TileType.None:
							emptyTiles.Add(tileData);
							break;
					}
				}
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.BeginVertical();
				DrawSelectionTiles(normalTiles, TileType.Normal, "Normal Tiles");
				EditorGUILayout.EndVertical();
				EditorGUILayout.BeginVertical();
				DrawSelectionTiles(powerUpTiles, TileType.PowerUP, "PowerUp Tiles");
				DrawSelectionTiles(emptyTiles, TileType.None, "Empty Tiles");
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
				InitPlacementTiles();
				DrawPlacementTiles();
				
				EditorGUILayout.BeginHorizontal();
				if(GUILayout.Button("Create New Level"))
				{
					config.levelDatas.Add(new LevelData(levelName, config.Width, config.Height));
				}

				if (GUILayout.Button("Save Current Level"))
				{
					SaveLevel();
				}
				
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				EditorGUILayout.HelpBox("Please select a Board Configuration", MessageType.Warning);
			}
			
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndScrollView();
			
			if (GUI.changed)
			{
				EditorUtility.SetDirty(config);
				Undo.RecordObject(config, "Board Configuration");
			}
		}
		
		void OnInspectorUpdate()
		{
			Repaint();
		}

		void DrawSelectionTiles(List<TileData> normalTiles, TileType tileType, string label)
		{
			EditorGUILayout.LabelField(label);
			foreach (var tileData in normalTiles)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(tileData.tileName.ToString(), GUILayout.Width(100));
				if(tileData.tileIcon == null)
					Debug.Log("Tile Data is null");
				if (GUILayout.Button(tileData.tileIcon.texture, GUILayout.Width(50), GUILayout.Height(50)))
				{
					config.TileType = tileType;
					config.TileName = tileData.tileName;
					config.PowerUpType = tileData.powerUpType;
				}

				EditorGUILayout.EndHorizontal();
			}
		}

		void InitPlacementTiles()
		{
			if (levelDataInstance.tilesPlacementData == null)
			{
				levelDataInstance.tilesPlacementData = new TilePlacementData[levelDataInstance.width * levelDataInstance.height];
				for(int x = 0; x < levelDataInstance.width; x++)
				{
					for(int y = 0; y < levelDataInstance.height; y++)
					{
						levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y] = new TilePlacementData(x, y,config.tilesData[0].tileType, config.tilesData[0].tileName, config.tilesData[0].tileIcon); 
					}
				}
			}
			else
			{
				TilePlacementData[] temp = levelDataInstance.tilesPlacementData;
				levelDataInstance.tilesPlacementData = new TilePlacementData[levelDataInstance.width * levelDataInstance.height];
				if(temp.Length < levelDataInstance.width * levelDataInstance.height)
					Array.Copy(temp, levelDataInstance.tilesPlacementData, temp.Length);
				else
					Array.Copy(temp, levelDataInstance.tilesPlacementData, levelDataInstance.width * levelDataInstance.height);
			
			
				for (int x = 0; x < levelDataInstance.width; x++)
				{
					for (int y = 0; y < levelDataInstance.height; y++)
					{
						if(levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y] != null)
						{
							levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].x = x;
							levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].y = y;
						}
					}
				}
			}
		}
		void DrawPlacementTiles()
		{
			GUILayout.BeginHorizontal();
			for (int x = 0; x < levelDataInstance.width; x++)
			{
				EditorGUILayout.BeginVertical();
				for (int y = levelDataInstance.height - 1; y >= 0; y--)
				{
					GUILayout.BeginVertical();
//						Debug.Log("X: " + x + " Y: " + y);
					if (levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y] != null)
					{
						Texture2D texture = AssetPreview.GetAssetPreview(Array.Find(config.tilesData,t => t.tileName == levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].tileName).tileIcon.texture);
						if (GUILayout.Button(texture, GUILayout.Width(50), GUILayout.Height(50)))
						{
							if (config.TileType == TileType.None)
							{
								levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].tileName = TileName.None;
								config.TilesPlacementData[x * levelDataInstance.height + y].tileType = TileType.None;
							}
							else
							{
								levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].tileName =config.TileName;
								levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].tileType =config.TileType;
								levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].powerUpType =config.PowerUpType;
								levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].tileIcon =config.tilesData[Array.FindIndex(config.tilesData, t => t.tileName == config.TileName)].tileIcon;
							}

							levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].x = x;
							levelDataInstance.tilesPlacementData[x * levelDataInstance.height + y].y = y;
							config.SetConfiguration(selectedLevelIndex);
							Undo.RecordObject(config, "Tile Type Changed");
						}
					}

					GUILayout.Label("X: " + x + " Y: " + y);
					EditorGUILayout.EndVertical();
				}

				EditorGUILayout.EndVertical();
			}

			GUILayout.EndHorizontal();
		}
		
		TileName challangeTileName;
		void DrawChallenge()
		{
			EditorGUILayout.BeginVertical("Box");
			GUILayout.Label("Challenge", EditorStyles.boldLabel);
			if (levelDataInstance.challenge == null)
			{
				levelDataInstance.challenge = new Challenge("Match 3 Tiles", 10);
			}
			levelDataInstance.challenge.moveLimit = EditorGUILayout.IntField("Move Limit", levelDataInstance.challenge.moveLimit);
			DrawClearSpecificTilesChallenge();
			EditorGUILayout.EndVertical();
		}
		
		private void DrawClearSpecificTilesChallenge()
		{
			EditorGUILayout.LabelField("Clear Specific Tiles Challenge");
			GUILayout.Space(5);
			
			Challenge challenge = levelDataInstance.challenge;
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Tiles to Clear");
				
			challenge.targetTile1.tileName = (TileName)EditorGUILayout.EnumPopup("Tile 1", challenge.targetTile1.tileName);
			challenge.targetTile1.amount = EditorGUILayout.IntField("Amount", challenge.targetTile1.amount);
			challenge.targetTile2.tileName = (TileName)EditorGUILayout.EnumPopup("Tile 2", challenge.targetTile2.tileName);
			challenge.targetTile2.amount = EditorGUILayout.IntField("Amount", challenge.targetTile2.amount);
			
			EditorGUILayout.EndVertical();
		}
		
		public void SaveLevel()
		{
			string json = JsonUtility.ToJson(config.levelDatas[selectedLevelIndex]);
			Match3Constants.Match3SaveAndLoadConstants.SaveLevel(json, selectedLevelIndex);
			AssetDatabase.Refresh();
		}

		public void LoadLevel()
		{
			string levelDirectoryPath = Match3Constants.Match3SaveAndLoadConstants.levelDirectoryPath;
			if(!Directory.Exists(levelDirectoryPath))
			{
				Directory.CreateDirectory(levelDirectoryPath);
			}

			var levelPaths = Directory.GetFiles(levelDirectoryPath);
			Debug.Log(levelPaths.Length);
			config.levelDatas.Clear();
			foreach (var levelPath in levelPaths)
			{
				string ext = Path.GetExtension(levelPath);
				if (ext != ".meta")
				{
					Debug.Log(levelPath);
					string json = File.ReadAllText(levelPath);
					Debug.Log(json);
					config.levelDatas.Add(JsonUtility.FromJson<LevelData>(json));
				}
			}
			
		}
	}
}