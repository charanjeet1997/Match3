using System.Collections.Generic;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class PowerUpNeighbourTilesProvider
	{
		public List<Tile> GetTilesToDestroy(PowerUpTile tile, Tile[,] tiles, PowerUpType powerUpType)
		{
			List<Tile> neighbours = new List<Tile>();
			int tileX = tile.x;
			int tileY = tile.y;

			switch (powerUpType)
			{
				case PowerUpType.None:
					break;

				case PowerUpType.CrossSection:
					AddCrossSectionNeighbours(neighbours, tiles, tileX, tileY);
					break;

				case PowerUpType.TwoTileRadius:
					AddNeighbourInRadius(neighbours, tiles, tileX, tileY, 2);
					break;

				case PowerUpType.ThreeTileRadius:
					AddNeighbourInRadius(neighbours, tiles, tileX, tileY, 3);
					break;

				case PowerUpType.FourTileRadius:
					AddNeighbourInRadius(neighbours, tiles, tileX, tileY, 4);
					break;

				case PowerUpType.CrushAllOfOneType:
					ClearAllOfType(neighbours, tiles, tile); 
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			return neighbours;
		}

		private void AddCrossSectionNeighbours(List<Tile> neighbours, Tile[,] tiles, int centerX, int centerY)
		{
			// Add all tiles in the same row and column
			for (int x = 0; x < tiles.GetLength(0); x++)
			{
				AddNeighbourIfValid(neighbours, tiles, x, centerY);
			}
			for (int y = 0; y < tiles.GetLength(1); y++)
			{
				AddNeighbourIfValid(neighbours, tiles, centerX, y);
			}
		}

		private void AddNeighbourInRadius(List<Tile> neighbours, Tile[,] tiles, int centerX, int centerY, int radius)
		{
			for (int x = centerX - radius; x <= centerX + radius; x++)
			{
				for (int y = centerY - radius; y <= centerY + radius; y++)
				{
					AddNeighbourIfValid(neighbours, tiles, x, y);
				}
			}
		}

		private void ClearAllOfType(List<Tile> neighbours, Tile[,] tiles, PowerUpTile tile)
		{
			for (int x = 0; x < tiles.GetLength(0); x++)
			{
				for (int y = 0; y < tiles.GetLength(1); y++)
				{
					if (tiles[x, y] != null && tiles[x, y].tileName == tile.tileName && tiles[x, y].tileType != TileType.None)
					{
						neighbours.Add(tiles[x, y]);
					}
				}
			}
		}

		private void AddNeighbourIfValid(List<Tile> neighbours, Tile[,] tiles, int x, int y)
		{
			if (x >= 0 && y >= 0 && x < tiles.GetLength(0) && y < tiles.GetLength(1) && tiles[x, y] != null && tiles[x, y].tileType != TileType.None)
			{
				neighbours.Add(tiles[x, y]);
			}
		}
	}
}