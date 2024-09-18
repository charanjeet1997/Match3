using System.Collections.Generic;
using JetBrains.Annotations;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class TilePool
	{
		private Stack<Tile> availableTiles;
		
		public TilePool()
		{
			availableTiles = new Stack<Tile>();
		}
		
		public void AddTile(Tile tile)
		{
			if (availableTiles.Contains(tile))
			{
				Debug.LogError("Tile already in pool");
				return;
			}

			availableTiles.Push(tile);
		}
		
		[CanBeNull]
		public Tile GetTile()
		{
			if (availableTiles.Count > 0)
			{
				return availableTiles.Pop();
			}
			return null;
		}
		
		public bool IsEmpty()
		{
			return availableTiles.Count == 0;
		}
		
		public void ClearPool()
		{
			availableTiles.Clear();
		}
	}
}