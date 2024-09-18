using System.Collections.Generic;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class TilesDropAndSpawnManager : MonoBehaviour
	{
		#region PRIVATE_VARS
		private BoardManager boardManager;
		TileManager tileManager;
		private TileMatchManager tileMatchManager;
		private float centerX, centerY;
		[SerializeField] private Match3GameConfiguration config;
		[SerializeField] private Transform tileParent;
		TileInstantiationHandler tileInstantiationHandler;
		bool canSpawnNewTiles = true;
		#endregion

		#region PUBLIC_VARS
		
		#endregion
		
		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}

		private void Start()
		{
			tileInstantiationHandler = new TileInstantiationHandler();
			boardManager = GetComponent<BoardManager>();
			tileMatchManager = GetComponent<TileMatchManager>();
			tileManager = GetComponent<TileManager>();
			centerX = (config.Width - 1f) / 2f;
			centerY = (config.Height - 1f) / 2f;
		}

		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<TilesDropAndSpawnManager>();
		}

		#endregion
		
		#region PUBLIC_METHODS
		public void DropTiles(int matchCount = 0)
		{
			StartCoroutine(DropTilesRoutine(matchCount));
		}
		#endregion

		#region PRIVATE_METHODS
		private IEnumerator DropTilesRoutine(int matchCount = 0)
		{
			yield return new WaitForSeconds(0.2f);
			Debug.Log($"Drop Tiles Match Count : {matchCount}");
			int width = config.Width;
			int height = config.Height;
			float tileGap = config.TileGap;

			// Identify columns with gaps
			HashSet<int> columnsToUpdate = new HashSet<int>();
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (IsValidPosition(x, y) && tileManager.Tiles[x, y] == null)
					{
						columnsToUpdate.Add(x);
						break;
					}
				}
			}
			foreach (int x in columnsToUpdate)
			{
				for (int y = 0; y < height; y++)
				{
					if (IsValidPosition(x, y) && tileManager.Tiles[x, y] == null)
					{
						for (int ny = y + 1; ny < height; ny++)
						{
							if (IsValidPosition(x, ny) && tileManager.Tiles[x, ny] != null && tileManager.Tiles[x, ny].tileType != TileType.None)
							{
								Tile tile = tileManager.Tiles[x, ny];
								tileManager.Tiles[x, y] = tile;
								tileManager.Tiles[x, ny] = null;
								tile.SetGridPosition(x, y);
								tile.name = "Tile " + x + ", " + y;
								tile.AnimateToPosition(new Vector2((x - centerX) * tileGap, (y - centerY) * tileGap) + (Vector2)tileManager.TileOffset);
								break;
							}
						}
					}
				}
			}

			yield return new WaitForSeconds(0.1f);

			StartCoroutine(SpawnNewTiles(columnsToUpdate, matchCount));
		}

		private IEnumerator SpawnNewTiles(HashSet<int> columnsToUpdate, int matchCount = 0)
		{
			if (canSpawnNewTiles)
			{
				int width = config.Width;
				int height = config.Height;
				float tileGap = config.TileGap;
				Debug.Log($"Match Count : {matchCount}");
				foreach (int x in columnsToUpdate)
				{
					int firstValidRow = GetFirstValidRow(x, height);
					for (int y = height - 1; y >= 0; y--)
					{
						if (IsValidPosition(x, y) && y >= firstValidRow && tileManager.Tiles[x, y] == null)
						{
							Tile tile = ServiceLocator.Current.Get<TilePoolManager>().GetTileFromPool();
							Vector2 startPosition = new Vector2((x - centerX) * tileGap, (y + config.Height) * tileGap) + (Vector2)tileManager.TileOffset;
							Vector2 targetPosition = new Vector2((x - centerX) * tileGap, (y - centerY) * tileGap) + (Vector2)tileManager.TileOffset;
							if (tile == null)
							{
								Debug.LogError("Tile is null. New tile will be spawned with default values.");
								tile = tileInstantiationHandler.InstantiateTile(config, tileParent, startPosition, x, y, TileName.Random, TileType.Normal, PowerUpType.None);
							}

							tile.transform.position = startPosition;
							InitializeAndPlaceTile(tile, x, y, TileType.Normal);
							Debug.Log($"Normal Tile Spawned at {x},{y}");
							tile.AnimateToPosition(targetPosition);
						}
					}

					yield return new WaitForEndOfFrame();
				}

				yield return new WaitForSeconds(0.1f); // Wait for new tiles to settle
				
				bool newMatchesFound = false;
				for (int x = 0; x < width; x++)
				{
					for (int y = 0; y < height; y++)
					{
						if (tileManager.Tiles == null)
						{
							Debug.LogError("tileManager.Tiles is null.");
							yield break;
						}

						Tile tile = tileManager.Tiles[x, y];
						if (tile != null && tile.tileType != TileType.None)
						{
							if (tileMatchManager == null)
							{
								Debug.LogError("matchHandler is null.");
								yield break;
							}

							if (tileMatchManager.HandleMatches(tile))
							{
								newMatchesFound = true;
							}
						}
					}
				}
				if (newMatchesFound)
				{
					DropTiles();
				}
				else
				{
					yield return CoroutineExtension.Execute(boardManager, () => boardManager.ResetSwipe(), 0.5f);
					yield return new WaitForEndOfFrame();
					tileManager.TileNeighbourHandler.RemoveTileNeighbours(config.Width, config.Height, tileManager.Tiles);
					yield return new WaitForEndOfFrame();
					tileManager.TileNeighbourHandler.SetTileNeighbours(config.Width, config.Height, tileManager.Tiles);
					yield return new WaitForEndOfFrame();
					// boardManager.CheckAndHandleDuplicateTiles();
				}
			}
		}
		private void InitializeAndPlaceTile(Tile tile, int x, int y, TileType tileType, TileName tileName = TileName.Random)
		{
			tile.transform.parent = tileParent;
			tile.gameObject.SetActive(true);
			tile.SetGridPosition(x, y);
			tile.name = "Tile " + x + ", " + y;
			tileManager.Tiles[x, y] = tile;
			tileManager.SetNeighbours(tile);
			tile.ShowTile();
			tileManager.TilePlacementHandlers[tileName].HandlePlacement(x, y, tileType, tileName, tile, config.tilesData);
		}

		private int GetFirstValidRow(int x, int height)
		{
			for (int y = 0; y < height; y++)
			{
				if (IsValidPosition(x, y))
				{
					return y;
				}
			}
			return height;
		}

		private bool IsValidPosition(int x, int y)
		{
//			Debug.Log($"Checking if position {x},{y} is valid.");
			return config.TilesPlacementData[x * config.Height + y].tileType != TileType.None;
		}
		#endregion
	}
}
