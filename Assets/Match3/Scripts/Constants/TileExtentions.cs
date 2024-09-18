using System.Collections.Generic;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;
	public static class TileExtentions
	{
		public static List<Tile> GetMaxNumberOfTile(this Tile[,] tiles)
		{
			Debug.Log($"Tiles Count {tiles.Length}");
			List<Tile> maxTiles = new List<Tile>();
			Dictionary<TileName,int> tileCount = new Dictionary<TileName, int>();
			int maxCount = 0;
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					if (tiles[i, j] != null)
					{
						if (tiles[i, j].tileType == TileType.Normal && tiles[i, j].tileName != TileName.None && tiles[i, j].tileType != TileType.None)
						{
							if (tileCount.ContainsKey(tiles[i, j].tileName))
							{
								tileCount[tiles[i, j].tileName]++;
							}
							else
							{
								tileCount.Add(tiles[i, j].tileName, 1);
							}
						}
					}
				}
			}
			
			foreach (var count in tileCount)
			{
				if (count.Value > maxCount)
				{
					maxCount = count.Value;
				}
			}
			
			foreach (var tile in tiles)
			{
				if (tile != null)
				{
					if (tile.tileType == TileType.Normal && tile.tileName != TileName.None && tile.tileType != TileType.None)
					{
						if (tileCount[tile.tileName] == maxCount)
						{
							maxTiles.Add(tile);
						}
					}
				}
			}

			return maxTiles;
		}
	}
}