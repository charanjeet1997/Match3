using System;
using System.Collections.Generic;
using ServiceLocatorFramework;
using UnityEngine;

namespace Games.Match3Game
{
	public class PowerUpTile : Tile
	{
		#region PRIVATE_VARS
		private Dictionary<PowerUpType,PowerUpTileHandler> boosterHandlers;
		private PowerUpNeighbourTilesProvider neighbourTilesProvider;
		#endregion
		
		#region PUBLIC_VARS
		public PowerUpType powerUpType;
		#endregion

		#region UNITY_CALLBACKS

		private void Start()
		{
			neighbourTilesProvider = new PowerUpNeighbourTilesProvider();
			boosterHandlers = new Dictionary<PowerUpType, PowerUpTileHandler>
			{
				{PowerUpType.CrossSection, new CrossSectionPowerUpTileHandler()},
				{PowerUpType.TwoTileRadius, new TwoTileRadiusPowerUpTileHandler()},
				{PowerUpType.ThreeTileRadius, new ThreeTileRadiusPowerUpTileHandler()},
				{PowerUpType.FourTileRadius, new FourTileRadiusPowerUpTileHandler()},
				{PowerUpType.CrushAllOfOneType, new CrushAllOfOneTypePowerUpTileHandler()}
			};
		}

		#endregion

		#region PUBLIC_METHODS
		public void Init(int x, int y, TileName tileName, Sprite sprite, PowerUpType powerUpType)
		{
			base.Init(x, y,TileType.PowerUP, tileName, sprite);
			if(neighbourTilesProvider == null)
				neighbourTilesProvider = new PowerUpNeighbourTilesProvider();
			//neighbours = neighbourTilesProvider.GetTileNeighbours(this, ServiceLocator.Current.Get<TileManager>().Tiles, 4); 
			this.powerUpType = powerUpType;
		}
		
		public void UseBooster()
		{
			boosterHandlers[powerUpType].UseBooster(this);
		}
		#endregion

		#region PRIVATE_METHODS
		
		#endregion
	}

	public abstract class PowerUpTileHandler
	{
		public abstract void UseBooster(PowerUpTile powerUpTile);
		protected abstract List<Tile> GetNeighbours();
	}
	
	public class CrossSectionPowerUpTileHandler : PowerUpTileHandler
	{
		PowerUpTile powerUpTile;
		public override void UseBooster(PowerUpTile powerUpTile)
		{
			Debug.Log("Using Cross Section Booster");
			this.powerUpTile = powerUpTile;
			List<Tile> crossSectionNeighbours = GetNeighbours();
			foreach (Tile neighbour in crossSectionNeighbours)
			{
				if (neighbour is PowerUpTile tile)
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(tile);	
					if(tile != powerUpTile)
						tile.UseBooster();
				}
				else
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(neighbour);
					ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.Challenge.UpdateProgress(neighbour.tileName,1);
				}
				neighbour.HideTile();
				ServiceLocator.Current.Get<TileManager>().Tiles[neighbour.x, neighbour.y] = null;
			}
			ServiceLocator.Current.Get<TilesDropAndSpawnManager>().DropTiles();
		}

		protected override List<Tile> GetNeighbours()
		{
			List<Tile> crossSectionNeighbours = new List<Tile>();
			// Add all the neighbours of the booster tile
			crossSectionNeighbours = powerUpTile.neighbours;
			crossSectionNeighbours.Add(powerUpTile);
			return crossSectionNeighbours;
		}
	}
	
	public class TwoTileRadiusPowerUpTileHandler : PowerUpTileHandler
	{
		PowerUpTile tile;
		PowerUpNeighbourTilesProvider neighbourTilesProvider;
		
		public TwoTileRadiusPowerUpTileHandler()
		{
			neighbourTilesProvider = new PowerUpNeighbourTilesProvider();
		}
		public override void UseBooster(PowerUpTile powerUpTile)
		{
			Debug.Log("Using Two Tile Radius Booster");
			this.tile = powerUpTile;
			List<Tile> neighbours = GetNeighbours();
			neighbours.Add(tile);
			foreach (Tile neighbour in neighbours)
			{
				if (neighbour is PowerUpTile tile)
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(tile);	
					if(tile != powerUpTile)
						tile.UseBooster();
				}
				else
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(neighbour);
					ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.Challenge.UpdateProgress(neighbour.tileName,1);
				}
				neighbour.HideTile();
				ServiceLocator.Current.Get<TileManager>().Tiles[neighbour.x, neighbour.y] = null;
			}
			ServiceLocator.Current.Get<TilesDropAndSpawnManager>().DropTiles();
		}

		protected override List<Tile> GetNeighbours()
		{
			List<Tile> neighbours = new List<Tile>();
			neighbours = neighbourTilesProvider.GetTilesToDestroy(tile, ServiceLocator.Current.Get<TileManager>().Tiles, tile.powerUpType);
			return neighbours;
		}
	}
	
	public class ThreeTileRadiusPowerUpTileHandler : PowerUpTileHandler
	{
		PowerUpTile powerUpTile;
		PowerUpNeighbourTilesProvider neighbourTilesProvider;
		
		public ThreeTileRadiusPowerUpTileHandler()
		{
			neighbourTilesProvider = new PowerUpNeighbourTilesProvider();
		}
		public override void UseBooster(PowerUpTile powerUpTile)
		{
			Debug.Log("Using Three Tile Radius Booster");
			this.powerUpTile = powerUpTile;
			List<Tile> neighbours = GetNeighbours();
			neighbours.Add(powerUpTile);
			foreach (Tile neighbour in neighbours)
			{
				if (neighbour is PowerUpTile tile)
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(tile);
					if(tile != powerUpTile)
						tile.UseBooster();
				}
				else
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(neighbour);
					ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.Challenge.UpdateProgress(neighbour.tileName,1);
				}
				neighbour.HideTile();
				ServiceLocator.Current.Get<TileManager>().Tiles[neighbour.x, neighbour.y] = null;
			}
			ServiceLocator.Current.Get<TilesDropAndSpawnManager>().DropTiles();
		}
		
		protected override List<Tile> GetNeighbours()
		{
			List<Tile> neighbours = new List<Tile>();
			neighbours = neighbourTilesProvider.GetTilesToDestroy(powerUpTile, ServiceLocator.Current.Get<TileManager>().Tiles, powerUpTile.powerUpType);
			return neighbours;
		}
	}
	
	public class FourTileRadiusPowerUpTileHandler : PowerUpTileHandler
	{
		PowerUpTile powerUpTile;
		PowerUpNeighbourTilesProvider neighbourTilesProvider;
		
		public FourTileRadiusPowerUpTileHandler()
		{
			neighbourTilesProvider = new PowerUpNeighbourTilesProvider();
		}
		public override void UseBooster(PowerUpTile powerUpTile)
		{
			Debug.Log("Using Four Tile Radius Booster");
			this.powerUpTile = powerUpTile;
			List<Tile> neighbours = GetNeighbours();
			neighbours.Add(powerUpTile);
			foreach (Tile neighbour in neighbours)
			{
				if (neighbour is PowerUpTile tile)
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(tile);
					if(tile != powerUpTile)
						tile.UseBooster();
				}
				else
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(neighbour);
					ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.Challenge.UpdateProgress(neighbour.tileName,1);
				}
				neighbour.HideTile();
				ServiceLocator.Current.Get<TileManager>().Tiles[neighbour.x, neighbour.y] = null;
			}
			ServiceLocator.Current.Get<TilesDropAndSpawnManager>().DropTiles();
		}
		
		protected override List<Tile> GetNeighbours() 
		{
			List<Tile> neighbours = new List<Tile>();
			neighbours = neighbourTilesProvider.GetTilesToDestroy(powerUpTile, ServiceLocator.Current.Get<TileManager>().Tiles, powerUpTile.powerUpType);
			return neighbours;
		}
	}
	
	public class CrushAllOfOneTypePowerUpTileHandler : PowerUpTileHandler
	{
		public override void UseBooster(PowerUpTile boosterTile)
		{
			Debug.Log("Using Crush All Of One Type Booster");
			var tiles = ServiceLocator.Current.Get<TileManager>().Tiles.GetMaxNumberOfTile();
			tiles.Add(boosterTile);
			ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(boosterTile);
			foreach (var tile in tiles)
			{
				ServiceLocator.Current.Get<BoardManager>().match3GameConfiguration.Challenge.UpdateProgress(tile.tileName,1);
				if(tile is PowerUpTile powerUpTile)
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddPowerUpToPool(powerUpTile);
					//powerUpTile.UseBooster();
					powerUpTile.HideTile();
				}
				else
				{
					ServiceLocator.Current.Get<TilePoolManager>().AddTileToPool(tile);
					tile.HideTile();
				}
				ServiceLocator.Current.Get<TileManager>().Tiles[tile.x, tile.y] = null;
				//ServiceLocator.Current.Get<TilesDropAndSpawnManager>().DropTiles();
			}
			CoroutineExtension.Execute(boosterTile,() => ServiceLocator.Current.Get<TilesDropAndSpawnManager>().DropTiles(),0.3f); 
		}
		
		protected override List<Tile> GetNeighbours()
		{
			List<Tile> neighbours = new List<Tile>();
			return neighbours;
		}
	}
}