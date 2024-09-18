namespace Games.Match3Game
{
	using System.Collections.Generic;
	public class TileNeighbourHandler
	{
		public void RemoveTileNeighbours(int Width,int Height, Tile[,] tiles)
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Tile tile = tiles[x, y];
					if (tile != null)
					{
						tile.neighbours.Clear();
					}
				}
			}
		}
		
		public void SetTileNeighbours(int Width,int Height, Tile[,] tiles)
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Tile tile = tiles[x, y];
					AddNeighbour(tile,tiles);
				}
			}
		}

		public void AddNeighbour(Tile tile, Tile[,] tiles)
		{
			if(tile == null) return;
			// up neighbour
			if (tile.y < tiles.GetLength(1) - 1 && tiles[tile.x, tile.y + 1] != null && tiles[tile.x, tile.y + 1].tileType != TileType.None)
			{
				tile.AddNeighbour(tiles[tile.x, tile.y + 1]);
			}

			// down neighbour
			if (tile.y > 0 && tiles[tile.x, tile.y - 1] != null && tiles[tile.x, tile.y - 1].tileType != TileType.None)
			{
				tile.AddNeighbour(tiles[tile.x, tile.y - 1]);
			}

			// right neighbour
			if (tile.x < tiles.GetLength(0) - 1 && tiles[tile.x + 1, tile.y] != null && tiles[tile.x + 1, tile.y].tileType != TileType.None)
			{
				tile.AddNeighbour(tiles[tile.x + 1, tile.y]);
			}
			
			
			// left neighbour
			if (tile.x > 0 && tiles[tile.x - 1, tile.y] != null && tiles[tile.x - 1, tile.y].tileType != TileType.None)
			{
				tile.AddNeighbour(tiles[tile.x - 1, tile.y]);
			}
			
			// // up right neighbour
			// if (tile.x < tiles.GetLength(0) - 1 && tile.y < tiles.GetLength(1) - 1 && tiles[tile.x + 1, tile.y + 1] != null)
			// {
			// 	tile.AddNeighbour(tiles[tile.x + 1, tile.y + 1]);
			// }
			
			// // up left neighbour
			// if (tile.x > 0 && tile.y < tiles.GetLength(1) - 1 && tiles[tile.x - 1, tile.y + 1] != null)
			// {
			// 	tile.AddNeighbour(tiles[tile.x - 1, tile.y + 1]);
			// }
			
			// // down right neighbour
			// if (tile.x < tiles.GetLength(0) - 1 && tile.y > 0 && tiles[tile.x + 1, tile.y - 1] != null)
			// {
			// 	tile.AddNeighbour(tiles[tile.x + 1, tile.y - 1]);
			// }
			//
			// // down left neighbour
			// if (tile.x > 0 && tile.y > 0 && tiles[tile.x - 1, tile.y - 1] != null)
			// {
			// 	tile.AddNeighbour(tiles[tile.x - 1, tile.y - 1]);
			// }
		}
		
	}
}