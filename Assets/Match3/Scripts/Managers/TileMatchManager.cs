using System;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System.Collections.Generic;

	public class TileMatchManager : MonoBehaviour
	{

		#region PRIVATE_VARS
		[SerializeField] private Match3GameConfiguration config;
		PowerUpSpawner powerUpSpawner;
		private BoardManager boardManager;
		private TileManager tileManager;
		private TilesDropAndSpawnManager tilesDropAndSpawnManager;
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void Start()
		{
			tilesDropAndSpawnManager = GetComponent<TilesDropAndSpawnManager>();
			powerUpSpawner = GetComponent<PowerUpSpawner>();
			boardManager = GetComponent<BoardManager>();
			tileManager = GetComponent<TileManager>();
		}

		#endregion

		#region PUBLIC_METHODS
		public bool HandleMatches(Tile tile)
		{
			List<Tile> matches = FindMatches(tile);
			var tempMatches = matches;
			if (matches.Count > 2)
			{
				if(matches.Count > 3)
				{
					//Spawn Booster
					if (tile is PowerUpTile powerUpTile)
					{
						ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(powerUpTile);
					}
					else
					{
						ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(tile);
					}
					powerUpSpawner.SpawnBooster(tile.x, tile.y, matches.Count);
					tile.HideTile();
					matches.Remove(tile);
				}
				RemoveMatches(tempMatches);
				Debug.Log($"Match Handler Match Count {matches.Count}"); 
				CoroutineExtension.Execute(this,() => tilesDropAndSpawnManager.DropTiles(matches.Count),0.3f) ;
				config.Challenge.UpdateProgress(tile.tileName, matches.Count);
				return true;
			}
			return false;
		}
		#endregion

		#region PRIVATE_METHODS

		private List<Tile> FindMatches(Tile tile)
        {
            List<Tile> matchingTiles = new List<Tile>();
            // Check horizontal matches
            List<Tile> horizontalMatches = FindHorizontalMatches(tile);
            if (horizontalMatches.Count > 2)
            {
                matchingTiles.AddRange(horizontalMatches);
            }
            // Check vertical matches
            List<Tile> verticalMatches = FindVerticalMatches(tile);
            if (verticalMatches.Count > 2)
            {
                matchingTiles.AddRange(verticalMatches);
            }
            matchingTiles = matchingTiles.RemoveDuplicates();
            return matchingTiles;
        }
		
		private List<Tile> FindHorizontalMatches(Tile tile)
        {
            List<Tile> horizontalMatches = new List<Tile>();
            horizontalMatches.Add(tile);

            // Check left
            for (int x = tile.x - 1; x >= 0; x--)
            {
                Tile checkTile = tileManager.GetTile(x, tile.y);
                if (checkTile != null && checkTile.tileName == tile.tileName && tile.tileType != TileType.None)
                {
                    horizontalMatches.Add(checkTile);
                }
                else
                {
                    break;
                }
            }

            // Check right
            for (int x = tile.x + 1; x < config.Width; x++)
            {
                Tile checkTile = tileManager.GetTile(x, tile.y);
                if (checkTile != null && checkTile.tileName == tile.tileName && tile.tileType != TileType.None)
                {
                    horizontalMatches.Add(checkTile);
                }
                else
                {
                    break;
                }
            }

            return horizontalMatches;
        }

        private List<Tile> FindVerticalMatches(Tile tile)
        {
            List<Tile> verticalMatches = new List<Tile>();
            verticalMatches.Add(tile);

            // Check down
            for (int y = tile.y - 1; y >= 0; y--)
            {
                Tile checkTile = tileManager.GetTile(tile.x, y);
                if (checkTile != null && checkTile.tileName == tile.tileName && tile.tileType != TileType.None)
                {
	                Debug.Log(($"Down Match Found at {checkTile.x},{checkTile.y}"));
                    verticalMatches.Add(checkTile);
                }
                else
                {
                    break;
                }
            }

            // Check up
            for (int y = tile.y + 1; y < config.Height; y++)
            {
                Tile checkTile = tileManager.GetTile(tile.x, y);
                if (checkTile != null && checkTile.tileName == tile.tileName && tile.tileType != TileType.None)
                {
	                Debug.Log(($"Up Match Found at {checkTile.x},{checkTile.y}"));
                    verticalMatches.Add(checkTile);
                }
                else
                {
                    break;
                }
            }

            return verticalMatches;
        }
        
        private void RemoveMatches(List<Tile> matches)
		{
			foreach (Tile tile in matches)
			{
				tile.HideTile();
				if(tile is PowerUpTile)
				{
					PowerUpTile powerUpTile = tile as PowerUpTile;
					tileManager.Tiles[tile.x, tile.y] = null;
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(powerUpTile);
				}
				else
				{
					tileManager.Tiles[tile.x, tile.y] = null;
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(tile);
				}
			}
		}
		#endregion
	}
}