using System.Collections.Generic;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class Rake : Booster
	{

		#region PRIVATE_VARS
		
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region PUBLIC_METHODS
		public Rake(BoosterType boosterType,Sprite boosterIcon) : base(boosterType, boosterIcon)
		{
		}

		public override void UseBooster(Tile tile, Tile[,] tiles)
		{
			ClearRowAndColumn(tile, tiles);
		}
		private void ClearRowAndColumn(Tile tile, Tile[,] tiles)
		{
			List<Tile> tilesToDestroy = new List<Tile>();
			for (int col = tile.y; col < tiles.GetLength(1); col++)
			{
				tilesToDestroy.Add(tiles[tile.x, col]);
			}
			
			for (int col = tile.y - 1; col >= 0; col--)
			{
				tilesToDestroy.Add(tiles[tile.x, col]);
			}
			
			// clear row
			for (int row = tile.x; row < tiles.GetLength(0); row++)
			{
				tilesToDestroy.Add(tiles[row, tile.y]);
			}
			
			for (int row = tile.x - 1; row >= 0; row--)
			{
				tilesToDestroy.Add(tiles[row, tile.y]);
			}
			ServiceLocator.Current.Get<TileManager>().DestroyMutipleTiles(tilesToDestroy);
		}

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}