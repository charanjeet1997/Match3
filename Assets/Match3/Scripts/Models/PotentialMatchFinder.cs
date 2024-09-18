namespace Games.Match3Game
{
	using System.Collections.Generic;
	using ServiceLocatorFramework;
	using UnityEngine;
	using System;

	public class PotentialMatchFinder : MonoBehaviour
	{
		 public TileManager tileManager;
         public List<Tile> tiles;
         [ContextMenu("FindPotentialMatches")]
         public void FindPotentialMatches()
         {
             tiles = FindHint();
         }
         
        /// <summary>
        /// Finds potential matches by checking adjacent tiles for possible swaps that would result in a match.
        /// </summary>
        /// <returns>List of tiles that would be matched if swapped.</returns>
        public List<Tile> FindHint()
        {
            Tile[,] tiles = tileManager.Tiles;

            for (int x = 0; x < tileManager.config.Width; x++)
            {
                for (int y = 0; y < tileManager.config.Height; y++)
                {
                    Tile currentTile = tiles[x, y];

                    // Check the tile to the right (horizontal swap)
                    if (x < tileManager.config.Width - 1)
                    {
                        var matchedTiles = CheckForMatchAfterSwap(currentTile, tiles[x + 1, y]);
                        if (matchedTiles != null && matchedTiles.Count > 0)
                        {
                            return matchedTiles;
                        }
                    }

                    // Check the tile below (vertical swap)
                    if (y < tileManager.config.Height - 1)
                    {
                        var matchedTiles = CheckForMatchAfterSwap(currentTile, tiles[x, y + 1]);
                        if (matchedTiles != null && matchedTiles.Count > 0)
                        {
                            return matchedTiles;
                        }
                    }
                }
            }

            return null; // Return null if no hints are found
        }

        /// <summary>
        /// Checks for matches after a simulated swap of two tiles.
        /// </summary>
        /// <param name="tile1">The first tile.</param>
        /// <param name="tile2">The second tile.</param>
        /// <returns>List of tiles that form a match after the swap.</returns>
        private List<Tile> CheckForMatchAfterSwap(Tile tile1, Tile tile2)
        {
            // Temporarily swap the tiles
            SwapTiles(tile1, tile2);

            // Check if this swap results in a match for either tile
            List<Tile> matchedTiles = GetMatchedTiles(tile1);
            matchedTiles.AddRange(GetMatchedTiles(tile2));

            // Swap the tiles back to their original positions
            SwapTiles(tile1, tile2);

            return matchedTiles.Count > 0 ? matchedTiles : null;
        }

        /// <summary>
        /// Swaps two tiles' positions within the grid.
        /// </summary>
        /// <param name="tile1">The first tile.</param>
        /// <param name="tile2">The second tile.</param>
        private void SwapTiles(Tile tile1, Tile tile2)
        {
            int tempX = tile1.x;
            int tempY = tile1.y;

            // Swap positions in the grid
            tileManager.Tiles[tile1.x, tile1.y] = tile2;
            tileManager.Tiles[tile2.x, tile2.y] = tile1;

            // Swap their x and y coordinates
            tile1.SetGridPosition(tile2.x, tile2.y);
            tile2.SetGridPosition(tempX, tempY);
        }

        /// <summary>
        /// Returns a list of tiles that form a match of three or more consecutive tiles of the same type.
        /// </summary>
        /// <param name="tile">The tile to check from.</param>
        /// <returns>List of tiles that form a match.</returns>
        private List<Tile> GetMatchedTiles(Tile tile)
        {
            List<Tile> matchedTiles = new List<Tile>();
            Tile[,] tiles = tileManager.Tiles;
            int x = tile.x;
            int y = tile.y;

            // Check horizontal match
            if (x > 1 && tiles[x - 1, y] != null && tiles[x - 2, y] != null &&
                tile.tileName == tiles[x - 1, y].tileName && tile.tileName == tiles[x - 2, y].tileName)
            {
                matchedTiles.Add(tiles[x - 1, y]);
                matchedTiles.Add(tiles[x - 2, y]);
                matchedTiles.Add(tile);
            }
            if (x < tileManager.config.Width - 2 && tiles[x + 1, y] != null && tiles[x + 2, y] != null &&
                tile.tileName == tiles[x + 1, y].tileName && tile.tileName == tiles[x + 2, y].tileName)
            {
                matchedTiles.Add(tiles[x + 1, y]);
                matchedTiles.Add(tiles[x + 2, y]);
                matchedTiles.Add(tile);
            }
            if (x > 0 && x < tileManager.config.Width - 1 &&
                tiles[x - 1, y] != null && tiles[x + 1, y] != null &&
                tile.tileName == tiles[x - 1, y].tileName && tile.tileName == tiles[x + 1, y].tileName)
            {
                matchedTiles.Add(tiles[x - 1, y]);
                matchedTiles.Add(tiles[x + 1, y]);
                matchedTiles.Add(tile);
            }

            // Check vertical match
            if (y > 1 && tiles[x, y - 1] != null && tiles[x, y - 2] != null &&
                tile.tileName == tiles[x, y - 1].tileName && tile.tileName == tiles[x, y - 2].tileName)
            {
                matchedTiles.Add(tiles[x, y - 1]);
                matchedTiles.Add(tiles[x, y - 2]);
                matchedTiles.Add(tile);
            }
            if (y < tileManager.config.Height - 2 && tiles[x, y + 1] != null && tiles[x, y + 2] != null &&
                tile.tileName == tiles[x, y + 1].tileName && tile.tileName == tiles[x, y + 2].tileName)
            {
                matchedTiles.Add(tiles[x, y + 1]);
                matchedTiles.Add(tiles[x, y + 2]);
                matchedTiles.Add(tile);
            }
            if (y > 0 && y < tileManager.config.Height - 1 &&
                tiles[x, y - 1] != null && tiles[x, y + 1] != null &&
                tile.tileName == tiles[x, y - 1].tileName && tile.tileName == tiles[x, y + 1].tileName)
            {
                matchedTiles.Add(tiles[x, y - 1]);
                matchedTiles.Add(tiles[x, y + 1]);
                matchedTiles.Add(tile);
            }

            return matchedTiles;
        }
	}
}