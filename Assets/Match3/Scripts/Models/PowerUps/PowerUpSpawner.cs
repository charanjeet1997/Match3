using System;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;

	public class PowerUpSpawner : MonoBehaviour
	{

		#region PRIVATE_VARS

		[SerializeField] private Match3GameConfiguration config;
		[SerializeField] private Transform tileParent;
		private BoardManager boardManager;
		private TileManager tileManager;
		private float centerX, centerY;
		float tileGap = 1.5f;
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void Start()
		{
			boardManager = GetComponent<BoardManager>();
			tileManager = GetComponent<TileManager>();
			tileGap = boardManager.match3GameConfiguration.TileGap;
			centerX = (boardManager.match3GameConfiguration.Width - 1f) / 2f;
			centerY = (boardManager.match3GameConfiguration.Height - 1f) / 2f;
		}

		#endregion

		#region PUBLIC_METHODS

		public bool SpawnBooster(int x, int y,int matchCount)
		{
			Debug.Log($"Match Count : {matchCount}");
			Debug.Log($"Booster Tile Spawned at {x},{y}");
			Tile tile = null;
			switch (matchCount)
			{
				case 4:
					tile = InstantiateBoosterTile(x, y, TileType.PowerUP,TileName.Flower, config.GetTileData(TileName.Flower).tileIcon, PowerUpType.CrossSection);
					return true;
				case 5:
					tile = InstantiateBoosterTile(x, y, TileType.PowerUP,TileName.Tree, config.GetTileData(TileName.Tree).tileIcon, PowerUpType.TwoTileRadius);
					return true;
				case 6:
					tile = InstantiateBoosterTile(x, y, TileType.PowerUP,TileName.WaterDrop, config.GetTileData(TileName.WaterDrop).tileIcon, PowerUpType.ThreeTileRadius);
					return true;
				case 7:
					tile = InstantiateBoosterTile(x, y,TileType.PowerUP, TileName.WindTurbine, config.GetTileData(TileName.WindTurbine).tileIcon, PowerUpType.FourTileRadius);
					return true;
			}
			return false;
		}
		#endregion

		#region PRIVATE_METHODS
		private PowerUpTile InstantiateBoosterTile(int x, int y,TileType tileType, TileName tileName, Sprite sprite, PowerUpType powerUpType)
		{
			PowerUpTile powerUpTile = ServiceLocator.Current.Get<TilePoolManager>().IsPowerUpPoolEmpty ? Instantiate(config.powerUpTilePrefab, tileParent) : ServiceLocator.Current.Get<TilePoolManager>().GetPowerUpFromPool() as PowerUpTile;
			powerUpTile.gameObject.SetActive(true);
			powerUpTile.Init(x, y, tileName, sprite, powerUpType);
			Vector2 targetPosition = new Vector2((x - centerX) * tileGap, (y - centerY) * tileGap) + (Vector2)tileManager.TileOffset;
			powerUpTile.transform.position = targetPosition;
			InitializeAndPlaceTile(powerUpTile, x, y, tileType,tileName);
			return powerUpTile;
		}
		
		private void InitializeAndPlaceTile(Tile tile, int x, int y,TileType tileType,TileName tileName = TileName.Random)
		{
			tile.transform.parent = tileParent;
			tile.gameObject.SetActive(true);
			tile.SetGridPosition(x, y);
			tile.name = "Tile " + x + ", " + y;
			tileManager.Tiles[x, y] = tile;
			tileManager.SetNeighbours(tile);
			tile.ShowTile();
			tileManager.TilePlacementHandlers[tileName].HandlePlacement(x, y,tileType, tileName, tile, boardManager.match3GameConfiguration.tilesData);
		}

		#endregion

	}
}