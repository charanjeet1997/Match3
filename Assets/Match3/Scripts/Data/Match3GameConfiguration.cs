using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Serialization;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	[CreateAssetMenu(fileName = "Match3GameConfiguration", menuName = "Match3/Match3GameConfiguration")]
	public class Match3GameConfiguration : ScriptableObject
	{

		#region PRIVATE_VARS
		[SerializeField] private float tileGap;
		[SerializeField] private float animationSpeed;
		#endregion

		#region PUBLIC_VARS
		[HideInInspector] public int selectedBoardIndex;
		public List<LevelData> levelDatas;
		
		[Header("Tiles")] 
		public TileData[] tilesData;

		public Tile tilePrefab;
		public PowerUpTile powerUpTilePrefab;
		public EmptyTile emptyTilePrefab;
		public GameObject tileBackgroundPrefab;
		public float TileGap { get => tileGap;  set => tileGap = value; }
		public float AnimationSpeed => animationSpeed;
		
		public BoosterData[] boosterData;
		
		public int Width {
			get
			{
				if (levelDatas.Count > selectedBoardIndex)
				{
					return levelDatas[selectedBoardIndex].width;
				}
				return 9;
			}
			set
			{
				levelDatas[selectedBoardIndex].width = value;
				SetPlacementTiles();
			}
		}
		public int Height {
			get
			{
				if (levelDatas.Count > selectedBoardIndex)
				{
					return levelDatas[selectedBoardIndex].height;
				}
				return 9;
			}
			set
			{
				levelDatas[selectedBoardIndex].height = value;
			}
		}
		public TileType TileType { get; set; }
		public TileName TileName { get; set; }
		public PowerUpType PowerUpType { get; set; }
		public TilePlacementData[] TilesPlacementData { get; set; }
		public Challenge Challenge;

		#endregion

		#region UNITY_CALLBACKS

		#endregion

		#region PUBLIC_METHODS
		
		public void SetConfiguration(int boardIndex)
		{
			selectedBoardIndex = boardIndex;
			Width = GetBoardData(boardIndex).width;
			Height = GetBoardData(boardIndex).height;
			TilesPlacementData = GetBoardData(boardIndex).tilesPlacementData;
			Challenge = GetBoardData(boardIndex).challenge;
			//Challenge.Init();
		}

		[CanBeNull]
		public LevelData GetBoardData(int boardIndex)
		{
			LevelData data = levelDatas[boardIndex];
			if (data != null)
			{
				return data;
			}
			return null;
		}
		[CanBeNull]
		public TileData GetTileData(TileName tileName)
		{
			TileData tileData = Array.Find(tilesData, data => data.tileName == tileName);
			if (tileData != null)
			{
				return tileData;
			}
			return null;
		}

		public void SetPlacementTiles()
		{
			var tilePlacementData = levelDatas[selectedBoardIndex].tilesPlacementData;
			levelDatas[selectedBoardIndex].tilesPlacementData = new TilePlacementData[Width * Height];
			//Debug.Log("SetPlacementTiles");
			//Debug.Log("Width: " + Width + " Height: " + Height);
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var tileData = tilePlacementData[x + y * Width];
					if (tileData != null)
					{
						//Debug.Log("Tile Data is not null");
						levelDatas[selectedBoardIndex].tilesPlacementData[x + y * Width] = new TilePlacementData(x, y, tileData.tileType, tileData.tileName, tileData.tileIcon);
					}
					else
					{
						//Debug.Log("Tile Data is null");
						TileData randomTileData = Array.Find(tilesData, data => data.tileType == TileType.Normal && data.tileName == TileName.Random);
						levelDatas[selectedBoardIndex].tilesPlacementData[x + y * Width] = new TilePlacementData(x, y, TileType.Normal, TileName.Random, randomTileData.tileIcon);
					}
				}
			}
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion

	}

	[Serializable]
	public class TilePlacementData
	{
		public int x, y;
		public TileType tileType;
		public TileName tileName;
		public PowerUpType powerUpType;
		public Sprite tileIcon;
		public TilePlacementData(int x, int y,TileType tileType, TileName tileName, Sprite tileIcon)
		{
			this.x = x;
			this.y = y;
			this.tileType = tileType;
			this.tileName = tileName;
			this.tileIcon = tileIcon;
		}
	}
	

	[Serializable]
	public class TileData
	{
		public Sprite tileIcon;
		public TileType tileType;
		public TileName tileName;
		public PowerUpType powerUpType;
	}
}