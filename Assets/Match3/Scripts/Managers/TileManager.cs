using System;
using Games.CameraManager;

namespace Games.Match3Game
{
	using UnityEngine;
	using System.Collections.Generic;
	using ServiceLocatorFramework;
	using Unity.Mathematics;

	public class TileManager:MonoBehaviour
	{

		#region PRIVATE_VARS
		[SerializeField] private Transform tileParent, tileBackgroundParent;
		[SerializeField] private Vector3 tileOffset;
		private Tile[,] tiles = new Tile[0, 0];
		private GameObject[,] tileBackgrounds = new GameObject[0, 0];
		private float centerX, centerY;
		Dictionary<TileType,TileInstantiationHandler> tileInstantiationHandlers;
		private Dictionary<TileName,TilePlacementHandler> tilePlacementHandlers;
		private TileNeighbourHandler tileNeighbourHandler;
		TilesDropAndSpawnManager tilesDropAndSpawnManager;
		Camera mainCamera;
		#endregion

		#region PUBLIC_VARS
		public Match3GameConfiguration config;
		public Tile[,] Tiles => tiles;
		public Vector3 TileOffset => tileOffset;
		public Dictionary<TileName, TilePlacementHandler> TilePlacementHandlers => tilePlacementHandlers;
		public TileNeighbourHandler TileNeighbourHandler => tileNeighbourHandler;

		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}

		private void Start()
		{
			tilesDropAndSpawnManager = GetComponent<TilesDropAndSpawnManager>();
			mainCamera = ServiceLocator.Current.Get<ICameraManager>().GetCamera();
			InitTileInstantiationHandlers();
			InitTilePlacementHandlers();
		}

		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<TileManager>();	
			tileNeighbourHandler = new TileNeighbourHandler();
		}

		#endregion

		#region PUBLIC_METHODS
		public void Initialize()
		{
			tileNeighbourHandler = new TileNeighbourHandler();
			centerX = (config.Width - 1f) / 2f;
			centerY = (config.Height - 1f) / 2f;
			InstantiateBackground();
			InstantiateTiles();
			tileNeighbourHandler.SetTileNeighbours(config.Width,config.Height,tiles);
			HandelTiles();
		}
		public Tile GetTile(int x, int y)
		{
			if (x < 0 || x >= config.Width || y < 0 || y >= config.Height)
			{
				return null;
			}
			return tiles[x, y];
		}
		
		public void DestroyTile(int x, int y)
		{
			if (x < 0 || x >= config.Width || y < 0 || y >= config.Height)
			{
				return;
			}
			if (tiles[x, y] != null && tiles[x, y].tileType != TileType.None)
			{
				config.Challenge.UpdateProgress(tiles[x,y].tileName,1);
				tiles[x, y].HideTile();
				if(tiles[x,y] is PowerUpTile)
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(tiles[x, y]);
				else 
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(tiles[x, y]);
				tiles[x, y] = null;
				tilesDropAndSpawnManager.DropTiles();
			}
		}
		
		public void DestroyMutipleTiles(List<Tile> tilesToDestroy)
		{
			foreach (Tile tile in tilesToDestroy)
			{
				if (tile.tileType != TileType.None)
				{
					DestroyTile(tile.x, tile.y);
				}
			}
			tilesDropAndSpawnManager.DropTiles();
		}
		public void DestroyAllTiles()
		{
			for (int x = 0; x < config.Width; x++)
			{
				for (int y = 0; y < config.Height; y++)
				{
					if (tiles[x, y] != null && tiles.Length > 0)
					{
						Destroy(tiles[x, y].gameObject);
					}
				}
			}
			DestroyAllObjectInHierarchy(tileParent);
			tiles = new Tile[0, 0];
		}
		
		public void DestroyAllObjectInHierarchy(Transform parent)
		{
			foreach (Transform child in parent)
			{
				Destroy(child.gameObject);
			}
		}
		#endregion

		#region PRIVATE_METHODS
		void InstantiateBackground()
		{
			tileBackgrounds = new GameObject[config.Width, config.Height];
			for (int x = 0; x < config.Width; x++)
			{
				for (int y = 0; y < config.Height; y++)
				{
					if (config.TilesPlacementData[x * config.Height + y].tileType != TileType.None)
					{
						Vector3 position = new Vector3((x - centerX) * config.TileGap, (y - centerY) * config.TileGap, 0) + tileOffset;
						tileBackgrounds[x, y] = Instantiate(config.tileBackgroundPrefab, position, quaternion.identity, tileBackgroundParent);
						tileBackgrounds[x, y].name = "TileBG " + x + ", " + y;
					}
				}
			}
		}

		void AdjustCameraOrthographicSize()
		{
			float aspectRatio = (float)Screen.width / Screen.height;
			float othographicSize = config.GetBoardData(config.selectedBoardIndex).width;
			mainCamera.orthographicSize = othographicSize / 2 + aspectRatio;
		}
		void InstantiateTiles()
		{
			AdjustCameraOrthographicSize();
			tiles = new Tile[config.Width, config.Height];
			for (int x = 0; x < config.Width; x++)
			{
				for (int y = 0; y < config.Height; y++)
				{
					Vector3 tilePosition = new Vector3((x - centerX) * config.TileGap, (y - centerY) * config.TileGap, 0) + tileOffset;
					tiles[x, y] = tileInstantiationHandlers[config.TilesPlacementData[x * config.Height + y].tileType].InstantiateTile(config, tileParent, tilePosition, x, y, config.TilesPlacementData[x * config.Height + y].tileName, config.TilesPlacementData[x * config.Height + y].tileType, config.TilesPlacementData[x * config.Height + y].powerUpType);
				}
			}
		}
		
		private void InitTileInstantiationHandlers()
		{
			tileInstantiationHandlers = new Dictionary<TileType, TileInstantiationHandler>
			{
				{TileType.Normal, new TileInstantiationHandler()}, {TileType.PowerUP, new PowerUpTileInstantiationHandler()}, {TileType.None, new EmptyTileInstantiationHandler()}
			};
		}
		
		private void InitTilePlacementHandlers()
		{
			tilePlacementHandlers = new Dictionary<TileName, TilePlacementHandler>
			{
				{TileName.BlueBottle, new FixedTilePlacementHandler()}, {TileName.GreenBottle, new FixedTilePlacementHandler()}, 
				{TileName.RedBottle, new FixedTilePlacementHandler()}, {TileName.Random, new RandomTilePlacementHandler()},
				{TileName.PlasticBag, new FixedTilePlacementHandler()}, {TileName.PlasticCup, new FixedTilePlacementHandler()},
				{TileName.FishNet, new FixedTilePlacementHandler()}, {TileName.Flower, new FixedTilePlacementHandler()},
				{TileName.Tree, new FixedTilePlacementHandler()}, {TileName.WaterDrop, new FixedTilePlacementHandler()},
				{TileName.WindTurbine, new FixedTilePlacementHandler()}, {TileName.Energy, new FixedTilePlacementHandler()},
				{TileName.None, new EmptyPlacementHandler()}
			};
		}
		
		void HandelTiles()
		{
			for (int x = 0; x < config.Width; x++)
			{
				for (int y = 0; y < config.Height; y++)
				{
					Tile tile = tiles[x, y];
					if (config.TilesPlacementData[x * config.Height + y].tileType != TileType.None && tile != null) 
					{
						TilePlacementData tilePlacementData = config.TilesPlacementData[x * config.Height + y];
						tilePlacementHandlers[tilePlacementData.tileName].HandlePlacement(x, y,tilePlacementData.tileType, tilePlacementData.tileName, tile, config.tilesData);
					}
				}
			}
		}
		
		
		public void SetNeighbours(Tile tile)
		{
			tileNeighbourHandler.AddNeighbour(tile,tiles);
		}
		#endregion
	}
}