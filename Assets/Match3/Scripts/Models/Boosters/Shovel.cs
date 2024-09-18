using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class Shovel : Booster
	{

		#region PRIVATE_VARS
		
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region PUBLIC_METHODS
		
		public Shovel(BoosterType boosterType, Sprite boosterIcon) : base(boosterType, boosterIcon)
		{
		}

		public override void UseBooster(Tile tile, Tile[,] tiles)
		{
			ServiceLocator.Current.Get<TileManager>().DestroyTile(tile.x,tile.y);
		}

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}