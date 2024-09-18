using System.Collections.Generic;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	[Serializable]
	public class BoosterManager : MonoBehaviour
	{

		#region PRIVATE_VARS
		
		#endregion

		#region PUBLIC_VARS
		public BoosterType selectedBooster;
		private Dictionary<BoosterType, Booster> boosters;
		[SerializeField] Match3GameConfiguration config;
		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}

		private void Start()
		{
			selectedBooster = BoosterType.None;
			boosters = new Dictionary<BoosterType, Booster>();
			foreach (var data in config.boosterData)
			{
				if (data.amount > 0)
				{
					boosters.Add(data.boosterType, CreateBooster(data.boosterType, data.boosterIcon));
				}
			}
		}
		
		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<BoosterManager>();
		}

		#endregion
		
		#region PUBLIC_METHODS
		
		
		public void SelectBooster(BoosterType boosterType)
		{
			selectedBooster = boosterType;
		}

		public void UseBooster( Tile tile, Tile[,] tiles)
		{
			if (boosters.ContainsKey(selectedBooster))
			{
				boosters[selectedBooster].UseBooster(tile, tiles);
				config.boosterData[(int)selectedBooster].amount--;
				selectedBooster = BoosterType.None;
			}
		}
		#endregion

		#region PRIVATE_METHODS
		private Booster CreateBooster(BoosterType dataBoosterType, Sprite dataBoosterIcon)
		{
			switch (dataBoosterType)
			{
				case BoosterType.Shovel:
					return new Shovel(dataBoosterType, dataBoosterIcon);
				case BoosterType.Rake:
					return new Rake(dataBoosterType, dataBoosterIcon);
				case BoosterType.GardeningGloves:
					return new TileSwapper(dataBoosterType, dataBoosterIcon);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		

		#endregion

	}

	[Serializable]
	public class BoosterData
	{
		public BoosterType boosterType;
		public Sprite boosterIcon;
		public int amount;
	}
}