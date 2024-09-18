using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class TileSwapper : Booster
	{

		#region UNITY_CALLBACKS

		#endregion

		public TileSwapper(BoosterType boosterType, Sprite boosterIcon) : base(boosterType, boosterIcon)
		{
		}

		public override void UseBooster(Tile tile, Tile[,] tiles)
		{
			ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.Challenge.skipMove = true;
		}
	}
}