using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class TilePoolManager:MonoBehaviour
	{

		#region PRIVATE_VARS
		private TilePool tilePool;
		private TilePool powerUpPool;
		#endregion

		#region PUBLIC_VARS
		public bool IsTilePoolEmpty => tilePool.IsEmpty();
		public bool IsPowerUpPoolEmpty => powerUpPool.IsEmpty();
		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}

		private void Start()
		{
			tilePool = new TilePool();
			powerUpPool = new TilePool();
		}

		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<TilePoolManager>();
		}

		#endregion

		#region PUBLIC_METHODS
		public void AddTileToPool(Tile tile)
		{
			tilePool.AddTile(tile);
		}
		public void AddPowerUpToPool(Tile tile)
		{
			powerUpPool.AddTile(tile);
		}
		public Tile GetTileFromPool()
		{
			return tilePool.GetTile();
		}
		public Tile GetPowerUpFromPool()
		{
			return powerUpPool.GetTile();
		}
		
		public void ClearTilePool()
		{
			tilePool.ClearPool();
		}
		public void ClearPowerUpPool()
		{
			powerUpPool.ClearPool();
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}